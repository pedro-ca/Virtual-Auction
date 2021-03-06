using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuctionClient
{
    public partial class FormClient : Form
    {
        public List<ItemLance> ListaLances = new List<ItemLance>();
        public bool isAuditServer = true;
        MulticasterClient multicast = new MulticasterClient();

        public FormClient()
        {
            InitializeComponent();
            multicast.CustomEvent += ReceiveMessage;
            multicast.JoinGroup();
            Thread t1 = new Thread(new ThreadStart(this.DoTimeTick));
            t1.Start();
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

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.DoEvents();
            multicast.LeaveGroup();
        }
    }
}

