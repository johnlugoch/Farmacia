using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            lblHora.Text = DateTime.Now.ToString();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            DataTable datos = CapaNegocio.NEmpleado.Login(this.txtUsuario.Text, this.txtPassword.Text);
            if (datos.Rows.Count == 0)
            {
                MessageBox.Show("No Tiene Acceso al Sistema","Sistema de Medicamentos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FrmProducto frm = new FrmProducto();
                frm.IdEmpleado = datos.Rows[0][0].ToString();
                frm.Apellidos = datos.Rows[0][3].ToString();
                frm.Nombre = datos.Rows[0][2].ToString();                
                frm.centro = datos.Rows[0][5].ToString();
                frm.Show();                
                this.Hide();
            }
        }
    }
}
