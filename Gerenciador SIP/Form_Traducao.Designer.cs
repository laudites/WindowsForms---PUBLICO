
namespace Gerenciador_SIP
{
    partial class Form_Traducao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Traducao));
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_cadastrar = new System.Windows.Forms.Button();
            this.button_lista_sem_cadastro = new System.Windows.Forms.Button();
            this.button_Lista_completa = new System.Windows.Forms.Button();
            this.button_Voltar = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 35);
            this.panel1.TabIndex = 14;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox2.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox2.Location = new System.Drawing.Point(79, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(197, 30);
            this.textBox2.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(0, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(224, 23);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Produto:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_cadastrar);
            this.panel2.Controls.Add(this.button_lista_sem_cadastro);
            this.panel2.Controls.Add(this.button_Lista_completa);
            this.panel2.Controls.Add(this.button_Voltar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 400);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 50);
            this.panel2.TabIndex = 13;
            // 
            // button_cadastrar
            // 
            this.button_cadastrar.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_cadastrar.Location = new System.Drawing.Point(300, 0);
            this.button_cadastrar.Name = "button_cadastrar";
            this.button_cadastrar.Size = new System.Drawing.Size(100, 50);
            this.button_cadastrar.TabIndex = 6;
            this.button_cadastrar.Text = "Cadastrar";
            this.button_cadastrar.UseVisualStyleBackColor = true;
            this.button_cadastrar.Click += new System.EventHandler(this.button_cadastrar_Click);
            // 
            // button_lista_sem_cadastro
            // 
            this.button_lista_sem_cadastro.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_lista_sem_cadastro.Location = new System.Drawing.Point(200, 0);
            this.button_lista_sem_cadastro.Name = "button_lista_sem_cadastro";
            this.button_lista_sem_cadastro.Size = new System.Drawing.Size(100, 50);
            this.button_lista_sem_cadastro.TabIndex = 5;
            this.button_lista_sem_cadastro.Text = "Lista de itens sem cadastro";
            this.button_lista_sem_cadastro.UseVisualStyleBackColor = true;
            this.button_lista_sem_cadastro.Click += new System.EventHandler(this.button_lista_sem_cadastro_Click);
            // 
            // button_Lista_completa
            // 
            this.button_Lista_completa.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Lista_completa.Location = new System.Drawing.Point(100, 0);
            this.button_Lista_completa.Name = "button_Lista_completa";
            this.button_Lista_completa.Size = new System.Drawing.Size(100, 50);
            this.button_Lista_completa.TabIndex = 4;
            this.button_Lista_completa.Text = "Lista completa";
            this.button_Lista_completa.UseVisualStyleBackColor = true;
            this.button_Lista_completa.Click += new System.EventHandler(this.button_Lista_completa_Click);
            // 
            // button_Voltar
            // 
            this.button_Voltar.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Voltar.Location = new System.Drawing.Point(0, 0);
            this.button_Voltar.Name = "button_Voltar";
            this.button_Voltar.Size = new System.Drawing.Size(100, 50);
            this.button_Voltar.TabIndex = 3;
            this.button_Voltar.Text = "Voltar";
            this.button_Voltar.UseVisualStyleBackColor = true;
            this.button_Voltar.Click += new System.EventHandler(this.button_Voltar_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.listBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(130, 365);
            this.panel3.TabIndex = 15;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Items.AddRange(new object[] {
            "Todos",
            "Comercial",
            "Conjunto_Ind",
            "Espanhol",
            "FINAME",
            "Conferencia"});
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(130, 365);
            this.listBox1.TabIndex = 1;
            this.listBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(130, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(670, 365);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.TabStop = false;
            // 
            // Form_Traducao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Traducao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Traducao";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_Lista_completa;
        private System.Windows.Forms.Button button_Voltar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListBox listBox1;
        
        private System.Windows.Forms.Button button_cadastrar;
        private System.Windows.Forms.Button button_lista_sem_cadastro;
    }
}