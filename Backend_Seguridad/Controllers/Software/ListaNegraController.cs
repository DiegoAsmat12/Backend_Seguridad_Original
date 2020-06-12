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
    public class ListaNegraController : ControllerBase
    {
        private readonly ApplicationSoftwareDbContext context;
        private readonly IMapper mapper;

        public ListaNegraController(ApplicationSoftwareDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(Name = "ObtenerListaNegra")]
        public async Task<ActionResult<IEnumerable<ListaNegraDTO>>> Get()
        {

            var ListaNegra = await context.ListaNegra.ToListAsync();
            var ListaNegraDTO = mapper.Map<List<ListaNegraDTO>>(ListaNegra);
            return ListaNegraDTO;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{NumeroDePlaca}",Name="ObtenerPlacaDEListaNegra")]
        public async Task<ActionResult<ListaNegraDTO>> Get(string NumeroDePlaca)
        {
            
            var placaListaNegra = await context.ListaNegra.FirstOrDefaultAsync(x => x.NumeroDePlaca == NumeroDePlaca);
            if (placaListaNegra == null)
            {
                return NotFound();
            }
            var placaListaNegraDTO = mapper.Map<ListaNegraDTO>(placaListaNegra);
            return placaListaNegraDTO;
        }
        [HttpPost(Name="CrearPlacaParaListaNegra")]
        public async Task<ActionResult> Post([FromBody] ListaNegraCreacionDTO listaNegraCreacion)
        {
            var persona = await context.Personas.FirstOrDefaultAsync(x => x.DNI == listaNegraCreacion.DNI);
            if (persona == null)
            {
                var placaListaNegra = mapper.Map<ListaNegra>(listaNegraCreacion);
                placaListaNegra.Nombres = "Municipalidad";
                placaListaNegra.Estado = "No Capturado";
                context.Add(placaListaNegra);
                var placas = await context.Placas.Where(x => x.NumeroDePlaca == listaNegraCreacion.NumeroDePlaca).ToListAsync();
                if (placas != null)
                {
                    foreach(Placa placa in placas)
                    {
                        
                        var placaNegra = new PlacaListaNegra();
                        placaNegra.NumeroDePlaca = placa.NumeroDePlaca;
                        placaNegra.NombreCamara = placa.NombreCamara;
                        placaNegra.Direccion = placa.Direccion;
                        placaNegra.Latitud = placa.Latitud;
                        placaNegra.Longitud = placa.Longitud;
                        placaNegra.Hora = placa.Hora;
                        placaNegra.Fecha = placa.Fecha;
                        placaNegra.Codigo = placa.Codigo;
                        placaNegra.ImagenCarro = placa.ImagenCarro;
                        placaNegra.ImagenPlaca = placa.ImagenPlaca;
                        context.PlacasListaNegra.Add(placaNegra);
                    }
                    
                }
                await context.SaveChangesAsync();
                var placaListaNegraDTO = mapper.Map<ListaNegraDTO>(placaListaNegra);
                return new CreatedAtRouteResult("ObtenerCamara", new { id = placaListaNegra.Id }, placaListaNegraDTO);
            }
            else
            {
                var placaListaNegra = mapper.Map<ListaNegra>(listaNegraCreacion);
                placaListaNegra.Persona = persona;
                placaListaNegra.Nombres = persona.Nombres;
                placaListaNegra.ApellidoPaterno = persona.ApellidoPaterno;
                placaListaNegra.ApellidoMaterno = persona.ApellidoMaterno;
                placaListaNegra.Direccion = persona.Direccion;
                placaListaNegra.FechaDeNacimiento = persona.FechaDeNacimiento;
                placaListaNegra.NumeroDeCelular = persona.NumeroDeCelular;
                placaListaNegra.Estado = "No Capturado";
                context.Add(placaListaNegra);
                await context.SaveChangesAsync();
                var placaListaNegraDTO = mapper.Map<ListaNegraDTO>(placaListaNegra);
                return new CreatedAtRouteResult("ObtenerCamara", new { id = placaListaNegra.Id }, placaListaNegraDTO);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPatch("{NumeroDePlaca}", Name = "ActualizarParcialmentePlacaListaNegra")]
        public async Task<ActionResult> Patch(string NumeroDePlaca, [FromBody] JsonPatchDocument<ListaNegra> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var placaDeLaDB = await context.ListaNegra.FirstOrDefaultAsync(x => x.NumeroDePlaca == NumeroDePlaca);
            if (placaDeLaDB == null)
            {
                return NotFound();
            }
            var placa = placaDeLaDB;

            patchDocument.ApplyTo(placa, ModelState);
            var isValid = TryValidateModel(placaDeLaDB);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
