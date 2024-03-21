using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class Form_Procurar : Form
    {
        string Banco;
        string Instancia;
        string Senha;
        string Usuario;
        Thread t1;
        List<string> valor_Procurar;
        string Login;

        public Form_Procurar(List<string> valor, string banco, string instancia, string senha, string usuario, string login)
        {
            Login = login;
            Banco = banco;
            Instancia = instancia;
            Senha = senha;
            Usuario = usuario;
            InitializeComponent();
            valor_Procurar = valor;
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
            Application.Run(new Form_Gerenciador_SIP(valor_Procurar,Login));
        }

        private void button_Atualizar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "BEGIN CREATE TABLE #Results (ColumnName nvarchar(370), ColumnValue nvarchar(3630)) SET NOCOUNT ON \n"
                            + "DECLARE @TableName nvarchar(256), @ColumnName nvarchar(128), @SearchStr2 nvarchar(110) \n"
                            + "SET  @TableName = '' \n"
                            + "SET @SearchStr2 = QUOTENAME('%' + '" + textBox2.Text + "' + '%','''') WHILE @TableName IS NOT NULL \n"
                            + "BEGIN SET @ColumnName = '' SET @TableName = (SELECT MIN(QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME)) FROM INFORMATION_SCHEMA.TABLES \n"
                            + "WHERE TABLE_TYPE = 'BASE TABLE'	AND	QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) > @TableName	AND	OBJECTPROPERTY(OBJECT_ID( \n"
                            + "QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME)), 'IsMSShipped') = 0) \n"
                            + "WHILE (@TableName IS NOT NULL) AND (@ColumnName IS NOT NULL) \n"
                            + "BEGIN SET @ColumnName =(SELECT MIN(QUOTENAME(COLUMN_NAME))FROM INFORMATION_SCHEMA.COLUMNS \n"
                            + "WHERE TABLE_SCHEMA	= PARSENAME(@TableName, 2) AND	TABLE_NAME	= PARSENAME(@TableName, 1) AND	DATA_TYPE IN ('char', 'varchar', 'nchar', 'nvarchar') \n"
                            + "AND	QUOTENAME(COLUMN_NAME) > @ColumnName) IF @ColumnName IS NOT NULL \n"
                            + "BEGIN INSERT INTO #Results EXEC ('SELECT distinct ''' + @TableName + '.' + @ColumnName + ''', LEFT(' + @ColumnName + ', 3630) FROM ' + @TableName + ' (NOLOCK) ' +' WHERE ' + @ColumnName + ' LIKE ' + @SearchStr2) \n"
                            + "END END	END SELECT ColumnName, ColumnValue FROM #Results END";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.CommandTimeout = 200;
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Cursor.Current = Cursors.Default;
        }
    }
}
