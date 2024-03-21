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
    public partial class Form_Cabeceira_Eng_New : Form
    {
        string Banco = "BANCODEDADOS";
        string Instancia = @"INSTANCIA";
        string Senha = "SENHA";
        string Usuario = "sa";
        string Tipo = "";
        string Lado = "";
        string Modelo1 = "";
        string Modelo2 = "";
        Thread t1;
        List<string> valor_Gerenciador_SIP;
        string login = "";
        //private DataTable dataTable;
        private DataTable dataTable = new DataTable();
        string linhaModelo = "";

        // Classe para representar os dados da consulta
        class SeuObjeto
        {
            public string Cabeceira { get; set; }
            public string Codigo { get; set; }
            public string Esq_Dir { get; set; }
            public string Expositor { get; set; }
            public string Acessorios { get; set; }
        }

        public Form_Cabeceira_Eng_New(List<string> valorGerenciador, string login_1, string linha)
        {

            valor_Gerenciador_SIP = valorGerenciador;
            linhaModelo = linha;
            login = login_1;
            InitializeComponent();
            textBox_Nome.Text = "Cab ";
            Carregar_Lista_Tipo();
            Carregar_Lista_Cabeceira();
            // dataTable = new DataTable();
            controleAcessoUsuario();

        }

        private void controleAcessoUsuario()
        {
            if (valor_Gerenciador_SIP.Contains("Engenharia de Produtos"))
            {
                //button_limpar.Enabled = true;
                button_Deletar.Enabled = true;
                button_Salvar.Enabled = true;
                button_Novo.Enabled = true;
                tabControl6.Enabled = true;
                tabControl8.Enabled = true;
                textBox_Observacao.Enabled = true;
            }
        }

        private void Carregar_Lista_Cabeceira()
        {
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = "SELECT *  FROM Cabeceira_Eng";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                dataTable.Clear();

                connection.Open();
                adapter.Fill(dataTable);
                dataGridView_ListaCab.DataSource = dataTable;
            }
        }

        private void Carregar_Lista_Tipo()
        {
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = "SELECT [Tipo_Cab] FROM [Cabeceira_Tipo_Eng] where basecodigo = 'Eletrofrio'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string nome = reader["Tipo_Cab"].ToString();
                    comboBox_Tipo.Items.Add(nome);
                }

                reader.Close();
            }
        }


        private void textBox7_Validating(object sender, CancelEventArgs e)
        {
            string input1 = textBox_Espessura2.Text.Trim();
            string input2 = textBox5.Text.Trim();
            string input3 = textBox6.Text.Trim();
            string input4 = textBox7.Text.Trim();
            if (!string.IsNullOrEmpty(input1))
            {
                if (!input1.EndsWith("cm"))
                {
                    // Remove qualquer "cm" existente no final
                    input1 = input1.Replace("cm", "").Trim();
                    double value;
                    if (double.TryParse(input1, out value) || double.TryParse(input2, out value) || double.TryParse(input3, out value) || double.TryParse(input4, out value))
                    {
                        // Adiciona "cm" após o númerodataTable = new DataTable();
                        textBox_Espessura2.Text = value.ToString("0.##") + " cm";
                    }
                }
            }
            if (!input2.EndsWith("cm"))
            {
                input2 = input2.Replace("cm", "").Trim();
                double value;

                if (double.TryParse(input2, out value))
                {
                    // Adiciona "cm" após o número                    
                    textBox5.Text = value.ToString("0.##") + " cm";
                }
            }
            if (!input3.EndsWith("cm"))
            {
                input3 = input3.Replace("cm", "").Trim();
                double value;

                if (double.TryParse(input3, out value))
                {
                    // Adiciona "cm" após o número
                    textBox6.Text = value.ToString("0.##") + " cm";
                }
            }
            if (!input4.EndsWith("cm"))
            {
                input4 = input4.Replace("cm", "").Trim();
                double value;

                if (double.TryParse(input4, out value))
                {
                    // Adiciona "cm" após o número
                    textBox7.Text = value.ToString("0.##") + " cm";
                }
            }
        }

        private void comboBox_Tipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox_modelos_cab.Items.Clear();
            listBox_modelos_cab_dir.Items.Clear();
            listBox_modelos_cab_esq.Items.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            if (comboBox_Tipo.SelectedItem != null)
            {
                Tipo = comboBox_Tipo.SelectedItem.ToString();

                if (Tipo != "Ajuste")
                {
                    panel_lado_cab.Visible = true;
                    tableLayoutPanel8.Visible = true;
                    tableLayoutPanel7.Visible = false;
                    //16/08/2023-09:39 --> novo
                    tableLayoutPanel12.Enabled = false;
                }
                else
                {


                    panel_lado_cab.Visible = false;
                    tableLayoutPanel8.Visible = false;
                    tableLayoutPanel7.Visible = true;
                    comboBox_Modelo2_Parte2.Text = "";
                    comboBox_modelo1_parte2.Text = "";
                    Lado = "";

                    listBox_modelos_cab.Items.Clear();
                    listBox_modelos_cab_dir.Items.Clear();
                    listBox_modelos_cab_esq.Items.Clear();
                    LiberarPanelCab(Tipo);
                }
            }
            textBox_Nome.Text = "Cab " + Tipo;
            checkBox_dir.Checked = false;
            checkBox_esq.Checked = false;
            comboBox_Modelo_parte1.Text = "";
            //CorCabeceira(Tipo);
        }

        private void LiberarPanelCab(string tipo)
        {
            if (tipo == "Ajuste")
            {
                listBox_modelos_cab_esq.Enabled = true;
                listBox_modelos_cab_esq.BackColor = SystemColors.Window;

                listBox_modelos_cab_dir.Enabled = true;
                listBox_modelos_cab_dir.BackColor = SystemColors.Window;
            }

            if (tipo == "Dir")
            {
                if (checkBox_dir.Checked == false)
                {
                    listBox_modelos_cab_dir.Enabled = false;
                    listBox_modelos_cab_dir.BackColor = SystemColors.ControlLight;
                }
                else
                {
                    listBox_modelos_cab_dir.Enabled = true;
                    listBox_modelos_cab_dir.BackColor = SystemColors.Window;

                    listBox_modelos_cab_esq.Enabled = false;
                    listBox_modelos_cab_esq.BackColor = SystemColors.ControlLight;
                }
            }

            if (tipo == "Esq")
            {
                if (checkBox_esq.Checked == false)
                {
                    listBox_modelos_cab_esq.Enabled = false;
                    listBox_modelos_cab_esq.BackColor = SystemColors.ControlLight;
                }
                else
                {
                    listBox_modelos_cab_esq.Enabled = true;
                    listBox_modelos_cab_esq.BackColor = SystemColors.Window;

                    listBox_modelos_cab_dir.Enabled = false;
                    listBox_modelos_cab_dir.BackColor = SystemColors.ControlLight;
                }
            }
            if (tipo == "Novo")
            {
                listBox_modelos_cab_esq.Enabled = false;
                listBox_modelos_cab_esq.BackColor = SystemColors.ControlLight;

                listBox_modelos_cab_dir.Enabled = false;
                listBox_modelos_cab_dir.BackColor = SystemColors.ControlLight;

                checkBox_dir.Checked = false;
                checkBox_esq.Checked = false;
            }
        }

        private void CorCabeceira(string cabeceira)
        {
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = "SELECT [Grupo_Cor] FROM [Cabeceira_Tipo_Eng] where basecodigo = 'Eletrofrio' and Tipo_Cab = '" + cabeceira + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string nome = reader["Grupo_Cor"].ToString();
                    //textBox_GrupoCor1.Text = nome; // Definir o texto do textBox_GrupoCor1 como o valor do nome
                    //textBox_GrupoCor2.Text = nome;
                }

                reader.Close();
            }
        }

        private void comboBox10_DropDown(object sender, EventArgs e)
        {
            comboBox_Modelo_parte1.Items.Clear(); // Limpa os itens existentes na ComboBox
            comboBox_Modelo2_Parte2.Items.Clear();
            comboBox_modelo1_parte2.Items.Clear();

            string mensagem = "Clique em SIM para selecionar o Expositor especifico ou NÃO para selecionar o grupo.\nEspecifico (SIM) = XPTO-4\nGrupo (NÃO)=XPTO";
            MessageBoxButtons botoes = MessageBoxButtons.YesNo;
            MessageBoxIcon icone = MessageBoxIcon.Question;

            // Exibir a caixa de diálogo de mensagem com as opções personalizadas
            DialogResult result = MessageBox.Show(mensagem, "Escolha o lado", botoes, icone);


            if (result == DialogResult.Yes)
            {
                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                string query = "select Modelo from Modelos_Eng where basecodigo = 'Eletrofrio' and Linha = '" + linhaModelo + "' order by Modelo";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string nome = reader["Modelo"].ToString();
                        comboBox_Modelo_parte1.Items.Add(nome);
                        comboBox_Modelo2_Parte2.Items.Add(nome);
                        comboBox_modelo1_parte2.Items.Add(nome);
                    }
                    reader.Close();
                }


            }
            else
            {
                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                string query = "SELECT distinct LEFT(Modelo, CHARINDEX('-', Modelo + '-') - 1) AS Modelo FROM Modelos_Eng where basecodigo = 'Eletrofrio'  and Linha = '" + linhaModelo + "' order by Modelo";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string nome = reader["Modelo"].ToString();
                        comboBox_Modelo_parte1.Items.Add(nome);
                        comboBox_Modelo2_Parte2.Items.Add(nome);
                        comboBox_modelo1_parte2.Items.Add(nome);
                    }
                    reader.Close();
                }
            }
        }



        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox_esq.Checked)
            {
                listBox_modelos_cab.Items.Clear();
                checkBox_dir.Checked = false;
                Lado = "Esq";
                if (comboBox_Modelo_parte1.Text != "")
                {
                    textBox_Nome.Text = "Cab " + Tipo + " " + Lado + " " + comboBox_Modelo_parte1.Text;
                }
                else
                {
                    textBox_Nome.Text = "Cab " + Tipo + " " + Lado;
                }

                LiberarPanelCab(Lado);


                listBox_modelos_cab_dir.Items.Clear();

                if (comboBox_Modelo_parte1.Text != "")
                {
                    Carregar_Modelos_Cab_Esq(comboBox_Modelo_parte1.Text);
                }

            }
            if (checkBox_esq.Checked == false)
            {
                textBox_Nome.Text = "Cab " + Tipo;
                LiberarPanelCab(Lado);

                listBox_modelos_cab_esq.Items.Clear();
                listBox_modelos_cab.Items.Clear();
                Carregar_Modelos_Cab(comboBox_Modelo_parte1.Text);
            }

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_dir.Checked)
            {
                listBox_modelos_cab.Items.Clear();
                checkBox_esq.Checked = false;
                Lado = "Dir";
                if (comboBox_Modelo_parte1.Text != "")
                {
                    textBox_Nome.Text = "Cab " + Tipo + " " + Lado + " " + comboBox_Modelo_parte1.Text;
                }
                else
                {
                    textBox_Nome.Text = "Cab " + Tipo + " " + Lado;
                }


                LiberarPanelCab(Lado);

                listBox_modelos_cab_esq.Items.Clear();

                if (comboBox_Modelo_parte1.Text != "")
                {
                    Carregar_Modelos_Cab_Dir(comboBox_Modelo_parte1.Text);
                }

            }
            if (checkBox_dir.Checked == false)
            {
                textBox_Nome.Text = "Cab " + Tipo;

                LiberarPanelCab(Lado);

                listBox_modelos_cab_dir.Items.Clear();
                listBox_modelos_cab.Items.Clear();
                Carregar_Modelos_Cab(comboBox_Modelo_parte1.Text);
            }
        }


        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

            tabControl8.SelectedTab = tabPage_Modelos;
            listBox_modelos_cab.Items.Clear();
            listBox_modelos_cab_dir.Items.Clear();
            listBox_modelos_cab_esq.Items.Clear();
            if (checkBox_dir.Checked == true || checkBox_esq.Checked == true)
            {
                Modelo1 = comboBox_Modelo_parte1.Text;
                textBox_Nome.Text = "Cab " + Tipo + " " + Lado + " " + Modelo1;


                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                string query1 = "SELECT MAX(Espessura_Cab) AS espessura_Cab FROM Modelos_Eng INNER JOIN Modelo_Regime_Eng ON Modelos_Eng.Resfriamento = Modelo_Regime_Eng.Regime WHERE Modelo_Regime_Eng.basecodigo = 'Eletrofrio' and Modelo LIKE '" + Modelo1 + "%'  and Linha = '" + linhaModelo + "'";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query1, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string nome = reader["espessura_Cab"].ToString();
                        textBox_Espessura1.Text = nome;
                    }
                    reader.Close();
                }

                if (checkBox_dir.Checked)
                {
                    Carregar_Modelos_Cab_Dir(Modelo1);
                }
                if (checkBox_esq.Checked)
                {
                    Carregar_Modelos_Cab_Esq(Modelo1);
                }
                //Carregar_Modelos_Cab(Modelo1);
            }
            else
            {
                MessageBox.Show("Selecionar o lado da cabeceira primeiro!");
                comboBox_Modelo_parte1.Items.Clear();
            }
        }

        private void Carregar_Modelos_Cab_Esq(string Modelo)
        {
            if (Modelo.Contains("-"))
            {
                // Limpar itens existentes no ListBox
                listBox_modelos_cab_esq.Items.Clear();

                // Adicionar o valor ao ListBox
                listBox_modelos_cab_esq.Items.Add(Modelo1);
            }
            else
            {
                string query = "SELECT distinct Modelo FROM Modelos_Eng LEFT JOIN Cabeceira_Ligacao_Eng ON Modelos_Eng.Modelo = Cabeceira_Ligacao_Eng.Expositor WHERE Modelos_Eng.basecodigo = 'Eletrofrio' and Modelos_Eng.Modelo like '" + Modelo + "-%'  and Linha = '" + linhaModelo + "'";
                // Configuração da conexão com o banco de dados
                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

                // Criação da conexão com o banco de dados e execução da consulta
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    //command.Parameters.AddWithValue("@cabeceira", cabeceira);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Limpar itens existentes no ListBox
                    listBox_modelos_cab_esq.Items.Clear();

                    // Loop através dos resultados e preencher o ListBox
                    while (reader.Read())
                    {
                        // Obter o valor da coluna da consulta
                        string Cab = reader.GetString(0);

                        // Adicionar o valor ao ListBox
                        listBox_modelos_cab_esq.Items.Add(Cab);
                    }

                    reader.Close();
                }
            }

        }

        private void Carregar_Modelos_Cab_Dir(string Modelo)
        {
            if (Modelo.Contains("-"))
            {
                // Limpar itens existentes no ListBox
                listBox_modelos_cab_dir.Items.Clear();

                // Adicionar o valor ao ListBox
                listBox_modelos_cab_dir.Items.Add(Modelo);
            }
            else
            {
                string query = "SELECT distinct Modelo FROM Modelos_Eng LEFT JOIN Cabeceira_Ligacao_Eng ON Modelos_Eng.Modelo = Cabeceira_Ligacao_Eng.Expositor WHERE Modelos_Eng.basecodigo = 'Eletrofrio' and Modelos_Eng.Modelo like '" + Modelo + "-%'";
                // Configuração da conexão com o banco de dados
                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

                // Criação da conexão com o banco de dados e execução da consulta
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    //command.Parameters.AddWithValue("@cabeceira", cabeceira);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Limpar itens existentes no ListBox
                    listBox_modelos_cab_dir.Items.Clear();

                    // Loop através dos resultados e preencher o ListBox
                    while (reader.Read())
                    {
                        // Obter o valor da coluna da consulta
                        string Cab = reader.GetString(0);

                        // Adicionar o valor ao ListBox
                        listBox_modelos_cab_dir.Items.Add(Cab);
                    }

                    reader.Close();
                }
            }

        }

        private void Carregar_Modelos_Cab(string Modelo)
        {
            string query = "";

            if (!Modelo.Contains("-"))
            {
                query = "SELECT Modelo FROM Modelos_Eng LEFT JOIN Cabeceira_Ligacao_Eng ON Modelos_Eng.Modelo = Cabeceira_Ligacao_Eng.Expositor WHERE Modelos_Eng.basecodigo = 'Eletrofrio' and Cabeceira_Ligacao_Eng.Expositor IS NULL and Modelos_Eng.Modelo like '" + Modelo + "-%'  and Linha = '" + linhaModelo + "'";
                // Configuração da conexão com o banco de dados
                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

                // Criação da conexão com o banco de dados e execução da consulta
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    //command.Parameters.AddWithValue("@cabeceira", cabeceira);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Limpar itens existentes no ListBox
                    listBox_modelos_cab.Items.Clear();

                    // Loop através dos resultados e preencher o ListBox
                    while (reader.Read())
                    {
                        // Obter o valor da coluna da consulta
                        string Cab = reader.GetString(0);

                        // Adicionar o valor ao ListBox
                        listBox_modelos_cab.Items.Add(Cab);
                    }

                    reader.Close();
                }
            }
        }

        private void lista_acessorios_cabeceira(string Cabeceira)
        {

            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            //string query = "select Acessorios from Cabeceira_Acessorios_Eng where Cabeceira = @Cabeceira and BaseCodigo = 'Eletrofrio'";
            string query = "SELECT DISTINCT \n" +
                            //"CAE.cabeceira, \n" +
                            "CAE.Acessorios, \n" +
                            "PE.Cores, \n" +
                            "COALESCE(PE.M125, PE.M150, PE.M187, PE.M225, PE.M250, PE.M300, PE.M375, PE.E45, PE.E90, PE.I45, PE.I90, PE.T1875, PE.T2167, PE.T2500, PE.T360) AS Codigo \n" +
                        "FROM Cabeceira_Acessorios_Eng CAE \n" +
                        "INNER JOIN Cabeceira_Ligacao_Eng CL ON CAE.cabeceira = CL.cabeceira \n" +
                        "INNER JOIN Planilhas_Eng PE ON CL.Expositor = PE.Modelo AND CAE.Acessorios = PE.Componente \n" +
                        "WHERE CAE.BaseCodigo = 'Eletrofrio' AND CAE.Cabeceira = @Cabeceira;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                //dataTable.Clear();
                command.Parameters.AddWithValue("@Cabeceira", Cabeceira);
                connection.Open();
                adapter.Fill(dataTable);

                dataGridView_Acessorio_Cabeceira.DataSource = dataTable;
            }
            dataGridView_Acessorio_Cabeceira.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn column in dataGridView_Acessorio_Cabeceira.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.FillWeight = 1;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl8.SelectedTab = tabPage_Modelos;
            Modelo1 = comboBox_modelo1_parte2.Text;
            if (Tipo == "Ajuste")
            {
                textBox_Nome.Text = $"Cab {Tipo} {Modelo1}|{Modelo2}";
            }
            else
            {
                textBox_Nome.Text = $"Cab {Tipo} {Lado} {Modelo1}|{Modelo2}";
            }

            comboBox_Modelo2_Parte2.Enabled = true;
            Carregar_Modelos_Cab_Esq(Modelo1);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl8.SelectedTab = tabPage_Modelos;
            if (comboBox_modelo1_parte2.Text != "")
            {

                Modelo2 = comboBox_Modelo2_Parte2.Text;
                textBox_Nome.Text = "Cab " + Tipo + " " + Lado + " " + Modelo1 + "|" + Modelo2;

                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                string query1 = "SELECT MAX(Espessura_Cab) AS espessura_Cab FROM Modelos_Eng INNER JOIN Modelo_Regime_Eng ON Modelos_Eng.Resfriamento = Modelo_Regime_Eng.Regime WHERE Modelo_Regime_Eng.basecodigo = 'Eletrofrio' and (Modelo LIKE '" + Modelo1 + "%' or Modelo like '" + Modelo2 + "%')";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query1, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string nome = reader["espessura_Cab"].ToString();
                        textBox_Espessura2.Text = nome;
                    }
                    reader.Close();
                }
                Carregar_Modelos_Cab_Dir(Modelo2);
            }
        }

        private void comboBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Cancela o evento de tecla pressionada

            // Verifica se a tecla pressionada é uma tecla de exclusão
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete)
            {
                // Nenhuma ação é realizada ao pressionar uma tecla de exclusão
                return;
            }

            // Restaura o caractere digitado pelo usuário no ComboBox
            ComboBox comboBox = (ComboBox)sender;
            comboBox.Text += e.KeyChar.ToString();
        }

        private void LogCabeceira(string modelo, string origem, string para, string observacao)
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = "insert into Log_eng values (" +
                $"'{login}','Cabeceiras','{modelo}','{origem}','{para}','{observacao}','{dataAtual}',NULL,NULL)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }





        private void NotificacaoCabeceiraAlteracao(string cabeceira)
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

            string scriptSQL = $"IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Temp2') BEGIN \n" +
                $"CREATE TABLE Temp2( \n" +
                                            "Cabeceira nvarchar(255) NULL, \n" +
                                            "Codigo nvarchar(255) NULL, \n" +
                                            "Esq_Dir nvarchar(5) NULL, \n" +
                                            "Expositor nvarchar(255) NULL, \n" +
                                            "Acessorios nvarchar(255) NULL) END \n" +
                                        "insert into Temp2 \n" +
                                        "SELECT \n" +
                                            "Cabeceira_Eng.Cabeceira,  \n" +
                                            "Cabeceira_Eng.Codigo,  \n" +
                                            "Cabeceira_Ligacao_Eng.Esq_Dir,  \n" +
                                            "Cabeceira_Ligacao_Eng.Expositor,  \n" +
                                            "Cabeceira_Acessorios_Eng.Acessorios \n" +
                                        "FROM Cabeceira_Eng \n" +
                                            "LEFT JOIN Cabeceira_Acessorios_Eng ON Cabeceira_Eng.Cabeceira = Cabeceira_Acessorios_Eng.Cabeceira \n" +
                                            "LEFT JOIN Cabeceira_Ligacao_Eng ON Cabeceira_Eng.Cabeceira = Cabeceira_Ligacao_Eng.Cabeceira \n" +
                                        $"where Cabeceira_Eng.Cabeceira = '{cabeceira}'";

            string scriptSQL2 = "WITH CTE AS ( \n" +
                                "SELECT Cabeceira, Codigo, Esq_Dir, Expositor, Acessorios, \n" +
                                "ROW_NUMBER() OVER(PARTITION BY Cabeceira, Codigo, Esq_Dir, Expositor, Acessorios ORDER BY(SELECT 0)) AS RowNum \n" +
                                "FROM Temp2) \n" +
                                "delete FROM CTE WHERE RowNum > 1; ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand(scriptSQL, connection);
                SqlCommand command2 = new SqlCommand(scriptSQL2, connection);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                connection.Close();
            }
        }



        private void NotificacaoCabeceiraDeletar(string cabeceira)
        {
            // Consulta SQL para obter os valores de Expositor e Esq_Dir
            string consultaSql = $"SELECT Expositor, Esq_Dir FROM Cabeceira_Ligacao_Eng WHERE Cabeceira = '{cabeceira}'";

            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Lista para armazenar os resultados da primeira consulta
                List<Tuple<string, string>> resultados = new List<Tuple<string, string>>();

                // Executar a consulta para obter os valores de Expositor e Esq_Dir
                using (SqlCommand commandConsulta = new SqlCommand(consultaSql, connection))
                using (SqlDataReader reader = commandConsulta.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Obter os valores de Expositor e Esq_Dir para cada linha
                        string expositor = reader["Expositor"].ToString();
                        string ladoCab = reader["Esq_Dir"].ToString();

                        resultados.Add(new Tuple<string, string>(expositor, ladoCab));
                    }
                }

                // Fechar o leitor de dados antes de executar a próxima consulta
                connection.Close();

                // Abrir novamente a conexão antes de inserir os dados
                connection.Open();

                // Executar a consulta final para cada linha
                foreach (var resultado in resultados)
                {
                    string query_status = $"INSERT INTO Notificacao VALUES ('{login}', '{resultado.Item1}', '{cabeceira},{resultado.Item2},{resultado.Item1}','','Deletar cabeceira','{dataAtual}',NULL,NULL)";

                    using (SqlCommand command1 = new SqlCommand(query_status, connection))
                    {
                        command1.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
        }



        private void NotificacaoCabeceiraNew(string cabeceira, string Codigo, string Esq_Dir, string Expositor, string Acessorios, string Status)
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            //string status = VerificarStatus();
            //DropTempTable();
            if (Status == "ALTERACAO")
            {
                string query_alter = "IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Temp1') BEGIN \n" +
                    "CREATE TABLE [dbo].[Temp1]( \n" +
                               "[Cabeceira][nvarchar](255) NULL, \n" +
                               "[Codigo][nvarchar] (255) NULL, \n" +
                               "[Esq_Dir][nvarchar] (5) NULL, \n" +
                               "[Expositor][nvarchar] (255) NULL, \n" +
                               "[Acessorios][nvarchar] (255) NULL, \n" +
                               "[ID][int] IDENTITY(1, 1) NOT NULL," +
                               "[Status][nvarchar] (255) NULL, \n" +
                           "PRIMARY KEY CLUSTERED \n" +
                           "( \n" +
                               "[ID] ASC \n" +
                           ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] \n" +
                           ") ON[PRIMARY] END";

                string query_alter2 = $"insert into Temp1 values ('{cabeceira}','{Codigo}','{Esq_Dir}','{Expositor}','{Acessorios}','{Status}');";

                //string queryNotification = $"";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command1 = new SqlCommand(query_alter, connection);
                    SqlCommand command2 = new SqlCommand(query_alter2, connection);
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    connection.Close();
                }
                // DropTempTables();
                return;
            }


            string query1 = @"
                    IF OBJECT_ID('Temp1', 'U') IS NULL
                    BEGIN
                        CREATE TABLE [dbo].[Temp1]( 
                            [Cabeceira][nvarchar](255) NULL, 
                            [Codigo][nvarchar] (255) NULL, 
                            [Esq_Dir][nvarchar] (5) NULL, 
                            [Expositor][nvarchar] (255) NULL, 
                            [Acessorios][nvarchar] (255) NULL, 
                            [ID][int] IDENTITY(1, 1) NOT NULL,
                            [Status][nvarchar] (255) NULL, 
                            PRIMARY KEY CLUSTERED 
                            ( 
                                [ID] ASC 
                            ) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] 
                        )
                    END
                ";

            string query2 = $"insert into Temp1 values ('{cabeceira}','{Codigo}','{Esq_Dir}','{Expositor}','{Acessorios}','{Status}');";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand(query1, connection);
                SqlCommand command2 = new SqlCommand(query2, connection);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                connection.Close();
            }

            if (Status == "NOVO")
            {
                string query_status = $"insert into Notificacao values ('{login}','{Expositor}','',(select Cabeceira+','+Codigo+','+Esq_Dir+','+Expositor+','+Acessorios+','+Status from Temp1),'Nova cabeceira','{dataAtual}',NULL,NULL)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command1 = new SqlCommand(query_status, connection);
                    command1.ExecuteNonQuery();
                    connection.Close();
                }
            }
            DropTempTables();
        }

        private void DropTempTables()
        {
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query_status = @"
                    IF OBJECT_ID('Temp1', 'U') IS NOT NULL
                        DROP TABLE Temp1;
                    IF OBJECT_ID('Temp2', 'U') IS NOT NULL
                        DROP TABLE Temp2;
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand(query_status, connection);
                command1.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void VerificarAcessoriosCabeceiraCriado()
        {
            // 1. Obtenha os itens do ListBox em uma lista
            HashSet<string> acessorios = new HashSet<string>();
            HashSet<string> modelos = new HashSet<string>();
            List<string> modelosExistente = new List<string>();

            foreach (var item in listBox2.Items)
            {
                acessorios.Add(item.ToString());
            }

            foreach (var item in listBox_modelos_cab_esq.Items)
            {
                modelos.Add(item.ToString());
            }

            foreach (var item in listBox_modelos_cab_dir.Items)
            {
                modelos.Add(item.ToString());
            }

            // 2. Conecte-se ao banco de dados
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // 3. Para cada modelo na lista, execute a consulta SQL para verificar se o modelo já existe
                foreach (string modelo in modelos)
                {
                    // Converta o conjunto de acessórios para uma string formatada para usar na cláusula IN da consulta SQL
                    string acessoriosString = string.Join("','", acessorios);

                    string consultaSQL = $"SELECT TOP 1 Modelo FROM Planilhas_Eng WHERE Modelo = '{modelo}' AND Acessarios_CAB = 1 and Componente IN ('{acessoriosString}')";

                    using (SqlCommand commandExistencia = new SqlCommand(consultaSQL, connection))
                    {
                        object modeloExistente = commandExistencia.ExecuteScalar();

                        if (modeloExistente != null && modeloExistente != DBNull.Value)
                        {
                            // O modelo já existe, adicione à lista de modelos existentes
                            modelosExistente.Add(modeloExistente.ToString());
                        }
                    }
                }

                // 4. Para cada modelo na lista, execute o INSERT se não existir
                foreach (string modelo in modelos)
                {
                    // Converta o conjunto de acessórios para uma string formatada para usar na cláusula IN da consulta SQL
                    string acessoriosString = string.Join("','", acessorios);
                    string modelosExistenteString = string.Join("','", modelosExistente);

                    // Verifica se o modelo já existe
                    if (!modelosExistente.Contains(modelo))
                    {
                        string insertSQL = $"INSERT INTO Planilhas_Eng (Modelo, Componente, Cores, M125, M150, M187, M225, M250, M300, M375, E45, E90, I45, I90, T1875, T2167, T2500, T360, Observacao, LiberarRepresentante, Acessarios_CAB, BaseCodigo) " +
                                           $"SELECT '{modelo}', '{acessoriosString}', Cores, M125, M150, M187, M225, M250, M300, M375, E45, E90, I45, I90, T1875, T2167, T2500, T360, Observacao, LiberarRepresentante, Acessarios_CAB, BaseCodigo " +
                                           $"FROM Planilhas_Eng WHERE Modelo = '{modelosExistenteString}' AND Acessarios_CAB = 1 and Componente IN ('{acessoriosString}')";

                        using (SqlCommand insertCommand = new SqlCommand(insertSQL, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }




        private void button_Salvar_Click(object sender, EventArgs e)
        {
            DropTempTables();
            VerificarAcessoriosCabeceiraCriado();
            string grupoModelo = "";
            string cab = "";
            if (textBox_Nome_Visualizacao.Text != "")
            {
                cab = textBox_Nome_Visualizacao.Text;
            }
            else
            {
                cab = textBox_Nome.Text;
            }
            NotificacaoCabeceiraAlteracao(cab);

            if (tableLayoutPanel10.Visible == true)
            {
                string Cabeceira = textBox_Nome.Text;
                string grupoCor = null;
                string espessura = null;
                string Lado_cab = "";
                int ajuste;
                string profundidade = null;
                string codigo = null;
                string Grupo_Modelo = "";
                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                string query_Cabeceira_Eng = "INSERT INTO Cabeceira_Eng (Cabeceira,Grupo_Cor,Espessura,Ajuste,Profundidade,Codigo,Grupo_Modelo,Lado_Cab,Observacao,BaseCodigo" +
                    ") VALUES (@Cabeceira, @Grupo_Cor, @Espessura,@Ajuste,@Profundidade,@Codigo,@Grupo_Modelo,@Lado_Cab,@Observacao,'Eletrofrio')";



                if (comboBox_Tipo.Text != "")
                {

                    if (comboBox_Tipo.Text == "Ajuste")
                    {
                        ajuste = 1;
                        Grupo_Modelo = comboBox_modelo1_parte2.Text + "|" + comboBox_Modelo2_Parte2.Text;
                        CheckBoxAjusteSelecionado();



                        if (comboBox_modelo1_parte2.Text != "" && comboBox_Modelo2_Parte2.Text != "")
                        {
                            if (comboBox_grupoCor2.SelectedItem != null && comboBox_grupoCor2.SelectedItem.ToString() != "")
                            {
                                grupoCor = comboBox_grupoCor2.SelectedItem.ToString();
                                if (comboBox_Profundidade2.Text != "")
                                {
                                    profundidade = comboBox_Profundidade2.Text;
                                    if (textBox_Espessura2.Text != "")
                                    {
                                        espessura = textBox_Espessura2.Text;
                                        if (textBox_Codigo2.Text != "")
                                        {
                                            if (listBox_modelos_cab_dir.Items.Count != 0 && listBox_modelos_cab_esq.Items.Count != 0)
                                            {
                                                codigo = textBox_Codigo2.Text;
                                                string para = $"'{Cabeceira}','{codigo}','A'";
                                                LogCabeceira(Grupo_Modelo, "", para, "Criando cabeceira ajuste");
                                                Inserir_Cabeceira_Acessorios_Eng();
                                                Inserir_Cabeceira_Ligacao_Eng();
                                                foreach (object expositor in listBox_modelos_cab_esq.Items)
                                                {
                                                    foreach (object acessorio in listBox2.Items)
                                                    {
                                                        NotificacaoCabeceiraNew(Cabeceira, codigo, "E", expositor.ToString(), acessorio.ToString(), "NOVO");
                                                    }
                                                }
                                                foreach (object expositor in listBox_modelos_cab_dir.Items)
                                                {
                                                    foreach (object acessorio in listBox2.Items)
                                                    {
                                                        NotificacaoCabeceiraNew(Cabeceira, codigo, "D", expositor.ToString(), acessorio.ToString(), "NOVO");
                                                    }
                                                }
                                                //NotificacaoCabeceiraNew(Cabeceira, codigo, espessura, profundidade, grupoCor, Lado_cab,)
                                                using (SqlConnection connection = new SqlConnection(connectionString))
                                                {
                                                    SqlCommand command = new SqlCommand(query_Cabeceira_Eng, connection);
                                                    command.Parameters.AddWithValue("@Cabeceira", Cabeceira);
                                                    command.Parameters.AddWithValue("@Grupo_Cor", grupoCor);
                                                    command.Parameters.AddWithValue("@Espessura", espessura);
                                                    command.Parameters.AddWithValue("@Ajuste", ajuste);
                                                    command.Parameters.AddWithValue("@Profundidade", profundidade);
                                                    command.Parameters.AddWithValue("@Codigo", codigo);
                                                    command.Parameters.AddWithValue("@Grupo_Modelo", Grupo_Modelo);
                                                    command.Parameters.AddWithValue("@Lado_cab", Lado_cab);
                                                    command.Parameters.AddWithValue("@Observacao", textBox_Observacao.Text);
                                                    connection.Open();
                                                    command.ExecuteNonQuery();
                                                    Carregar_Lista_Cabeceira();
                                                    MessageBox.Show("Cabeceira cadastrada!");
                                                    NovaCab();
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Selecionar o lado da cabeceira!");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Informar o código da cabeceira!");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Informar a espessura da cabeceira!");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Informar a profundidade da cabeceira!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Grupo de cor de cabeceira está vazio!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Campo modelo está vazio!");
                        }
                    }
                    else
                    {
                        ajuste = 0;
                    }
                    if (checkBox_dir.Checked || checkBox_esq.Checked)
                    {
                        if (checkBox_dir.Checked)
                        {
                            CheckBoxDirSelecionado();
                            Lado_cab = "D";
                        }
                        if (checkBox_esq.Checked)
                        {
                            CheckBoxEsqSelecionado();
                            Lado_cab = "E";
                        }
                        if (comboBox_Modelo_parte1.Text != "")
                        {
                            if (comboBox_grupoCor1.SelectedItem != null && comboBox_grupoCor1.SelectedItem.ToString() != "")
                            {
                                grupoCor = comboBox_grupoCor1.SelectedItem.ToString();
                                if (comboBox_profundidade1.Text != "")
                                {
                                    profundidade = comboBox_profundidade1.Text;
                                    if (textBox_Espessura1.Text != "")
                                    {
                                        espessura = textBox_Espessura1.Text;
                                        if (textBox_Codigo.Text != "")
                                        {
                                            codigo = textBox_Codigo.Text;
                                            if (listBox_modelos_cab_dir.Items.Count != 0 || listBox_modelos_cab_esq.Items.Count != 0)
                                            {
                                                LogCabeceira(grupoModelo, $"Cabeceira {Cabeceira} alterada", $"Cabeceira {Cabeceira} alterada", "Cabeceira alterada");
                                                Inserir_Cabeceira_Acessorios_Eng();
                                                Inserir_Cabeceira_Ligacao_Eng();
                                                foreach (object expositor in listBox_modelos_cab_esq.Items)
                                                {
                                                    if (listBox2.Items.Count != 0)
                                                    {
                                                        foreach (object acessorio in listBox2.Items)
                                                        {
                                                            NotificacaoCabeceiraNew(Cabeceira, codigo, "E", expositor.ToString(), acessorio.ToString(), "NOVO");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        NotificacaoCabeceiraNew(Cabeceira, codigo, "E", expositor.ToString(), "", "NOVO");
                                                    }

                                                }
                                                foreach (object expositor in listBox_modelos_cab_dir.Items)
                                                {
                                                    if (listBox2.Items.Count != 0)
                                                    {
                                                        foreach (object acessorio in listBox2.Items)
                                                        {
                                                            NotificacaoCabeceiraNew(Cabeceira, codigo, "D", expositor.ToString(), acessorio.ToString(), "NOVO");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        NotificacaoCabeceiraNew(Cabeceira, codigo, "D", expositor.ToString(), "", "NOVO");
                                                    }
                                                }
                                                using (SqlConnection connection = new SqlConnection(connectionString))
                                                {
                                                    SqlCommand command = new SqlCommand(query_Cabeceira_Eng, connection);
                                                    command.Parameters.AddWithValue("@Cabeceira", Cabeceira);
                                                    command.Parameters.AddWithValue("@Grupo_Cor", grupoCor);
                                                    command.Parameters.AddWithValue("@Espessura", espessura);
                                                    command.Parameters.AddWithValue("@Ajuste", ajuste);
                                                    command.Parameters.AddWithValue("@Profundidade", profundidade);
                                                    command.Parameters.AddWithValue("@Codigo", codigo);
                                                    command.Parameters.AddWithValue("@Grupo_Modelo", comboBox_Modelo_parte1.Text);
                                                    command.Parameters.AddWithValue("@Lado_Cab", Lado_cab);
                                                    command.Parameters.AddWithValue("@Observacao", textBox_Observacao.Text);
                                                    connection.Open();
                                                    command.ExecuteNonQuery();
                                                    Carregar_Lista_Cabeceira();
                                                    MessageBox.Show("Cabeceira cadastrada!");
                                                    NovaCab();
                                                    return;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Informar o código da cabeceira!");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Informar a espessura da cabeceira!");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Informar a profundidade da cabeceira!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Grupo de cor de cabeceira está vazio!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Campo modelo está vazio!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecionar um lado para cabeceira!");
                    }
                }
                else
                {
                    MessageBox.Show("O campo tipo não pode estar vazio!");
                }
                foreach (string item in listBox_modelos_cab_dir.Items)
                {
                    foreach (DataGridViewRow row in dataGridView_Acessorio_Cabeceira.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            var acessorios = row.Cells["Acessorios"].Value.ToString();
                            //NotificacaoCabeceiraAlteracao(textBox_Nome.Text, item);
                            NotificacaoCabeceiraNew(textBox_Nome.Text, codigo, Lado_cab, item, acessorios, "ALTERACAO");
                        }
                    }

                }
            }
            if (tableLayoutPanel12.Visible == true)
            {
                string nome = textBox_Nome_Visualizacao.Text;
                string grupoCor = comboBox_GrupoCor_Visualizacao.Text;
                int profundidade = Convert.ToInt32(comboBox_profundidade3.Text);
                int espessura = Convert.ToInt32(textBox_Espessura_Visualizacao.Text);
                string codigo = textBox_Codigo_Visualizacao.Text;
                int ID = Convert.ToInt32(textBox_ID.Text);

                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                string query1 = "WITH CTE AS ( \n" +
                                    "select t1.ID as t1_ID,t3.ID as t3_ID \n" +
                                    "from Cabeceira_Eng t1 \n" +
                                    "inner join Cabeceira_Ligacao_Eng t3 on t1.Cabeceira = t3.Cabeceira \n" +
                                    "where t1.ID = @ID)  \n" +
                                    "delete Cabeceira_Ligacao_Eng WHERE ID IN(SELECT t3_ID FROM CTE) and Esq_Dir like @Lado;";
                string query5 = "insert into Cabeceira_Ligacao_Eng (Cabeceira,Esq_Dir,Expositor,Travar_representante,BaseCodigo) values (@Cabeceira,@Esq_Dir,@Expositor,@Travar_representante,'Eletrofrio')";
                string query2 = "";
                string query4 = "";

                if (dataGridView_Acessorio_Cabeceira.RowCount > 1 && listBox2.Items.Count > 0)
                {
                    query2 = "WITH CTE2 AS( \n" +
                                   "select t1.ID as t1_ID, t2.ID as t2_ID \n" +
                                   "from Cabeceira_Eng t1 \n" +
                                   "inner join Cabeceira_Acessorios_Eng t2 on t1.Cabeceira = t2.Cabeceira \n" +
                                   "where t1.ID = @ID)  \n" +
                                   "delete from Cabeceira_Acessorios_Eng WHERE ID IN(SELECT t2_ID FROM CTE2);";
                    query4 = "insert into Cabeceira_Acessorios_Eng (Cabeceira,Acessorios,BaseCodigo) values (@Cabeceira,@Acessorios,'Eletrofrio')";

                }
                if (dataGridView_Acessorio_Cabeceira.RowCount == 1 && listBox2.Items.Count > 0)
                {
                    query2 = "insert into Cabeceira_Acessorios_Eng (Cabeceira,Acessorios,BaseCodigo) values (@Cabeceira,@Acessorios,'Eletrofrio')";
                }
                if (dataGridView_Acessorio_Cabeceira.RowCount > 1 && listBox2.Items.Count == 0)
                {
                    query2 = "delete t1 from Cabeceira_Acessorios_Eng t1 \n" +
                                "inner join Cabeceira_Eng t2 on t2.Cabeceira = t1.Cabeceira where t2.ID = @ID";
                }

                dataGridView_Acessorio_Cabeceira.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                string query3 = "UPDATE Cabeceira_Eng SET Cabeceira = @Cabeceira, Grupo_Cor = @Grupo_Cor, Espessura = @Espessura, Profundidade = @Profundidade, Codigo = @Codigo, Observacao = @Observacao, BaseCodigo = 'Eletrofrio' WHERE ID = @ID";

                if (listBox_modelos_cab_dir.Items.Count > 0)
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query1, connection);
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@Lado", "D");
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        foreach (var item in listBox_modelos_cab_dir.Items)
                        {
                            SqlCommand command = new SqlCommand(query5, connection);
                            command.Parameters.AddWithValue("@Cabeceira", nome);
                            command.Parameters.AddWithValue("@Esq_Dir", "D");
                            command.Parameters.AddWithValue("@Expositor", item);
                            command.Parameters.AddWithValue("@Travar_representante", "0");
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    Verificar_Modelos_Disponiveis_Cab();
                }
                if (listBox_modelos_cab_esq.Items.Count > 0)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query1, connection);
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@Lado", "E");
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        foreach (var item in listBox_modelos_cab_esq.Items)
                        {
                            SqlCommand command = new SqlCommand(query5, connection);
                            command.Parameters.AddWithValue("@Cabeceira", nome);
                            command.Parameters.AddWithValue("@Esq_Dir", "E");
                            command.Parameters.AddWithValue("@Expositor", item);
                            command.Parameters.AddWithValue("@Travar_representante", "0");
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
                if (listBox_modelos_cab_esq.Items.Count == 0 && listBox_modelos_cab_dir.Items.Count == 0)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query1, connection);
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@Lado", "%");
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                if (query2 != "")
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        if (listBox2.Items.Count > 0)
                        {
                            foreach (var item in listBox2.Items)
                            {
                                SqlCommand command = new SqlCommand(query2, connection);
                                command.Parameters.AddWithValue("@Cabeceira", nome);
                                command.Parameters.AddWithValue("@Acessorios", item);
                                command.Parameters.AddWithValue("@ID", ID);
                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }
                        else
                        {
                            SqlCommand command = new SqlCommand(query2, connection);
                            command.Parameters.AddWithValue("@ID", ID);
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                        }

                    }

                    if (query4 != "")
                    {
                        if (listBox2.Items.Count > 0)
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                foreach (var item in listBox2.Items)
                                {
                                    SqlCommand command = new SqlCommand(query4, connection);
                                    command.Parameters.AddWithValue("@Cabeceira", nome);
                                    command.Parameters.AddWithValue("@Acessorios", item);
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                    connection.Close();
                                }
                            }
                        }
                    }
                }

                if (listBox_modelos_cab_esq.Items.Count == 0 && listBox_modelos_cab_dir.Items.Count == 0)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Consulta para obter os dados de Temp2
                        string consultaTemp2 = "SELECT [Cabeceira], [Codigo], [Esq_Dir], [Expositor], [Acessorios] FROM [BANCODEDADOS].[dbo].[Temp2]";

                        using (SqlCommand command = new SqlCommand(consultaTemp2, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    // Para cada linha, insira na tabela Notificacao
                                    string resultado = $"{reader["Cabeceira"]},{reader["Codigo"]},{reader["Esq_Dir"]},{reader["Expositor"]},{reader["Acessorios"]}";
                                    string expositor = $"{reader["Expositor"]}";
                                    InserirNotificacao(resultado, expositor, login);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (object expositor in listBox_modelos_cab_esq.Items)
                    {
                        if (listBox2.Items.Count > 0)
                        {
                            foreach (object acessorio in listBox2.Items)
                            {
                                //NotificacaoCabeceiraAlteracao(nome, expositor.ToString());
                                NotificacaoCabeceiraNew(nome, codigo, "E", expositor.ToString(), acessorio.ToString(), "ALTERACAO");
                            }
                        }
                        else
                        {
                            //NotificacaoCabeceiraAlteracao(nome, expositor.ToString());
                            NotificacaoCabeceiraNew(nome, codigo, "E", expositor.ToString(), "", "ALTERACAO");
                        }
                    }
                    foreach (object expositor in listBox_modelos_cab_dir.Items)
                    {
                        if (listBox2.Items.Count > 0)
                        {
                            foreach (object acessorio in listBox2.Items)
                            {
                                //NotificacaoCabeceiraAlteracao(nome, expositor.ToString());
                                NotificacaoCabeceiraNew(nome, codigo, "D", expositor.ToString(), acessorio.ToString(), "ALTERACAO");
                            }
                        }
                        else
                        {
                            //NotificacaoCabeceiraAlteracao(nome, expositor.ToString());
                            NotificacaoCabeceiraNew(nome, codigo, "D", expositor.ToString(), "", "ALTERACAO");
                        }
                    }
                    insertCabNotificacao();
                }




                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query3, connection);
                    command.Parameters.AddWithValue("@Cabeceira", nome);
                    command.Parameters.AddWithValue("@Grupo_Cor", grupoCor);
                    command.Parameters.AddWithValue("@Espessura", espessura);
                    command.Parameters.AddWithValue("@Profundidade", profundidade);
                    command.Parameters.AddWithValue("@Codigo", codigo);
                    command.Parameters.AddWithValue("@ID", ID);
                    command.Parameters.AddWithValue("@Observacao", textBox_Observacao.Text);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                Carregar_Lista_Cabeceira();
                //insertCabNotificacao();
                NovaCab();
                LogCabeceira(grupoModelo, $"Cabeceira {nome} alterada", $"Cabeceira {nome} alterada", "Cabeceira alterada");
                MessageBox.Show("Executado com sucesso!");


            }

        }

        private void InserirNotificacao(string resultado, string expositor, string login)
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

            // Inserir na tabela Notificacao
            string inserirQuery = $"INSERT INTO Notificacao VALUES (@login, @Expositor, @resultado, '', 'Alterar cabeceira', '{dataAtual}', NULL, NULL)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand(inserirQuery, connection);
                command1.Parameters.AddWithValue("@login", login);
                command1.Parameters.AddWithValue("@Expositor", expositor);
                command1.Parameters.AddWithValue("@resultado", resultado);
                command1.ExecuteNonQuery();
                connection.Close();
            }


        }

        private void insertCabNotificacao()
        {
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query_status = $"create table #tabelaTemporaria ( \n" +
                                    "Usuario nvarchar(255), \n" +
                                    "Expositor nvarchar(255), \n" +
                                    "De_cabeceira nvarchar(255), \n" +
                                    "Para_cabeceira nvarchar(255), \n" +
                                    "info_cabeceira nvarchar(255), \n" +
                                    "Data date, \n" +
                                    "Data_verificado date, \n" +
                                    "Verificador_por nvarchar(255)); \n" +
                                            "insert into #tabelaTemporaria \n" +
                                        $"select '{login}',COALESCE(Temp1.Expositor, Temp2.Expositor) AS Expositor, \n" +
                                        "Temp2.Cabeceira + ',' + \n" +
                                        "Temp2.Codigo + ',' + \n" +
                                        "Temp2.Esq_Dir + ',' + \n" +
                                        "Temp2.Expositor + ',' + \n" +
                                        "Temp2.Acessorios, \n" +
                                        "Temp1.Cabeceira + ',' + \n" +
                                        "Temp1.Codigo + ',' + \n" +
                                        "Temp1.Esq_Dir + ',' + \n" +
                                        "Temp1.Expositor + ',' + \n" +
                                        "Temp1.Acessorios, \n" +
                                        "'Alterar cabeceira', \n" +
                                        $"'{dataAtual}', \n" +
                                        "NULL, \n" +
                                        "NULL \n" +
                                    "from Temp1 \n" +
                                    "FULL JOIN Temp2 ON  \n" +
                                    "(Temp1.Cabeceira = Temp2.Cabeceira OR(Temp1.Cabeceira IS NULL AND Temp2.Cabeceira IS NULL)) \n" +
                                    "AND(Temp1.Expositor = Temp2.Expositor OR(Temp1.Expositor IS NULL AND Temp2.Expositor IS NULL)) \n" +
                                    "AND(Temp1.Acessorios = Temp2.Acessorios OR(Temp1.Acessorios IS NULL AND Temp2.Acessorios IS NULL)) \n" +
                                    "WHERE Temp2.Cabeceira + ',' + Temp2.Codigo + ',' + Temp2.Esq_Dir + ',' + Temp2.Expositor + ',' + Temp2.Acessorios \n" +
                                    "<> Temp1.Cabeceira + ',' + Temp1.Codigo + ',' + Temp1.Esq_Dir + ',' + Temp1.Expositor + ',' + Temp1.Acessorios \n" +
                                    "OR(Temp1.Cabeceira IS NULL AND Temp2.Cabeceira IS NOT NULL) \n" +
                                    "OR(Temp1.Cabeceira IS NOT NULL AND Temp2.Cabeceira IS NULL) \n" +
                                    "Insert into Notificacao\n" +
                                    "select* from #tabelaTemporaria where Expositor is not null;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand(query_status, connection);
                command1.ExecuteNonQuery();
                connection.Close();
            }
            DropTempTables();
        }

        private void Verificar_Modelos_Disponiveis_Cab()
        {
            if (dataGridView_ListaCab.SelectedRows.Count > 0)
            {
                listBox_modelos_cab.Items.Clear();
                string Grupo_Modelo_Selecionado = dataGridView_ListaCab.SelectedRows[0].Cells["Grupo_Modelo"].Value.ToString();
                string Cabeceira_Selecionado = dataGridView_ListaCab.SelectedRows[0].Cells["Cabeceira"].Value.ToString();

                if (!Grupo_Modelo_Selecionado.Contains("|"))
                {
                    string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                    string query = "SELECT DISTINCT Modelos_Eng.Modelo \n" +
                                    "FROM Modelos_Eng \n" +
                                    "LEFT JOIN( \n" +
                                        "select \n" +
                                            "Cabeceira_Eng.Cabeceira, \n" +
                                            "Cabeceira_Eng.Grupo_Modelo, \n" +
                                            "Cabeceira_Ligacao_Eng.Esq_Dir, \n" +
                                            "Cabeceira_Ligacao_Eng.Expositor \n" +
                                        "from Cabeceira_Eng \n" +
                                        "inner join Cabeceira_Ligacao_Eng on Cabeceira_Eng.Cabeceira = Cabeceira_Ligacao_Eng.Cabeceira \n" +
                                        "where Cabeceira_Eng.Cabeceira = '" + Cabeceira_Selecionado + "' and Cabeceira_Eng.BaseCodigo = 'Eletrofrio' \n" +
                                    ") AS Consulta1 \n" +
                                    "ON Modelos_Eng.Modelo = Consulta1.Expositor \n" +
                                    "WHERE Consulta1.Expositor IS NULL AND Modelos_Eng.Modelo LIKE '" + Grupo_Modelo_Selecionado + "-%' and Modelos_Eng.basecodigo = 'Eletrofrio'  and Linha = '" + linhaModelo + "'";


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string nome = reader["Modelo"].ToString();
                            listBox_modelos_cab.Items.Add(nome);

                        }
                        reader.Close();
                    }
                }
                if (Grupo_Modelo_Selecionado.Contains("|"))
                {
                    string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

                    string query = "create table #temp1 (Cabeceira nvarchar(200),Grupo_Modelo nvarchar(200),Grupo_Modelo_Esq nvarchar (100),Grupo_Modelo_Dir nvarchar (100),Esq_Dir nvarchar(5)) \n" +
                                    "insert into #temp1 \n" +
                                    "select \n" +
                                    "Cabeceira_Eng.Cabeceira, \n" +
                                    "Cabeceira_Eng.Grupo_Modelo, \n" +
                                    "LEFT(Cabeceira_Eng.Grupo_Modelo, CHARINDEX('|', Cabeceira_Eng.Grupo_Modelo + '|') - 1) AS Grupo_Modelo_Esq, \n" +
                                    "SUBSTRING(Cabeceira_Eng.Grupo_Modelo, CHARINDEX('|', Cabeceira_Eng.Grupo_Modelo) + 1, LEN(Cabeceira_Eng.Grupo_Modelo)) AS Grupo_Modelo_Dir, \n" +
                                    "Cabeceira_Ligacao_Eng.Esq_Dir \n" +
                                    "from Cabeceira_Eng \n" +
                                    "inner join Cabeceira_Ligacao_Eng on Cabeceira_Eng.Cabeceira = Cabeceira_Ligacao_Eng.Cabeceira \n" +
                                    "where Cabeceira_Ligacao_Eng.Cabeceira = '" + Cabeceira_Selecionado + "'; \n" +
                                    "create table #temp2 (Modelo nvarchar (100)) \n" +
                                    "insert into #temp2 \n" +
                                    "select distinct Modelos_Eng.Modelo \n" +
                                    "from #temp1 \n" +
                                    "inner join Modelos_Eng on \n" +
                                    "((CHARINDEX(',', #temp1.Grupo_Modelo_Dir) > 0 AND Modelos_Eng.Modelo LIKE #temp1.Grupo_Modelo_Dir + '%') or (CHARINDEX(',', #temp1.Grupo_Modelo_Esq) > 0 AND Modelos_Eng.Modelo LIKE #temp1.Grupo_Modelo_Esq + '%') \n" +
                                    "OR  \n" +
                                    "(CHARINDEX(',', #temp1.Grupo_Modelo_Dir) = 0 AND Modelos_Eng.Modelo LIKE #temp1.Grupo_Modelo_Dir + '-%') or (CHARINDEX(',', #temp1.Grupo_Modelo_Esq) = 0 AND Modelos_Eng.Modelo LIKE #temp1.Grupo_Modelo_Esq + '-%')); \n" +
                                    "SELECT Modelo \n" +
                                    "FROM #temp2 \n" +
                                    "WHERE Modelo NOT IN( \n" +
                                    "SELECT Expositor \n" +
                                    "FROM Cabeceira_Ligacao_Eng \n" +
                                    "WHERE Cabeceira_Ligacao_Eng.basecodigo = 'eletrofrio' and Cabeceira = '" + Cabeceira_Selecionado + "' \n" +
                                    "); ";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string nome = reader["Modelo"].ToString();
                            listBox_modelos_cab.Items.Add(nome);

                        }
                        reader.Close();
                    }
                }
            }
        }

        private void Inserir_Cabeceira_Acessorios_Eng()
        {
            if (listBox2.Items.Count != 0)
            {
                foreach (var item in listBox2.Items)
                {
                    string query_Cabeceira_Acessorios_Eng = "insert into Cabeceira_Acessorios_Eng (Cabeceira, Acessorios, BaseCodigo) values (@Cabeceira, @Acessorios,'Eletrofrio');";
                    string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query_Cabeceira_Acessorios_Eng, connection);
                        command.Parameters.AddWithValue("@Cabeceira", textBox_Nome.Text);
                        command.Parameters.AddWithValue("@Acessorios", item);


                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
        }


        private void Inserir_Cabeceira_Ligacao_Eng()
        {
            if (listBox_modelos_cab_dir.Items.Count != 0)
            {
                foreach (var item in listBox_modelos_cab_dir.Items)
                {
                    string query_Cabeceira_Ligacao_Eng = "insert into Cabeceira_Ligacao_Eng (Cabeceira,Esq_Dir,Expositor,Travar_representante,Basecodigo) values (@Cabeceira,@Esq_Dir, @Expositor,@Travar_representante,'Eletrofrio');";
                    string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query_Cabeceira_Ligacao_Eng, connection);
                        command.Parameters.AddWithValue("@Cabeceira", textBox_Nome.Text);
                        command.Parameters.AddWithValue("@Esq_Dir", "D");
                        command.Parameters.AddWithValue("@Expositor", item);
                        command.Parameters.AddWithValue("@Travar_representante", "0");

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            if (listBox_modelos_cab_esq.Items.Count != 0)
            {
                foreach (var item in listBox_modelos_cab_esq.Items)
                {
                    string query_Cabeceira_Ligacao_Eng = "insert into Cabeceira_Ligacao_Eng (Cabeceira,Esq_Dir,Expositor,Travar_representante, basecodigo) values (@Cabeceira,@Esq_Dir, @Expositor,@Travar_representante, 'Eletrofrio');";
                    string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query_Cabeceira_Ligacao_Eng, connection);
                        command.Parameters.AddWithValue("@Cabeceira", textBox_Nome.Text);
                        command.Parameters.AddWithValue("@Esq_Dir", "E");
                        command.Parameters.AddWithValue("@Expositor", item);
                        command.Parameters.AddWithValue("@Travar_representante", "0");

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void CheckBoxDirSelecionado()
        {
            listBox_modelos_cab_dir.Enabled = true;
            listBox_modelos_cab_esq.Enabled = false;
        }

        private void CheckBoxEsqSelecionado()
        {
            listBox_modelos_cab_dir.Enabled = false;
            listBox_modelos_cab_esq.Enabled = true;
        }

        private void CheckBoxAjusteSelecionado()
        {
            listBox_modelos_cab_dir.Enabled = true;
            listBox_modelos_cab_esq.Enabled = true;
        }

        private void dataGridView_ListaCab_SelectionChanged(object sender, EventArgs e)
        {


            tableLayoutPanel12.Visible = true;
            tableLayoutPanel7.Visible = false;
            tableLayoutPanel8.Visible = false;
            panel_lado_cab.Visible = false;
            tableLayoutPanel10.Visible = false;
            comboBox_GrupoCor_Visualizacao.Enabled = true;
            tabControl8.SelectedTab = tabPage_Modelos;
            int Id = 0;
            string nome = "";
            string GrupoDeCor = "";
            int Profundidade = 0;
            int Espessura = 0;
            string Codigo = "";
            string Observacao = "";

            if (dataGridView_ListaCab.SelectedRows.Count > 0)
            {
                // Obtém o valor do ID da linha selecionada
                int idSelecionado = Convert.ToInt32(dataGridView_ListaCab.SelectedRows[0].Cells["ID"].Value);

                // Aqui você pode usar o ID para buscar as informações do banco de dados
                // Use sua lógica de acesso ao banco de dados para recuperar os dados desejados

                // Exemplo de consulta usando ADO.NET
                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Cabeceira_Eng WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", idSelecionado);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // Recupere as informações do banco de dados
                        Id = reader.GetInt32(reader.GetOrdinal("ID"));
                        nome = reader.GetString(reader.GetOrdinal("Cabeceira"));
                        GrupoDeCor = reader.GetString(reader.GetOrdinal("Grupo_Cor"));
                        Profundidade = reader.GetInt32(reader.GetOrdinal("Profundidade"));
                        Espessura = reader.GetInt32(reader.GetOrdinal("Espessura"));
                        Codigo = reader.GetString(reader.GetOrdinal("Codigo"));
                        Observacao = reader.GetString(reader.GetOrdinal("Observacao"));
                        // ... recupere outras informações conforme necessário
                        // Faça o que você precisa com os dados recuperados
                    }
                    reader.Close();
                    textBox_Nome_Visualizacao.Text = nome;
                    comboBox_GrupoCor_Visualizacao.Text = GrupoDeCor;
                    comboBox_profundidade3.Text = Profundidade.ToString();
                    textBox_Espessura_Visualizacao.Text = Espessura.ToString();
                    textBox_Codigo_Visualizacao.Text = Codigo;
                    textBox_ID.Text = Id.ToString();
                    textBox_Observacao.Text = Observacao.ToString();
                    //carregar_acessorios_cabeceiras(nome);
                    //carregar_acessorios_selecionados_cabeceiras(nome);
                    lista_acessorios_cabeceira(nome);
                }
                Carregar_Listbox_Modelos_Cab();
            }
            dataGridView_ListaCab.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn column in dataGridView_ListaCab.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.FillWeight = 1;
            }
            Verificar_Modelos_Disponiveis_Cab();
            ListBox_modelos_cab_painel();
        }

        private void Carregar_Listbox_Modelos_Cab()
        {
            string Lado = "";
            string Expositor = "";
            listBox_modelos_cab_esq.Items.Clear();
            listBox_modelos_cab_dir.Items.Clear();

            if (dataGridView_ListaCab.SelectedRows.Count > 0)
            {
                int idSelecionado = Convert.ToInt32(dataGridView_ListaCab.SelectedRows[0].Cells["ID"].Value);
                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "select Cabeceira_Eng.Cabeceira,Esq_Dir,Expositor from Cabeceira_Eng inner join Cabeceira_Ligacao_Eng on Cabeceira_Eng.Cabeceira = Cabeceira_Ligacao_Eng.Cabeceira where Cabeceira_Eng.ID = @ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", idSelecionado);
                    SqlDataReader reader = command.ExecuteReader();
                    string cabeceira = "";
                    while (reader.Read())
                    {
                        Lado = reader.GetString(reader.GetOrdinal("Esq_Dir"));
                        Expositor = reader.GetString(reader.GetOrdinal("Expositor"));
                        cabeceira = reader.GetString(reader.GetOrdinal("Cabeceira"));
                        if (Lado == "E")
                        {
                            listBox_modelos_cab_esq.Items.Add(Expositor);
                            listBox_modelos_cab_esq.Enabled = true;
                            listBox_modelos_cab_esq.BackColor = SystemColors.Window;

                        }
                        if (Lado == "D")
                        {
                            listBox_modelos_cab_dir.Items.Add(Expositor);
                            listBox_modelos_cab_dir.Enabled = true;
                            listBox_modelos_cab_dir.BackColor = SystemColors.Window;
                        }

                    }
                    if (cabeceira.Contains("Ajuste"))
                    {
                        listBox_modelos_cab_dir.Enabled = true;
                        listBox_modelos_cab_esq.Enabled = true;
                    }
                    reader.Close();
                }

            }

        }

        private void carregar_acessorios_cabeceiras(string cabeceira)
        {


            List<string> modelosSelecionados = new List<string>();
            foreach (var item in listBox_modelos_cab_dir.Items)
            {
                modelosSelecionados.Add(item.ToString());
            }
            foreach (var item in listBox_modelos_cab_esq.Items)
            {
                modelosSelecionados.Add(item.ToString());
            }

            if (modelosSelecionados.Count > 0)
            {
                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

                // Inicializar a query com a parte estática
                string query = "SELECT DISTINCT Componente " +
                               "FROM Planilhas_Eng " +
                               "WHERE Planilhas_Eng.basecodigo = 'Eletrofrio' " +
                               "AND Planilhas_Eng.Acessarios_CAB = 1 " +
                               "AND Componente NOT IN (SELECT Acessorios FROM Cabeceira_Acessorios_Eng WHERE Cabeceira = @cabeceira AND basecodigo = 'Eletrofrio')";

                // Adicionar condições para cada modelo selecionado
                if (modelosSelecionados.Count > 0)
                {
                    query += " AND (";
                    for (int i = 0; i < modelosSelecionados.Count; i++)
                    {
                        if (i > 0)
                            query += " OR ";
                        query += "Planilhas_Eng.Modelo = '" + modelosSelecionados[i] + "'";
                    }
                    query += ")";
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@cabeceira", cabeceira);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    listBox1.Items.Clear();
                    while (reader.Read())
                    {
                        string Acessorios = reader.GetString(0);
                        listBox1.Items.Add(Acessorios);
                    }
                    reader.Close();
                }
            }


        }

        private void carregar_acessorios_selecionados_cabeceiras(string nome)
        {
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = "SELECT distinct Cabeceira, Acessorios FROM Cabeceira_Acessorios_Eng WHERE Cabeceira = @Cabeceira and basecodigo = 'Eletrofrio'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Cabeceira", nome);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                listBox2.Items.Clear();
                while (reader.Read())
                {
                    string acessorios = reader.GetString(1);
                    listBox2.Items.Add(acessorios);
                }
                reader.Close();
            }

        }

        private void button_Novo_Click(object sender, EventArgs e)
        {
            NovaCab();
        }


        private void NovaCab()
        {
            textBox_Nome_Visualizacao.Text = "";
            dataGridView_ListaCab.ClearSelection();
            textBox_Nome.Text = "";
            Tipo = "";
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox_modelos_cab.Items.Clear();
            listBox_modelos_cab_dir.Items.Clear();
            listBox_modelos_cab_esq.Items.Clear();
            tableLayoutPanel10.Visible = true;
            tableLayoutPanel12.Visible = false;
            textBox_Nome.Text = "";
            comboBox_Tipo.Text = "";
            comboBox_Modelo_parte1.Text = "";
            comboBox_grupoCor1.SelectedItem = null;
            comboBox_profundidade1.Text = "";
            textBox_Espessura1.Text = "";
            textBox_Codigo.Text = "";
            comboBox_modelo1_parte2.Text = "";
            comboBox_Modelo2_Parte2.Text = "";
            comboBox_grupoCor2.SelectedItem = null;
            comboBox_Profundidade2.Text = "";
            textBox_Espessura2.Text = "";
            textBox_Codigo2.Text = "";
            textBox_Observacao.Text = "";
            lista_acessorios_cabeceira("");
            carregar_acessorios_cabeceiras("");
            LiberarPanelCab("Novo");
        }

        private void comboBox_GrupoCor_Visualizacao_DropDown(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = "select distinct Grupo_Cor from Cabeceira_Tipo_Eng";
            comboBox_GrupoCor_Visualizacao.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string cabeceira = reader["Grupo_Cor"].ToString();
                    comboBox_GrupoCor_Visualizacao.Items.Add(cabeceira);

                }
                reader.Close();
            }
            lista_acessorios_cabeceira(textBox_Nome_Visualizacao.Text);
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                // Obter o item selecionado
                string item = listBox2.SelectedItem.ToString();

                // Remover o item da ListBox2
                listBox2.Items.Remove(item);

                // Adicionar o item à ListBox1
                listBox1.Items.Add(item);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            // Verificar se há um item selecionado na ListBox2
            if (listBox1.SelectedItem != null)
            {
                // Obter o item selecionado
                string item = listBox1.SelectedItem.ToString();

                // Remover o item da ListBox2
                listBox1.Items.Remove(item);

                // Adicionar o item à ListBox1
                listBox2.Items.Add(item);
            }
        }

        private void listBox_modelos_cab_esq_DoubleClick(object sender, EventArgs e)
        {
            if (listBox_modelos_cab_esq.SelectedIndex != -1)
            {
                // Obtém o item selecionado
                string itemSelecionado = listBox_modelos_cab_esq.SelectedItem.ToString();

                // Remove o item selecionado da listBox_modelos_cab_esq
                listBox_modelos_cab_esq.Items.Remove(itemSelecionado);

                // Adiciona o item selecionado na listBox_modelos_cab_esq2
                listBox_modelos_cab.Items.Add(itemSelecionado);
            }
            //}
            ListBox_modelos_cab_painel();
        }

        private void listBox_modelos_cab_dir_DoubleClick(object sender, EventArgs e)
        {
            if (listBox_modelos_cab_dir.SelectedIndex != -1)
            {
                // Obtém o item selecionado
                string itemSelecionado = listBox_modelos_cab_dir.SelectedItem.ToString();

                // Remove o item selecionado da listBox_modelos_cab_esq
                listBox_modelos_cab_dir.Items.Remove(itemSelecionado);

                // Adiciona o item selecionado na listBox_modelos_cab_esq2
                listBox_modelos_cab.Items.Add(itemSelecionado);
            }
            //}
            ListBox_modelos_cab_painel();

        }

        private void listBox_modelos_cab_DoubleClick(object sender, EventArgs e)
        {
            if (listBox_modelos_cab.SelectedIndex != -1)
            {
                // Obtém o item selecionado
                string itemSelecionado = listBox_modelos_cab.SelectedItem.ToString();

                if (checkBox_dir.Checked)
                {
                    // Remove o item selecionado da listBox_modelos_cab_esq
                    listBox_modelos_cab.Items.Remove(itemSelecionado);

                    // Adiciona o item selecionado na listBox_modelos_cab_esq2
                    listBox_modelos_cab_dir.Items.Add(itemSelecionado);
                }
                if (checkBox_esq.Checked)
                {
                    // Remove o item selecionado da listBox_modelos_cab_esq
                    listBox_modelos_cab.Items.Remove(itemSelecionado);

                    // Adiciona o item selecionado na listBox_modelos_cab_esq2
                    listBox_modelos_cab_esq.Items.Add(itemSelecionado);
                }
                if (Tipo == "Ajuste")
                {
                    int indiceTraco = itemSelecionado.IndexOf(',');
                    if (indiceTraco != -1)
                    {
                        string resultado = itemSelecionado.Substring(0, indiceTraco);

                        if (comboBox_modelo1_parte2.Text == resultado || comboBox_modelo1_parte2.Text == itemSelecionado && comboBox_Modelo2_Parte2.Text == resultado || comboBox_Modelo2_Parte2.Text == itemSelecionado)
                        {
                            // Exibir a caixa de diálogo de mensagem com as opções personalizadas
                            DialogResult result = MessageBox.Show("Deseja inserir no lado esquerdo?", "Escolha o lado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                foreach (var item in listBox_modelos_cab_esq.Items)
                                {
                                    // Verifica se o texto já existe
                                    if (item.ToString() == itemSelecionado)
                                    {
                                        MessageBox.Show("Cabeceira existente!");
                                        return;
                                    }
                                    else
                                    {
                                        listBox_modelos_cab.Items.Remove(itemSelecionado);
                                        listBox_modelos_cab_esq.Items.Add(itemSelecionado);
                                        return;
                                    }
                                }
                                // Remove o item selecionado da listBox_modelos_cab_esq

                            }
                            else
                            {
                                // Exibir a caixa de diálogo de mensagem com as opções personalizadas
                                DialogResult result1 = MessageBox.Show("Deseja inserir no lado direito?", "Escolha o lado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result1 == DialogResult.Yes)
                                {
                                    foreach (var item in listBox_modelos_cab_dir.Items)
                                    {
                                        if (item.ToString() == itemSelecionado)
                                        {
                                            MessageBox.Show("Cabeceira existente!");
                                            return;
                                        }
                                        else
                                        {
                                            listBox_modelos_cab.Items.Remove(itemSelecionado);
                                            listBox_modelos_cab_dir.Items.Add(itemSelecionado);
                                            return;
                                        }
                                    }

                                }
                                else
                                {
                                    return;
                                }
                            }
                        }

                        if (comboBox_modelo1_parte2.Text == resultado || comboBox_modelo1_parte2.Text == itemSelecionado)
                        {
                            // Remove o item selecionado da listBox_modelos_cab_esq
                            listBox_modelos_cab.Items.Remove(itemSelecionado);
                            listBox_modelos_cab_esq.Items.Add(itemSelecionado);
                            return;
                        }

                        if (comboBox_Modelo2_Parte2.Text == resultado || comboBox_Modelo2_Parte2.Text == itemSelecionado)
                        {
                            listBox_modelos_cab.Items.Remove(itemSelecionado);
                            listBox_modelos_cab_dir.Items.Add(itemSelecionado);
                            return;
                        }
                    }
                }
                if (tableLayoutPanel12.Enabled == true)
                {
                    string ladoCab = "";
                    string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                    string consultaSQL = "select Lado_Cab from Cabeceira_Eng where basecodigo = 'Eletrofrio' and Cabeceira = '" + textBox_Nome_Visualizacao.Text + "'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(consultaSQL, connection);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            // Acessar os valores retornados da consulta                            
                            ladoCab = reader.GetString(0);
                        }
                        reader.Close();
                    }

                    if (ladoCab == "D")
                    {
                        listBox_modelos_cab_dir.Items.Add(itemSelecionado);
                        listBox_modelos_cab.Items.Remove(itemSelecionado);
                    }
                    if (ladoCab == "E")
                    {
                        listBox_modelos_cab_esq.Items.Add(itemSelecionado);
                        listBox_modelos_cab.Items.Remove(itemSelecionado);
                    }
                    if (ladoCab == "")
                    {
                        string query = "select Grupo_Modelo from Cabeceira_Eng where basecodigo = 'Eletrofrio' and Cabeceira like '" + textBox_Nome_Visualizacao.Text + "'";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand command = new SqlCommand(query, connection);
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                // Acessar os valores retornados da consulta                            
                                ladoCab = reader.GetString(0);
                            }
                            reader.Close();
                        }

                        string[] resultados = ladoCab.Split('|');

                        string ladoEsq = resultados[0];

                        string ladoDir = resultados[1];


                        string valorVerificacao = "";

                        int indiceHifen1 = ladoEsq.IndexOf("-");
                        if (indiceHifen1 != -1)
                        {
                            ladoEsq = ladoEsq.Substring(0, indiceHifen1);
                        }

                        int indiceHifen2 = ladoDir.IndexOf("-");
                        if (indiceHifen2 != -1)
                        {
                            ladoDir = ladoDir.Substring(0, indiceHifen2);
                        }

                        int indiceHifen3 = itemSelecionado.IndexOf("-");
                        if (indiceHifen3 != -1)
                        {
                            valorVerificacao = itemSelecionado.Substring(0, indiceHifen3);
                        }

                        if (valorVerificacao == ladoEsq && valorVerificacao != ladoDir)
                        {
                            listBox_modelos_cab_esq.Items.Add(itemSelecionado);
                            listBox_modelos_cab.Items.Remove(itemSelecionado);
                        }
                        if (valorVerificacao == ladoDir && valorVerificacao != ladoEsq)
                        {
                            listBox_modelos_cab_dir.Items.Add(itemSelecionado);
                            listBox_modelos_cab.Items.Remove(itemSelecionado);
                        }
                        if (valorVerificacao == ladoEsq && valorVerificacao == ladoDir)
                        {
                            DialogResult result = MessageBox.Show("Deseja inserir no lado esquerdo?", "Escolha o lado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                foreach (var item in listBox_modelos_cab_esq.Items)
                                {
                                    // Verifica se o texto já existe
                                    if (item.ToString() == itemSelecionado)
                                    {
                                        MessageBox.Show("Cabeceira existente!");
                                        return;
                                    }
                                    else
                                    {
                                        listBox_modelos_cab.Items.Remove(itemSelecionado);
                                        listBox_modelos_cab_esq.Items.Add(itemSelecionado);
                                        return;
                                    }
                                }
                                // Remove o item selecionado da listBox_modelos_cab_esq

                            }
                            else
                            {
                                // Exibir a caixa de diálogo de mensagem com as opções personalizadas
                                DialogResult result1 = MessageBox.Show("Deseja inserir no lado direito?", "Escolha o lado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result1 == DialogResult.Yes)
                                {
                                    foreach (var item in listBox_modelos_cab_dir.Items)
                                    {
                                        if (item.ToString() == itemSelecionado)
                                        {
                                            MessageBox.Show("Cabeceira existente!");
                                            return;
                                        }
                                        else
                                        {
                                            listBox_modelos_cab.Items.Remove(itemSelecionado);
                                            listBox_modelos_cab_dir.Items.Add(itemSelecionado);
                                            return;
                                        }
                                    }

                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                }

            }

            ListBox_modelos_cab_painel();
        }

        private void ListBox_modelos_cab_painel()
        {
            listBox_modelos_cab.Enabled = false;
            listBox_modelos_cab.BackColor = SystemColors.ControlLight;
            listBox_modelos_cab_esq.Enabled = false;
            listBox_modelos_cab_esq.BackColor = SystemColors.ControlLight;
            listBox_modelos_cab_dir.Enabled = false;
            listBox_modelos_cab_dir.BackColor = SystemColors.ControlLight;
            if (listBox_modelos_cab.Items.Count > 0)
            {
                listBox_modelos_cab.Enabled = true;
                listBox_modelos_cab.BackColor = SystemColors.Window;
            }
            if (listBox_modelos_cab_esq.Items.Count > 0)
            {
                listBox_modelos_cab_esq.Enabled = true;
                listBox_modelos_cab_esq.BackColor = SystemColors.Window;
            }
            if (listBox_modelos_cab_dir.Items.Count > 0)
            {
                listBox_modelos_cab_dir.Enabled = true;
                listBox_modelos_cab_dir.BackColor = SystemColors.Window;
            }

        }

        private void button_Deletar_Click(object sender, EventArgs e)
        {
            // Verificar se algum registro está selecionado
            if (dataGridView_ListaCab.SelectedRows.Count > 0)
            {

                // Obter o ID selecionado
                int idSelecionado = Convert.ToInt32(dataGridView_ListaCab.SelectedRows[0].Cells["ID"].Value);
                string grupoModelo = dataGridView_ListaCab.SelectedRows[0].Cells["Grupo_Modelo"].Value as string;
                string cabeceira = dataGridView_ListaCab.SelectedRows[0].Cells["Cabeceira"].Value as string;
                string ladoCab = dataGridView_ListaCab.SelectedRows[0].Cells["Lado_Cab"].Value as string;

                //NotificacaoCabeceiraDeletar(grupoModelo,cabeceira,ladoCab);
                NotificacaoCabeceiraDeletar(cabeceira);
                LogCabeceira(grupoModelo, cabeceira, "", "Cabeceira deletada");
                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                string query = "WITH CTE AS ( \n" +
                    "select t1.ID as t1_ID,t3.ID as t3_ID \n" +
                    "from Cabeceira_Eng t1 \n" +
                    "inner join Cabeceira_Ligacao_Eng t3 on t1.Cabeceira = t3.Cabeceira \n" +
                    "where t1.ID = @ID) \n" +
                    "delete FROM Cabeceira_Ligacao_Eng WHERE ID IN(SELECT t3_ID FROM CTE); \n" +
                    "WITH CTE2 AS( \n" +
                    "select t1.ID as t1_ID, t2.ID as t2_ID \n" +
                    "from Cabeceira_Eng t1 \n" +
                    "inner join Cabeceira_Acessorios_Eng t2 on t1.Cabeceira = t2.Cabeceira \n" +
                    "where t1.ID = @ID) \n" +
                    "delete FROM Cabeceira_Acessorios_Eng WHERE ID IN(SELECT t2_ID FROM CTE2); \n" +
                    "delete from Cabeceira_Eng where Cabeceira_Eng.ID = @ID;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", idSelecionado);

                    connection.Open();
                    command.ExecuteNonQuery(); // Executa o comando de inserção no banco de dados
                }
                Carregar_Lista_Cabeceira();
                NovaCab();

                MessageBox.Show("Deletado com sucesso!.");
            }
            else
            {
                MessageBox.Show("Selecione um registro para excluir.");
            }
        }

        private void button_voltar_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Planilha_eng);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_Planilha_eng(object obj)
        {
            Application.Run(new Form_Planilha_Eng(valor_Gerenciador_SIP, login));
        }

        private void dataGridView_ListaCab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                // Verificar se algum registro está selecionado
                if (dataGridView_ListaCab.SelectedRows.Count > 0)
                {
                    // Obter o ID selecionado
                    int idSelecionado = Convert.ToInt32(dataGridView_ListaCab.SelectedRows[0].Cells["ID"].Value);

                    string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                    string query = "WITH CTE AS ( \n" +
                        "select t1.ID as t1_ID,t3.ID as t3_ID \n" +
                        "from Cabeceira_Eng t1 \n" +
                        "inner join Cabeceira_Ligacao_Eng t3 on t1.Cabeceira = t3.Cabeceira \n" +
                        "where t1.ID = @ID) \n" +
                        "delete FROM Cabeceira_Ligacao_Eng WHERE ID IN(SELECT t3_ID FROM CTE); \n" +
                        "WITH CTE2 AS( \n" +
                        "select t1.ID as t1_ID, t2.ID as t2_ID \n" +
                        "from Cabeceira_Eng t1 \n" +
                        "inner join Cabeceira_Acessorios_Eng t2 on t1.Cabeceira = t2.Cabeceira \n" +
                        "where t1.ID = @ID) \n" +
                        "delete FROM Cabeceira_Acessorios_Eng WHERE ID IN(SELECT t2_ID FROM CTE2); \n" +
                        "delete from Cabeceira_Eng where Cabeceira_Eng.ID = @ID;";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", idSelecionado);

                        connection.Open();
                        command.ExecuteNonQuery(); // Executa o comando de inserção no banco de dados
                    }
                    Carregar_Lista_Cabeceira();
                    NovaCab();
                    MessageBox.Show("Deletado com sucesso!.");
                }
                else
                {
                    MessageBox.Show("Selecione um registro para excluir.");
                }
            }
        }

        private void comboBox_profundidade1_DropDown(object sender, EventArgs e)
        {
            comboBox_profundidade1.Items.Clear();
            ListaProfundidade();
        }

        private void comboBox_Profundidade2_DropDown(object sender, EventArgs e)
        {
            comboBox_Profundidade2.Items.Clear();
            ListaProfundidade();
        }
        private void ListaProfundidade()
        {
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
                    comboBox_profundidade1.Items.Add(profundidade);
                    comboBox_Profundidade2.Items.Add(profundidade);
                    comboBox_profundidade3.Items.Add(profundidade);

                }
                reader.Close();
            }
        }

        private void comboBox_profundidade3_DropDown(object sender, EventArgs e)
        {
            comboBox_profundidade3.Items.Clear();
            ListaProfundidade();
        }

        private void textBox_pesquisaDescricao_TextChanged(object sender, EventArgs e)
        {
            // Atualizar o filtro sempre que o texto do TextBox mudar
            string filterText = textBox_pesquisaDescricao.Text;
            filterText = filterText.Replace("'", "''"); // Tratar as aspas para evitar problemas no filtro
            dataTable.DefaultView.RowFilter = $"Cabeceira LIKE '%{filterText}%'";
        }

        private void tabControl8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textBox_Nome.Text == "" && tableLayoutPanel12.Visible == false && textBox_Nome_Visualizacao.Text == "")
            {
                MessageBox.Show("Nenhuma cabeceira selecionada!");
                tabControl8.SelectedTab = tabPage_Modelos;
                return;
            }
            if (tabControl8.SelectedTab == tabPage_Acessorios && textBox_Nome.Text != "" || textBox_Nome_Visualizacao.Text != "")
            {
                carregar_acessorios_cabeceiras(textBox_Nome_Visualizacao.Text);
                carregar_acessorios_selecionados_cabeceiras(textBox_Nome_Visualizacao.Text);
            }
        }


        private void textBox_Nome_Visualizacao_TextChanged(object sender, EventArgs e)
        {
            carregar_acessorios_cabeceiras(textBox_Nome_Visualizacao.Text);
            carregar_acessorios_selecionados_cabeceiras(textBox_Nome_Visualizacao.Text);
        }

        private void Form_Cabeceira_Eng_New_Load(object sender, EventArgs e)
        {
            ListBox_modelos_cab_painel();
            if (valor_Gerenciador_SIP != null && valor_Gerenciador_SIP.Contains("Adm Eletrofrio") && valor_Gerenciador_SIP.Contains("Engenharia de Produtos"))
            {
                textBox_Observacao.Enabled = true;

            }
        }

        private void comboBox_grupoCor1_DropDown(object sender, EventArgs e)
        {

            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + "Eletrofast_2019" + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = "SELECT nome FROM gen_grp_acab where BaseCodigo = 'Eletrofrio' and TRAVAR_REPRESENTANTE = 0 and nome not like '9%'";
            // Limpe os itens existentes no ComboBox
            comboBox_grupoCor1.Items.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute o comando e leia os dados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Adicione os itens ao ComboBox
                                comboBox_grupoCor1.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do banco de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox_grupoCor2_DropDown(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + "Eletrofast_2019" + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = "SELECT nome FROM gen_grp_acab where BaseCodigo = 'Eletrofrio' and TRAVAR_REPRESENTANTE = 0 and nome not like '9%'";
            // Limpe os itens existentes no ComboBox
            comboBox_grupoCor2.Items.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute o comando e leia os dados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Adicione os itens ao ComboBox
                                comboBox_grupoCor2.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do banco de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


