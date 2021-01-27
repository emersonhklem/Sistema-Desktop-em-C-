using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace App_usando_acces
{
    public partial class Form1 : Form
    {
        private int linhaAtual = 0;
        //variáveis para os campos da tabela CLientes
        private string codigoID, nome, endereco, cidade, estado, cep, telefone;


        //Representa uma conexão aberta com uma fonte de dados. criar o objeto connection
        private OleDbConnection Conn;
        //Representa um conjunto de comandos de dados e uma conexão de banco de dados
        //que são usados para preencher o DataSet e atualizar a fonte de dados. criar o data adapter e
        //executar a consulta
private OleDbDataAdapter da;
        //Representa dados em um cache de memória.
        /*
        * O ADO.NET DataSet é uma representação de dados residentes na memória que
        * fornecem um modelo de programação relacional, consistente e independente da
        * fonte de dados.O DataSet representa um conjunto completo de dados,
        * que inclui tabelas, restrições e relações entre as tabelas.Como DataSet é
        * independente da fonte de dados, um DataSet pode incluir o local de dados
        * para o aplicativo, e os dados de várias fontes de dados.A interação
        * com fontes de dados existente é controlada com o DataAdapter.
        */
        private DataSet ds;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //para atualizar a linha
            linhaAtual = int.Parse(e.RowIndex.ToString());

        }

        //para access 2007 = Microsoft.ACE.OLEDB.12.0
        //para access 2003 = Microsoft.ACE.OLEDB.4.0
        private void iniciaAcesso()
        {
            ds = new DataSet();
            //mudar o caminho na hora da apresentação
            Conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
            textBox1.Text + ".accdb"); //Supondo que ó banco esteja dentro da pasta \bin\Debug

            // ou dentro do diretorio que você escolheu para seu banco

            //Conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|Caminho
            //completo | " + textBox1.Text + ".accdb");
        try
            {
                Conn.Open();//testa a conexão

            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }

            //
            if (Conn.State == ConnectionState.Open)
            {
                da = new OleDbDataAdapter("SELECT * from " + textBox2.Text, Conn);
                //O método Fill do DataAdapter é usado para preencher um DataSet
                //com os resultados do SelectCommand do DataAdapter. Fill utiliza como
                //seus argumentos um DataSet a ser preenchido, e um objeto DataTable,
                //ou nome do DataTable a ser preenchido com as linhas retornadas do
                //SelectCommand.
                da.Fill(ds, "Categories");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Categories";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            frmprocurar f5 = new frmprocurar();
            f5.ShowDialog();
            if (f5.sqlString != null && f5.sqlString != "")
                carregaGrid(f5.sqlString);
        }
        private void carregaGrid(string criterioSQL)
        {
            //define o dataset
            ds = new DataSet();
            //cria uma conexão usando a string de conexão
            Conn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = dbp1.accdb");
            try
            {
                //abre a conexao
                Conn.Open();
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            if (Conn.State == ConnectionState.Open)
            {
                //se a conexão estiver aberta usa uma instrução SQL para selecionar os registros da
                //tabela clientes
            //SELECT campos FROM tabela
da = new OleDbDataAdapter(criterioSQL, Conn);
                da.Fill(ds, "Tabela");
                //exibe os dados no datagridview
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Tabela";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iniciaAcesso();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmIncluir f2 = new frmIncluir();
            f2.Show();
            iniciaAcesso();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //obtem o código do cliente a partir da linha selecionada no datagridview
                codigoID = dataGridView1[0, linhaAtual].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro..." + ex.Message);
            }
            if (linhaAtual >= 0)
            {
                //obtem dados do datagridview e atribui as variáveis definidas no formulario frmexcluir
                obtemDadosGrid();
                frmexcluir f4 = new frmexcluir();
                f4.codigoID = codigoID;
                f4.nome = nome;
                f4.endereco = endereco;
                f4.cidade = cidade;
                f4.estado = estado;
                f4.cep = cep;
                f4.telefone = telefone;
                //exibe o formulário para exclusão
                f4.ShowDialog();
                //atualiza o grid e reexibe os dados
                dataGridView1.Update();
                iniciaAcesso();
            }
        }

        private void btnEncerrar_Click(object sender, EventArgs e)
        {

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

            try
            {
                //obtem o código do cliente a partir da linha selecionada no datagridview
                codigoID = dataGridView1[0, linhaAtual].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro..." + ex.Message);
            }
            if (linhaAtual >= 0)
            {
                //obtem dados do datagridview e atribui as variáveis definidas no formulario f3
                obtemDadosGrid();
                frmalterar f3 = new frmalterar();
                //
                f3.codigoID = codigoID;
                f3.nome = nome;
                f3.endereco = endereco;
                f3.cidade = cidade;
                f3.estado = estado;
                f3.cep = cep;
                f3.telefone = telefone;
                //exibe o formulário para alteração
                f3.ShowDialog();
                //atualiza o grid e reexibe os dados
                dataGridView1.Update();
                iniciaAcesso();
            }
        }

        private void obtemDadosGrid()
        {
            //obtém os dados do datagridview da linha selecionada usando as posi‡äes das colunas
            //a primeira coluna ‚ a coluna 0 a segunda ‚ a coluna 1 , e , assim por diante
            nome = dataGridView1[1, linhaAtual].Value.ToString();
            endereco = dataGridView1[2, linhaAtual].Value.ToString();
            cidade = dataGridView1[3, linhaAtual].Value.ToString();
            estado = dataGridView1[4, linhaAtual].Value.ToString();
            cep = dataGridView1[5, linhaAtual].Value.ToString();
            telefone = dataGridView1[6, linhaAtual].Value.ToString();
        }
    }
}
