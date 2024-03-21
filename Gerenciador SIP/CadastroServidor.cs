using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class CadastroServidor : Form
    {
        Thread t1;
        List<string> valor_Gerenciador_SIP;
        string Login;
        public CadastroServidor(List<string> valorGerenciador, string login)
        {
            Login = login;
            valor_Gerenciador_SIP = valorGerenciador;
            InitializeComponent();
            BuscarConfiguracoes();
        }
        private void BuscarConfiguracoes()
        {
            Cursor.Current = Cursors.WaitCursor;
            SqlConnection con = new SqlConnection(@"Data Source=INSTANCIA;Initial Catalog=BANCODEDADOS;Persist Security Info=True;User ID=sa;Password=SENHA");
            var query1 = "SELECT [NomeInstancia],[CaminhoBanco],[NomeBanco],[UsuarioBanco],[SenhaBanco],[idBanco]  FROM [BANCODEDADOS].[dbo].[BancoServidor]";
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
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string SenhaCrypt = Encrypt(textBox_senha.Text);
            Cursor.Current = Cursors.WaitCursor;
            SqlConnection con = new SqlConnection(@"Data Source=INSTANCIA;Initial Catalog=BANCODEDADOS;Persist Security Info=True;User ID=sa;Password=SENHA");
            var query1 = "insert into BancoServidor  ([NomeInstancia],[SenhaBanco],[CaminhoBanco],[UsuarioBanco],[NomeBanco]) values ('" + textBox_NomeConfiguracao.Text + "','" + SenhaCrypt + "','" + textBox_NomeServidor.Text + "','" + textBox_usuario.Text + "','" + textBox_NomeBanco.Text + "')";
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query1, con, transaction);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            con.Close();
            Cursor.Current = Cursors.Default;
            BuscarConfiguracoes();
        }


        public string Encrypt(string plainText)
        {
            //encrypt data
            var data = Encoding.Unicode.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.LocalMachine);
            //return as base64 string
            return Convert.ToBase64String(encrypted);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string cellValue = Convert.ToString(selectedRow.Cells["idBanco"].Value);

                SqlConnection con = new SqlConnection(@"Data Source=INSTANCIA;Initial Catalog=BANCODEDADOS;Persist Security Info=True;User ID=sa;Password=SENHA");
                var query1 = "delete from BancoServidor where idBanco = '" + cellValue + "'";
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                SqlCommand cmd = new SqlCommand(query1, con, transaction);
                cmd.ExecuteNonQuery();
                transaction.Commit();
                con.Close();
            }
            Cursor.Current = Cursors.Default;
            BuscarConfiguracoes();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_GerenciadorSip);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }

        private void Form_GerenciadorSip(object obj)
        {
            Application.Run(new Form_Gerenciador_SIP(valor_Gerenciador_SIP, Login));
        }

    }
}
