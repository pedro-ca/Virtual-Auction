using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeilaoServer
{
    public partial class FormMain : Form
    {
        public List<Participante> ListaParticipantes = new List<Participante>();
        public List<ItemLance> ListaLances = new List<ItemLance>();
        Multicaster multicast = new Multicaster();

        public FormMain()
        {
            InitializeComponent();
            multicast.CustomEvent += ReceiveMessage;
            multicast.JoinGroup();
            Thread t1 = new Thread(new ThreadStart(DoTimeTick));
            t1.Name = "TimerThread - ";
            t1.Start();
        }

        public void AddParticipante(string nomeUsuario, string ip, string certificadoDigital)
        {
            Participante participanteTemp = new Participante(nomeUsuario, ip, certificadoDigital);
            ListaParticipantes.Add(participanteTemp);
            dataGridView2.Rows.Add(participanteTemp.NomeUsuario, participanteTemp.Ip);
        }

        public void AddLance(string nomeItem, float valorInicial, float valorAdicionalMinimonceMinimo, int tempoRestante)
        {
            ItemLance itemLanceTemp = new ItemLance(nomeItem, valorInicial, valorAdicionalMinimonceMinimo, tempoRestante);
            ListaLances.Add(itemLanceTemp);
            dataGridView1.Rows.Add(itemLanceTemp.NomeItem, itemLanceTemp.EstaDisponivel, itemLanceTemp.DonoAtual, itemLanceTemp.ValorAtual, itemLanceTemp.ValorAdicionalMinimo, itemLanceTemp.TempoRestante);

            multicast.SendInsertMessage(dataGridView1.Rows.Count + 1, itemLanceTemp);
        }

        public string UpdateLanceValorAtual(ItemLance itemLance, Participante participante, float valorLance)
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

                    dataGridView1.Rows[listIndex].Cells[3].Value = itemLance.ValorAtual;
                    dataGridView1.Rows[listIndex].Cells[2].Value = itemLance.DonoAtual;

                    //ListaLances.Insert(listIndex, itemLance);
                    //dataGridView1.Rows.Insert(listIndex, itemLance.NomeItem, itemLance.EstaDisponivel, itemLance.DonoAtual, itemLance.ValorAtual, itemLance.ValorAdicionalMinimo, itemLance.TempoRestante);
                    //dataGridView1.Update();

                    multicast.SendInsertMessage(listIndex, itemLance);

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

        public void DoTimeTick()
        {
            while (!this.IsDisposed)
            {
                if (ListaLances.Count > 0 && dataGridView1 != null)
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
                                    dataGridView1.Rows[listIndex].Cells[5].Value = itemLance.TempoRestante;
                                }
                                else
                                {
                                    itemLance.EstaDisponivel = false;
                                    ListaLances[listIndex].EstaDisponivel = itemLance.EstaDisponivel;
                                    dataGridView1.Rows[listIndex].Cells[1].Value = itemLance.EstaDisponivel;

                                    multicast.SendInsertMessage(listIndex, itemLance);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e.Message);
                        }
                        //ListaLances.Insert(listIndex, item); 
                        //.Rows.Insert(listIndex, item.NomeItem, item.EstaDisponivel, item.DonoAtual, item.ValorAtual, item.ValorAdicionalMinimo, item.TempoRestante);
                    }
                }
                Thread.Sleep(1000);
            }
            //dataGridView1.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var telaAdicionarItem = new AddItem();
            telaAdicionarItem.Owner = this;
            telaAdicionarItem.Show();

            //ItemLance itemTemp = telaAdicionarItem.NovoLance;
            //if (itemTemp != null)
            //{
            //     leiloeiro.ListaLances.Add(itemTemp);
            //    MessageBox.Show(leiloeiro.ListaLances[0].ToString());
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja remover este item do leilão?", "Confirmação necessária", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int listIndex = dataGridView1.CurrentCell.RowIndex;
                    ItemLance itemLance = ListaLances[listIndex];
                    if (itemLance.EstaDisponivel)
                    {
                        itemLance.TempoRestante = 0;

                        ListaLances[listIndex].TempoRestante = itemLance.TempoRestante;
                        dataGridView1.Rows[listIndex].Cells[5].Value = itemLance.TempoRestante;
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
            if (dataGridView1.CurrentCell != null)
            {
                int listIndex = dataGridView1.CurrentCell.RowIndex;
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

        private void ReceiveMessage(string message)
        {
            if (message.Length > 0 && message.StartsWith("#"))
            {
                if (message.StartsWith(multicast.comandoJoin))
                {
                    message = message.Substring(multicast.comandoJoin.Length);
                    MessageBox.Show("comandoJoin = " + message);
                }
                else if (message.StartsWith(multicast.comandoBuy))
                {
                    message = message.Substring(multicast.comandoBuy.Length);
                    MessageBox.Show("comandoBuy = " + message);
                }
                else if (message.StartsWith(multicast.comandoClear))
                {
                    message = message.Substring(multicast.comandoClear.Length);
                    //ListaLances.Clear();
                    //ListaParticipantes.Clear();
                    //dataGridView1.Rows.Clear();
                    //dataGridView2.Rows.Clear();
                    MessageBox.Show("comandoClear = " + message);
                }
                else if (message.StartsWith(multicast.comandoInsert)) //formato #insert=listIndex,NomeItem,ValorInicial, ValorAdicionalMinimo,ValorAtual,DonoAtual,TempoRestant,EstaDisponivel
                {
                    message = message.Substring(multicast.comandoInsert.Length);
                  

                   
                    MessageBox.Show("comandoInsert = " + message);
                }
            }
        }
    }
}
