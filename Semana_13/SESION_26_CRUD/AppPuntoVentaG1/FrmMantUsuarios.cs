using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Referencias
//Capa de acceso a datos
using DAL;
//Capa de logica de negocio
using BLL;
using System.Configuration;

namespace AppPuntoVentaG1
{
    public partial class FrmMantUsuarios : Form
    {

        //Variable para manejar la referencia de la conexion
        private Conexion _conexion = null;

        //variable objeto para almacenar los datos del front end
        private Usuario _usuario = null;

        public FrmMantUsuarios()
        {
            InitializeComponent();

            //Se instancia la conexion y se envia como parametro del string de conexion 
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Se cierra el formulario
            this.Close();
        }

        private void btnAcciones_Click(object sender, EventArgs e)
        {
            try {
                //Se validad la funcion
                if (this.btnAcciones.Text.Equals("Modificar"))
                {
                    ModificarUsuario();
                    //Se muestra un mensaje al usuario todo salio correctamente
                    MessageBox.Show("Usuario modificado con exito", "Proceso aplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else {
                    //Se usa el metodo
                    GuardarUsuario();

                    //Se muestra un mensaje al usuario todo salio correctamente
                    MessageBox.Show("Usuario registrado con exito", "Proceso aplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                //Cerramos le formualario
                this.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarUsuario()
        {
            try
            {
                //Se instancia el usuario
                this._usuario = new Usuario();

                //Se rellenan los datos
                this._usuario.Email = this.txtEmail.Text.Trim();
                this._usuario.NombreCompleto = this.txtNombreCompleto.Text.Trim();
                this._usuario.Password = this.txtPassword.Text.Trim();

                //Se confirma el password
                if (this._usuario.ConfirmarPassword(this.txtConfirmar.Text.Trim())) {

                    //Se almacena el object enn la BD
                    _conexion.GuardarUsuario(this._usuario);

                }
                else { 

                    throw new Exception("La confirmacion del password no coincide");
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        
        
        }//end method


        //Metodo para controlar las funciones a realizar el formulario
        public void FuncionRealizar(int pFunc)
        {
            //Se valida que la funcio va a realizar el boton
            if (pFunc == 1) { 
            
                //se cambian el texto del boton
                this.btnAcciones.Text = "Modificar";
            }

        }//end method

        private void FrmMantUsuarios_Load(object sender, EventArgs e)
        {

        }

        public void CargarUsuarios(Usuario pTemp) {

            try { 
                
                //Se rellena el front end con los datos del usuario
                this.txtEmail.Text = pTemp.Email;
                this.txtNombreCompleto.Text = pTemp.NombreCompleto;
                this.txtPassword.Text = pTemp.Password;
                this.txtConfirmar.Text = pTemp.Password;

                //Se bloquea el editar el email
                this.txtEmail.ReadOnly = true;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        
        }//end method

        public void ModificarUsuario()
        {
            try { 
                
                //Se instancia el usuario
                this._usuario = new Usuario();

                //Se rellenan los datos
                this._usuario.Email = this.txtEmail.Text.Trim();
                this._usuario.NombreCompleto = this.txtNombreCompleto.Text.Trim();
                this._usuario.Password = this.txtPassword.Text.Trim();

                if (this._usuario.ConfirmarPassword(this.txtConfirmar.Text.Trim()))
                {

                    //Proceso de modificar
                    _conexion.ModificarUsuario(this._usuario);

                }
                else
                {
                    throw new Exception("La confirmacion del password no coincide");
                }
            
            }
            catch(Exception ex)
            {
                throw ex;
            }
        
        }//end method modifcar usuarios

    }//end class

}//end namespace
