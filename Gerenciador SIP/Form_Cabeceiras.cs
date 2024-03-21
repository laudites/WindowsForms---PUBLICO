using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class Form_Cabeceiras : Form
    {
        string Banco;
        string Instancia;
        string Senha;
        string Usuario;
        Thread t1;
        List<string> valor_Cabeceira;
        string Login;

        public Form_Cabeceiras(List<string> valor, string banco, string instancia, string senha, string usuario, string login)
        {
            Login = login;
            Banco = banco;
            Instancia = instancia;
            Senha = senha;
            Usuario = usuario;
            InitializeComponent();
            valor_Cabeceira = valor;
        }
        private void atualizar()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "select   \n" +
                                "GAB_CAB.idgabcab, \n" +
                                "GAB_CAB.descricao as cabeceira, \n" +
                                "GAB_CAB.grupo, \n" +
                                "GAB_CAB.chave_busca, \n" +
                                "GAB_CAB.desenho, \n" +
                                "GAB_CAB.largura, \n" +
                                "GAB_CABDET.idgabcabdet, \n" +
                                "GAB_CAB.e_intermediaria, \n" +
                                "GAB_CABDET.flag_e_d, \n" +
                                "GAB_CABDET.ligacao, \n" +
                                "gab_acsg_cab.idgabacsgcab, \n" +
                                "gab_acsg_cab.acessorio \n" +
                            "from GAB_CAB \n" +
                            "full outer join GAB_CABDET on gab_cab.descricao = GAB_CABDET.descricao \n" +
                            "full outer join gab_acsg_cab on GAB_CABDET.descricao = gab_acsg_cab.cabeceira \n" +
                            "where GAB_CABDET.ligacao like '" + textBox_corte.Text + "' \n" +
                            "order by GAB_CABDET.descricao";

            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["idgabcab"].Visible = false;
            this.dataGridView1.Columns["idgabcabdet"].Visible = false;
            this.dataGridView1.Columns["idgabacsgcab"].Visible = false;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Columns[2].DefaultCellStyle.Format = "#0.00\\%";
            }
        }
        private void button_atualizar_Click(object sender, EventArgs e)
        {
            atualizar();
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Deseja importar os dados no banco " + Banco + "?", Banco, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                    {
                        DataGridViewRow selectedRow = r;
                        if (selectedRow.Cells[0].Value != null &&
                            selectedRow.Cells[1].Value != null &&
                            selectedRow.Cells[2].Value != null &&
                            selectedRow.Cells[3].Value != null &&
                            selectedRow.Cells[4].Value != null &&
                            selectedRow.Cells[5].Value != null &&
                            selectedRow.Cells[6].Value != null &&
                            selectedRow.Cells[7].Value != null &&
                            selectedRow.Cells[8].Value != null &&
                            selectedRow.Cells[9].Value != null &&
                            selectedRow.Cells[10].Value != null)
                        {
                            string cabeceira = Convert.ToString(selectedRow.Cells["cabeceira"].Value);
                            string grupo = Convert.ToString(selectedRow.Cells["grupo"].Value);
                            string chave_busca = Convert.ToString(selectedRow.Cells["chave_busca"].Value);
                            string desenho = Convert.ToString(selectedRow.Cells["desenho"].Value);
                            string largura = Convert.ToString(selectedRow.Cells["largura"].Value);
                            string intermediaria = selectedRow.Cells["e_intermediaria"].Value.ToString();
                            string flag_e_d = Convert.ToString(selectedRow.Cells["flag_e_d"].Value);
                            string ligacao = Convert.ToString(selectedRow.Cells["ligacao"].Value);
                            string acessorio = Convert.ToString(selectedRow.Cells["acessorio"].Value);

                            if (intermediaria == "True")
                            {
                                intermediaria = "1";
                            }
                            else if (intermediaria == "False")
                            {
                                intermediaria = "0";
                            }

                            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                            string query_gab_cab = "if NOT EXISTS ( \n" +
                                                       "select* from GAB_CAB \n" +
                                                       "where descricao like '" + cabeceira + "' and grupo like '" + grupo + "' and chave_busca like '" + chave_busca + "' and desenho like '" + desenho + "' and largura like '" + largura + "' and e_intermediaria like '" + intermediaria + "') \n" +
                                                       "insert into GAB_CAB values('" + cabeceira + "','" + grupo + "','" + chave_busca + "','" + desenho + "','" + largura + "',0,0,'" + intermediaria + "') \n" +
                                                       "else if EXISTS(select * from GAB_CAB \n" +
                                                       "where descricao like '" + cabeceira + "' and(grupo not like '" + grupo + "' or chave_busca not like '" + chave_busca + "' or desenho not like '" + desenho + "' or largura not like '" + largura + "' or e_intermediaria not like '" + intermediaria + "')) \n" +
                                                       "insert into GAB_CAB values('" + cabeceira + "','" + grupo + "','" + chave_busca + "','" + desenho + "','" + largura + "',0,0,'" + intermediaria + "')";
                            string query_GAB_CABDET = "insert into GAB_CABDET values ('" + cabeceira + "','" + flag_e_d + "','" + ligacao + "','" + intermediaria + "',0); \n" +
                                "WITH CTE AS(SELECT descricao, flag_e_d,ligacao,RN = ROW_NUMBER()OVER(PARTITION BY descricao, flag_e_d,ligacao ORDER BY descricao) FROM dbo.GAB_CABDET) delete  FROM CTE WHERE RN > 1;";
                            string query_gab_acsg_cab = "insert into gab_acsg_cab values ('" + cabeceira + "','" + acessorio + "',null); \n" +
                                "WITH CTE AS(SELECT cabeceira, acessorio,RN = ROW_NUMBER()OVER(PARTITION BY cabeceira,acessorio ORDER BY cabeceira) FROM dbo.gab_acsg_cab) delete  FROM CTE WHERE RN > 1";
                            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
                            SqlCommand sqlcmd_gab_cab = new SqlCommand(query_gab_cab, sqlconn);

                            SqlCommand sqlcmd_GAB_CABDET = new SqlCommand(query_GAB_CABDET, sqlconn);
                            SqlCommand sqlcmd_gab_acsg_cab = new SqlCommand(query_gab_acsg_cab, sqlconn);
                            sqlconn.Open();

                            try
                            {
                                sqlcmd_gab_cab.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            sqlcmd_GAB_CABDET.ExecuteNonQuery();

                            if (acessorio != "")
                            {
                                sqlcmd_gab_acsg_cab.ExecuteNonQuery();
                            }
                        }
                    }
                    MessageBox.Show("Dados importado com sucesso!");
                    atualizar();
                }
            }
            else
            {
                MessageBox.Show("Selecionar a linha desejada para importar.");
            }
        }

        private void button_alter_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Deseja importar os dados no banco " + Banco + "?", Banco, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                    {
                        DataGridViewRow selectedRow = r;
                        if (selectedRow.Cells[0].Value != null &&
                            selectedRow.Cells[1].Value != null &&
                            selectedRow.Cells[2].Value != null &&
                            selectedRow.Cells[3].Value != null &&
                            selectedRow.Cells[4].Value != null &&
                            selectedRow.Cells[5].Value != null &&
                            selectedRow.Cells[6].Value != null &&
                            selectedRow.Cells[7].Value != null &&
                            selectedRow.Cells[8].Value != null &&
                            selectedRow.Cells[9].Value != null &&
                            selectedRow.Cells[10].Value != null)
                        {
                            string idgabcab = Convert.ToString(selectedRow.Cells["idgabcab"].Value);
                            string cabeceira = Convert.ToString(selectedRow.Cells["cabeceira"].Value);
                            string grupo = Convert.ToString(selectedRow.Cells["grupo"].Value);
                            string chave_busca = Convert.ToString(selectedRow.Cells["chave_busca"].Value);
                            string desenho = Convert.ToString(selectedRow.Cells["desenho"].Value);
                            string largura = Convert.ToString(selectedRow.Cells["largura"].Value);
                            string intermediaria = selectedRow.Cells["e_intermediaria"].Value.ToString();

                            string idgabcabdet = Convert.ToString(selectedRow.Cells["idgabcabdet"].Value);
                            string flag_e_d = Convert.ToString(selectedRow.Cells["flag_e_d"].Value);
                            string ligacao = Convert.ToString(selectedRow.Cells["ligacao"].Value);

                            string idgabacsgcab = Convert.ToString(selectedRow.Cells["idgabacsgcab"].Value);
                            string acessorio = Convert.ToString(selectedRow.Cells["acessorio"].Value);

                            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                            string query_gab_cab = "update GAB_CAB set descricao = '" + cabeceira +
                                                                                                    "',grupo = '" + grupo +
                                                                                                    "', chave_busca = '" + chave_busca +
                                                                                                    "', desenho = '" + desenho +
                                                                                                    "', largura = '" + largura +
                                                                                                    "', e_intermediaria = '" + intermediaria +
                                                                                                    "' where idgabcab = '" + idgabcab + "';";
                            string query_GAB_CABDET = "update GAB_CABDET set descricao = '" + cabeceira +
                                                                                            "', flag_e_d = '" + flag_e_d +
                                                                                            "', ligacao = '" + ligacao +
                                                                                            "', e_intermediaria = '" + intermediaria +
                                                                                            "' where idgabcabdet = '" + idgabcabdet + "';";
                            string query_gab_acsg_cab = "update gab_acsg_cab set cabeceira = '" + cabeceira +
                                                                                                "', acessorio = '" + acessorio +
                                                                                                "' where idgabacsgcab = '" + idgabacsgcab + "';";
                            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
                            SqlCommand sqlcmd_gab_cab = new SqlCommand(query_gab_cab, sqlconn);
                            SqlCommand sqlcmd_GAB_CABDET = new SqlCommand(query_GAB_CABDET, sqlconn);
                            SqlCommand sqlcmd_gab_acsg_cab = new SqlCommand(query_gab_acsg_cab, sqlconn);
                            sqlconn.Open();
                            if (idgabcab != "")
                            {
                                sqlcmd_gab_cab.ExecuteNonQuery();
                            }
                            if (idgabcabdet != "")
                            {
                                sqlcmd_GAB_CABDET.ExecuteNonQuery();
                            }
                            if (idgabacsgcab != "")
                            {
                                sqlcmd_gab_acsg_cab.ExecuteNonQuery();
                            }
                            sqlconn.Close();

                        }
                    }
                    MessageBox.Show("Dados importados com sucesso!");
                    atualizar();
                }
            }
            else
            {
                MessageBox.Show("Selecionar a linha desejada para importar.");
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Deseja importar os dados no banco " + Banco + "?", Banco, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                    {
                        DataGridViewRow selectedRow = r;
                        if (selectedRow.Cells[0].Value != null &&
                            selectedRow.Cells[1].Value != null &&
                            selectedRow.Cells[2].Value != null &&
                            selectedRow.Cells[3].Value != null &&
                            selectedRow.Cells[4].Value != null &&
                            selectedRow.Cells[5].Value != null &&
                            selectedRow.Cells[6].Value != null &&
                            selectedRow.Cells[7].Value != null &&
                            selectedRow.Cells[8].Value != null &&
                            selectedRow.Cells[9].Value != null &&
                            selectedRow.Cells[10].Value != null)
                        {
                            string idgabcab = Convert.ToString(selectedRow.Cells["idgabcab"].Value);

                            string idgabcabdet = Convert.ToString(selectedRow.Cells["idgabcabdet"].Value);

                            string idgabacsgcab = Convert.ToString(selectedRow.Cells["idgabacsgcab"].Value);

                            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                            string query_gab_cab = "delete from gab_cab where idgabcab = '" + idgabcab + "';";
                            string query_GAB_CABDET = "delete from GAB_CABDET where idgabcabdet = '" + idgabcabdet + "';";
                            string query_gab_acsg_cab = "delete from gab_acsg_cab where idgabacsgcab = '" + idgabacsgcab + "';";
                            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
                            SqlCommand sqlcmd_gab_cab = new SqlCommand(query_gab_cab, sqlconn);
                            SqlCommand sqlcmd_GAB_CABDET = new SqlCommand(query_GAB_CABDET, sqlconn);
                            SqlCommand sqlcmd_gab_acsg_cab = new SqlCommand(query_gab_acsg_cab, sqlconn);
                            sqlconn.Open();
                            if (idgabcab != "")
                            {
                                sqlcmd_gab_cab.ExecuteNonQuery();
                            }
                            if (idgabcabdet != "")
                            {
                                sqlcmd_GAB_CABDET.ExecuteNonQuery();
                            }
                            if (idgabacsgcab != "")
                            {
                                sqlcmd_gab_acsg_cab.ExecuteNonQuery();
                            }
                            sqlconn.Close();

                        }
                    }
                    MessageBox.Show("Dados importados com sucesso!");
                    atualizar();
                }
            }
            else
            {
                MessageBox.Show("Selecionar a linha desejada para importar.");
            }
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
            Application.Run(new Form_Gerenciador_SIP(valor_Cabeceira, Login));
        }

        private void button_exportar_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Exportar_tab_cab);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_Exportar_tab_cab(object obj)
        {
            Application.Run(new Form_Exportar_tab_cab(valor_Cabeceira, Banco, Login));
        }


        private void button_importar_tela_Click_1(object sender, EventArgs e)
        {
            var dateString2 = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
            if (File.Exists(@"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\02-Importar\Tela_cabeceiras.xlsx"))
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                string folder_importar = @"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\02-Importar\";
                string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + folder_importar + "Tela_cabeceiras.xlsx" + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                string folder_importardos = @"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\03-Importados\";

                OleDbConnection Con = new OleDbConnection(constr);
                OleDbCommand OleConnection = new OleDbCommand("SELECT * FROM [Tela1$]", Con);
                Con.Open();
                OleDbDataAdapter sda = new OleDbDataAdapter(OleConnection);
                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView1.DataSource = data;
                Con.Close();

                dataGridView1.ClearSelection();

                File.Move(folder_importar + "Tela_cabeceiras" + ".xlsx", folder_importardos + "Tela_cabeceiras" + "_" + dateString2 + ".xlsx");
            }
            else
            {
                MessageBox.Show("Não existem aquivos na pasta para importar!");
            }
        }

        private void button_exportar_tela_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DialogResult dialogResult = MessageBox.Show("Deseja realmente exportar ?", "Exportar", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                //Creating DataTable
                DataTable dt = new DataTable();

                //Adding the Columns
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    dt.Columns.Add(column.HeaderText, column.ValueType);
                }

                //Adding the Rows
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value;
                        }
                    }
                }

                //Exporting to Excel
                string folderPath = @"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\01-Exportar\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Tela1");
                    wb.SaveAs(folderPath + "Tela_cabeceiras" + ".xlsx");
                }
                Cursor.Current = Cursors.Default;

                MessageBox.Show("Tabela exportada com sucesso!");

            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
    }
}
