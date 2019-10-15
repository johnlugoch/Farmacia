using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using Entidades;
using System.IO;

namespace CapaPresentacion
{
    public partial class FrmProducto : Form
    {
        private readonly ProductoLog productoLog = new ProductoLog();
        private EProducto eproducto;
        private bool IsNuevo = false;
        private bool IsEditar = false;

        public string IdEmpleado = "";
        public string Nombre = "";
        public string Apellidos = "";
        public string acceso = "";
        public string centro = "";

        public FrmProducto()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //int codigo = 1;
            try
            {
                if (this.txtNombre.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos");
                    ErrorIcono.SetError(txtNombre, "Ingrese un Nombre de Producto");
                }
                else
                {
                    if (this.IsNuevo)
                    {
                        
                        eproducto = new EProducto();
                        eproducto.Nombre = txtNombre.Text.ToUpper();
                        eproducto.concentracion = txtConcentracion.Text.ToUpper();
                        eproducto.presentacion = txtPresentacion.Text.ToUpper();
                        eproducto.fechav = dtpFecha.Value;
                        eproducto.centro = Int32.Parse(centro);
                        eproducto.lote = txtLote.Text.ToUpper();
                        eproducto.invima = txtInvima.Text.ToUpper();
                        eproducto.forma = txtForma.Text.ToUpper();
                        eproducto.idlab = Int32.Parse(comboBox1.SelectedValue.ToString());
                        eproducto.idcat= Int32.Parse(comboBox2.SelectedValue.ToString());
                        eproducto.estado = "1";
                        productoLog.registrar(eproducto);
                        MessageBox.Show("Producto registrado/actualizado con éxito");
                    }
                    else
                    {
                        productoLog.editar(Convert.ToInt32(txtcodigo.Text), txtNombre.Text.Trim().ToUpper(), 
                            txtConcentracion.Text.Trim().ToUpper(), txtPresentacion.Text.Trim().ToUpper(), 
                            Convert.ToDateTime(dtpFecha.Text), 
                            txtLote.Text.Trim().ToUpper(), txtInvima.Text.Trim().ToUpper(), txtForma.Text.Trim(), Convert.ToInt32(comboBox1.SelectedValue.ToString()), Convert.ToInt32(comboBox2.SelectedValue.ToString()));
                        MessageBox.Show("Producto registrado/actualizado con éxito");
                    }
                }
                this.IsNuevo = false;
                this.IsEditar = false;
                this.Botones();
                this.Limpiar();
                this.Mostrar(centro);
                this.ProximoVencer(centro);
                this.tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje,"Sistema de Productos", MessageBoxButtons.OK);
        }

        private void Mostrar(string centro)
        {
            //this.OcultarColumnas();            
            this.dataListado.DataSource = ProductoLog.Mostrar(centro);
            lblTotal.Text = "Total de registros " + Convert.ToString(dataListado.Rows.Count);
        }

        private void MostrarInactivos()
        {
            //this.OcultarColumnas();            
            this.dtInactivo.DataSource = ProductoLog.MostrarInactivos();
            lblTotal.Text = "Total de registros " + Convert.ToString(dtInactivo.Rows.Count);
        }

        private void ProximoVencer(string centro)
        {
            //this.OcultarColumnas();            
            this.dataVencer.DataSource = ProductoLog.ProximoVencer(centro);
            lblTotal.Text = "Total de registros " + Convert.ToString(dataVencer.Rows.Count);           
        }

        private int HayProximoVencer(string centro)
        {
            int resultado = 0;
            resultado= ProductoLog.HayProximoVencer(centro);
            //lblTotal.Text = "Total de registros " + Convert.ToString(dataVencer.Rows.Count);
            return resultado;
        }

        private void cargarLaboratorio()
        {
            comboBox1.DisplayMember = "laboratorio";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = ProductoLog.CargarLaboratorio();            
        }

        private void cargarCategoria()
        {
            comboBox2.DisplayMember = "nombre";
            comboBox2.ValueMember = "id";
            comboBox2.DataSource = ProductoLog.CargarCategoria();
        }

