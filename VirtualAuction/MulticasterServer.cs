using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AuctionModel;

namespace AuctionServer
{
    public delegate void MyCustomEvent(string value);

    public class MulticasterServer : UserControl
    {
        public event MyCustomEvent CustomEvent;

        private const int TIME_TO_LIVE = 50;
        private UdpClient client = null;
        private IPAddress group = null;
        private int port = 0;
        private IPEndPoint multiCastEP = null;
        private bool stayAlive = true;
        private Thread t3 = null;

        public string privateKey;       //yes, i know Ive declared a private key as public. not secure at all, but it just works
        RijndaelManaged rijndaelEncryption = new RijndaelManaged();

        public readonly string comandoClear = "#clear=";
        public readonly string comandoUpdate = "#update=";
        public readonly string comandoBuy = "#buy=";
        public readonly string comandoJoin = "#join=";
        public readonly string comandoKey = "!key=";
        public readonly string comandoDeny = "!deny=";

        public void SendClearMessage()
        {
            string message = comandoClear;
            SendMessage(message);
        }

        public void SendUpdateMessage(List<ItemLance> ListaLances)
        {
            string message = comandoUpdate + JsonSerializer.Serialize(ListaLances);
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

                GerateRijandelKey();

                rijndaelEncryption.Key = Encoding.UTF8.GetBytes(privateKey);
                rijndaelEncryption.IV = Encoding.UTF8.GetBytes(privateKey);        //seria melhor se o iv fosse aleatorio...

                client = new UdpClient();
                client.Client.ExclusiveAddressUse = false;
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                client.JoinMulticastGroup(group, TIME_TO_LIVE);
                multiCastEP = new IPEndPoint(group, port);
                client.Client.Bind(new IPEndPoint(IPAddress.Any, port));
                this.stayAlive = true;
                t3 = new Thread(this.RunThread);
                t3.Start();

                SendClearMessage();
            }
            catch (Exception e)
            {
                MessageBox.Show("JoinGroup Error: \n" + e.Message, "Exception Caught", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GerateRijandelKey()         //gera uma key aes aleatoria.  16 caraceres é string ideal pra ser usado como key e iv
        {
            Random rd = new Random();   
            int stringLength = 16;
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            privateKey = new string(chars);
        }

        public void LeaveGroup()
        {
            try
            {
                if (group != null)
                {
                    Thread.Sleep(500);
                    stayAlive = false;
                    t3.Abort();
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
                MessageBox.Show("Leave Group Error: \n" + e.Message, "Exception Caught", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
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
        
        private string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
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
