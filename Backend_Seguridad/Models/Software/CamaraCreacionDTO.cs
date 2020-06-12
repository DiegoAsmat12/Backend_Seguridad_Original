using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Seguridad.Models.Software
{
    public class CamaraCreacionDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public double Latitud { get; set; }
        [Required] 
        public double Longitud { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public string Estado { get; set; }
    }
}
