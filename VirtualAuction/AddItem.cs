﻿using System;
using System.Windows.Forms;

namespace AuctionServer
{
    public partial class AddItem : Form
    {
        public AddItem()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBoxNomeItem.Text) && !String.IsNullOrEmpty(txtBoxTempoDeLeilao.Text) && !String.IsNullOrEmpty(txtBoxValorInicial.Text) && !String.IsNullOrEmpty(txtBoxValorMin.Text))
            {
                DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja adicionar novo item ao leilão?", "Confirmação necessária", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    FormServer parentForm = (FormServer)this.Owner;
                    parentForm.AddLance(txtBoxNomeItem.Text, float.Parse(txtBoxValorInicial.Text), float.Parse(txtBoxValorMin.Text), int.Parse(txtBoxTempoDeLeilao.Text));
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos antes de adicionar novo item.", "Dados Inválidos");
            }
        }

        private void txtBoxValorInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtBoxValorMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtBoxTempoDeLeilao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtBoxNomeItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',')       //pra evitar bug nainterpretação da mensagem, não é permitido virgula
            {
                e.Handled = true;
            }
        }
    }
}
