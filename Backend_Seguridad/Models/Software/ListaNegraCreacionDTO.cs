using Backend_Seguridad.Entities.Software;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Seguridad.Models.Software
{
    public class ListaNegraCreacionDTO
    {
        [Required]
        public string NumeroDePlaca { get; set; }
        public string DNI { get; set; }
        public string Estado { get; set; }
    }
}
