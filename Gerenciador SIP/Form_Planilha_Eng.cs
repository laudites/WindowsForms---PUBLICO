using ClosedXML.Excel;
using DocumentFormat.OpenXml.Math;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class Form_Planilha_Eng : Form
    {
        List<string> valor_Gerenciador_SIP;
        string Banco = "BANCODEDADOS";
        string Instancia = @"INSTANCIA";
        string Senha = "SENHA";
        string Usuario = "sa";
        string Linha;
        Thread t1;
        private DataTable dataTable;
        string Login;
        string OrigemCopia = "";
        private List<string> modelosOriginais = new List<string>();
        //private BindingSource bindingSource = new BindingSource();
        public Form_Planilha_Eng(List<string> valorGerenciador, string login)
        {
            valor_Gerenciador_SIP = valorGerenciador;
            InitializeComponent();
            Lista_Linhas();
            dataTable = new DataTable();
            Login = login;
            textBox_filter.KeyUp += textBox_filter_KeyUp; // Adicione esta linha para associar o evento KeyUp ao método de filtragem.
            controleAcessoUsuario();
        }

        private void controleAcessoUsuario()
        {
            if (valor_Gerenciador_SIP.Contains("Engenharia de Produtos"))
            {
                button_salvar.Enabled = true;
                button_deletar.Enabled = true;
                button_copiar.Enabled = true;
                button_importar.Enabled = true;
                dataGridView1.ReadOnly = false;
            }
        }

        private void Lista_Linhas()
        {
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
            Cursor.Current = Cursors.WaitCursor;
            con.Open();
            string query = "SELECT Linha From Linha_Expositor";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    comboBox1.Items.Add("");
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString(0));
                    }
                    con.Close();
                    comboBox1.SelectedIndex = 0;
                    Linha = comboBox1.Text;
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void Lista_Expositor()
        {
            /* if (comboBox1.Text != "")
             {
                 //checkedListBox1.Items.Clear();
                 SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
                 Cursor.Current = Cursors.WaitCursor;
                 con.Open();
                 string query = "SELECT [Modelo],[Tipo],[Resfriamento],[Situacao],[Linha] FROM [Modelos_Eng] WHERE Linha = '" + comboBox1.Text + "' order by Modelo;";
                 using (SqlCommand command = new SqlCommand(query, con))
                 {
                     using (SqlDataReader reader = command.ExecuteReader())
                     {
                         while (reader.Read())
                         {
                             checkedListBox1.Items.Add(reader.GetString(0));
                         }
                     }
                 }
                 con.Close();
                 Cursor.Current = Cursors.Default;
             }*/
            if (comboBox1.Text != "")
            {
                checkedListBox1.Items.Clear();
                modelosOriginais.Clear();

                SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
                Cursor.Current = Cursors.WaitCursor;
                con.Open();
                string query = "SELECT [Modelo],[Tipo],[Resfriamento],[Situacao],[Linha] FROM [Modelos_Eng] WHERE Linha = '" + comboBox1.Text + "' order by Modelo;";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string modelo = reader.GetString(0);
                            modelosOriginais.Add(modelo); // Adiciona o modelo à lista original.
                        }
                    }
                }

                // Exibe os modelos filtrados inicialmente (sem filtro).
                FiltrarModelos();
                con.Close();
                Cursor.Current = Cursors.Default;
            }

        }


        private void button_Atualizar_Click(object sender, EventArgs e)
        {
            Atualizar();
        }

        private void Atualizar()
        {
            string selectedItemsQuery = "";

            foreach (object item in checkedListBox1.CheckedItems)
            {
                string itemName = item.ToString();
                selectedItemsQuery += "or Modelo = '" + itemName + "' ";
            }

            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
            Cursor.Current = Cursors.WaitCursor;
            con.Open();
            string query = "SELECT * FROM [BANCODEDADOS].[dbo].[Planilhas_Eng] \n" +
                            "where basecodigo = 'Eletrofrio' and Modelo = '' " + selectedItemsQuery;
            dataTable.Clear();

            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@Linha", Linha);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }

            con.Close();
            Cursor.Current = Cursors.Default;
            dataGridView1.DataSource = dataTable;
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
            Application.Run(new Form_Gerenciador_SIP(valor_Gerenciador_SIP, Login));
        }

        private void button_exportar_Click(object sender, EventArgs e)
        {
            Atualizar();
            Cursor.Current = Cursors.WaitCursor;
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DataTable dt = new DataTable();
            List<string> resultadosSelecionados = new List<string>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                string resultadoSelecionado = item.ToString();
                resultadosSelecionados.Add(resultadoSelecionado);
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dt.Columns.Add(column.HeaderText, column.ValueType);
            }
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
            string folderPath = @"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\01-Exportar\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Pagina1");
                wb.SaveAs(folderPath + "Planilhas_Eng.xlsx");
            }
            string resultadosSelecionadosString = string.Join("|", resultadosSelecionados);
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
            con.Open();
            string query_log = "insert into Log_eng values ('" + Login + "','Planilha de engenharia','" + resultadosSelecionadosString + "','','','Exportação de planilha','" + dataAtual + "',NULL,NULL);";
            SqlTransaction transaction1 = con.BeginTransaction();
            SqlCommand insertCmd = new SqlCommand(query_log, con, transaction1);
            insertCmd.ExecuteNonQuery();
            transaction1.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Tabela exportada com sucesso!");
        }

        private void button_importar_Click(object sender, EventArgs e)
        {
            var dateString2 = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
            DeletarTabelaTemp();
            CriarTabelaTemp();
            FormatarPlanilha();
            ImportarExcel();
            InserirLinhasNovas();
            LimparTabelasInseridas();
            MargeTabelaImport();
            DeletarTabelaTemp();
            File.Move(@"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\02-Importar\Planilhas_Eng.xlsx", @"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\03-Importados\Planilhas_Eng-" + dateString2 + ".xlsx");
            Atualizar();
            MessageBox.Show("Importado com sucesso!");
        }

        private void FormatarPlanilha()
        {
            var file = new FileInfo(@"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\02-Importar\Planilhas_Eng.xlsx");
            using (var package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets["Pagina1"];
                var column = worksheet.Cells["E:S"];
                column.Style.Numberformat.Format = "@";
                package.Save();
            }
        }



        private void LimparTabelasInseridas()
        {
            Cursor.Current = Cursors.WaitCursor;
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            var query1 = "  delete  FROM [BANCODEDADOS].[dbo].[TempPlanilhas_Eng] where ID is null";
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;
        }

        private void InserirNotificacaoNovas()
        {
            Cursor.Current = Cursors.WaitCursor;
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            var query1 = "insert into notificacao select \n" +
                            $"'{Login}', \n" +
                            "TempPlanilhas_Eng.modelo, \n" +
                            "'', \n" +
                            "CONCAT( \n" +
                                "CASE WHEN Componente IS Not NULL THEN Componente ELSE ',_' END, \n" +
                                "CASE WHEN Cores IS NOT NULL THEN ',' + Cores ELSE ',_' END, \n" +
                                "CASE WHEN M125 IS NOT NULL THEN ',' + M125 ELSE ',_' END, \n" +
                                "CASE WHEN M150 IS NOT NULL THEN ',' + M150 ELSE ',_' END, \n" +
                                "CASE WHEN M187 IS NOT NULL THEN ',' + M187 ELSE ',_' END, \n" +
                                "CASE WHEN M225 IS NOT NULL THEN ',' + M225 ELSE ',_' END, \n" +
                                "CASE WHEN M250 IS NOT NULL THEN ',' + M250 ELSE ',_' END, \n" +
                                "CASE WHEN M300 IS NOT NULL THEN ',' + M300 ELSE ',_' END, \n" +
                                "CASE WHEN M375 IS NOT NULL THEN ',' + M375 ELSE ',_' END, \n" +
                                "CASE WHEN E45 IS NOT NULL THEN ',' + E45 ELSE ',_' END, \n" +
                                "CASE WHEN E90 IS NOT NULL THEN ',' + E90 ELSE ',_' END, \n" +
                                "CASE WHEN T1875 IS NOT NULL THEN ',' + T1875 ELSE ',_' END, \n" +
                                "CASE WHEN T2167 IS NOT NULL THEN ',' + T2167 ELSE ',_' END, \n" +
                                "CASE WHEN T2500 IS NOT NULL THEN ',' + T2500 ELSE ',_' END, \n" +
                                "CASE WHEN T360 IS NOT NULL THEN ',' + T360 ELSE ',_' END, \n" +
                                "CASE WHEN Observacao IS NOT NULL THEN ',' + Observacao ELSE ',_' END, \n" +
                                "CASE WHEN TempPlanilhas_Eng.LiberarRepresentante IS NOT NULL THEN ',' + CONVERT(VARCHAR, TempPlanilhas_Eng.LiberarRepresentante) ELSE ',_' END,\n" +
                                "CASE WHEN TempPlanilhas_Eng.Acessarios_CAB IS NOT NULL THEN ',' + CONVERT(VARCHAR, TempPlanilhas_Eng.Acessarios_CAB) ELSE ',_' END,\n" +
                                "CASE WHEN TempPlanilhas_Eng.BaseCodigo IS NOT NULL THEN ',' + TempPlanilhas_Eng.BaseCodigo ELSE ',_' END\n" +
                            ") as Alteracao_Para, \n" +
                            "'Novo acessorio', \n" +
                            $"'{dataAtual}', \n" +
                            "NULL, \n" +
                            "NULL \n" +
                            "from TempPlanilhas_Eng where ID is null; ";
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;
        }

        private void InserirLinhasNovas()
        {
            Cursor.Current = Cursors.WaitCursor;
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            con.Open();
            string query_log_de = "select * from TempPlanilhas_Eng where ID is null";
            SqlCommand cmd_log_de = new SqlCommand(query_log_de, con);
            SqlDataReader reader_log_de = cmd_log_de.ExecuteReader();
            string resultadoColuna1 = null;
            while (reader_log_de.Read())
            {
                StringBuilder linhaBuilder = new StringBuilder();
                string Modelo = null;

                for (int i = 0; i < reader_log_de.FieldCount; i++)
                {
                    object valorColuna = reader_log_de.GetValue(i);
                    string valorColunaStr = (valorColuna != null) ? valorColuna.ToString() : string.Empty;
                    if (i == 1)
                    {
                        Modelo = valorColunaStr;
                    }
                    linhaBuilder.Append(valorColunaStr);

                    if (i < reader_log_de.FieldCount - 1)
                    {
                        linhaBuilder.Append(",");
                    }
                }
                resultadoColuna1 = linhaBuilder.ToString();
                SqlConnection con1 = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
                con1.Open();
                string insert_log = "insert into Log_eng values ('" + Login + "','Planilha de engenharia','" + Modelo + "','','" + resultadoColuna1 + "','Importação de planilha - Inserção de dados','" + dataAtual + "',NULL,NULL);";
                SqlTransaction transaction1 = con1.BeginTransaction();
                SqlCommand cmd1 = new SqlCommand(insert_log, con1, transaction1);
                cmd1.ExecuteNonQuery();
                transaction1.Commit();
                con1.Close();
                InserirNotificacaoNovas();
            }

            reader_log_de.Close();

            var query1 = "insert into Planilhas_Eng \n" +
                         "SELECT [Modelo],[Componente],[Cores],[M125],[M150],[M187],[M225],[M250],[M300],[M375],[E45],[E90],[I45],[I90],[T1875],[T2167],[T2500],[T360],[Observacao],[LiberarRepresentante],[Acessarios_CAB],[BaseCodigo] FROM[BANCODEDADOS].[dbo].[TempPlanilhas_Eng] where ID is null and [Modelo] is not null";

            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;
        }

        private void DeletarTabelaTemp()
        {
            Cursor.Current = Cursors.WaitCursor;
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            var query1 = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempPlanilhas_Eng]') AND type in (N'U')) \n" +
                            "DROP TABLE[dbo].[TempPlanilhas_Eng] ";
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;
        }

        private void CriarTabelaTemp()
        {
            Cursor.Current = Cursors.WaitCursor;
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            var query1 = "CREATE TABLE TempPlanilhas_Eng ([ID] INT, [Modelo] VARCHAR(MAX), [Componente] VARCHAR(MAX), [Cores] VARCHAR(MAX), [M125] VARCHAR(MAX),[M150] [nvarchar](255) NULL, [M187] VARCHAR(MAX),[M225] [nvarchar](255) NULL, [M250] VARCHAR(MAX),[M300] [nvarchar](255) NULL, [M375] VARCHAR(MAX), [E45] VARCHAR(MAX), [E90] VARCHAR(MAX), [I45] VARCHAR(MAX), [I90] VARCHAR(MAX), [T1875] VARCHAR(MAX), [T2167] VARCHAR(MAX), [T2500] VARCHAR(MAX), [T360] VARCHAR(MAX), [Observacao] VARCHAR(MAX), [LiberarRepresentante] [bit] NULL,[Acessarios_CAB] [bit] NULL,[BaseCodigo] [nvarchar](100) NULL)";
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;
        }
        private void ImportarExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            string sexcelconnectionstring = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source=" + @"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\02-Importar\Planilhas_Eng.xlsx" +
                                         ";Extended Properties = " + "\"Excel 12.0 Xml;HDR=YES;\"";
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha;
            string myexceldataquery = "select * from [Pagina1$]";
            OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionstring);
            OleDbCommand oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);
            oledbconn.Open();
            OleDbDataReader dr = oledbcmd.ExecuteReader();
            SqlBulkCopy bulkcopy = new SqlBulkCopy(ssqlconnectionstring);
            bulkcopy.DestinationTableName = "TempPlanilhas_Eng";
            bulkcopy.WriteToServer(dr);
            oledbconn.Close();
            Cursor.Current = Cursors.Default;
        }

        private void InserirNotificacaoAlteracoes()
        {
            Cursor.Current = Cursors.WaitCursor;
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            var query1 = "insert into notificacao select \n" +
                            $"'{Login}', \n" +
                            "Temp_Resultado.modelo, \n" +
                            "CONCAT( Temp_Resultado.modelo+',',\n" +
                            "CASE WHEN Temp_Resultado.Componente IS Not NULL THEN Temp_Resultado.Componente ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.Cores IS NOT NULL THEN ',' + Temp_Resultado.Cores ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.M125 IS NOT NULL THEN ',' + Temp_Resultado.M125 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.M150 IS NOT NULL THEN ',' + Temp_Resultado.M150 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.M187 IS NOT NULL THEN ',' + Temp_Resultado.M187 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.M225 IS NOT NULL THEN ',' + Temp_Resultado.M225 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.M250 IS NOT NULL THEN ',' + Temp_Resultado.M250 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.M300 IS NOT NULL THEN ',' + Temp_Resultado.M300 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.M375 IS NOT NULL THEN ',' + Temp_Resultado.M375 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.E45 IS NOT NULL THEN ',' + Temp_Resultado.E45 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.E90 IS NOT NULL THEN ',' + Temp_Resultado.E90 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.T1875 IS NOT NULL THEN ',' + Temp_Resultado.T1875 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.T2167 IS NOT NULL THEN ',' + Temp_Resultado.T2167 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.T2500 IS NOT NULL THEN ',' + Temp_Resultado.T2500 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.T360 IS NOT NULL THEN ',' + Temp_Resultado.T360 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.Observacao IS NOT NULL THEN ',' + Temp_Resultado.Observacao ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.LiberarRepresentante IS NOT NULL THEN ',' + CONVERT(VARCHAR, Temp_Resultado.LiberarRepresentante) ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.Acessarios_CAB IS NOT NULL THEN ',' + CONVERT(VARCHAR, Temp_Resultado.Acessarios_CAB) ELSE ',_' END, \n" +
                                "CASE WHEN Temp_Resultado.BaseCodigo IS NOT NULL THEN ',' + Temp_Resultado.BaseCodigo ELSE ',_' END \n" +
                            ") as Alteracao_De, \n" +
                            "CONCAT( Temp_Resultado.modelo+',',\n" +
                            "CASE WHEN Temp_ResultadoTemp.Componente IS Not NULL THEN Temp_ResultadoTemp.Componente ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.Cores IS NOT NULL THEN ',' + Temp_ResultadoTemp.Cores ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.M125 IS NOT NULL THEN ',' + Temp_ResultadoTemp.M125 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.M150 IS NOT NULL THEN ',' + Temp_ResultadoTemp.M150 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.M187 IS NOT NULL THEN ',' + Temp_ResultadoTemp.M187 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.M225 IS NOT NULL THEN ',' + Temp_ResultadoTemp.M225 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.M250 IS NOT NULL THEN ',' + Temp_ResultadoTemp.M250 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.M300 IS NOT NULL THEN ',' + Temp_ResultadoTemp.M300 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.M375 IS NOT NULL THEN ',' + Temp_ResultadoTemp.M375 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.E45 IS NOT NULL THEN ',' + Temp_ResultadoTemp.E45 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.E90 IS NOT NULL THEN ',' + Temp_ResultadoTemp.E90 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.T1875 IS NOT NULL THEN ',' + Temp_ResultadoTemp.T1875 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.T2167 IS NOT NULL THEN ',' + Temp_ResultadoTemp.T2167 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.T2500 IS NOT NULL THEN ',' + Temp_ResultadoTemp.T2500 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.T360 IS NOT NULL THEN ',' + Temp_ResultadoTemp.T360 ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.Observacao IS NOT NULL THEN ',' + Temp_ResultadoTemp.Observacao ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.LiberarRepresentante IS NOT NULL THEN ',' + CONVERT(VARCHAR, Temp_ResultadoTemp.LiberarRepresentante) ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.Acessarios_CAB IS NOT NULL THEN ',' + CONVERT(VARCHAR, Temp_ResultadoTemp.Acessarios_CAB) ELSE ',_' END, \n" +
                                "CASE WHEN Temp_ResultadoTemp.BaseCodigo IS NOT NULL THEN ',' + Temp_ResultadoTemp.BaseCodigo ELSE ',_' END \n" +
                            ") as Alteracao_Para, \n" +
                            "'Alteracao acessorio', \n" +
                            $"'{dataAtual}', \n" +
                            "NULL,NULL \n" +
                            "from Temp_Resultado \n" +
                            "inner join Temp_ResultadoTemp on Temp_Resultado.ID = Temp_ResultadoTemp.ID";
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;
        }

        private void MargeTabelaImport()
        {
            Cursor.Current = Cursors.WaitCursor;
            Log_Marge_Delete();
            Log_Marge();
            InserirNotificacaoAlteracoes();
            Log_Marge_Insert();
            Log_Marge_Delete();
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            var query1 = "SET IDENTITY_INSERT Planilhas_Eng ON \n" +
                            "MERGE INTO Planilhas_Eng AS Target \n" +
                            "USING TempPlanilhas_Eng AS Source \n" +
                            "ON(Target.ID = Source.ID) \n" +
                            "WHEN MATCHED THEN \n" +
                            "UPDATE SET Target.Modelo = Source.Modelo, \n" +
                               "Target.Componente = Source.Componente, \n" +
                               "Target.Cores = Source.Cores, \n" +
                               "Target.M125 = Source.M125, \n" +
                               "Target.M187 = Source.M187, \n" +
                               "Target.M250 = Source.M250, \n" +
                               "Target.M375 = Source.M375, \n" +
                               "Target.E45 = Source.E45, \n" +
                               "Target.E90 = Source.E90, \n" +
                               "Target.I45 = Source.I45, \n" +
                               "Target.I90 = Source.I90, \n" +
                               "Target.T1875 = Source.T1875, \n" +
                               "Target.T2167 = Source.T2167, \n" +
                               "Target.T2500 = Source.T2500, \n" +
                               "Target.T360 = Source.T360, \n" +
                               "Target.Observacao = Source.Observacao, \n" +
                               "Target.LiberarRepresentante = Source.LiberarRepresentante, \n" +
                               "Target.Acessarios_CAB = Source.Acessarios_CAB, \n" +
                               "Target.BaseCodigo = Source.BaseCodigo \n" +
                                "WHEN NOT MATCHED THEN \n" +
                                    "INSERT(ID, Modelo, Componente, Cores, M125, M187, M250, M375, E45, E90, I45, I90, T1875, T2167, T2500, T360, Observacao,  LiberarRepresentante,Acessarios_CAB,BaseCodigo) \n" +
                                    "VALUES(Source.ID, Source.Modelo, Source.Componente, Source.Cores, Source.M125, Source.M187, Source.M250, Source.M375, Source.E45, Source.E90, Source.I45, Source.I90, Source.T1875, Source.T2167, Source.T2500, Source.T360, Source.Observacao, Source.LiberarRepresentante,Source.Acessarios_CAB, Source.BaseCodigo); \n" +
                                    "SET IDENTITY_INSERT Planilhas_Eng OFF;";
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            con.Close();

            Cursor.Current = Cursors.Default;
        }


        private void Log_Marge_Insert()
        {
            Cursor.Current = Cursors.WaitCursor;
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int numLinhas = 0;
            List<string> modeloList = new List<string>();
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            con.Open();
            string query_log_de = "select * from TEMP_Resultado";
            SqlCommand cmd_log_de = new SqlCommand(query_log_de, con);
            SqlDataReader reader_log_de = cmd_log_de.ExecuteReader();
            List<string> resultadoColuna1List = new List<string>();
            while (reader_log_de.Read())
            {
                StringBuilder linhaBuilder = new StringBuilder();

                for (int i = 0; i < reader_log_de.FieldCount; i++)
                {
                    object valorColuna = reader_log_de.GetValue(i);
                    string valorColunaStr = (valorColuna != null) ? valorColuna.ToString() : string.Empty;

                    if (i == 1)
                    {
                        modeloList.Add(valorColunaStr);
                    }
                    linhaBuilder.Append(valorColunaStr);

                    if (i < reader_log_de.FieldCount - 1)
                    {
                        linhaBuilder.Append(",");
                    }
                }
                resultadoColuna1List.Add(linhaBuilder.ToString());
            }
            reader_log_de.Close();
            SqlConnection con2 = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            con2.Open();
            string query_log_de2 = "select * from TEMP_ResultadoTemp";
            SqlCommand cmd_log_de2 = new SqlCommand(query_log_de2, con2);
            SqlDataReader reader_log_de2 = cmd_log_de2.ExecuteReader();
            List<string> resultadoColuna2List = new List<string>();

            while (reader_log_de2.Read())
            {
                StringBuilder linhaBuilder2 = new StringBuilder();

                for (int i = 0; i < reader_log_de2.FieldCount; i++)
                {
                    object valorColuna2 = reader_log_de2.GetValue(i);
                    string valorColunaStr2 = (valorColuna2 != null) ? valorColuna2.ToString() : string.Empty;
                    if (i == 1)
                    {
                        if (modeloList[numLinhas] == "" || modeloList[numLinhas] is null)
                        {
                            modeloList[i] = valorColunaStr2;
                        }
                    }
                    linhaBuilder2.Append(valorColunaStr2);

                    if (i < reader_log_de2.FieldCount - 1)
                    {
                        linhaBuilder2.Append(",");
                    }
                }
                resultadoColuna2List.Add(linhaBuilder2.ToString());
                numLinhas++;
            }
            reader_log_de2.Close();
            for (int i = 0; resultadoColuna1List.Count > i; i++)
            {
                string resultadoColuna1 = resultadoColuna1List[i];
                string resultadoColuna2 = resultadoColuna2List[i];
                string modelo = modeloList[i];

                SqlConnection con1 = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
                con1.Open();
                string insert_log = "insert into Log_eng values ('" + Login + "','Planilha de engenharia','" + modelo + "','" + resultadoColuna1 + "','" + resultadoColuna2 + "','Importação de planilha - Alteração de dados','" + dataAtual + "',NULL,NULL);";
                SqlTransaction transaction1 = con1.BeginTransaction();
                SqlCommand cmd1 = new SqlCommand(insert_log, con1, transaction1);
                cmd1.ExecuteNonQuery();
                transaction1.Commit();
                con1.Close();
            }
        }
        private void Log_Marge_Delete()
        {
            Cursor.Current = Cursors.WaitCursor;
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            var query1 = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEMP_Resultado]') AND type in (N'U')) \n" +
                            "DROP TABLE[dbo].[TEMP_Resultado] ";
            var query2 = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEMP_ResultadoTemp]') AND type in (N'U')) \n" +
                            "DROP TABLE[dbo].[TEMP_ResultadoTemp] ";
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            SqlTransaction transaction2 = con.BeginTransaction();
            SqlCommand cmd2 = new SqlCommand(query2, con, transaction2);
            cmd2.ExecuteNonQuery();
            transaction2.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;
        }

        private void Log_Marge()
        {
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            var query1 = "--Tabela do Resultado final \n" +
                                "CREATE TABLE TEMP_Resultado ( \n" +
                                "ID INT, \n" +
                                "[Modelo] [nvarchar] (255) NOT NULL, \n" +
                                "[Componente] [nvarchar] (255) NOT NULL, \n" +
                                "[Cores] [nvarchar] (10) NOT NULL, \n" +
                                "[M125] [nvarchar] (255) NULL, \n" +
                                "[M150][nvarchar] (255) NULL, \n" +
                                "[M187][nvarchar] (255) NULL, \n" +
                                "[M225][nvarchar] (255) NULL, \n" +
                                "[M250][nvarchar] (255) NULL, \n" +
                                "[M300][nvarchar] (255) NULL, \n" +
                                "[M375][nvarchar] (255) NULL, \n" +
                                "[E45][nvarchar] (255) NULL, \n" +
                                "[E90][nvarchar] (255) NULL, \n" +
                                "[I45][nvarchar] (255) NULL, \n" +
                                "[I90][nvarchar] (255) NULL, \n" +
                                "[T1875][nvarchar] (255) NULL, \n" +
                                "[T2167][nvarchar] (255) NULL, \n" +
                                "[T2500][nvarchar] (255) NULL, \n" +
                                "[T360][nvarchar] (255) NULL, \n" +
                                "[Observacao][nvarchar] (255) NULL, \n" +
                                "[LiberarRepresentante][bit] NULL, \n" +
                                "[Acessarios_CAB][bit] NULL,[BaseCodigo] [nvarchar](100) NULL) \n" +
                                "CREATE TABLE TEMP_ResultadoTemp ( \n" +
                                "ID INT, \n" +
                                "[Modelo] [nvarchar] (255) NOT NULL, \n" +
                                "[Componente] [nvarchar] (255) NOT NULL, \n" +
                                "[Cores] [nvarchar] (10) NOT NULL, \n" +
                                "[M125] [nvarchar] (255) NULL, \n" +
                                "[M150][nvarchar] (255) NULL, \n" +
                                "[M187][nvarchar] (255) NULL, \n" +
                                "[M225][nvarchar] (255) NULL, \n" +
                                "[M250][nvarchar] (255) NULL, \n" +
                                "[M300][nvarchar] (255) NULL, \n" +
                                "[M375][nvarchar] (255) NULL, \n" +
                                "[E45][nvarchar] (255) NULL, \n" +
                                "[E90][nvarchar] (255) NULL, \n" +
                                "[I45][nvarchar] (255) NULL, \n" +
                                "[I90][nvarchar] (255) NULL, \n" +
                                "[T1875][nvarchar] (255) NULL, \n" +
                                "[T2167][nvarchar] (255) NULL, \n" +
                                "[T2500][nvarchar] (255) NULL, \n" +
                                "[T360][nvarchar] (255) NULL, \n" +
                                "[Observacao][nvarchar] (255) NULL, \n" +
                                //"[Alteracao][nvarchar] (255) NULL, \n" +
                                "[LiberarRepresentante][bit] NULL, \n" +
                                "[Acessarios_CAB][bit] NULL,[BaseCodigo] [nvarchar](100) NULL) \n" +
                                "--Crie uma tabela temporária para armazenar os IDs e campos alterados \n" +
                                "CREATE TABLE #IDsAlterados ( \n" +
                                "ID INT, \n" +
                                "CampoAlterado VARCHAR(100)) \n" +
                                "DECLARE @id INT, @campo_alterado VARCHAR(100) \n" +
                                "DECLARE campo_cursor CURSOR FOR \n" +
                                "SELECT t1.ID, \n" +
                                "CASE \n" +
                                "WHEN t1.Modelo<> t2.Modelo THEN 'Modelo' \n" +
                                "WHEN t1.Componente<> t2.Componente THEN 'Componente' \n" +
                                "WHEN t1.Cores<> t2.Cores THEN 'Cores' \n" +
                                "WHEN t1.M125<> t2.M125 THEN 'M125' \n" +
                                "WHEN t1.M150<> t2.M150 THEN 'M150' \n" +
                                "WHEN t1.M187<> t2.M187 THEN 'M187' \n" +
                                "WHEN t1.M225<> t2.M225 THEN 'M225' \n" +
                                "WHEN t1.M250<> t2.M250 THEN 'M250' \n" +
                                "WHEN t1.M300<> t2.M300 THEN 'M300' \n" +
                                "WHEN t1.M375<> t2.M375 THEN 'M375' \n" +
                                "WHEN t1.E45<> t2.E45 THEN 'E45' \n" +
                                "WHEN t1.E90<> t2.E90 THEN 'E90' \n" +
                                "WHEN t1.I45<> t2.I45 THEN 'I45' \n" +
                                "WHEN t1.I90<> t2.I90 THEN 'I90' \n" +
                                "WHEN t1.T1875<> t2.T1875 THEN 'T1875' \n" +
                                "WHEN t1.T2167<> t2.T2167 THEN 'T2167' \n" +
                                "WHEN t1.T2500<> t2.T2500 THEN 'T2500' \n" +
                                "WHEN t1.T360<> t2.T360 THEN 'T360' \n" +
                                "WHEN t1.Observacao<> t2.Observacao THEN 'Observacao' \n" +
                                "WHEN t1.LiberarRepresentante<> t2.LiberarRepresentante THEN 'LiberarRepresentante' \n" +
                                "WHEN t1.Acessarios_CAB<> t2.Acessarios_CAB THEN 'Acessarios_CAB' \n" +
                                "WHEN t1.BaseCodigo<> t2.BaseCodigo THEN 'BaseCodigo' \n" +
                                "ELSE 'Nenhum campo diferente' \n" +
                                "END AS campo_alterado \n" +
                                "FROM Planilhas_Eng t1 \n" +
                                "INNER JOIN TempPlanilhas_Eng t2 ON t1.ID = t2.ID \n" +
                                "WHERE t1.Modelo<> t2.Modelo \n" +
                                "OR t1.Componente<> t2.Componente \n" +
                                "OR t1.Cores<> t2.Cores \n" +
                                "OR t1.M125<> t2.M125 \n" +
                                "OR t1.M150<> t2.M150 \n" +
                                "OR t1.M187<> t2.M187 \n" +
                                "OR t1.M225<> t2.M225 \n" +
                                "OR t1.M250<> t2.M250 \n" +
                                "OR t1.M300<> t2.M300 \n" +
                                "OR t1.M375<> t2.M375 \n" +
                                "OR t1.E45<> t2.E45 \n" +
                                "OR t1.E90<> t2.E90 \n" +
                                "OR t1.I45<> t2.I45 \n" +
                                "OR t1.I90<> t2.I90 \n" +
                                "OR t1.T1875<> t2.T1875 \n" +
                                "OR t1.T2167<> t2.T2167 \n" +
                                "OR t1.T2500<> t2.T2500 \n" +
                                "OR t1.T360<> t2.T360 \n" +
                                "OR t1.Observacao<> t2.Observacao \n" +
                                "OR t1.LiberarRepresentante<> t2.LiberarRepresentante \n" +
                                "OR t1.Acessarios_CAB<> t2.Acessarios_CAB \n" +
                                "OR t1.BaseCodigo<> t2.BaseCodigo \n" +
                                "OPEN campo_cursor \n" +
                                "FETCH NEXT FROM campo_cursor INTO @id, @campo_alterado \n" +
                                "WHILE @@FETCH_STATUS = 0 \n" +
                                "BEGIN \n" +
                                "-- Armazene o ID e campo alterado na tabela temporária \n" +
                                "INSERT INTO #IDsAlterados (ID, CampoAlterado) VALUES (@id, @campo_alterado) \n" +
                                "-- Aqui você pode adicionar a lógica para lidar com cada campo alterado \n" +
                                "FETCH NEXT FROM campo_cursor INTO @id, @campo_alterado \n" +
                                "END \n" +
                                "CLOSE campo_cursor \n" +
                                "DEALLOCATE campo_cursor; \n" +
                                "--Consulta na tabela Planilhas_Eng com base nos dados da tabela temporária \n" +
                                "DECLARE @sqlQuery NVARCHAR(MAX) = '' \n" +
                                "-- Iterar sobre os registros da tabela temporária \n" +
                                "DECLARE campo_alterado_cursor CURSOR FOR \n" +
                                "SELECT ID, CampoAlterado \n" +
                                "FROM #IDsAlterados \n" +
                                "OPEN campo_alterado_cursor \n" +
                                "FETCH NEXT FROM campo_alterado_cursor INTO @id, @campo_alterado \n" +
                                "WHILE @@FETCH_STATUS = 0 \n" +
                                "BEGIN \n" +
                                "-- Construir a consulta dinâmica \n" +
                                "SET @sqlQuery = @sqlQuery + 'INSERT INTO TEMP_Resultado SELECT * FROM Planilhas_Eng WHERE ID = ' + CAST(@id AS VARCHAR(10)) + ';' \n" +
                                "SET @sqlQuery = @sqlQuery + 'INSERT INTO TEMP_ResultadoTemp SELECT * FROM TempPlanilhas_Eng WHERE ID = ' + CAST(@id AS VARCHAR(10)) + ';' \n" +
                                "FETCH NEXT FROM campo_alterado_cursor INTO @id, @campo_alterado \n" +
                                "END \n" +
                                "CLOSE campo_alterado_cursor \n" +
                                "DEALLOCATE campo_alterado_cursor; \n" +
                                "--Executar a consulta dinâmica \n" +
                                "EXEC sp_executesql @sqlQuery \n" +
                                "--Exibir o resultado armazenado na tabela temporária \n" +
                                "--select* from TEMP_ResultadoTemp \n" +
                                "--SELECT* FROM TEMP_Resultado";
            con.Open();
            SqlCommand cmd = new SqlCommand(query1, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            con.Close();


        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string newValue = cell.Value?.ToString() ?? string.Empty;
            int rowIndex = cell.RowIndex;
            int columnIndex = cell.ColumnIndex;
            DataGridViewCell column0Cell = dataGridView1.Rows[rowIndex].Cells[0];
            string column0Value = column0Cell.Value?.ToString() ?? string.Empty;
            if (column0Value == newValue)
            {
                if (dataTable.Rows.Count <= rowIndex)
                {
                    int rowsToAdd = rowIndex - dataTable.Rows.Count + 1;
                    for (int i = 0; i < rowsToAdd; i++)
                    {
                        dataTable.Rows.Add(dataTable.NewRow());
                    }
                }
                if (dataTable.Columns.Count <= columnIndex)
                {
                    int columnsToAdd = columnIndex - dataTable.Columns.Count + 1;
                    for (int i = 0; i < columnsToAdd; i++)
                    {
                        string columnName = "Column" + (dataTable.Columns.Count + i);
                        dataTable.Columns.Add(columnName);
                    }
                }
                dataTable.Rows[rowIndex][columnIndex] = newValue;
            }
        }

        private void button_salvar_Click(object sender, EventArgs e)
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
            Cursor.Current = Cursors.WaitCursor;
            con.Open();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("Deseja realmente alterar as " + dataGridView1.SelectedRows.Count + " linhas selecionadas?", "Salvar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            int id = Convert.ToInt32(row.Cells["ID"].Value);
                            string query_log_de = "SELECT * FROM Planilhas_Eng WHERE BaseCodigo = 'Eletrofrio' and ID = '" + id + "'";
                            SqlCommand cmd_log_de = new SqlCommand(query_log_de, con);
                            SqlDataReader reader_log_de = cmd_log_de.ExecuteReader();
                            string resultadoColuna1 = null;
                            if (reader_log_de.Read())
                            {
                                StringBuilder linhaBuilder = new StringBuilder();
                                for (int i = 0; i < reader_log_de.FieldCount; i++)
                                {
                                    object valorColuna = reader_log_de.GetValue(i);
                                    string valorColunaStr = (valorColuna != null) ? valorColuna.ToString() : string.Empty;
                                    linhaBuilder.Append(valorColunaStr);
                                    if (i < reader_log_de.FieldCount - 1)
                                    {
                                        linhaBuilder.Append(",");
                                    }
                                }

                                resultadoColuna1 = linhaBuilder.ToString();
                            }
                            reader_log_de.Close();
                            string modelo = row.Cells["Modelo"].Value.ToString();
                            string descricao = row.Cells["Componente"].Value.ToString();
                            string Cores = row.Cells["Cores"].Value.ToString();
                            string M125 = row.Cells["M125"].Value.ToString();
                            string M150 = row.Cells["M150"].Value.ToString();
                            string M187 = row.Cells["M187"].Value.ToString();
                            string M225 = row.Cells["M225"].Value.ToString();
                            string M250 = row.Cells["M250"].Value.ToString();
                            string M300 = row.Cells["M300"].Value.ToString();
                            string M375 = row.Cells["M375"].Value.ToString();
                            string E45 = row.Cells["E45"].Value.ToString();
                            string E90 = row.Cells["E90"].Value.ToString();
                            string I45 = row.Cells["I45"].Value.ToString();
                            string I90 = row.Cells["I90"].Value.ToString();
                            string T1875 = row.Cells["T1875"].Value.ToString();
                            string T2167 = row.Cells["T2167"].Value.ToString();
                            string T2500 = row.Cells["T2500"].Value.ToString();
                            string T360 = row.Cells["T360"].Value.ToString();
                            string Observacao = row.Cells["Observacao"].Value.ToString();
                            string LiberarRepresentante = row.Cells["LiberarRepresentante"].Value.ToString();
                            string Acessarios_CAB = row.Cells["Acessarios_CAB"].Value.ToString();
                            string BaseCodigo = row.Cells["BaseCodigo"].Value.ToString();
                            string query_update = "UPDATE [BANCODEDADOS].[dbo].[Planilhas_Eng] SET Modelo = '" + modelo + "', Componente = '" + descricao + "', Cores = '" + Cores + "' \n" +
                            ",M125 = '" + M125 + "' \n" +
                            ",M150 = '" + M150 + "' \n" +
                            ",M187 = '" + M187 + "' \n" +
                            ",M225 = '" + M225 + "' \n" +
                            ",M250 = '" + M250 + "' \n" +
                            ",M300 = '" + M300 + "' \n" +
                            ",M375 = '" + M375 + "' \n" +
                            ",E45 = '" + E45 + "' \n" +
                            ",E90 = '" + E90 + "' \n" +
                            ",I45 = '" + I45 + "' \n" +
                            ",I90 = '" + I90 + "' \n" +
                            ",T1875 = '" + T1875 + "' \n" +
                            ",T2167 = '" + T2167 + "' \n" +
                            ",T2500 = '" + T2500 + "' \n" +
                            ",T360 = '" + T360 + "' \n" +
                            ",Observacao = '" + Observacao + "' \n" +
                            ",LiberarRepresentante = '" + LiberarRepresentante + "' \n" +
                            ",Acessarios_CAB = '" + Acessarios_CAB + "' \n" +
                            ",BaseCodigo = '" + BaseCodigo + "' \n" +
                            "WHERE ID = " + id + "";
                            SqlTransaction transaction = con.BeginTransaction();
                            SqlCommand updateCmd = new SqlCommand(query_update, con, transaction);
                            updateCmd.ExecuteNonQuery();
                            transaction.Commit();
                            string query_log_de1 = "SELECT * FROM Planilhas_Eng WHERE basecodigo = 'Eletrofrio' and ID = '" + id + "'";
                            SqlCommand cmd_log_de1 = new SqlCommand(query_log_de1, con);
                            SqlDataReader reader_log_de1 = cmd_log_de1.ExecuteReader();
                            string resultadoColuna2 = null;
                            if (reader_log_de1.Read())
                            {
                                StringBuilder linhaBuilder2 = new StringBuilder();
                                for (int i = 0; i < reader_log_de1.FieldCount; i++)
                                {
                                    object valorColuna1 = reader_log_de1.GetValue(i);
                                    string valorColunaStr1 = (valorColuna1 != null) ? valorColuna1.ToString() : string.Empty;
                                    linhaBuilder2.Append(valorColunaStr1);
                                    if (i < reader_log_de1.FieldCount - 1)
                                    {
                                        linhaBuilder2.Append(",");
                                    }
                                }
                                resultadoColuna2 = linhaBuilder2.ToString();
                            }
                            reader_log_de1.Close();
                            NotificacaoAlteracao(modelo, resultadoColuna1, resultadoColuna2);
                            string query_log = "insert into Log_eng values ('" + Login + "','Planilha de engenharia','" + modelo + "','" + resultadoColuna1 + "','" + resultadoColuna2 + "','Alteração de dados','" + dataAtual + "',NULL,NULL);";
                            SqlTransaction transaction1 = con.BeginTransaction();
                            SqlCommand insertCmd = new SqlCommand(query_log, con, transaction1);
                            insertCmd.ExecuteNonQuery();
                            transaction1.Commit();
                        }
                    }
                    con.Close();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("As alterações foram salvas no banco de dados.", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (resultado == DialogResult.No)
                {

                }
            }
            else
            {
                MessageBox.Show("Favor selecionar uma linha para alterar!");
            }
        }

        private void NotificacaoAlteracao(string modelo, string resultado1, string resultado2)
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query_status = $"insert into Notificacao values ('{Login}','{modelo}','{resultado1}','{resultado2}','Alteracao acessorios','{dataAtual}',NULL,NULL)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand(query_status, connection);
                command1.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void button_deletar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("Deseja realmente deletar as linhas selecionadas?", "Deletar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
                    con.Open();
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int id = Convert.ToInt32(row.Cells["id"].Value);
                        string query_log_de = "SELECT * FROM Planilhas_Eng WHERE basecodigo = 'Eletrofrio' and ID = '" + id + "'";
                        SqlCommand cmd_log_de = new SqlCommand(query_log_de, con);
                        SqlDataReader reader_log_de = cmd_log_de.ExecuteReader();
                        string resultadoColuna1 = null;
                        if (reader_log_de.Read())
                        {
                            StringBuilder linhaBuilder = new StringBuilder();

                            for (int i = 0; i < reader_log_de.FieldCount; i++)
                            {
                                object valorColuna = reader_log_de.GetValue(i);
                                string valorColunaStr = (valorColuna != null) ? valorColuna.ToString() : string.Empty;
                                linhaBuilder.Append(valorColunaStr);
                                if (i < reader_log_de.FieldCount - 1)
                                {
                                    linhaBuilder.Append(",");
                                }
                            }
                            resultadoColuna1 = linhaBuilder.ToString();
                        }
                        reader_log_de.Close();
                        string modelo = row.Cells["Modelo"].Value.ToString();
                        string query_log = "insert into Log_eng values ('" + Login + "','Planilha de engenharia','" + modelo + "','" + resultadoColuna1 + "','','Dados deletado','" + dataAtual + "',NULL,NULL);";
                        SqlTransaction transaction1 = con.BeginTransaction();
                        SqlCommand insertCmd = new SqlCommand(query_log, con, transaction1);
                        insertCmd.ExecuteNonQuery();
                        transaction1.Commit();
                        NotificacaoDeletarAcessorios(modelo, id);
                        DeletarRegistroDoBancoDeDados(id);
                    }
                    con.Close();
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows.RemoveAt(row.Index);
                    }
                    MessageBox.Show("Arquivos deletados!");
                }
            }
            else
            {
                MessageBox.Show("Selecionar uma linha para deletar.");
            }
        }

        private void NotificacaoDeletarAcessorios(string modelo, int id)
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            string query = "insert into notificacao  SELECT \n" +
                            $"'{Login}','{modelo}', \n" +
                             "CONCAT( \n" +
                                    $"'{modelo}' + ',', \n" +
                                   "CASE WHEN Planilhas_Eng.Componente IS NOT NULL THEN Planilhas_Eng.Componente ELSE ',_' END, \n" +
                                            "CASE WHEN Planilhas_Eng.Cores IS NOT NULL THEN ',' + Planilhas_Eng.Cores ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.M125 IS NOT NULL THEN ',' + Planilhas_Eng.M125 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.M150 IS NOT NULL THEN ',' + Planilhas_Eng.M150 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.M187 IS NOT NULL THEN ',' + Planilhas_Eng.M187 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.M225 IS NOT NULL THEN ',' + Planilhas_Eng.M225 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.M250 IS NOT NULL THEN ',' + Planilhas_Eng.M250 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.M300 IS NOT NULL THEN ',' + Planilhas_Eng.M300 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.M375 IS NOT NULL THEN ',' + Planilhas_Eng.M375 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.E45 IS NOT NULL THEN ',' + Planilhas_Eng.E45 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.E90 IS NOT NULL THEN ',' + Planilhas_Eng.E90 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.I45 IS NOT NULL THEN ',' + Planilhas_Eng.I45 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.I90 IS NOT NULL THEN ',' + Planilhas_Eng.I90 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.T1875 IS NOT NULL THEN ',' + Planilhas_Eng.T1875 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.T2167 IS NOT NULL THEN ',' + Planilhas_Eng.T2167 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.T2500 IS NOT NULL THEN ',' + Planilhas_Eng.T2500 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.T360 IS NOT NULL THEN ',' + Planilhas_Eng.T360 ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.Observacao IS NOT NULL THEN ',' + Planilhas_Eng.Observacao ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.LiberarRepresentante IS NOT NULL THEN ',' + CONVERT(VARCHAR, Planilhas_Eng.LiberarRepresentante) ELSE ',' END, \n" +
                                            "CASE WHEN Planilhas_Eng.[Acessarios_CAB] IS NOT NULL THEN ',' + CONVERT(VARCHAR, Planilhas_Eng.[Acessarios_CAB]) ELSE ',' END, \n" +
                                             "CASE WHEN Planilhas_Eng.BaseCodigo IS NOT NULL THEN ',' + Planilhas_Eng.BaseCodigo ELSE ',' END \n" +
                                ") AS Alteracao_Para, \n" +
                                $"'','Deletando acessorios','{dataAtual}',NULL,NULL \n" +
                            $"FROM[BANCODEDADOS].[dbo].[Planilhas_Eng] where ID = '{id}'";
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query, con, transaction);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            con.Close();
        }


        private void DeletarRegistroDoBancoDeDados(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            string query = "DELETE FROM Planilhas_Eng WHERE id = '" + id + "'";
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query, con, transaction);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            con.Close();
        }

        private void button_Modulo_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Modulos);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_Modulos(object obj)
        {
            Application.Run(new Form_Modulos(valor_Gerenciador_SIP, Login));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Linha = comboBox1.Text;
            Lista_Expositor();
        }

        private void button_cabeceiras_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                this.Close();
                t1 = new Thread(Form_Cabeceiras);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            else
            {
                MessageBox.Show("Selecionar a linha de expositores desejada!");
            }
        }
        private void Form_Cabeceiras(object obj)
        {
            try
            {
                Application.Run(new Form_Cabeceira_Eng_New(valor_Gerenciador_SIP, Login, Linha));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Delete)
            {
                if (valor_Gerenciador_SIP.Contains("Engenharia de Produtos"))
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        DialogResult resultado = MessageBox.Show("Deseja realmente deletar a linha selecionada?", "Deletar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resultado == DialogResult.Yes)
                        {
                            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
                            con.Open();
                            string query_log_de = "SELECT * FROM Planilhas_Eng WHERE basecodigo = 'Eletrofrio' and ID = '" + id + "'";
                            SqlCommand cmd_log_de = new SqlCommand(query_log_de, con);
                            SqlDataReader reader_log_de = cmd_log_de.ExecuteReader();
                            string resultadoColuna1 = null;
                            if (reader_log_de.Read())
                            {
                                StringBuilder linhaBuilder = new StringBuilder();
                                for (int i = 0; i < reader_log_de.FieldCount; i++)
                                {
                                    object valorColuna = reader_log_de.GetValue(i);
                                    string valorColunaStr = (valorColuna != null) ? valorColuna.ToString() : string.Empty;
                                    linhaBuilder.Append(valorColunaStr);
                                    if (i < reader_log_de.FieldCount - 1)
                                    {
                                        linhaBuilder.Append(",");
                                    }
                                }
                                resultadoColuna1 = linhaBuilder.ToString();
                            }
                            reader_log_de.Close();
                            string modelo = dataGridView1.SelectedRows[0].Cells["Modelo"].Value.ToString();
                            string query_log = "insert into Log_eng values ('" + Login + "','Planilha de engenharia','" + modelo + "','" + resultadoColuna1 + "','','Dados deletado','" + dataAtual + "',NULL,NULL);";
                            SqlTransaction transaction1 = con.BeginTransaction();
                            SqlCommand insertCmd = new SqlCommand(query_log, con, transaction1);
                            insertCmd.ExecuteNonQuery();
                            transaction1.Commit();
                            con.Close();
                            DeletarRegistroDoBancoDeDados(id);
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                            MessageBox.Show("Arquivo deletado!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecionar uma linha para deletar.");
                    }
                }

            }
        }

        private void button_copiar_Click(object sender, EventArgs e)
        {
            int selectedCount = 0;
            int selectedIndex = -1;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    selectedCount++;
                    selectedIndex = i;
                }
            }
            if (selectedCount == 0)
            {
                MessageBox.Show("Selecionar um expositor para copiar!");
            }
            if (selectedCount == 1)
            {
                OrigemCopia = checkedListBox1.Items[selectedIndex].ToString();
                this.Close();
                t1 = new Thread(FormCopiarAcessorios);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            if (selectedCount > 1)
            {
                MessageBox.Show("Selecionar somente um expositor para copiar!");
            }
        }
        private void FormCopiarAcessorios(object obj)
        {
            Application.Run(new Form_Copiar_Acessorios(valor_Gerenciador_SIP, Login, OrigemCopia, comboBox1.SelectedItem.ToString()));
        }

        private void textBox_filter_KeyUp(object sender, KeyEventArgs e)
        {
            FiltrarModelos();
        }
        private void FiltrarModelos()
        {
            if (!string.IsNullOrEmpty(textBox_filter.Text))
            {
                // Filtro digitado pelo usuário
                string filtro = textBox_filter.Text.ToLower();

                // Filtra os modelos com base no texto digitado
                List<string> modelosFiltrados = modelosOriginais.Where(modelo => modelo.ToLower().Contains(filtro)).ToList();

                // Atualiza o conteúdo do checkedListBox1 com os modelos filtrados
                checkedListBox1.Items.Clear();
                checkedListBox1.Items.AddRange(modelosFiltrados.ToArray());
            }
            else
            {
                // Se o filtro estiver vazio, exibe todos os modelos originais.
                checkedListBox1.Items.Clear();
                checkedListBox1.Items.AddRange(modelosOriginais.ToArray());
            }
        }

        private void textBox_pesquisaComponente_TextChanged(object sender, EventArgs e)
        {
            // Atualizar o filtro sempre que o texto do TextBox mudar
            string filterText = textBox_pesquisaComponente.Text;
            filterText = filterText.Replace("'", "''"); // Tratar as aspas para evitar problemas no filtro
            dataTable.DefaultView.RowFilter = $"Componente LIKE '%{filterText}%'";

        }

        private void button_cadastarAcessorios_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                this.Close();
                t1 = new Thread(Form_Cadastrar_Acessorios);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            else
            {
                MessageBox.Show("Selecionar a linha de expositores desejada!");
            }
        }

        private void Form_Cadastrar_Acessorios(object obj)
        {

            Application.Run(new Form_cadastrarAcessorios(valor_Gerenciador_SIP, Login, Linha));

        }

        private void button_verificar_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Verificar_Log);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_Verificar_Log(object obj)
        {
            Application.Run(new Form_Verificar_Log(valor_Gerenciador_SIP, Login));
        }

        private void Form_Planilha_Eng_Load(object sender, EventArgs e)
        {
            if (valor_Gerenciador_SIP != null && valor_Gerenciador_SIP.Contains("Engenharia de Produtos"))
            {
                button_nova_linha.Enabled = true;

            }

            // Configurar a conexão com o banco de dados
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir a conexão
                connection.Open();

                // Executar a consulta SQL
                string sqlQuery = "SELECT * FROM Notificacao WHERE Data IS NULL OR Verificado_por IS NULL";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Verificar se há mais de uma linha no resultado
                        if (reader.HasRows)
                        {
                            // Tornar a label4 visível
                            label4.Visible = true;
                            button_verificar.Enabled = true;
                        }
                        else
                        {
                            // Ocultar a label4
                            label4.Visible = false;
                            button_verificar.Enabled = false;
                        }
                    }
                }
            }
        }

        private void button_nova_linha_Click(object sender, EventArgs e)
        {
            if (Linha == null || Linha == "")
            {
                MessageBox.Show("Favor selecionar a linha de expositores.");
                return;
            }
            else if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Por favor, selecione pelo menos um expositor.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (checkedListBox1.CheckedItems.Count > 1)
            {
                MessageBox.Show("Por favor, selecione apenas um expositor.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           
            this.Close();
            t1 = new Thread(Form_NovoAcessorios);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }

        private void Form_NovoAcessorios(object obj)
        {
            //HashSet<string> acessorios = new HashSet<string>();
            string Expositor = "";
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                // Aqui, você deve substituir "Modelo" pelo nome real da coluna 'Modelo' na sua DataTable
                Expositor = itemChecked.ToString();
            }
            Application.Run(new Form_Novo_Acessorio(valor_Gerenciador_SIP, Login, Linha, Expositor));
        }

  
    }
}