        private void OcultarColumnas()
        {
            /*this.dataListado.Columns[3].Visible = false;
            this.dataListado.Columns[5].Visible = false;
            this.dataListado.Columns[6].Visible = false;
            this.dataListado.Columns[7].Visible = false;
            this.dataListado.Columns[8].Visible = false;*/
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            int hay = 0;
            cargarLaboratorio();
            cargarCategoria();
            this.Habilitar(false);
            this.Botones();
            tabControl1.SelectedTab = tabPage1;
            hay = (HayProximoVencer(this.centro));
            if (hay > 0)
            {                
                MessageBox.Show("Hay productos que se venceran en menos de 90 días", "Alerta vencimiento de productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //ES LA APLICACION DE CONSULTA, SOLO SE VERA LOS REPORTES
            tabControl1.TabPages.Remove(this.tabPage1);



        }

        //Limpiar todos los controles del formulario
        private void Limpiar()
        {
            this.txtcodigo.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtPresentacion.Text = string.Empty;
            this.txtConcentracion.Text = string.Empty;
            this.txtInvima.Text = string.Empty;
            this.txtLote.Text = string.Empty;
            this.txtForma.Text = string.Empty;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtNombre.Focus();
        }
        
        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtPresentacion.ReadOnly = !valor;
            this.txtConcentracion.ReadOnly = !valor;
            this.txtInvima.ReadOnly = !valor;
            this.txtLote.ReadOnly = !valor;
            this.txtForma.ReadOnly = !valor;
            this.txtcodigo.ReadOnly = !valor;
        }

        //Habilitar los botones
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

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtcodigo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["cod_producto"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            this.txtConcentracion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["concentracion"].Value);            
            this.txtPresentacion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["presentacion"].Value);
            this.txtLote.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["lote"].Value);
            this.txtInvima.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["reg_invima"].Value);
            this.txtForma.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["forma"].Value);
            this.dtpFecha.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["fechavenc"].Value);
            this.comboBox2.Text = Convert.ToString(this.dataListado.CurrentRow.Cells[9].Value);
            this.comboBox1.Text = Convert.ToString(this.dataListado.CurrentRow.Cells[5].Value);
            this.tabControl1.SelectedIndex = 0;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtcodigo.Text.Equals(""))
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportarDataGridViewExcel(dataListado);
        }

        private void ExportarDataGridViewExcel(DataGridView grd)
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xls)|*.xls";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo =
                    (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
                //Recorremos el DataGridView rellenando la hoja de trabajo
                for (int i = 0; i < grd.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < grd.Columns.Count; j++)
                    {
                        hoja_trabajo.Cells[i + 1, j + 1] = grd.Rows[i].Cells[j].Value.ToString();
                    }
                }
                libros_trabajo.SaveAs(fichero.FileName,
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                libros_trabajo.Close(true);
                aplicacion.Quit();
            }
        }

        public static void ExportarExcelDataTable(DataTable dt, string RutaExcel)
        {
            const string FIELDSEPARATOR = "\t";
            const string ROWSEPARATOR = "\n";
            StringBuilder output = new StringBuilder();
            // Escribir encabezados    
            foreach (DataColumn dc in dt.Columns)
            {
                output.Append(dc.ColumnName);
                output.Append(FIELDSEPARATOR);
            }
            output.Append(ROWSEPARATOR);
            foreach (DataRow item in dt.Rows)
            {
                foreach (object value in item.ItemArray)
                {
                    output.Append(value.ToString().Replace('\n', ' ').Replace('\r', ' ').Replace('.', ','));
                    output.Append(FIELDSEPARATOR);
                }
                // Escribir una línea de registro        
                output.Append(ROWSEPARATOR);
            }
            // Valor de retorno    
            // output.ToString();
            StreamWriter sw = new StreamWriter(RutaExcel);
            sw.Write(output.ToString());
            sw.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ExportarDataGridViewExcel(dataVencer);
        }
                

        private void button3_Click(object sender, EventArgs e)
        {
            Mostrar(this.centro.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProximoVencer(this.centro.ToString());
        }

        private void nuevoLaboratorioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLaboratorio frmLab = new FrmLaboratorio();
            frmLab.Show();
        }

        private void inactivarVencidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cant;
            if (MessageBox.Show("Desea Inactivar los productos vencidos?", "Medicamento e Insumos",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Application.Exit();
                productoLog.inactivarProducto();
                cant = productoLog.contarInactivar();
                MessageBox.Show("Se ha Inactivado " + cant + " Productos");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MostrarInactivos();
        }
    }
}
