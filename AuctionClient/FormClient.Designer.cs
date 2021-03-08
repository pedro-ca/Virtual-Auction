namespace AuctionClient
{
    partial class FormClient
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridItemLance = new System.Windows.Forms.DataGridView();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Disp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonoAtual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValAtual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValPorLance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempoRestante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtBoxServerIp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxCertificateKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItemLance)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dataGridItemLance);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(717, 439);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Items em Leilão";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(251, 370);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(217, 50);
            this.button1.TabIndex = 2;
            this.button1.Text = "Enviar novo Lance para o Item";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridItemLance
            // 
            this.dataGridItemLance.AllowUserToAddRows = false;
            this.dataGridItemLance.AllowUserToDeleteRows = false;
            this.dataGridItemLance.AllowUserToResizeColumns = false;
            this.dataGridItemLance.AllowUserToResizeRows = false;
            this.dataGridItemLance.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridItemLance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridItemLance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nome,
            this.Disp,
            this.DonoAtual,
            this.ValAtual,
            this.ValPorLance,
            this.TempoRestante});
            this.dataGridItemLance.Location = new System.Drawing.Point(16, 31);
            this.dataGridItemLance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridItemLance.MultiSelect = false;
            this.dataGridItemLance.Name = "dataGridItemLance";
            this.dataGridItemLance.ReadOnly = true;
            this.dataGridItemLance.RowHeadersVisible = false;
            this.dataGridItemLance.RowHeadersWidth = 51;
            this.dataGridItemLance.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridItemLance.RowTemplate.Height = 24;
            this.dataGridItemLance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridItemLance.Size = new System.Drawing.Size(687, 318);
            this.dataGridItemLance.TabIndex = 0;
            // 
            // Nome
            // 
            this.Nome.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Nome.HeaderText = "Nome";
            this.Nome.MaxInputLength = 300;
            this.Nome.MinimumWidth = 6;
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            this.Nome.Width = 75;
            // 
            // Disp
            // 
            this.Disp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Disp.HeaderText = "Disp.";
            this.Disp.MaxInputLength = 300;
            this.Disp.MinimumWidth = 6;
            this.Disp.Name = "Disp";
            this.Disp.ReadOnly = true;
            this.Disp.Width = 65;
            // 
            // DonoAtual
            // 
            this.DonoAtual.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DonoAtual.HeaderText = "Dono Atual";
            this.DonoAtual.MaxInputLength = 300;
            this.DonoAtual.MinimumWidth = 6;
            this.DonoAtual.Name = "DonoAtual";
            this.DonoAtual.ReadOnly = true;
            this.DonoAtual.Width = 95;
            // 
            // ValAtual
            // 
            this.ValAtual.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ValAtual.HeaderText = "Valor Atual";
            this.ValAtual.MaxInputLength = 300;
            this.ValAtual.MinimumWidth = 6;
            this.ValAtual.Name = "ValAtual";
            this.ValAtual.ReadOnly = true;
            this.ValAtual.Width = 95;
            // 
            // ValPorLance
            // 
            this.ValPorLance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ValPorLance.HeaderText = "Val. p/ Lance";
            this.ValPorLance.MaxInputLength = 300;
            this.ValPorLance.MinimumWidth = 6;
            this.ValPorLance.Name = "ValPorLance";
            this.ValPorLance.ReadOnly = true;
            this.ValPorLance.Width = 95;
            // 
            // TempoRestante
            // 
            this.TempoRestante.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TempoRestante.HeaderText = "Tempo";
            this.TempoRestante.MaxInputLength = 300;
            this.TempoRestante.MinimumWidth = 6;
            this.TempoRestante.Name = "TempoRestante";
            this.TempoRestante.ReadOnly = true;
            this.TempoRestante.Width = 85;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.txtBoxServerIp);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtBoxCertificateKey);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtBoxUsername);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(745, 14);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(319, 232);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conectar ao Leilão";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(103, 181);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 33);
            this.button2.TabIndex = 3;
            this.button2.Text = "Conectar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // txtBoxServerIp
            // 
            this.txtBoxServerIp.Location = new System.Drawing.Point(24, 142);
            this.txtBoxServerIp.MaxLength = 15;
            this.txtBoxServerIp.Name = "txtBoxServerIp";
            this.txtBoxServerIp.Size = new System.Drawing.Size(272, 22);
            this.txtBoxServerIp.TabIndex = 8;
            this.txtBoxServerIp.Text = "127.0.0.1";
            this.txtBoxServerIp.TextChanged += new System.EventHandler(this.txtBoxServerIp_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Server IP:";
            // 
            // txtBoxCertificateKey
            // 
            this.txtBoxCertificateKey.Location = new System.Drawing.Point(24, 96);
            this.txtBoxCertificateKey.MaxLength = 30;
            this.txtBoxCertificateKey.Name = "txtBoxCertificateKey";
            this.txtBoxCertificateKey.Size = new System.Drawing.Size(272, 22);
            this.txtBoxCertificateKey.TabIndex = 6;
            this.txtBoxCertificateKey.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Chave do Certificado:";
            // 
            // txtBoxUsername
            // 
            this.txtBoxUsername.Location = new System.Drawing.Point(24, 50);
            this.txtBoxUsername.MaxLength = 150;
            this.txtBoxUsername.Name = "txtBoxUsername";
            this.txtBoxUsername.Size = new System.Drawing.Size(272, 22);
            this.txtBoxUsername.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username:";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(745, 251);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(319, 200);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mensagens do Leilão";
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 464);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormClient";
            this.Text = "Leilão Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItemLance)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridItemLance;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Disp;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonoAtual;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValAtual;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValPorLance;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempoRestante;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtBoxServerIp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxCertificateKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxUsername;
        private System.Windows.Forms.Label label1;
    }
}

