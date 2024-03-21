using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Gerenciador_SIP
{
    public partial class Form_Comparativo : Form
    {
        string Banco;
        string Instancia;
        string Senha;
        string Usuario;
        Thread t1;
        List<string> valor_Comparativo;
        string Nome_planilha = null;
        string Login;

        public Form_Comparativo(List<string> valor, string banco, string instancia, string senha, string usuario, string login)
        {
            Login = login;
            Banco = banco;
            Instancia = instancia;
            Senha = senha;
            Usuario = usuario;
            InitializeComponent();
            valor_Comparativo = valor;
            button_acessorio.Visible = false;
            button_componentes_gondolas.Visible = false;
            button_componentes_camaras.Visible = false;
            button_componentes_checkout.Visible = false;
            button_componentes_mobilias.Visible = false;
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tab_control();
        }

        private void tab_control()
        {
            this.tabPage2.Text = "Teste";
            this.tabPage2.ForeColor = System.Drawing.Color.Red;
        }


        private void button_total_Click(object sender, EventArgs e)
        {
            Nome_planilha = "Total";
            button_acessorio.Visible = false;
            button_componentes_gondolas.Visible = false;
            button_componentes_camaras.Visible = false;
            button_componentes_checkout.Visible = false;
            button_componentes_mobilias.Visible = false;
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "create table #temp1 (" +
                                "NumeroOrcamento nvarchar(255), \n" +
                                "Base nvarchar(255), \n" +
                                "Grupo nvarchar(255), \n" +
                                "SubGrupo nvarchar(255), \n" +
                                "Codigo nvarchar(255), \n" +
                                "Descricao nvarchar(255), \n" +
                                "Cor nvarchar(255), \n" +
                                "quantidade int, \n" +
                                "Altura_painel float, \n" +
                                "Comprimento_painel float, \n" +
                                "preco_unitario_1 float, \n" +
                                "precoXqtd_1 float, \n" +
                                "preco_unitario_2 float, \n" +
                                "precoXqtd_2 float) \n" +
                            "insert into #temp1 select \n" +
                                "OrcMat.numeroOrcamento, \n" +
                                "OrcMat.orcmatbase, \n" +
                                "prdgrp.GrupoBusca, \n" +
                                "prdgrp.Descricao, \n" +
                                "OrcMat.orcmat_codigo_pai, \n" +
                                "OrcMat.orcmat_descricao, \n" +
                                "OrcMat.OrcMatCorPesquisa, \n" +
                                "OrcMat.orcmat_qtde, \n" +
                                "OrcMat.ORCMATALTURA, \n" +
                                "OrcMat.ORCMATCOMPRIMENTO,	 \n" +
                                "sum(round(prcprd1.Preco, 2) * prcftr1.fator) as preco_uni_1,	 \n" +
                                "sum(round(prcprd1.preco, 2) * prcftr1.fator * orcmat.orcmat_qtde) as precoXqtd_1, \n" +
                                "sum(round(prcprd2.Preco, 2) * prcftr2.fator) as preco_uni_2,	 \n" +
                                "sum(round(prcprd2.preco, 2) * prcftr2.fator * orcmat.orcmat_qtde) as precoXqtd_2 \n" +
                            "from OrcMat \n" +
                            "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
                            "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
                            "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
                            "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
                            "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
                            "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
                            "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
                            "where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "' and prdgrp.Descricao not like 'Painel frigorifico' \n" +
                                "group by OrcMat.numeroOrcamento, \n" +
                                "OrcMat.orcmatbase, \n" +
                                "prdgrp.GrupoBusca, \n" +
                                "prdgrp.Descricao, \n" +
                                "OrcMat.orcmat_codigo_pai, \n" +
                                "OrcMat.orcmat_descricao, \n" +
                                "OrcMat.OrcMatCorPesquisa, \n" +
                                "OrcMat.orcmat_qtde, \n" +
                                "OrcMat.ORCMATALTURA, \n" +
                                "OrcMat.ORCMATCOMPRIMENTO; \n" +
                            "insert into #temp1	select \n" +
                                "OrcMat.numeroOrcamento, \n" +
                                "OrcMat.orcmatbase, \n" +
                                "prdgrp.GrupoBusca, \n" +
                                "prdgrp.Descricao, \n" +
                                "OrcMat.orcmat_codigo_pai, \n" +
                                "OrcMat.orcmat_descricao, \n" +
                                "OrcMat.OrcMatCorPesquisa, \n" +
                                "OrcMat.orcmat_qtde, \n" +
                                "OrcMat.ORCMATALTURA, \n" +
                                "OrcMat.ORCMATCOMPRIMENTO,	 \n" +
                                "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd1.Preco), 2) * prcftr1.fator as preco_uni_1, \n" +
                                "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd1.Preco), 2) * prcftr1.fator * orcmat.orcmat_qtde as precoXqtd_1, \n" +
                                "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd2.Preco), 2) * prcftr2.fator as preco_uni_2, \n" +
                                "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd2.Preco), 2) * prcftr2.fator * orcmat.orcmat_qtde as precoXqtd_2 \n" +
                            "from OrcMat \n" +
                            "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
                            "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
                            "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
                            "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
                            "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
                            "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
                            "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
                            "where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "' and prdgrp.Descricao = 'Painel frigorifico' \n" +
                            "group by \n" +
                                "OrcMat.numeroOrcamento, \n" +
                                "OrcMat.orcmatbase, \n" +
                                "prdgrp.GrupoBusca, \n" +
                                "prdgrp.Descricao, \n" +
                                "OrcMat.orcmat_codigo_pai, \n" +
                                "OrcMat.orcmat_descricao, \n" +
                                "OrcMat.OrcMatCorPesquisa, \n" +
                                "OrcMat.orcmat_qtde, \n" +
                                "OrcMat.ORCMATCOMPRIMENTO,	 \n" +
                                "OrcMat.ORCMATALTURA, \n" +
                                "prcftr1.fator, \n" +
                                "prcftr2.fator; \n" +
                                "with tabela1 as ( \n" +
                                "select \n" +
                                "#temp1.NumeroOrcamento, \n" +
                                "#temp1.Base, \n" +
                                "round(sum(#temp1.precoXqtd_1),2) as Preco_1, \n" +
                                "round(sum(#temp1.precoXqtd_2),2) as Preco_2 \n" +
                                "from #temp1 \n" +
                                "group by \n" +
                                "#temp1.NumeroOrcamento, \n" +
                                "#temp1.Base ) \n" +
                                "select \n" +
                                "Base, \n" +
                                "Preco_1 as Preco_Antigo, \n" +
                                "round((-1 * (100 * (1 - (Preco_2 / nullif(Preco_1, 0))))), 2) as Variacao, \n" +
                                "Preco_2 as Preco_Novo \n" +
                                "from tabela1";

            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Columns[2].DefaultCellStyle.Format = "#0.00\\%";
            }
        }

        private void button_voltar_Click(object sender, EventArgs e)
        {
            this.Close();
            t1 = new Thread(Form_Menu);
            t1.SetApartmentState(ApartmentState.STA);
            t1.Start();
        }

        private void Form_Menu(object obj)
        {
            Application.Run(new Form_Gerenciador_SIP(valor_Comparativo, Login));
        }

        private void Button_grupo_Click(object sender, EventArgs e)
        {
            Nome_planilha = "Grupo";
            button_acessorio.Visible = false;
            button_componentes_gondolas.Visible = false;
            button_componentes_camaras.Visible = false;
            button_componentes_checkout.Visible = false;
            button_componentes_mobilias.Visible = false;
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "create table #temp1 ( \n" +
        "NumeroOrcamento nvarchar(255), \n" +
    "Base nvarchar(255), \n" +
    "Grupo nvarchar(255), \n" +
    "Grupo_busca nvarchar(255), \n" +
    "SubGrupo nvarchar(255), \n" +
    "Nome_subgrupo nvarchar(255), \n" +
    "Codigo nvarchar(255), \n" +
    "Descricao nvarchar(255), \n" +
    "Cor nvarchar(255), \n" +
    "quantidade int, \n" +
    "Altura_painel float, \n" +
    "Comprimento_painel float, \n" +
    "preco_unitario_1 float, \n" +
    "precoXqtd_1 float, \n" +
    "preco_unitario_2 float, \n" +
    "precoXqtd_2 float) \n" +
"insert into #temp1 \n" +
"select \n" +
    "OrcMat.numeroOrcamento, \n" +
    "OrcMat.orcmatbase, \n" +
    "prdgrp.Grupo, \n" +
    "prdgrp.grupobusca, \n" +
    "prdgrp.Subgrupo, \n" +
    "prdgrp.descricao, \n" +
    "OrcMat.orcmat_codigo_pai, \n" +
    "OrcMat.orcmat_descricao, \n" +
    "OrcMat.OrcMatCorPesquisa, \n" +
    "OrcMat.orcmat_qtde, \n" +
    "OrcMat.ORCMATALTURA, \n" +
    "OrcMat.ORCMATCOMPRIMENTO,	 \n" +
    "sum(round(prcprd1.Preco, 2) * prcftr1.fator) as preco_uni_1,	 \n" +
    "sum(round(prcprd1.preco, 2) * prcftr1.fator * orcmat.orcmat_qtde) as precoXqtd_1, \n" +
    "sum(round(prcprd2.Preco, 2) * prcftr2.fator) as preco_uni_2,	 \n" +
    "sum(round(prcprd2.preco, 2) * prcftr2.fator * orcmat.orcmat_qtde) as precoXqtd_2 \n" +
"from OrcMat \n" +
    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
    "where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "' and prdgrp.Descricao not like 'Painel frigorifico' \n" +
    "group by OrcMat.numeroOrcamento, \n" +
    "OrcMat.numeroOrcamento, \n" +
    "OrcMat.orcmatbase, \n" +
    "prdgrp.Grupo, \n" +
    "prdgrp.grupobusca, \n" +
    "prdgrp.Subgrupo, \n" +
    "prdgrp.descricao, \n" +
    "OrcMat.orcmat_codigo_pai, \n" +
    "OrcMat.orcmat_descricao, \n" +
    "OrcMat.OrcMatCorPesquisa, \n" +
    "OrcMat.orcmat_qtde, \n" +
    "OrcMat.ORCMATALTURA, \n" +
    "OrcMat.ORCMATCOMPRIMENTO; \n" +
            "insert into #temp1 \n" +
    "select \n" +
    "OrcMat.numeroOrcamento, \n" +
    "OrcMat.orcmatbase, \n" +
    "prdgrp.Grupo, \n" +
    "prdgrp.grupobusca, \n" +
    "prdgrp.Subgrupo, \n" +
    "prdgrp.descricao, \n" +
    "OrcMat.orcmat_codigo_pai, \n" +
    "OrcMat.orcmat_descricao, \n" +
    "OrcMat.OrcMatCorPesquisa, \n" +
    "OrcMat.orcmat_qtde, \n" +
    "OrcMat.ORCMATALTURA, \n" +
    "OrcMat.ORCMATCOMPRIMENTO,	 \n" +
    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd1.Preco), 2) * prcftr1.fator as preco_uni_1, \n" +
    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd1.Preco), 2) * prcftr1.fator * orcmat.orcmat_qtde as precoXqtd_1, \n" +
    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd2.Preco), 2) * prcftr2.fator as preco_uni_2, \n" +
    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd2.Preco), 2) * prcftr2.fator * orcmat.orcmat_qtde as precoXqtd_2 \n" +
