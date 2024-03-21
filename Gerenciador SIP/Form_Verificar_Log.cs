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
    public partial class Form_Verificar_Log : Form
    {
        List<string> valor_Gerenciador_SIP;
        string Login;
        Thread t1;
        string Banco = "BANCODEDADOS";
        string Instancia = @"INSTANCIA";
        string Senha = "SENHA";
        string Usuario = "sa";
        private List<string> modelosOriginais;

        public Form_Verificar_Log(List<string> valorGerenciador, string login)
        {
            Login = login;
            valor_Gerenciador_SIP = valorGerenciador;
            InitializeComponent();
            modelosOriginais = new List<string>();
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
            Application.Run(new Form_Planilha_Eng(valor_Gerenciador_SIP, Login));
        }

        private void Form_Verificar_Log_Load(object sender, EventArgs e)
        {
            CarregarCheckBoxList();
            tabControl1.TabPages.Remove(tabpageExpositor);
            tabControl1.TabPages.Remove(tabpageCabeceiras);
            tabControl1.TabPages.Remove(tabpageAcessorios);

            if (valor_Gerenciador_SIP != null && valor_Gerenciador_SIP.Contains("Adm Eletrofrio"))
            {
                button_cadastrado_expositor.Enabled = true;
                button_cadastrar_cabeceira.Enabled = true;
                button_acessorios_cadastro.Enabled = true;
            }
        }

        private void CarregarCheckBoxList()
        {
            Cursor.Current = Cursors.WaitCursor;
            using (SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + ""))
            {
                con.Open();
                string query = "select distinct Modelo from Notificacao where (Data_Verificado is null or Verificado_por is null)";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        modelosOriginais.Clear(); // Limpa os itens anteriores na lista, se houver algum

                        while (reader.Read())
                        {
                            string modelo = reader["Modelo"].ToString();
                            modelosOriginais.Add(modelo); // Adicione o modelo à lista
                        }
                    }
                }
                con.Close();
            }
            // Após preencher a lista, carregue o checkedListBox1
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(modelosOriginais.ToArray());
            Cursor.Current = Cursors.Default;
        }

        private void textBox_pesquisaExpositor_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_pesquisaExpositor.Text))
            {
                // Filtro digitado pelo usuário
                string filtro = textBox_pesquisaExpositor.Text.ToLower();

                // Filtra os modelos com base no texto digitado
                List<string> modelosFiltrados = modelosOriginais
                    .Where(modelo => modelo.ToLower().Contains(filtro))
                    .ToList();

                // Atualiza o conteúdo do checkedListBox1 com os modelos filtrados
                checkedListBox1.Items.Clear();
                checkedListBox1.Items.AddRange(modelosFiltrados.ToArray());
            }
            else
            {
                // Se o filtro estiver vazio, exibe todos os modelos originais.
                checkedListBox1.Items.Clear();
                checkedListBox1.Items.AddRange(modelosOriginais.ToArray());
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Suponha que você tenha um TabControl chamado tabControl1 e uma TabPage chamada tabPage1.

            // Ocultar a tabPage1
            tabControl1.TabPages.Remove(tabpageExpositor);
            tabControl1.TabPages.Remove(tabpageCabeceiras);
            tabControl1.TabPages.Remove(tabpageAcessorios);

            // Verifica se o item está sendo marcado
            if (e.NewValue == CheckState.Checked)
            {
                // Desmarca todos os outros itens
                for (int i = 0; i < checkedListBox1.Items.Count; i++)

                {
                    if (i != e.Index)
                    {
                        checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }

                // Chama CarregarDataGridDe() com base no item marcado
                string itemMarcado = checkedListBox1.Items[e.Index].ToString();
                CarregarDataGridDe(itemMarcado);
                VerificarColunasLiberadas(itemMarcado);
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void VerificarColunasLiberadas(string modelo)
        {
            Cursor.Current = Cursors.WaitCursor;
            using (SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + ""))
            {
                con.Open();
                string query = $"SELECT Observacao \n" +
                                    "FROM Notificacao \n" +
                                    $"WHERE Modelo = '{modelo}' AND(Data_Verificado IS NULL OR Verificado_por IS NULL) \n" +
                                    "GROUP BY Observacao \n" +
                                    "ORDER BY \n" +
                                        "MAX( \n" +
                                            "CASE \n" +
                                                "WHEN Observacao LIKE '%expositor%' THEN 1 \n" +
                                                "WHEN Observacao LIKE '%acessorios%' THEN 2 \n" +
                                                "WHEN Observacao LIKE '%cabeceira%' THEN 3 \n" +
                                                "ELSE 4 \n" +
                                            "END \n" +
                                        "); ";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string observacao = reader["Observacao"].ToString();

                            // Verificar a observacao e liberar o TabControl correspondente
                            LiberarTabControl(observacao);
                        }
                    }
                }
                con.Close();
            }
        }

        private void LiberarTabControl(string observacao)
        {
            // Lógica para liberar as páginas do TabControl com base na observacao


            // Função para verificar se a TabPage já está presente
            bool TabPageExiste(TabPage tabPage)
            {
                return tabControl1.TabPages.Contains(tabPage);
            }

            if (observacao.Contains("expositor") && !TabPageExiste(tabpageExpositor))
            {
                // Liberar a página "Expositor"
                tabControl1.TabPages.Add(tabpageExpositor);
            }

            if (observacao.Contains("acessorios") && !TabPageExiste(tabpageAcessorios))
            {
                // Liberar a página "Acessorios"
                tabControl1.TabPages.Add(tabpageAcessorios);
            }

            if (observacao.Contains("cabeceira") && !TabPageExiste(tabpageCabeceiras))
            {
                // Liberar a página "Cabeceira"
                tabControl1.TabPages.Add(tabpageCabeceiras);
            }
        }





        private void CarregarDataGridDe(string modelo)
        {
            string query = "select distinct Usuario, Modelo, Observacao, Data from Notificacao \n" +
               $"Where Modelo = '{modelo}' and (Data_Verificado is null or Verificado_por is null)";

            using (SqlConnection con = new SqlConnection(@"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + ""))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Associar o DataTable ao DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Configurar o modo de redimensionamento automático para todas as células
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    }
                }
                con.Close();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica se a TabPage Expositores está selecionada
            if (tabControl1.SelectedTab == tabpageExpositor)
            {
                // Execute o procedimento desejado
                ProcedimentoParaExpositores();
                CompararDataGridViews(dataGridView_expositor1, dataGridView_expositor2);
            }
            else if (tabControl1.SelectedTab == tabpageCabeceiras)
            {
                ProcedimentoParaCabeceiras();
                CompararDataGridViews(dataGridView2_cabeceira1, dataGridView2_cabeceira2);
            }
            else if (tabControl1.SelectedTab == tabpageAcessorios)
            {
                ProcedimentoParaAcessorios();
                CompararDataGridViews(dataGridView4_acessorios, dataGridView5_acessorio);
            }

        }

        private void CompararDataGridViews(DataGridView dgv1, DataGridView dgv2)
        {
            // Certifique-se de que ambos os DataGridViews têm o mesmo número de linhas e colunas
            if (dgv1.Rows.Count != dgv2.Rows.Count || dgv1.Columns.Count != dgv2.Columns.Count)
            {
                MessageBox.Show("Os DataGridViews têm tamanhos diferentes.");
                return;
            }

            // Iterar pelas linhas
            for (int i = 0; i < dgv1.Rows.Count; i++)
            {
                // Iterar pelas células em cada linha
                for (int j = 0; j < dgv1.Columns.Count; j++)
                {
                    // Comparar valores das células
                    if (!dgv1.Rows[i].Cells[j].Value.Equals(dgv2.Rows[i].Cells[j].Value))
                    {
                        // Se os valores forem diferentes, destacar as células
                        dgv1.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                        dgv2.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                    }
                }
            }
        }

        //EXPOSITOR
        private void ProcedimentoParaExpositores()
        {
            string modelo = "";
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                // Verificar se o item na posição 'i' está selecionado
                if (checkedListBox1.GetItemChecked(i))
                {
                    // Faça algo com o item selecionado, por exemplo, exiba em uma MessageBox
                    modelo = checkedListBox1.Items[i].ToString();
                }
            }
            // Sua string de conexão
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

            // Lista para armazenar os resultados
            List<ResultadoConsulta> resultados = new List<ResultadoConsulta>();

            // Sua string de consulta
            string consulta = $"select id,Alteracao_de, Alteracao_para from Notificacao where Observacao like '%expositor%' and Modelo = '{modelo}' and (Data_Verificado is null or Verificado_por is null)";

            // Executar a consulta e obter o resultado
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string alteracaoDe = reader["Alteracao_de"].ToString();
                            string alteracaoPara = reader["Alteracao_para"].ToString();
                            // Adicionar o resultado à lista
                            resultados.Add(new ResultadoConsulta
                            {
                                Id = id,
                                AlteracaoDe = alteracaoDe,
                                AlteracaoPara = alteracaoPara
                            });

                        }
                    }
                }
            }
            TabelaTemp_de_Modelos_Eng(resultados);
        }

        private List<DadosModelo> ParseAlteracao(string alteracao)
        {
            List<DadosModelo> dadosModelo = new List<DadosModelo>();

            string[] valores = alteracao.Split(',');

            dadosModelo.Add(new DadosModelo
            {
                Modelo = valores[0],
                Tipo = valores[1],
                Resfriamento = valores[2],
                Situacao = valores[3],
                Linha = valores[4],
                Travar_representante = valores[5],
                Profundidade = valores[6]
                // Adicione outras propriedades conforme necessário
            });

            return dadosModelo;
        }
        private List<DadosModeloCabeceira> ParseAlteracaoCabeceira(string alteracao)
        {
            List<DadosModeloCabeceira> dadosModelo = new List<DadosModeloCabeceira>();

            string[] valores = alteracao.Split(',');

            while (valores.Length < 4)
            {
                // Adicione valores vazios até que haja pelo menos 4 elementos
                valores = valores.Concat(new string[] { "" }).ToArray();
            }

            dadosModelo.Add(new DadosModeloCabeceira
            {
                Cabeceira = valores[0],
                Codigo = valores[1],
                Esq_Dir = valores[2],
                Expositor = valores[3],
                Acessorios = valores.Length > 4 ? valores[4] : string.Empty
                // Adicione outras propriedades conforme necessário
            });

            return dadosModelo;
        }

        private List<DadosModeloAcessorio> ParseAlteracaoAcessorio(string alteracao)
        {
            List<DadosModeloAcessorio> dadosModelo = new List<DadosModeloAcessorio>();

            string[] valores = alteracao.Split(',');

            dadosModelo.Add(new DadosModeloAcessorio
            {
                Modelo = valores[1],
                Componente = valores[2],
                Cores = valores[3],
                M125 = valores[4],
                M150 = valores[5],
                M187 = valores[6],
                M225 = valores[7],
                M250 = valores[8],
                M300 = valores[9],
                M375 = valores[10],
                E45 = valores[11],
                E90 = valores[12],
                I45 = valores[13],
                I90 = valores[14],
                T1875 = valores[15],
                T2167 = valores[16],
                T2500 = valores[17],
                T360 = valores[18],
                Observacao = valores[19],
                LiberarRepresentante = valores[20],
                Acessorios_CAB = valores[21],
                BaseCodigo = valores[22]
                // Adicione outras propriedades conforme necessário
            });

            return dadosModelo;
        }

        // Classe para armazenar os resultados
        private class ResultadoConsulta
        {
            public int Id { get; set; }
            public string AlteracaoDe { get; set; }
            public string AlteracaoPara { get; set; }
            public string Observacao { get; set; }
            // public List<DadosModelo> DadosModelo { get; set; }
        }

        // Classe para armazenar os dados do modelo
        private class DadosModelo
        {
            public string Modelo { get; set; }
            public string Tipo { get; set; }
            public string Resfriamento { get; set; }
            public string Situacao { get; set; }
            public string Linha { get; set; }
            public string Travar_representante { get; set; }
            public string BaseCodigo { get; set; }
            public string Profundidade { get; set; }
            // Adicione outras propriedades conforme necessário
        }

        private class DadosModeloCabeceira
        {
            public string Cabeceira { get; set; }
            public string Codigo { get; set; }
            public string Esq_Dir { get; set; }
            public string Expositor { get; set; }
            public string Acessorios { get; set; }
        }

        private class DadosModeloAcessorio
        {
            public string ID { get; set; }
            public string Modelo { get; set; }
            public string Componente { get; set; }
            public string Cores { get; set; }
            public string M125 { get; set; }
            public string M150 { get; set; }
            public string M187 { get; set; }
            public string M225 { get; set; }
            public string M250 { get; set; }
            public string M300 { get; set; }
            public string M375 { get; set; }
            public string E45 { get; set; }
            public string E90 { get; set; }
            public string I45 { get; set; }
            public string I90 { get; set; }
            public string T1875 { get; set; }
            public string T2167 { get; set; }
            public string T2500 { get; set; }
            public string T360 { get; set; }
            public string Observacao { get; set; }
            public string LiberarRepresentante { get; set; }
            public string Acessorios_CAB { get; set; }
            public string BaseCodigo { get; set; }

        }

        private void TabelaTemp_de_Modelos_Eng(List<ResultadoConsulta> ListaResultado)
        {
            // Criar um DataTable para armazenar os dados do dataGridView2
            DataTable dt2 = new DataTable();

            // Adicionar colunas ao DataTable
            dt2.Columns.Add("Id");
            dt2.Columns.Add("Modelo");
            dt2.Columns.Add("Tipo");
            dt2.Columns.Add("Resfriamento");
            dt2.Columns.Add("Situacao");
            dt2.Columns.Add("Linha");
            dt2.Columns.Add("Travar_representante");
            dt2.Columns.Add("Profundidade");

            // Criar um DataTable para armazenar os dados do dataGridView3
            DataTable dt3 = new DataTable();

            // Adicionar colunas ao DataTable
            dt3.Columns.Add("Id");
            dt3.Columns.Add("Modelo");
            dt3.Columns.Add("Tipo");
            dt3.Columns.Add("Resfriamento");
            dt3.Columns.Add("Situacao");
            dt3.Columns.Add("Linha");
            dt3.Columns.Add("Travar_representante");
            dt3.Columns.Add("Profundidade");

            // Iterar sobre os elementos da lista
            foreach (var resultado in ListaResultado)
            {
                List<DadosModelo> dadosModelo = null;
                // Obter a lista de DadosModelo
                if (resultado.AlteracaoDe != "")
                {
                    dadosModelo = ParseAlteracao(resultado.AlteracaoDe);
                }

                if (dadosModelo != null)
                {
                    // Preencher o DataTable do dataGridView2 com os dados
                    foreach (var modelo in dadosModelo)
                    {
                        dt2.Rows.Add(resultado.Id, modelo.Modelo, modelo.Tipo, modelo.Resfriamento, modelo.Situacao,
                            modelo.Linha, modelo.Travar_representante, modelo.Profundidade);
                    }
                }
                else
                {
                    // Adicionar uma linha vazia ao DataTable do dataGridView2
                    dt2.Rows.Add(resultado.Id, "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO");
                }

                List<DadosModelo> dadosModelo2 = null;
                // Obter a lista de DadosModelo
                if (resultado.AlteracaoPara != "")
                {
                    dadosModelo2 = ParseAlteracao(resultado.AlteracaoPara);
                }

                if (dadosModelo2 != null)
                {
                    // Preencher o DataTable do dataGridView3 com os dados
                    foreach (var modelo in dadosModelo2)
                    {
                        dt3.Rows.Add(resultado.Id, modelo.Modelo, modelo.Tipo, modelo.Resfriamento, modelo.Situacao,
                            modelo.Linha, modelo.Travar_representante, modelo.Profundidade);
                    }
                }
                else
                {
                    // Adicionar uma linha vazia ao DataTable do dataGridView3
                    dt3.Rows.Add(resultado.Id, "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO");
                }
            }

            // Configurar o DataGridView2
            dataGridView_expositor1.AutoGenerateColumns = true;
            dataGridView_expositor1.DataSource = dt2.Copy(); // Use Copy() para criar uma nova instância do DataTable

            // Configurar o DataGridView3
            dataGridView_expositor2.AutoGenerateColumns = true;
            dataGridView_expositor2.DataSource = dt3.Copy(); // Use Copy() para criar uma nova instância do DataTable
        }




        private void button_cadastrado_expositor_Click(object sender, EventArgs e)
        {
            // Definir o cursor como "Wait"
            this.Cursor = Cursors.WaitCursor;

            // Chamar a função que faz o processamento
            CompararLinhasSelecionadas();

            // Restaurar o cursor padrão após a conclusão do processamento
            this.Cursor = Cursors.Default;
        }

        private void CompararLinhasSelecionadas()
        {
            // Obter os IDs selecionados do DataGridView1
            List<int> idsDataGridView1 = ObterIdsSelecionados(dataGridView_expositor1);

            // Obter os IDs selecionados do DataGridView2
            List<int> idsDataGridView2 = ObterIdsSelecionados(dataGridView_expositor2);

            // Encontrar IDs diferentes entre as linhas selecionadas
            List<int> idsDiferentes = EncontrarIdsDiferentes(idsDataGridView1, idsDataGridView2);

            if (idsDiferentes.Any())
            {
                string mensagem = "IDs diferentes nas linhas selecionadas:\n" + string.Join(", ", idsDiferentes);
                MessageBox.Show(mensagem);
            }
            else
            {
                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                foreach (int id in idsDataGridView1)
                {
                    var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string query_status = $"update Notificacao set Verificado_por = '{Login}', Data_Verificado = '{dataAtual}' where ID = '{id}'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command1 = new SqlCommand(query_status, connection);
                        command1.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                ProcedimentoParaExpositores();
            }
        }

        private void CompararLinhasSelecionadasCabeceiras()
        {
            // Obter os IDs selecionados do DataGridView1
            List<int> idsDataGridView1 = ObterIdsSelecionados(dataGridView2_cabeceira1);

            // Obter os IDs selecionados do DataGridView2
            List<int> idsDataGridView2 = ObterIdsSelecionados(dataGridView2_cabeceira2);

            // Encontrar IDs diferentes entre as linhas selecionadas
            List<int> idsDiferentes = EncontrarIdsDiferentes(idsDataGridView1, idsDataGridView2);

            if (idsDiferentes.Any())
            {
                string mensagem = "IDs diferentes nas linhas selecionadas:\n" + string.Join(", ", idsDiferentes);
                MessageBox.Show(mensagem);
            }
            else
            {
                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                foreach (int id in idsDataGridView1)
                {
                    var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string query_status = $"update Notificacao set Verificado_por = '{Login}', Data_Verificado = '{dataAtual}' where ID = '{id}'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command1 = new SqlCommand(query_status, connection);
                        command1.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                ProcedimentoParaCabeceiras();
            }
        }

        private List<int> ObterIdsSelecionados(DataGridView dataGridView)
        {
            List<int> idsSelecionados = new List<int>();

            // Verificar se há linhas selecionadas no DataGridView
            if (dataGridView.SelectedRows.Count > 0)
            {
                // Iterar pelas linhas selecionadas e obter o valor da coluna "ID"
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    if (row.Cells["ID"].Value != null && int.TryParse(row.Cells["ID"].Value.ToString(), out int id))
                    {
                        idsSelecionados.Add(id);
                    }
                }
            }

            return idsSelecionados;
        }

        private List<int> EncontrarIdsDiferentes(List<int> ids1, List<int> ids2)
        {
            // Encontrar IDs que estão em uma lista, mas não na outra
            List<int> idsDiferentes = ids1.Except(ids2).Union(ids2.Except(ids1)).ToList();
            return idsDiferentes;
        }

        // CABECEIRA

        private void button_cadastrar_cabeceira_Click(object sender, EventArgs e)
        {
            // Definir o cursor como "Wait"
            this.Cursor = Cursors.WaitCursor;

            // Chamar a função que faz o processamento
            CompararLinhasSelecionadasCabeceiras();

            // Restaurar o cursor padrão após a conclusão do processamento
            this.Cursor = Cursors.Default;
        }

        private void ProcedimentoParaCabeceiras()
        {
            string modelo = "";
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                // Verificar se o item na posição 'i' está selecionado
                if (checkedListBox1.GetItemChecked(i))
                {
                    // Faça algo com o item selecionado, por exemplo, exiba em uma MessageBox
                    modelo = checkedListBox1.Items[i].ToString();
                }
            }
            // Sua string de conexão
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

            // Lista para armazenar os resultados
            List<ResultadoConsulta> resultados = new List<ResultadoConsulta>();

            // Sua string de consulta
            string consulta = $"select id,Alteracao_de, Alteracao_para,Observacao from Notificacao where Observacao like '%cabeceira%' and Modelo = '{modelo}' and (Data_Verificado is null or Verificado_por is null)";

            // Executar a consulta e obter o resultado
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string alteracaoDe = reader["Alteracao_de"].ToString();
                            string alteracaoPara = reader["Alteracao_para"].ToString();
                            string observacao = reader["Observacao"].ToString();
                            // Adicionar o resultado à lista
                            resultados.Add(new ResultadoConsulta
                            {
                                Id = id,
                                AlteracaoDe = alteracaoDe,
                                AlteracaoPara = alteracaoPara,
                                Observacao = observacao
                            });

                        }
                    }
                }
            }
            TabelaTemp_de_Cabeceiras_Eng(resultados);
        }

        private void TabelaTemp_de_Cabeceiras_Eng(List<ResultadoConsulta> ListaResultado)
        {
            // Criar um DataTable para armazenar os dados do dataGridView2
            DataTable dt2 = new DataTable();

            // Adicionar colunas ao DataTable
            //dt2.Columns.Add("Cabeceira");
            dt2.Columns.Add("ID");
            dt2.Columns.Add("Codigo");
            dt2.Columns.Add("Alteracao_de");
            dt2.Columns.Add("Alteracao_para");
            dt2.Columns.Add("Observacao");
            dt2.Columns.Add("Data");
            dt2.Columns.Add("Data_Verificado");
            dt2.Columns.Add("Verificado_por");

            // Criar um DataTable para armazenar os dados do dataGridView3
            DataTable dt3 = new DataTable();

            // Adicionar colunas ao DataTable
            //dt3.Columns.Add("Cabeceira");
            dt3.Columns.Add("ID");
            dt3.Columns.Add("Codigo");
            dt3.Columns.Add("Alteracao_de");
            dt3.Columns.Add("Alteracao_para");
            dt3.Columns.Add("Observacao");
            dt3.Columns.Add("Data");
            dt3.Columns.Add("Data_Verificado");
            dt3.Columns.Add("Verificado_por");

            // Iterar sobre os elementos da lista
            foreach (var resultado in ListaResultado)
            {
                List<DadosModeloCabeceira> dadosModelo = null;
                // Obter a lista de DadosModelo
                if (resultado.AlteracaoDe != "")
                {
                    dadosModelo = ParseAlteracaoCabeceira(resultado.AlteracaoDe);
                }
                List<DadosModeloCabeceira> dadosModelo2 = null;
                // Obter a lista de DadosModelo
                if (resultado.AlteracaoPara != "")
                {
                    dadosModelo2 = ParseAlteracaoCabeceira(resultado.AlteracaoPara);
                }

                if (dadosModelo != null)
                {
                    // Preencher o DataTable do dataGridView2 com os dados
                    foreach (var modelo in dadosModelo)
                    {
                        dt2.Rows.Add(resultado.Id, modelo.Cabeceira, modelo.Codigo, modelo.Esq_Dir, modelo.Expositor,
                            modelo.Acessorios);
                    }
                }
                else
                {
                    if (resultado.Observacao == "Alterar cabeceira")
                    {
                        // Preencher o DataTable do dataGridView3 com os dados
                        if (dadosModelo2 != null)
                        {
                            foreach (var modelo in dadosModelo2)
                            {
                                dt2.Rows.Add(resultado.Id, modelo.Cabeceira, modelo.Codigo, modelo.Esq_Dir, modelo.Expositor, "NOVO");
                            }
                        }
                       
                    }
                    else
                    {
                        // Adicionar uma linha vazia ao DataTable do dataGridView2
                        dt2.Rows.Add(resultado.Id, "NOVO", "NOVO", "NOVO", "NOVO", "NOVO");
                    }
                }


                if (dadosModelo2 != null)
                {
                    // Preencher o DataTable do dataGridView3 com os dados
                    foreach (var modelo in dadosModelo2)
                    {
                        dt3.Rows.Add(resultado.Id, modelo.Cabeceira, modelo.Codigo, modelo.Esq_Dir, modelo.Expositor,
                            modelo.Acessorios);
                    }
                }
                else
                {
                    if (resultado.Observacao == "Alterar cabeceira")
                    {
                        if (dadosModelo != null)
                        {
                            // Preencher o DataTable do dataGridView3 com os dados
                            foreach (var modelo in dadosModelo)
                            {
                                dt3.Rows.Add(resultado.Id, modelo.Cabeceira, modelo.Codigo, modelo.Esq_Dir, modelo.Expositor, "EXCLUIDO");
                            }
                        }
                      
                    }
                    else
                    {
                        // Adicionar uma linha vazia ao DataTable do dataGridView2
                        dt3.Rows.Add(resultado.Id, "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO");
                    }
                }
            }

            // Configurar o DataGridView2
            dataGridView2_cabeceira1.AutoGenerateColumns = true;
            dataGridView2_cabeceira1.DataSource = dt2.Copy(); // Use Copy() para criar uma nova instância do DataTable

            // Configurar o DataGridView3
            dataGridView2_cabeceira2.AutoGenerateColumns = true;
            dataGridView2_cabeceira2.DataSource = dt3.Copy(); // Use Copy() para criar uma nova instância do DataTable
        }

        // ACESSORIOS


        private void ProcedimentoParaAcessorios()
        {
            string modelo = "";
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                // Verificar se o item na posição 'i' está selecionado
                if (checkedListBox1.GetItemChecked(i))
                {
                    // Faça algo com o item selecionado, por exemplo, exiba em uma MessageBox
                    modelo = checkedListBox1.Items[i].ToString();
                }
            }
            // Sua string de conexão
            string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";

            // Lista para armazenar os resultados
            List<ResultadoConsulta> resultados = new List<ResultadoConsulta>();

            // Sua string de consulta
            string consulta = $"select id,Alteracao_de, Alteracao_para,Observacao from Notificacao where Observacao like '%acessorio%' and Modelo = '{modelo}' and (Data_Verificado is null or Verificado_por is null)";

            // Executar a consulta e obter o resultado
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string alteracaoDe = reader["Alteracao_de"].ToString();
                            string alteracaoPara = reader["Alteracao_para"].ToString();
                            string observacao = reader["Observacao"].ToString();
                            // Adicionar o resultado à lista
                            string alteracaoDeNew = "";
                            string alteracaoParaNew = "";

                            if (alteracaoDe.Contains(","))
                            {
                                alteracaoDeNew = id + "," + alteracaoDe;
                            }
                            else
                            {
                                alteracaoDeNew = alteracaoDe;
                            }

                            if (alteracaoPara.Contains(","))
                            {
                                alteracaoParaNew = id + "," + alteracaoPara;
                            }
                            else
                            {
                                alteracaoParaNew = alteracaoPara;
                            }

                            resultados.Add(new ResultadoConsulta
                            {
                                Id = id,
                                AlteracaoDe = alteracaoDeNew,
                                AlteracaoPara = alteracaoParaNew,
                                Observacao = observacao
                            });

                        }
                    }
                }
            }
            TabelaTemp_de_Acessorio_Eng(resultados);
        }

        private void TabelaTemp_de_Acessorio_Eng(List<ResultadoConsulta> ListaResultado)
        {
            // Criar um DataTable para armazenar os dados do dataGridView2
            DataTable dt2 = new DataTable();

            // Adicionar colunas ao DataTable
            dt2.Columns.Add("ID");
            dt2.Columns.Add("Modelo");
            dt2.Columns.Add("Componente");
            dt2.Columns.Add("Cores");
            dt2.Columns.Add("M125");
            dt2.Columns.Add("M150");
            dt2.Columns.Add("M187");
            dt2.Columns.Add("M225");
            dt2.Columns.Add("M250");
            dt2.Columns.Add("M300");
            dt2.Columns.Add("M375");
            dt2.Columns.Add("E45");
            dt2.Columns.Add("E90");
            dt2.Columns.Add("I45");
            dt2.Columns.Add("I90");
            dt2.Columns.Add("T1875");
            dt2.Columns.Add("T2167");
            dt2.Columns.Add("T2500");
            dt2.Columns.Add("T360");
            dt2.Columns.Add("Observacao");
            dt2.Columns.Add("LiberarRepresentante");
            dt2.Columns.Add("Acessarios_CAB");
            dt2.Columns.Add("BaseCodigo");

            // Criar um DataTable para armazenar os dados do dataGridView3
            DataTable dt3 = new DataTable();

            // Adicionar colunas ao DataTable
            dt3.Columns.Add("ID");
            dt3.Columns.Add("Modelo");
            dt3.Columns.Add("Componente");
            dt3.Columns.Add("Cores");
            dt3.Columns.Add("M125");
            dt3.Columns.Add("M150");
            dt3.Columns.Add("M187");
            dt3.Columns.Add("M225");
            dt3.Columns.Add("M250");
            dt3.Columns.Add("M300");
            dt3.Columns.Add("M375");
            dt3.Columns.Add("E45");
            dt3.Columns.Add("E90");
            dt3.Columns.Add("I45");
            dt3.Columns.Add("I90");
            dt3.Columns.Add("T1875");
            dt3.Columns.Add("T2167");
            dt3.Columns.Add("T2500");
            dt3.Columns.Add("T360");
            dt3.Columns.Add("Observacao");
            dt3.Columns.Add("LiberarRepresentante");
            dt3.Columns.Add("Acessarios_CAB");
            dt3.Columns.Add("BaseCodigo");

            // Iterar sobre os elementos da lista
            foreach (var resultado in ListaResultado)
            {
                List<DadosModeloAcessorio> dadosModelo = null;
                // Obter a lista de DadosModelo
                if (!string.IsNullOrEmpty(resultado.AlteracaoDe) && resultado.AlteracaoDe.Contains(","))
                {
                    dadosModelo = ParseAlteracaoAcessorio(resultado.AlteracaoDe);
                }
                List<DadosModeloAcessorio> dadosModelo2 = null;
                // Obter a lista de DadosModelo
                if (!string.IsNullOrEmpty(resultado.AlteracaoPara) && resultado.AlteracaoPara.Contains(","))
                {
                    dadosModelo2 = ParseAlteracaoAcessorio(resultado.AlteracaoPara);
                }

                if (dadosModelo != null)
                {
                    // Preencher o DataTable do dataGridView2 com os dados
                    foreach (var modelo in dadosModelo)
                    {
                        dt2.Rows.Add(resultado.Id, modelo.Modelo, modelo.Componente, modelo.Cores, modelo.M125,
                            modelo.M150, modelo.M187, modelo.M225, modelo.M250, modelo.M300, modelo.M375, modelo.E45, modelo.E90, modelo.I45, modelo.I90,
                            modelo.T1875, modelo.T2167, modelo.T2500, modelo.T360, modelo.Observacao, modelo.LiberarRepresentante, modelo.Acessorios_CAB, modelo.BaseCodigo);
                    }
                }
                else
                {

                    // Adicionar uma linha vazia ao DataTable do dataGridView2
                    dt2.Rows.Add(resultado.Id, "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO", "NOVO");

                }


                if (dadosModelo2 != null)
                {
                    // Preencher o DataTable do dataGridView3 com os dados
                    foreach (var modelo in dadosModelo2)
                    {
                        dt3.Rows.Add(resultado.Id, modelo.Modelo, modelo.Componente, modelo.Cores, modelo.M125,
                           modelo.M150, modelo.M187, modelo.M225, modelo.M250, modelo.M300, modelo.M375, modelo.E45, modelo.E90, modelo.I45, modelo.I90,
                           modelo.T1875, modelo.T2167, modelo.T2500, modelo.T360, modelo.Observacao, modelo.LiberarRepresentante, modelo.Acessorios_CAB, modelo.BaseCodigo);
                    }
                }
                else
                {

                    // Adicionar uma linha vazia ao DataTable do dataGridView2
                    dt3.Rows.Add(resultado.Id, "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO",
                        "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO", "EXCLUIDO");

                }
            }

            // Configurar o DataGridView2
            dataGridView4_acessorios.AutoGenerateColumns = true;
            dataGridView4_acessorios.DataSource = dt2.Copy(); // Use Copy() para criar uma nova instância do DataTable

            // Configurar o DataGridView3
            dataGridView5_acessorio.AutoGenerateColumns = true;
            dataGridView5_acessorio.DataSource = dt3.Copy(); // Use Copy() para criar uma nova instância do DataTable
        }

        private void button_acessorios_cadastro_Click(object sender, EventArgs e)
        {
            // Definir o cursor como "Wait"
            this.Cursor = Cursors.WaitCursor;

            // Chamar a função que faz o processamento
            CompararLinhasSelecionadasAcessorios();

            // Restaurar o cursor padrão após a conclusão do processamento
            this.Cursor = Cursors.Default;
        }

        private void CompararLinhasSelecionadasAcessorios()
        {
            // Obter os IDs selecionados do DataGridView1
            List<int> idsDataGridView1 = ObterIdsSelecionados(dataGridView4_acessorios);

            // Obter os IDs selecionados do DataGridView2
            List<int> idsDataGridView2 = ObterIdsSelecionados(dataGridView5_acessorio);

            // Encontrar IDs diferentes entre as linhas selecionadas
            List<int> idsDiferentes = EncontrarIdsDiferentes(idsDataGridView1, idsDataGridView2);

            if (idsDiferentes.Any())
            {
                string mensagem = "IDs diferentes nas linhas selecionadas:\n" + string.Join(", ", idsDiferentes);
                MessageBox.Show(mensagem);
            }
            else
            {
                string connectionString = @"Data Source=" + Instancia + ";Initial Catalog=" + Banco + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Senha + "";
                foreach (int id in idsDataGridView1)
                {
                    var dataAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string query_status = $"update Notificacao set Verificado_por = '{Login}', Data_Verificado = '{dataAtual}' where ID = '{id}'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command1 = new SqlCommand(query_status, connection);
                        command1.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                ProcedimentoParaAcessorios();
            }
        }
    }


}

