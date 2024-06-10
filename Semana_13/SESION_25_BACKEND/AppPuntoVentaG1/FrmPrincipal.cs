using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPuntoVentaG1
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Se meustra un mensaje de confirmación al usuario para salir del sistema
            if (MessageBox.Show("¿Esta seguro de cerrar el sistema?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Si confirma se cierra la aplicación
                Environment.Exit(0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(25);
            MostrarLogin();
        }

        private void MostrarLogin()
        {
            //Se declara e instancia una variable de tipo formulario FrmLogin
            FrmLogin formulario = new FrmLogin();

            //Se muestra el formulario para el proceso de autenticacion
            formulario.ShowDialog();

            //Se liberan los procesos
            formulario.Dispose();
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                MostrarBuscarUsuarios();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void MostrarBuscarUsuarios()
        {
            try
            {
                // Se declara e instancia una variable de tipo formulario FrmBuscarUsuarios
                FrmBuscarUsuarios frm = new FrmBuscarUsuarios();
                //Se muestra el formulario para el proceso de busqueda de usuarios
                frm.ShowDialog();
                //Se liberan los procesos
                frm.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//Cierremetodo MostrarBuscarUsuarios

    }//Cierre formulario
}//Cierre namespace
