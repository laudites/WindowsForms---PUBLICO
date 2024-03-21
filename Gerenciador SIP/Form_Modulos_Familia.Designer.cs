namespace Gerenciador_SIP
{
    partial class Form_Modulos_Familia
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
            panel7 = new System.Windows.Forms.Panel();
            panel10 = new System.Windows.Forms.Panel();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            panel9 = new System.Windows.Forms.Panel();
            panel8 = new System.Windows.Forms.Panel();
            textBox_familia = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            textBox_linha = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            panel6 = new System.Windows.Forms.Panel();
            panel5 = new System.Windows.Forms.Panel();
            button_Deletar = new System.Windows.Forms.Button();
            button_Alterar = new System.Windows.Forms.Button();
            button_confirmar = new System.Windows.Forms.Button();
            button_criar = new System.Windows.Forms.Button();
            button_Voltar = new System.Windows.Forms.Button();
            panel4 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            panel7.SuspendLayout();
            panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel8.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // panel7
            // 
            panel7.Controls.Add(panel10);
            panel7.Controls.Add(panel9);
            panel7.Controls.Add(panel8);
            panel7.Controls.Add(panel6);
            panel7.Controls.Add(panel5);
            panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            panel7.Location = new System.Drawing.Point(10, 10);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(780, 430);
            panel7.TabIndex = 11;
            // 
            // panel10
            // 
            panel10.Controls.Add(dataGridView1);
            panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            panel10.Location = new System.Drawing.Point(312, 0);
            panel10.Name = "panel10";
            panel10.Size = new System.Drawing.Size(468, 385);
            panel10.TabIndex = 10;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(0, 0);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            dataGridView1.Size = new System.Drawing.Size(468, 385);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // panel9
            // 
            panel9.Dock = System.Windows.Forms.DockStyle.Left;
            panel9.Location = new System.Drawing.Point(302, 0);
            panel9.Name = "panel9";
            panel9.Size = new System.Drawing.Size(10, 385);
            panel9.TabIndex = 9;
            // 
            // panel8
            // 
            panel8.Controls.Add(textBox_familia);
            panel8.Controls.Add(label2);
            panel8.Controls.Add(textBox_linha);
            panel8.Controls.Add(label1);
            panel8.Dock = System.Windows.Forms.DockStyle.Left;
            panel8.Location = new System.Drawing.Point(0, 0);
            panel8.Name = "panel8";
            panel8.Size = new System.Drawing.Size(302, 385);
            panel8.TabIndex = 8;
            // 
            // textBox_familia
            // 
            textBox_familia.Dock = System.Windows.Forms.DockStyle.Top;
            textBox_familia.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox_familia.Location = new System.Drawing.Point(0, 67);
            textBox_familia.Name = "textBox_familia";
            textBox_familia.Size = new System.Drawing.Size(302, 27);
            textBox_familia.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = System.Windows.Forms.DockStyle.Top;
            label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(0, 47);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(147, 20);
            label2.TabIndex = 2;
            label2.Text = "Familia do expositor:";
            // 
            // textBox_linha
            // 
            textBox_linha.Dock = System.Windows.Forms.DockStyle.Top;
            textBox_linha.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox_linha.Location = new System.Drawing.Point(0, 20);
            textBox_linha.Name = "textBox_linha";
            textBox_linha.Size = new System.Drawing.Size(302, 27);
            textBox_linha.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Top;
            label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(0, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(135, 20);
            label1.TabIndex = 0;
            label1.Text = "Linha do expositor:";
            // 
            // panel6
            // 
            panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel6.Location = new System.Drawing.Point(0, 385);
            panel6.Name = "panel6";
            panel6.Size = new System.Drawing.Size(780, 10);
            panel6.TabIndex = 7;
            // 
            // panel5
            // 
            panel5.Controls.Add(button_Deletar);
            panel5.Controls.Add(button_Alterar);
            panel5.Controls.Add(button_confirmar);
            panel5.Controls.Add(button_criar);
            panel5.Controls.Add(button_Voltar);
            panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel5.Location = new System.Drawing.Point(0, 395);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(780, 35);
            panel5.TabIndex = 6;
            // 
            // button_Deletar
            // 
            button_Deletar.Dock = System.Windows.Forms.DockStyle.Left;
            button_Deletar.Enabled = false;
            button_Deletar.Location = new System.Drawing.Point(230, 0);
            button_Deletar.Name = "button_Deletar";
            button_Deletar.Size = new System.Drawing.Size(115, 35);
            button_Deletar.TabIndex = 9;
            button_Deletar.Text = "Deletar";
            button_Deletar.UseVisualStyleBackColor = true;
            button_Deletar.Click += button_Deletar_Click;
            // 
            // button_Alterar
            // 
            button_Alterar.Dock = System.Windows.Forms.DockStyle.Left;
            button_Alterar.Enabled = false;
            button_Alterar.Location = new System.Drawing.Point(115, 0);
            button_Alterar.Name = "button_Alterar";
            button_Alterar.Size = new System.Drawing.Size(115, 35);
            button_Alterar.TabIndex = 8;
            button_Alterar.Text = "Alterar";
            button_Alterar.UseVisualStyleBackColor = true;
            button_Alterar.Click += button_Alterar_Click;
            // 
            // button_confirmar
            // 
            button_confirmar.Dock = System.Windows.Forms.DockStyle.Left;
            button_confirmar.Enabled = false;
            button_confirmar.Location = new System.Drawing.Point(0, 0);
            button_confirmar.Name = "button_confirmar";
            button_confirmar.Size = new System.Drawing.Size(115, 35);
            button_confirmar.TabIndex = 7;
            button_confirmar.Text = "Criar";
            button_confirmar.UseVisualStyleBackColor = true;
            button_confirmar.Click += button_confirmar_Click;
            // 
            // button_criar
            // 
            button_criar.Location = new System.Drawing.Point(0, 0);
            button_criar.Name = "button_criar";
            button_criar.Size = new System.Drawing.Size(75, 23);
            button_criar.TabIndex = 9;
            // 
            // button_Voltar
            // 
            button_Voltar.Dock = System.Windows.Forms.DockStyle.Right;
            button_Voltar.Location = new System.Drawing.Point(665, 0);
            button_Voltar.Name = "button_Voltar";
            button_Voltar.Size = new System.Drawing.Size(115, 35);
            button_Voltar.TabIndex = 10;
            button_Voltar.Text = "Voltar";
            button_Voltar.UseVisualStyleBackColor = true;
            button_Voltar.Click += button_Voltar_Click;
            // 
            // panel4
            // 
            panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel4.Location = new System.Drawing.Point(10, 440);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(780, 10);
            panel4.TabIndex = 10;
            // 
            // panel3
            // 
            panel3.Dock = System.Windows.Forms.DockStyle.Top;
            panel3.Location = new System.Drawing.Point(10, 0);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(780, 10);
            panel3.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.Dock = System.Windows.Forms.DockStyle.Right;
            panel2.Location = new System.Drawing.Point(790, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(10, 450);
            panel2.TabIndex = 8;
            // 
            // panel1
            // 
            panel1.Dock = System.Windows.Forms.DockStyle.Left;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(10, 450);
            panel1.TabIndex = 7;
            // 
            // Form_Modulos_Familia
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(panel7);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form_Modulos_Familia";
            Text = "Form_Modulos_Familia";
            Load += Form_Modulos_Familia_Load;
            panel7.ResumeLayout(false);
            panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox textBox_linha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button_Deletar;
        private System.Windows.Forms.Button button_Alterar;
        private System.Windows.Forms.Button button_confirmar;
        private System.Windows.Forms.Button button_criar;
        private System.Windows.Forms.Button button_Voltar;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_familia;
        private System.Windows.Forms.Label label2;
    }
}