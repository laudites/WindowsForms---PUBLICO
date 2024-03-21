using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Office2010.Excel;
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
    public partial class Form_Novo_Acessorio : Form
    {
        string Banco = "BANCODEDADOS";
        string Instancia = @"INSTANCIA";
        string Senha = "SENHA";
        string Usuario = "sa";
        Thread t1;
        List<string> valor_Gerenciador_SIP;
        string Login;
        string Linha;
        string Expositor;

        public Form_Novo_Acessorio(List<string> valorGerenciador, string login, string linha, string expositor)
        {
            InitializeComponent();
            valor_Gerenciador_SIP = valorGerenciador;
            Login = login;
            Linha = linha;
            Expositor = expositor;
        }

        private void button_Voltar_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Planilha);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_Planilha(object obj)
        {
            Application.Run(new Form_Planilha_Eng(valor_Gerenciador_SIP, Login));
        }


        private void Form_Novo_Acessorio_Load(object sender, EventArgs e)
        {
            comboBox_LiberarRepresentante.Items.Clear();
            comboBox_AcessoriosCab.Items.Clear();
            // Carregar opções para comboBox1
            comboBox_LiberarRepresentante.Items.Add("SIM");
            comboBox_LiberarRepresentante.Items.Add("NÃO");

            // Selecionar o primeiro item por padrão
            comboBox_LiberarRepresentante.SelectedIndex = 0;

            // Carregar opções para comboBox2
            comboBox_AcessoriosCab.Items.Add("SIM");
            comboBox_AcessoriosCab.Items.Add("NÃO");

            // Selecionar o primeiro item por padrão
            comboBox_AcessoriosCab.SelectedIndex = 1;

            textBox_modelo.Text = Expositor;
        }



        private void comboBox_tipo_DropDown(object sender, EventArgs e)
        {

            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + "Eletrofast_2019" + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query = "SELECT nome FROM gen_grp_acab where BaseCodigo = 'Eletrofrio' and TRAVAR_REPRESENTANTE = 0 and nome not like '9%'";
            // Limpe os itens existentes no ComboBox
            comboBox_cores.Items.Clear();

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
                                comboBox_cores.Items.Add(reader.GetString(0));
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

        private void button_confirmar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha))
            {
                // Abra a conexão
                con.Open();

                // Inicie uma transação
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // Crie a consulta SQL com parâmetros
                    string query = "INSERT INTO Planilhas_Eng (Modelo\r\n,Componente\r\n,Cores\r\n,M125\r\n,M150\r\n,M187\r\n,M225\r\n,M250\r\n,M300\r\n,M375\r\n,E45\r\n,E90\r\n,I45\r\n,I90\r\n,T1875\r\n,T2167\r\n,T2500\r\n,T360\r\n,Observacao\r\n,LiberarRepresentante\r\n,Acessarios_CAB\r\n,BaseCodigo) VALUES (@Modelo\r\n,@Componente\r\n,@Cores\r\n,@M125\r\n,@M150\r\n,@M187\r\n,@M225\r\n,@M250\r\n,@M300\r\n,@M375\r\n,@E45\r\n,@E90\r\n,@I45\r\n,@I90\r\n,@T1875\r\n,@T2167\r\n,@T2500\r\n,@T360\r\n,@Observacao\r\n,@LiberarRepresentante\r\n,@Acessarios_CAB\r\n,@BaseCodigo)";

                    // Crie um comando SQL com a consulta e a conexão
                    using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                    {
                        // Adicione os parâmetros e seus valores
                        cmd.Parameters.AddWithValue("@Modelo", textBox_modelo.Text);
                        cmd.Parameters.AddWithValue("@Componente", textBox_componente.Text);
                        cmd.Parameters.AddWithValue("@Cores", comboBox_cores.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@M125", textBox_M125.Text);
                        cmd.Parameters.AddWithValue("@M150", textBox_M150.Text);
                        cmd.Parameters.AddWithValue("@M187", textBox_M187.Text);
                        cmd.Parameters.AddWithValue("@M225", textBox_M225.Text);
                        cmd.Parameters.AddWithValue("@M250", textBox_M250.Text);
                        cmd.Parameters.AddWithValue("@M300", textBox_M300.Text);
                        cmd.Parameters.AddWithValue("@M375", textBox_M375.Text);
                        cmd.Parameters.AddWithValue("@E45", textBox_E45.Text);
                        cmd.Parameters.AddWithValue("@E90", textBox_E90.Text);
                        cmd.Parameters.AddWithValue("@I45", textBox_I45.Text);
                        cmd.Parameters.AddWithValue("@I90", textBox_I90.Text);
                        cmd.Parameters.AddWithValue("@T1875", textBox_T1875.Text);
                        cmd.Parameters.AddWithValue("@T2167", textBox_T2167.Text);
                        cmd.Parameters.AddWithValue("@T2500", textBox_T2500.Text);
                        cmd.Parameters.AddWithValue("@T360", textBox_T360.Text);
                        cmd.Parameters.AddWithValue("@Observacao", textBox_Observacao.Text);
                        if (comboBox_LiberarRepresentante.SelectedItem.ToString() == "SIM")
                        {
                            cmd.Parameters.AddWithValue("@LiberarRepresentante", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@LiberarRepresentante", false);
                        }
                        if (comboBox_AcessoriosCab.SelectedItem.ToString() == "SIM")
                        {
                            cmd.Parameters.AddWithValue("@Acessarios_CAB", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Acessarios_CAB", false);
                        }
                        
                        cmd.Parameters.AddWithValue("@BaseCodigo", "Eletrofrio");
                        // Execute a consulta
                        cmd.ExecuteNonQuery();

                        // Commit da transação se tudo ocorrer bem
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    // Se ocorrer algum erro, faça o rollback da transação
                    transaction.Rollback();
                    MessageBox.Show("Erro: " + ex.Message);
                    con.Close();
                    return;
                }
                finally
                {
                    // Feche a conexão
                    con.Close();
                }

                textBox_componente.Text = "";                
                textBox_M125.Text = "";
                textBox_M150.Text = "";
                textBox_M187.Text = "";
                textBox_M225.Text = "";
                textBox_M250.Text = "";
                textBox_M300.Text = "";
                textBox_M375.Text = "";
                textBox_E45.Text = "";
                textBox_E90.Text = "";
                textBox_I45.Text = "";
                textBox_I90.Text = "";
                textBox_T1875.Text = "";
                textBox_T2167.Text = "";
                textBox_T2500.Text = "";
                textBox_T360.Text = "";
                textBox_Observacao.Text = "";

                MessageBox.Show("Componente criado com sucesso!");
            }
        }
    }
}
