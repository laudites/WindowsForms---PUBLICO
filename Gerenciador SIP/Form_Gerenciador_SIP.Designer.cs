
namespace Gerenciador_SIP
{
    partial class Form_Gerenciador_SIP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Gerenciador_SIP));
            pictureBox1 = new System.Windows.Forms.PictureBox();
            panel1 = new System.Windows.Forms.Panel();
            button_diretoria = new System.Windows.Forms.Button();
            button_planilha_eng = new System.Windows.Forms.Button();
            button_cadastrarServidor = new System.Windows.Forms.Button();
            button_procurar = new System.Windows.Forms.Button();
            button_Eletrofrio = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            button_Tabelas_SQL = new System.Windows.Forms.Button();
            button_Fechar = new System.Windows.Forms.Button();
            Panel_Sub_Eletrofrio = new System.Windows.Forms.Panel();
            button_cabeceiras = new System.Windows.Forms.Button();
            button_comparativo = new System.Windows.Forms.Button();
            button_Traducao = new System.Windows.Forms.Button();
            button_desenho = new System.Windows.Forms.Button();
            button_Descritivo_tecnico = new System.Windows.Forms.Button();
            button_chave_busca = new System.Windows.Forms.Button();
            comboBox_Instancia = new System.Windows.Forms.ComboBox();
            textBox_banco_dados = new System.Windows.Forms.TextBox();
            textBox_Banco = new System.Windows.Forms.TextBox();
            comboBox_banco = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            Panel_Sub_Eletrofrio.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new System.Drawing.Point(0, 370);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(800, 80);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(button_diretoria);
            panel1.Controls.Add(button_planilha_eng);
            panel1.Controls.Add(button_cadastrarServidor);
            panel1.Controls.Add(button_procurar);
            panel1.Controls.Add(button_Eletrofrio);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button_Tabelas_SQL);
            panel1.Controls.Add(button_Fechar);
            panel1.Dock = System.Windows.Forms.DockStyle.Left;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(150, 370);
            panel1.TabIndex = 2;
            // 
            // button_diretoria
            // 
            button_diretoria.Dock = System.Windows.Forms.DockStyle.Top;
            button_diretoria.Location = new System.Drawing.Point(0, 161);
            button_diretoria.Name = "button_diretoria";
            button_diretoria.Size = new System.Drawing.Size(150, 23);
            button_diretoria.TabIndex = 7;
            button_diretoria.Text = "Relatorio Gerencial";
            button_diretoria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button_diretoria.UseVisualStyleBackColor = true;
            button_diretoria.Visible = false;
            button_diretoria.Click += button_diretoria_Click;
            // 
            // button_planilha_eng
            // 
            button_planilha_eng.Dock = System.Windows.Forms.DockStyle.Top;
            button_planilha_eng.Location = new System.Drawing.Point(0, 138);
            button_planilha_eng.Name = "button_planilha_eng";
            button_planilha_eng.Size = new System.Drawing.Size(150, 23);
            button_planilha_eng.TabIndex = 6;
            button_planilha_eng.Text = "Planilhas Eng.";
            button_planilha_eng.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button_planilha_eng.UseVisualStyleBackColor = true;
            button_planilha_eng.Visible = false;
            button_planilha_eng.Click += button_planilha_eng_Click;
            // 
            // button_cadastrarServidor
            // 
            button_cadastrarServidor.Dock = System.Windows.Forms.DockStyle.Top;
            button_cadastrarServidor.Location = new System.Drawing.Point(0, 115);
            button_cadastrarServidor.Name = "button_cadastrarServidor";
            button_cadastrarServidor.Size = new System.Drawing.Size(150, 23);
            button_cadastrarServidor.TabIndex = 5;
            button_cadastrarServidor.Text = "Cadastrar servidor";
            button_cadastrarServidor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button_cadastrarServidor.UseVisualStyleBackColor = true;
            button_cadastrarServidor.Visible = false;
            button_cadastrarServidor.Click += button_cadastrarServidor_Click;
            // 
            // button_procurar
            // 
            button_procurar.Dock = System.Windows.Forms.DockStyle.Top;
            button_procurar.Location = new System.Drawing.Point(0, 92);
            button_procurar.Name = "button_procurar";
            button_procurar.Size = new System.Drawing.Size(150, 23);
            button_procurar.TabIndex = 4;
            button_procurar.Text = "Procurar texto";
            button_procurar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button_procurar.UseVisualStyleBackColor = true;
            button_procurar.Visible = false;
            button_procurar.Click += button_procurar_Click;
            // 
            // button_Eletrofrio
            // 
            button_Eletrofrio.Dock = System.Windows.Forms.DockStyle.Top;
            button_Eletrofrio.Location = new System.Drawing.Point(0, 69);
            button_Eletrofrio.Name = "button_Eletrofrio";
            button_Eletrofrio.Size = new System.Drawing.Size(150, 23);
            button_Eletrofrio.TabIndex = 3;
            button_Eletrofrio.Text = "Eletrofrio";
            button_Eletrofrio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button_Eletrofrio.UseVisualStyleBackColor = true;
            button_Eletrofrio.Visible = false;
            button_Eletrofrio.Click += button_Eletrofrio_Click;
            // 
            // button1
            // 
            button1.Dock = System.Windows.Forms.DockStyle.Top;
            button1.Location = new System.Drawing.Point(0, 46);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(150, 23);
            button1.TabIndex = 2;
            button1.Text = "Importacao em lote";
            button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // button_Tabelas_SQL
            // 
            button_Tabelas_SQL.Dock = System.Windows.Forms.DockStyle.Top;
            button_Tabelas_SQL.Location = new System.Drawing.Point(0, 23);
            button_Tabelas_SQL.Name = "button_Tabelas_SQL";
            button_Tabelas_SQL.Size = new System.Drawing.Size(150, 23);
            button_Tabelas_SQL.TabIndex = 1;
            button_Tabelas_SQL.Text = "Tabelas SQL";
            button_Tabelas_SQL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button_Tabelas_SQL.UseVisualStyleBackColor = true;
            button_Tabelas_SQL.Visible = false;
            button_Tabelas_SQL.Click += button_Tabelas_SQL_Click;
            // 
            // button_Fechar
            // 
            button_Fechar.Dock = System.Windows.Forms.DockStyle.Top;
            button_Fechar.Location = new System.Drawing.Point(0, 0);
            button_Fechar.Name = "button_Fechar";
            button_Fechar.Size = new System.Drawing.Size(150, 23);
            button_Fechar.TabIndex = 0;
            button_Fechar.Text = "Fechar";
            button_Fechar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button_Fechar.UseVisualStyleBackColor = true;
            button_Fechar.Click += button_Fechar_Click;
            // 
            // Panel_Sub_Eletrofrio
            // 
            Panel_Sub_Eletrofrio.Controls.Add(button_cabeceiras);
            Panel_Sub_Eletrofrio.Controls.Add(button_comparativo);
            Panel_Sub_Eletrofrio.Controls.Add(button_Traducao);
            Panel_Sub_Eletrofrio.Controls.Add(button_desenho);
            Panel_Sub_Eletrofrio.Controls.Add(button_Descritivo_tecnico);
            Panel_Sub_Eletrofrio.Controls.Add(button_chave_busca);
            Panel_Sub_Eletrofrio.Dock = System.Windows.Forms.DockStyle.Left;
            Panel_Sub_Eletrofrio.Location = new System.Drawing.Point(150, 0);
            Panel_Sub_Eletrofrio.Name = "Panel_Sub_Eletrofrio";
            Panel_Sub_Eletrofrio.Size = new System.Drawing.Size(169, 370);
            Panel_Sub_Eletrofrio.TabIndex = 3;
            Panel_Sub_Eletrofrio.Visible = false;
            // 
            // button_cabeceiras
            // 
            button_cabeceiras.Dock = System.Windows.Forms.DockStyle.Top;
            button_cabeceiras.Location = new System.Drawing.Point(0, 115);
            button_cabeceiras.Name = "button_cabeceiras";
            button_cabeceiras.Size = new System.Drawing.Size(169, 23);
            button_cabeceiras.TabIndex = 9;
            button_cabeceiras.Text = "Cadastrar cabeceiras";
            button_cabeceiras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button_cabeceiras.UseVisualStyleBackColor = true;
            button_cabeceiras.Visible = false;
            button_cabeceiras.Click += button_cabeceiras_Click;
            // 
            // button_comparativo
            // 
            button_comparativo.Dock = System.Windows.Forms.DockStyle.Top;
            button_comparativo.Location = new System.Drawing.Point(0, 92);
            button_comparativo.Name = "button_comparativo";
            button_comparativo.Size = new System.Drawing.Size(169, 23);
            button_comparativo.TabIndex = 8;
            button_comparativo.Text = "Comparativo";
            button_comparativo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button_comparativo.UseVisualStyleBackColor = true;
            button_comparativo.Visible = false;
            button_comparativo.Click += button_comparativo_Click;
            // 
            // button_Traducao
            // 
            button_Traducao.Dock = System.Windows.Forms.DockStyle.Top;
            button_Traducao.Location = new System.Drawing.Point(0, 69);
            button_Traducao.Name = "button_Traducao";
            button_Traducao.Size = new System.Drawing.Size(169, 23);
            button_Traducao.TabIndex = 7;
            button_Traducao.Text = "Tradução";
            button_Traducao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button_Traducao.UseVisualStyleBackColor = true;
            button_Traducao.Visible = false;
            button_Traducao.Click += button_Traducao_Click;
            // 
            // button_desenho
            // 
            button_desenho.Dock = System.Windows.Forms.DockStyle.Top;
            button_desenho.Location = new System.Drawing.Point(0, 46);
            button_desenho.Name = "button_desenho";
            button_desenho.Size = new System.Drawing.Size(169, 23);
            button_desenho.TabIndex = 4;
            button_desenho.Text = "Desenhos";
            button_desenho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button_desenho.UseVisualStyleBackColor = true;
            button_desenho.Visible = false;
            button_desenho.Click += button_desenho_Click;
            // 
            // button_Descritivo_tecnico
            // 
            button_Descritivo_tecnico.Dock = System.Windows.Forms.DockStyle.Top;
            button_Descritivo_tecnico.Location = new System.Drawing.Point(0, 23);
            button_Descritivo_tecnico.Name = "button_Descritivo_tecnico";
            button_Descritivo_tecnico.Size = new System.Drawing.Size(169, 23);
            button_Descritivo_tecnico.TabIndex = 3;
            button_Descritivo_tecnico.Text = "Descritivo técnico";
            button_Descritivo_tecnico.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button_Descritivo_tecnico.UseVisualStyleBackColor = true;
            button_Descritivo_tecnico.Visible = false;
            button_Descritivo_tecnico.Click += button_Descritivo_tecnico_Click;
            // 
            // button_chave_busca
            // 
            button_chave_busca.Dock = System.Windows.Forms.DockStyle.Top;
            button_chave_busca.Location = new System.Drawing.Point(0, 0);
            button_chave_busca.Name = "button_chave_busca";
            button_chave_busca.Size = new System.Drawing.Size(169, 23);
            button_chave_busca.TabIndex = 0;
            button_chave_busca.Text = "Chave_Busca";
            button_chave_busca.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button_chave_busca.UseVisualStyleBackColor = true;
            button_chave_busca.Visible = false;
            button_chave_busca.Click += button_chave_busca_Click;
            // 
            // comboBox_Instancia
            // 
            comboBox_Instancia.FormattingEnabled = true;
            comboBox_Instancia.Location = new System.Drawing.Point(622, 34);
            comboBox_Instancia.Name = "comboBox_Instancia";
            comboBox_Instancia.Size = new System.Drawing.Size(166, 23);
            comboBox_Instancia.TabIndex = 4;
            comboBox_Instancia.Visible = false;
            comboBox_Instancia.SelectionChangeCommitted += comboBox_Instancia_SelectionChangeCommitted;
            // 
            // textBox_banco_dados
            // 
            textBox_banco_dados.BackColor = System.Drawing.SystemColors.Control;
            textBox_banco_dados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox_banco_dados.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            textBox_banco_dados.Location = new System.Drawing.Point(622, 12);
            textBox_banco_dados.Name = "textBox_banco_dados";
            textBox_banco_dados.Size = new System.Drawing.Size(166, 16);
            textBox_banco_dados.TabIndex = 5;
            textBox_banco_dados.Text = "Nome da configuração";
            textBox_banco_dados.Visible = false;
            // 
            // textBox_Banco
            // 
            textBox_Banco.BackColor = System.Drawing.SystemColors.Control;
            textBox_Banco.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox_Banco.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            textBox_Banco.Location = new System.Drawing.Point(622, 63);
            textBox_Banco.Name = "textBox_Banco";
            textBox_Banco.Size = new System.Drawing.Size(166, 16);
            textBox_Banco.TabIndex = 7;
            textBox_Banco.Text = "Banco de dados";
            textBox_Banco.Visible = false;
            // 
            // comboBox_banco
            // 
            comboBox_banco.FormattingEnabled = true;
            comboBox_banco.Location = new System.Drawing.Point(622, 85);
            comboBox_banco.Name = "comboBox_banco";
            comboBox_banco.Size = new System.Drawing.Size(166, 23);
            comboBox_banco.TabIndex = 6;
            comboBox_banco.Visible = false;
            // 
            // Form_Gerenciador_SIP
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(textBox_Banco);
            Controls.Add(comboBox_banco);
            Controls.Add(textBox_banco_dados);
            Controls.Add(comboBox_Instancia);
            Controls.Add(Panel_Sub_Eletrofrio);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "Form_Gerenciador_SIP";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Gerenciador";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            Panel_Sub_Eletrofrio.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_Tabelas_SQL;
        private System.Windows.Forms.Button button_Fechar;
        private System.Windows.Forms.Button button_Eletrofrio;
        private System.Windows.Forms.Panel Panel_Sub_Eletrofrio;
        private System.Windows.Forms.Button button_chave_busca;
        private System.Windows.Forms.Button button_Descritivo_tecnico;
        private System.Windows.Forms.Button button_procurar;
        private System.Windows.Forms.Button button_desenho;
        private System.Windows.Forms.Button button_Traducao;
        private System.Windows.Forms.Button button_comparativo;
        private System.Windows.Forms.ComboBox comboBox_Instancia;
        private System.Windows.Forms.TextBox textBox_banco_dados;
        private System.Windows.Forms.Button button_cabeceiras;
        private System.Windows.Forms.TextBox textBox_Banco;
        private System.Windows.Forms.ComboBox comboBox_banco;
        private System.Windows.Forms.Button button_cadastrarServidor;
        private System.Windows.Forms.Button button_planilha_eng;
        private System.Windows.Forms.Button button_diretoria;
    }
}