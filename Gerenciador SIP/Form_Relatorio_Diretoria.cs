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
using Excel = Microsoft.Office.Interop.Excel;

namespace Gerenciador_SIP
{
    public partial class Form_Relatorio_Diretoria : Form
    {
        List<string> valor_Gerenciador_SIP;

        //LOCAL
        //string Banco = "Eletrofast_2019";
        //string Instancia = @"INSTANCIA";
        //string Senha = "SENHA";
        //string Usuario = "sa";

        //BANCODEDADOS
        string Banco = "BANCODEDADOS";
        string Instancia = @"SERVIDOR";
        string Senha = "SENHA";
        string Usuario = "sa";

        string Login = "";
        Thread t1;

        public Form_Relatorio_Diretoria(List<string> valorGerenciador, string login)
        {
            Login = login;
            valor_Gerenciador_SIP = valorGerenciador;
            InitializeComponent();
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

        private void Form_Relatorio_Diretoria_Load(object sender, EventArgs e)
        {
            // Obtém a data atual
            DateTime dataAtual = DateTime.Now;

            // Define o texto do TextBox para o mês e o ano
            comboBox_Mes.Text = dataAtual.Month.ToString();
            comboBox_Ano.Text = dataAtual.Year.ToString();
            comboBox_Gerente.Text = "Todos";
            comboBox_Status.Text = "Todos";
            comboBox_UF.Text = "Todos";
            comboBox_Vendedor.Text = "Todos";

            this.Cursor = Cursors.WaitCursor;
            Atualizar_Datagrid();
            this.Cursor = Cursors.Default;
        }

        private void Atualizar_Datagrid()
        {
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = @"SELECT 
                            [Numero Orcamento],
                            [Data Cadastro],
                            [Ultima Atualizacao],
                            [Razao Social],
                            [Nome Fantasia],
                            UF,
                            Vendedor,
                            Gerente,
                            SUM([Valor venda]) AS [Valor venda],
                            SUM([Valor lista]) AS [Valor lista],
                            CONCAT(
                                CASE
                                    WHEN SUM([Valor lista]) = 0 THEN NULL
                                    ELSE (-SUM([Valor venda] - [Valor lista]) / SUM([Valor lista])) * 100
                                END,
                                '%'
                            ) AS [Desconto/Over],
                            SUM(Servicos) AS Servicos,
                            Status
                        FROM
                            (
                                SELECT 
                                    CONCAT(orclst_numero, orclst_revisao) AS [Numero Orcamento],
                                    CONVERT(varchar, ORCCAB.orccab_Cadastro, 103) AS [Data Cadastro],
                                    CONVERT(varchar, ORCCABDATAULTATUALIZACAO, 103) AS [Ultima Atualizacao],
                                    ORCCAB.orccab_cliente_Nome AS [Razao Social],
                                    ORCCAB.orccab_cliente_codinome AS [Nome Fantasia],
                                    ORCCAB.orccab_UF AS UF,
                                    ORCCAB.orccab_orcamentista AS Vendedor,
                                    ORCCAB.orccab_vendedor AS Gerente,
                                    OrcGrpSit.OrcGrpSit_ValorVenda AS [Valor venda],
                                    OrcGrpSit.OrcGrpSit_ValorLista AS [Valor lista],
                                    OrcGrpSit.OrcGrpSit_ValorServicos as Servicos,
                                    Orcst.st_descricao AS Status
                                FROM 
                                    ORCCAB
                                    INNER JOIN OrcGrpSit ON orccab.numeroOrcamento = OrcGrpSit.numeroOrcamento and OrcGrpSit_Status = 0 and (idGrupo = 4 or idGrupo = 5 or idGrupo = 22 or idGrupo = 30)
                                    INNER JOIN Orclst ON orclst.orclst_numero + orclst.orclst_revisao = orccab.numeroOrcamento
                                    INNER JOIN Orcst ON ORCCAB.orccab_Status = Orcst.st_codigo								
                                WHERE 
			                          YEAR(ORCCAB.orccab_Cadastro) LIKE @Ano AND 
			                          MONTH(ORCCAB.orccab_Cadastro) LIKE @Mes AND 
			                          ORCCAB.orccab_vendedor LIKE @Gerente AND 
			                          ORCCAB.orccab_UF LIKE @UF AND 
			                          ORCCAB.orccab_orcamentista LIKE @Vendedor AND 
			                          ORCCAB.numeroOrcamento LIKE '%'+@NumeroOrcamento+'%' AND
			                          ORCCAB.orccab_Status like @Status
                            ) AS subquery
                        GROUP BY 
                            [Numero Orcamento],
                            [Data Cadastro],
                            [Ultima Atualizacao],
                            [Razao Social],
                            [Nome Fantasia],
                            UF,
                            Vendedor,
                            Gerente,
                            Status;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando parâmetros
                if (String.IsNullOrEmpty(comboBox_Status.Text) || comboBox_Status.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Status", "%");
                }
                else
                {
                    //command.Parameters.AddWithValue("@Status", comboBox_Status.Text);

                    // Crie uma conexão com o banco de dados
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        // Abra a conexão
                        connection1.Open();

                        // Crie o comando SQL
                        string sql = $"SELECT st_codigo FROM Orcst WHERE st_descricao = '{comboBox_Status.Text}'";
                        using (SqlCommand sqlCommand = new SqlCommand(sql, connection1))
                        {
                            // Execute o comando SQL
                            SqlDataReader reader = sqlCommand.ExecuteReader();

                            // Verifique se há linhas retornadas
                            if (reader.Read())
                            {
                                // Se houver linhas, obtenha o valor da coluna st_codigo
                                string resultado = reader["st_codigo"].ToString();

                                // Adicione o valor à variável RESULTADO
                                command.Parameters.AddWithValue("@Status", resultado);
                            }
                            else
                            {
                                // Se nenhuma linha for retornada, defina um valor padrão ou trate a condição adequadamente
                                // Neste exemplo, estou definindo um valor padrão como "N/A"
                                string resultado = "N/A";
                                command.Parameters.AddWithValue("@Status", resultado);
                            }
                        }
                    }
                }


                if (String.IsNullOrEmpty(textBox_orcamento.Text) || textBox_orcamento.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@NumeroOrcamento", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@NumeroOrcamento", textBox_orcamento.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Ano.Text) || comboBox_Ano.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Ano", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Ano", comboBox_Ano.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Mes.Text) || comboBox_Mes.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Mes", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Mes", comboBox_Mes.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Vendedor.Text) || comboBox_Vendedor.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Vendedor", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Vendedor", comboBox_Vendedor.Text);
                }

                if (String.IsNullOrEmpty(comboBox_UF.Text) || comboBox_UF.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@UF", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@UF", comboBox_UF.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Gerente.Text) || comboBox_Gerente.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Gerente", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Gerente", comboBox_Gerente.Text);
                }


                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);



                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = table;

                // Formatando as colunas para exibir moeda brasileira
                dataGridView1.Columns["Valor venda"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["Valor lista"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["Servicos"].DefaultCellStyle.Format = "C2";
                Atualizar_Panel_Inferior();
            }
        }

        private void comboBox_Gerente_DropDown(object sender, EventArgs e)
        {
            // Limpa os itens existentes na ComboBox
            comboBox_Gerente.Items.Clear();

            // Adiciona a opção "Todos" à ComboBox
            comboBox_Gerente.Items.Add("Todos");

            // Conecta ao banco de dados e executa a consulta SQL
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = @"SELECT distinct filho  FROM [pesrelacionamentos]
                      where tipo = 'orcamentista/vendedor' 
                      and pai like @vendedor";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                if (comboBox_Vendedor.Text != "Todos")
                {
                    command.Parameters.AddWithValue("@vendedor", comboBox_Vendedor.Text);
                }
                else
                {
                    command.Parameters.AddWithValue("@vendedor", "%");
                }

                connection.Open();

                // Executa o comando SQL e lê os resultados
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Adiciona cada item à ComboBox
                        comboBox_Gerente.Items.Add(reader["filho"].ToString());
                    }
                }
            }
        }

        private void comboBox_Gerente_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Atualizar_Datagrid();
            this.Cursor = Cursors.Default;
        }

        private void comboBox_Vendedor_DropDown(object sender, EventArgs e)
        {
            // Limpa os itens existentes na ComboBox
            comboBox_Vendedor.Items.Clear();

            // Adiciona a opção "Todos" à ComboBox
            comboBox_Vendedor.Items.Add("Todos");

            // Conecta ao banco de dados e executa a consulta SQL
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            /* string query = @"
                                 SELECT Pescab.PESCAB_CODINOME 
                             FROM pesorc
                             LEFT JOIN Pescab ON pesorc.pesorc_codigo = pescab.pescab_codigo
                             ORDER BY PESCAB_CODINOME
                             ";*/

            string query = @$"SELECT distinct [pai]
                          FROM [pesrelacionamentos]
                          where tipo = 'orcamentista/vendedor' 
                          and filho like @Gerente";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                if (comboBox_Gerente.Text != "Todos")
                {
                    command.Parameters.AddWithValue("@Gerente", comboBox_Gerente.Text);
                }
                else
                {
                    command.Parameters.AddWithValue("@Gerente", "%");
                }

                connection.Open();

                // Executa o comando SQL e lê os resultados
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Adiciona cada item à ComboBox
                        comboBox_Vendedor.Items.Add(reader["pai"].ToString());
                    }
                }
            }
        }

        private void comboBox_Vendedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Atualizar_Datagrid();
            this.Cursor = Cursors.Default;
        }

        private void comboBox_Status_DropDown(object sender, EventArgs e)
        {
            // Limpa os itens existentes na ComboBox
            comboBox_Status.Items.Clear();

            // Adiciona a opção "Todos" à ComboBox
            comboBox_Status.Items.Add("Todos");

            // Conecta ao banco de dados e executa a consulta SQL
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = @"SELECT distinct
	                            Orcst.st_descricao as Status
                            FROM 
                                ORCCAB
                            INNER JOIN 
                                Orcst ON ORCCAB.orccab_Status = Orcst.st_codigo
                            WHERE YEAR(ORCCAB.orccab_Cadastro) LIKE @Ano AND 
                                  MONTH(ORCCAB.orccab_Cadastro) LIKE @Mes AND 
                                  ORCCAB.orccab_vendedor LIKE @Gerente AND 
                                  ORCCAB.orccab_UF LIKE @UF AND 
                                  ORCCAB.orccab_orcamentista LIKE @Vendedor AND 
                                  numeroOrcamento LIKE @NumeroOrcamento";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                if (String.IsNullOrEmpty(textBox_orcamento.Text) || textBox_orcamento.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@NumeroOrcamento", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@NumeroOrcamento", textBox_orcamento.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Ano.Text) || comboBox_Ano.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Ano", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Ano", comboBox_Ano.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Mes.Text) || comboBox_Mes.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Mes", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Mes", comboBox_Mes.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Vendedor.Text) || comboBox_Vendedor.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Vendedor", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Vendedor", comboBox_Vendedor.Text);
                }

                if (String.IsNullOrEmpty(comboBox_UF.Text) || comboBox_UF.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@UF", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@UF", comboBox_UF.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Gerente.Text) || comboBox_Gerente.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Gerente", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Gerente", comboBox_Gerente.Text);
                }
                connection.Open();

                // Executa o comando SQL e lê os resultados
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Adiciona cada item à ComboBox
                        comboBox_Status.Items.Add(reader["Status"].ToString());
                    }
                }
            }
        }

        private void comboBox_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Atualizar_Datagrid();
            this.Cursor = Cursors.Default;
        }

        private void comboBox_UF_DropDown(object sender, EventArgs e)
        {
            // Limpa os itens existentes na ComboBox
            comboBox_UF.Items.Clear();

            // Adiciona a opção "Todos" à ComboBox
            comboBox_UF.Items.Add("Todos");

            // Conecta ao banco de dados e executa a consulta SQL
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = @"SELECT DISTINCT
	                            ORCCAB.orccab_UF as UF
                            FROM 
                                ORCCAB
                            INNER JOIN 
                                Orcst ON ORCCAB.orccab_Status = Orcst.st_codigo
                            WHERE YEAR(ORCCAB.orccab_Cadastro) LIKE @Ano AND 
                                  MONTH(ORCCAB.orccab_Cadastro) LIKE @Mes AND 
                                  ORCCAB.orccab_vendedor LIKE @Gerente AND 
                                  ORCCAB.orccab_orcamentista LIKE @Vendedor AND 
                                  numeroOrcamento LIKE @NumeroOrcamento AND
                                  ORCCAB.orccab_Status like @Status";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                // Adicionando parâmetros
                if (String.IsNullOrEmpty(comboBox_Status.Text) || comboBox_Status.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Status", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Status", comboBox_Status.Text);
                }


                if (String.IsNullOrEmpty(textBox_orcamento.Text) || textBox_orcamento.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@NumeroOrcamento", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@NumeroOrcamento", textBox_orcamento.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Ano.Text) || comboBox_Ano.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Ano", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Ano", comboBox_Ano.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Mes.Text) || comboBox_Mes.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Mes", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Mes", comboBox_Mes.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Vendedor.Text) || comboBox_Vendedor.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Vendedor", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Vendedor", comboBox_Vendedor.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Gerente.Text) || comboBox_Gerente.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Gerente", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Gerente", comboBox_Gerente.Text);
                }
                connection.Open();

                // Executa o comando SQL e lê os resultados
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Adiciona cada item à ComboBox
                        comboBox_UF.Items.Add(reader["UF"].ToString());
                    }
                }
            }
        }

        private void comboBox_UF_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Atualizar_Datagrid();
            this.Cursor = Cursors.Default;
        }

        private void comboBox_Ano_DropDown(object sender, EventArgs e)
        {

            // Limpa os itens existentes na ComboBox
            comboBox_Ano.Items.Clear();

            // Adiciona a opção "Todos" à ComboBox
            comboBox_Ano.Items.Add("Todos");

            // Conecta ao banco de dados e executa a consulta SQL
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = @"SELECT DISTINCT
                        YEAR(ORCCAB.orccab_Cadastro) as [Ano Cadastro]
                            FROM 
                                ORCCAB
                            INNER JOIN 
                                Orcst ON ORCCAB.orccab_Status = Orcst.st_codigo
                            WHERE 
                                  MONTH(ORCCAB.orccab_Cadastro) LIKE @Mes AND 
                                  ORCCAB.orccab_vendedor LIKE @Gerente AND 
                                  ORCCAB.orccab_UF LIKE @UF AND 
                                  ORCCAB.orccab_orcamentista LIKE @Vendedor AND 
                                  numeroOrcamento LIKE @NumeroOrcamento AND
                                  ORCCAB.orccab_Status like @Status";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                // Adicionando parâmetros

                if (String.IsNullOrEmpty(comboBox_Status.Text) || comboBox_Status.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Status", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Status", comboBox_Status.Text);
                }


                if (String.IsNullOrEmpty(textBox_orcamento.Text) || textBox_orcamento.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@NumeroOrcamento", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@NumeroOrcamento", textBox_orcamento.Text);
                }


                if (String.IsNullOrEmpty(comboBox_Mes.Text) || comboBox_Mes.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Mes", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Mes", comboBox_Mes.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Vendedor.Text) || comboBox_Vendedor.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Vendedor", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Vendedor", comboBox_Vendedor.Text);
                }

                if (String.IsNullOrEmpty(comboBox_UF.Text) || comboBox_UF.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@UF", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@UF", comboBox_UF.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Gerente.Text) || comboBox_Gerente.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Gerente", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Gerente", comboBox_Gerente.Text);
                }
                connection.Open();

                // Executa o comando SQL e lê os resultados
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Adiciona cada item à ComboBox
                        comboBox_Ano.Items.Add(reader["Ano Cadastro"].ToString());
                    }
                }
            }
        }

        private void comboBox_Ano_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Atualizar_Datagrid();
            this.Cursor = Cursors.Default;
        }

        private void comboBox_Mes_DropDown(object sender, EventArgs e)
        {
            // Limpa os itens existentes na ComboBox
            comboBox_Mes.Items.Clear();

            // Adiciona a opção "Todos" à ComboBox
            comboBox_Mes.Items.Add("Todos");

            // Conecta ao banco de dados e executa a consulta SQL
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = @"SELECT DISTINCT
                        MONTH(ORCCAB.orccab_Cadastro) as [Mes Cadastro]
                            FROM 
                                ORCCAB
                            INNER JOIN 
                                Orcst ON ORCCAB.orccab_Status = Orcst.st_codigo
                            WHERE 
                                  YEAR(ORCCAB.orccab_Cadastro) LIKE @Ano AND 
                                  ORCCAB.orccab_vendedor LIKE @Gerente AND 
                                  ORCCAB.orccab_UF LIKE @UF AND 
                                  ORCCAB.orccab_orcamentista LIKE @Vendedor AND 
                                  numeroOrcamento LIKE @NumeroOrcamento AND
                                  ORCCAB.orccab_Status like @Status";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                // Adicionando parâmetros
                if (String.IsNullOrEmpty(comboBox_Status.Text) || comboBox_Status.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Status", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Status", comboBox_Status.Text);
                }


                if (String.IsNullOrEmpty(textBox_orcamento.Text) || textBox_orcamento.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@NumeroOrcamento", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@NumeroOrcamento", textBox_orcamento.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Ano.Text) || comboBox_Ano.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Ano", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Ano", comboBox_Ano.Text);
                }


                if (String.IsNullOrEmpty(comboBox_Vendedor.Text) || comboBox_Vendedor.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Vendedor", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Vendedor", comboBox_Vendedor.Text);
                }

                if (String.IsNullOrEmpty(comboBox_UF.Text) || comboBox_UF.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@UF", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@UF", comboBox_UF.Text);
                }

                if (String.IsNullOrEmpty(comboBox_Gerente.Text) || comboBox_Gerente.Text == "Todos")
                {
                    command.Parameters.AddWithValue("@Gerente", "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Gerente", comboBox_Gerente.Text);
                }
                connection.Open();

                // Executa o comando SQL e lê os resultados
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Adiciona cada item à ComboBox
                        comboBox_Mes.Items.Add(reader["Mes Cadastro"].ToString());
                    }
                }
            }
        }

        private void comboBox_Mes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Atualizar_Datagrid();
            this.Cursor = Cursors.Default;
        }

        private void Atualizar_Panel_Inferior()
        {
            // Inicializa a soma
            decimal soma_valorVenda = 0;
            decimal soma_valorLista = 0;
            decimal soma_valorServicos = 0;

            // Itera pelas linhas do DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Verifica se a célula não está vazia e se o valor é numérico
                if (row.Cells["Valor Venda"].Value != null && decimal.TryParse(row.Cells["Valor Venda"].Value.ToString(), out decimal valorVenda))
                {
                    // Adiciona o valor à soma
                    soma_valorVenda += valorVenda;
                }

                if (row.Cells["Valor Lista"].Value != null && decimal.TryParse(row.Cells["Valor Lista"].Value.ToString(), out decimal valorLista))
                {
                    // Adiciona o valor à soma
                    soma_valorLista += valorLista;
                }

                if (row.Cells["Servicos"].Value != null && decimal.TryParse(row.Cells["Servicos"].Value.ToString(), out decimal valorServicos))
                {
                    // Adiciona o valor à soma
                    soma_valorServicos += valorServicos;
                }
            }

            // Exibe o total na Label
            label_total_valor_venda.Text = soma_valorVenda.ToString("C"); // Formata o valor como moeda
            label_Valor_Lista.Text = soma_valorLista.ToString("C"); // Formata o valor como moeda
            label_Total_Servico.Text = soma_valorServicos.ToString("C"); // Formata o valor como moeda

            List<decimal> percentuais = new List<decimal>();

            // Itera pelas linhas do DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Verifica se a célula não está vazia e se o valor é numérico
                if (row.Cells["Desconto/over"].Value != null && decimal.TryParse(row.Cells["Desconto/over"].Value.ToString().Replace("%", ""), out decimal percentual))
                {
                    // Adiciona o valor à lista de percentuais
                    percentuais.Add(percentual / 10000); // Convertendo para proporção
                }
            }

            if (percentuais.Count > 0)
            {
                // Calcula a média dos percentuais (em proporção)
                decimal mediaProporcao = percentuais.Average();

                // Converte a média de volta para porcentagem
                decimal mediaPorcentagem = mediaProporcao * 100;

                // Exibe a média na Label
                label_Desconto_over_medio.Text = mediaPorcentagem.ToString("0.00\\%"); // Formata como porcentagem
            }
            else
            {
                // Se não houver nenhum percentual válido, exibe uma mensagem
                label_Desconto_over_medio.Text = "N/A";
            }
        }

        private void textBox_orcamento_TextChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Atualizar_Datagrid();
            this.Cursor = Cursors.Default;
        }

        private void button_exportar_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Arquivo do Excel (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Salvar como";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                ExportToExcel(dataGridView1, saveFileDialog.FileName);
                this.Cursor = Cursors.Default;
            }

        
        }
        private void ExportToExcel(DataGridView dataGridView, string filePath)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = null;

            try
            {
                worksheet = workbook.ActiveSheet;

                for (int i = 1; i < dataGridView.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        if (dataGridView.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }

                workbook.SaveAs(filePath);
                MessageBox.Show("Dados exportados com sucesso para: " + filePath, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao exportar os dados para o Excel: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                excelApp.Quit();
                workbook = null;
                excelApp = null;
            }
        }
    }
}
