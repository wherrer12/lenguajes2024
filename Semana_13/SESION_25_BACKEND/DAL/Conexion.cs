using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Driver para interactuar con un servidor SQL
using System.Data.SqlClient;
using BLL;
using System.Data;

namespace DAL
{
    public class Conexion
    {   //Variable Conexion
        private SqlConnection _connection;

        //
        private SqlCommand _command;

        //variable para leer datos desde la base de datos
        private SqlDataReader _reader;

        //Variable para almacenar los datos del servidor donde nos vamos a conectar
        private string StringConexion;

        /// <summary>
        /// Constructor con parametros
        //</summary>

        public Conexion(string pStrConexion)
        {
            //Se asignan los datos del servidor a conectar
            StringConexion = pStrConexion; 
        }

        public Usuario ValidarUsuario(string pEmail, string pPw)
        {
            try {
                //Variable para almacenar los datos de la BD
                Usuario usuario = null;

                //Se instancia la conexion
                _connection = new SqlConnection(StringConexion);

                //Se intenta abrir la conexion
                _connection.Open();

                //Se instancia un comando para utilizar el procedimento almacenado
                _command = new SqlCommand();

                //Se debe asignar siempre la conexion
                _command.Connection = _connection;

                //Se indica el tipo de comando que se va a ejecutar el comando
                _command.CommandType = System.Data.CommandType.StoredProcedure;

                //Se indica el nombre del procedimiento almacenado
                _command.CommandText = "[Sp_Cns_Usuario]";

                //Se debe asignar los valores para los parametros del procedimiento almacenado
                _command.Parameters.AddWithValue("pEmail", pEmail);
                _command.Parameters.AddWithValue("pPassword", pPw);

                //Se ejecuta el procedimiento almacenado
                _reader = _command.ExecuteReader();

                //Se valida si hay datos
                if (_reader.Read()) { 
                    
                    //Se istancia el objeto usuario
                    usuario = new Usuario();

                    //Se rellena el objeto con la info de la BD
                    usuario.Email = _reader.GetValue(0).ToString();
                    usuario.NombreCompleto = _reader.GetValue(1).ToString();
                    usuario.Password = _reader.GetValue(2).ToString();
                    usuario.FechaRegistro = DateTime.Parse(_reader.GetValue(3).ToString());
                    usuario.Estado = char.Parse(_reader.GetValue(4).ToString());

                }

                //Siempre se cierra la conexion
                _connection.Close();

                //Se deben liberar los recursos
                _connection.Dispose();
                _command.Dispose();
                _reader = null;

                //Se retorna el object
                return usuario;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }//fin metodo validar usuario

        public DataSet BuscarUsuarios(string pNombre) {

            try { 
                DataSet datos = new DataSet(); //Contenedor para mostrar los datos tabulados
                SqlDataAdapter adapter = new SqlDataAdapter(); //Adaptador encargado de acoplar los datos al dataset

                _connection = new SqlConnection(StringConexion); // Se instancia la conexion
                _connection.Open(); //Se abre la conexion

                _command = new SqlCommand(); //Se instancia el comando encargado de ejecutar el procedimiento almacenado
                _command.Connection = _connection; //Se asigna la conexion al procedimiento almacenado
                _command.CommandType = CommandType.StoredProcedure; //Se indica el tipo de comando
                _command.CommandText = "[Sp_Buscar_Usuarios]"; //Se indica el nombre del procedimiento almacenado a ejecutar
                _command.Parameters.AddWithValue("@Nombre", pNombre); //Muy importante darle el valor al parametro

                adapter.SelectCommand = _command; //Se asigna el comando al adaptador de datos
                adapter.Fill(datos); //Se llena el dataset con los datos del que devuelve el comando

                _connection.Close(); //siempre se debe cerrar la conexion
                _connection.Dispose(); // Se liberan los recursos
                _command.Dispose();
                adapter.Dispose(); 

                //Se retorna los datos llenos de informacion
                return datos;
            
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }//fin metodo buscar usuarios

        //Metodo para eliminar un usuario
        public void EliminarUsuario(string pEmail)
        {
            try
            {
                _connection = new SqlConnection(StringConexion); // Se instancia la conexion
                _connection.Open(); //Se abre la conexion

                _command = new SqlCommand(); //Se instancia el comando encargado de ejecutar el procedimiento almacenado
                _command.Connection = _connection; //Se asigna la conexion al procedimiento almacenado
                _command.CommandType = CommandType.StoredProcedure; //Se indica el tipo de comando
                _command.CommandText = "[Sp_Del_Usuarios]"; //Se indica el nombre del procedimiento almacenado a ejecutar
                _command.Parameters.AddWithValue("@Email", pEmail); //Muy importante darle el valor al parametro

                _command.ExecuteNonQuery(); //Se ejecuta el procedimiento almacenado

                _connection.Close(); //siempre se debe cerrar la conexion
                _connection.Dispose(); // Se liberan los recursos
                _command.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//fin metodo eliminar usuario

        //Metodo modificar usuario
        public void ModificarUsuario(Usuario usuario) {

            try
            {
                _connection = new SqlConnection(StringConexion); // Se instancia la conexion
                _connection.Open(); //Se abre la conexion
                _command = new SqlCommand(); //Se instancia el comando encargado de ejecutar el procedimiento almacenado
                _command.Connection = _connection; //Se asigna la conexion al procedimiento almacenado
                _command.CommandType = CommandType.StoredProcedure; //Se indica el tipo de comando
                _command.CommandText = "[Sp_Upd_Usuarios]"; //Se indica el nombre del procedimiento almacenado a ejecutar
                _command.Parameters.AddWithValue("@Email", usuario.Email); //Se asigna le valor al email
                _command.Parameters.AddWithValue("@NombreComp", usuario.NombreCompleto); //Se asigna el valor al nombre completo
                _command.Parameters.AddWithValue("@Passwd", usuario.Password); //Se asigna el valor al password

                _command.ExecuteNonQuery(); //Se ejecuta el procedimiento almacenado
                _connection.Close(); //Se cierra la conexion
                _connection.Dispose(); // Se liberan los recursos
                _command.Dispose(); 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }//fin metodo modificar usuario

    }//fin clase
}//fin namespace
