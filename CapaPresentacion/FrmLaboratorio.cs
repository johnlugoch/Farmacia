using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CapaNegocio;
using Entidades;

namespace CapaPresentacion
{
    public partial class FrmLaboratorio : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;
        private readonly ProductoLog productoLog = new ProductoLog();
        private ELaboratorio elaboratorio;

        public FrmLaboratorio()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtLaboratorio.Focus();
        }
        private void Limpiar()
        {
            this.txtCodigo.Text = string.Empty;
            this.txtLaboratorio.Text = string.Empty;            
        }
        private void Habilitar(bool valor)
        {
            this.txtLaboratorio.ReadOnly = !valor;            
            this.txtCodigo.ReadOnly = !valor;
        }

        private void Botones()
        {
            if (this.IsNuevo || this.IsEditar) //Alt + 124
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }

        }

        private void Mostrar()
        {
            //this.OcultarColumnas();            
            this.dataLab.DataSource = ProductoLog.MostrarLab();
            lblTotal.Text = "Total de registros " + Convert.ToString(dataLab.Rows.Count);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtCodigo.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                this.MensajeError("Debe de seleccionar primero el registro a Modificar");
            }
        }

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Productos", MessageBoxButtons.OK);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //int codigo = 1;
            try
            {
                if (this.txtLaboratorio.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos");
                    //ErrorIcono.SetError(txtNombre, "Ingrese un Nombre de Laboratorio");
                }
                else
                {
                    if (this.IsNuevo)
                    {

                        elaboratorio = new ELaboratorio();
                        elaboratorio.codigo = txtCodigo.Text.ToUpper();
                        elaboratorio.nombre = txtLaboratorio.Text.ToUpper();                       
                        productoLog.registrarLab(elaboratorio);
                        MessageBox.Show("Laboratorio registrado/actualizado con éxito");
                    }
                    else
                    {
                        productoLog.editarLaboratorio(txtCodigo.Text, txtLaboratorio.Text.Trim().ToUpper());
                        MessageBox.Show("Producto actualizado con éxito");
                    }
                }
                this.IsNuevo = false;
                this.IsEditar = false;
                this.Botones();
                this.Limpiar();
                //this.Mostrar(centro);
                //this.ProximoVencer(centro);
                //this.tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void dataLab_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtCodigo.Text = Convert.ToString(this.dataLab.CurrentRow.Cells["id"].Value);
            this.txtLaboratorio.Text = Convert.ToString(this.dataLab.CurrentRow.Cells["laboratorio"].Value);
        }
    }
}
