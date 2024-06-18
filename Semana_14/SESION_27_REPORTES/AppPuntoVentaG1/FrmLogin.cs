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

//Importacion BLL
using BLL;
using DAL;

//Importacion del DAL

namespace AppPuntoVentaG1
{
    public partial class FrmLogin : Form
    {
        //Variable objeto de tipo Usuario
        private Usuario objUsuario = null;

        //Variable necesaria para manejar la referencia de la conexion al servidor
        private Conexion _conexion = null;

        /// <summary>
        /// Constructor por omision del formulario
        /// </summary>

        public FrmLogin()
        {
            InitializeComponent();

            //Se instancia la conexion, se envia como parametro el string de conexion almacenado dentro del archivo config
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);//Finaliza la aplicación
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try { 
            
                //Se llama al metodo de la autenticacion
                IntentoAutenticacion();

            }
            catch(Exception ex)
            {
                MessageBox.Show(/*"Error al intentar autenticar: " +*/ ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IntentoAutenticacion() {
            try
            {

                //Se crea una instancia del objeto usuario
                objUsuario = new Usuario();

                //Se rellenan el objeto cpn los datos escritos a nivel front end
                objUsuario.Email = this.txtUsuario.Text.Trim();
                objUsuario.Password = this.txtPassword.Text.Trim();

                //Se realiza la verificacion de credenciales
                //if (objUsuario.Email.Equals("admin@gmail.com") && objUsuario.Password.Equals("admin"))
                if (_conexion.ValidarUsuario(objUsuario.Email, objUsuario.Password)!=null)
                {

                    this.Close(); // Se cierra el formulario, las credenciales son correctas
                }
                else { 
                    //Cuando las credenciales fallan
                    LimpiarPantalla();
                    //Se genera una excepcion al usuario
                    throw new Exception("Usuario o contraseña incorrectos");
                }

            }
            catch(Exception ex)
            {
                throw ex;
                //MessageBox.Show("Error al intentar autenticar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }//Fin metodo intentoAutenticacion

        //Metodo encargado de limpiar la pantalla
        private void LimpiarPantalla()
        {
            //txtUsuario.Text = "";
            //txtPassword.Text = "";
            this.txtUsuario.Clear();
            this.txtPassword.Clear();
        }

    }//Fin clase
}//Fin namespace
