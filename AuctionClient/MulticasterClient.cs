using System.Text.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AuctionModel;

namespace AuctionClient
{
    public delegate void MyCustomEvent(string value);

    public class MulticasterClient : UserControl
    {
        public event MyCustomEvent CustomEvent;

        private const int TIME_TO_LIVE = 50;
        private UdpClient client = null;
        private IPAddress group = null;
        private int port = 0;
        private IPEndPoint multiCastEP = null;
        private bool stayAlive = true;
        private Thread t2 = null;

        public string privateSessionKey;          //yes, i know Ive declared a private key as public. not secure at all, but it just works
        RijndaelManaged rijndaelEncryption = new RijndaelManaged();

        public Participante participanteAtual;

        public readonly string comandoClear = "#clear=";
        public readonly string comandoUpdate = "#update=";
        public readonly string comandoBuy = "#buy=";
        public readonly string comandoJoin = "#join=";
        public readonly string comandoKey = "!key=";
        public readonly string comandoDeny = "!deny=";
        //talvez adicionar um comando chamado comandoMessage, onde client e servidores adicionam messagens em um datagridview que faz log de transações.

        public void SendJoinMessage(Participante participante)      
        {
            string message = comandoJoin + JsonSerializer.Serialize(participante);
            SendMessage(message);
        }

        public void SendBuyMessage(int rowIndex, float valorLance, Participante participante)
        {
            string message = comandoBuy + rowIndex +","+ valorLance +","+ JsonSerializer.Serialize(participante);
            SendMessage(message);
        }

        private void SendMessage(String message)
        {
            if (message.Length > 0)
            {
                Byte[] buff;
                buff = EncryptStringToBytes(message, rijndaelEncryption.Key, rijndaelEncryption.IV);

                client.Send(buff, buff.Length, multiCastEP);
            }
        }

        public void JoinGroup()
        {
            try
            {
                if (!int.TryParse("10002", out port))     //valor fixo
                    throw new ApplicationException("Invalid Port Number");
                if (!IPAddress.TryParse("224.0.0.251", out group))   //valor fixo
                    throw new ApplicationException("Invalid Multicast Group Address");

                rijndaelEncryption.Key = Encoding.UTF8.GetBytes(privateSessionKey);
                rijndaelEncryption.IV = Encoding.UTF8.GetBytes(privateSessionKey);        //seria melhor se o iv fosse aleatorio...

                client = new UdpClient();
                client.Client.ExclusiveAddressUse = false;
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                client.JoinMulticastGroup(group, TIME_TO_LIVE);
                multiCastEP = new IPEndPoint(group, port);
                client.Client.Bind(new IPEndPoint(IPAddress.Any, port));
                this.stayAlive = true;
                t2 = new Thread(this.RunThread);
                t2.Start();

                SendJoinMessage(participanteAtual);
            }
            catch (Exception e)
            {
                MessageBox.Show("JoinGroup Error: \n" + e.Message, "Exception Caught", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LeaveGroup()
        {
            try
            {
                if (group != null && t2 != null && client != null)
                {
                    Thread.Sleep(500);
                    stayAlive = false;
                    t2.Abort();
                    client.DropMulticastGroup(group);
                    client.Close();
                    client = null;
                    group = null;
                    multiCastEP = null;

                    Thread.Sleep(500);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("LeaveGroup Error:\n" + e.Message, "Exception Caught", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunThread()
        {
            Byte[] buff;
            String message;
            IPEndPoint ep = null;

            while (stayAlive)
            {
                buff = client.Receive(ref ep);
                message = DecryptStringFromBytes(buff, rijndaelEncryption.Key, rijndaelEncryption.IV);
                CustomEvent?.Invoke(message);       //invoke receive message event
                Thread.Sleep(10);
            }
        }

        public byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }
        
        public string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;

            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                try
                {
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch (Exception)    //se decryption não der certo, faz conversao normal.
                {
                    plaintext = Encoding.Unicode.GetString(cipherText);
                }
            }
            return plaintext;
        }
    }
}
