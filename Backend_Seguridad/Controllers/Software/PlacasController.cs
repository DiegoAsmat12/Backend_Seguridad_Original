using AutoMapper;
using Backend_Seguridad.Contexts.Software;
using Backend_Seguridad.Entities.Software;
using Backend_Seguridad.Models.Software;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Seguridad.Controllers.Software
{
    [ApiController]
    [Route("software/api/[controller]")]
    public class PlacasController:ControllerBase
    {
        private readonly ApplicationSoftwareDbContext context;
        private readonly IMapper mapper;

        public PlacasController(ApplicationSoftwareDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(Name = "ObtenerPlacas")]
        public async Task<ActionResult<IEnumerable<PlacaDTO>>> Get(string buscar,string orden = "ID",string tipo_orden = "DESC",int pagina = 1,int registros_por_pagina = 10)
        {
            
            var placas = await context.Placas.ToListAsync();
            if (!string.IsNullOrEmpty(buscar))
            {
                foreach (var item in buscar.Split(new char[] { ' ' },
                         StringSplitOptions.RemoveEmptyEntries))
                {
                    placas = placas.Where(x => x.NombreCamara.Contains(item) ||
                                                  x.NumeroDePlaca.Contains(item)||
                                                  x.Hora.Contains(item)||
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
            var placasDTO = mapper.Map<List<PlacaDTO>>(placas);
            return placasDTO;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}", Name = "ObtenerPlaca")]
        public async Task<ActionResult<PlacaDTO>> Get(int id)
        {
            var placa = await context.Placas.FirstOrDefaultAsync(x => x.Id == id);
            if (placa == null)
            {
                return NotFound();
            }
            var placaDTO = mapper.Map<PlacaDTO>(placa);
            return placaDTO;
        }
        [HttpPost(Name = "CrearPlaca")]
        public async Task<ActionResult> Post([FromBody] PlacaCreacionDTO placaCreacion)
        {
            var camara = await context.Camaras.FirstOrDefaultAsync(x => x.Nombre == placaCreacion.NombreCamara);
            if(camara == null)
            {
                return BadRequest("La camara no existe");
            }
            else
            {
                var placa = mapper.Map<Placa>(placaCreacion);
                placa.CamaraDetectora = camara;
                placa.Direccion = camara.Direccion;
                placa.Latitud = camara.Latitud;
                placa.Longitud = camara.Longitud;
                placa.Hora = DateTime.Now.ToShortTimeString();
                placa.Fecha = DateTime.Now.ToShortDateString();
                placa.Codigo = $"{placa.NumeroDePlaca}-{camara.Id}";
                context.Add(placa);
                camara.Placas.Add(placa);
                var placaListaNegra = await context.ListaNegra.FirstOrDefaultAsync(x => x.NumeroDePlaca == placa.NumeroDePlaca);
                if (placaListaNegra != null)
                {
                    var placaNegra = new PlacaListaNegra();
                    placaNegra.NumeroDePlaca = placa.NumeroDePlaca;
                    placaNegra.NombreCamara = placa.NombreCamara;
                    placaNegra.Direccion = placa.Direccion;
                    placaNegra.Latitud=placa.Latitud;
                    placaNegra.Longitud=placa.Longitud;
                    placaNegra.Hora=placa.Hora;
                    placaNegra.Fecha=placa.Fecha;
                    placaNegra.Codigo=placa.Codigo;
                    placaNegra.ImagenCarro = placa.ImagenCarro;
                    placaNegra.ImagenPlaca = placa.ImagenPlaca;
                    context.PlacasListaNegra.Add(placaNegra);
                }
                await context.SaveChangesAsync();
                var placaDTO = mapper.Map<PlacaDTO>(placa);
                return new CreatedAtRouteResult("ObtenerPlaca", new { id = placa.Id }, placaDTO);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}", Name = "ActualizarPlaca")]
        public async Task<ActionResult> Put(int id, [FromBody] PlacaCreacionDTO placaActualizacion)
        {
            var placa = mapper.Map<Placa>(placaActualizacion);
            placa.Id = id;
            context.Entry(placa).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPatch("{id}", Name = "ActualizarParcialmentePlaca")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PlacaCreacionDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var placaDeLaDB = await context.Placas.FirstOrDefaultAsync(x => x.Id == id);
            if (placaDeLaDB == null)
            {
                return NotFound();
            }
            var placaDTO = mapper.Map<PlacaCreacionDTO>(placaDeLaDB);

            patchDocument.ApplyTo(placaDTO, ModelState);
            mapper.Map(placaDTO, placaDeLaDB);
            var isValid = TryValidateModel(placaDeLaDB);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }


            await context.SaveChangesAsync();
            return NoContent();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}", Name = "BorrarPlaca")]
        public async Task<ActionResult<Placa>> Delete(int id)
        {
            var placaId = await context.Placas.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (placaId == default(int))
            {
                return NotFound();
            }
            context.Remove(new Placa { Id = placaId });
            await context.SaveChangesAsync();
            return NoContent();
        }
        private async Task<byte[]> ImagenAArray(string path)
        {
            byte[] imgData = await System.IO.File.ReadAllBytesAsync(path);
            return imgData;
        }
    }
}
