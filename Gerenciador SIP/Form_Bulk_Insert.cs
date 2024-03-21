using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using OfficeOpenXml;
using System.Collections.Generic;

namespace Gerenciador_SIP
{
    public partial class Form_Bulk_Insert : Form
    {
        Thread t1;
        List<string> valor_Bulk_Insert;
        string Banco;
        string Instancia;
        string Senha;
        string Usuario;
        string Login;

        public Form_Bulk_Insert(List<string> valor, string banco, string instancia, string senha, string usuario, string login)
        {
            Banco = banco;
            Instancia = instancia;
            Senha = senha;
            Usuario = usuario;
            InitializeComponent();
            valor_Bulk_Insert = valor;
            Login = login;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string pastaImportar = @"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\02-Importar";
            var arquivos = Directory.EnumerateFiles(pastaImportar, "*", SearchOption.AllDirectories).Select(Path.GetFileNameWithoutExtension);
            textBox1.Text = string.Empty;

            DialogResult dialogResult = MessageBox.Show("Deseja Importar as tabelas no banco " + Banco + "?", Banco, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (string arquivo in arquivos)
                {
                    var dateString2 = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
                    string excelFilePath = Path.Combine(pastaImportar, arquivo + ".xlsx");
                    string sqlConnectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha;
                    string clearSql = "DELETE FROM " + arquivo;

                    using (SqlConnection sqlConn = new SqlConnection(sqlConnectionString))
                    {
                        sqlConn.Open();

                        using (SqlCommand sqlCmd = new SqlCommand(clearSql, sqlConn))
                        {
                            sqlCmd.ExecuteNonQuery();
                        }
                    }

                    using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(excelFilePath)))
                    {
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.First();


                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;

                        DataTable dataTable = new DataTable();

                        for (int col = 1; col <= colCount; col++)
                        {
                            string columnName = Convert.ToString(worksheet.Cells[1, col].Value);
                            dataTable.Columns.Add(columnName);
                        }

                        for (int row = 2; row <= rowCount; row++)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            for (int col = 1; col <= colCount; col++)
                            {
                                dataRow[col - 1] = worksheet.Cells[row, col].Value;
                            }
                            dataTable.Rows.Add(dataRow);
                        }

                        using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                        {
                            connection.Open();

                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                            {
                                bulkCopy.DestinationTableName = arquivo;
                                bulkCopy.WriteToServer(dataTable);
                            }
                        }
                    }

                    string destinationFilePath = Path.Combine(@"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\03-Importados", arquivo + "-" + dateString2 + ".xlsx");
                    File.Move(excelFilePath, destinationFilePath);

                    textBox1.AppendText(arquivo + " Importado com sucesso!" + Environment.NewLine);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }

            Cursor.Current = Cursors.Default;
            MessageBox.Show(@"Banco de dados atualizado com sucesso!");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Menu);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_Menu(object obj)
        {
            Application.Run(new Form_Gerenciador_SIP(valor_Bulk_Insert, Login));
        }

        private void Form_Bulk_Insert_Load(object sender, EventArgs e)
        {
            string pasta_importar = @"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\02-Importar";
            var arquivos = Directory.EnumerateFiles(pasta_importar, "*", SearchOption.AllDirectories).Select(Path.GetFileNameWithoutExtension);
            foreach (string arquivo in arquivos)
            {
                textBox1.AppendText(arquivo + Environment.NewLine);
            }
        }
    }
}