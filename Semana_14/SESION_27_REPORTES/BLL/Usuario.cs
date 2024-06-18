﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Usuario
    {

        public string Email { get; set; }
        public string NombreCompleto { get; set; }

        public string Password { get; set; }

        public DateTime FechaRegistro { get; set; }

        public char Estado { get; set; }

        public bool ConfirmarPassword (string pw)
        {

            bool confirmado = false;
            if (! string.IsNullOrEmpty(pw)) { // AREGLAR ESTA LINEA con otro if

                if (Password.Equals(pw)) {
                    confirmado = true;
                }
                
            }

            return confirmado;
        }

    }//Fin clase
}//Fin namespace
