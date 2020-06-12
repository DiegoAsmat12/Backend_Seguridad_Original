using Backend_Seguridad.Entities.Software;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Seguridad.Models.Software
{
    public class PlacaCreacionDTO
    {
        [Required]
        public string NumeroDePlaca { get; set; }
        public string NombreCamara { get; set; }
        //public string ImagenCarroSource { get; set; }
        //public string ImagenPlacaSource { get; set; }
        public byte[] ImagenCarro { get; set; }
        public byte[] ImagenPlaca { get; set; }
    }
}
