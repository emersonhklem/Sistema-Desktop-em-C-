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
    public partial class frmalterar : Form
    {
        //definição das variaveis para a alteração dos dados
public string nome, endereco, cidade, estado, cep, telefone;
public string codigoID;

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (validaDados())
                AlterarDados();
            else
                MessageBox.Show("Dados Inválidos...");
            txtNome.Focus();
            return;

        }

        //rotina para verificar se dados são válidos
        private Boolean validaDados()
        {
            if (txtNome.Text == string.Empty) return false;
            if (txtEndereco.Text == string.Empty) return false;
            if (txtCidade.Text == string.Empty) return false;
            if (txtestado.Text == string.Empty) return false;
            if (txtcep.Text == string.Empty) return false;
            return true;
        }

        //rotina para alterar os dados
        private void AlterarDados()
        {

            //define string de conexão - Provedor + fonte de dados (caminho do banco de dados e seu
            //nome)
string strConnection = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source =";
            //define a instrução SQL para atualizar os dados da tabela Clientes - UPDATE tabela SET
            //campos
            string strSQL = "UPDATE clientes SET nome ='" + txtNome.Text
                + "',endereco='" + txtEndereco.Text + "', cidade='" + txtCidade.Text + "'," +
                " estado='" + txtestado.Text + "',cep='" + txtcep.Text + "', telefone='" + 
                txttelefone.Text + "' WHERE codigoID= " + int.Parse(codigoID) + "";

            //  nome,endereco,cidade,estado,cep,telefone


            //cria a conexão com o banco de dados
            OleDbConnection dbConnection = new OleDbConnection(strConnection);
            //Cria o comando que inicia a instrução SQL para alteração
            OleDbCommand cmdAlterar = new OleDbCommand(strSQL, dbConnection);
            try
            {
                // abre o banco de dados
                dbConnection.Open();
                // executa a instru‡Æo SQL
                cmdAlterar.ExecuteNonQuery();
                MessageBox.Show("Dados Alterados com sucesso.");
            }
            //Trata a exce‡Æo
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

        

        public frmalterar()
        {
            InitializeComponent();
        }

        private void frmalterar_Load(object sender, EventArgs e)
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
