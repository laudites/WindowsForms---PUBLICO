namespace Gerenciador_SIP
{
    partial class Form_Cabeceiras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Cabeceiras));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_exportar = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_alter = new System.Windows.Forms.Button();
            this.button_create = new System.Windows.Forms.Button();
            this.button_atualizar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_importar_tela = new System.Windows.Forms.Button();
            this.button_exportar_tela = new System.Windows.Forms.Button();
            this.button_voltar = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox_corte = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_exportar);
            this.panel1.Controls.Add(this.button_delete);
            this.panel1.Controls.Add(this.button_alter);
            this.panel1.Controls.Add(this.button_create);
            this.panel1.Controls.Add(this.button_atualizar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(165, 574);
            this.panel1.TabIndex = 0;
            // 
            // button_exportar
            // 
            this.button_exportar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button_exportar.Location = new System.Drawing.Point(0, 522);
            this.button_exportar.Name = "button_exportar";
            this.button_exportar.Size = new System.Drawing.Size(165, 52);
            this.button_exportar.TabIndex = 6;
            this.button_exportar.Text = "Exportar todas tabelas de cabeceiras - EXCEL";
            this.button_exportar.UseVisualStyleBackColor = true;
            this.button_exportar.Click += new System.EventHandler(this.button_exportar_Click);
            // 
            // button_delete
            // 
            this.button_delete.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_delete.Location = new System.Drawing.Point(0, 138);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(165, 46);
            this.button_delete.TabIndex = 3;
            this.button_delete.Text = "Deletar";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_alter
            // 
            this.button_alter.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_alter.Location = new System.Drawing.Point(0, 92);
            this.button_alter.Name = "button_alter";
            this.button_alter.Size = new System.Drawing.Size(165, 46);
            this.button_alter.TabIndex = 2;
            this.button_alter.Text = "Alterar";
            this.button_alter.UseVisualStyleBackColor = true;
            this.button_alter.Click += new System.EventHandler(this.button_alter_Click);
            // 
            // button_create
            // 
            this.button_create.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_create.Location = new System.Drawing.Point(0, 46);
            this.button_create.Name = "button_create";
            this.button_create.Size = new System.Drawing.Size(165, 46);
            this.button_create.TabIndex = 1;
            this.button_create.Text = "Criar";
            this.button_create.UseVisualStyleBackColor = true;
            this.button_create.Click += new System.EventHandler(this.button_create_Click);
            // 
            // button_atualizar
            // 
            this.button_atualizar.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_atualizar.Location = new System.Drawing.Point(0, 0);
            this.button_atualizar.Name = "button_atualizar";
            this.button_atualizar.Size = new System.Drawing.Size(165, 46);
            this.button_atualizar.TabIndex = 0;
            this.button_atualizar.Text = "Atualizar";
            this.button_atualizar.UseVisualStyleBackColor = true;
            this.button_atualizar.Click += new System.EventHandler(this.button_atualizar_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_importar_tela);
            this.panel2.Controls.Add(this.button_exportar_tela);
            this.panel2.Controls.Add(this.button_voltar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(165, 522);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 52);
            this.panel2.TabIndex = 1;
            // 
            // button_importar_tela
            // 
            this.button_importar_tela.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_importar_tela.Location = new System.Drawing.Point(165, 0);
            this.button_importar_tela.Name = "button_importar_tela";
            this.button_importar_tela.Size = new System.Drawing.Size(165, 52);
            this.button_importar_tela.TabIndex = 9;
            this.button_importar_tela.Text = "Importar tela - EXCEL";
            this.button_importar_tela.UseVisualStyleBackColor = true;
            this.button_importar_tela.Click += new System.EventHandler(this.button_importar_tela_Click_1);
            // 
            // button_exportar_tela
            // 
            this.button_exportar_tela.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_exportar_tela.Location = new System.Drawing.Point(0, 0);
            this.button_exportar_tela.Name = "button_exportar_tela";
            this.button_exportar_tela.Size = new System.Drawing.Size(165, 52);
            this.button_exportar_tela.TabIndex = 8;
            this.button_exportar_tela.Text = "Exportar tela - EXCEL";
            this.button_exportar_tela.UseVisualStyleBackColor = true;
            this.button_exportar_tela.Click += new System.EventHandler(this.button_exportar_tela_Click);
            // 
            // button_voltar
            // 
            this.button_voltar.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_voltar.Location = new System.Drawing.Point(685, 0);
            this.button_voltar.Name = "button_voltar";
            this.button_voltar.Size = new System.Drawing.Size(165, 52);
            this.button_voltar.TabIndex = 1;
            this.button_voltar.Text = "Voltar";
            this.button_voltar.UseVisualStyleBackColor = true;
            this.button_voltar.Click += new System.EventHandler(this.button_voltar_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox_corte);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(165, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(850, 46);
            this.panel3.TabIndex = 2;
            // 
            // textBox_corte
            // 
            this.textBox_corte.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox_corte.Location = new System.Drawing.Point(76, 6);
            this.textBox_corte.Name = "textBox_corte";
            this.textBox_corte.Size = new System.Drawing.Size(168, 34);
            this.textBox_corte.TabIndex = 1;
            this.textBox_corte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Corte:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(165, 46);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(850, 476);
            this.dataGridView1.TabIndex = 3;
            // 
            // Form_Cabeceiras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 574);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Cabeceiras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cabeceiras";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_atualizar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox_corte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_voltar;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_alter;
        private System.Windows.Forms.Button button_create;
        private System.Windows.Forms.Button button_exportar;
        private System.Windows.Forms.Button button_importar_tela;
        private System.Windows.Forms.Button button_exportar_tela;
    }
}