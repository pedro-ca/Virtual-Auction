using System;
using System.Windows.Forms;

namespace AuctionClient
{
    public partial class BuyItem : Form
    {
        private float LanceMinimo; 

        public BuyItem(float valorAtual, float valorAdicionalMinimo)
        {
            InitializeComponent();
            this.LanceMinimo = valorAtual + valorAdicionalMinimo;
            this.txtBoxPrecoAtual.Text = valorAtual.ToString();
            this.txtBoxLanceMinimo.Text = LanceMinimo.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.textBoxNovoLance.Text))
            {
                float valorNovoLance = float.Parse(textBoxNovoLance.Text);
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to send a bid of $"+valorNovoLance.ToString()+"?", "Confirmation Necessary", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (valorNovoLance >= LanceMinimo)
                    {
                        FormClient parentForm = (FormClient)this.Owner;
                        parentForm.SendBid(valorNovoLance);
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Your bid must be equal or higher than:\n$" + LanceMinimo.ToString(), "Invalid data");
                    }
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

        private void textBoxNovoLance_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
