using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;
using AuctionModel;

namespace AuctionClient
{
    public partial class FormClient : Form
    {
        public const int serverPort = 10002;
        X509Certificate2 userCertificate;

        public List<AuctionItem> ItemList = new List<AuctionItem>();
        MulticasterClient multicast = new MulticasterClient();

        private string privateClientKey; 

        public FormClient()
        {
            InitializeComponent();
            multicast.CustomEvent += ReceiveMulticastMessage;
            Thread t1 = new Thread(new ThreadStart(this.DoTimeTick));
            t1.Start();
        }

        public X509Certificate2 CreateCert(string user)
        {
            ECDsa ecdsa = ECDsa.Create(); // generate asymmetric key pair
            CertificateRequest req = new CertificateRequest("cn=" + user, ecdsa, HashAlgorithmName.SHA256);
            X509Certificate2 cert = req.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(5));

            return cert;
        }

        public void SendAuthRequest(X509Certificate2 certificate, string serverIp)
        {
            try
            {
                TcpClient client = new TcpClient(serverIp, serverPort);
                NetworkStream nwStream = client.GetStream();
                byte[] bytesToSend = certificate.Export(X509ContentType.Cert);  //= ASCIIEncoding.Unicode.GetBytes("beep?");

                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);

                string answerTemp = Encoding.Unicode.GetString(bytesToRead, 0, bytesRead);

                while (privateClientKey.Length < 16)  //cambiarra f*dida. 16 caraceres é string ideal pra ser usado como key e iv
                {
                    privateClientKey = privateClientKey + " ";
                }

                RijndaelManaged rijndaelEncryption = new RijndaelManaged();
                rijndaelEncryption.Key = Encoding.UTF8.GetBytes(privateClientKey);
                rijndaelEncryption.IV = Encoding.UTF8.GetBytes(privateClientKey);        //seria melhor se o iv fosse aleatorio...

                string answer = multicast.DecryptStringFromBytes(Encoding.Unicode.GetBytes(answerTemp), rijndaelEncryption.Key, rijndaelEncryption.IV);;  //Encoding.UTF8.GetString(bytesToRead, 0, bytesRead);

                if (answer.StartsWith(multicast.comandoDeny))
                {
                    MessageBox.Show("Certificate does not exist or invalid format.", "Auth Error", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else if (answer.StartsWith(multicast.comandoKey))
                {
                    answer = answer.Substring(multicast.comandoKey.Length);
                    multicast.privateSessionKey = answer;
                    multicast.participanteAtual = new AuctionParticipant(txtBoxUsername.Text, "exampleip") ;
                    multicast.JoinGroup();
                    groupBoxItens.Enabled = true;
                    groupBoxConnect.Enabled = false;
                    MessageBox.Show("Sucessfully logged in. Current Session's Private Key:\n"+ multicast.privateSessionKey, "Auth Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Private Client Key does not exist or invalid format.", "Auth Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                client.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("SendAuthRequest Error:\n" + e.Message, "Exception Caught", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SendBid(float novoValorLance)
        {
            if (dataGridItemLance.CurrentCell != null)
            {
                multicast.SendBuyMessage(dataGridItemLance.CurrentCell.RowIndex, novoValorLance, multicast.participanteAtual);
            }
        }
        public void UpdateDataGridAuctionItem()
        {
            dataGridItemLance.Invoke(new MethodInvoker(() => { dataGridItemLance.Rows.Clear(); }));
            foreach (AuctionItem item in ItemList){
                if (dataGridItemLance.InvokeRequired)
                {
                    dataGridItemLance.Invoke(new MethodInvoker(() => { dataGridItemLance.Rows.Add(item.ItemName, item.IsAvailable, item.CurrentOwner, "$ " + item.CurrentValue, "$ " + item.MinAditionalValue, item.RemainingTime); }));
                }
                else
                {
                    dataGridItemLance.Rows.Add(item.ItemName, item.IsAvailable, item.CurrentOwner, "$ " + item.CurrentValue, "$ " + item.MinAditionalValue, item.RemainingTime);
                }
            }
        }

        public void DoTimeTick()
        {
            while (!this.IsDisposed)
            {
                if (ItemList.Count > 0 && dataGridItemLance != null)
                {
                    foreach (AuctionItem itemLance in ItemList)
                    {
                        try
                        {
                            if (itemLance.IsAvailable)
                            {
                                int listIndex = ItemList.IndexOf(itemLance);

                                if (itemLance.RemainingTime > 0)
                                {
                                    itemLance.RemainingTime--;
                                    ItemList[listIndex].RemainingTime = itemLance.RemainingTime;
                                    dataGridItemLance.Rows[listIndex].Cells[5].Value = itemLance.RemainingTime;
                                }
                                else
                                {
                                    itemLance.IsAvailable = false;
                                    ItemList[listIndex].IsAvailable = itemLance.IsAvailable;
                                    dataGridItemLance.Rows[listIndex].Cells[1].Value = itemLance.IsAvailable;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e.Message);
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }

        private void ReceiveMulticastMessage(string message)
        {
            try
            {
                if (message.Length > 0 && message.StartsWith("#"))
                {
                    //messages only treated by the audit clients 
                    if (message.StartsWith(multicast.comandoClear))     //Clear operation. Format: #clear=
                    {
                        message = message.Substring(multicast.comandoClear.Length);
                        ItemList.Clear();
                        UpdateDataGridAuctionItem();
                        multicast.SendJoinMessage(multicast.participanteAtual);
                        MessageBox.Show("The server restared. All bids have been reset.");
                    }
                    else if (message.StartsWith(multicast.comandoUpdate))   //Update operation. Format: #update=List<ItemLance>
                    {
                        message = message.Substring(multicast.comandoUpdate.Length);

                        ItemList = JsonSerializer.Deserialize<List<AuctionItem>>(message);
                        UpdateDataGridAuctionItem();
                    }
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("Error: " + e.Message);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.DoEvents();
            multicast.LeaveGroup();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridItemLance.CurrentCell != null)
            {
                float valorAtual = ItemList[dataGridItemLance.CurrentCell.RowIndex].CurrentValue;
                float valorAdicionalMinimo = ItemList[dataGridItemLance.CurrentCell.RowIndex].MinAditionalValue;

                var telaAdicionarItem = new BuyItem(valorAtual, valorAdicionalMinimo)
                {
                    Owner = this
                };
                telaAdicionarItem.Show();
            }
            else
            {
                MessageBox.Show("Selecione uma linha da tabela antes de tentar enviar um novo lance.", "Operação Inválida");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBoxCertificateKey.Text) && !String.IsNullOrEmpty(txtBoxServerIp.Text) && !String.IsNullOrEmpty(txtBoxUsername.Text))
            {
                DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja se conectar ao servidor?", "Confirmação necessária", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string username = txtBoxUsername.Text;
                    string serverIp = txtBoxServerIp.Text;
                    privateClientKey = txtBoxCertificateKey.Text;

                    userCertificate = CreateCert(username);
                    SendAuthRequest(userCertificate, serverIp);
                }
            }
            else
            {
                MessageBox.Show("Fill all fields before trying to connect with the server.", "Invalid data.");
            }
        }

        private void txtBoxServerIp_TextChanged(object sender, EventArgs e)
        {
            txtBoxServerIp.Text = txtBoxServerIp.Text.Replace(" ", "");
        }
    }
}

