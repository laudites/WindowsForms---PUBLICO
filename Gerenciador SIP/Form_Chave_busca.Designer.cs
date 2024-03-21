
namespace Gerenciador_SIP
{
    partial class Form_Chave_busca
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Chave_busca));
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_import_excel = new System.Windows.Forms.Button();
            this.button_exportar_importar = new System.Windows.Forms.Button();
            this.button_exportar_geral = new System.Windows.Forms.Button();
            this.button_Alterar = new System.Windows.Forms.Button();
            this.button_Deletear = new System.Windows.Forms.Button();
            this.button_Importar_SQL = new System.Windows.Forms.Button();
            this.button_Importar = new System.Windows.Forms.Button();
            this.button_Atualizar = new System.Windows.Forms.Button();
            this.button_voltar2 = new System.Windows.Forms.Button();
            this.button_Voltar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_import_excel);
            this.panel2.Controls.Add(this.button_exportar_importar);
            this.panel2.Controls.Add(this.button_exportar_geral);
            this.panel2.Controls.Add(this.button_Alterar);
            this.panel2.Controls.Add(this.button_Deletear);
            this.panel2.Controls.Add(this.button_Importar_SQL);
            this.panel2.Controls.Add(this.button_Importar);
            this.panel2.Controls.Add(this.button_Atualizar);
            this.panel2.Controls.Add(this.button_voltar2);
            this.panel2.Controls.Add(this.button_Voltar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 473);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1125, 50);
            this.panel2.TabIndex = 6;
            // 
            // button_import_excel
            // 
            this.button_import_excel.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_import_excel.Location = new System.Drawing.Point(900, 0);
            this.button_import_excel.Name = "button_import_excel";
            this.button_import_excel.Size = new System.Drawing.Size(100, 50);
            this.button_import_excel.TabIndex = 11;
            this.button_import_excel.Text = "Importar excel";
            this.button_import_excel.UseVisualStyleBackColor = true;
            this.button_import_excel.Click += new System.EventHandler(this.button_import_excel_Click);
            // 
            // button_exportar_importar
            // 
            this.button_exportar_importar.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_exportar_importar.Location = new System.Drawing.Point(800, 0);
            this.button_exportar_importar.Name = "button_exportar_importar";
            this.button_exportar_importar.Size = new System.Drawing.Size(100, 50);
            this.button_exportar_importar.TabIndex = 10;
            this.button_exportar_importar.Text = "Exportar excel";
            this.button_exportar_importar.UseVisualStyleBackColor = true;
            this.button_exportar_importar.Visible = false;
            this.button_exportar_importar.Click += new System.EventHandler(this.button_exportar_importar_Click);
            // 
            // button_exportar_geral
            // 
            this.button_exportar_geral.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_exportar_geral.Enabled = false;
            this.button_exportar_geral.Location = new System.Drawing.Point(700, 0);
            this.button_exportar_geral.Name = "button_exportar_geral";
            this.button_exportar_geral.Size = new System.Drawing.Size(100, 50);
            this.button_exportar_geral.TabIndex = 9;
            this.button_exportar_geral.Text = "Exportar excel";
            this.button_exportar_geral.UseVisualStyleBackColor = true;
            this.button_exportar_geral.Click += new System.EventHandler(this.button_exportar_geral_Click);
            // 
            // button_Alterar
            // 
            this.button_Alterar.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Alterar.Enabled = false;
            this.button_Alterar.Location = new System.Drawing.Point(600, 0);
            this.button_Alterar.Name = "button_Alterar";
            this.button_Alterar.Size = new System.Drawing.Size(100, 50);
            this.button_Alterar.TabIndex = 8;
            this.button_Alterar.Text = "Alterar chave de busca";
            this.button_Alterar.UseVisualStyleBackColor = true;
            this.button_Alterar.Click += new System.EventHandler(this.button_Alterar_Click);
            // 
            // button_Deletear
            // 
            this.button_Deletear.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Deletear.Enabled = false;
            this.button_Deletear.Location = new System.Drawing.Point(500, 0);
            this.button_Deletear.Name = "button_Deletear";
            this.button_Deletear.Size = new System.Drawing.Size(100, 50);
            this.button_Deletear.TabIndex = 7;
            this.button_Deletear.Text = "Deletar chave de busca";
            this.button_Deletear.UseVisualStyleBackColor = true;
            this.button_Deletear.Click += new System.EventHandler(this.button_Deletear_Click);
            // 
            // button_Importar_SQL
            // 
            this.button_Importar_SQL.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Importar_SQL.Location = new System.Drawing.Point(400, 0);
            this.button_Importar_SQL.Name = "button_Importar_SQL";
            this.button_Importar_SQL.Size = new System.Drawing.Size(100, 50);
            this.button_Importar_SQL.TabIndex = 6;
            this.button_Importar_SQL.Text = "Importar para banco de dados";
            this.button_Importar_SQL.UseVisualStyleBackColor = true;
            this.button_Importar_SQL.Visible = false;
            this.button_Importar_SQL.Click += new System.EventHandler(this.button_Importar_SQL_Click);
            // 
            // button_Importar
            // 
            this.button_Importar.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Importar.Location = new System.Drawing.Point(300, 0);
            this.button_Importar.Name = "button_Importar";
            this.button_Importar.Size = new System.Drawing.Size(100, 50);
            this.button_Importar.TabIndex = 5;
            this.button_Importar.Text = "Criar chave de busca";
            this.button_Importar.UseVisualStyleBackColor = true;
            this.button_Importar.Click += new System.EventHandler(this.button_Importar_Click);
            // 
            // button_Atualizar
            // 
            this.button_Atualizar.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Atualizar.Location = new System.Drawing.Point(200, 0);
            this.button_Atualizar.Name = "button_Atualizar";
            this.button_Atualizar.Size = new System.Drawing.Size(100, 50);
            this.button_Atualizar.TabIndex = 4;
            this.button_Atualizar.Text = "Atualizar";
            this.button_Atualizar.UseVisualStyleBackColor = true;
            this.button_Atualizar.Click += new System.EventHandler(this.button_Atualizar_Click);
            // 
            // button_voltar2
            // 
            this.button_voltar2.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_voltar2.Location = new System.Drawing.Point(100, 0);
            this.button_voltar2.Name = "button_voltar2";
            this.button_voltar2.Size = new System.Drawing.Size(100, 50);
            this.button_voltar2.TabIndex = 3;
            this.button_voltar2.Text = "Voltar";
            this.button_voltar2.UseVisualStyleBackColor = true;
            this.button_voltar2.Visible = false;
            this.button_voltar2.Click += new System.EventHandler(this.button_voltar2_Click);
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
            this.button_Voltar.Click += new System.EventHandler(this.button_Voltar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1125, 52);
            this.panel1.TabIndex = 8;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox2.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox2.Location = new System.Drawing.Point(343, 9);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(193, 39);
            this.textBox2.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(325, 32);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Informar chave de busca:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(1125, 421);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.TabStop = false;
            // 
            // Form_Chave_busca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 523);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Chave_busca";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chave_busca";
            this.Load += new System.EventHandler(this.Form_Chave_busca_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_Voltar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_Importar_SQL;
        private System.Windows.Forms.Button button_Importar;
        private System.Windows.Forms.Button button_Atualizar;
        private System.Windows.Forms.Button button_voltar2;
        private System.Windows.Forms.Button button_Deletear;
        private System.Windows.Forms.Button button_Alterar;
        private System.Windows.Forms.Button button_exportar_importar;
        private System.Windows.Forms.Button button_exportar_geral;
        private System.Windows.Forms.Button button_import_excel;
    }
}