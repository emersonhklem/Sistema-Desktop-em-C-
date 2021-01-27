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

 


    public partial class frmIncluir : Form
    {
        public frmIncluir()
        {
            InitializeComponent();

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (validaDados())
              {
                SalvaDados();         
            }
            else
            {
                MessageBox.Show("dados invalidos...");
                txtNome.Focus();
                return;
            }
        }
        //rotina para validar dados
        private Boolean validaDados()
        {
            if (txtNome.Text == string.Empty)
            {
                return false;
            }

            if (txtEndereco.Text == string.Empty)
            {
                return false;
            }

            if (txtCidade.Text == string.Empty)
            {
                return false;
            }

            if (txtestado.Text == string.Empty)
            {
                return false;
            }

            if (txtcep.Text == string.Empty)
            {
                return false;
            }

            return true;
        }


        //rotina para salvar dados
        private void SalvaDados()
        {
            //string de conexao
            string strConnection = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source =";


            //cria a conexão com o banco de dados
            OleDbConnection dbConnection = new OleDbConnection(strConnection);
            //cria um comando
            OleDbCommand cmdQry = dbConnection.CreateCommand();
            //Define a instrução SQL com parâmetros
            cmdQry.CommandText = "INSERT INTO clientes(nome,endereco,cidade,estado,cep,telefone)"
            + " VALUES (@nome, @endereco, @cidade, @estado, @cep, @Telefone)";
            //instrução antiga
            //string strSQL = "INSERT INTO Clientes(nome,endereco,cidade,estado,cep,telefone)"
            // + " VALUES ('" + txtnome.Text + "','" + txtendereco.Text + "','" +
            //txtcidade.Text + "','" + txtestado.Text + "','" + txtcep.Text + "','" + txttelefone.Text + "')";
            // definindo os parâmetro: nome , tipo de dados e tamanho
            cmdQry.Parameters.Add("@nome", OleDbType.VarChar, 50);
            cmdQry.Parameters.Add("@endereco", OleDbType.VarChar, 50);
            cmdQry.Parameters.Add("@cidade", OleDbType.VarChar, 50);
            cmdQry.Parameters.Add("@estado", OleDbType.VarChar, 50);
            cmdQry.Parameters.Add("@cep", OleDbType.VarChar, 50);
            cmdQry.Parameters.Add("@telefone", OleDbType.VarChar, 50);
            //atribuindo valores aos parâmetros
            cmdQry.Parameters["@nome"].Value = txtNome.Text;
            cmdQry.Parameters["@endereco"].Value = txtEndereco.Text;
            cmdQry.Parameters["@cidade"].Value = txtCidade.Text;
            cmdQry.Parameters["@estado"].Value = txtestado.Text;
            cmdQry.Parameters["@cep"].Value = txtcep.Text;
            cmdQry.Parameters["@telefone"].Value = txttelefone.Text;
            try
            {
                // abre o banco
                dbConnection.Open();
                // executa a query
                cmdQry.ExecuteNonQuery();
                MessageBox.Show("Dados Salvos com sucesso.");
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


        private void frmIncluir_Load(object sender, EventArgs e)
        {

        }
    }
}
