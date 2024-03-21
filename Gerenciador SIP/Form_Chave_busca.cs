using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class Form_Chave_busca : Form
    {
        string Banco;
        string Instancia;
        string Senha;
        string Usuario;
        Thread t1;
        List<string> valor_Chave_busca;
        string Login;

        public Form_Chave_busca(List<string> valor, string banco, string instancia, string senha, string usuario, string login)
        {
            Banco = banco;
            Instancia = instancia;
            Senha = senha;
            Usuario = usuario;
            InitializeComponent();
            valor_Chave_busca = valor;
            Login = login;  
        }
        private void create_split()
        {
            try
            {
                drop_split();
            }
            catch
            {

            }
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query_function = "Create FUNCTION [dbo].[Split3](@String NVARCHAR(4000),@Delimiter NCHAR(1)) \n" +
                            "RETURNS TABLE AS RETURN( \n" +
                            "WITH Split(stpos, endpos) AS( \n" +
                            "SELECT 0 AS stpos, CHARINDEX(@Delimiter, @String) AS endpos \n" +
                            "UNION ALL \n" +
                            "SELECT endpos + 1, CHARINDEX(@Delimiter, @String, endpos + 1) FROM Split WHERE endpos > 0) \n" +
                            "SELECT 'Id' = ROW_NUMBER() OVER(ORDER BY(SELECT 1)), 'Data' = SUBSTRING(@String, stpos, COALESCE(NULLIF(endpos, 0), LEN(@String) + 1) - stpos)FROM Split);";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query_function, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
        }
        private void drop_split()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string quero_drop_function = "drop function [Split3];";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(quero_drop_function, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
        }
        private void planilhao()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "create table #Append (corte nvarchar (50),acessorio nvarchar (255)) \n" +
                            "insert into #Append select distinct	gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte, acessorio from gab_gabacsg; \n" +
            "create table #Marge3 (chave_busca_1 nvarchar (255),chave_busca nvarchar (255))  \n" +
                            "insert into #Marge3 select distinct	replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca_1,GAB_ACSG.chave_busca	 \n" +
                            "from #Append inner join GAB_PARAM_MED_CRT on #Append.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append.acessorio = GAB_ACSG.acessorio;  \n" +
                            "Create table #Append1 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append1 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP1 (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP1 select distinct 	replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append1.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca,GAB_ACSG.chave_busca as chave  \n" +
                            "from #Append1 inner join GAB_PARAM_MED_CRT on #Append1.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append1.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par1>%';  \n" +
                            "create table #Append2 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append2 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP2 (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP2 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append2.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append2 inner join GAB_PARAM_MED_CRT on #Append2.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append2.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par2>%';  \n" +
                            "Create table #Append3 (corte nvarchar (50),acessorio nvarchar (255))   \n" +
                            "insert into #Append3 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP3  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP3 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append3.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca	 ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append3 inner join GAB_PARAM_MED_CRT on #Append3.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append3.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par3>%';  \n" +
                            "Create table #Append4 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append4 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP4  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP4 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append4.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append4 inner join GAB_PARAM_MED_CRT on #Append4.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append4.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par4>%';  \n" +
                            "Create table #Append5 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append5 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP5  (chave_busca nvarchar (255), chave nvarchar (255))   \n" +
                            "insert into #PAR_TEMP5 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append5.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca	 ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append5 inner join GAB_PARAM_MED_CRT on #Append5.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append5.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par5>%';  \n" +
                            "Create table #Append6 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append6 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP6  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP6 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append6.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca	 ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append6 inner join GAB_PARAM_MED_CRT on #Append6.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append6.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par6>%';  \n" +
                            "Create table #Append7 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append7 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP7  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP7 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append7.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append7 inner join GAB_PARAM_MED_CRT on #Append7.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append7.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par7>%';  \n" +
                            "Create table #Append8 (corte nvarchar (50),acessorio nvarchar (255))insert into #Append8 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP8  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP8 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append8.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca	 ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append8 inner join GAB_PARAM_MED_CRT on #Append8.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append8.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par8>%'  \n" +
                            "union all \n" +
                            "select distinct replace(replace(GAB_CAB.chave_busca, '<MODELO>', GAB_CABDET.ligacao), '<COMPR>', GAB_PARAM_MED_CRT.medida) as chave_busca,GAB_CAB.chave_busca as chave  from GAB_CAB inner join GAB_CABDET on GAB_CAB.descricao = GAB_CABDET.descricao full outer join GAB_PARAM_MED_CRT on GAB_CABDET.ligacao = GAB_PARAM_MED_CRT.Corte where gab_cab.chave_busca like '%PAR8%'; Create table #Append9 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append9 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte, acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP9  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP9 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append9.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca	 ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append9 inner join GAB_PARAM_MED_CRT on #Append9.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append9.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par9>%';  \n" +
                            "Create table #Append10 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append10 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP10  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP10 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append10.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append10 inner join GAB_PARAM_MED_CRT on #Append10.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append10.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par10>%';  \n" +
                            "Create table #Append11 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append11 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP11  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP11 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append11.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append11 inner join GAB_PARAM_MED_CRT	on #Append11.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append11.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par11>%';  \n" +
                            "Create table #Append12 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append12 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP12  (chave_busca nvarchar (255),chave nvarchar (255))   \n" +
                            "insert into #PAR_TEMP12 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append12.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append12 inner join GAB_PARAM_MED_CRT on #Append12.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append12.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par12>%';  \n" +
                            "DECLARE @DelimitedString1 NVARCHAR(128) SET @DelimitedString1 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR1')  \n" +
                            "Create table #PAR1 ( PAR1 varchar (128)) insert into #PAR1 SELECT a.data as PAR1 FROM dbo.split3(@DelimitedString1,'|') A;  \n" +
                            "DECLARE @DelimitedString2 NVARCHAR(128) SET @DelimitedString2 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR2')  \n" +
                            "Create table #PAR2 ( PAR2 varchar (128)) insert into #PAR2 select b.data as PAR2 from dbo.Split3(@DelimitedString2,'|') B;  \n" +
                            "DECLARE @DelimitedString3 NVARCHAR(128) SET @DelimitedString3 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR3')  \n" +
                            "create table #PAR3 ( PAR3 varchar (128)) insert into #PAR3 select C.data as PAR3 from dbo.Split3(@DelimitedString3,'|') C;  \n" +
                            "DECLARE @DelimitedString4 NVARCHAR(128) SET @DelimitedString4 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR4')  \n" +
                            "create table #PAR4 ( PAR4 varchar (128)) insert into #PAR4 select d.data as PAR4 from dbo.Split3(@DelimitedString4,'|') D;  \n" +
                            "DECLARE @DelimitedString5 NVARCHAR(128) SET @DelimitedString5 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR5')  \n" +
                            "create table #PAR5 ( PAR5 varchar (128)) insert into #PAR5 select e.data as PAR5 from dbo.Split3(@DelimitedString5,'|') E;  \n" +
                            "DECLARE @DelimitedString6 NVARCHAR(128) SET @DelimitedString6 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR6')  \n" +
                            "create table #PAR6 ( PAR6 varchar (128)) insert into #PAR6 select f.data as PAR6 from dbo.Split3(@DelimitedString6,'|') F;  \n" +
                            "DECLARE @DelimitedString7 NVARCHAR(128) SET @DelimitedString7 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR7')  \n" +
                            "create table #PAR7 ( PAR7 varchar (128)) insert into #PAR7 select g.data as PAR7 from dbo.Split3(@DelimitedString7,'|') G;  \n" +
                            "DECLARE @DelimitedString8 NVARCHAR(128) SET @DelimitedString8 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR8')   \n" +
                            "create table #par8 ( par8 varchar (128)) insert into #par8 select h.data as PAR8 from dbo.Split3(@DelimitedString8,'|') H;  \n" +
                            "DECLARE @DelimitedString9 NVARCHAR(128) SET @DelimitedString9 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR9')  \n" +
                            "create table #PAR9 ( PAR9 varchar (128)) insert into #PAR9 select i.data as PAR9 from dbo.Split3(@DelimitedString9,'|') I;  \n" +
                            "DECLARE @DelimitedString10 NVARCHAR(128) SET @DelimitedString10 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR10')  \n" +
                            "create table #PAR10 ( PAR10 varchar (128)) insert into #PAR10 	select J.data as PAR10 from dbo.Split3(@DelimitedString10,'|') J;  \n" +
                            "DECLARE @DelimitedString11 NVARCHAR(128) SET @DelimitedString11 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR11')  \n" +
                            "create table #PAR11 ( PAR11 varchar (128)) insert into #PAR11 select K.data as PAR11 from dbo.Split3(@DelimitedString11,'|') K;  \n" +
                            "DECLARE @DelimitedString12 NVARCHAR(128) SET @DelimitedString12 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR12')  \n" +
                            "create table #PAR12 ( PAR12 varchar (128)) insert into #PAR12 select L.data as PAR12 from dbo.Split3(@DelimitedString12,'|') L;  \n" +
                            "create table #chave_busca_teste1 (chave_busca_1 varchar (100),chave_busca varchar (100))  \n" +
                            "insert into #chave_busca_teste1 select distinct replace(replace(chave_busca,'<PAR1>',#PAR1.PAR1),'<PAR10>',#PAR10.PAR10), chave_busca  from #PAR_TEMP1 cross join #PAR1 cross join #PAR10  \n" +
                            "union all \n" +
                            "select distinct replace(chave_busca, '<PAR2>',#PAR2.PAR2), chave_busca from #PAR_TEMP2 cross join #PAR2  \n" +
                            "union all \n" +
                            "select distinct replace(chave_busca, '<PAR3>',#PAR3.PAR3), chave_busca from #PAR_TEMP3 cross join #PAR3  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par4>',#PAR4.PAR4), chave_busca from #PAR_TEMP4 cross join #PAR4  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par5>',#PAR5.PAR5), chave_busca from #PAR_TEMP5 cross join #PAR5  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par6>',#PAR6.PAR6), chave_busca from #PAR_TEMP6 cross join #PAR6   \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par7>',#PAR7.PAR7), chave_busca from #PAR_TEMP7 cross join #PAR7  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par8>',#par8.par8), chave_busca from #PAR_TEMP8 cross join #par8  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par9>',#PAR9.PAR9), chave_busca from #PAR_TEMP9 cross join #PAR9  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par10>',#PAR10.PAR10), chave_busca from #PAR_TEMP10 cross join #PAR10  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par11>',#PAR11.PAR11), chave_busca from #PAR_TEMP11 cross join #PAR11  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par12>',#PAR12.PAR12), chave as chave_busca from #PAR_TEMP12 cross join #PAR12;  \n" +
                            "create table #chave_busca_final (chave_busca_1 varchar (100),chave_busca varchar (100))   \n" +
                            "insert into #chave_busca_final select * from #chave_busca_teste1 where chave_busca is not null  \n" +
                            "union all \n" +
                            "select * from #marge3 where chave_busca not like '%<PAR%'  \n" +
                            "union all \n" +
                            "select distinct replace(replace(replace(GAB_CAB.chave_busca, '<MODELO>', gab_cabdet.ligacao), '<COMPR>', GAB_PARAM_MED_CRT.medida), '<PAR4>',#PAR4.PAR4) as chave_busca_1, GAB_CAB.chave_busca from GAB_CAB inner join GAB_CABDET on gab_cab.descricao = GAB_CABDET.descricao inner join GAB_PARAM_MED_CRT on GAB_CABDET.ligacao = GAB_PARAM_MED_CRT.Corte cross join #par4 where chave_busca not like '%<PAR8>'  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<COMPR>', GAB_PARAM_MED_CRT.medida), chave_busca from GAB_CRTGAB inner join GAB_PARAM_MED_CRT on GAB_CRTGAB.codigo = GAB_PARAM_MED_CRT.Corte \n" +
                            "union all \n" +
                            "select distinct replace(replace(replace(replace(GAB_ACSG.chave_busca, '<PROF>', GAB_CRTGAB.profundidade), '<MODELO>', GAB_CRTGAB.codigo), '<COMPR>', GAB_PARAM_MED_CRT.medida), '<PAR4>',#PAR4.PAR4) as chave_busca_1, GAB_ACSG.chave_busca from GAB_CAB  \n" +
                            "inner join gab_acsg_cab on GAB_CAB.descricao = gab_acsg_cab.cabeceira \n" +
                            "inner join GAB_ACSG on gab_acsg_cab.acessorio = GAB_ACSG.acessorio \n" +
                            "inner join GAB_CABDET on GAB_CAB.descricao = GAB_CABDET.descricao \n" +
                            "inner join GAB_CRTGAB on GAB_CABDET.ligacao = GAB_CRTGAB.codigo \n" +
                            "inner join GAB_PARAM_MED_CRT on GAB_CRTGAB.codigo = GAB_PARAM_MED_CRT.Corte \n" +
                            "cross join #par4;  \n" +
                            "select distinct #chave_busca_final.chave_busca_1,#chave_busca_final.chave_busca,prdchb.chave_busca_montada,prdchb.produto,prdorc.descricao,PRDCHB.Esconder_orcamento from #chave_busca_final   \n" +
                            "full outer join prdchb on #chave_busca_final.chave_busca_1 = prdchb.chave_busca_montada left join prdorc on prdchb.produto = prdorc.produto   \n" +
                            "where #chave_busca_final.chave_busca_1 like '" + textBox2.Text + "';";

            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void planilhao_importar()
        {
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "create table #Append (corte nvarchar (50),acessorio nvarchar (255)) \n" +
                            "insert into #Append select distinct	gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte, acessorio from gab_gabacsg; \n" +
            "create table #Marge3 (chave_busca_1 nvarchar (255),chave_busca nvarchar (255))  \n" +
                            "insert into #Marge3 select distinct	replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca_1,GAB_ACSG.chave_busca	 \n" +
                            "from #Append inner join GAB_PARAM_MED_CRT on #Append.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append.acessorio = GAB_ACSG.acessorio;  \n" +
                            "Create table #Append1 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append1 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP1 (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP1 select distinct 	replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append1.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca,GAB_ACSG.chave_busca as chave  \n" +
                            "from #Append1 inner join GAB_PARAM_MED_CRT on #Append1.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append1.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par1>%';  \n" +
                            "create table #Append2 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append2 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP2 (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP2 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append2.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append2 inner join GAB_PARAM_MED_CRT on #Append2.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append2.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par2>%';  \n" +
                            "Create table #Append3 (corte nvarchar (50),acessorio nvarchar (255))   \n" +
                            "insert into #Append3 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP3  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP3 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append3.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca	 ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append3 inner join GAB_PARAM_MED_CRT on #Append3.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append3.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par3>%';  \n" +
                            "Create table #Append4 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append4 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP4  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP4 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append4.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append4 inner join GAB_PARAM_MED_CRT on #Append4.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append4.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par4>%';  \n" +
                            "Create table #Append5 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append5 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP5  (chave_busca nvarchar (255), chave nvarchar (255))   \n" +
                            "insert into #PAR_TEMP5 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append5.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca	 ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append5 inner join GAB_PARAM_MED_CRT on #Append5.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append5.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par5>%';  \n" +
                            "Create table #Append6 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append6 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP6  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP6 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append6.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca	 ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append6 inner join GAB_PARAM_MED_CRT on #Append6.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append6.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par6>%';  \n" +
                            "Create table #Append7 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append7 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP7  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP7 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append7.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append7 inner join GAB_PARAM_MED_CRT on #Append7.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append7.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par7>%';  \n" +
                            "Create table #Append8 (corte nvarchar (50),acessorio nvarchar (255))insert into #Append8 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP8  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP8 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append8.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca	 ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append8 inner join GAB_PARAM_MED_CRT on #Append8.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append8.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par8>%'  \n" +
                            "union all \n" +
                            "select distinct replace(replace(GAB_CAB.chave_busca, '<MODELO>', GAB_CABDET.ligacao), '<COMPR>', GAB_PARAM_MED_CRT.medida) as chave_busca,GAB_CAB.chave_busca as chave  from GAB_CAB inner join GAB_CABDET on GAB_CAB.descricao = GAB_CABDET.descricao full outer join GAB_PARAM_MED_CRT on GAB_CABDET.ligacao = GAB_PARAM_MED_CRT.Corte where gab_cab.chave_busca like '%PAR8%'; Create table #Append9 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append9 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte, acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP9  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP9 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append9.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca	 ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append9 inner join GAB_PARAM_MED_CRT on #Append9.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append9.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par9>%';  \n" +
                            "Create table #Append10 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append10 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP10  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP10 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append10.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append10 inner join GAB_PARAM_MED_CRT on #Append10.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append10.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par10>%';  \n" +
                            "Create table #Append11 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append11 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP11  (chave_busca nvarchar (255), chave nvarchar (255))  \n" +
                            "insert into #PAR_TEMP11 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append11.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca ,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append11 inner join GAB_PARAM_MED_CRT	on #Append11.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append11.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par11>%';  \n" +
                            "Create table #Append12 (corte nvarchar (50),acessorio nvarchar (255))  \n" +
                            "insert into #Append12 select distinct gab_gabacsg.corte,gab_obracsg.acessorio_obrigatorio as acessorio from gab_gabacsg inner join gab_obracsg on gab_gabacsg.acessorio = gab_obracsg.acessorio  \n" +
                            "union all \n" +
                            "select corte,acessorio from gab_gabacsg; \n" +
            "create table #PAR_TEMP12  (chave_busca nvarchar (255),chave nvarchar (255))   \n" +
                            "insert into #PAR_TEMP12 select distinct replace(replace(replace(GAB_ACSG.chave_busca,'<MODELO>',#Append12.corte),'<COMPR>',GAB_PARAM_MED_CRT.medida),'<ANGULO>',GAB_PARAM_MED_CRT.tipo_med) as chave_busca,GAB_ACSG.chave_busca as chave \n" +
                            "from #Append12 inner join GAB_PARAM_MED_CRT on #Append12.corte = GAB_PARAM_MED_CRT.Corte inner join GAB_ACSG on #Append12.acessorio = GAB_ACSG.acessorio where chave_busca like '%<par12>%';  \n" +
                            "DECLARE @DelimitedString1 NVARCHAR(128) SET @DelimitedString1 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR1')  \n" +
                            "Create table #PAR1 ( PAR1 varchar (128)) insert into #PAR1 SELECT a.data as PAR1 FROM dbo.split3(@DelimitedString1,'|') A;  \n" +
                            "DECLARE @DelimitedString2 NVARCHAR(128) SET @DelimitedString2 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR2')  \n" +
                            "Create table #PAR2 ( PAR2 varchar (128)) insert into #PAR2 select b.data as PAR2 from dbo.Split3(@DelimitedString2,'|') B;  \n" +
                            "DECLARE @DelimitedString3 NVARCHAR(128) SET @DelimitedString3 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR3')  \n" +
                            "create table #PAR3 ( PAR3 varchar (128)) insert into #PAR3 select C.data as PAR3 from dbo.Split3(@DelimitedString3,'|') C;  \n" +
                            "DECLARE @DelimitedString4 NVARCHAR(128) SET @DelimitedString4 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR4')  \n" +
                            "create table #PAR4 ( PAR4 varchar (128)) insert into #PAR4 select d.data as PAR4 from dbo.Split3(@DelimitedString4,'|') D;  \n" +
                            "DECLARE @DelimitedString5 NVARCHAR(128) SET @DelimitedString5 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR5')  \n" +
                            "create table #PAR5 ( PAR5 varchar (128)) insert into #PAR5 select e.data as PAR5 from dbo.Split3(@DelimitedString5,'|') E;  \n" +
                            "DECLARE @DelimitedString6 NVARCHAR(128) SET @DelimitedString6 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR6')  \n" +
                            "create table #PAR6 ( PAR6 varchar (128)) insert into #PAR6 select f.data as PAR6 from dbo.Split3(@DelimitedString6,'|') F;  \n" +
                            "DECLARE @DelimitedString7 NVARCHAR(128) SET @DelimitedString7 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR7')  \n" +
                            "create table #PAR7 ( PAR7 varchar (128)) insert into #PAR7 select g.data as PAR7 from dbo.Split3(@DelimitedString7,'|') G;  \n" +
                            "DECLARE @DelimitedString8 NVARCHAR(128) SET @DelimitedString8 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR8')   \n" +
                            "create table #par8 ( par8 varchar (128)) insert into #par8 select h.data as PAR8 from dbo.Split3(@DelimitedString8,'|') H;  \n" +
                            "DECLARE @DelimitedString9 NVARCHAR(128) SET @DelimitedString9 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR9')  \n" +
                            "create table #PAR9 ( PAR9 varchar (128)) insert into #PAR9 select i.data as PAR9 from dbo.Split3(@DelimitedString9,'|') I;  \n" +
                            "DECLARE @DelimitedString10 NVARCHAR(128) SET @DelimitedString10 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR10')  \n" +
                            "create table #PAR10 ( PAR10 varchar (128)) insert into #PAR10 	select J.data as PAR10 from dbo.Split3(@DelimitedString10,'|') J;  \n" +
                            "DECLARE @DelimitedString11 NVARCHAR(128) SET @DelimitedString11 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR11')  \n" +
                            "create table #PAR11 ( PAR11 varchar (128)) insert into #PAR11 select K.data as PAR11 from dbo.Split3(@DelimitedString11,'|') K;  \n" +
                            "DECLARE @DelimitedString12 NVARCHAR(128) SET @DelimitedString12 = (select distinct lista from tblDados_projeto where grupo like 'não usar' and codigochave = 'PAR12')  \n" +
                            "create table #PAR12 ( PAR12 varchar (128)) insert into #PAR12 select L.data as PAR12 from dbo.Split3(@DelimitedString12,'|') L;  \n" +
                            "create table #chave_busca_teste1 (chave_busca_1 varchar (100),chave_busca varchar (100))  \n" +
                            "insert into #chave_busca_teste1 select distinct replace(replace(chave_busca,'<PAR1>',#PAR1.PAR1),'<PAR10>',#PAR10.PAR10), chave_busca  from #PAR_TEMP1 cross join #PAR1 cross join #PAR10  \n" +
                            "union all \n" +
                            "select distinct replace(chave_busca, '<PAR2>',#PAR2.PAR2), chave_busca from #PAR_TEMP2 cross join #PAR2  \n" +
                            "union all \n" +
                            "select distinct replace(chave_busca, '<PAR3>',#PAR3.PAR3), chave_busca from #PAR_TEMP3 cross join #PAR3  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par4>',#PAR4.PAR4), chave_busca from #PAR_TEMP4 cross join #PAR4  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par5>',#PAR5.PAR5), chave_busca from #PAR_TEMP5 cross join #PAR5  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par6>',#PAR6.PAR6), chave_busca from #PAR_TEMP6 cross join #PAR6   \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par7>',#PAR7.PAR7), chave_busca from #PAR_TEMP7 cross join #PAR7  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par8>',#par8.par8), chave_busca from #PAR_TEMP8 cross join #par8  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par9>',#PAR9.PAR9), chave_busca from #PAR_TEMP9 cross join #PAR9  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par10>',#PAR10.PAR10), chave_busca from #PAR_TEMP10 cross join #PAR10  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par11>',#PAR11.PAR11), chave_busca from #PAR_TEMP11 cross join #PAR11  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<par12>',#PAR12.PAR12), chave as chave_busca from #PAR_TEMP12 cross join #PAR12;  \n" +
                            "create table #chave_busca_final (chave_busca_1 varchar (100),chave_busca varchar (100))   \n" +
                            "insert into #chave_busca_final select * from #chave_busca_teste1 where chave_busca is not null  \n" +
                            "union all \n" +
                            "select * from #marge3 where chave_busca not like '%<PAR%'  \n" +
                            "union all \n" +
                            "select distinct replace(replace(replace(GAB_CAB.chave_busca, '<MODELO>', gab_cabdet.ligacao), '<COMPR>', GAB_PARAM_MED_CRT.medida), '<PAR4>',#PAR4.PAR4) as chave_busca_1, GAB_CAB.chave_busca from GAB_CAB inner join GAB_CABDET on gab_cab.descricao = GAB_CABDET.descricao inner join GAB_PARAM_MED_CRT on GAB_CABDET.ligacao = GAB_PARAM_MED_CRT.Corte cross join #par4 where chave_busca not like '%<PAR8>'  \n" +
                            "union all \n" +
                            "select replace(chave_busca, '<COMPR>', GAB_PARAM_MED_CRT.medida), chave_busca from GAB_CRTGAB inner join GAB_PARAM_MED_CRT on GAB_CRTGAB.codigo = GAB_PARAM_MED_CRT.Corte \n" +
                            "union all \n" +
                            "select distinct replace(replace(replace(replace(GAB_ACSG.chave_busca, '<PROF>', GAB_CRTGAB.profundidade), '<MODELO>', GAB_CRTGAB.codigo), '<COMPR>', GAB_PARAM_MED_CRT.medida), '<PAR4>',#PAR4.PAR4) as chave_busca_1, GAB_ACSG.chave_busca from GAB_CAB  \n" +
                            "inner join gab_acsg_cab on GAB_CAB.descricao = gab_acsg_cab.cabeceira \n" +
                            "inner join GAB_ACSG on gab_acsg_cab.acessorio = GAB_ACSG.acessorio \n" +
                            "inner join GAB_CABDET on GAB_CAB.descricao = GAB_CABDET.descricao \n" +
                            "inner join GAB_CRTGAB on GAB_CABDET.ligacao = GAB_CRTGAB.codigo \n" +
                            "inner join GAB_PARAM_MED_CRT on GAB_CRTGAB.codigo = GAB_PARAM_MED_CRT.Corte \n" +
                            "cross join #par4;  \n" +
                            "select distinct #chave_busca_final.chave_busca_1,#chave_busca_final.chave_busca,prdchb.chave_busca_montada,prdchb.produto,prdorc.descricao,PRDCHB.Esconder_orcamento from #chave_busca_final   \n" +
                            "full outer join prdchb on #chave_busca_final.chave_busca_1 = prdchb.chave_busca_montada left join prdorc on prdchb.produto = prdorc.produto   \n" +
                            "where #chave_busca_final.chave_busca_1 like '" + textBox2.Text + "' and prdchb.produto is null;";

            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        string planilha_buscar = "chave_busca_geral";
        private void button_Atualizar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            create_split();
            planilhao();
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            drop_split();
            button_Importar.Enabled = true;
            button_Deletear.Enabled = true;
            button_Alterar.Enabled = true;
            button_exportar_geral.Enabled = true;
            button_import_excel.Enabled = true;
            Cursor.Current = Cursors.Default;

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
            Application.Run(new Form_Gerenciador_SIP(valor_Chave_busca, Login));
        }

        private void button_Importar_Click(object sender, EventArgs e)
        {
            planilha_buscar = "chave_busca_importar";
            button_Deletear.Visible = false;
            button_Alterar.Visible = false;
            textBox2.Enabled = false;
            button_Voltar.Visible = false;
            button_voltar2.Visible = true;
            button_Atualizar.Visible = false;
            button_exportar_geral.Visible = false;
            button_exportar_importar.Visible = true;
            Cursor.Current = Cursors.WaitCursor;
            //NEW
            create_split();
            planilhao_importar();
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            drop_split();
            //END NEW
            dataGridView1.Columns.Remove("chave_busca_montada");
            dataGridView1.Columns.Remove("descricao");
            button_Importar_SQL.Visible = true;
            button_Importar.Visible = false;

            Cursor.Current = Cursors.Default;
        }


        private void button_Importar_SQL_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string StrQuery;
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            using (SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = sqlconn;
                    sqlconn.Open();
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[2].Value == null || dataGridView1.Rows[i].Cells[2].Value == DBNull.Value || String.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[2].Value.ToString()))
                        {
                            MessageBox.Show("Campo produto da linha " + (i + 1) + " esta vazio!");
                            return;
                        }
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        StrQuery = @"INSERT INTO PRDCHB VALUES ('Vittrine','"
                        + dataGridView1.Rows[i].Cells["Produto"].Value + "','"
                        + dataGridView1.Rows[i].Cells["chave_busca_1"].Value + "','"
                        + dataGridView1.Rows[i].Cells["chave_busca"].Value + "','"
                        + "','','"
                        + dataGridView1.Rows[i].Cells["Esconder_orcamento"].Value.ToString() + "','"
                        + "','',NULL,0,NULL,'Eletrofrio');";
                        comm.CommandText = StrQuery;
                        comm.ExecuteNonQuery();
                    }
                }
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Dados importado na tabela PRDCHB com sucesso!");

        }

        private void button_voltar2_Click(object sender, EventArgs e)
        {
            button_Deletear.Visible = true;
            button_Alterar.Visible = true;
            textBox2.Enabled = true;
            Cursor.Current = Cursors.WaitCursor;
            button_Atualizar.Visible = true;
            button_Importar_SQL.Visible = false;
            button_Importar.Visible = true;
            create_split();
            planilhao();
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            drop_split();

            button_voltar2.Visible = false;
            button_Voltar.Visible = true;

            Cursor.Current = Cursors.Default;
        }

        private void Form_Chave_busca_Load(object sender, EventArgs e)
        {
            button_Importar.Enabled = false;
            button_Deletear.Enabled = false;
            button_Alterar.Enabled = false;

        }

        private void button_Deletear_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja deletar esses arquivos ?", "Deletar", MessageBoxButtons.YesNo);
            string StrQuery;
            // string StrQuery1;
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            if (dialogResult == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;

                using (SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring))
                {
                    using (SqlCommand comm = new SqlCommand())
                    {
                        comm.Connection = sqlconn;
                        sqlconn.Open();
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            StrQuery = @"Delete from prdchb where basecodigo = 'Eletrofrio' and chave_busca_montada = '"
                            + dataGridView1.Rows[i].Cells["chave_busca_1"].Value + "';";
                            comm.CommandText = StrQuery;
                            comm.ExecuteNonQuery();
                        }
                    }
                }

                Cursor.Current = Cursors.Default;
                MessageBox.Show("Dados alterados na tabela PRDCHB com sucesso!");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void button_Alterar_Click(object sender, EventArgs e)
        {
            string StrQuery1;
            string StrQuery;
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja alterar esses arquivos ?", "Alterar", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Produto"].Value == null || dataGridView1.Rows[i].Cells["Produto"].Value == DBNull.Value || String.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["Produto"].Value.ToString()))
                    {
                        MessageBox.Show("Campo produto da linha " + (i + 1) + " esta vazio!");
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                }
                // string ssqlconnectionstring = @"Data Source=INSTANCIA;Initial Catalog=Eletrofast_2019;Persist Security Info=True;User ID=sa;Password=SENHA";
                using (SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring))
                {
                    using (SqlCommand comm = new SqlCommand())
                    {
                        comm.Connection = sqlconn;
                        sqlconn.Open();
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            StrQuery = @"Delete from prdchb where basecodigo = 'Eletrofrio' and chave_busca_montada = '"
                            + dataGridView1.Rows[i].Cells["chave_busca_1"].Value + "';";
                            comm.CommandText = StrQuery;
                            comm.ExecuteNonQuery();
                        }
                    }
                }
                using (SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring))
                {
                    using (SqlCommand comm = new SqlCommand())
                    {
                        comm.Connection = sqlconn;
                        sqlconn.Open();

                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            StrQuery1 = @"INSERT INTO PRDCHB VALUES ('Vittrine','"
                            + dataGridView1.Rows[i].Cells["Produto"].Value + "','"
                            + dataGridView1.Rows[i].Cells["chave_busca_1"].Value + "','"
                            + dataGridView1.Rows[i].Cells["chave_busca"].Value + "','"
                            + "','','"
                            + dataGridView1.Rows[i].Cells["Esconder_orcamento"].Value.ToString() + "','"
                            + "','',NULL,0,NULL,'Eletrofrio');";
                            comm.CommandText = StrQuery1;
                            comm.ExecuteNonQuery();
                        }
                    }
                }

                Cursor.Current = Cursors.Default;
                MessageBox.Show("Dados alterados na tabela PRDCHB com sucesso!");
            }
            else if (dialogResult == DialogResult.No)
            {

            }

        }

        private void button_exportar_geral_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DialogResult dialogResult = MessageBox.Show("Deseja realemnte exportar ?", "Exportar", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                //Creating DataTable
                DataTable dt = new DataTable();

                //Adding the Columns
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    dt.Columns.Add(column.HeaderText, column.ValueType);
                }

                //Adding the Rows
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value;
                        }
                    }
                }

                //Exporting to Excel
                string folderPath = @"C:\WBC\Ferramentas\Aplicativos\Chave_de_busca\01-Exportar\";
                if (!Directory.Exists(folderPath) || !Directory.Exists(@"C:\WBC\Ferramentas\Aplicativos\Chave_de_busca\02-Importar\") || !Directory.Exists(@"C:\WBC\Ferramentas\Aplicativos\Chave_de_busca\03-Importados\"))
                {
                    try
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    catch { MessageBox.Show("01-Exportar"); }
                    try
                    {
                        Directory.CreateDirectory(@"C:\WBC\Ferramentas\Aplicativos\Chave_de_busca\02-Importar\");
                    }
                    catch { MessageBox.Show("02-Importar"); }
                    try
                    {
                        Directory.CreateDirectory(@"C:\WBC\Ferramentas\Aplicativos\Chave_de_busca\03-Importados\");
                    }
                    catch { MessageBox.Show("03-Importados"); }
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "chave_de_busca");
                    wb.SaveAs(folderPath + "chave_busca_geral" + ".xlsx");
                }
                Cursor.Current = Cursors.Default;

                MessageBox.Show("Tabela exportada com sucesso!");

            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void button_exportar_importar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DialogResult dialogResult = MessageBox.Show("Deseja realemnte exportar ?", "Exportar", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                //Creating DataTable
                DataTable dt = new DataTable();

                //Adding the Columns
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    dt.Columns.Add(column.HeaderText, column.ValueType);
                }

                //Adding the Rows
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value;
                        }
                    }
                }

                //Exporting to Excel
                string folderPath = @"C:\WBC\Ferramentas\Aplicativos\Chave_de_busca\01-Exportar\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "chave_de_busca");
                    wb.SaveAs(folderPath + "chave_busca_importar" + ".xlsx");
                }
                Cursor.Current = Cursors.Default;

                MessageBox.Show("Tabela exportada com sucesso!");

            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void button_import_excel_Click(object sender, EventArgs e)
        {
            var dateString2 = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
            if (File.Exists(@"C:\WBC\Ferramentas\Aplicativos\Chave_de_busca\02-Importar\" + planilha_buscar + ".xlsx"))
            {
                button_Importar.Enabled = false;
                button_Deletear.Enabled = true;
                button_Alterar.Enabled = true;
                button_exportar_geral.Enabled = true;
                button_import_excel.Enabled = true;

                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                string folder_importar = @"C:\WBC\Ferramentas\Aplicativos\Chave_de_busca\02-Importar\";
                string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + folder_importar + planilha_buscar + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                string folder_importardos = @"C:\WBC\Ferramentas\Aplicativos\Chave_de_busca\03-Importados\";

                OleDbConnection Con = new OleDbConnection(constr);
                OleDbCommand OleConnection = new OleDbCommand("SELECT * FROM [chave_de_busca$]", Con);
                Con.Open();
                OleDbDataAdapter sda = new OleDbDataAdapter(OleConnection);
                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView1.DataSource = data;
                Con.Close();

                dataGridView1.ClearSelection();

                File.Move(folder_importar + planilha_buscar + ".xlsx", folder_importardos + planilha_buscar + "_" + dateString2 + ".xlsx");
            }
            else
            {
                MessageBox.Show("Não existe aquivos na pasta para importar!");
            }
        }
    }
}

