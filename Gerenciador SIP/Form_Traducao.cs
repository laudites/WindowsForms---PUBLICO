using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class Form_Traducao : Form
    {
        string Banco;
        string Instancia;
        string Senha;
        string Usuario;
        Thread t1;
        List<string> valor_Traducao;
        string Login;

        public Form_Traducao(List<string> valor, string banco, string instancia, string senha, string usuario, string login)
        {
            Login = login;
            Banco = banco;
            Instancia = instancia;
            Senha = senha;
            Usuario = usuario;
            InitializeComponent();
            listBox1.SelectedIndex = 0;
            button_cadastrar.Enabled = false;
            textBox2.Text = "%";
            valor_Traducao = valor;
        }

        private void button_Voltar_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Menu);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }

        private void Form_Menu(object obj)
        {
            Application.Run(new Form_Gerenciador_SIP(valor_Traducao, Login));
        }
        private void Todos()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string sclearsql = "create table #temp1 (ID int, Produto nvarchar(255),idioma nvarchar(255),descricao nvarchar(255))  \n" +
                              "insert into #temp1 select distinct idmprd.ididmprd,PRDCHB.Produto,idmprd.idioma,idmprd.descricao from PRDCHB \n" +
                              "left join idmprd on prdchb.produto = idmprd.produto where PRDCHB.Esconder_orcamento = '0' \n" +
                               "and PRDCHB.BaseCodigo = 'Eletrofrio' and PRDCHB.Aplicacao = 'Vittrine'; \n" +
                               "create table #temp2 (produto nvarchar(255),Chave_busca_montada nvarchar(255))  \n" +
                               "insert into #temp2 select Produto, Chave_busca_montada from PRDCHB  where Aplicacao = 'Vittrine' and Esconder_orcamento = 0 and BaseCodigo = 'Eletrofrio'; \n" +
                               "WITH CTE AS(SELECT Produto, Chave_busca_montada,RN = ROW_NUMBER()OVER(PARTITION BY Produto ORDER BY Produto) FROM #temp2) \n" +
                               "delete FROM CTE WHERE RN > 1; \n" +
                               "select #temp1.*,#temp2.Chave_busca_montada from #temp1 inner join #temp2 on #temp1.Produto = #temp2.produto where #temp1.produto like '" + textBox2.Text + "';";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(sclearsql, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void Comercial()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string sclearsql = "create table #temp1 (ID int, Produto nvarchar(255),idioma nvarchar(255),descricao nvarchar(255))  \n" +
                              "insert into #temp1 select distinct idmprd.ididmprd,PRDCHB.Produto,idmprd.idioma,idmprd.descricao from PRDCHB \n" +
                               "left join idmprd on prdchb.produto = idmprd.produto where PRDCHB.Esconder_orcamento = '0' \n" +
                                "and PRDCHB.BaseCodigo = 'Eletrofrio' and PRDCHB.Aplicacao = 'Vittrine'; \n" +
                                "create table #temp2 (produto nvarchar(255),Chave_busca_montada nvarchar(255))  \n" +
                                "insert into #temp2 select Produto, Chave_busca_montada from PRDCHB  where Aplicacao = 'Vittrine' and Esconder_orcamento = 0 and BaseCodigo = 'Eletrofrio'; \n" +
                                "WITH CTE AS(SELECT Produto, Chave_busca_montada,RN = ROW_NUMBER()OVER(PARTITION BY Produto ORDER BY Produto) FROM #temp2) \n" +
                                "delete FROM CTE WHERE RN > 1; \n" +
                                "select #temp1.*,#temp2.Chave_busca_montada from #temp1 inner join #temp2 on #temp1.Produto = #temp2.produto \n" +
                                "where (idioma = 'Comercial' or idioma is null) and #temp1.produto like '" + textBox2.Text + "'";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(sclearsql, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void Conjunto_Ind()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string sclearsql = "create table #temp1 (ID int, Produto nvarchar(255),idioma nvarchar(255),descricao nvarchar(255))  \n" +
                              "insert into #temp1 select distinct idmprd.ididmprd,PRDCHB.Produto,idmprd.idioma,idmprd.descricao from PRDCHB \n" +
                               "left join idmprd on prdchb.produto = idmprd.produto where PRDCHB.Esconder_orcamento = '0' \n" +
                                "and PRDCHB.BaseCodigo = 'Eletrofrio' and PRDCHB.Aplicacao = 'Vittrine'; \n" +
                                "create table #temp2 (produto nvarchar(255),Chave_busca_montada nvarchar(255))  \n" +
                                "insert into #temp2 select Produto, Chave_busca_montada from PRDCHB  where Aplicacao = 'Vittrine' and Esconder_orcamento = 0 and BaseCodigo = 'Eletrofrio'; \n" +
                                "WITH CTE AS(SELECT Produto, Chave_busca_montada,RN = ROW_NUMBER()OVER(PARTITION BY Produto ORDER BY Produto) FROM #temp2) \n" +
                                "delete FROM CTE WHERE RN > 1; \n" +
                                "select #temp1.*,#temp2.Chave_busca_montada from #temp1 inner join #temp2 on #temp1.Produto = #temp2.produto \n" +
                                "where (idioma like '%conju%'  or idioma is null) and #temp1.produto like '" + textBox2.Text + "'";

            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(sclearsql, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void Espanhol()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string sclearsql = "create table #temp1 (ID int, Produto nvarchar(255),idioma nvarchar(255),descricao nvarchar(255))  \n" +
                              "insert into #temp1 select distinct idmprd.ididmprd,PRDCHB.Produto,idmprd.idioma,idmprd.descricao from PRDCHB \n" +
                               "left join idmprd on prdchb.produto = idmprd.produto where PRDCHB.Esconder_orcamento = '0' \n" +
                                "and PRDCHB.BaseCodigo = 'Eletrofrio' and PRDCHB.Aplicacao = 'Vittrine'; \n" +
                                "create table #temp2 (produto nvarchar(255),Chave_busca_montada nvarchar(255))  \n" +
                                "insert into #temp2 select Produto, Chave_busca_montada from PRDCHB  where Aplicacao = 'Vittrine' and Esconder_orcamento = 0 and BaseCodigo = 'Eletrofrio'; \n" +
                                "WITH CTE AS(SELECT Produto, Chave_busca_montada,RN = ROW_NUMBER()OVER(PARTITION BY Produto ORDER BY Produto) FROM #temp2) \n" +
                                "delete FROM CTE WHERE RN > 1; \n" +
                                "select #temp1.*,#temp2.Chave_busca_montada from #temp1 inner join #temp2 on #temp1.Produto = #temp2.produto \n" +
                                "where (idioma = 'Espanhol'  or idioma is null) and #temp1.produto like '" + textBox2.Text + "'";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(sclearsql, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void FINAME()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string sclearsql = "create table #temp1 (ID int, Produto nvarchar(255),idioma nvarchar(255),descricao nvarchar(255))  \n" +
                              "insert into #temp1 select distinct idmprd.ididmprd,PRDCHB.Produto,idmprd.idioma,idmprd.descricao from PRDCHB \n" +
                               "left join idmprd on prdchb.produto = idmprd.produto where PRDCHB.Esconder_orcamento = '0' \n" +
                                "and PRDCHB.BaseCodigo = 'Eletrofrio' and PRDCHB.Aplicacao = 'Vittrine'; \n" +
                                "create table #temp2 (produto nvarchar(255),Chave_busca_montada nvarchar(255))  \n" +
                                "insert into #temp2 select Produto, Chave_busca_montada from PRDCHB  where Aplicacao = 'Vittrine' and Esconder_orcamento = 0 and BaseCodigo = 'Eletrofrio'; \n" +
                                "WITH CTE AS(SELECT Produto, Chave_busca_montada,RN = ROW_NUMBER()OVER(PARTITION BY Produto ORDER BY Produto) FROM #temp2) \n" +
                                "delete FROM CTE WHERE RN > 1; \n" +
                                "select #temp1.*,#temp2.Chave_busca_montada from #temp1 inner join #temp2 on #temp1.Produto = #temp2.produto \n" +
                                "where (idioma = 'FINAME'  or idioma is null) and #temp1.produto like '" + textBox2.Text + "'";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(sclearsql, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void Conferencia()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string sclearsql = "create table #temp1 (ID int, Produto nvarchar(255),idioma nvarchar(255),descricao nvarchar(255))  \n" +
                              "insert into #temp1 select distinct idmprd.ididmprd,PRDCHB.Produto,idmprd.idioma,idmprd.descricao from PRDCHB \n" +
                              "left join idmprd on prdchb.produto = idmprd.produto where PRDCHB.Esconder_orcamento = '0' \n" +
                               "and PRDCHB.BaseCodigo = 'Eletrofrio' and PRDCHB.Aplicacao = 'Vittrine'; \n" +
                               "create table #temp2 (produto nvarchar(255),Chave_busca_montada nvarchar(255))  \n" +
                               "insert into #temp2 select Produto, Chave_busca_montada from PRDCHB  where Aplicacao = 'Vittrine' and Esconder_orcamento = 0 and BaseCodigo = 'Eletrofrio'; \n" +
                               "WITH CTE AS(SELECT Produto, Chave_busca_montada,RN = ROW_NUMBER()OVER(PARTITION BY Produto ORDER BY Produto) FROM #temp2) \n" +
                               "delete FROM CTE WHERE RN > 1; \n" +
                               "select #temp1.*,#temp2.Chave_busca_montada from #temp1 inner join #temp2 on #temp1.Produto = #temp2.produto \n" +
                               "where (idioma = 'Conferencia'  or idioma is null) and #temp1.produto like '" + textBox2.Text + "'";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(sclearsql, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void button_Lista_completa_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            button_cadastrar.Enabled = true;
            if (listBox1.SelectedItem.Equals("Todos"))
            {
                Todos();
            }
            else if (listBox1.SelectedItem.Equals("Comercial"))
            {
                Comercial();
            }
            else if (listBox1.SelectedItem.Equals("Conjunto_Ind"))
            {
                Conjunto_Ind();
            }
            else if (listBox1.SelectedItem.Equals("Espanhol"))
            {
                Espanhol();
            }
            else if (listBox1.SelectedItem.Equals("FINAME"))
            {
                FINAME();
            }
            else if (listBox1.SelectedItem.Equals("Conferencia"))
            {
                Conferencia();
            }
            Cursor.Current = Cursors.Default;
        }
        private void button_cadastrar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["idioma"].Value.ToString() == "")
                {
                    MessageBox.Show("Campo Idioma esta vazio");
                    return;
                }
                else if (dataGridView1.Rows[i].Cells["descricao"].Value.ToString() == "")
                {
                    MessageBox.Show("Campo Descricao esta vazio");
                    return;
                }
            }
            DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja cadastrar esses dados ?", "Cadastrar", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string StrQuery;
                string ssqlconnectionstring = @"Data Source=INSTANCIA;Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=sa;Password=SENHA";
                using (SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring))
                {
                    using (SqlCommand comm = new SqlCommand())
                    {

                        comm.Connection = sqlconn;
                        sqlconn.Open();
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            if (dataGridView1.Columns.Contains("ID") == true)
                            {
                                if (dataGridView1.Rows[i].Cells["ID"].Value != null)
                                {
                                    StrQuery = @"Delete from idmprd where basecodigo = 'Eletrofrio' and idmprd.ididmprd = '"
                                    + dataGridView1.Rows[i].Cells["ididmprd"].Value + "';";
                                    comm.CommandText = StrQuery;
                                    comm.ExecuteNonQuery();
                                }
                            }

                        }
                        sqlconn.Close();
                    }
                }
                using (SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring))
                {
                    using (SqlCommand comm = new SqlCommand())
                    {
                        comm.Connection = sqlconn;
                        sqlconn.Open();
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            StrQuery = @"INSERT INTO idmprd VALUES ('"
                            + dataGridView1.Rows[i].Cells["idioma"].Value + "','"
                            + dataGridView1.Rows[i].Cells["produto"].Value + "','"
                            + dataGridView1.Rows[i].Cells["descricao"].Value + "','"
                            + "pc','Eletrofrio','');";
                            comm.CommandText = StrQuery;
                            comm.ExecuteNonQuery();
                        }
                        sqlconn.Close();
                    }
                }
            }

            Cursor.Current = Cursors.Default;
            MessageBox.Show("Dados cadastrado na tabela IDMPRD com sucesso!");
        }

        private void button_lista_sem_cadastro_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            button_cadastrar.Enabled = true;
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string sclearsql = "create table #temp1 (Produto nvarchar(255),idioma nvarchar(255),descricao nvarchar(255))  \n" +
                              "insert into #temp1 select distinct PRDCHB.Produto,idmprd.idioma,idmprd.descricao from PRDCHB \n" +
                              "left join idmprd on prdchb.produto = idmprd.produto where PRDCHB.Esconder_orcamento = '0' \n" +
                               "and PRDCHB.BaseCodigo = 'Eletrofrio' and PRDCHB.Aplicacao = 'Vittrine'; \n" +
                               "create table #temp2 (produto nvarchar(255),Chave_busca_montada nvarchar(255))  \n" +
                               "insert into #temp2 select Produto, Chave_busca_montada from PRDCHB  where Aplicacao = 'Vittrine' and Esconder_orcamento = 0 and BaseCodigo = 'Eletrofrio'; \n" +
                               "WITH CTE AS(SELECT Produto, Chave_busca_montada,RN = ROW_NUMBER()OVER(PARTITION BY Produto ORDER BY Produto) FROM #temp2) \n" +
                               "delete FROM CTE WHERE RN > 1; \n" +
                               "select #temp1.*,#temp2.Chave_busca_montada from #temp1 inner join #temp2 on #temp1.Produto = #temp2.produto \n" +
                               "where descricao is null and #temp1.produto like '" + textBox2.Text + "'";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(sclearsql, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Cursor.Current = Cursors.Default;

        }
    }
}