"from OrcMat \n" +
    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
    "where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "' and prdgrp.Descricao = 'Painel frigorifico' \n" +
    "group by \n" +
    "OrcMat.numeroOrcamento, \n" +
    "OrcMat.orcmatbase, \n" +
    "prdgrp.Grupo, \n" +
    "prdgrp.grupobusca, \n" +
    "prdgrp.Subgrupo, \n" +
    "prdgrp.descricao, \n" +
    "OrcMat.orcmat_codigo_pai, \n" +
    "OrcMat.orcmat_descricao, \n" +
    "OrcMat.OrcMatCorPesquisa, \n" +
    "OrcMat.orcmat_qtde, \n" +
    "OrcMat.ORCMATALTURA, \n" +
    "OrcMat.ORCMATCOMPRIMENTO,	 \n" +
    "prcftr1.fator, \n" +
    "prcftr2.fator; \n" +
            "with tabela1 as ( \n" +
            "select \n" +
        "#temp1.NumeroOrcamento, \n" +
        "#temp1.Base, \n" +
        "prdgrp.Descricao, \n" +
        "sum(#temp1.precoXqtd_1) as Preco_1, \n" +
        "sum(#temp1.precoXqtd_2) as Preco_2 \n" +
    "from #temp1 \n" +
    "inner join prdgrp on #temp1.Grupo = prdgrp.Grupo and prdgrp.Subgrupo = 0 \n" +
    "group by \n" +
    "#temp1.NumeroOrcamento, \n" +
    "#temp1.Base, \n" +
        "prdgrp.Descricao, \n" +
        "#temp1.Grupo) \n" +
    "select \n" +
        "Base, \n" +
        "Descricao, \n" +
        "Preco_1 as Preco_Antigo, \n" +
        "round((-1 * (100 * (1 - (Preco_2 / nullif(Preco_1, 0))))), 2) as variacao, \n" +
        "Preco_2 as Preco_Novo\n" +
    "from tabela1";





            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[4].DefaultCellStyle.Format = "c2";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Columns[3].DefaultCellStyle.Format = "#0.00\\%";
            }
        }

        private void button_alinhamento_Click(object sender, EventArgs e)
        {
            Nome_planilha = "Alinhamento";
            button_acessorio.Visible = false;
            button_componentes_gondolas.Visible = false;
            button_componentes_camaras.Visible = false;
            button_componentes_checkout.Visible = false;
            button_componentes_mobilias.Visible = false;
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "create table #temp_painel ( \n" +
                                "numeroOrcamento nvarchar(255),  \n" +
                                "orcmatbase nvarchar(255),  \n" +
                                "Grupo nvarchar(255),  \n" +
                                "grupobusca nvarchar(255),  \n" +
                                "Subgrupo nvarchar(255),  \n" +
                                "descricao nvarchar(255),  \n" +
                                "orcmat_codigo_pai nvarchar(255), \n" +
                                "orcmat_descricao nvarchar(255),  \n" +
                                "OrcMatCorPesquisa nvarchar(255),  \n" +
                                "orcdet_qtde int,\n" +
                                "ORCMATALTURA nvarchar(255),  \n" +
                                "ORCMATCOMPRIMENTO nvarchar(255),  \n" +
                                "preco_uni_1 float,\n" +
                                "precoXqtd_1 float,\n" +
                                "preco_uni_2 float ,\n" +
                                "precoXqtd_2 float,\n" +
                                "item int)  \n" +
                            "insert into #temp_painel  \n" +
                                "select\n" +
                                    "OrcMat.numeroOrcamento,   \n" +
                                    "OrcMat.orcmatbase,   \n" +
                                    "prdgrp.Grupo,   \n" +
                                    "prdgrp.grupobusca,   \n" +
                                    "prdgrp.Subgrupo,   \n" +
                                    "prdgrp.descricao,   \n" +
                                    "OrcMat.orcmat_codigo_pai,\n" +
                                   "OrcMat.orcmat_descricao,   \n" +
                                    "OrcMat.OrcMatCorPesquisa,    \n" +
                                    "orcdet.orcdet_qtde,  \n" +
                                    "OrcMat.ORCMATALTURA,   \n" +
                                    "OrcMat.ORCMATCOMPRIMENTO,  \n" +
                                    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd1.Preco), 2) * prcftr1.fator as preco_uni_1,   \n" +
                                    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd1.Preco), 2) * prcftr1.fator * orcdet.orcdet_qtde as precoXqtd_1,   \n" +
                                    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd2.Preco), 2) * prcftr2.fator as preco_uni_2,   \n" +
                                    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd2.Preco), 2) * prcftr2.fator * orcdet.orcdet_qtde as precoXqtd_2\n" +
                                    ",OrcDet.orcdet_item\n" +
                                "from OrcMat\n" +
                                    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo\n" +
                                    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "'\n" +
                                    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista\n" +
                                    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo\n" +
                                    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "'\n" +
                                    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista\n" +
                                    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo\n" +
                                    "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo\n" +
                                "where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "' and prdgrp.Descricao = 'Painel frigorifico'\n" +
                                "group by OrcMat.numeroOrcamento,   \n" +
                                    "OrcMat.numeroOrcamento,   \n" +
                                    "OrcMat.orcmatbase,   \n" +
                                    "prdgrp.Grupo,   \n" +
                                    "prdgrp.grupobusca,   \n" +
                                    "prdgrp.Subgrupo,   \n" +
                                    "prdgrp.descricao,   \n" +
                                    "OrcMat.orcmat_codigo_pai,\n" +
                                    "OrcMat.orcmat_descricao,   \n" +
                                    "OrcMat.OrcMatCorPesquisa,   \n" +
                                    "OrcMat.ORCMATALTURA,   \n" +
                                    "OrcMat.ORCMATCOMPRIMENTO,  \n" +
                                    "orcdet.orcdet_item,  \n" +
                                    "prcftr1.fator,  \n" +
                                    "prcftr2.fator,  \n" +
                                    "OrcDet.orcdet_item\n" +
                                    ",OrcDet.orcdet_qtde;\n" +
                                "create table #temp_item (  \n" +
                                    "NumeroOrcamento nvarchar(255),  \n" +
                                    "Base nvarchar(100),  \n" +
                                    "Grupo int,\n" +
                                    "GrupoBusca nvarchar(255),  \n" +
                                    "orcdet_item int,\n" +
                                    "Descricao nvarchar(255),  \n" +
                                    "Cor nvarchar(100),  \n" +
                                    "PrecoXqtd_1 float,\n" +
                                    "PrecoXqtd_2 float)  \n" +
                                "insert into #temp_item  \n" +
                                    "select\n" +
                                        "OrcMat.numeroOrcamento,   \n" +
                                        "OrcMat.orcmatbase,  \n" +
                                        "prdgrp.Grupo, \n" +
                                        "prdgrp.GrupoBusca, \n" +
                                        "OrcDet.orcdet_item,  \n" +
                                        "prdgrp.Descricao,   \n" +
                                        "OrcMat.OrcMatCorPesquisa,\n" +
                                        "sum(round(prcprd1.preco, 2) * prcftr1.fator * orcdet.orcdet_qtde) as precoXqtd_1,   \n" +
                                        "sum(round(prcprd2.preco, 2) * prcftr2.fator * orcdet.orcdet_qtde) as precoXqtd_2\n" +
                                    "from OrcMat\n" +
                                        "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo\n" +
                                        "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "'\n" +
                                        "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista\n" +
                                        "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo\n" +
                                        "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "'\n" +
                                        "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista\n" +
                                        "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo\n" +
                                        "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo\n" +
                                    "where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "' and prdgrp.Descricao not like 'Painel frigorifico'  and orcmat_codigo_pai not like '<NAOORCAR>'\n" +
                                    "group by OrcMat.numeroOrcamento,   \n" +
                                        "OrcMat.orcmatbase,   \n" +
                                        "prdgrp.Grupo, \n" +
                                        "prdgrp.GrupoBusca, \n" +
                                        "prdgrp.Descricao,   \n" +
                                        "OrcMat.OrcMatCorPesquisa,\n" +
                                        "orcdet.orcdet_qtde,   \n" +
                                        "OrcDet.orcdet_item\n" +
                                        ",OrcDet.orcdet_qtde;\n" +
                                    "create table #temp_final (\n" +
                                    "Base nvarchar(100),\n" +
                                    "Grupo nvarchar(100),\n" +
                                    "Alinhamento int,\n" +
                                    "Descricao nvarchar(255),\n" +
                                    "Preco_antigo float,\n" +
                                    "Variacao float,\n" +
                                    "Preco_novo float)\n" +
                                    "insert into #temp_final			\n" +
                                        "select\n" +
                                            "#temp_painel.orcmatbase,  \n" +
                                            "prdgrp.descricao as Grupo, \n" +
                                            "#temp_painel.item,  \n" +
                                           "'Câmaras' as Descricao,\n" +
                                           "round(sum(#temp_painel.precoXqtd_1),2) as precoXqtd_1, \n" +
                                            "null,\n" +
                                            "round(sum(#temp_painel.precoXqtd_2),2) as precoXqtd_2  \n" +
                                        "from #temp_painel  \n" +
                                            "left join prdgrp on #temp_painel.Grupo = prdgrp.Grupo where prdgrp.Subgrupo = '00' \n" +
                                        "group by\n" +
                                         "#temp_painel.orcmatbase,  \n" +
                                            "prdgrp.Descricao,\n" +
                                         "#temp_painel.grupobusca, \n" +
                                         "#temp_painel.item,  \n" +
                                         "#temp_painel.descricao  \n" +
                                   "union all\n" +
                                        "select\n" +
                                            "#temp_item.Base,  \n" +
                                            "prdgrp.Descricao as Grupo,\n" +
                                            "iif(prdgrp.Descricao = 'Câmaras', '1',#temp_item.orcdet_item) as Alinhamento,\n" +
                                           "iif(prdgrp.Descricao = 'Câmaras', 'Câmaras', #temp_item.Descricao) as Descricao,\n" +
                                            "round(sum(#temp_item.PrecoXqtd_1),2) as PrecoXqtd_1,  \n" +
                                            "null,\n" +
                                            "round(sum(#temp_item.PrecoXqtd_2),2) as PrecoXqtd_2  \n" +
                                        "from #temp_item  \n" +
                                            "left join prdgrp on #temp_item.Grupo = prdgrp.Grupo where Subgrupo = '00' \n" +
                                        "group by\n" +
                                         "#temp_item.Base,  \n" +
                                            "prdgrp.Descricao,\n" +
                                         "#temp_item.GrupoBusca,  \n" +
                                         "#temp_item.orcdet_item,  \n" +
                                         "#temp_item.Descricao; \n" +
                                            "with dados as (\n" +
                                            "select\n" +
                                         "#temp_final.Base,\n" +
                                         "#temp_final.Grupo,\n" +
                                         "#temp_final.Alinhamento,\n" +
                                         "#temp_final.Descricao,\n" +
                                            "sum(#temp_final.Preco_antigo) as Preco_antigo,\n" +
                                            "sum(#temp_final.Preco_novo) as Preco_novo\n" +
                                            "from #temp_final\n" +
                                            "group by\n" +
                                            "#temp_final.Base,\n" +
                                            "#temp_final.Grupo,\n" +
                                            "#temp_final.Alinhamento,\n" +
                                            "#temp_final.Descricao )\n" +
                                            "select\n" +
                                            "dados.Base,\n" +
                                            "dados.Grupo,\n" +
                                            "dados.Alinhamento,\n" +
                                            "dados.Descricao,\n" +
                                            "dados.Preco_antigo,\n" +
                                            "round((-1 * (100 * (1 - (Preco_novo / nullif(Preco_antigo, 0))))), 2) as Variacao,\n" +
                                            "dados.Preco_novo\n" +
                                            "from dados";

            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "c2";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Columns[5].DefaultCellStyle.Format = "#0.00\\%";
            }
        }

        private void button_expositores_Click(object sender, EventArgs e)
        {
            Nome_planilha = "Expositores";
            button_acessorio.Visible = true;
            button_componentes_gondolas.Visible = false;

            button_componentes_camaras.Visible = false;
            button_componentes_checkout.Visible = false;
            button_componentes_mobilias.Visible = false;

            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "with dados as (\n" +
                "select \n" +
    "OrcMat.orcmatbase,  \n" +
    "prdgrp.Descricao,  \n" +
    "OrcDet.orcdet_item, \n" +
    "orcdet.orcdet_corte, \n" +
    "orcdet.orcdet_Comprimento, \n" +
    "sum(round(prcprd1.preco, 2) * prcftr1.fator * orcdet.orcdet_qtde) as precoXqtd_1,  \n" +
    "sum(round(prcprd2.preco, 2) * prcftr2.fator * orcdet.orcdet_qtde) as precoXqtd_2 \n" +
"from OrcMat \n" +
    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
    "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo \n" +
"where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "'  and prdgrp.GrupoBusca = 'Gabinetes' \n" +
"group by \n" +
    "OrcMat.orcmatbase,  \n" +
    "prdgrp.Descricao,  \n" +
    "prdgrp.Descricao,  \n" +
    "OrcDet.orcdet_corte, \n" +
    "orcdet.orcdet_Comprimento, \n" +
    "OrcDet.orcdet_item, \n" +
    "orcdet.orcdet_nr_seq_crt) \n" +
"select  \n" +
    "orcmatbase,  \n" +
    "Descricao,  \n" +
    "orcdet_item, \n" +
    "orcdet_corte, \n" +
    "orcdet_Comprimento, \n" +
    "precoXqtd_1, \n" +
    "round((-1 * (100 * (1 - (precoXqtd_2 / nullif(precoXqtd_1, 0))))), 2) as Variacao, \n" +
    "precoXqtd_2 \n" +
"from dados \n" +
    "order by orcdet_item, orcdet_corte, orcdet_Comprimento";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns[5].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "c2";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dataGridView1.Columns[6].DefaultCellStyle.Format = "#0.00\\%";

            }
        }

        private void button_acessorio_Click(object sender, EventArgs e)
        {
            Nome_planilha = "Acessorios";
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "with dados as ( \n" +
"select \n" +
    "OrcDet.orcdet_item, \n" +
    "prdgrp.Descricao as SubGrupo, \n" +
    "orcdet.orcdet_corte,  \n" +
    "orcdet.orcdet_Comprimento, \n" +
    "OrcMat.orcmat_descricao,  \n" +
    "OrcMat.orcmat_codigo_pai, \n" +
    "OrcMat.OrcMatCorPesquisa,  \n" +
    "orcdet.orcdet_qtde,  \n" +
    "sum(round(prcprd1.Preco, 2) * prcftr1.fator) as preco_uni_1,	  \n" +
    "sum(round(prcprd1.preco, 2) * prcftr1.fator * orcdet.orcdet_qtde) as precoXqtd_1,  \n" +
    "sum(round(prcprd2.Preco, 2) * prcftr2.fator) as preco_uni_2,	  \n" +
    "sum(round(prcprd2.preco, 2) * prcftr2.fator * orcdet.orcdet_qtde) as precoXqtd_2 \n" +
"from OrcMat \n" +
    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
    "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo \n" +
"where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "' and prdgrp.GrupoBusca = 'Gabinetes' \n" +
"group by \n" +
    "orcdet.orcdet_Comprimento, \n" +
    "prdgrp.Descricao, \n" +
    "orcdet.orcdet_corte, \n" +
    "OrcMat.orcmatbase,  \n" +
    "prdgrp.GrupoBusca,  \n" +
    "prdgrp.Descricao,  \n" +
    "OrcMat.orcmat_codigo_pai,  \n" +
    "OrcMat.orcmat_descricao,  \n" +
    "OrcMat.OrcMatCorPesquisa,  \n" +
    "orcdet.orcdet_qtde,  \n" +
    "OrcDet.orcdet_item \n" +
    ",OrcDet.orcdet_qtde) \n" +
"select \n" +
    "orcdet_item as Item, \n" +
    "dados.SubGrupo, \n" +
    "orcdet_corte as Corte,   \n" +
    "orcdet_Comprimento as Comprimento, \n" +
    "orcmat_descricao as Descricao,  \n" +
    "orcmat_codigo_pai as Codigo, \n" +
    "OrcMatCorPesquisa as Cor,  \n" +
    "orcdet_qtde as Qtde, 	 \n" +
    "preco_uni_1 as Preco_Antigo_uni, \n" +
    "precoXqtd_1 as Preco_Antigo_x_qtde, \n" +
    "round((-1 * (100 * (1 - (preco_uni_2 / nullif(preco_uni_1, 0))))), 2) as Variacao, \n" +
    "preco_uni_2 as Preco_Novo_uni, \n" +
    "precoXqtd_2 as Preco_Novo_x_qtde \n" +
"from dados \n" +
    "order by orcdet_item, orcdet_corte, orcdet_Comprimento, orcmat_descricao, OrcMatCorPesquisa";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns[9].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[12].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "c2";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Columns[10].DefaultCellStyle.Format = "#0.00\\%";
            }
        }

        private void button_Gondolas_Click(object sender, EventArgs e)
        {
            Nome_planilha = "Gondolas";
            button_acessorio.Visible = false;
            button_componentes_gondolas.Visible = true;

            button_componentes_camaras.Visible = false;
            button_componentes_checkout.Visible = false;
            button_componentes_mobilias.Visible = false;

            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "with dados as ( \n" +
                                        "select \n" +
                "OrcMat.orcmatbase,   \n" +
                "prdgrp.Descricao,   \n" +
                "OrcDet.orcdet_item,  \n" +
                "orcdet.orcdet_corte,  \n" +
                "orcdet.orcdet_Comprimento,  \n" +
                "sum(round(prcprd1.preco, 2) * prcftr1.fator * orcdet.orcdet_qtde) as precoXqtd_1,   \n" +
                "sum(round(prcprd2.preco, 2) * prcftr2.fator * orcdet.orcdet_qtde) as precoXqtd_2 \n" +
            "from OrcMat \n" +
                "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
                "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
                "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
                "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
                "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
                "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
                "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
                "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo \n" +
            "where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "'  and prdgrp.Grupo = '1' \n" +
            "group by \n" +
                "OrcMat.orcmatbase,   \n" +
                "prdgrp.Descricao,   \n" +
                "prdgrp.Descricao,   \n" +
                "OrcDet.orcdet_corte,  \n" +
                "orcdet.orcdet_Comprimento,  \n" +
                "OrcDet.orcdet_item,  \n" +
                "orcdet.orcdet_nr_seq_crt)  \n" +
            "select \n" +
                "orcmatbase, \n" +
                "Descricao, \n" +
                "orcdet_item, \n" +
                "orcdet_corte, \n" +
                "orcdet_Comprimento, \n" +
                "round(precoXqtd_1, 2) as precoXqtd_1,  \n" +
                "round((-1 * (100 * (1 - (precoXqtd_2 / nullif(precoXqtd_1, 0))))),2) as Variacao,  \n" +
                "round(precoXqtd_2, 2) as precoXqtd_2 \n" +
            "from dados \n" +
                "order by orcdet_item, orcdet_corte, orcdet_Comprimento";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns[5].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "c2";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dataGridView1.Columns[6].DefaultCellStyle.Format = "#0.00\\%";

            }
        }

        private void button_Camaras_Click(object sender, EventArgs e)
        {
            Nome_planilha = "Camaras";
            button_acessorio.Visible = false;
            button_componentes_gondolas.Visible = false;

            button_componentes_camaras.Visible = true;
            button_componentes_checkout.Visible = false;
            button_componentes_mobilias.Visible = false;

            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "create table #temp_painel ( \n" +
                    "numeroOrcamento nvarchar(255),  \n" +
                    "orcmatbase nvarchar(255),  \n" +
                    "Grupo nvarchar(255),  \n" +
                    "grupobusca nvarchar(255),  \n" +
                    "Subgrupo nvarchar(255),  \n" +
                    "descricao nvarchar(255),  \n" +
                    "orcmat_codigo_pai nvarchar(255),  \n" +
                    "orcmat_descricao nvarchar(255),  \n" +
                    "OrcMatCorPesquisa nvarchar(255),  \n" +
                    "orcdet_qtde int, \n" +
                    "ORCMATALTURA nvarchar(255),  \n" +
                    "ORCMATCOMPRIMENTO nvarchar(255),  \n" +
                    "preco_uni_1 float, \n" +
                    "precoXqtd_1 float, \n" +
                   "preco_uni_2 float , \n" +
                    "precoXqtd_2 float, \n" +
                    "item int)  \n" +
                "insert into #temp_painel  \n" +
                  "select \n" +
                    "OrcMat.numeroOrcamento,   \n" +
                    "OrcMat.orcmatbase,   \n" +
                    "prdgrp.Grupo,   \n" +
                    "prdgrp.grupobusca,   \n" +
                    "prdgrp.Subgrupo,   \n" +
                    "prdgrp.descricao,   \n" +
                    "OrcMat.orcmat_codigo_pai, \n" +
                    "OrcMat.orcmat_descricao,   \n" +
                    "OrcMat.OrcMatCorPesquisa,    \n" +
                    "orcdet.orcdet_qtde,  \n" +
                    "OrcMat.ORCMATALTURA,   \n" +
                    "OrcMat.ORCMATCOMPRIMENTO,	 \n" +
                    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd1.Preco), 2) * prcftr1.fator as preco_uni_1,   \n" +
                    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd1.Preco), 2) * prcftr1.fator * orcdet.orcdet_qtde as precoXqtd_1,   \n" +
                    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd2.Preco), 2) * prcftr2.fator as preco_uni_2,   \n" +
                    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd2.Preco), 2) * prcftr2.fator * orcdet.orcdet_qtde as precoXqtd_2 \n" +
                    ",OrcDet.orcdet_item \n" +
                "from OrcMat \n" +
                    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
                    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
                    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
                    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
                    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
                    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
                    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
                    "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo \n" +
                    "where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "' and prdgrp.Descricao = 'Painel frigorifico' \n" +
                      "group by OrcMat.numeroOrcamento,   \n" +
                    "OrcMat.numeroOrcamento,   \n" +
                    "OrcMat.orcmatbase,   \n" +
                    "prdgrp.Grupo,   \n" +
                    "prdgrp.grupobusca,   \n" +
                    "prdgrp.Subgrupo,   \n" +
                    "prdgrp.descricao,   \n" +
                    "OrcMat.orcmat_codigo_pai, \n" +
                    "OrcMat.orcmat_descricao,   \n" +
                    "OrcMat.OrcMatCorPesquisa,   \n" +
                    "OrcMat.ORCMATALTURA,   \n" +
                    "OrcMat.ORCMATCOMPRIMENTO,  \n" +
                    "orcdet.orcdet_item,  \n" +
                    "prcftr1.fator,  \n" +
                    "prcftr2.fator,  \n" +
                    "OrcDet.orcdet_item \n" +
                    ",OrcDet.orcdet_qtde \n" +
                "create table #temp_item (  \n" +
                    "NumeroOrcamento nvarchar(255),  \n" +
                    "Base nvarchar(100),  \n" +
                    "GrupoBusca nvarchar(255),  \n" +
                    "orcdet_item int, \n" +
                    "Descricao nvarchar(255),  \n" +
                    "Cor nvarchar(100),  \n" +
                    "PrecoXqtd_1 float, \n" +
                    "PrecoXqtd_2 float)  \n" +
                "insert into #temp_item  \n" +
                "select \n" +
                    "OrcMat.numeroOrcamento,   \n" +
                    "OrcMat.orcmatbase,   \n" +
                    "prdgrp.GrupoBusca,   \n" +
                    "OrcDet.orcdet_item,  \n" +
                    "prdgrp.Descricao,   \n" +
                    "OrcMat.OrcMatCorPesquisa, \n" +
                    "round(sum(round(prcprd1.preco, 2) * prcftr1.fator * orcdet.orcdet_qtde), 2) as precoXqtd_1,   \n" +
                    "round(sum(round(prcprd2.preco, 2) * prcftr2.fator * orcdet.orcdet_qtde), 2) as precoXqtd_2 \n" +
                "from OrcMat \n" +
                    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
                    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
                    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
                    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
                    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
                    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
                    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
                    "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo \n" +
                "where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "' and prdgrp.Descricao not like 'Painel frigorifico'  and orcmat_codigo_pai not like '<NAOORCAR>' and prdgrp.GrupoBusca = 'Câmaras' \n" +
                "group by OrcMat.numeroOrcamento,   \n" +
                    "OrcMat.orcmatbase,   \n" +
                    "prdgrp.GrupoBusca,   \n" +
                    "prdgrp.Descricao,   \n" +
                    "OrcMat.OrcMatCorPesquisa, \n" +
                    "orcdet.orcdet_qtde,   \n" +
                    "OrcDet.orcdet_item \n" +
                    ",OrcDet.orcdet_qtde; \n" +
                            "Create table #temp2 (Descricao nvarchar (255),Preco_antigo float,Preco_Novo float) \n" +
                "insert into #temp2 \n" +
                            "select \n" +
                "#temp_item.Descricao,  \n" +
                    "round(sum(#temp_item.PrecoXqtd_1),2) as PrecoXqtd_1,  \n" +
                    "round(sum(#temp_item.PrecoXqtd_2),2) as PrecoXqtd_2  \n" +
                "from #temp_item  \n" +
                "group by \n" +
                "#temp_item.Base,  \n" +
                "#temp_item.GrupoBusca,  \n" +
                "#temp_item.orcdet_item,  \n" +
                "#temp_item.Descricao  \n" +
                "union all \n" +
                "select \n" +
                    "#temp_painel.descricao,  \n" +
                    "round(sum(#temp_painel.precoXqtd_1),2) as precoXqtd_1,  \n" +
                    "round(sum(#temp_painel.precoXqtd_2),2) as precoXqtd_2  \n" +
                "from #temp_painel  \n" +
                "group by \n" +
                "#temp_painel.orcmatbase,  \n" +
                "#temp_painel.grupobusca,  \n" +
                "#temp_painel.item,  \n" +
                "#temp_painel.descricao ; \n" +
                "with dados as ( \n" +
                "select \n" +
                "Descricao, \n" +
                "SUM (Preco_antigo) as Preco_antigo, \n" +
                "SUM(Preco_Novo) as Preco_novo \n" +
                "from #temp2 \n" +
                "group by Descricao) \n" +
                "select \n" +
                "Descricao, \n" +
                "Preco_antigo, \n" +
                "round((-1 * (100 * (1 - (Preco_Novo / nullif(Preco_antigo, 0))))), 2) as Variacao, \n" +
                "Preco_novo \n" +
                "from dados";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns[1].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "c2";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dataGridView1.Columns[2].DefaultCellStyle.Format = "#0.00\\%";

            }
        }

        private void button_Checkout_Click(object sender, EventArgs e)
        {
            Nome_planilha = "Checkout";
            button_acessorio.Visible = false;
            button_componentes_gondolas.Visible = false;

            button_componentes_camaras.Visible = false;
            button_componentes_checkout.Visible = true;
            button_componentes_mobilias.Visible = false;

            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "with dados as ( \n" +
"select \n" +
    "OrcMat.orcmatbase,   \n" +
    "prdgrp.Descricao,   \n" +
    "OrcDet.orcdet_item,  \n" +
    "orcdet.orcdet_corte,  \n" +
    "orcdet.orcdet_Comprimento,  \n" +
    "sum(round(prcprd1.preco, 2) * prcftr1.fator * orcdet.orcdet_qtde) as precoXqtd_1,   \n" +
    "sum(round(prcprd2.preco, 2) * prcftr2.fator * orcdet.orcdet_qtde) as precoXqtd_2 \n" +
"from OrcMat \n" +
    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
    "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo \n" +
"where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "'  and prdgrp.Grupo = '2' \n" +
"group by \n" +
    "OrcMat.orcmatbase,   \n" +
    "prdgrp.Descricao,   \n" +
    "prdgrp.Descricao,   \n" +
    "OrcDet.orcdet_corte,  \n" +
    "orcdet.orcdet_Comprimento,  \n" +
    "OrcDet.orcdet_item,  \n" +
    "orcdet.orcdet_nr_seq_crt)  \n" +
"select \n" +
    "orcmatbase, \n" +
    "Descricao, \n" +
    "orcdet_item, \n" +
    "orcdet_corte, \n" +
    "orcdet_Comprimento, \n" +
    "round(precoXqtd_1, 2) as precoXqtd_1,  \n" +
    "round((-1 * (100 * (1 - (precoXqtd_2 / nullif(precoXqtd_1, 0))))), 2) as Variacao,  \n" +
    "round(precoXqtd_2, 2) as precoXqtd_2 \n" +
"from dados \n" +
    "order by orcdet_item, orcdet_corte, orcdet_Comprimento";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns[5].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "c2";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dataGridView1.Columns[6].DefaultCellStyle.Format = "#0.00\\%";

            }
        }

        private void button_Mobilias_Click(object sender, EventArgs e)
        {
            Nome_planilha = "Mobilias";
            button_acessorio.Visible = false;
            button_componentes_gondolas.Visible = false;

            button_componentes_camaras.Visible = false;
            button_componentes_checkout.Visible = false;
            button_componentes_mobilias.Visible = true;

            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "with dados as ( \n" +
"select \n" +
    "OrcMat.orcmatbase,   \n" +
    "prdgrp.Descricao,   \n" +
    "OrcDet.orcdet_item,  \n" +
    "orcdet.orcdet_corte,  \n" +
    "orcdet.orcdet_Comprimento,  \n" +
    "sum(round(prcprd1.preco, 2) * prcftr1.fator * orcdet.orcdet_qtde) as precoXqtd_1,   \n" +
    "sum(round(prcprd2.preco, 2) * prcftr2.fator * orcdet.orcdet_qtde) as precoXqtd_2 \n" +
"from OrcMat \n" +
    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
    "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo \n" +
"where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "'  and prdgrp.Grupo = '3' \n" +
"group by \n" +
    "OrcMat.orcmatbase,   \n" +
    "prdgrp.Descricao,   \n" +
    "prdgrp.Descricao,   \n" +
    "OrcDet.orcdet_corte,  \n" +
    "orcdet.orcdet_Comprimento,  \n" +
    "OrcDet.orcdet_item,  \n" +
    "orcdet.orcdet_nr_seq_crt)  \n" +
"select \n" +
    "orcmatbase, \n" +
    "Descricao, \n" +
    "orcdet_item, \n" +
    "orcdet_corte, \n" +
    "orcdet_Comprimento, \n" +
    "round(precoXqtd_1, 2) as precoXqtd_1,  \n" +
    "round((-1 * (100 * (1 - (precoXqtd_2 / nullif(precoXqtd_1, 0))))), 2) as Variacao,  \n" +
    "round(precoXqtd_2, 2) as precoXqtd_2 \n" +
"from dados \n" +
    "order by orcdet_item, orcdet_corte, orcdet_Comprimento";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns[5].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "c2";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dataGridView1.Columns[6].DefaultCellStyle.Format = "#0.00\\%";

            }
        }

        private void button_componentes_camaras_Click(object sender, EventArgs e)
        {

            Nome_planilha = "Componentes_camaras";
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "create table #temp_comp_cam ( \n" +
"Item int, \n" +
"Descricao nvarchar(255),  \n" +
"Codigo_pai nvarchar(100),  \n" +
"orcmat_descricao nvarchar(255),  \n" +
"orcmat_tde int, \n" +
"Altura int, \n" +
"Comprimento int, \n" +
"Cor nvarchar(50),  \n" +
"Preco_uni_1 float, \n" +
"PrecoXqtde_1 float, \n" +
"Preco_uni_2 float, \n" +
"PrecoXqtde_2 float)  \n" +
"insert into #temp_comp_cam \n" +
"select \n" +
    "orcdet.orcdet_item,  \n" +
    "prdgrp.Descricao,   \n" +
    "orcmat.orcmat_codigo_pai, \n" +
    "orcmat.orcmat_descricao,  \n" +
    "orcmat.orcmat_qtde,  \n" +
    "orcmat.ORCMATALTURA,  \n" +
    "orcmat.ORCMATCOMPRIMENTO,  \n" +
    "OrcMat.OrcMatCorPesquisa,   \n" +
    "sum(round((prcprd1.preco * prcftr1.fator), 2)) as Preco_uni_1,  \n" +
    "sum(round(prcprd1.preco, 2) * prcftr1.fator * orcdet.orcdet_qtde) as precoXqtd_1,   \n" +
    "sum(round((prcprd2.preco * prcftr1.fator), 2)) as Preco_uni_2,  \n" +
    "sum(round(prcprd2.preco, 2) * prcftr2.fator * orcdet.orcdet_qtde) as precoXqtd_2 \n" +
"from OrcMat \n" +
    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
    "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo \n" +
"where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "' and prdgrp.Descricao not like 'Painel frigorifico'  and orcmat_codigo_pai not like '<NAOORCAR>' and prdgrp.GrupoBusca = 'Câmaras' \n" +
"group by prdgrp.Descricao,   \n" +
    "orcmat.orcmat_codigo_pai,  \n" +
    "orcmat.orcmat_descricao,  \n" +
    "orcmat.ORCMATALTURA,  \n" +
    "orcmat.ORCMATCOMPRIMENTO,  \n" +
    "OrcMat.OrcMatCorPesquisa,  \n" +
    "orcdet.orcdet_item,  \n" +
    "orcmat.orcmat_qtde; \n" +
            "create table #temp_painel (   \n" +
    "numeroOrcamento nvarchar(255),  \n" +
    "orcmatbase nvarchar(255),  \n" +
    "Grupo nvarchar(255),  \n" +
    "grupobusca nvarchar(255),  \n" +
    "Subgrupo nvarchar(255),  \n" +
    "descricao nvarchar(255),  \n" +
    "orcmat_codigo_pai nvarchar(255),  \n" +
    "orcmat_descricao nvarchar(255),  \n" +
    "OrcMatCorPesquisa nvarchar(255),  \n" +
    "orcmat_qtde int, \n" +
    "ORCMATALTURA nvarchar(255),  \n" +
    "ORCMATCOMPRIMENTO nvarchar(255),  \n" +
    "preco_uni_1 float, \n" +
    "precoXqtd_1 float, \n" +
    "preco_uni_2 float , \n" +
    "precoXqtd_2 float, \n" +
    "item int)  \n" +
"insert into #temp_painel  \n" +
  "select \n" +
    "OrcMat.numeroOrcamento,   \n" +
    "OrcMat.orcmatbase,   \n" +
    "prdgrp.Grupo,   \n" +
    "prdgrp.grupobusca,   \n" +
    "prdgrp.Subgrupo,   \n" +
    "prdgrp.descricao,   \n" +
    "OrcMat.orcmat_codigo_pai, \n" +
    "OrcMat.orcmat_descricao,   \n" +
    "OrcMat.OrcMatCorPesquisa,    \n" +
    "orcmat.orcmat_qtde,  \n" +
    "OrcMat.ORCMATALTURA,   \n" +
    "OrcMat.ORCMATCOMPRIMENTO,	 \n" +
    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd1.Preco), 2) * prcftr1.fator as preco_uni_1,   \n" +
    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd1.Preco), 2) * prcftr1.fator * orcdet.orcdet_qtde as precoXqtd_1,   \n" +
    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd2.Preco), 2) * prcftr2.fator as preco_uni_2,   \n" +
    "round(sum(((orcmat.ORCMATALTURA / 100) * (orcmat.ORCMATCOMPRIMENTO / 100)) * prcprd2.Preco), 2) * prcftr2.fator * orcdet.orcdet_qtde as precoXqtd_2 \n" +
    ",OrcDet.orcdet_item \n" +
