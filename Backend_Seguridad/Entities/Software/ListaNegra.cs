using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Seguridad.Entities.Software
{
    public class ListaNegra
    {
        public int Id { get; set; }
        [Required]
        public string NumeroDePlaca { get; set; }
        public string Estado { get; set; }
        public Persona Persona { get; set; }
        public string DNI { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Direccion { get; set; }
        public string FechaDeNacimiento { get; set; }
        public string NumeroDeCelular { get; set; }
    }
}
