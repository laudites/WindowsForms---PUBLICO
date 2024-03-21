namespace Gerenciador_SIP
{
    partial class Form_Planilha_Eng
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Planilha_Eng));
            panel1 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            panel14 = new System.Windows.Forms.Panel();
            panel12 = new System.Windows.Forms.Panel();
            textBox_pesquisaComponente = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            panel11 = new System.Windows.Forms.Panel();
            panel15 = new System.Windows.Forms.Panel();
            button_verificar = new System.Windows.Forms.Button();
            label4 = new System.Windows.Forms.Label();
            button_Modulo = new System.Windows.Forms.Button();
            button_cabeceiras = new System.Windows.Forms.Button();
            panel5 = new System.Windows.Forms.Panel();
            button_nova_linha = new System.Windows.Forms.Button();
            button_cadastarAcessorios = new System.Windows.Forms.Button();
            button_copiar = new System.Windows.Forms.Button();
            button_deletar = new System.Windows.Forms.Button();
            button_salvar = new System.Windows.Forms.Button();
            button_Atualizar = new System.Windows.Forms.Button();
            button_exportar = new System.Windows.Forms.Button();
            button_importar = new System.Windows.Forms.Button();
            button_voltar = new System.Windows.Forms.Button();
            panel9 = new System.Windows.Forms.Panel();
            panel10 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            panel8 = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            textBox_filter = new System.Windows.Forms.TextBox();
            panel13 = new System.Windows.Forms.Panel();
            comboBox1 = new System.Windows.Forms.ComboBox();
            label3 = new System.Windows.Forms.Label();
            panel6 = new System.Windows.Forms.Panel();
            panel7 = new System.Windows.Forms.Panel();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel12.SuspendLayout();
            panel11.SuspendLayout();
            panel15.SuspendLayout();
            panel5.SuspendLayout();
            panel2.SuspendLayout();
            panel8.SuspendLayout();
            panel13.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1420, 10);
            panel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Dock = System.Windows.Forms.DockStyle.Right;
            panel3.Location = new System.Drawing.Point(1410, 10);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(10, 735);
            panel3.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.Controls.Add(dataGridView1);
            panel4.Controls.Add(panel14);
            panel4.Controls.Add(panel12);
            panel4.Controls.Add(panel11);
            panel4.Controls.Add(panel5);
            panel4.Controls.Add(panel10);
            panel4.Controls.Add(panel2);
            panel4.Controls.Add(panel7);
            panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            panel4.Location = new System.Drawing.Point(0, 10);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(1410, 735);
            panel4.TabIndex = 3;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(171, 74);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new System.Drawing.Size(1229, 606);
            dataGridView1.TabIndex = 5;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // panel14
            // 
            panel14.Dock = System.Windows.Forms.DockStyle.Right;
            panel14.Location = new System.Drawing.Point(1400, 74);
            panel14.Name = "panel14";
            panel14.Size = new System.Drawing.Size(10, 606);
            panel14.TabIndex = 12;
            // 
            // panel12
            // 
            panel12.Controls.Add(textBox_pesquisaComponente);
            panel12.Controls.Add(label2);
            panel12.Dock = System.Windows.Forms.DockStyle.Top;
            panel12.Location = new System.Drawing.Point(171, 35);
            panel12.Name = "panel12";
            panel12.Size = new System.Drawing.Size(1239, 39);
            panel12.TabIndex = 11;
            // 
            // textBox_pesquisaComponente
            // 
            textBox_pesquisaComponente.Location = new System.Drawing.Point(130, 15);
            textBox_pesquisaComponente.Name = "textBox_pesquisaComponente";
            textBox_pesquisaComponente.Size = new System.Drawing.Size(394, 23);
            textBox_pesquisaComponente.TabIndex = 4;
            textBox_pesquisaComponente.TextChanged += textBox_pesquisaComponente_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 18);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(127, 15);
            label2.TabIndex = 1;
            label2.Text = "Pesquisa componente:";
            // 
            // panel11
            // 
            panel11.Controls.Add(panel15);
            panel11.Controls.Add(button_Modulo);
            panel11.Controls.Add(button_cabeceiras);
            panel11.Dock = System.Windows.Forms.DockStyle.Top;
            panel11.Location = new System.Drawing.Point(171, 0);
            panel11.Name = "panel11";
            panel11.Size = new System.Drawing.Size(1239, 35);
            panel11.TabIndex = 10;
            // 
            // panel15
            // 
            panel15.Controls.Add(button_verificar);
            panel15.Controls.Add(label4);
            panel15.Dock = System.Windows.Forms.DockStyle.Right;
            panel15.Location = new System.Drawing.Point(614, 0);
            panel15.Name = "panel15";
            panel15.Size = new System.Drawing.Size(625, 35);
            panel15.TabIndex = 15;
            // 
            // button_verificar
            // 
            button_verificar.Dock = System.Windows.Forms.DockStyle.Right;
            button_verificar.Location = new System.Drawing.Point(511, 0);
            button_verificar.Name = "button_verificar";
            button_verificar.Size = new System.Drawing.Size(114, 35);
            button_verificar.TabIndex = 16;
            button_verificar.Text = "Verificar";
            button_verificar.UseVisualStyleBackColor = true;
            button_verificar.Click += button_verificar_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = System.Windows.Forms.DockStyle.Left;
            label4.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label4.ForeColor = System.Drawing.Color.Red;
            label4.Location = new System.Drawing.Point(0, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(477, 32);
            label4.TabIndex = 15;
            label4.Text = "Alteração nos cadastros de expositores!!";
            // 
            // button_Modulo
            // 
            button_Modulo.Dock = System.Windows.Forms.DockStyle.Left;
            button_Modulo.Location = new System.Drawing.Point(114, 0);
            button_Modulo.Name = "button_Modulo";
            button_Modulo.Size = new System.Drawing.Size(114, 35);
            button_Modulo.TabIndex = 14;
            button_Modulo.Text = "Modelo";
            button_Modulo.UseVisualStyleBackColor = true;
            button_Modulo.Click += button_Modulo_Click;
            // 
            // button_cabeceiras
            // 
            button_cabeceiras.Dock = System.Windows.Forms.DockStyle.Left;
            button_cabeceiras.Location = new System.Drawing.Point(0, 0);
            button_cabeceiras.Name = "button_cabeceiras";
            button_cabeceiras.Size = new System.Drawing.Size(114, 35);
            button_cabeceiras.TabIndex = 13;
            button_cabeceiras.Text = "Cabeceiras";
            button_cabeceiras.UseVisualStyleBackColor = true;
            button_cabeceiras.Click += button_cabeceiras_Click;
            // 
            // panel5
            // 
            panel5.Controls.Add(button_nova_linha);
            panel5.Controls.Add(button_cadastarAcessorios);
            panel5.Controls.Add(button_copiar);
            panel5.Controls.Add(button_deletar);
            panel5.Controls.Add(button_salvar);
            panel5.Controls.Add(button_Atualizar);
            panel5.Controls.Add(button_exportar);
            panel5.Controls.Add(button_importar);
            panel5.Controls.Add(button_voltar);
            panel5.Controls.Add(panel9);
            panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel5.Location = new System.Drawing.Point(171, 680);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(1239, 45);
            panel5.TabIndex = 9;
            // 
            // button_nova_linha
            // 
            button_nova_linha.Dock = System.Windows.Forms.DockStyle.Left;
            button_nova_linha.Enabled = false;
            button_nova_linha.Location = new System.Drawing.Point(342, 10);
            button_nova_linha.Name = "button_nova_linha";
            button_nova_linha.Size = new System.Drawing.Size(119, 35);
            button_nova_linha.TabIndex = 14;
            button_nova_linha.Text = "Inserir componente";
            button_nova_linha.UseVisualStyleBackColor = true;
            button_nova_linha.Click += button_nova_linha_Click;
            // 
            // button_cadastarAcessorios
            // 
            button_cadastarAcessorios.Dock = System.Windows.Forms.DockStyle.Right;
            button_cadastarAcessorios.Enabled = false;
            button_cadastarAcessorios.Location = new System.Drawing.Point(669, 10);
            button_cadastarAcessorios.Name = "button_cadastarAcessorios";
            button_cadastarAcessorios.Size = new System.Drawing.Size(114, 35);
            button_cadastarAcessorios.TabIndex = 13;
            button_cadastarAcessorios.Text = "Cadastrar Acess.";
            button_cadastarAcessorios.UseVisualStyleBackColor = true;
            button_cadastarAcessorios.Visible = false;
            button_cadastarAcessorios.Click += button_cadastarAcessorios_Click;
            // 
            // button_copiar
            // 
            button_copiar.Dock = System.Windows.Forms.DockStyle.Right;
            button_copiar.Enabled = false;
            button_copiar.Location = new System.Drawing.Point(783, 10);
            button_copiar.Name = "button_copiar";
            button_copiar.Size = new System.Drawing.Size(114, 35);
            button_copiar.TabIndex = 9;
            button_copiar.Text = "Copiar";
            button_copiar.UseVisualStyleBackColor = true;
            button_copiar.Click += button_copiar_Click;
            // 
            // button_deletar
            // 
            button_deletar.Dock = System.Windows.Forms.DockStyle.Left;
            button_deletar.Enabled = false;
            button_deletar.Location = new System.Drawing.Point(228, 10);
            button_deletar.Name = "button_deletar";
            button_deletar.Size = new System.Drawing.Size(114, 35);
            button_deletar.TabIndex = 8;
            button_deletar.Text = "Deletar";
            button_deletar.UseVisualStyleBackColor = true;
            button_deletar.Click += button_deletar_Click;
            // 
            // button_salvar
            // 
            button_salvar.Dock = System.Windows.Forms.DockStyle.Left;
            button_salvar.Enabled = false;
            button_salvar.Location = new System.Drawing.Point(114, 10);
            button_salvar.Name = "button_salvar";
            button_salvar.Size = new System.Drawing.Size(114, 35);
            button_salvar.TabIndex = 7;
            button_salvar.Text = "Alterar";
            button_salvar.UseVisualStyleBackColor = true;
            button_salvar.Click += button_salvar_Click;
            // 
            // button_Atualizar
            // 
            button_Atualizar.Dock = System.Windows.Forms.DockStyle.Left;
            button_Atualizar.Location = new System.Drawing.Point(0, 10);
            button_Atualizar.Name = "button_Atualizar";
            button_Atualizar.Size = new System.Drawing.Size(114, 35);
            button_Atualizar.TabIndex = 6;
            button_Atualizar.Text = "Atualizar";
            button_Atualizar.UseVisualStyleBackColor = true;
            button_Atualizar.Click += button_Atualizar_Click;
            // 
            // button_exportar
            // 
            button_exportar.Dock = System.Windows.Forms.DockStyle.Right;
            button_exportar.Location = new System.Drawing.Point(897, 10);
            button_exportar.Name = "button_exportar";
            button_exportar.Size = new System.Drawing.Size(114, 35);
            button_exportar.TabIndex = 10;
            button_exportar.Text = "Exportar";
            button_exportar.UseVisualStyleBackColor = true;
            button_exportar.Click += button_exportar_Click;
            // 
            // button_importar
            // 
            button_importar.Dock = System.Windows.Forms.DockStyle.Right;
            button_importar.Enabled = false;
            button_importar.Location = new System.Drawing.Point(1011, 10);
            button_importar.Name = "button_importar";
            button_importar.Size = new System.Drawing.Size(114, 35);
            button_importar.TabIndex = 11;
            button_importar.Text = "Importar";
            button_importar.UseVisualStyleBackColor = true;
            button_importar.Click += button_importar_Click;
            // 
            // button_voltar
            // 
            button_voltar.Dock = System.Windows.Forms.DockStyle.Right;
            button_voltar.Location = new System.Drawing.Point(1125, 10);
            button_voltar.Name = "button_voltar";
            button_voltar.Size = new System.Drawing.Size(114, 35);
            button_voltar.TabIndex = 12;
            button_voltar.Text = "Voltar";
            button_voltar.UseVisualStyleBackColor = true;
            button_voltar.Click += button_voltar_Click;
            // 
            // panel9
            // 
            panel9.Dock = System.Windows.Forms.DockStyle.Top;
            panel9.Location = new System.Drawing.Point(0, 0);
            panel9.Name = "panel9";
            panel9.Size = new System.Drawing.Size(1239, 10);
            panel9.TabIndex = 3;
            // 
            // panel10
            // 
            panel10.Dock = System.Windows.Forms.DockStyle.Left;
            panel10.Location = new System.Drawing.Point(161, 0);
            panel10.Name = "panel10";
            panel10.Size = new System.Drawing.Size(10, 725);
            panel10.TabIndex = 7;
            // 
            // panel2
            // 
            panel2.Controls.Add(checkedListBox1);
            panel2.Controls.Add(panel8);
            panel2.Controls.Add(panel13);
            panel2.Controls.Add(panel6);
            panel2.Dock = System.Windows.Forms.DockStyle.Left;
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(161, 725);
            panel2.TabIndex = 5;
            // 
            // checkedListBox1
            // 
            checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new System.Drawing.Point(10, 79);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new System.Drawing.Size(151, 646);
            checkedListBox1.TabIndex = 3;
            // 
            // panel8
            // 
            panel8.Controls.Add(label1);
            panel8.Controls.Add(textBox_filter);
            panel8.Dock = System.Windows.Forms.DockStyle.Top;
            panel8.Location = new System.Drawing.Point(10, 40);
            panel8.Name = "panel8";
            panel8.Size = new System.Drawing.Size(151, 39);
            panel8.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Top;
            label1.Location = new System.Drawing.Point(0, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(108, 15);
            label1.TabIndex = 1;
            label1.Text = "Pesquisa expositor:";
            // 
            // textBox_filter
            // 
            textBox_filter.Dock = System.Windows.Forms.DockStyle.Bottom;
            textBox_filter.Location = new System.Drawing.Point(0, 16);
            textBox_filter.Name = "textBox_filter";
            textBox_filter.Size = new System.Drawing.Size(151, 23);
            textBox_filter.TabIndex = 2;
            textBox_filter.KeyUp += textBox_filter_KeyUp;
            // 
            // panel13
            // 
            panel13.Controls.Add(comboBox1);
            panel13.Controls.Add(label3);
            panel13.Dock = System.Windows.Forms.DockStyle.Top;
            panel13.Location = new System.Drawing.Point(10, 0);
            panel13.Name = "panel13";
            panel13.Size = new System.Drawing.Size(151, 40);
            panel13.TabIndex = 9;
            // 
            // comboBox1
            // 
            comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(0, 15);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(151, 23);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = System.Windows.Forms.DockStyle.Top;
            label3.Location = new System.Drawing.Point(0, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(39, 15);
            label3.TabIndex = 2;
            label3.Text = "Linha:";
            // 
            // panel6
            // 
            panel6.Dock = System.Windows.Forms.DockStyle.Left;
            panel6.Location = new System.Drawing.Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new System.Drawing.Size(10, 725);
            panel6.TabIndex = 0;
            // 
            // panel7
            // 
            panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel7.Location = new System.Drawing.Point(0, 725);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(1410, 10);
            panel7.TabIndex = 1;
            // 
            // Form_Planilha_Eng
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1420, 745);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(1012, 655);
            Name = "Form_Planilha_Eng";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Planilha acessorios";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += Form_Planilha_Eng_Load;
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            panel11.ResumeLayout(false);
            panel15.ResumeLayout(false);
            panel15.PerformLayout();
            panel5.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button_Atualizar;
        private System.Windows.Forms.Button button_exportar;
        private System.Windows.Forms.Button button_importar;
        private System.Windows.Forms.Button button_voltar;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button button_salvar;
        private System.Windows.Forms.Button button_deletar;
        private System.Windows.Forms.Button button_Modulo;
        private System.Windows.Forms.Button button_cabeceiras;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Button button_copiar;
        private System.Windows.Forms.TextBox textBox_filter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_pesquisaComponente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_cadastarAcessorios;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Button button_verificar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_nova_linha;
    }
}