"from OrcMat \n" +
    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
    "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo \n" +
    "where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "' and prdgrp.Descricao = 'Painel frigorifico' \n" +
      "group by OrcMat.numeroOrcamento,   \n" +
    "OrcMat.numeroOrcamento,   \n" +
    "OrcMat.orcmatbase,   \n" +
    "prdgrp.Grupo,   \n" +
    "prdgrp.grupobusca,   \n" +
    "prdgrp.Subgrupo,   \n" +
    "prdgrp.descricao,   \n" +
    "OrcMat.orcmat_codigo_pai,   \n" +
    "OrcMat.orcmat_descricao,   \n" +
    "OrcMat.OrcMatCorPesquisa,   \n" +
    "OrcMat.ORCMATALTURA,   \n" +
    "OrcMat.ORCMATCOMPRIMENTO,  \n" +
    "orcdet.orcdet_item,  \n" +
    "orcmat.orcmat_qtde,  \n" +
    "prcftr1.fator,  \n" +
    "prcftr2.fator,  \n" +
    "OrcDet.orcdet_item \n" +
    ",OrcDet.orcdet_qtde; \n" +
            "create table #final ( \n" +
"Descricao nvarchar(255), \n" +
"Codigo_pai nvarchar(100), \n" +
"Descricao_BAAN nvarchar(255), \n" +
"qtde int, \n" +
"Altura int, \n" +
"Comprimento int, \n" +
"Preco_uni_antigo float, \n" +
"Preco_qtd_antigo float, \n" +
"Variacao float, \n" +
"Preco_uni_novo float, \n" +
"Preco_qtd_novo float) \n" +
"insert into #final \n" +
            "select \n" +
    "#temp_painel.descricao,  \n" +
    "#temp_painel.orcmat_codigo_pai,  \n" +
    "#temp_painel.orcmat_descricao,  \n" +
    "#temp_painel.orcmat_qtde,  \n" +
    "#temp_painel.ORCMATALTURA,  \n" +
    "#temp_painel.ORCMATCOMPRIMENTO,  \n" +
    "round(#temp_painel.preco_uni_1,2) as preco_uni_1,  \n" +
    "round(sum(#temp_painel.precoXqtd_1),2) as precoXqtd_1,  \n" +
    "round((-1 * (100 * (1 - (preco_uni_2 / nullif(preco_uni_1, 0))))), 2) as Variacao, \n" +
    "round(#temp_painel.preco_uni_2,2) as preco_uni_2,  \n" +
    "round(sum(#temp_painel.precoXqtd_2),2) as precoXqtd_2  \n" +
