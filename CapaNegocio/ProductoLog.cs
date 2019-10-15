using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class ProductoLog
    {
        private DProducto _dproducto = new CapaDatos.DProducto();
        private DLaboratorio _laboratorio = new CapaDatos.DLaboratorio();
        public readonly StringBuilder stringBuilder = new StringBuilder();

        public void registrar(EProducto producto)
        {
            if (validarProducto(producto))
            {
                _dproducto.insertar(producto);                
            }
        }
        public void registrarLab(ELaboratorio lab)
        {
            if (validarLaboratorio(lab))
            {
                _laboratorio.insertar(lab);
            }
        }

        //Método Editar que llama al método Editar de la clase DCategoría
        //de la CapaDatos
        public void editar(int id, string nombre, string concentracion, string des, DateTime fechav, string lote, string invima, string forma, int idlabo, int idcat)
        {
            DProducto Obj = new DProducto();
            Obj.Idproducto = id;
            Obj.Nombre = nombre;
            Obj.Concetracion = concentracion;
            Obj.Presentacion = des;
            Obj.Fechav = fechav;
            Obj.Lote = lote;
            Obj.Invima = invima;
            Obj.Forma = forma;
            Obj.Idlab = idlabo;
            Obj.Idcat = idcat;
            Obj.editar(Obj);
        }

        public void editarLaboratorio(string id, string nombre)
        {
            DLaboratorio Obj = new DLaboratorio();
            Obj.Id= id;
            Obj.Nombre = nombre;            
            Obj.editar(Obj);
        }

        public void inactivarProducto()
        {
            int cant;
            DProducto obj = new DProducto();
            obj.InactivarProducto();            
        }

        public int contarInactivar()
        {
            int cant;
            DProducto obj = new DProducto();
            cant = obj.ContarInactivar();
            return cant;             
        }

        public void eliminar(int idProducto)
        {
            _dproducto.eliminar(idProducto);
        }

        public static DataTable Mostrar(string centro)
        {
            return new DProducto().Mostrar(centro);
        }

        public static DataTable MostrarInactivos()
        {
            return new DProducto().MostrarInactivos();
        }

        public static DataTable MostrarLab()
        {
            return new DLaboratorio().MostrarLab();
        }

        public static DataTable ProximoVencer(string centro)
        {
            return new DProducto().ProximoVencer(centro);
        }

        public static int HayProximoVencer(string centro)
        {
            return new DProducto().HayProximoVencer(centro);
        }

        public static DataTable CargarLaboratorio()
        {
            return new DProducto().CargarLaboratorio();
        }

        public static DataTable CargarCategoria()
        {
            return new DProducto().CargarCategoria();
        }

        private bool validarProducto(EProducto producto)
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(producto.presentacion)) stringBuilder.Append("El campo Descripción es obligatorio");
            if (string.IsNullOrEmpty(producto.Nombre)) stringBuilder.Append(Environment.NewLine + "El campo Nombre es obligatorio");
            if (string.IsNullOrEmpty(producto.concentracion)) stringBuilder.Append(Environment.NewLine + "El campo Concentración es obligatorio");

            //if (producto.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");

            return stringBuilder.Length == 0;
        }

        private bool validarLaboratorio(ELaboratorio lab)
        {
            stringBuilder.Clear();

           
            if (string.IsNullOrEmpty(lab.nombre)) stringBuilder.Append(Environment.NewLine + "El campo Nombre es obligatorio");
           

            //if (producto.Precio <= 0) stringBuilder.Append(Environment.NewLine + "El campo Precio es obligatorio");

            return stringBuilder.Length == 0;
        }
    }
}
