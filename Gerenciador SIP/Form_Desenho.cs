using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class Form_Desenho : Form
    {
        string Banco;
        string Instancia;
        string Senha;
        string Usuario;
        Thread t1;
        string Login;

        List<string> valor_Desenho;
        public Form_Desenho(List<string> valor, string banco, string instancia, string senha, string usuario, string login)
        {
            Banco = banco;
            Instancia = instancia;
            Senha = senha;
            Usuario = usuario;
            InitializeComponent();
            valor_Desenho = valor;
            Login = login;
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
            Application.Run(new Form_Gerenciador_SIP(valor_Desenho, Login));
        }

        private void button_Atualizar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            criar_tabela();
            inserindo_dados();
            formatando_tabela();
            consultando();
            deletando_tabela();
            Cursor.Current = Cursors.Default;
        }
        private void deletando_tabela()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string sclearsql = "drop table temp1";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(sclearsql, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
        }
        private void consultando()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string sclearsql = "DECLARE @DelimitedString2 NVARCHAR(128);\n" +
                "SET @DelimitedString2 = (select distinct  lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR2');\n" +
                "Create table #PAR2 ( PAR2 varchar (128));\n" +
                "insert into #PAR2 select b.data as PAR2 from dbo.Split3(@DelimitedString2,'|') B;\n" +
                "DECLARE @DelimitedString3 NVARCHAR(128);\n" +
                "SET @DelimitedString3 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR3');\n" +
                "create table #PAR3 ( PAR3 varchar (128)) insert into #PAR3 select  C.data as PAR3 from dbo.Split3(@DelimitedString3,'|') C;\n" +
                "DECLARE @DelimitedString8 NVARCHAR(128);\n" +
                "SET @DelimitedString8 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR8') ;\n" +
                "create table #par8 ( par8 varchar (128)) ;\n" +
                "insert into #par8 select  h.data as PAR8 from dbo.Split3(@DelimitedString8,'|') H ;\n" +
                "create table #tblpar2 ( corte nvarchar (40),acessorio nvarchar (50),desenho nvarchar (50));\n" +
                "insert into #tblpar2 (corte,acessorio,desenho) select gab_gabacsg.corte,GAB_ACSG.acessorio,gab_acsg.desenho from GAB_ACSG inner join gab_gabacsg on gab_gabacsg.acessorio = gab_acsg.acessorio where desenho not like '' and desenho like '%<par2%' ;\n" +
                "create table #tblpar3 (corte nvarchar (40),acessorio nvarchar (50),desenho nvarchar (50)) ;\n" +
                "insert into #tblpar3 (corte,acessorio,desenho) select gab_gabacsg.corte,GAB_ACSG.acessorio,gab_acsg.desenho from GAB_ACSG inner join gab_gabacsg on gab_gabacsg.acessorio = gab_acsg.acessorio where desenho not like '' and desenho like '%<par3%';\n" +
                "create table #tblpar8 (corte nvarchar (40),acessorio nvarchar (50),desenho nvarchar (50)) ;\n" +
                "insert into #tblpar8 (corte,acessorio,desenho) select gab_gabacsg.corte,GAB_ACSG.acessorio,replace(gab_acsg.desenho,'<modelo>',gab_gabacsg.corte) from GAB_ACSG inner join gab_gabacsg on gab_gabacsg.acessorio = gab_acsg.acessorio where desenho not like '' and desenho like '%<par8%';\n" +
                "create table #temp1 (corte nvarchar (100),acessorio nvarchar (255),desenho nvarchar (255));\n " +
                "insert into #temp1 select GAB_CRTGAB.codigo,GAB_CRTGAB.prefixo_desenho,replace (replace (replace (replace (concat (GAB_CRTGAB.prefixo_desenho, GAB_PARAM_MED_CRT.medida),'E4545','E45'),'I4545','I45'),'E9090','E90'),'I9090','I90') from GAB_CRTGAB \n" +
                "inner join GAB_PARAM_MED_CRT on GAB_CRTGAB.codigo = GAB_PARAM_MED_CRT.Corte\n" +
                "union all\n" +
                "select GAB_CRTGAB.codigo, GAB_CRTGAB.prefixo_desenho,replace(replace(replace(replace(concat(GAB_CRTGAB.prefixo_desenho, GAB_PARAM_MED_CRT.medida), 'E4545', 'E45'), 'I4545', 'I45'), 'E9090', 'E90'), 'I9090', 'I90') + '-exi' from GAB_CRTGAB \n" +
                "inner join GAB_PARAM_MED_CRT on GAB_CRTGAB.codigo = GAB_PARAM_MED_CRT.Corte \n" +
                "union all \n" +
                "select corte,acessorio,replace(desenho, '<par2>',#par2.par2) from #tblpar2 \n" +
                "cross join #par2 \n" +
                "union all \n" +
                "select corte, acessorio, replace(desenho, '<par3>',#par3.par3) from #tblpar3 \n" +
                "cross join #par3 \n" +
                "union all \n" +
                "select corte, acessorio, replace(desenho, '<par8>',#par8.par8) from #tblpar8 \n" +
                "cross join #par8 \n" +
                "union all \n" +
                "select gab_gabacsg.corte, GAB_ACSG.acessorio, replace(gab_acsg.desenho, '<modelo>', gab_gabacsg.corte) desenho from GAB_ACSG \n" +
                "inner join gab_gabacsg on gab_gabacsg.acessorio = gab_acsg.acessorio where desenho is not null and desenho not like '' and desenho not like '%<PAR%>%' \n" +
                "union all \n" +
                "select GAB_CABDET.ligacao, GAB_CAB.descricao, GAB_CAB.desenho from GAB_CAB \n" +
                "inner join GAB_CABDET on GAB_CABDET.descricao = GAB_CAB.descricao \n" +
                "select #temp1.*,temp1.refr from #temp1 \n" +
                "left join temp1 on #temp1.desenho = temp1.Refr \n" +
                "where corte like '" + textBox2.Text + "';";


            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(sclearsql, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void criar_tabela()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string sclearsql = "CREATE TABLE temp1 (ID int IDENTITY(1,1) PRIMARY KEY,Refr varchar(255) NOT NULL);";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(sclearsql, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();

        }
        private void inserindo_dados()
        {

            string StrQuery;
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            String[] files = Directory.GetFiles(@"C:\WBC\Refr", "*.dwg");
            DataTable table = new DataTable();
            table.Columns.Add("Refr");

            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = new FileInfo(files[i]); ;
                using (SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring))
                {
                    using (SqlCommand comm = new SqlCommand())
                    {
                        comm.Connection = sqlconn;
                        sqlconn.Open();
                        StrQuery = @"INSERT INTO temp1 VALUES ('" + file.Name + "');";
                        comm.CommandText = StrQuery;
                        comm.ExecuteNonQuery();
                    }
                }
            }
        }
        private void formatando_tabela()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string sclearsql = "update temp1 set Refr = REPLACE (Refr,'.dwg','');";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(sclearsql, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
        }

    }
}