"from #temp_painel  \n" +
"group by \n" +
"#temp_painel.descricao,  \n" +
"#temp_painel.orcmat_codigo_pai,  \n" +
"#temp_painel.orcmat_descricao,  \n" +
"#temp_painel.orcmat_qtde,  \n" +
"#temp_painel.ORCMATALTURA,  \n" +
"#temp_painel.ORCMATCOMPRIMENTO,  \n" +
"#temp_painel.preco_uni_1,  \n" +
"#temp_painel.preco_uni_2  \n" +
    "union all \n" +
    "select \n" +
    "#temp_comp_cam.Descricao,  \n" +
    "#temp_comp_cam.Codigo_pai,  \n" +
    "#temp_comp_cam.orcmat_descricao,  \n" +
    "#temp_comp_cam.orcmat_tde as orcmat_qtde,  \n" +
    "#temp_comp_cam.Altura,  \n" +
    "#temp_comp_cam.Comprimento,  \n" +
    "round(#temp_comp_cam.Preco_uni_1,2) as Preco_uni_1,  \n" +
    "round(sum(#temp_comp_cam.PrecoXqtde_1),2) as PrecoXqtde_1,  \n" +
    "round((-1 * (100 * (1 - (preco_uni_2 / nullif(preco_uni_1, 0))))), 2) as Variacao, \n" +
    "round(#temp_comp_cam.Preco_uni_2,2) as Preco_uni_2,	  \n" +
    "round(sum(#temp_comp_cam.PrecoXqtde_2),2) as PrecoXqtde_2  \n" +
