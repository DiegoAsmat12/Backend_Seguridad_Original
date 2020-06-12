using Backend_Seguridad.Contexts.Software;
using Backend_Seguridad.Entities.Software;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Backend_Seguridad.Controllers.Software
{
    [ApiController]
    [Route("software/api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlacasListaNegraController : ControllerBase
    {
        private readonly ApplicationSoftwareDbContext context;

        public PlacasListaNegraController(ApplicationSoftwareDbContext context)
        {
            this.context = context;
        }
        
        [HttpGet(Name = "ObtenerPlacasListaNegra")]
        public async Task<ActionResult<IEnumerable<PlacaListaNegra>>> Get(string buscar, string orden = "ID", string tipo_orden = "DESC", int pagina = 1, int registros_por_pagina = 10)
        {
            var placas = await context.PlacasListaNegra.ToListAsync();
            if (!string.IsNullOrEmpty(buscar))
            {
                foreach (var item in buscar.Split(new char[] { ' ' },
                         StringSplitOptions.RemoveEmptyEntries))
                {
                    placas = placas.Where(x => x.NombreCamara.Contains(item) ||
                                                  x.NumeroDePlaca.Contains(item) ||
                                                  x.Hora.Contains(item) ||
                                                  x.Fecha.Contains(item))
                                                  .ToList();
                }
            }
            switch (orden)
            {
                case "ID":
                    if (tipo_orden.ToLower() == "desc")
                        placas = placas.OrderByDescending(x => x.Id).ToList();
                    else if (tipo_orden.ToLower() == "asc")
                        placas = placas.OrderBy(x => x.Id).ToList();
                    break;

                case "CAMARA":
                    if (tipo_orden.ToLower() == "desc")
                        placas = placas.OrderByDescending(x => x.NombreCamara).ToList();
                    else if (tipo_orden.ToLower() == "asc")
                        placas = placas.OrderBy(x => x.NombreCamara).ToList();
                    break;

                case "PLACA":
                    if (tipo_orden.ToLower() == "desc")
                        placas = placas.OrderByDescending(x => x.NumeroDePlaca).ToList();
                    else if (tipo_orden.ToLower() == "asc")
                        placas = placas.OrderBy(x => x.NumeroDePlaca).ToList();
                    break;
                case "HORA":
                    if (tipo_orden.ToLower() == "desc")
                        placas = placas.OrderByDescending(x => x.Hora).ToList();
                    else if (tipo_orden.ToLower() == "asc")
                        placas = placas.OrderBy(x => x.Hora).ToList();
                    break;
                case "FECHA":
                    if (tipo_orden.ToLower() == "desc")
                        placas = placas.OrderByDescending(x => x.Fecha).ToList();
                    else if (tipo_orden.ToLower() == "asc")
                        placas = placas.OrderBy(x => x.Fecha).ToList();
                    break;


                default:
                    if (tipo_orden.ToLower() == "desc")
                        placas = placas.OrderByDescending(x => x.Id).ToList();
                    else if (tipo_orden.ToLower() == "asc")
                        placas = placas.OrderBy(x => x.Id).ToList();
                    break;
            }
            int _TotalRegistros = 0;
            int _TotalPaginas = 0;

            _TotalRegistros = placas.Count();

            placas = placas.Skip((pagina - 1) * registros_por_pagina)
                                             .Take(registros_por_pagina)
                                             .ToList();

            _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / registros_por_pagina);
            return placas;
        }
        [HttpGet("{NumeroDePlaca}",Name="ObtenerPlacaListaNegra")]
        public async Task<ActionResult<PlacaListaNegra>> Get(string NumeroDePlaca)
        {
            var placaListaNegra = await context.PlacasListaNegra.LastOrDefaultAsync(x => x.NumeroDePlaca == NumeroDePlaca);
            if(placaListaNegra == null)
            {
                return NotFound();
            }
            return placaListaNegra;
        }
    }
}
