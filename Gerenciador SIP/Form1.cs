using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
using ClosedXML.Excel;
using System.Collections.Generic;

namespace Gerenciador_SIP
{
    public partial class Form_Tabela_SQL : Form
    {
        List<string> valor_Tabela_SQL;
        string Banco;
        string Instancia;
        string Senha;
        string Usuario;
        Thread t1;
        string Login;

        public Form_Tabela_SQL(List<string> valor, string banco, string instancia, string senha, string usuario, string login)
        {
            Login = login;
            this.Text = "Tabelas SQL " + banco;
            Banco = banco;
            Instancia = instancia;
            Senha = senha;
            Usuario = usuario;
            InitializeComponent();
            Lista_tabela();
            valor_Tabela_SQL = valor;

        }
        private void Lista_tabela()
        {

            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
            Cursor.Current = Cursors.WaitCursor;
            con.Open();
            string query = "SELECT TABLE_NAME FROM information_schema.tables order by TABLE_NAME";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        treeView1.Nodes.Add(reader.GetString(0));
                    }
                    con.Close();

                }
            }
            Cursor.Current = Cursors.Default;

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
            Application.Run(new Form_Gerenciador_SIP(valor_Tabela_SQL, Login));
        }


        private void treeView1_DoubleClick(object sender, TreeViewEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string selectedNodeText = e.Node.Text;
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
            var query1 = "select * from " + selectedNodeText;
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;

        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (e.Node.Text == Banco)
            {

            }
            else
            {
                string nodeText = treeView1.SelectedNode.Text;
                SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
                var query1 = "select * from " + nodeText;
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                SqlCommand cmd = new SqlCommand(query1, con, transaction);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                transaction.Commit();
                con.Close();

            }
            Cursor.Current = Cursors.Default;

        }

        private void button_Atualizar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string nodeText = treeView1.SelectedNode.Text;
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
            var query1 = "select * from " + nodeText;
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;


        }

        private void button_Exportar_EXCEL_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string nodeText = treeView1.SelectedNode.Text;


            DialogResult dialogResult = MessageBox.Show("Deseja exportar a tabela " + Banco + ".dbo." + nodeText, Banco, MessageBoxButtons.YesNo);
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
                            //= cell.Value.ToString();
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
                    wb.Worksheets.Add(dt, "Pagina1");
                    wb.SaveAs(folderPath + nodeText + ".xlsx");
                }
                Cursor.Current = Cursors.Default;

                MessageBox.Show("Tabela exportada com sucesso!");

            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }

        }
        private void button_Exportar_SQL_Click_teste(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string nodeText = treeView1.SelectedNode.Text;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text File|*.sql";
            var dateString2 = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
            dialog.FileName = nodeText + "-" + dateString2;
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            dataGridView1.ClearSelection();
            StreamWriter x;
            x = File.CreateText(dialog.FileName);
            x.WriteLine("DELETE FROM " + nodeText);
            for (int r = 0; r < dataGridView1.RowCount - 1; r++)
            {
                x.WriteLine("GO");

                dataGridView1[0, r].Selected = true;
                string folga_minima = dataGridView1.GetClipboardContent().GetText();
                dataGridView1.ClearSelection();

                dataGridView1[1, r].Selected = true;
                string folga_maxima = dataGridView1.GetClipboardContent().GetText();
                dataGridView1.ClearSelection();

                dataGridView1[2, r].Selected = true;
                string delta_minimo = dataGridView1.GetClipboardContent().GetText();
                dataGridView1.ClearSelection();

                dataGridView1[3, r].Selected = true;
                string folga_minima_cond = dataGridView1.GetClipboardContent().GetText();
                dataGridView1.ClearSelection();

                dataGridView1[4, r].Selected = true;
                string folga_maxima_cond = dataGridView1.GetClipboardContent().GetText();
                dataGridView1.ClearSelection();



                x.WriteLine("INSERT INTO " + nodeText + "VALUES ('" + "" + "');");

                dataGridView1.ClearSelection();
            }
            x.Close();
            var rowHeaders = dataGridView1.RowHeadersVisible;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ClearSelection();
            dataGridView1.RowHeadersVisible = rowHeaders;
            Cursor.Current = Cursors.Default;

            MessageBox.Show(@"Arquivo SQL criado com sucesso!");
        }

        private void button_Exportar_SQL_Click(object sender, EventArgs e)
        {
            int colCount = dataGridView1.ColumnCount;
            int rowcount = dataGridView1.RowCount;
            int[] colunas = new int[] { colCount };

            string nodeText = treeView1.SelectedNode.Text;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text File|*.sql";
            var dateString2 = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
            dialog.FileName = nodeText + "-" + dateString2;
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            dataGridView1.ClearSelection();
            StreamWriter x;
            x = File.CreateText(dialog.FileName);
            x.WriteLine("DELETE FROM " + nodeText);

            using (TextWriter tw = x)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    tw.Write("INSERT INTO " + nodeText + " VALUES (");
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        tw.Write($"{dataGridView1.Rows[i].Cells[j].Value.ToString()}" + ",");

                        if (j == dataGridView1.Columns.Count - 1)
                        {
                            tw.Write(");");
                        }
                    }
                    tw.WriteLine();
                }
            }

            x.Close();
            var rowHeaders = dataGridView1.RowHeadersVisible;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ClearSelection();
            dataGridView1.RowHeadersVisible = rowHeaders;

            MessageBox.Show(@"Arquivo SQL criado com sucesso!");
        }

        private void button_Estrutura_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
            var query1 = "select " +
                            "INFORMATION_SCHEMA.COLUMNS.TABLE_NAME," +
                            "INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME," +
                            "INFORMATION_SCHEMA.COLUMNS.IS_NULLABLE," +
                            "INFORMATION_SCHEMA.COLUMNS.DATA_TYPE," +
                            "INFORMATION_SCHEMA.COLUMNS.CHARACTER_MAXIMUM_LENGTH," +
                            "INFORMATION_SCHEMA.KEY_COLUMN_USAGE.COLUMN_NAME as Primary_key " +
                            "from INFORMATION_SCHEMA.COLUMNS " +
                            "left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE on " +
                            "INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = INFORMATION_SCHEMA.KEY_COLUMN_USAGE.TABLE_NAME " +
                            "where INFORMATION_SCHEMA.COLUMNS.TABLE_NAME like '" + Banco + "'";
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;


        }
    }
}
