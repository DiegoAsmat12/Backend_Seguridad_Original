using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Seguridad.Entities.Software
{
    public class Placa
    {
        public int Id { get; set; }
        [Required]
        public string NumeroDePlaca { get; set; }
        public Camara CamaraDetectora { get; set; }
        public string NombreCamara { get; set; }
        public string Direccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Codigo { get; set; }
        public string Hora { get; set; }
        public string Fecha { get; set; } 
        //public string ImagenCarroSource { get; set; }
        //public string ImagenPlacaSource { get; set; }
        public byte[] ImagenCarro { get; set; }
        public byte[] ImagenPlaca { get; set; }

        
    }
}
