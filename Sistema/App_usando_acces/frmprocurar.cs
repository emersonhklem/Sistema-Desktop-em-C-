using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_usando_acces
{
    public partial class frmprocurar : Form
    {
        private string criterio = "";
        public string sqlString = "";

        public frmprocurar()
        {
            InitializeComponent();
        }

        private void frmprocurar_Load(object sender, EventArgs e)
        {

        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            criterio = txtNome.Text.ToString();
            if (criterio != "")
            {
                sqlString = "SELECT * FROM clientes Where nome LIKE '" + criterio + "%'";
                this.Close();
            }
            else
            {

                MessageBox.Show("Informe o nome a procurar", "Aviso", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            }
        }
    }
}
