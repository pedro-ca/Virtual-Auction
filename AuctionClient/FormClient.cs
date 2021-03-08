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

        public List<ItemLance> ListaLances = new List<ItemLance>();
        MulticasterClient multicast = new MulticasterClient();

        public FormClient()
        {
            InitializeComponent();
            multicast.CustomEvent += ReceiveMessage;
            multicast.JoinGroup();
            Thread t1 = new Thread(new ThreadStart(this.DoTimeTick));
            t1.Start();
        }

        public X509Certificate2 CreateCert(string user, string key)
        {
            ECDsa ecdsa = ECDsa.Create(); // generate asymmetric key pair
            CertificateRequest req = new CertificateRequest("cn=" + key, ecdsa, HashAlgorithmName.SHA256);
            X509Certificate2 cert = req.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(5));
            cert.FriendlyName = user;

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
                string answer = Encoding.UTF8.GetString(bytesToRead, 0, bytesRead);
                if (answer == "!deny=")
                {
                    MessageBox.Show("Certificate does not exist or invalid format.", "Auth Error", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else if (answer.StartsWith("!key="))
                {
                    answer = answer.Substring(multicast.comandoClear.Length);
                    multicast.privateKey = answer;
                    MessageBox.Show("Sucessfully logged in. Current Private Key:\n"+ multicast.privateKey, "Auth Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                client.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("SendAuthRequest Error:\n " + e.Message, "Exception Caught", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SendLance(float novoValorLance)
        {
            if (dataGridItemLance.CurrentCell != null)
            {
                multicast.SendBuyMessage(dataGridItemLance.CurrentCell.RowIndex, novoValorLance, multicast.participanteAtual);
            }
        }
        public void UpdateDataGridItemLance()
        {
            dataGridItemLance.Invoke(new MethodInvoker(() => { dataGridItemLance.Rows.Clear(); }));
            foreach (ItemLance item in ListaLances){
                if (dataGridItemLance.InvokeRequired)
                {
                    dataGridItemLance.Invoke(new MethodInvoker(() => { dataGridItemLance.Rows.Add(item.NomeItem, item.EstaDisponivel, item.DonoAtual, "$ " + item.ValorAtual, "$ " + item.ValorAdicionalMinimo, item.TempoRestante); }));
                }
                else
                {
                    dataGridItemLance.Rows.Add(item.NomeItem, item.EstaDisponivel, item.DonoAtual, "$ " + item.ValorAtual, "$ " + item.ValorAdicionalMinimo, item.TempoRestante);
                }
            }
        }

        public void DoTimeTick()
        {
            while (!this.IsDisposed)
            {
                if (ListaLances.Count > 0 && dataGridItemLance != null)
                {
                    foreach (ItemLance itemLance in ListaLances)
                    {
                        try
                        {
                            if (itemLance.EstaDisponivel)
                            {
                                int listIndex = ListaLances.IndexOf(itemLance);

                                if (itemLance.TempoRestante > 0)
                                {
                                    itemLance.TempoRestante--;
                                    ListaLances[listIndex].TempoRestante = itemLance.TempoRestante;
                                    dataGridItemLance.Rows[listIndex].Cells[5].Value = itemLance.TempoRestante;
                                }
                                else
                                {
                                    itemLance.EstaDisponivel = false;
                                    ListaLances[listIndex].EstaDisponivel = itemLance.EstaDisponivel;
                                    dataGridItemLance.Rows[listIndex].Cells[1].Value = itemLance.EstaDisponivel;
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

        private void ReceiveMessage(string message)
        {
            try
            {
                if (message.Length > 0 && message.StartsWith("#"))
                {
                    //messages only treated by the audit clients 
                    if (message.StartsWith(multicast.comandoClear))     //Clear operation. Format: #clear=
                    {
                        message = message.Substring(multicast.comandoClear.Length);
                        ListaLances.Clear();
                        UpdateDataGridItemLance();
                        multicast.SendJoinMessage(multicast.participanteAtual);
                        MessageBox.Show("O Servidor de Lances reiniciou. Todos os lances foram limpados.");
                    }
                    else if (message.StartsWith(multicast.comandoUpdate))   //Update operation. Format: #update=List<ItemLance>
                    {
                        message = message.Substring(multicast.comandoUpdate.Length);

                        ListaLances = JsonSerializer.Deserialize<List<ItemLance>>(message);
                        UpdateDataGridItemLance();
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
                float valorAtual = ListaLances[dataGridItemLance.CurrentCell.RowIndex].ValorAtual;
                float valorAdicionalMinimo = ListaLances[dataGridItemLance.CurrentCell.RowIndex].ValorAdicionalMinimo;

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
            string username = txtBoxUsername.Text;
            string certificateKey = txtBoxCertificateKey.Text;
            string serverIp = txtBoxServerIp.Text;

            userCertificate = CreateCert(username, certificateKey);
            SendAuthRequest(userCertificate, serverIp);
        }

        private void txtBoxServerIp_TextChanged(object sender, EventArgs e)
        {
            txtBoxServerIp.Text = txtBoxServerIp.Text.Replace(" ", "");
        }
    }
}

