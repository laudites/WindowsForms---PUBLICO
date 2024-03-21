namespace Gerenciador_SIP
{
    partial class Form_Relatorio_Diretoria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Relatorio_Diretoria));
            panel_middle = new System.Windows.Forms.Panel();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            panel_middle_left = new System.Windows.Forms.Panel();
            button_total = new System.Windows.Forms.Button();
            panel_under = new System.Windows.Forms.Panel();
            button_exportar = new System.Windows.Forms.Button();
            button_voltar = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            label8 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label_Valor_Lista = new System.Windows.Forms.Label();
            label_total_valor_venda = new System.Windows.Forms.Label();
            label_Total_Servico = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label_Desconto_over_medio = new System.Windows.Forms.Label();
            panel_top = new System.Windows.Forms.Panel();
            comboBox_Mes = new System.Windows.Forms.ComboBox();
            comboBox_Ano = new System.Windows.Forms.ComboBox();
            comboBox_UF = new System.Windows.Forms.ComboBox();
            comboBox_Status = new System.Windows.Forms.ComboBox();
            comboBox_Vendedor = new System.Windows.Forms.ComboBox();
            comboBox_Gerente = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label_Vendedor = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            textBox_orcamento = new System.Windows.Forms.TextBox();
            label_orcamento = new System.Windows.Forms.Label();
            panel_middle.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel_middle_left.SuspendLayout();
            panel_under.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel_top.SuspendLayout();
            SuspendLayout();
            // 
            // panel_middle
            // 
            panel_middle.Controls.Add(tabControl1);
            panel_middle.Controls.Add(panel_middle_left);
            panel_middle.Dock = System.Windows.Forms.DockStyle.Fill;
            panel_middle.Location = new System.Drawing.Point(0, 89);
            panel_middle.Name = "panel_middle";
            panel_middle.Size = new System.Drawing.Size(1199, 298);
            panel_middle.TabIndex = 5;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tabControl1.Location = new System.Drawing.Point(171, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(1028, 298);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tabPage1.Location = new System.Drawing.Point(4, 30);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(1020, 264);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Lista";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new System.Drawing.Size(1014, 258);
            dataGridView1.TabIndex = 16;
            // 
            // panel_middle_left
            // 
            panel_middle_left.Controls.Add(button_total);
            panel_middle_left.Dock = System.Windows.Forms.DockStyle.Left;
            panel_middle_left.Location = new System.Drawing.Point(0, 0);
            panel_middle_left.Name = "panel_middle_left";
            panel_middle_left.Size = new System.Drawing.Size(171, 298);
            panel_middle_left.TabIndex = 0;
            // 
            // button_total
            // 
            button_total.Dock = System.Windows.Forms.DockStyle.Top;
            button_total.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button_total.Location = new System.Drawing.Point(0, 0);
            button_total.Name = "button_total";
            button_total.Size = new System.Drawing.Size(171, 36);
            button_total.TabIndex = 3;
            button_total.Text = "Geral";
            button_total.UseVisualStyleBackColor = true;
            // 
            // panel_under
            // 
            panel_under.Controls.Add(button_exportar);
            panel_under.Controls.Add(button_voltar);
            panel_under.Controls.Add(panel1);
            panel_under.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel_under.Location = new System.Drawing.Point(0, 387);
            panel_under.Name = "panel_under";
            panel_under.Size = new System.Drawing.Size(1199, 124);
            panel_under.TabIndex = 4;
            // 
            // button_exportar
            // 
            button_exportar.Dock = System.Windows.Forms.DockStyle.Right;
            button_exportar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button_exportar.Location = new System.Drawing.Point(717, 80);
            button_exportar.Name = "button_exportar";
            button_exportar.Size = new System.Drawing.Size(241, 44);
            button_exportar.TabIndex = 1;
            button_exportar.Text = "Exportar";
            button_exportar.UseVisualStyleBackColor = true;
            button_exportar.Click += button_exportar_Click;
            // 
            // button_voltar
            // 
            button_voltar.Dock = System.Windows.Forms.DockStyle.Right;
            button_voltar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button_voltar.Location = new System.Drawing.Point(958, 80);
            button_voltar.Name = "button_voltar";
            button_voltar.Size = new System.Drawing.Size(241, 44);
            button_voltar.TabIndex = 0;
            button_voltar.Text = "Voltar";
            button_voltar.UseVisualStyleBackColor = true;
            button_voltar.Click += button_voltar_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1199, 80);
            panel1.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add(label8);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label_Valor_Lista);
            panel2.Controls.Add(label_total_valor_venda);
            panel2.Controls.Add(label_Total_Servico);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label_Desconto_over_medio);
            panel2.Dock = System.Windows.Forms.DockStyle.Right;
            panel2.Location = new System.Drawing.Point(-262, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(1461, 80);
            panel2.TabIndex = 27;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = System.Drawing.SystemColors.Control;
            label8.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            label8.Location = new System.Drawing.Point(1017, 45);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(263, 28);
            label8.TabIndex = 21;
            label8.Text = "Total Desconto/Over médio:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = System.Drawing.SystemColors.Control;
            label6.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            label6.Location = new System.Drawing.Point(318, 3);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(173, 28);
            label6.TabIndex = 19;
            label6.Text = "Total valor venda:";
            // 
            // label_Valor_Lista
            // 
            label_Valor_Lista.AutoSize = true;
            label_Valor_Lista.BackColor = System.Drawing.SystemColors.Control;
            label_Valor_Lista.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            label_Valor_Lista.Location = new System.Drawing.Point(889, 3);
            label_Valor_Lista.Name = "label_Valor_Lista";
            label_Valor_Lista.Size = new System.Drawing.Size(134, 28);
            label_Valor_Lista.TabIndex = 24;
            label_Valor_Lista.Text = "R$000000000";
            // 
            // label_total_valor_venda
            // 
            label_total_valor_venda.AutoSize = true;
            label_total_valor_venda.BackColor = System.Drawing.SystemColors.Control;
            label_total_valor_venda.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            label_total_valor_venda.Location = new System.Drawing.Point(486, 3);
            label_total_valor_venda.Name = "label_total_valor_venda";
            label_total_valor_venda.Size = new System.Drawing.Size(134, 28);
            label_total_valor_venda.TabIndex = 23;
            label_total_valor_venda.Text = "R$000000000";
            // 
            // label_Total_Servico
            // 
            label_Total_Servico.AutoSize = true;
            label_Total_Servico.BackColor = System.Drawing.SystemColors.Control;
            label_Total_Servico.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            label_Total_Servico.Location = new System.Drawing.Point(1286, 3);
            label_Total_Servico.Name = "label_Total_Servico";
            label_Total_Servico.Size = new System.Drawing.Size(134, 28);
            label_Total_Servico.TabIndex = 26;
            label_Total_Servico.Text = "R$000000000";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = System.Drawing.SystemColors.Control;
            label9.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            label9.Location = new System.Drawing.Point(1142, 3);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(138, 28);
            label9.TabIndex = 22;
            label9.Text = "Total serviços:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = System.Drawing.SystemColors.Control;
            label7.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            label7.Location = new System.Drawing.Point(726, 3);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(157, 28);
            label7.TabIndex = 20;
            label7.Text = "Total valor lista:";
            // 
            // label_Desconto_over_medio
            // 
            label_Desconto_over_medio.AutoSize = true;
            label_Desconto_over_medio.BackColor = System.Drawing.SystemColors.Control;
            label_Desconto_over_medio.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            label_Desconto_over_medio.Location = new System.Drawing.Point(1286, 45);
            label_Desconto_over_medio.Name = "label_Desconto_over_medio";
            label_Desconto_over_medio.Size = new System.Drawing.Size(73, 28);
            label_Desconto_over_medio.TabIndex = 25;
            label_Desconto_over_medio.Text = "0000%";
            // 
            // panel_top
            // 
            panel_top.Controls.Add(comboBox_Mes);
            panel_top.Controls.Add(comboBox_Ano);
            panel_top.Controls.Add(comboBox_UF);
            panel_top.Controls.Add(comboBox_Status);
            panel_top.Controls.Add(comboBox_Vendedor);
            panel_top.Controls.Add(comboBox_Gerente);
            panel_top.Controls.Add(label2);
            panel_top.Controls.Add(label5);
            panel_top.Controls.Add(label4);
            panel_top.Controls.Add(label3);
            panel_top.Controls.Add(label_Vendedor);
            panel_top.Controls.Add(label1);
            panel_top.Controls.Add(textBox_orcamento);
            panel_top.Controls.Add(label_orcamento);
            panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            panel_top.Location = new System.Drawing.Point(0, 0);
            panel_top.Name = "panel_top";
            panel_top.Size = new System.Drawing.Size(1199, 89);
            panel_top.TabIndex = 3;
            // 
            // comboBox_Mes
            // 
            comboBox_Mes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBox_Mes.FormattingEnabled = true;
            comboBox_Mes.Location = new System.Drawing.Point(371, 48);
            comboBox_Mes.Name = "comboBox_Mes";
            comboBox_Mes.Size = new System.Drawing.Size(129, 29);
            comboBox_Mes.TabIndex = 18;
            comboBox_Mes.DropDown += comboBox_Mes_DropDown;
            comboBox_Mes.SelectedIndexChanged += comboBox_Mes_SelectedIndexChanged;
            // 
            // comboBox_Ano
            // 
            comboBox_Ano.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBox_Ano.FormattingEnabled = true;
            comboBox_Ano.Location = new System.Drawing.Point(132, 48);
            comboBox_Ano.Name = "comboBox_Ano";
            comboBox_Ano.Size = new System.Drawing.Size(129, 29);
            comboBox_Ano.TabIndex = 17;
            comboBox_Ano.DropDown += comboBox_Ano_DropDown;
            comboBox_Ano.SelectedIndexChanged += comboBox_Ano_SelectedIndexChanged;
            // 
            // comboBox_UF
            // 
            comboBox_UF.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBox_UF.FormattingEnabled = true;
            comboBox_UF.Location = new System.Drawing.Point(1089, 12);
            comboBox_UF.Name = "comboBox_UF";
            comboBox_UF.Size = new System.Drawing.Size(101, 29);
            comboBox_UF.TabIndex = 16;
            comboBox_UF.DropDown += comboBox_UF_DropDown;
            comboBox_UF.SelectedIndexChanged += comboBox_UF_SelectedIndexChanged;
            // 
            // comboBox_Status
            // 
            comboBox_Status.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBox_Status.FormattingEnabled = true;
            comboBox_Status.Location = new System.Drawing.Point(908, 11);
            comboBox_Status.Name = "comboBox_Status";
            comboBox_Status.Size = new System.Drawing.Size(129, 29);
            comboBox_Status.TabIndex = 15;
            comboBox_Status.DropDown += comboBox_Status_DropDown;
            comboBox_Status.SelectedIndexChanged += comboBox_Status_SelectedIndexChanged;
            // 
            // comboBox_Vendedor
            // 
            comboBox_Vendedor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBox_Vendedor.FormattingEnabled = true;
            comboBox_Vendedor.Location = new System.Drawing.Point(671, 12);
            comboBox_Vendedor.Name = "comboBox_Vendedor";
            comboBox_Vendedor.Size = new System.Drawing.Size(156, 29);
            comboBox_Vendedor.TabIndex = 14;
            comboBox_Vendedor.DropDown += comboBox_Vendedor_DropDown;
            comboBox_Vendedor.SelectedIndexChanged += comboBox_Vendedor_SelectedIndexChanged;
            // 
            // comboBox_Gerente
            // 
            comboBox_Gerente.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBox_Gerente.FormattingEnabled = true;
            comboBox_Gerente.Location = new System.Drawing.Point(371, 10);
            comboBox_Gerente.Name = "comboBox_Gerente";
            comboBox_Gerente.Size = new System.Drawing.Size(193, 29);
            comboBox_Gerente.TabIndex = 13;
            comboBox_Gerente.DropDown += comboBox_Gerente_DropDown;
            comboBox_Gerente.SelectedIndexChanged += comboBox_Gerente_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(833, 7);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(69, 28);
            label2.TabIndex = 12;
            label2.Text = "Status:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label5.Location = new System.Drawing.Point(313, 48);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(52, 28);
            label5.TabIndex = 10;
            label5.Text = "Mês:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label4.Location = new System.Drawing.Point(74, 45);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(52, 28);
            label4.TabIndex = 8;
            label4.Text = "Ano:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label3.Location = new System.Drawing.Point(1043, 8);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(40, 28);
            label3.TabIndex = 6;
            label3.Text = "UF:";
            // 
            // label_Vendedor
            // 
            label_Vendedor.AutoSize = true;
            label_Vendedor.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label_Vendedor.Location = new System.Drawing.Point(570, 9);
            label_Vendedor.Name = "label_Vendedor";
            label_Vendedor.Size = new System.Drawing.Size(101, 28);
            label_Vendedor.TabIndex = 4;
            label_Vendedor.Text = "Vendedor:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(286, 8);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(85, 28);
            label1.TabIndex = 2;
            label1.Text = "Gerente:";
            // 
            // textBox_orcamento
            // 
            textBox_orcamento.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox_orcamento.Location = new System.Drawing.Point(132, 6);
            textBox_orcamento.Name = "textBox_orcamento";
            textBox_orcamento.Size = new System.Drawing.Size(148, 34);
            textBox_orcamento.TabIndex = 0;
            textBox_orcamento.TextChanged += textBox_orcamento_TextChanged;
            // 
            // label_orcamento
            // 
            label_orcamento.AutoSize = true;
            label_orcamento.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label_orcamento.Location = new System.Drawing.Point(12, 9);
            label_orcamento.Name = "label_orcamento";
            label_orcamento.Size = new System.Drawing.Size(114, 28);
            label_orcamento.TabIndex = 0;
            label_orcamento.Text = "Orçamento:";
            // 
            // Form_Relatorio_Diretoria
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1199, 511);
            Controls.Add(panel_middle);
            Controls.Add(panel_under);
            Controls.Add(panel_top);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(1215, 550);
            Name = "Form_Relatorio_Diretoria";
            Text = "Relatorio Gerencial";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += Form_Relatorio_Diretoria_Load;
            panel_middle.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel_middle_left.ResumeLayout(false);
            panel_under.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel_top.ResumeLayout(false);
            panel_top.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel_middle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel_middle_left;
        private System.Windows.Forms.Button button_total;
        private System.Windows.Forms.Panel panel_under;
        private System.Windows.Forms.Button button_exportar;
        private System.Windows.Forms.Button button_voltar;
        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.TextBox textBox_orcamento;
        private System.Windows.Forms.Label label_orcamento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_Vendedor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Gerente;
        private System.Windows.Forms.ComboBox comboBox_Vendedor;
        private System.Windows.Forms.ComboBox comboBox_Status;
        private System.Windows.Forms.ComboBox comboBox_UF;
        private System.Windows.Forms.ComboBox comboBox_Ano;
        private System.Windows.Forms.ComboBox comboBox_Mes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label_Total_Servico;
        private System.Windows.Forms.Label label_Desconto_over_medio;
        private System.Windows.Forms.Label label_Valor_Lista;
        private System.Windows.Forms.Label label_total_valor_venda;
        private System.Windows.Forms.Panel panel2;
    }
}