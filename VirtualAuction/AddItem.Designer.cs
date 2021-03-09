namespace AuctionServer
{
    partial class AddItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtBoxTempoDeLeilao = new System.Windows.Forms.TextBox();
            this.txtBoxValorMin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxValorInicial = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxNomeItem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtBoxTempoDeLeilao);
            this.groupBox2.Controls.Add(this.txtBoxValorMin);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtBoxValorInicial);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtBoxNomeItem);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(9, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(253, 120);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalhes do Novo Item";
            // 
            // txtBoxTempoDeLeilao
            // 
            this.txtBoxTempoDeLeilao.Location = new System.Drawing.Point(7, 85);
            this.txtBoxTempoDeLeilao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBoxTempoDeLeilao.MaxLength = 10;
            this.txtBoxTempoDeLeilao.Name = "txtBoxTempoDeLeilao";
            this.txtBoxTempoDeLeilao.Size = new System.Drawing.Size(99, 20);
            this.txtBoxTempoDeLeilao.TabIndex = 3;
            this.txtBoxTempoDeLeilao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxTempoDeLeilao_KeyPress);
            // 
            // txtBoxValorMin
            // 
            this.txtBoxValorMin.Location = new System.Drawing.Point(136, 85);
            this.txtBoxValorMin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBoxValorMin.MaxLength = 7;
            this.txtBoxValorMin.Name = "txtBoxValorMin";
            this.txtBoxValorMin.Size = new System.Drawing.Size(99, 20);
            this.txtBoxValorMin.TabIndex = 4;
            this.txtBoxValorMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxValorMin_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 70);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Valor Min. por lance:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tempo de Leilão:";
            // 
            // txtBoxValorInicial
            // 
            this.txtBoxValorInicial.Location = new System.Drawing.Point(136, 37);
            this.txtBoxValorInicial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBoxValorInicial.MaxLength = 7;
            this.txtBoxValorInicial.Name = "txtBoxValorInicial";
            this.txtBoxValorInicial.Size = new System.Drawing.Size(99, 20);
            this.txtBoxValorInicial.TabIndex = 2;
            this.txtBoxValorInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxValorInicial_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Valor Inicial:";
            // 
            // txtBoxNomeItem
            // 
            this.txtBoxNomeItem.Location = new System.Drawing.Point(7, 37);
            this.txtBoxNomeItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBoxNomeItem.MaxLength = 12;
            this.txtBoxNomeItem.Name = "txtBoxNomeItem";
            this.txtBoxNomeItem.Size = new System.Drawing.Size(99, 20);
            this.txtBoxNomeItem.TabIndex = 1;
            this.txtBoxNomeItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxNomeItem_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome do Item:";
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(9, 145);
            this.btnAdicionar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(66, 25);
            this.btnAdicionar.TabIndex = 5;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(196, 145);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(66, 25);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // AddItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 180);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AddItem";
            this.Text = "Novo Lance";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBoxNomeItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxValorMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxValorInicial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtBoxTempoDeLeilao;
    }
}