using Backend_Seguridad.Entities.Software;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Seguridad.Models.Software
{
    public class PlacaDTO
    {
        [Required]
        public string NumeroDePlaca { get; set; }
        public int Id { get; set; }
        public string NombreCamara { get; set; }
        public string Codigo { get; set; }
        public string Direccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Hora { get; set; }
        public string Fecha { get; set; }
        public byte[] ImagenCarro { get; set; }
        public byte[] ImagenPlaca { get; set; }
    }
}
