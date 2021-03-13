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
            this.groupBoxItens = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridItemLance = new System.Windows.Forms.DataGridView();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Disp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonoAtual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValAtual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValPorLance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempoRestante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxConnect = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtBoxServerIp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxCertificateKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxMessages = new System.Windows.Forms.GroupBox();
            this.groupBoxItens.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItemLance)).BeginInit();
            this.groupBoxConnect.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxItens
            // 
            this.groupBoxItens.Controls.Add(this.button1);
            this.groupBoxItens.Controls.Add(this.dataGridItemLance);
            this.groupBoxItens.Enabled = false;
            this.groupBoxItens.Location = new System.Drawing.Point(9, 10);
            this.groupBoxItens.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxItens.Name = "groupBoxItens";
            this.groupBoxItens.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxItens.Size = new System.Drawing.Size(538, 357);
            this.groupBoxItens.TabIndex = 0;
            this.groupBoxItens.TabStop = false;
            this.groupBoxItens.Text = "Items em Leilão";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(188, 301);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 41);
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
            this.dataGridItemLance.Location = new System.Drawing.Point(12, 25);
            this.dataGridItemLance.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridItemLance.MultiSelect = false;
            this.dataGridItemLance.Name = "dataGridItemLance";
            this.dataGridItemLance.ReadOnly = true;
            this.dataGridItemLance.RowHeadersVisible = false;
            this.dataGridItemLance.RowHeadersWidth = 51;
            this.dataGridItemLance.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridItemLance.RowTemplate.Height = 24;
            this.dataGridItemLance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridItemLance.Size = new System.Drawing.Size(515, 258);
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
            // groupBoxConnect
            // 
            this.groupBoxConnect.Controls.Add(this.button2);
            this.groupBoxConnect.Controls.Add(this.txtBoxServerIp);
            this.groupBoxConnect.Controls.Add(this.label3);
            this.groupBoxConnect.Controls.Add(this.txtBoxCertificateKey);
            this.groupBoxConnect.Controls.Add(this.label2);
            this.groupBoxConnect.Controls.Add(this.txtBoxUsername);
            this.groupBoxConnect.Controls.Add(this.label1);
            this.groupBoxConnect.Location = new System.Drawing.Point(559, 11);
            this.groupBoxConnect.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxConnect.Name = "groupBoxConnect";
            this.groupBoxConnect.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxConnect.Size = new System.Drawing.Size(239, 188);
            this.groupBoxConnect.TabIndex = 1;
            this.groupBoxConnect.TabStop = false;
            this.groupBoxConnect.Text = "Conectar ao Leilão";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(77, 147);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 27);
            this.button2.TabIndex = 3;
            this.button2.Text = "Conectar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // txtBoxServerIp
            // 
            this.txtBoxServerIp.Location = new System.Drawing.Point(18, 115);
            this.txtBoxServerIp.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxServerIp.MaxLength = 15;
            this.txtBoxServerIp.Name = "txtBoxServerIp";
            this.txtBoxServerIp.Size = new System.Drawing.Size(205, 20);
            this.txtBoxServerIp.TabIndex = 8;
            this.txtBoxServerIp.Text = "127.0.0.1";
            this.txtBoxServerIp.TextChanged += new System.EventHandler(this.txtBoxServerIp_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Server IP:";
            // 
            // txtBoxCertificateKey
            // 
            this.txtBoxCertificateKey.Location = new System.Drawing.Point(18, 78);
            this.txtBoxCertificateKey.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxCertificateKey.MaxLength = 10;
            this.txtBoxCertificateKey.Name = "txtBoxCertificateKey";
            this.txtBoxCertificateKey.Size = new System.Drawing.Size(205, 20);
            this.txtBoxCertificateKey.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password:";
            // 
            // txtBoxUsername
            // 
            this.txtBoxUsername.Location = new System.Drawing.Point(18, 41);
            this.txtBoxUsername.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxUsername.MaxLength = 15;
            this.txtBoxUsername.Name = "txtBoxUsername";
            this.txtBoxUsername.Size = new System.Drawing.Size(205, 20);
            this.txtBoxUsername.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username:";
            // 
            // groupBoxMessages
            // 
            this.groupBoxMessages.Location = new System.Drawing.Point(559, 204);
            this.groupBoxMessages.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxMessages.Name = "groupBoxMessages";
            this.groupBoxMessages.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxMessages.Size = new System.Drawing.Size(239, 162);
            this.groupBoxMessages.TabIndex = 3;
            this.groupBoxMessages.TabStop = false;
            this.groupBoxMessages.Text = "Mensagens do Leilão";
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 377);
            this.Controls.Add(this.groupBoxMessages);
            this.Controls.Add(this.groupBoxConnect);
            this.Controls.Add(this.groupBoxItens);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FormClient";
            this.Text = "Leilão Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.groupBoxItens.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItemLance)).EndInit();
            this.groupBoxConnect.ResumeLayout(false);
            this.groupBoxConnect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxItens;
        private System.Windows.Forms.GroupBox groupBoxConnect;
        private System.Windows.Forms.DataGridView dataGridItemLance;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Disp;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonoAtual;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValAtual;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValPorLance;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempoRestante;
        private System.Windows.Forms.GroupBox groupBoxMessages;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtBoxServerIp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxCertificateKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxUsername;
        private System.Windows.Forms.Label label1;
    }
}

