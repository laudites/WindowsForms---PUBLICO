using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class Form_Senha_ADM : Form
    {
        public string return_nivel { get; set; }
        List<string> valor_Gerenciador_SIP;
        List<String> Grupos = new List<String>();
        string senhaLocal = @"INSTANCIA";
        Thread t1;
        string Login;

        public Form_Senha_ADM()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';

            // Verifica se o código de diretores está presente no arquivo de configuração
            string codigoDiretores = ConfigurationManager.AppSettings["CodigoAcesso"];
            if (codigoDiretores == "Diretoria")
            {
                // Executa o conjunto específico de ações para diretores                
                Grupos.Add("Administrador");
                AcessoDiretoria();
                this.Close();
                return;
            }
        }

        private void AcessoDiretoria()
        {
            Form_Relatorio_Diretoria formPlanilhaEng = new Form_Relatorio_Diretoria(Grupos, "eletcom");
            Application.Run(formPlanilhaEng);
        }

        private void Login_New()
        {
            Login = textBox2.Text;
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source =" + senhaLocal + "; Initial Catalog = Eletrofast_2019; Persist Security Info = True; User ID = sa; Password = SENHA"))
                {
                    connection.Open();
                    string tsql = "select distinct \n" +
                                    "Grupo \n" +
                                    "from GruposUsuarios \n" +
                                    "inner join Usuarios on GruposUsuarios.idUsuario = Usuarios.idusuario \n" +
                                    "inner join Grupos on GruposUsuarios.idGrupo = grupos.idgrupo \n" +
                                    "where Login = '" + textBox2.Text + "'";
                    using (SqlCommand command = new SqlCommand(tsql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Grupos.Add(reader.GetString(0));
                            }
                        }
                    }
                    foreach (string grupo in Grupos)
                    {

                        return_nivel = "NIVEL_USUARIO_AVANCADO";
                        this.Close();
                        t1 = new Thread(Form_Gerenciador_SIP);
                        t1.SetApartmentState(ApartmentState.STA);
                        t1.Start();

                        return;

                    }

                    if (textBox2.Text == "wbc")
                    {
                        return_nivel = "WBC";
                        Grupos.Add("WBC");
                        this.Close();
                        t1 = new Thread(Form_Gerenciador_SIP);
                        t1.SetApartmentState(ApartmentState.STA);
                        t1.Start();

                        return;
                    }
                }
            }
            catch
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source = SERVIDOR; Initial Catalog = BANCODEDADOS; Persist Security Info = True; User ID = sip; Password = SENHA"))
                {
                    connection.Open();
                    string tsql = "select distinct \n" +
                                    "Grupo \n" +
                                    "from GruposUsuarios \n" +
                                    "inner join Usuarios on GruposUsuarios.idUsuario = Usuarios.idusuario \n" +
                                    "inner join Grupos on GruposUsuarios.idGrupo = grupos.idgrupo \n" +
                                    "where Login = '" + textBox2.Text + "'";
                    using (SqlCommand command = new SqlCommand(tsql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Grupos.Add(reader.GetString(0));
                            }
                        }
                    }
                    foreach (string grupo in Grupos)
                    {

                        return_nivel = "NIVEL_USUARIO_AVANCADO";
                        this.Close();
                        t1 = new Thread(Form_Gerenciador_SIP);
                        t1.SetApartmentState(ApartmentState.STA);
                        t1.Start();

                        return;

                    }
                }
            }

        }
        private void button_Entrar_Click(object sender, EventArgs e)
        {
            Login_New();
            MessageBox.Show("Senha errada! Digita novamente!");

        }
        private void Form_Gerenciador_SIP(object obj)
        {
            Application.Run(new Form_Gerenciador_SIP(Grupos, Login));
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_Entrar_Click(this, new EventArgs());
            }
        }
    }
}
