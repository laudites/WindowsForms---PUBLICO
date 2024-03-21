using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class Form_Exportar_tab_cab : Form
    {
        string Banco;
        Thread t1;
        List<string> valor_Exportar_tab_cab;
        string Login;

        public Form_Exportar_tab_cab(List<string> valor, string banco, string login)
        {
            Login = login;  
            Banco = banco;
            InitializeComponent();
            valor_Exportar_tab_cab = valor;
        }

        private void button_voltar_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Menu);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_Menu(object obj)
        {
            Application.Run(new Form_Gerenciador_SIP(valor_Exportar_tab_cab, Login));
        }

        private void button_exportar_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=INSTANCIA;Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=sa;Password=SENHA");
            var query1 = "select * from " + checkBox_gab_acsg_cab.Text;
            var query2 = "select * from " + checkBox_gab_cab.Text;
            var query3 = "select * from " + checkBox_gab_cabdet.Text;
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();

            Cursor.Current = Cursors.WaitCursor;

            //Exporting to Excel
            string folderPath = @"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\01-Exportar\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (checkBox_gab_acsg_cab.Checked)
            {
                SqlCommand cmd1 = new SqlCommand(query1, con, transaction);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt1, checkBox_gab_acsg_cab.Text);
                    wb.SaveAs(folderPath + checkBox_gab_acsg_cab.Text + ".xlsx");
                }
            }
            if (checkBox_gab_cab.Checked)
            {
                SqlCommand cmd2 = new SqlCommand(query2, con, transaction);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt2, checkBox_gab_cab.Text);
                    wb.SaveAs(folderPath + checkBox_gab_cab.Text + ".xlsx");
                }
            }
            if (checkBox_gab_cabdet.Checked)
            {
                SqlCommand cmd3 = new SqlCommand(query3, con, transaction);
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt3, checkBox_gab_cabdet.Text);
                    wb.SaveAs(folderPath + checkBox_gab_cabdet.Text + ".xlsx");
                }
            }
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;

            MessageBox.Show(@"Tabela exportada com sucesso na pasta C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\01-Exportar");
        }
    }
}
