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
    public partial class Form_Copiar_Acessorios : Form
    {
        Thread t1;
        List<string> valor_Gerenciador_SIP;
        string Login;
        string Banco = "BANCODEDADOS";
        string Instancia = @"INSTANCIA";
        string Senha = "SENHA";
        string Usuario = "sa";


        public Form_Copiar_Acessorios(List<string> valorGerenciador, string login, string origem, string linha)
        {
            InitializeComponent();
            valor_Gerenciador_SIP = valorGerenciador;
            Login = login;
            CarregarInformacoes(origem, linha);

        }
        private void CarregarInformacoes(string OrigemCopiar, string linha)
        {
            textBox_origem.Text = OrigemCopiar;
            checkedListBox1.Items.Clear();

            SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
            Cursor.Current = Cursors.WaitCursor;
            con.Open();
            string query = "SELECT [Modelo],[Tipo],[Resfriamento],[Situacao],[Linha] FROM [Modelos_Eng] WHERE Linha = '" + linha + "' order by Modelo;";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                //command.Parameters.AddWithValue("@Linha", Linha);

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
            Application.Run(new Form_Planilha_Eng(valor_Gerenciador_SIP, Login));
        }

        private void button_salvar_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("Deseja realmente copiar os acessorios desses expositores?", "Copiar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "");
                    con.Open();

                    foreach (object item in checkedListBox1.CheckedItems)
                    {
                        string selectedItem = item.ToString();

                        string query_log = "insert into Planilhas_Eng \n SELECT \n" +
                                     "'" + selectedItem + "' \n" +
                                     ",[Componente] \n" +
                                     ",[Cores] \n" +
                                     ",[M125] \n" +
                                     ",[M150] \n" +
                                     ",[M187] \n" +
                                     ",[M225] \n" +
                                     ",[M250] \n" +
                                     ",[M300] \n" +
                                     ",[M375] \n" +
                                     ",[E45] \n" +
                                     ",[E90] \n" +
                                     ",[I45] \n" +
                                     ",[I90] \n" +
                                     ",[T1875] \n" +
                                     ",[T2167] \n" +
                                     ",[T2500] \n" +
                                     ",[T360] \n" +
                                     ",[Observacao] \n" +
                                     ",[LiberarRepresentante] \n" +
                                     ",[Acessarios_CAB] \n" +
                                     ",[BaseCodigo] \n" +
                                     "FROM[BANCODEDADOS].[dbo].[Planilhas_Eng] where Modelo = '" + textBox_origem.Text + "'";
                        SqlTransaction transaction1 = con.BeginTransaction();
                        SqlCommand insertCmd = new SqlCommand(query_log, con, transaction1);
                        insertCmd.ExecuteNonQuery();
                        transaction1.Commit();
                        NotificacaoInserirCopia(selectedItem);
                        LogCopiar(selectedItem);
                    }
                    con.Close();


                    MessageBox.Show("Expositores copiados com sucesso!");
                }
            }
        }

        private void NotificacaoInserirCopia(string modelo)
        {
            Cursor.Current = Cursors.WaitCursor;
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            using (SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + ";MultipleActiveResultSets=True"))
            {
                con.Open();
                string insert_log = "insert into notificacao SELECT \n" +
                                    $"'{Login}','{modelo}','Copiando de {textBox_origem.Text}', \n" +
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
                                        $"'Copiando acessorios','{dataAtual}',NULL,NULL \n" +
                                    "FROM[BANCODEDADOS].[dbo].[Planilhas_Eng] \n" +
                                    $"where Modelo = '{textBox_origem.Text}'";

                using (SqlCommand cmd1 = new SqlCommand(insert_log, con))
                {
                    cmd1.ExecuteNonQuery();
                }
                con.Close();
            }
            Cursor.Current = Cursors.Default;
        }

        private void LogCopiar(string expositor)
        {
            Cursor.Current = Cursors.WaitCursor;
            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            using (SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + ";MultipleActiveResultSets=True"))
            {
                con.Open();
                string insert_log = "insert into Log_eng values (@Login, 'Planilha de engenharia', @OrigemExpositor, @Origem, @Expositor, 'Copia de planilha', @DataAtual, NULL, NULL)";

                using (SqlCommand cmd1 = new SqlCommand(insert_log, con))
                {
                    cmd1.Parameters.AddWithValue("@Login", Login);
                    cmd1.Parameters.AddWithValue("@OrigemExpositor", textBox_origem.Text + "|" + expositor);
                    cmd1.Parameters.AddWithValue("@Origem", textBox_origem.Text);
                    cmd1.Parameters.AddWithValue("@Expositor", expositor);
                    cmd1.Parameters.AddWithValue("@DataAtual", dataAtual);

                    cmd1.ExecuteNonQuery();
                }
                con.Close();
            }

            Cursor.Current = Cursors.Default;

        }


    }
}
