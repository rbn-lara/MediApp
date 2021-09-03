using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediApp.Models
{
    public class Patient
    {
        [PrimaryKey]
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int NumeroTelefono { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public Patient()
        {

        }
        public Patient( Patient aClonarSeguro)
        {
            Usuario = aClonarSeguro.Usuario;
            Nombre = aClonarSeguro.Nombre;
            PrimerApellido = aClonarSeguro.PrimerApellido;
            SegundoApellido = aClonarSeguro.SegundoApellido;
            Email = aClonarSeguro.Email;
            NumeroTelefono = aClonarSeguro.NumeroTelefono;
            FechaNacimiento = aClonarSeguro.FechaNacimiento;
            Password = "";
        }

    }
}
