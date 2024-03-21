using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class Form_Descritivo_tecnico : Form
    {
        string Banco;
        string Instancia;
        string Senha;
        string Usuario;
        Thread t1;
        List<string> valor_Descritivo_tecnico;
        string Login;

        public Form_Descritivo_tecnico(List<string> valor, string banco, string instancia, string senha, string usuario, string login)
        {
            Login = login; 
            Banco = banco;
            Instancia = instancia;
            Senha = senha;
            Usuario = usuario;
            InitializeComponent();
            valor_Descritivo_tecnico = valor;
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
            Application.Run(new Form_Gerenciador_SIP(valor_Descritivo_tecnico, Login));
        }

        private void button_Atualizar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //con.ConnectionString = @"Data Source=INSTANCIA;Initial Catalog=Eletrofast_2019;Persist Security Info=True;User ID=sa;Password=SENHA";
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
            var query1 = "select idmdtc.idioma,GAB_CRTGAB.codigo,idmdtc.idmdtc_agrupar_corte,idmdtc.dtc_linha,ididmdtc from idmdtc right join GAB_CRTGAB on GAB_CRTGAB.codigo = idmdtc.dtc_codigo where GAB_CRTGAB.codigo like '"
                + textBox2.Text + "'";
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;
        }

        private void button_inserir_dados_Click(object sender, EventArgs e)
        {
            string StrQuery;
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["ididmdtc"].Value.ToString() == "")
                {
                    // CADASTRAR
                    using (SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring))
                    {
                        using (SqlCommand comm = new SqlCommand())
                        {
                            comm.Connection = sqlconn;
                            sqlconn.Open();
                            StrQuery = @"INSERT INTO idmdtc VALUES ('"
                            + dataGridView1.Rows[i].Cells["idioma"].Value + "','"
                            + dataGridView1.Rows[i].Cells["codigo"].Value + "','','"
                            + dataGridView1.Rows[i].Cells["dtc_linha"].Value + "','',0,'','Eletrofrio','"
                            + dataGridView1.Rows[i].Cells["idmdtc_agrupar_corte"].Value + "');";
                            comm.CommandText = StrQuery;
                            comm.ExecuteNonQuery();

                        }
                    }
                }
                else if (dataGridView1.Rows[i].Cells["ididmdtc"].Value.ToString() != "")
                {
                    // AlTERAR                 
                    using (SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring))
                    {
                        using (SqlCommand comm = new SqlCommand())
                        {
                            comm.Connection = sqlconn;
                            sqlconn.Open();
                            StrQuery = @"delete from idmdtc where ididmdtc = '"
                            + dataGridView1.Rows[i].Cells["ididmdtc"].Value + "';";
                            comm.CommandText = StrQuery;
                            comm.ExecuteNonQuery();
                            StrQuery = @"INSERT INTO idmdtc VALUES ('"
                            + dataGridView1.Rows[i].Cells["idioma"].Value + "','"
                            + dataGridView1.Rows[i].Cells["codigo"].Value + "','','"
                            + dataGridView1.Rows[i].Cells["dtc_linha"].Value + "','',0,'','Eletrofrio','"
                            + dataGridView1.Rows[i].Cells["idmdtc_agrupar_corte"].Value + "');";
                            comm.CommandText = StrQuery;
                            comm.ExecuteNonQuery();
                            sqlconn.Close();
                        }
                    }
                }
            }
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
            var query1 = "select GAB_CRTGAB.codigo,idmdtc.idmdtc_agrupar_corte,idmdtc.dtc_linha,ididmdtc from idmdtc right join GAB_CRTGAB on GAB_CRTGAB.codigo = idmdtc.dtc_codigo where GAB_CRTGAB.codigo like '"
                + textBox2.Text + "'";
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Dados cadastrado na tabela IDMDTC com sucesso! \n Para criar o idmdtc_agrupar_corte utilizar o configurador do sistema SIP!");

        }
    }
}
