using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Gerenciador_SIP
{

    public partial class Form_Gerenciador_SIP : Form
    {
        string Banco = "BANCODEDADOS";
        string Instancia = @"INSTANCIA";
        string Senha = "SENHA";
        string Usuario = "sa";

        Thread t1;
        string exportar = @"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\01-Exportar";
        string importar = @"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\02-Importar";
        string importados = @"C:\WBC\Ferramentas\Aplicativos\Tabelas_excel\03-Importados";

        List<string> ListaBanco = new List<string>();
        List<string> ListaSenhaBanco = new List<string>();
        List<string> ListaCaminhoBanco = new List<string>();
        List<string> ListaUsuarioBanco = new List<string>();

        List<string> valor_Gerenciador_SIP;
        string Login;
        string senhaLocal = @"INSTANCIA";

        public Form_Gerenciador_SIP(List<string> value, string login)
        {
            InitializeComponent();
            valor_Gerenciador_SIP = value;
            Login = login;
            foreach (string grupo in valor_Gerenciador_SIP)
            {
                if (grupo == "Adm Eletrofrio" || grupo == "Adm Fast" || grupo == "Administrador")
                {
                    InfoBancoBANCODEDADOS("%");
                    button_Tabelas_SQL.Visible = true;
                    button1.Visible = true;
                    button_Eletrofrio.Visible = true;
                    button_procurar.Visible = true;
                    comboBox_banco.Visible = true;
                    textBox_Banco.Visible = true;
                    button_cadastrarServidor.Visible = true;
                    textBox_banco_dados.Visible = true;
                    comboBox_Instancia.Visible = true;
                    button_planilha_eng.Visible = true;
                }
                if (Login == "eletcom" || Login == "willian.laudites")
                {
                    button_diretoria.Visible = true;
                }

                if (grupo == "Gestor Engenharia")
                {
                    InfoBancoBANCODEDADOS("%");
                    button_Tabelas_SQL.Visible = true;
                    button1.Visible = true;
                    button_cadastrarServidor.Visible = true;
                    button_cadastrarServidor.Visible = true;
                    textBox_Banco.Visible = true;
                    comboBox_banco.Visible = true;
                    textBox_banco_dados.Visible = true;
                    comboBox_Instancia.Visible = true;
                    button_planilha_eng.Visible = true;
                }
                if (grupo == "Representante Master")
                {

                }
                if (grupo == "Usuario Avancado")
                {
                    InfoBancoBANCODEDADOS("%");
                    button_Eletrofrio.Visible = true;
                    textBox_Banco.Visible = true;
                    comboBox_banco.Visible = true;
                    textBox_banco_dados.Visible = true;
                    comboBox_Instancia.Visible = true;
                    button_planilha_eng.Visible = true;
                }
                if (grupo == "Engenharia de Produtos")
                {
                    button_planilha_eng.Visible = true;
                }
                if (grupo == "WBC")
                {
                    InfoBancoBANCODEDADOS("%");
                    textBox_Banco.Visible = true;
                    comboBox_banco.Visible = true;
                    textBox_banco_dados.Visible = true;
                    comboBox_Instancia.Visible = true;
                    Panel_Sub_Eletrofrio.Visible = true;
                    button_comparativo.Visible = true;
                    button_cadastrarServidor.Visible = true;
                }

            }

            if (ListaBanco.Count > 0)
            {
                comboBox_Instancia.DataSource = ListaBanco.ToArray();
                comboBox_banco.DataSource = Buscar_banco().ToArray();
            }


        }

        private void CadastrarLocal()
        {
            string instancia = @"INSTANCIA";
            string Senha = Encrypt("SENHA");
            string ssqlconnectionstring = @"Data Source=INSTANCIA;Initial Catalog=BANCODEDADOS ;Persist Security Info=True;User ID=sa;Password=SENHA";
            string query_function = "if not exists (select * from BancoServidor where NomeInstancia = 'Local') \n" +
                                        "begin insert into BancoServidor values('Local', '" + Senha + "', '" + instancia + "', 'sa', 'Eletrofast_2019')  end";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query_function, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
        }

        private void InfoBancoBANCODEDADOS(string Banco)
        {
            ListaBanco.Clear();
            ListaSenhaBanco.Clear();
            ListaCaminhoBanco.Clear();
            ListaUsuarioBanco.Clear();

            string conString = @"Data Source=INSTANCIA;Initial Catalog=BANCODEDADOS ;Persist Security Info=True;User ID=sa;Password=SENHA";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from BancoServidor where NomeInstancia like '" + Banco + "'", con))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ListaBanco.Add(dr[0].ToString());
                            ListaSenhaBanco.Add(dr[1].ToString());
                            ListaCaminhoBanco.Add(dr[3].ToString());
                            ListaUsuarioBanco.Add(dr[4].ToString());
                        }
                    }
                }
            }

        }

        private void button_Fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Tabelas_SQL_Click(object sender, EventArgs e)
        {
            //string instancia = this.comboBox_Instancia.SelectedValue.ToString();
            string banco_dados = this.comboBox_banco.SelectedValue.ToString();
            if (banco_dados != "")
            {
                if (!Directory.Exists(exportar))
                {
                    Directory.CreateDirectory(exportar);
                }
                if (!Directory.Exists(importar))
                {
                    Directory.CreateDirectory(importar);
                }
                if (!Directory.Exists(importados))
                {
                    Directory.CreateDirectory(importados);
                }
                this.Close();
                t1 = new Thread(Form_Tabela_SQL);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }

            else
            {
                MessageBox.Show("Favor selecionar o banco de dados!");
            }
        }
        private void Form_Tabela_SQL(object obj)
        {
            //string banco_dados_servidor = this.comboBox_Instancia.SelectedValue.ToString();
            string banco_dados = this.comboBox_banco.SelectedValue.ToString();
            string instancia = this.comboBox_Instancia.SelectedValue.ToString();
            InfoBancoBANCODEDADOS(instancia);
            string SenhaDescrypt = Decrypt(ListaSenhaBanco[0]);
            if (banco_dados != "")
            {
                string instancia_local = ListaCaminhoBanco[0];
                string usuario_local = ListaUsuarioBanco[0];
                //string senha_local = ListaSenhaBanco[0];
                Application.Run(new Form_Tabela_SQL(valor_Gerenciador_SIP, banco_dados, instancia_local, SenhaDescrypt, usuario_local, Login));
            }
            else if (banco_dados == "")
            {
                MessageBox.Show("Form_Gerenciador_SIP - 163\n Selecionar banco de dados");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string banco_dados = this.comboBox_banco.SelectedValue.ToString();
            if (banco_dados != "")
            {
                this.Close();
                t1 = new Thread(Form_Bulk_Insert);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            else
            {
                MessageBox.Show("Favor selecionar o banco de dados!");
            }
        }
        private void Form_Bulk_Insert(object obj)
        {
            string banco_dados = this.comboBox_banco.SelectedValue.ToString();
            string instancia = this.comboBox_Instancia.SelectedValue.ToString();
            InfoBancoBANCODEDADOS(instancia);
            string SenhaDescrypt = Decrypt(ListaSenhaBanco[0]);
            if (banco_dados != "")
            {
                string instancia_local = ListaCaminhoBanco[0];
                string usuario_local = ListaUsuarioBanco[0];
                Application.Run(new Form_Bulk_Insert(valor_Gerenciador_SIP, banco_dados, instancia_local, SenhaDescrypt, usuario_local, Login));
            }
        }
        private void hideSubMenu_Eletrofrio()
        {
            if (Panel_Sub_Eletrofrio.Visible == true)
                Panel_Sub_Eletrofrio.Visible = false;
        }
        private void showSubMenu_Eletrofrio(Panel Eletrofrio)
        {
            button_chave_busca.Visible = true;
            button_Descritivo_tecnico.Visible = true;
            button_desenho.Visible = true;
            button_Traducao.Visible = true;
            button_comparativo.Visible = true;
            button_cabeceiras.Visible = true;
            if (Eletrofrio.Visible == false)
            {
                hideSubMenu_Eletrofrio();
                Eletrofrio.Visible = true;

            }
            else
                Eletrofrio.Visible = false;

        }

        private void button_Eletrofrio_Click(object sender, EventArgs e)
        {
            showSubMenu_Eletrofrio(Panel_Sub_Eletrofrio);
        }

        private void button_chave_busca_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_chave_busca);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_chave_busca(object obj)
        {
            string banco_dados = this.comboBox_banco.SelectedValue.ToString();
            string instancia = this.comboBox_Instancia.SelectedValue.ToString();
            InfoBancoBANCODEDADOS(instancia);
            string SenhaDescrypt = Decrypt(ListaSenhaBanco[0]);
            string instancia_local = ListaCaminhoBanco[0];
            string usuario_local = ListaUsuarioBanco[0];
            Application.Run(new Form_Chave_busca(valor_Gerenciador_SIP, banco_dados, instancia_local, SenhaDescrypt, usuario_local, Login));
        }

        private void button_desenho_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Desenho);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_Desenho(object obj)
        {
            string banco_dados = this.comboBox_banco.SelectedValue.ToString();
            string instancia = this.comboBox_Instancia.SelectedValue.ToString();
            InfoBancoBANCODEDADOS(instancia);
            string SenhaDescrypt = Decrypt(ListaSenhaBanco[0]);
            string instancia_local = ListaCaminhoBanco[0];
            string usuario_local = ListaUsuarioBanco[0];
            Application.Run(new Form_Desenho(valor_Gerenciador_SIP, banco_dados, instancia_local, SenhaDescrypt, usuario_local, Login));
        }

        private void button_Traducao_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Traducao);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_Traducao(object obj)
        {
            string banco_dados = this.comboBox_banco.SelectedValue.ToString();
            string instancia = this.comboBox_Instancia.SelectedValue.ToString();
            InfoBancoBANCODEDADOS(instancia);
            string SenhaDescrypt = Decrypt(ListaSenhaBanco[0]);
            string instancia_local = ListaCaminhoBanco[0];
            string usuario_local = ListaUsuarioBanco[0];
            Application.Run(new Form_Traducao(valor_Gerenciador_SIP, banco_dados, instancia_local, SenhaDescrypt, usuario_local, Login));
        }


        private void Form_Descritivo_tecnico(object obj)
        {
            string banco_dados = this.comboBox_banco.SelectedValue.ToString();
            string instancia = this.comboBox_Instancia.SelectedValue.ToString();
            InfoBancoBANCODEDADOS(instancia);
            string SenhaDescrypt = Decrypt(ListaSenhaBanco[0]);
            string instancia_local = ListaCaminhoBanco[0];
            string usuario_local = ListaUsuarioBanco[0];
            Application.Run(new Form_Descritivo_tecnico(valor_Gerenciador_SIP, banco_dados, instancia_local, SenhaDescrypt, usuario_local, Login));
        }

        private void button_Descritivo_tecnico_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Descritivo_tecnico);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }

        private void button_procurar_Click(object sender, EventArgs e)
        {
            string banco_dados = this.comboBox_Instancia.SelectedValue.ToString();
            if (banco_dados != "")
            {
                this.Close();
                t1 = new Thread(Form_Procurar);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            else
            {
                MessageBox.Show("Favor selecionar o banco de dados!");
            }
        }
        private void Form_Procurar(object obj)
        {
            string banco_dados = this.comboBox_banco.SelectedValue.ToString();
            string instancia = this.comboBox_Instancia.SelectedValue.ToString();
            InfoBancoBANCODEDADOS(instancia);
            string SenhaDescrypt = Decrypt(ListaSenhaBanco[0]);
            string instancia_local = ListaCaminhoBanco[0];
            string usuario_local = ListaUsuarioBanco[0];
            Application.Run(new Form_Procurar(valor_Gerenciador_SIP, banco_dados, instancia_local, SenhaDescrypt, usuario_local, Login));
        }

        private void button_tabelas_cabeceiras_Click(object sender, EventArgs e)
        {
            string banco_dados = this.comboBox_banco.SelectedValue.ToString();
            if (banco_dados != "")
            {
                this.Close();
                t1 = new Thread(Form_Exportar_tab_cab);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            else
            {
                MessageBox.Show("Favor selecionar o banco de dados!");
            }
        }
        private void Form_Exportar_tab_cab(object obj)
        {
            string banco_dados = this.comboBox_Instancia.SelectedValue.ToString();
            Application.Run(new Form_Exportar_tab_cab(valor_Gerenciador_SIP, banco_dados, Login));
        }

        private void button_comparativo_Click(object sender, EventArgs e)
        {
            string banco_dados = this.comboBox_banco.SelectedValue.ToString();
            if (banco_dados != "")
            {
                this.Close();
                t1 = new Thread(Form_Comparativo);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            else
            {
                MessageBox.Show("Favor selecionar o banco de dados!");
            }
        }
        private void Form_Comparativo(object obj)
        {
            string banco_dados = this.comboBox_banco.SelectedValue.ToString();
            string instancia = this.comboBox_Instancia.SelectedValue.ToString();
            InfoBancoBANCODEDADOS(instancia);
            string SenhaDescrypt = Decrypt(ListaSenhaBanco[0]);
            string instancia_local = ListaCaminhoBanco[0];
            string usuario_local = ListaUsuarioBanco[0];
            Application.Run(new Form_Comparativo(valor_Gerenciador_SIP, banco_dados, instancia_local, SenhaDescrypt, usuario_local, Login));
        }


        public List<string> Buscar_banco()
        {
            string InstanciaSelecionada = comboBox_Instancia.SelectedValue.ToString();
            InfoBancoBANCODEDADOS(InstanciaSelecionada);
            ListaBanco.Clear();
            string conString = @"Data Source=" + senhaLocal + ";Initial Catalog=BANCODEDADOS ;Persist Security Info=True;User ID=sa;Password=SENHA";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select '' as name union all " +
                    "select NomeBanco from BancoServidor where NomeInstancia like '" + InstanciaSelecionada + "'", con))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ListaBanco.Add(dr[0].ToString());
                        }
                    }
                }
            }
            return ListaBanco;
        }
        private void button_cabeceiras_Click(object sender, EventArgs e)
        {
            string banco_dados = this.comboBox_Instancia.SelectedValue.ToString();
            if (banco_dados != "")
            {
                this.Close();
                t1 = new Thread(Form_Cabeceiras);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            else
            {
                MessageBox.Show("Favor selecionar o banco de dados!");
            }
        }
        private void Form_Cabeceiras(object obj)
        {
            string banco_dados = this.comboBox_banco.SelectedValue.ToString();
            string instancia = this.comboBox_Instancia.SelectedValue.ToString();
            InfoBancoBANCODEDADOS(instancia);
            string SenhaDescrypt = Decrypt(ListaSenhaBanco[0]);
            string instancia_local = ListaCaminhoBanco[0];
            string usuario_local = ListaUsuarioBanco[0];
            Application.Run(new Form_Cabeceiras(valor_Gerenciador_SIP, banco_dados, instancia_local, SenhaDescrypt, usuario_local, Login));
        }

        private void comboBox_Instancia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox_banco.DataSource = Buscar_banco().ToArray();
        }

        private void button_cadastrarServidor_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_CadastroServidor);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }

        private void Form_CadastroServidor(object obj)
        {
            Application.Run(new CadastroServidor(valor_Gerenciador_SIP, Login));
        }

        public string Decrypt(string cipher)
        {
            byte[] data = Convert.FromBase64String(cipher);
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.LocalMachine);
            return Encoding.Unicode.GetString(decrypted);
        }

        public string Encrypt(string plainText)
        {
            var data = Encoding.Unicode.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(encrypted);
        }

        private void button_planilha_eng_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_planilha_eng);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_planilha_eng(object obj)
        {
            Cursor.Current = Cursors.WaitCursor;

            Application.Run(new Form_Planilha_Eng(valor_Gerenciador_SIP, Login));
            Cursor.Current = Cursors.Default;
        }

        private void button_diretoria_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Diretoria);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }
        private void Form_Diretoria(object obj)
        {
            Cursor.Current = Cursors.WaitCursor;

            Application.Run(new Form_Relatorio_Diretoria(valor_Gerenciador_SIP, Login));
            Cursor.Current = Cursors.Default;
        }
    }
}
