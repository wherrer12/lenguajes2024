using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPuntoVentaG1
{
    public partial class FrmBuscarUsuarios : Form
    {

        // Variable para manejar la referencia de la conexion
        private Conexion _conexion = null;
        public FrmBuscarUsuarios()
        {
            InitializeComponent();
            //Se instancia la conexion y se envia como parametro del string de conexion almacenado en el archivo AppConfig
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            try {
                //Cada vez que se escriba se ejecuta el metodo buscar
                Buscar(this.txtNombre.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Buscar(string pNombre)
        {
            try {
                //Se utiliza la clase conexion, usando el metodo de buscar
                this.dtgDatos.DataSource = _conexion.BuscarUsuarios(pNombre).Tables[0];
                this.dtgDatos.AutoResizeColumns();//Se realiza el ajuste automatico del ancho de las columnas
                this.dtgDatos.ReadOnly = true;//Se marca los datos como solo lectura

            }
            catch (Exception ex) {

                throw ex;
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Se le pregunta al usuario si esta seguro de eliminar
                if (MessageBox.Show("Desea eliminar el usuario seleccionado?","Confirmar",
                    MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
                {

                    //Se valida que el usuario tenga seleccionada una fila en la lista
                    if (this.dtgDatos.SelectedRows.Count > 0)
                    {

                        //Se toma el email de la fila seleccionada por el usuario
                        EliminarUsuario(this.dtgDatos.SelectedRows[0].Cells["Email"].Value.ToString());
                    }
                    else { 
                        
                        //Se genera un error personalizado indicando que debe seleccionar una fila
                        throw new Exception("Debe seleccionar la fila del usuario que desea eliminar");

                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }//Fin del metodo

        //Metodo para eliminar un usuario
        private void EliminarUsuario(string email)
        {
            try
            {
                //Se utiliza la clase conexion para eliminar los datos del usuario en la BD
                _conexion.EliminarUsuario(email);
                //Se actualiza la vista
                Buscar("");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }//Fin del metodo

    }//Fin de la clase
}//Fin del namespace
