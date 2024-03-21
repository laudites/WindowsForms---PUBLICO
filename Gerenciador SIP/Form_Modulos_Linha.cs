using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class Form_Modulos_Linha : Form
    {

        List<string> valor_Gerenciador_SIP;
        string Login;
        Thread t1;
        string Banco = "BANCODEDADOS";
        string Instancia = @"INSTANCIA";
        string Senha = "SENHA";
        string Usuario = "sa";
        object valorID;

        public Form_Modulos_Linha(List<string> valorGerenciador, string login)
        {
            Login = login;
            valor_Gerenciador_SIP = valorGerenciador;
            InitializeComponent();
        }

        private void Form_Modulos_Linha_Load(object sender, EventArgs e)
        {
            button_confirmar.Enabled = true;


            // String de conexão com o banco de dados
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

            // Consulta SQL
            string consultaSql = "SELECT [ID], [Linha], [BaseCodigo] FROM [BANCODEDADOS].[dbo].[Linha_Expositor]";

            // Cria uma conexão com o banco de dados
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Cria um comando SQL
                SqlCommand comando = new SqlCommand(consultaSql, conexao);

                // Cria um adaptador de dados
                SqlDataAdapter adaptador = new SqlDataAdapter(comando);

                // Cria um DataTable para armazenar os dados
                DataTable dataTable = new DataTable();

                try
                {
                    // Abre a conexão
                    conexao.Open();

                    // Preenche o DataTable com os dados da consulta
                    adaptador.Fill(dataTable);

                    // Define o DataTable como a fonte de dados do DataGridView
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    // Manipula exceções, se ocorrerem
                    MessageBox.Show("Erro ao carregar os dados: " + ex.Message);
                }
            }

        }

        private void button_Voltar_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Modulo);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }


        private void Form_Modulo(object obj)
        {
            Application.Run(new Form_Modulos(valor_Gerenciador_SIP, Login));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se a célula clicada está em uma linha válida e não no cabeçalho
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string valorLinha = dataGridView1.Rows[e.RowIndex].Cells["Linha"].Value.ToString();
                textBox_linha.Text = valorLinha;
                // Obtém o valor da célula "ID" da linha selecionada
                valorID = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value;

                // Verifica se o valor é nulo ou vazio
                if (valorID != null && !string.IsNullOrWhiteSpace(valorID.ToString()))
                {
                    // Se houver um valor, habilita o botão de alteração
                    button_Alterar.Enabled = true;
                    button_Deletar.Enabled = true;
                }
                else
                {
                    // Se o valor for nulo ou vazio, desabilita o botão de alteração
                    button_Alterar.Enabled = false;
                    button_Deletar.Enabled = false;
                }
            }
            else
            {
                // Se a célula clicada estiver no cabeçalho, desabilita o botão de alteração
                button_Alterar.Enabled = false;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Verifica se há alguma linha selecionada
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtém a primeira linha selecionada
                DataGridViewRow linhaSelecionada = dataGridView1.SelectedRows[0];

                // Obtém o valor da coluna "Linha" na linha selecionada
                string valorLinha = linhaSelecionada.Cells["Linha"].Value.ToString();

                // Define o valor obtido no TextBox
                textBox_linha.Text = valorLinha;
            }
            else
            {
                // Se nenhuma linha estiver selecionada, limpa o TextBox
                textBox_linha.Text = "";
            }
        }

        private void button_confirmar_Click(object sender, EventArgs e)
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // Deletar o registro do banco de dados
            using (SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha))
            {
                con.Open();
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    string query = "insert into Linha_Expositor values (@Linha,'Eletrofrio')";
                    using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Linha", textBox_linha.Text);

                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            SqlConnection con1 = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            con1.Open();
            string insert_log = $"insert into Log_eng values ('{Login}','Cadastro Linha','','','{textBox_linha.Text}','Criação de linha do expositor','{dataAtual}',NULL,NULL);";
            SqlTransaction transaction1 = con1.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand(insert_log, con1, transaction1);
            cmd1.ExecuteNonQuery();
            transaction1.Commit();
            con1.Close();
            Form_Modulos_Linha_Load(sender, e);
        }

        private void button_Alterar_Click(object sender, EventArgs e)
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection con1 = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            con1.Open();
            string insert_log = $"insert into Log_eng values ('{Login}','Cadastro Linha','',(  select linha from Linha_Expositor where id = '{valorID}'),'{textBox_linha.Text}','Alteração de linha do expositor','{dataAtual}',NULL,NULL);";
            SqlTransaction transaction1 = con1.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand(insert_log, con1, transaction1);
            cmd1.ExecuteNonQuery();
            transaction1.Commit();
            con1.Close();

            // Deletar o registro do banco de dados
            using (SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha))
            {
                con.Open();
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    string query = $"update Linha_Expositor set Linha = @Linha where ID = '{valorID}'";
                    using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Linha", textBox_linha.Text);

                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            Form_Modulos_Linha_Load(sender, e);
        }

        private void button_Deletar_Click(object sender, EventArgs e)
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection con1 = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
            con1.Open();
            string insert_log = $"insert into Log_eng values ('{Login}','Cadastro Linha','',(  select linha from Linha_Expositor where id = '{valorID}'),'','Deletando linha expositor','{dataAtual}',NULL,NULL);";
            SqlTransaction transaction1 = con1.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand(insert_log, con1, transaction1);
            cmd1.ExecuteNonQuery();
            transaction1.Commit();
            con1.Close();

            // Deletar o registro do banco de dados
            using (SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha))
            {
                con.Open();
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    string query = $"delete Linha_Expositor where ID = '{valorID}'";
                    using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            Form_Modulos_Linha_Load(sender, e);
        }
    }
}
