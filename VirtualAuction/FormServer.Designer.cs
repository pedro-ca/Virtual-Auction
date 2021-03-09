namespace AuctionServer
{
    partial class FormServer
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridItemLance = new System.Windows.Forms.DataGridView();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Disp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonoAtual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValAtual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValPorLance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempoRestante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridParticipante = new System.Windows.Forms.DataGridView();
            this.NomeUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItemLance)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridParticipante)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dataGridItemLance);
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(538, 357);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Items em Leilão";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(364, 301);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(163, 41);
            this.button2.TabIndex = 2;
            this.button2.Text = "Remover Item do Leilão";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 301);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 41);
            this.button1.TabIndex = 1;
            this.button1.Text = "Adicionar Item ao Leilão";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.dataGridItemLance.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridParticipante);
            this.groupBox2.Location = new System.Drawing.Point(559, 11);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(239, 188);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Participantes";
            // 
            // dataGridParticipante
            // 
            this.dataGridParticipante.AllowUserToAddRows = false;
            this.dataGridParticipante.AllowUserToDeleteRows = false;
            this.dataGridParticipante.AllowUserToResizeColumns = false;
            this.dataGridParticipante.AllowUserToResizeRows = false;
            this.dataGridParticipante.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridParticipante.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridParticipante.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NomeUsuario,
            this.Ip});
            this.dataGridParticipante.Location = new System.Drawing.Point(14, 25);
            this.dataGridParticipante.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridParticipante.MultiSelect = false;
            this.dataGridParticipante.Name = "dataGridParticipante";
            this.dataGridParticipante.ReadOnly = true;
            this.dataGridParticipante.RowHeadersVisible = false;
            this.dataGridParticipante.RowHeadersWidth = 51;
            this.dataGridParticipante.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridParticipante.RowTemplate.Height = 24;
            this.dataGridParticipante.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridParticipante.Size = new System.Drawing.Size(210, 152);
            this.dataGridParticipante.TabIndex = 1;
            // 
            // NomeUsuario
            // 
            this.NomeUsuario.HeaderText = "Nome";
            this.NomeUsuario.MinimumWidth = 6;
            this.NomeUsuario.Name = "NomeUsuario";
            this.NomeUsuario.ReadOnly = true;
            this.NomeUsuario.Width = 80;
            // 
            // Ip
            // 
            this.Ip.HeaderText = "IP";
            this.Ip.MinimumWidth = 6;
            this.Ip.Name = "Ip";
            this.Ip.ReadOnly = true;
            this.Ip.Width = 125;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(561, 204);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(237, 162);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mensagens do Leilão";
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 377);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "FormServer";
            this.Text = "Leilão Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItemLance)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridParticipante)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridItemLance;
        private System.Windows.Forms.DataGridView dataGridParticipante;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Disp;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonoAtual;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValAtual;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValPorLance;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempoRestante;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

