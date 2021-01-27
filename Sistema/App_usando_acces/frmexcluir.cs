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
    public partial class frmexcluir : Form
    {
        //variaveis para receber a exclusão
        public string nome, endereco, cidade, estado, cep, telefone;

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //solicitação confirmação para excluir
            if (MessageBox.Show("Confirma exclusÆo? ", "Excluir", MessageBoxButtons.YesNo) ==
            DialogResult.Yes)
            {
                ExcluirDados();
            }

        }

        private void ExcluirDados()
        {
            //define string de conexão - Provedor + fonte de dados (caminho do banco de dados e seu
            //nome)
            string strConnection = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source =";
            //define instrução SQL para excluir dados da tabela Clientes - DELETE FROM tabela Where
            //< criterio >
            string strSQL = "DELETE FROM clientes WHERE codigoID=" + int.Parse(codigoID) + "";
            //cria a conexão com o banco de dados
            OleDbConnection dbConnection = new OleDbConnection(strConnection);
            //Cria o comando que inicia a instru‡Æo SQL para exclusÆo
            OleDbCommand cmdExcluir = new OleDbCommand(strSQL, dbConnection);
            try
            {
                // abre o banco de dados
                dbConnection.Open();
                // executa a instru‡Æo SQL

                cmdExcluir.ExecuteNonQuery();
                //
                MessageBox.Show("Dados Excluídos com sucesso.");
            }
            //Trata a exceção
            catch (OleDbException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                //fecha a conexao
                dbConnection.Close();
            }
        }

        public string codigoID;


        public frmexcluir()
        {
            InitializeComponent();
        }

        private void frmexcluir_Load(object sender, EventArgs e)
        {
            txtcodigo.Text = codigoID;
            txtNome.Text = nome;
            txtEndereco.Text = endereco;
            txtCidade.Text = cidade;
            txtestado.Text = estado;
            txtcep.Text = cep;
            txttelefone.Text = telefone;
        }
    }
}
