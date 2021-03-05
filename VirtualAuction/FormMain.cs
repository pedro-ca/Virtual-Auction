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

namespace LeilaoServer
{
    public partial class FormMain : Form
    {
        public List<Participante> ListaParticipantes = new List<Participante>();
        public List<ItemLance> ListaLances = new List<ItemLance>();
        public bool isAuditServer = true;
        Multicaster multicast = new Multicaster();

        public FormMain()
        {
            InitializeComponent();
            multicast.CustomEvent += ReceiveMessage;
            multicast.JoinGroup();
            Thread t1 = new Thread(new ThreadStart(this.DoTimeTick));
            t1.Start();
        }

        public void AddParticipante(string nomeUsuario, string ip, string certificadoDigital)   //server side
        {
            Participante participanteTemp = new Participante(nomeUsuario, ip, certificadoDigital);
            ListaParticipantes.Add(participanteTemp);
            dataGridParticipante.Rows.Add(participanteTemp.NomeUsuario, participanteTemp.Ip);
        }

        public void AddLance(string nomeItem, float valorInicial, float valorAdicionalMinimo, int tempoRestante)    //server side
        {
            ItemLance itemLanceTemp = new ItemLance(nomeItem, valorInicial, valorAdicionalMinimo, valorInicial, "Leiloeiro", tempoRestante, true);
            ListaLances.Add(itemLanceTemp);
            dataGridItemLance.Rows.Add(itemLanceTemp.NomeItem, itemLanceTemp.EstaDisponivel, itemLanceTemp.DonoAtual, itemLanceTemp.ValorAtual, itemLanceTemp.ValorAdicionalMinimo, itemLanceTemp.TempoRestante);

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

                    //dataGridItemLance.Rows[listIndex].Cells[3].Value = itemLance.ValorAtual;
                    //dataGridItemLance.Rows[listIndex].Cells[2].Value = itemLance.DonoAtual;

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
            foreach (ItemLance item in ListaLances){
                if (dataGridItemLance.InvokeRequired)
                {
                    dataGridItemLance.Invoke(new MethodInvoker(() => { dataGridItemLance.Rows.Add(item.NomeItem, item.EstaDisponivel, item.DonoAtual, item.ValorAtual, item.ValorAdicionalMinimo, item.TempoRestante); }));
                }
                else
                {
                    dataGridItemLance.Rows.Add(item.NomeItem, item.EstaDisponivel, item.DonoAtual, item.ValorAtual, item.ValorAdicionalMinimo, item.TempoRestante);
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
                                else    //server side
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
                            Console.WriteLine("Error: " + e.Message);
                        }
                    }
                }
                Thread.Sleep(1000);
            }
            //dataGridView1.Update();
        }

        private void ReceiveMessage(string message)     //NOTA: CLIENTS NÃO DEVEM PODER MANDAR COMANDOS UPDATE E CLEAR
        {
            if (message.Length > 0 && message.StartsWith("#"))
            {
                //messages only treated by the audit server 
                if (message.StartsWith(multicast.comandoJoin))      //Join Operation. Format: #join= Participante
                {
                    message = message.Substring(multicast.comandoJoin.Length);
                    multicast.SendUpdateMessage(ListaLances);
                    MessageBox.Show("comandoJoin = " + message);
                }
                else if (message.StartsWith(multicast.comandoBuy))      //Buy Operation. Format: #buy= index, value
                {
                    message = message.Substring(multicast.comandoBuy.Length);
                    MessageBox.Show("comandoBuy = " + message);
                }
                //messages only treated by the audit clients 
                else if (message.StartsWith(multicast.comandoClear))     //Clear operation. Format: #clear=
                {
                    message = message.Substring(multicast.comandoClear.Length);
                    ListaLances.Clear();
                    ListaParticipantes.Clear();
                    //dataGridView1.Rows.Clear();
                    //dataGridView2.Rows.Clear();
                    MessageBox.Show("comandoClear = " + message);
                }
                else if (message.StartsWith(multicast.comandoUpdate))   //Update operation. Format: #update=List<ItemLance>
                {
                    message = message.Substring(multicast.comandoUpdate.Length);

                    ListaLances = JsonSerializer.Deserialize<List<ItemLance>>(message);
                    UpdateDataGridItemLance();
                    //this.dataGridItemLance.DataSource = ListaLances;
                    MessageBox.Show("comandoUpdate = " + message);
                }
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
        }

        private void btnNovoParticipante_Click(object sender, EventArgs e)
        {
            AddParticipante("DESGRAÇAAA", "5451.124", "CERTIFICADO FODA");
        }

        private void btnNovoLance_Click(object sender, EventArgs e)
        {
            if (dataGridItemLance.CurrentCell != null)
            {
                int listIndex = dataGridItemLance.CurrentCell.RowIndex;
                ItemLance itemLance = ListaLances[listIndex];
                if (itemLance.EstaDisponivel)
                {
                    UpdateLanceValorAtual(itemLance, ListaParticipantes[0], 1000);
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.DoEvents();
            multicast.LeaveGroup();
        }
    }
}

