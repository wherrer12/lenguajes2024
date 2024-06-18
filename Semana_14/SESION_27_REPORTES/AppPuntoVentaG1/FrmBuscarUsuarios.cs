using BLL;
using DAL;
using System;
using System.CodeDom.Compiler;
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try {
                //El 0 representa el proceso de registro
                MostrarMantUsuarios(0);
                Buscar(""); // Se actualiza la lista despues de registrar un usuario

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarMantUsuarios(int funcion)
        {
            try
            {
                FrmMantUsuarios formulario = new FrmMantUsuarios();

                //Aqui se reutiliza el formulario para realizar la funcion de registrar o modificar
                formulario.FuncionRealizar(funcion);

                if (funcion == 1)
                {
                    if (this.dtgDatos.SelectedRows.Count > 0) {

                        //Se envia losa datos del usuario para rellenar el front end
                        formulario.CargarUsuarios(UsuarioSeleccionado());

                    }               
                    else
                    {
                        //Error indicando al usuario ue debe seleccionar la fila de datos
                        throw new Exception("Debe seleccionar un usuario para modificar");
                    }

                }

                formulario.ShowDialog();
                formulario.Dispose();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }//Fin del metodo MostrarMantUsuarios

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //El 1 representa el proceso de modificar los datos
                MostrarMantUsuarios(1);
                Buscar(""); // Se actualiza la lista despues de registrar un usuario

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//Fin del metodo

        private Usuario UsuarioSeleccionado()
        {
            try
            {
                Usuario temp = null;

                //Se verifica que el usuario tenga una fila selccionada
                if (this.dtgDatos.SelectedRows.Count > 0)
                {
                    //Se instancia el usuario
                    temp = new Usuario();

                    //Se rellena el objeto con los datos de la fila seleccionada
                    temp.Email = this.dtgDatos.SelectedRows[0].Cells["Email"].Value.ToString();
                    temp.NombreCompleto = this.dtgDatos.SelectedRows[0].Cells["NombreCompleto"].Value.ToString();
                    temp.Password = this.dtgDatos.SelectedRows[0].Cells["Password"].Value.ToString();
                }
                return temp;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }//Fin del metodo UsuarioSeleccionado

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                //Se llama al metodo 
                MostrarInforme();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//Fin del metodo btnImprimir

        //Metodo encargado de mostrar el informe
        public void MostrarInforme()
        {
            try
            {
                //Se declara e instancia una variable de tipo formulario
                FrmRepUsuario formulario = new FrmRepUsuario();

                //Se muestra el formulario
                formulario.ShowDialog();

                //Se liberan los procesos
                formulario.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }//Fin del metodo MostrarInformes

    }//Fin de la clase
}//Fin del namespace
