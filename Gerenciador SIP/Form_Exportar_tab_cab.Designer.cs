
namespace Gerenciador_SIP
{
    partial class Form_Exportar_tab_cab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Exportar_tab_cab));
            this.checkBox_gab_cab = new System.Windows.Forms.CheckBox();
            this.checkBox_gab_cabdet = new System.Windows.Forms.CheckBox();
            this.checkBox_gab_acsg_cab = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_voltar = new System.Windows.Forms.Button();
            this.button_exportar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBox_gab_cab
            // 
            this.checkBox_gab_cab.AutoSize = true;
            this.checkBox_gab_cab.Checked = true;
            this.checkBox_gab_cab.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_gab_cab.Location = new System.Drawing.Point(12, 56);
            this.checkBox_gab_cab.Name = "checkBox_gab_cab";
            this.checkBox_gab_cab.Size = new System.Drawing.Size(77, 19);
            this.checkBox_gab_cab.TabIndex = 0;
            this.checkBox_gab_cab.Text = "GAB_CAB";
            this.checkBox_gab_cab.UseVisualStyleBackColor = true;
            // 
            // checkBox_gab_cabdet
            // 
            this.checkBox_gab_cabdet.AutoSize = true;
            this.checkBox_gab_cabdet.Checked = true;
            this.checkBox_gab_cabdet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_gab_cabdet.Location = new System.Drawing.Point(239, 56);
            this.checkBox_gab_cabdet.Name = "checkBox_gab_cabdet";
            this.checkBox_gab_cabdet.Size = new System.Drawing.Size(97, 19);
            this.checkBox_gab_cabdet.TabIndex = 2;
            this.checkBox_gab_cabdet.Text = "GAB_CABDET";
            this.checkBox_gab_cabdet.UseVisualStyleBackColor = true;
            // 
            // checkBox_gab_acsg_cab
            // 
            this.checkBox_gab_acsg_cab.AutoSize = true;
            this.checkBox_gab_acsg_cab.Checked = true;
            this.checkBox_gab_acsg_cab.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_gab_acsg_cab.Location = new System.Drawing.Point(108, 56);
            this.checkBox_gab_acsg_cab.Name = "checkBox_gab_acsg_cab";
            this.checkBox_gab_acsg_cab.Size = new System.Drawing.Size(112, 19);
            this.checkBox_gab_acsg_cab.TabIndex = 1;
            this.checkBox_gab_acsg_cab.Text = "GAB_ACSG_CAB";
            this.checkBox_gab_acsg_cab.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 37);
            this.label1.TabIndex = 3;
            this.label1.Text = "Exportar tabelas:";
            // 
            // button_voltar
            // 
            this.button_voltar.Location = new System.Drawing.Point(12, 87);
            this.button_voltar.Name = "button_voltar";
            this.button_voltar.Size = new System.Drawing.Size(97, 25);
            this.button_voltar.TabIndex = 3;
            this.button_voltar.Text = "Voltar";
            this.button_voltar.UseVisualStyleBackColor = true;
            this.button_voltar.Click += new System.EventHandler(this.button_voltar_Click);
            // 
            // button_exportar
            // 
            this.button_exportar.Location = new System.Drawing.Point(239, 87);
            this.button_exportar.Name = "button_exportar";
            this.button_exportar.Size = new System.Drawing.Size(97, 25);
            this.button_exportar.TabIndex = 4;
            this.button_exportar.Text = "Exportar";
            this.button_exportar.UseVisualStyleBackColor = true;
            this.button_exportar.Click += new System.EventHandler(this.button_exportar_Click);
            // 
            // Form_Exportar_tab_cab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 122);
            this.Controls.Add(this.button_exportar);
            this.Controls.Add(this.button_voltar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_gab_acsg_cab);
            this.Controls.Add(this.checkBox_gab_cabdet);
            this.Controls.Add(this.checkBox_gab_cab);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Exportar_tab_cab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportar_tab_cab";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_gab_cab;
        private System.Windows.Forms.CheckBox checkBox_gab_cabdet;
        private System.Windows.Forms.CheckBox checkBox_gab_acsg_cab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_voltar;
        private System.Windows.Forms.Button button_exportar;
    }
}