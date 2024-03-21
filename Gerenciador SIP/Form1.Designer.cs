
namespace Gerenciador_SIP
{
    partial class Form_Tabela_SQL
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Tabela_SQL));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_Estrutura = new System.Windows.Forms.Button();
            this.button_Exportar_EXCEL = new System.Windows.Forms.Button();
            this.button_Atualizar = new System.Windows.Forms.Button();
            this.button_Voltar = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(250, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(550, 400);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_Estrutura);
            this.panel2.Controls.Add(this.button_Exportar_EXCEL);
            this.panel2.Controls.Add(this.button_Atualizar);
            this.panel2.Controls.Add(this.button_Voltar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 400);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 50);
            this.panel2.TabIndex = 4;
            // 
            // button_Estrutura
            // 
            this.button_Estrutura.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Estrutura.Location = new System.Drawing.Point(300, 0);
            this.button_Estrutura.Name = "button_Estrutura";
            this.button_Estrutura.Size = new System.Drawing.Size(100, 50);
            this.button_Estrutura.TabIndex = 5;
            this.button_Estrutura.Text = "Estrutura da tabela";
            this.button_Estrutura.UseVisualStyleBackColor = true;
            this.button_Estrutura.Click += new System.EventHandler(this.button_Estrutura_Click);
            // 
            // button_Exportar_EXCEL
            // 
            this.button_Exportar_EXCEL.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Exportar_EXCEL.Location = new System.Drawing.Point(200, 0);
            this.button_Exportar_EXCEL.Name = "button_Exportar_EXCEL";
            this.button_Exportar_EXCEL.Size = new System.Drawing.Size(100, 50);
            this.button_Exportar_EXCEL.TabIndex = 4;
            this.button_Exportar_EXCEL.Text = "Exportar EXCEL";
            this.button_Exportar_EXCEL.UseVisualStyleBackColor = true;
            this.button_Exportar_EXCEL.Click += new System.EventHandler(this.button_Exportar_EXCEL_Click);
            // 
            // button_Atualizar
            // 
            this.button_Atualizar.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Atualizar.Location = new System.Drawing.Point(100, 0);
            this.button_Atualizar.Name = "button_Atualizar";
            this.button_Atualizar.Size = new System.Drawing.Size(100, 50);
            this.button_Atualizar.TabIndex = 3;
            this.button_Atualizar.Text = "Atualizar";
            this.button_Atualizar.UseVisualStyleBackColor = true;
            this.button_Atualizar.Click += new System.EventHandler(this.button_Atualizar_Click);
            // 
            // button_Voltar
            // 
            this.button_Voltar.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Voltar.Location = new System.Drawing.Point(0, 0);
            this.button_Voltar.Name = "button_Voltar";
            this.button_Voltar.Size = new System.Drawing.Size(100, 50);
            this.button_Voltar.TabIndex = 2;
            this.button_Voltar.Text = "Voltar";
            this.button_Voltar.UseVisualStyleBackColor = true;
            this.button_Voltar.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(250, 400);
            this.treeView1.TabIndex = 0;
            this.treeView1.TabStop = false;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 400);
            this.panel1.TabIndex = 6;
            // 
            // Form_Tabela_SQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Tabela_SQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_Exportar_EXCEL;
        private System.Windows.Forms.Button button_Atualizar;
        private System.Windows.Forms.Button button_Voltar;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Estrutura;
    }
}

