namespace Gerenciador_SIP
{
    partial class Form_Verificar_Log
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
            panel5 = new System.Windows.Forms.Panel();
            button_voltar = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.TabControl();
            Geral = new System.Windows.Forms.TabPage();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            panel1 = new System.Windows.Forms.Panel();
            textBox_pesquisaExpositor = new System.Windows.Forms.TextBox();
            tabpageExpositor = new System.Windows.Forms.TabPage();
            panel3 = new System.Windows.Forms.Panel();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            dataGridView_expositor1 = new System.Windows.Forms.DataGridView();
            label1 = new System.Windows.Forms.Label();
            dataGridView_expositor2 = new System.Windows.Forms.DataGridView();
            label2 = new System.Windows.Forms.Label();
            panel2 = new System.Windows.Forms.Panel();
            button_cadastrado_expositor = new System.Windows.Forms.Button();
            panel4 = new System.Windows.Forms.Panel();
            label7 = new System.Windows.Forms.Label();
            tabpageCabeceiras = new System.Windows.Forms.TabPage();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            dataGridView2_cabeceira1 = new System.Windows.Forms.DataGridView();
            label3 = new System.Windows.Forms.Label();
            dataGridView2_cabeceira2 = new System.Windows.Forms.DataGridView();
            label4 = new System.Windows.Forms.Label();
            panel6 = new System.Windows.Forms.Panel();
            button_cadastrar_cabeceira = new System.Windows.Forms.Button();
            panel7 = new System.Windows.Forms.Panel();
            label8 = new System.Windows.Forms.Label();
            tabpageAcessorios = new System.Windows.Forms.TabPage();
            splitContainer3 = new System.Windows.Forms.SplitContainer();
            dataGridView4_acessorios = new System.Windows.Forms.DataGridView();
            label5 = new System.Windows.Forms.Label();
            dataGridView5_acessorio = new System.Windows.Forms.DataGridView();
            label6 = new System.Windows.Forms.Label();
            panel8 = new System.Windows.Forms.Panel();
            button_acessorios_cadastro = new System.Windows.Forms.Button();
            panel9 = new System.Windows.Forms.Panel();
            label9 = new System.Windows.Forms.Label();
            panel5.SuspendLayout();
            tabControl1.SuspendLayout();
            Geral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            tabpageExpositor.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_expositor1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_expositor2).BeginInit();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            tabpageCabeceiras.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2_cabeceira1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2_cabeceira2).BeginInit();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            tabpageAcessorios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView4_acessorios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView5_acessorio).BeginInit();
            panel8.SuspendLayout();
            panel9.SuspendLayout();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.Controls.Add(button_voltar);
            panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel5.Location = new System.Drawing.Point(0, 512);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(1165, 39);
            panel5.TabIndex = 10;
            // 
            // button_voltar
            // 
            button_voltar.Dock = System.Windows.Forms.DockStyle.Right;
            button_voltar.Location = new System.Drawing.Point(1051, 0);
            button_voltar.Name = "button_voltar";
            button_voltar.Size = new System.Drawing.Size(114, 39);
            button_voltar.TabIndex = 12;
            button_voltar.Text = "Voltar";
            button_voltar.UseVisualStyleBackColor = true;
            button_voltar.Click += button_voltar_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Geral);
            tabControl1.Controls.Add(tabpageExpositor);
            tabControl1.Controls.Add(tabpageCabeceiras);
            tabControl1.Controls.Add(tabpageAcessorios);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(1165, 512);
            tabControl1.TabIndex = 11;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // Geral
            // 
            Geral.Controls.Add(dataGridView1);
            Geral.Controls.Add(checkedListBox1);
            Geral.Controls.Add(panel1);
            Geral.Location = new System.Drawing.Point(4, 24);
            Geral.Name = "Geral";
            Geral.Padding = new System.Windows.Forms.Padding(3);
            Geral.Size = new System.Drawing.Size(1157, 484);
            Geral.TabIndex = 0;
            Geral.Text = "Geral";
            Geral.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(123, 27);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new System.Drawing.Size(1031, 454);
            dataGridView1.TabIndex = 4;
            // 
            // checkedListBox1
            // 
            checkedListBox1.Dock = System.Windows.Forms.DockStyle.Left;
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new System.Drawing.Point(3, 27);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new System.Drawing.Size(120, 454);
            checkedListBox1.TabIndex = 3;
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            // 
            // panel1
            // 
            panel1.Controls.Add(textBox_pesquisaExpositor);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1151, 24);
            panel1.TabIndex = 1;
            // 
            // textBox_pesquisaExpositor
            // 
            textBox_pesquisaExpositor.Dock = System.Windows.Forms.DockStyle.Left;
            textBox_pesquisaExpositor.Location = new System.Drawing.Point(0, 0);
            textBox_pesquisaExpositor.Name = "textBox_pesquisaExpositor";
            textBox_pesquisaExpositor.Size = new System.Drawing.Size(120, 23);
            textBox_pesquisaExpositor.TabIndex = 0;
            textBox_pesquisaExpositor.TextChanged += textBox_pesquisaExpositor_TextChanged;
            // 
            // tabpageExpositor
            // 
            tabpageExpositor.Controls.Add(panel3);
            tabpageExpositor.Controls.Add(panel2);
            tabpageExpositor.Location = new System.Drawing.Point(4, 24);
            tabpageExpositor.Name = "tabpageExpositor";
            tabpageExpositor.Padding = new System.Windows.Forms.Padding(3);
            tabpageExpositor.Size = new System.Drawing.Size(1157, 484);
            tabpageExpositor.TabIndex = 1;
            tabpageExpositor.Text = "Expositor";
            tabpageExpositor.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(splitContainer1);
            panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Location = new System.Drawing.Point(133, 3);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(1021, 478);
            panel3.TabIndex = 1;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView_expositor1);
            splitContainer1.Panel1.Controls.Add(label1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView_expositor2);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Size = new System.Drawing.Size(1021, 478);
            splitContainer1.SplitterDistance = 508;
            splitContainer1.TabIndex = 0;
            // 
            // dataGridView_expositor1
            // 
            dataGridView_expositor1.AllowUserToAddRows = false;
            dataGridView_expositor1.AllowUserToDeleteRows = false;
            dataGridView_expositor1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_expositor1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView_expositor1.Location = new System.Drawing.Point(0, 28);
            dataGridView_expositor1.Name = "dataGridView_expositor1";
            dataGridView_expositor1.ReadOnly = true;
            dataGridView_expositor1.RowTemplate.Height = 25;
            dataGridView_expositor1.Size = new System.Drawing.Size(508, 450);
            dataGridView_expositor1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Top;
            label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(0, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(43, 28);
            label1.TabIndex = 0;
            label1.Text = "De:";
            // 
            // dataGridView_expositor2
            // 
            dataGridView_expositor2.AllowUserToAddRows = false;
            dataGridView_expositor2.AllowUserToDeleteRows = false;
            dataGridView_expositor2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_expositor2.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView_expositor2.Location = new System.Drawing.Point(0, 28);
            dataGridView_expositor2.Name = "dataGridView_expositor2";
            dataGridView_expositor2.ReadOnly = true;
            dataGridView_expositor2.RowTemplate.Height = 25;
            dataGridView_expositor2.Size = new System.Drawing.Size(509, 450);
            dataGridView_expositor2.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = System.Windows.Forms.DockStyle.Top;
            label2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(0, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(59, 28);
            label2.TabIndex = 1;
            label2.Text = "Para:";
            // 
            // panel2
            // 
            panel2.Controls.Add(button_cadastrado_expositor);
            panel2.Controls.Add(panel4);
            panel2.Dock = System.Windows.Forms.DockStyle.Left;
            panel2.Location = new System.Drawing.Point(3, 3);
            panel2.Name = "panel2";
            panel2.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            panel2.Size = new System.Drawing.Size(130, 478);
            panel2.TabIndex = 0;
            // 
            // button_cadastrado_expositor
            // 
            button_cadastrado_expositor.Dock = System.Windows.Forms.DockStyle.Top;
            button_cadastrado_expositor.Enabled = false;
            button_cadastrado_expositor.Location = new System.Drawing.Point(0, 28);
            button_cadastrado_expositor.Name = "button_cadastrado_expositor";
            button_cadastrado_expositor.Size = new System.Drawing.Size(126, 30);
            button_cadastrado_expositor.TabIndex = 1;
            button_cadastrado_expositor.Text = "Cadastrado";
            button_cadastrado_expositor.UseVisualStyleBackColor = true;
            button_cadastrado_expositor.Click += button_cadastrado_expositor_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(label7);
            panel4.Dock = System.Windows.Forms.DockStyle.Top;
            panel4.Location = new System.Drawing.Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(126, 28);
            panel4.TabIndex = 0;
            // 
            // label7
            // 
            label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label7.ForeColor = System.Drawing.Color.Red;
            label7.Location = new System.Drawing.Point(17, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(92, 25);
            label7.TabIndex = 0;
            label7.Text = "Expositor";
            label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabpageCabeceiras
            // 
            tabpageCabeceiras.Controls.Add(splitContainer2);
            tabpageCabeceiras.Controls.Add(panel6);
            tabpageCabeceiras.Location = new System.Drawing.Point(4, 24);
            tabpageCabeceiras.Name = "tabpageCabeceiras";
            tabpageCabeceiras.Size = new System.Drawing.Size(1157, 484);
            tabpageCabeceiras.TabIndex = 2;
            tabpageCabeceiras.Text = "Cabeceiras";
            tabpageCabeceiras.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.Location = new System.Drawing.Point(130, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(dataGridView2_cabeceira1);
            splitContainer2.Panel1.Controls.Add(label3);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(dataGridView2_cabeceira2);
            splitContainer2.Panel2.Controls.Add(label4);
            splitContainer2.Size = new System.Drawing.Size(1027, 484);
            splitContainer2.SplitterDistance = 513;
            splitContainer2.TabIndex = 2;
            // 
            // dataGridView2_cabeceira1
            // 
            dataGridView2_cabeceira1.AllowUserToAddRows = false;
            dataGridView2_cabeceira1.AllowUserToDeleteRows = false;
            dataGridView2_cabeceira1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2_cabeceira1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView2_cabeceira1.Location = new System.Drawing.Point(0, 28);
            dataGridView2_cabeceira1.Name = "dataGridView2_cabeceira1";
            dataGridView2_cabeceira1.ReadOnly = true;
            dataGridView2_cabeceira1.RowTemplate.Height = 25;
            dataGridView2_cabeceira1.Size = new System.Drawing.Size(513, 456);
            dataGridView2_cabeceira1.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = System.Windows.Forms.DockStyle.Top;
            label3.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label3.Location = new System.Drawing.Point(0, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(43, 28);
            label3.TabIndex = 2;
            label3.Text = "De:";
            // 
            // dataGridView2_cabeceira2
            // 
            dataGridView2_cabeceira2.AllowUserToAddRows = false;
            dataGridView2_cabeceira2.AllowUserToDeleteRows = false;
            dataGridView2_cabeceira2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2_cabeceira2.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView2_cabeceira2.Location = new System.Drawing.Point(0, 28);
            dataGridView2_cabeceira2.Name = "dataGridView2_cabeceira2";
            dataGridView2_cabeceira2.ReadOnly = true;
            dataGridView2_cabeceira2.RowTemplate.Height = 25;
            dataGridView2_cabeceira2.Size = new System.Drawing.Size(510, 456);
            dataGridView2_cabeceira2.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = System.Windows.Forms.DockStyle.Top;
            label4.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label4.Location = new System.Drawing.Point(0, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(59, 28);
            label4.TabIndex = 3;
            label4.Text = "Para:";
            // 
            // panel6
            // 
            panel6.Controls.Add(button_cadastrar_cabeceira);
            panel6.Controls.Add(panel7);
            panel6.Dock = System.Windows.Forms.DockStyle.Left;
            panel6.Location = new System.Drawing.Point(0, 0);
            panel6.Name = "panel6";
            panel6.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            panel6.Size = new System.Drawing.Size(130, 484);
            panel6.TabIndex = 1;
            // 
            // button_cadastrar_cabeceira
            // 
            button_cadastrar_cabeceira.Dock = System.Windows.Forms.DockStyle.Top;
            button_cadastrar_cabeceira.Enabled = false;
            button_cadastrar_cabeceira.Location = new System.Drawing.Point(0, 28);
            button_cadastrar_cabeceira.Name = "button_cadastrar_cabeceira";
            button_cadastrar_cabeceira.Size = new System.Drawing.Size(126, 30);
            button_cadastrar_cabeceira.TabIndex = 1;
            button_cadastrar_cabeceira.Text = "Cadastrado";
            button_cadastrar_cabeceira.UseVisualStyleBackColor = true;
            button_cadastrar_cabeceira.Click += button_cadastrar_cabeceira_Click;
            // 
            // panel7
            // 
            panel7.Controls.Add(label8);
            panel7.Dock = System.Windows.Forms.DockStyle.Top;
            panel7.Location = new System.Drawing.Point(0, 0);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(126, 28);
            panel7.TabIndex = 0;
            // 
            // label8
            // 
            label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label8.ForeColor = System.Drawing.Color.Red;
            label8.Location = new System.Drawing.Point(17, 2);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(95, 25);
            label8.TabIndex = 1;
            label8.Text = "Cabeceira";
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabpageAcessorios
            // 
            tabpageAcessorios.Controls.Add(splitContainer3);
            tabpageAcessorios.Controls.Add(panel8);
            tabpageAcessorios.Location = new System.Drawing.Point(4, 24);
            tabpageAcessorios.Name = "tabpageAcessorios";
            tabpageAcessorios.Size = new System.Drawing.Size(1157, 484);
            tabpageAcessorios.TabIndex = 3;
            tabpageAcessorios.Text = "Acessorios";
            tabpageAcessorios.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer3.Location = new System.Drawing.Point(130, 0);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(dataGridView4_acessorios);
            splitContainer3.Panel1.Controls.Add(label5);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(dataGridView5_acessorio);
            splitContainer3.Panel2.Controls.Add(label6);
            splitContainer3.Size = new System.Drawing.Size(1027, 484);
            splitContainer3.SplitterDistance = 513;
            splitContainer3.TabIndex = 3;
            // 
            // dataGridView4_acessorios
            // 
            dataGridView4_acessorios.AllowUserToAddRows = false;
            dataGridView4_acessorios.AllowUserToDeleteRows = false;
            dataGridView4_acessorios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView4_acessorios.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView4_acessorios.Location = new System.Drawing.Point(0, 28);
            dataGridView4_acessorios.Name = "dataGridView4_acessorios";
            dataGridView4_acessorios.ReadOnly = true;
            dataGridView4_acessorios.RowTemplate.Height = 25;
            dataGridView4_acessorios.Size = new System.Drawing.Size(513, 456);
            dataGridView4_acessorios.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = System.Windows.Forms.DockStyle.Top;
            label5.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label5.Location = new System.Drawing.Point(0, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(43, 28);
            label5.TabIndex = 4;
            label5.Text = "De:";
            // 
            // dataGridView5_acessorio
            // 
            dataGridView5_acessorio.AllowUserToAddRows = false;
            dataGridView5_acessorio.AllowUserToDeleteRows = false;
            dataGridView5_acessorio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView5_acessorio.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView5_acessorio.Location = new System.Drawing.Point(0, 28);
            dataGridView5_acessorio.Name = "dataGridView5_acessorio";
            dataGridView5_acessorio.ReadOnly = true;
            dataGridView5_acessorio.RowTemplate.Height = 25;
            dataGridView5_acessorio.Size = new System.Drawing.Size(510, 456);
            dataGridView5_acessorio.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = System.Windows.Forms.DockStyle.Top;
            label6.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label6.Location = new System.Drawing.Point(0, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(59, 28);
            label6.TabIndex = 5;
            label6.Text = "Para:";
            // 
            // panel8
            // 
            panel8.Controls.Add(button_acessorios_cadastro);
            panel8.Controls.Add(panel9);
            panel8.Dock = System.Windows.Forms.DockStyle.Left;
            panel8.Location = new System.Drawing.Point(0, 0);
            panel8.Name = "panel8";
            panel8.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            panel8.Size = new System.Drawing.Size(130, 484);
            panel8.TabIndex = 2;
            // 
            // button_acessorios_cadastro
            // 
            button_acessorios_cadastro.Dock = System.Windows.Forms.DockStyle.Top;
            button_acessorios_cadastro.Enabled = false;
            button_acessorios_cadastro.Location = new System.Drawing.Point(0, 28);
            button_acessorios_cadastro.Name = "button_acessorios_cadastro";
            button_acessorios_cadastro.Size = new System.Drawing.Size(126, 30);
            button_acessorios_cadastro.TabIndex = 1;
            button_acessorios_cadastro.Text = "Cadastrado";
            button_acessorios_cadastro.UseVisualStyleBackColor = true;
            button_acessorios_cadastro.Click += button_acessorios_cadastro_Click;
            // 
            // panel9
            // 
            panel9.Controls.Add(label9);
            panel9.Dock = System.Windows.Forms.DockStyle.Top;
            panel9.Location = new System.Drawing.Point(0, 0);
            panel9.Name = "panel9";
            panel9.Size = new System.Drawing.Size(126, 28);
            panel9.TabIndex = 0;
            // 
            // label9
            // 
            label9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label9.ForeColor = System.Drawing.Color.Red;
            label9.Location = new System.Drawing.Point(17, 2);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(94, 25);
            label9.TabIndex = 1;
            label9.Text = "Acessorio";
            label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_Verificar_Log
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1165, 551);
            Controls.Add(tabControl1);
            Controls.Add(panel5);
            Name = "Form_Verificar_Log";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Form_Verificar_Log";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += Form_Verificar_Log_Load;
            panel5.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            Geral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabpageExpositor.ResumeLayout(false);
            panel3.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView_expositor1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_expositor2).EndInit();
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            tabpageCabeceiras.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2_cabeceira1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2_cabeceira2).EndInit();
            panel6.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            tabpageAcessorios.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel1.PerformLayout();
            splitContainer3.Panel2.ResumeLayout(false);
            splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView4_acessorios).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView5_acessorio).EndInit();
            panel8.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button_voltar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Geral;
        private System.Windows.Forms.TabPage tabpageExpositor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_pesquisaExpositor;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TabPage tabpageAcessorios;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView_expositor1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView_expositor2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_cadastrado_expositor;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabPage tabpageCabeceiras;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView2_cabeceira1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView2_cabeceira2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button_cadastrar_cabeceira;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dataGridView4_acessorios;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView5_acessorio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button button_acessorios_cadastro;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}