using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Registro2.Entidades
{
    public class Personas
    {
        [Key]
        public int PersonaId { get; set; }
        public string Nombres { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public decimal Balance { get; set; }

        public Personas()
        {
            PersonaId = 0;
            Nombres = string.Empty;
            Cedula = string.Empty;
            Direccion = string.Empty;
            Telefono = string.Empty;
            FechaNacimiento = DateTime.Now;
            Balance = 0;
        }

        public Personas(int personaId, string nombres, string cedula, string direccion, string telefono, DateTime fechaNacimiento, int balance)
        {
            PersonaId = personaId;
            Nombres = nombres;
            Cedula = cedula;
            Direccion = direccion;
            Telefono = telefono;
            FechaNacimiento = fechaNacimiento;
            Balance = balance;
        }
    }
}
