// Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace LeilaoServer
{
    public delegate void MyCustomEvent(string value);

    public class Multicaster : UserControl
    {
        public event MyCustomEvent CustomEvent;

        private const int TIME_TO_LIVE = 50;
        private UdpClient client = null;
        private IPAddress group = null;
        private int port = 0;
        private IPEndPoint multiCastEP = null;
        private bool stayAlive = true;
        private Thread receiveThread = null;
        RijndaelManaged rijndaelEncryption = new RijndaelManaged();

        public readonly string comandoClear = "#clear=";
        public readonly string comandoUpdate = "#update=";
        public readonly string comandoBuy = "#buy=";
        public readonly string comandoJoin = "#join=";

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

        public void SendBuyMessage()
        {
            throw new NotImplementedException();
        }

        public void SendJoinMessage()
        {
            throw new NotImplementedException();
        }

        private void SendMessage(String message)     //mudar para private depois
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

                string rijKey = "default";  //valor fixo

                while (rijKey.Length < 16)  //cambiarra f*dida. 16 caraceres é string ideal pra ser usado como key e iv
                {
                    rijKey += " ";
                }

                rijndaelEncryption.Key = Encoding.UTF8.GetBytes(rijKey);
                rijndaelEncryption.IV = Encoding.UTF8.GetBytes(rijKey);        //seria melhor se o iv fosse aleatorio...


                client = new UdpClient();
                client.Client.ExclusiveAddressUse = false;
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                client.JoinMulticastGroup(group, TIME_TO_LIVE);
                multiCastEP = new IPEndPoint(group, port);
                client.Client.Bind(new IPEndPoint(IPAddress.Any, port));
                this.stayAlive = true;
                receiveThread = new Thread(this.RunThread);
                receiveThread.Start();

                SendClearMessage();
            }
            catch (Exception e)
            {
                MessageBox.Show("An exception occurred when attempting to Join roup: \n" + e.ToString(), "Join Group Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LeaveGroup()
        {
            try
            {
                if (group != null)
                {
                    Thread.Sleep(500);
                    stayAlive = false;
                    receiveThread.Abort();
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
                MessageBox.Show("An exception occurred when attempting to Leave Group: \n" + e.ToString(), "Leave Group Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
