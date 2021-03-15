using AuctionModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

namespace AuctionServer
{
    public partial class FormServer : Form
    {
        public const int serverPort = 10002;
        public const string serverAddress = "127.0.0.1";
        private Dictionary<string, string> dictClientKey = new Dictionary<string, string>();


        IPAddress localAdd;
        TcpListener listener;
        TcpClient client;

        public List<Participante> ListaParticipantes = new List<Participante>();
        public List<ItemLance> ListaLances = new List<ItemLance>();

        MulticasterServer multicast = new MulticasterServer();

        public FormServer()
        {
            InitializeClientKeys();

            localAdd = IPAddress.Parse(serverAddress);
            listener = new TcpListener(localAdd, serverPort);
            listener.Start();

            InitializeComponent();
            multicast.CustomEvent += ReceiveMessage;
            multicast.JoinGroup();
            this.Text = "Leilão Server - Chave AES da sessão: " + multicast.privateSessionKey;
            Thread t1 = new Thread(new ThreadStart(this.DoTimeTick));
            t1.Start();
            Thread t2 = new Thread(new ThreadStart(this.ReceiveAuthRequest));
            t2.Start();
        }
    
        public void InitializeClientKeys()
        {
            dictClientKey.Add("CN=Anonymous", "hackerman");
            dictClientKey.Add("CN=Beep", "beep");
            dictClientKey.Add("CN=Faustao", "olokobixo");
            dictClientKey.Add("CN=eren", "Tatakae");
            dictClientKey.Add("CN=heavyTF2", "pootis");
        }