"from #temp_comp_cam  \n" +
    "group by \n" +
"#temp_comp_cam.Descricao,  \n" +
"#temp_comp_cam.Codigo_pai,  \n" +
"#temp_comp_cam.orcmat_descricao,  \n" +
"#temp_comp_cam.orcmat_tde,  \n" +
"#temp_comp_cam.Altura,  \n" +
"#temp_comp_cam.Comprimento,  \n" +
"#temp_comp_cam.Preco_uni_1,	  \n" +
"#temp_comp_cam.Preco_uni_2; \n" +
"select \n" +
"Descricao, \n" +
"Codigo_pai, \n" +
"Descricao_BAAN, \n" +
"qtde, \n" +
"Altura, \n" +
"Comprimento, \n" +
"sum(Preco_qtd_antigo) as Preco_qtd_antigoas, \n" +
"Variacao, \n" +
"sum(Preco_qtd_novo) as Preco_qtd_novo \n" +
"from #final \n" +
"group by Descricao, \n" +
"Codigo_pai, \n" +
"Descricao_BAAN, \n" +
"qtde, \n" +
"Altura, \n" +
"Comprimento, \n" +
"Variacao";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            dataGridView1.Columns[6].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "c2";


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dataGridView1.Columns[7].DefaultCellStyle.Format = "#0.00\\%";

            }


        }

        private void button_componentes_gondolas_Click(object sender, EventArgs e)
        {
            Nome_planilha = "Componentes_gondolas";
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "with dados as ( \n" +
"select \n" +
    "OrcDet.orcdet_item,   \n" +
    "prdgrp.Descricao,    \n" +
    "orcdet.orcdet_corte,   \n" +
    "orcdet.orcdet_Comprimento,   \n" +
    "orcmat.orcmat_descricao, \n" +
    "orcmat.orcmat_codigo_pai, \n" +
    "orcdet.orcdet_qtde, \n" +
    "round(sum((round(prcprd1.preco, 2) * prcftr1.fator)), 2) as Preco_uni_1, \n" +
    "round(sum(round(prcprd1.preco, 2) * prcftr1.fator * orcdet.orcdet_qtde), 2) as precoXqtd_1,    \n" +
    "round(sum((round(prcprd2.preco, 2) * prcftr2.fator)), 2) as Preco_uni_2, \n" +
    "round(sum(round(prcprd2.preco, 2) * prcftr2.fator * orcdet.orcdet_qtde), 2) as precoXqtd_2 \n" +
"from OrcMat \n" +
    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
    "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo \n" +
"where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "'  and prdgrp.Grupo = '1' \n" +
"group by \n" +
    "OrcMat.orcmatbase,    \n" +
    "prdgrp.Descricao,    \n" +
    "prdgrp.Descricao,    \n" +
    "OrcDet.orcdet_corte,   \n" +
    "orcdet.orcdet_Comprimento,  \n" +
    "OrcDet.orcdet_item,   \n" +
    "orcdet.orcdet_nr_seq_crt, \n" +
    "orcmat.orcmat_descricao, \n" +
    "orcmat.orcmat_codigo_pai, \n" +
    "orcdet.orcdet_qtde) \n" +
"select \n" +
"dados.orcdet_item, \n" +
"dados.Descricao, \n" +
"dados.orcdet_corte, \n" +
"dados.orcdet_Comprimento, \n" +
"dados.orcmat_descricao, \n" +
"dados.orcmat_codigo_pai, \n" +
"dados.orcdet_qtde, \n" +
"dados.Preco_uni_1, \n" +
"dados.precoXqtd_1, \n" +
"round((-1 * (100 * (1 - (preco_uni_2 / nullif(preco_uni_1, 0))))), 2) as Variacao, \n" +
"dados.Preco_uni_2, \n" +
"dados.precoXqtd_2 \n" +
"from dados \n" +
"order by orcdet_item, orcdet_corte, orcdet_Comprimento, orcmat_codigo_pai";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns[7].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "c2";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dataGridView1.Columns[9].DefaultCellStyle.Format = "#0.00\\%";

            }

        }

        private void button_componentes_checkout_Click(object sender, EventArgs e)
        {
            Nome_planilha = "Componentes_checkout";
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "with dados as ( \n" +
"select \n" +
    "OrcDet.orcdet_item,   \n" +
    "prdgrp.Descricao,    \n" +
    "orcdet.orcdet_corte,   \n" +
    "orcdet.orcdet_Comprimento,   \n" +
    "orcmat.orcmat_descricao, \n" +
    "orcmat.orcmat_codigo_pai, \n" +
    "orcdet.orcdet_qtde, \n" +
    "round(sum((round(prcprd1.preco, 2) * prcftr1.fator)), 2) as Preco_uni_1, \n" +
    "round(sum(round(prcprd1.preco, 2) * prcftr1.fator * orcdet.orcdet_qtde), 2) as precoXqtd_1,    \n" +
    "round(sum((round(prcprd2.preco, 2) * prcftr2.fator)), 2) as Preco_uni_2, \n" +
    "round(sum(round(prcprd2.preco, 2) * prcftr2.fator * orcdet.orcdet_qtde), 2) as precoXqtd_2 \n" +
"from OrcMat \n" +
    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
    "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo \n" +
"where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "'  and prdgrp.Grupo = '2' \n" +
"group by \n" +
    "OrcMat.orcmatbase,    \n" +
    "prdgrp.Descricao,    \n" +
    "prdgrp.Descricao,    \n" +
    "OrcDet.orcdet_corte,   \n" +
    "orcdet.orcdet_Comprimento,  \n" +
    "OrcDet.orcdet_item,   \n" +
    "orcdet.orcdet_nr_seq_crt, \n" +
    "orcmat.orcmat_descricao, \n" +
    "orcmat.orcmat_codigo_pai, \n" +
    "orcdet.orcdet_qtde) \n" +
"select \n" +
"dados.orcdet_item, \n" +
"dados.Descricao, \n" +
"dados.orcdet_corte, \n" +
"dados.orcdet_Comprimento, \n" +
"dados.orcmat_descricao, \n" +
"dados.orcmat_codigo_pai, \n" +
"dados.orcdet_qtde, \n" +
"dados.Preco_uni_1, \n" +
"dados.precoXqtd_1, \n" +
"round((-1 * (100 * (1 - (preco_uni_2 / nullif(preco_uni_1, 0))))), 2) as Variacao, \n" +
"dados.Preco_uni_2, \n" +
"dados.precoXqtd_2 \n" +
"from dados \n" +
"order by orcdet_item, orcdet_corte, orcdet_Comprimento, orcmat_codigo_pai";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns[7].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "c2";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dataGridView1.Columns[9].DefaultCellStyle.Format = "#0.00\\%";

            }
        }

        private void button_componentes_mobilias_Click(object sender, EventArgs e)
        {
            Nome_planilha = "Componentes_mobilias";
            string ssqlconnectionstring = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
            string query1 = "with dados as ( \n" +
"select \n" +
    "OrcDet.orcdet_item,   \n" +
    "prdgrp.Descricao,    \n" +
    "orcdet.orcdet_corte,   \n" +
    "orcdet.orcdet_Comprimento,   \n" +
    "orcmat.orcmat_descricao, \n" +
    "orcmat.orcmat_codigo_pai, \n" +
    "orcdet.orcdet_qtde, \n" +
    "round(sum((round(prcprd1.preco, 2) * prcftr1.fator)), 2) as Preco_uni_1, \n" +
    "round(sum(round(prcprd1.preco, 2) * prcftr1.fator * orcdet.orcdet_qtde), 2) as precoXqtd_1,    \n" +
    "round(sum((round(prcprd2.preco, 2) * prcftr2.fator)), 2) as Preco_uni_2, \n" +
    "round(sum(round(prcprd2.preco, 2) * prcftr2.fator * orcdet.orcdet_qtde), 2) as precoXqtd_2 \n" +
"from OrcMat \n" +
    "left join prdgrp on orcmat.orcmatbase = prdgrp.base and orcmat.OrcMatGrupo = prdgrp.Grupo and orcmat.OrcMatSubGrupo = prdgrp.Subgrupo \n" +
    "left join prcprd as prcprd1 on orcmat.orcmatbase = prcprd1.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd1.Produto and orcmat.OrcMatCorPesquisa = prcprd1.Sigla_Cor and prcprd1.Prcprdcab_descricao = '" + textBox_lista_antiga.Text + "' \n" +
    "left join prccab as prccab1 on prcprd1.BaseCodigo = prccab1.BaseCodigo and prcprd1.Prcprdcab_descricao = prccab1.lista \n" +
    "left join prcftr as prcftr1 on prccab1.BaseCodigo = prcftr1.BaseCodigo and prccab1.lista_fator = prcftr1.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr1.grupo and orcmat.OrcMatSubGrupo = prcftr1.subgrupo \n" +
    "left join prcprd as prcprd2 on orcmat.orcmatbase = prcprd2.BaseCodigo and orcmat.orcmat_codigo_pai = prcprd2.Produto and orcmat.OrcMatCorPesquisa = prcprd2.Sigla_Cor and prcprd2.Prcprdcab_descricao = '" + textBox_lista_nova.Text + "' \n" +
    "left join prccab as prccab2 on prcprd2.BaseCodigo = prccab2.BaseCodigo and prcprd2.Prcprdcab_descricao = prccab2.lista \n" +
    "left join prcftr as prcftr2 on prccab2.BaseCodigo = prcftr2.BaseCodigo and prccab2.lista_fator = prcftr2.Prcftrcab_descricao and orcmat.OrcMatGrupo = prcftr2.grupo and orcmat.OrcMatSubGrupo = prcftr2.subgrupo \n" +
    "LEFT join OrcDet on OrcMat.numeroOrcamento = OrcDet.numeroOrcamento and OrcMat.orcmat_codigo_pai = OrcDet.ORCDET_CODIGO_ORI and OrcMat.orcmat_codigo = OrcDet.orcdet_codigo \n" +
"where orcmat.numeroOrcamento = '" + textBox_orcamento.Text + "'  and prdgrp.Grupo = '3' \n" +
"group by \n" +
    "OrcMat.orcmatbase,    \n" +
    "prdgrp.Descricao,    \n" +
    "prdgrp.Descricao,    \n" +
    "OrcDet.orcdet_corte,   \n" +
    "orcdet.orcdet_Comprimento,  \n" +
    "OrcDet.orcdet_item,   \n" +
    "orcdet.orcdet_nr_seq_crt, \n" +
    "orcmat.orcmat_descricao, \n" +
    "orcmat.orcmat_codigo_pai, \n" +
    "orcdet.orcdet_qtde) \n" +
"select \n" +
"dados.orcdet_item, \n" +
"dados.Descricao, \n" +
"dados.orcdet_corte, \n" +
"dados.orcdet_Comprimento, \n" +
"dados.orcmat_descricao, \n" +
"dados.orcmat_codigo_pai, \n" +
"dados.orcdet_qtde, \n" +
"dados.Preco_uni_1, \n" +
"dados.precoXqtd_1, \n" +
"round((-1 * (100 * (1 - (preco_uni_2 / nullif(preco_uni_1, 0))))), 2) as Variacao, \n" +
"dados.Preco_uni_2, \n" +
"dados.precoXqtd_2 \n" +
"from dados \n" +
"order by orcdet_item, orcdet_corte, orcdet_Comprimento, orcmat_codigo_pai";
            SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
            SqlCommand sqlcmd = new SqlCommand(query1, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            sqlconn.Close();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns[7].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "c2";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "c2";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dataGridView1.Columns[9].DefaultCellStyle.Format = "#0.00\\%";

            }
        }

        private void button_exportar_Click(object sender, EventArgs e)
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
                string folderPath = @"C:\WBC\Ferramentas\Aplicativos\Comparativos\01-Exportar\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, Nome_planilha);
                    wb.SaveAs(folderPath + Nome_planilha + ".xlsx");
                }
                Cursor.Current = Cursors.Default;

                MessageBox.Show("Tabela exportada com sucesso!");

            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
    }
}
