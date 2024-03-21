using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class Form_Modulos : Form
    {


        List<string> valor_Gerenciador_SIP;
        string Banco = "BANCODEDADOS";
        string Instancia = @"INSTANCIA";
        string Senha = "SENHA";
        string Usuario = "sa";
        private DataTable dataTable;
        Thread t1;
        int id;
        string Tipo;
        string Login;
        private bool painelAmpliado = false;

        public Form_Modulos(List<string> valorGerenciador, string login)
        {
            Login = login;
            valor_Gerenciador_SIP = valorGerenciador;
            InitializeComponent();
            dataTable = new DataTable();
            Atualizar();
            Combo_Linha();
            Combo_Familia();
            Combo_Regime();
            Combo_Situacao();
            CarregarComboBoxProfundidade();
            comboBox_TravarRepresentante.Items.Add("Sim");
            comboBox_TravarRepresentante.Items.Add("Não");
            controleAcessoUsuario();
            comboBox_Linha.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_tipo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Regime.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Situacao.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_TravarRepresentante.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_profundidade.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void controleAcessoUsuario()
        {
            if (valor_Gerenciador_SIP.Contains("Engenharia de Produtos"))
            {
                button_limpar.Enabled = true;
                button_confirmar.Enabled = true;
                button_Alterar.Enabled = true;
                button_Deletar.Enabled = true;
                panel_criar.Enabled = true;
            }
        }

        private void Atualizar()
        {
            Cursor.Current = Cursors.WaitCursor;
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
            con.Open();
            string query = "SELECT  [ID] \n" +
                          ",[Modelo] \n" +
                          ",[Tipo] \n" +
                          ",[Resfriamento] \n" +
                          ",[Situacao] \n" +
                          ",[Linha] \n" +
                          ",[Travar_representante],[BaseCodigo],[Profundidade] \n" +
                      "FROM[BANCODEDADOS].[dbo].[Modelos_Eng] where basecodigo = 'Eletrofrio' order by Modelo";
            dataTable.Clear();
            using (SqlCommand command = new SqlCommand(query, con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Carregar os dados do leitor no DataTable existente
                    dataTable.Load(reader);
                }
            }
            con.Close();
            Cursor.Current = Cursors.Default;

            // Atualizar o DataSource do DataGridView com o DataTable existente
            dataGridView1.DataSource = dataTable;
        }

        private void button_Voltar_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Planilha_eng);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_Planilha_eng(object obj)
        {
            Application.Run(new Form_Planilha_Eng(valor_Gerenciador_SIP, Login));
        }



        private void button_confirmar_Click(object sender, EventArgs e)
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (comboBox_Linha.Text == "")
            {
                MessageBox.Show("Informar a linha do expositor!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Informar o nome do expositor!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox_tipo.Text == "")
            {
                MessageBox.Show("Informar a familia do expositor!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox_Regime.Text == "")
            {
                MessageBox.Show("Informar o regime do expositor!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox_Situacao.Text == "")
            {
                MessageBox.Show("Informar a situação do expositor!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox_TravarRepresentante.Text == "")
            {
                MessageBox.Show("Informar se deve ser travado o expositor para o representante!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox_profundidade.Text == "")
            {
                MessageBox.Show("Informar a profundidade do expositor!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("Selecione somente uma linha antes de prosseguir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Sai do método sem executar o procedimento
            }
            string profundidade = comboBox_profundidade.Text;
            string Linha = comboBox_Linha.Text;
            string Modelo = textBox1.Text;
            string Familia = comboBox_tipo.Text;
            string Regime = comboBox_Regime.Text;
            string Situacao = comboBox_Situacao.Text;
            string TravarRepresentante1 = comboBox_TravarRepresentante.Text;
            string TravarRepresentante = "";

            if (TravarRepresentante1 == "Não")
            {
                TravarRepresentante = "0";
            }
            if (TravarRepresentante1 == "Sim")
            {
                TravarRepresentante = "1";
            }


            DialogResult result = MessageBox.Show("Inserir um novo modelo ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Deletar o registro do banco de dados
                    using (SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha))
                    {
                        con.Open();
                        using (SqlTransaction transaction = con.BeginTransaction())
                        {
                            string query = "INSERT INTO Modelos_Eng VALUES (@Modelo, @Familia, @Regime, @Situacao, @Linha, @TravarRepresentante, 'Eletrofrio', @Profundidade);";
                            using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Modelo", Modelo);
                                cmd.Parameters.AddWithValue("@Familia", Familia);
                                cmd.Parameters.AddWithValue("@Regime", Regime);
                                cmd.Parameters.AddWithValue("@Situacao", Situacao);
                                cmd.Parameters.AddWithValue("@Linha", Linha);
                                cmd.Parameters.AddWithValue("@TravarRepresentante", TravarRepresentante);
                                cmd.Parameters.AddWithValue("@Profundidade", profundidade);
                                cmd.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                    }
                    SqlConnection con1 = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
                    con1.Open();
                    string insert_log = $"insert into Log_eng values ('{Login}','Expositores','{Modelo}','','{Modelo}-{Familia}-{Regime}-{Situacao}-{Linha}-{TravarRepresentante}-{profundidade}','Criação de expositores','{dataAtual}',NULL,NULL);";
                    SqlTransaction transaction1 = con1.BeginTransaction();
                    SqlCommand cmd1 = new SqlCommand(insert_log, con1, transaction1);
                    cmd1.ExecuteNonQuery();
                    transaction1.Commit();
                    con1.Close();
                    InserirDadosNotificacao("Novo");
                    Atualizar();
                    LimparCampos();
                    MessageBox.Show("Arquivo Criado!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {

            }

        }

        private void Combo_Situacao()
        {
            Cursor.Current = Cursors.WaitCursor;
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");

            con.Open();
            string query = "SELECT \n" +
                              "[Situacao] \n" +
                              "FROM[BANCODEDADOS].[dbo].[Modelo_Situacao_Eng]";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Adicionar um item vazio no início da combobox
                    comboBox_Situacao.Items.Add("");

                    while (reader.Read())
                    {
                        comboBox_Situacao.Items.Add(reader.GetString(0));
                    }
                    con.Close();
                    comboBox_Situacao.SelectedIndex = 0;
                    Tipo = comboBox_Situacao.Text;
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void Combo_Regime()
        {
            Cursor.Current = Cursors.WaitCursor;
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");

            con.Open();
            string query = "SELECT \n" +
                              "[Regime] \n" +
                           "FROM[BANCODEDADOS].[dbo].[Modelo_Regime_Eng] where basecodigo = 'Eletrofrio'";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Adicionar um item vazio no início da combobox
                    comboBox_Regime.Items.Add("");

                    while (reader.Read())
                    {
                        comboBox_Regime.Items.Add(reader.GetString(0));
                    }
                    con.Close();
                    comboBox_Regime.SelectedIndex = 0;
                    Tipo = comboBox_Regime.Text;
                }
            }
            Cursor.Current = Cursors.Default;
        }


        private void Combo_Familia()
        {
            comboBox_tipo.Items.Clear();
            Cursor.Current = Cursors.WaitCursor;
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");

            con.Open();
            string query = "SELECT \n" +
                              "[Resfriamento] \n" +
                              "FROM[BANCODEDADOS].[dbo].[Modelo_Familia_Eng] where basecodigo = 'Eletrofrio' and Linha = '" + comboBox_Linha.Text + "'";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Adicionar um item vazio no início da combobox
                    comboBox_tipo.Items.Add("");

                    while (reader.Read())
                    {
                        comboBox_tipo.Items.Add(reader.GetString(0));
                    }
                    con.Close();
                    comboBox_tipo.SelectedIndex = 0;
                    Tipo = comboBox_tipo.Text;
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void Combo_Linha()
        {
            Cursor.Current = Cursors.WaitCursor;
            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");

            con.Open();
            string query = "SELECT \n" +
                              "[Linha] \n" +
                              "FROM[BANCODEDADOS].[dbo].[Linha_Expositor] where basecodigo = 'Eletrofrio'";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Adicionar um item vazio no início da combobox
                    comboBox_Linha.Items.Add("");

                    while (reader.Read())
                    {
                        comboBox_Linha.Items.Add(reader.GetString(0));
                    }
                    con.Close();
                    comboBox_Linha.SelectedIndex = 0;
                    Tipo = comboBox_Linha.Text;
                }
            }

            Cursor.Current = Cursors.Default;
        }


        private void button_Deletar_Click(object sender, EventArgs e)
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Selecione somente uma linha antes de prosseguir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Sai do método sem executar o procedimento
            }
            DialogResult result = MessageBox.Show("Deseja deletar ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Verifica se há uma linha selecionada
                if (dataGridView1.SelectedRows.Count > 0)
                {

                    InserirDadosNotificacao("Deletar");


                    // Obtém o valor da coluna "id" da linha selecionada
                    // int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                    string Modelo = dataGridView1.SelectedRows[0].Cells["Modelo"].Value.ToString();

                    SqlConnection con1 = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
                    con1.Open();
                    string insert_log = "DECLARE @ConcatenatedString NVARCHAR(MAX) = ''; \n" +
                        "--Declara uma variável de tabela para armazenar os resultados \n" +
                             "DECLARE @Results TABLE(ConcatenatedValue NVARCHAR(MAX)); \n" +
                                        "--Insere os valores concatenados na variável de tabela \n" +
                                        "INSERT INTO @Results(ConcatenatedValue) \n" +
                                            "SELECT CONCAT(Modelo, ',', Tipo, ',', Resfriamento, ',', Situacao, ',', Linha, ',', Travar_representante, ',', Profundidade) \n" +
                                            $"FROM Modelos_Eng where Modelo = '{Modelo}' \n" +
                                            "-- Concatena os valores em uma única string \n" +
                                            "SELECT @ConcatenatedString = @ConcatenatedString + ConcatenatedValue + '-' \n" +
                                            "FROM @Results; \n" +
                                            "IF LEN(@ConcatenatedString) > 0 \n" +
                                            "BEGIN \n" +
                                            "SET @ConcatenatedString = LEFT(@ConcatenatedString, LEN(@ConcatenatedString) - 1);\n" +
                                            "END \n" +
                                            "--Remove o último caractere  -  da string resultante \n" +
                                            "SET @ConcatenatedString = LEFT(@ConcatenatedString, LEN(@ConcatenatedString) - 1); " +
                                            $"insert into Log_eng values ('{Login}','Expositores','{Modelo}',(SELECT @ConcatenatedString),'','Remoção de modulos','{dataAtual}',NULL,NULL);";
                    SqlTransaction transaction1 = con1.BeginTransaction();
                    SqlCommand cmd1 = new SqlCommand(insert_log, con1, transaction1);
                    cmd1.ExecuteNonQuery();
                    transaction1.Commit();
                    con1.Close();

                    // Deletar o registro do banco de dados
                    DeletarRegistroDoBancoDeDados(Modelo);

                    // Remove a linha do DataGridView
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                    LimparCampos();
                    MessageBox.Show("Arquivo deletado!");
                }
            }
            else
            {

            }
        }

        private void DeletarRegistroDoBancoDeDados(string modelo)
        {
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // Query 1
                    string query1 = "DELETE FROM Planilhas_Eng WHERE Modelo LIKE @Modelo;";
                    using (SqlCommand cmd1 = new SqlCommand(query1, con, transaction))
                    {
                        cmd1.Parameters.AddWithValue("@Modelo", modelo);
                        cmd1.ExecuteNonQuery();
                    }

                    // Query 2
                    /*string query2 = "DELETE FROM Cabeceira_Eng WHERE cabeceira IN (SELECT cabeceira FROM Cabeceira_Ligacao_Eng WHERE Expositor LIKE @Modelo);";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con, transaction))
                    {
                        cmd2.Parameters.AddWithValue("@Modelo", modelo);
                        cmd2.ExecuteNonQuery();
                    }

                    // Query 3
                    string query3 = "DELETE FROM Cabeceira_Acessorios_Eng WHERE cabeceira IN (SELECT cabeceira FROM Cabeceira_Ligacao_Eng WHERE Expositor LIKE @Modelo);";
                    using (SqlCommand cmd3 = new SqlCommand(query3, con, transaction))
                    {
                        cmd3.Parameters.AddWithValue("@Modelo", modelo);
                        cmd3.ExecuteNonQuery();
                    }*/

                    // Query 4
                    string query4 = "DELETE FROM Cabeceira_Ligacao_Eng WHERE Expositor LIKE @Modelo;";
                    using (SqlCommand cmd4 = new SqlCommand(query4, con, transaction))
                    {
                        cmd4.Parameters.AddWithValue("@Modelo", modelo);
                        cmd4.ExecuteNonQuery();
                    }

                    // Query 5
                    string query5 = "DELETE FROM Modelos_Eng WHERE Modelo LIKE @Modelo;";
                    using (SqlCommand cmd5 = new SqlCommand(query5, con, transaction))
                    {
                        cmd5.Parameters.AddWithValue("@Modelo", modelo);
                        cmd5.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Caso ocorra algum erro, desfaz a transação (rollback)
                    MessageBox.Show("Erro ao deletar dados!");
                    transaction.Rollback();
                    // Trate ou registre a exceção aqui
                }
            }
        }


        private void button_Alterar_Click(object sender, EventArgs e)
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string profundidade = comboBox_profundidade.Text;
            string Linha = comboBox_Linha.Text;
            string Modelo = textBox1.Text;
            string Familia = comboBox_tipo.Text;
            string Regime = comboBox_Regime.Text;
            string Situacao = comboBox_Situacao.Text;
            string TravarRepresentante1 = comboBox_TravarRepresentante.Text;
            string TravarRepresentante = "";

            if (TravarRepresentante1 == "Não")
            {
                TravarRepresentante = "0";
            }
            if (TravarRepresentante1 == "Sim")
            {
                TravarRepresentante = "1";
            }

            DialogResult result = MessageBox.Show("Deseja alterar ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (id > 0)
                {

                    InserirDadosNotificacao("Alterar");


                    SqlConnection con1 = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
                    con1.Open();
                    string insert_log = "DECLARE @ConcatenatedString NVARCHAR(MAX) = ''; \n" +
                            "--Declara uma variável de tabela para armazenar os resultados \n" +
                             "DECLARE @Results TABLE(ConcatenatedValue NVARCHAR(MAX)); \n" +
                                        "--Insere os valores concatenados na variável de tabela \n" +
                                        "INSERT INTO @Results(ConcatenatedValue) \n" +
                                            "SELECT CONCAT(Modelo, '-', Tipo, '-', Resfriamento, '-', Situacao, '-', Linha, '-', Travar_representante, '-', Profundidade) \n" +
                                            $"FROM Modelos_Eng where Modelo = '{Modelo}' \n" +
                                            "-- Concatena os valores em uma única string \n" +
                                            "SELECT @ConcatenatedString = @ConcatenatedString + ConcatenatedValue + '-' \n" +
                                            "FROM @Results; \n" +
                                            "IF LEN(@ConcatenatedString) > 0 \n" +
                                            "BEGIN \n" +
                                            "SET @ConcatenatedString = LEFT(@ConcatenatedString, LEN(@ConcatenatedString) - 1);\n" +
                                            "END \n" +
                                            "--Remove o último caractere  -  da string resultante \n" +
                                            //"SET @ConcatenatedString = LEFT(@ConcatenatedString, LEN(@ConcatenatedString) - 1); " +
                                            $"insert into Log_eng values ('{Login}','Expositores','{Modelo}',(SELECT @ConcatenatedString),'{Modelo}-{Familia}-{Regime}-{Situacao}-{Linha}-{TravarRepresentante}-{profundidade}','alteração de expositores','{dataAtual}',NULL,NULL);";
                    SqlTransaction transaction1 = con1.BeginTransaction();
                    SqlCommand cmd1 = new SqlCommand(insert_log, con1, transaction1);
                    cmd1.ExecuteNonQuery();
                    transaction1.Commit();
                    con1.Close();


                    SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha);
                    string query = "UPDATE [BANCODEDADOS].[dbo].[Modelos_Eng] SET Modelo = '" + Modelo + "', Tipo = '" + Familia + "', Resfriamento = '" + Regime + "' \n" +
                                      ",Situacao = '" + Situacao + "' \n" +
                                      ",Linha = '" + Linha + "' \n" +
                                      ",Travar_representante = '" + TravarRepresentante + "',basecodigo = 'Eletrofrio', profundidade = '" + profundidade + "' \n" +
                                      "WHERE ID = " + id + "";
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    SqlCommand cmd = new SqlCommand(query, con, transaction);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    con.Close();


                    Atualizar();
                    MessageBox.Show("Arquivo alterado!");
                    LimparCampos();
                }
                else
                {
                    MessageBox.Show("Seleciona a linha que deseja alterar!");
                }
            }
            else
            {

            }
        }

        private void InserirDadosNotificacao(string status)
        {
            Cursor.Current = Cursors.WaitCursor;
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string Id = label_id.Text; // Obtenha o valor do Id desejado

            string linha = "";
            string Modelo = "";
            string Familia = "";
            string Regime = "";
            string Situacao = "";
            string Travar = "";
            string Profundidade = "";

            // Percorra as linhas do DataGridView para encontrar a linha com o Id correspondente
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string valorId = row.Cells["Id"].Value.ToString(); // Substitua "Id" pelo nome da coluna com os IDs

                if (valorId == Id)
                {
                    // Encontrou a linha correspondente, agora você pode obter os valores das outras colunas
                    linha = row.Cells["Linha"].Value.ToString();
                    Modelo = row.Cells["Modelo"].Value.ToString();
                    Familia = row.Cells["Tipo"].Value.ToString();
                    Regime = row.Cells["Resfriamento"].Value.ToString();
                    Situacao = row.Cells["Situacao"].Value.ToString();
                    Travar = row.Cells["Travar_representante"].Value.ToString();
                    Profundidade = row.Cells["Profundidade"].Value.ToString();
                    break; // Pode parar de procurar
                }
            }

            // Criação da tabela temporária #TEMP1 no banco de dados

            if (Situacao == "Ativo" || comboBox_Situacao.Text == "Ativo")
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + ""))
                {
                    bool travarRepresentante = false;
                    if (comboBox_TravarRepresentante.Text == "Sim")
                    {
                        travarRepresentante = true;
                    }
                    if (comboBox_TravarRepresentante.Text == "True")
                    {
                        travarRepresentante = true;
                    }
                    con.Open();
                    string query = "";
                    if (status == "Alterar")
                    {
                        query = "Insert into Notificacao values (\n" +
                                   $"'{Login}','{Modelo}','{Modelo},{Familia},{Regime},{Situacao},{linha},{Travar},{Profundidade}'," +
                                   $"'{textBox1.Text},{comboBox_tipo.Text},{comboBox_Regime.Text},{comboBox_Situacao.Text},{comboBox_Linha.Text},{travarRepresentante},{comboBox_profundidade.Text}'," +
                                   $"'Alteração no expositor','{dataAtual}',NULL,NULL)";
                    }
                    else if (status == "Deletar")
                    {
                        query = "Insert into Notificacao values (\n" +
                                  $"'{Login}','{textBox1.Text}','{textBox1.Text},{comboBox_tipo.Text},{comboBox_Regime.Text},{comboBox_Situacao.Text},{comboBox_Linha.Text},{travarRepresentante},{comboBox_profundidade.Text}'," +
                                  $"''," +
                                  $"'Deletou expositor','{dataAtual}',NULL,NULL)";
                    }
                    else if (status == "Novo")
                    {
                        query = "Insert into Notificacao values (\n" +
                                  $"'{Login}','{textBox1.Text}','','{textBox1.Text},{comboBox_tipo.Text},{comboBox_Regime.Text},{comboBox_Situacao.Text},{comboBox_Linha.Text},{travarRepresentante},{comboBox_profundidade.Text}'," +
                                  $"'Criou expositor','{dataAtual}',NULL,NULL)";
                    }

                    SqlTransaction transaction1 = con.BeginTransaction();
                    SqlCommand cmd1 = new SqlCommand(query, con, transaction1);
                    cmd1.ExecuteNonQuery();
                    transaction1.Commit();
                    con.Close();
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void LimparCampos()
        {
            comboBox_Linha.Text = "";
            textBox1.Text = "";
            comboBox_Regime.Text = "";
            comboBox_tipo.Text = "";
            comboBox_Situacao.Text = "";
            // Limpar ComboBox comboBox_TravarRepresentante
            comboBox_TravarRepresentante.SelectedItem = null;

            // Limpar ComboBox comboBox_profundidade
            comboBox_profundidade.SelectedItem = null;
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Certifica-se de que uma linha válida foi selecionada
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Acessa os valores das colunas específicas do DataGridView
                id = Convert.ToInt32(row.Cells["ID"].Value);
                string valorColuna1 = row.Cells[1].Value.ToString();
                string valorColuna2 = row.Cells[2].Value.ToString();
                string valorColuna3 = row.Cells[3].Value.ToString();
                string valorColuna4 = row.Cells[4].Value.ToString();
                string valorColuna5 = row.Cells[5].Value.ToString();
                string valorColuna8 = row.Cells[8].Value.ToString();
                bool valorColuna6 = Convert.ToBoolean(row.Cells[6].Value);

                // Insere os valores nas ComboBox e TextBox correspondentes
                comboBox_Linha.SelectedItem = valorColuna5;
                textBox1.Text = valorColuna1;
                comboBox_tipo.SelectedItem = valorColuna2;
                comboBox_Regime.SelectedItem = valorColuna3;
                comboBox_Situacao.SelectedItem = valorColuna4;
                comboBox_profundidade.SelectedItem = valorColuna8;
                label_id.Text = id.ToString();
                if (valorColuna6 == false)
                {
                    comboBox_TravarRepresentante.Items.Clear();
                    comboBox_TravarRepresentante.Items.Add("Sim");
                    comboBox_TravarRepresentante.Items.Add("Não");

                    comboBox_TravarRepresentante.SelectedItem = "Não";
                }
                if (valorColuna6 == true)
                {
                    comboBox_TravarRepresentante.Items.Clear();
                    comboBox_TravarRepresentante.Items.Add("Sim");
                    comboBox_TravarRepresentante.Items.Add("Não");

                    comboBox_TravarRepresentante.SelectedItem = "Sim";
                }


            }
        }

        private void button_limpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void comboBox_Linha_SelectedIndexChanged(object sender, EventArgs e)
        {
            Combo_Familia();
        }

        private void comboBox_profundidade_DropDown(object sender, EventArgs e)
        {
            CarregarComboBoxProfundidade();
        }

        private void CarregarComboBoxProfundidade()
        {
            comboBox_profundidade.Items.Clear();
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = "SELECT [Profundidade] FROM [BANCODEDADOS].[dbo].[Gen_Profundidade] where BaseCodigo = 'Eletrofrio'";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string profundidade = reader["Profundidade"].ToString();
                    comboBox_profundidade.Items.Add(profundidade);
                }
                reader.Close();
            }
        }

        private void textBox_pesquisaExpositor_TextChanged(object sender, EventArgs e)
        {
            // Atualizar o filtro sempre que o texto do TextBox mudar
            string filterText = textBox_pesquisaExpositor.Text;
            filterText = filterText.Replace("'", "''"); // Tratar as aspas para evitar problemas no filtro
            dataTable.DefaultView.RowFilter = $"Modelo LIKE '%{filterText}%'";
        }



        private void Form_Modulos_Load(object sender, EventArgs e)
        {
            panel3.Height = 12;
            painelAmpliado = false;
            panel_cadastro.Visible = false;
        }

        private void panel18_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void panel18_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void panel18_Click(object sender, EventArgs e)
        {
            if (painelAmpliado)
            {
                // Se o painel estiver ampliado, diminua para 12
                panel3.Height = 12;
                painelAmpliado = false;
                panel_cadastro.Visible = false;
            }
            else
            {
                // Se o painel não estiver ampliado, aumente para 400
                panel3.Height = 70;
                painelAmpliado = true;
                panel_cadastro.Visible = true;
            }
        }

        private void button_linha_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Modulo_Linha);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }

        private void Form_Modulo_Linha(object obj)
        {
            Application.Run(new Form_Modulos_Linha(valor_Gerenciador_SIP, Login));
        }

        private void button_Familia_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Modulo_Familia);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_Modulo_Familia(object obj)
        {
            Application.Run(new Form_Modulos_Familia(valor_Gerenciador_SIP, Login));
        }
    }
}