        public void ReceiveAuthRequest()
        {
            while (!this.IsDisposed && Thread.CurrentThread.IsAlive)
            {
                try
                {
                    client = listener.AcceptTcpClient();

                    NetworkStream nwStream = client.GetStream();
                    byte[] buffer = new byte[client.ReceiveBufferSize];

                    int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);


                    byte[] bytesToSend;
                    try
                    {
                        X509Certificate2 userCert = new X509Certificate2();
                        userCert.Import(buffer);
                        //MessageBox.Show("Received = " + userCert.ToString());

                        string privateClientKey;
                        if(dictClientKey.TryGetValue(userCert.Subject, out privateClientKey)) //(dictClientKey.ContainsKey(userCert.Subject))
                        {
                            while (privateClientKey.Length < 16)  //cambiarra f*dida. 16 caraceres é string ideal pra ser usado como key e iv
                            {
                                privateClientKey = privateClientKey + " ";
                            }

                            RijndaelManaged rijndaelEncryption = new RijndaelManaged();
                            rijndaelEncryption.Key = Encoding.UTF8.GetBytes(privateClientKey);
                            rijndaelEncryption.IV = Encoding.UTF8.GetBytes(privateClientKey);        //seria melhor se o iv fosse aleatorio...

                            bytesToSend = multicast.EncryptStringToBytes(multicast.comandoKey + multicast.privateSessionKey, rijndaelEncryption.Key, rijndaelEncryption.IV);

                            string debugString = multicast.DecryptStringFromBytes(bytesToSend, rijndaelEncryption.Key, rijndaelEncryption.IV);
                            rijndaelEncryption.Dispose();
                        }
                        else
                        {
                            throw new FormatException();
                        }
                    }
                    catch (FormatException)
                    {
                        bytesToSend = ASCIIEncoding.Unicode.GetBytes(multicast.comandoDeny);
                    }

                    nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                }
                catch (Exception e)
                {
                    MessageBox.Show("ReceiveAuthRequest Error:\n " + e.Message, "Exception Caught", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void AddParticipante(Participante novoParticipante)   //server side
        {
            ListaParticipantes.Add(novoParticipante);
            UpdateDataGridParticipante();
        }

        public void AddLance(string nomeItem, float valorInicial, float valorAdicionalMinimo, int tempoRestante)    //server side
        {
            ItemLance itemLanceTemp = new ItemLance(nomeItem, valorInicial, valorAdicionalMinimo, valorInicial, "Leiloeiro", tempoRestante, true);
            ListaLances.Add(itemLanceTemp);
            dataGridItemLance.Rows.Add(itemLanceTemp.NomeItem, itemLanceTemp.EstaDisponivel, itemLanceTemp.DonoAtual, "$ " + itemLanceTemp.ValorAtual, "$ " + itemLanceTemp.ValorAdicionalMinimo, itemLanceTemp.TempoRestante);

            multicast.SendUpdateMessage(ListaLances);
        }

        public string UpdateLanceValorAtual(ItemLance itemLance, Participante participante, float valorLance)   //server side
        {
            if (itemLance.EstaDisponivel && ListaLances.Contains(itemLance))
            {
                if (valorLance >= itemLance.ValorAtual + itemLance.ValorAdicionalMinimo)
                {
                    int listIndex = ListaLances.IndexOf(itemLance);

                    itemLance.ValorAtual = valorLance;
                    itemLance.DonoAtual = participante.NomeUsuario;

                    ListaLances[listIndex].ValorAtual = itemLance.ValorAtual;
                    ListaLances[listIndex].DonoAtual = itemLance.DonoAtual;

                    UpdateDataGridItemLance();

                    multicast.SendUpdateMessage(ListaLances);

                    return "Lance sucedido para o item '" + itemLance.NomeItem + "': \n  Lance de " + valorLance + " realizado com sucesso. \n  Novo dono do item: " + itemLance.DonoAtual;
                }
                else
                {
                    return "Lance inválido para o item '" + itemLance.NomeItem + "':\n  Valor de " + valorLance + " muito baixo. \n  Novos lances precisam de um incremento mínimo de " + itemLance.ValorAdicionalMinimo + " sobre o valor atual de " + itemLance.ValorAtual;
                }
            }
            else
            {
                return "Lance inválido para o item '" + itemLance.NomeItem + "': \n  O item não está mais disponível ou não existe.";
            }
        }

        public void UpdateDataGridItemLance()
        {
            dataGridItemLance.Invoke(new MethodInvoker(() => { dataGridItemLance.Rows.Clear(); }));
            foreach (ItemLance item in ListaLances)
            {
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

        public void UpdateDataGridParticipante()
        {
            dataGridParticipante.Invoke(new MethodInvoker(() => { dataGridParticipante.Rows.Clear(); }));
            foreach (Participante participante in ListaParticipantes)
            {
                if (dataGridParticipante.InvokeRequired)
                {
                    dataGridParticipante.Invoke(new MethodInvoker(() => { dataGridParticipante.Rows.Add(participante.NomeUsuario, participante.Ip); }));
                }
                else
                {
                    dataGridParticipante.Rows.Add(participante);
                }
            }
        }

        public void DoTimeTick()
        {
            while (!this.IsDisposed && Thread.CurrentThread.IsAlive)
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

                                    multicast.SendUpdateMessage(ListaLances);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("DoTimeTick Error:\n" + e.Message, "Exception Caught", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    //messages only treated by the audit server 
                    if (message.StartsWith(multicast.comandoJoin))      //Join Operation. Format: #join= Participante
                    {
                        message = message.Substring(multicast.comandoJoin.Length);
                        multicast.SendUpdateMessage(ListaLances);
                        AddParticipante(JsonSerializer.Deserialize<Participante>(message));
                    }
                    else if (message.StartsWith(multicast.comandoBuy))      //Buy Operation. Format: #buy= index, value, Participante
                    {
                        message = message.Substring(multicast.comandoBuy.Length);

                        int indexList = int.Parse(message.Substring(0, message.IndexOf(',')));      //get content before first ',', The index of datagridview
                        message = message.Substring(message.IndexOf(',') + 1);       //remove content from 0 to ','

                        float audictValue = float.Parse(message.Substring(0, message.IndexOf(',')));   //get content before the second ',', The value of the transaction 
                        message = message.Substring(message.IndexOf(',') + 1);       //remove content from 0 to ','

                        Participante newOwner = JsonSerializer.Deserialize<Participante>(message);      //deserialize the remaining message to Participante object

                        UpdateLanceValorAtual(ListaLances[indexList], newOwner, audictValue);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var telaAdicionarItem = new AddItem
            {
                Owner = this
            };
            telaAdicionarItem.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridItemLance.CurrentCell != null)
            {
                DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja remover este item do leilão?", "Confirmação necessária", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int listIndex = dataGridItemLance.CurrentCell.RowIndex;
                    ItemLance itemLance = ListaLances[listIndex];
                    if (itemLance.EstaDisponivel)
                    {
                        itemLance.TempoRestante = 0;

                        ListaLances[listIndex].TempoRestante = itemLance.TempoRestante;
                        dataGridItemLance.Rows[listIndex].Cells[5].Value = itemLance.TempoRestante;
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione uma linha da tabela antes de tentar remover um item.", "Operação Inválida");
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.DoEvents();
            if(client != null)
            {
                client.Close();
            }
            if (listener != null)
            {
                listener.Stop();
            }
            multicast.LeaveGroup();
        }
    }
}

