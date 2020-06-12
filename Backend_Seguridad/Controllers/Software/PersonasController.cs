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
    public class PersonasController:ControllerBase
    {
        private readonly ApplicationSoftwareDbContext context;
        public PersonasController(ApplicationSoftwareDbContext context)
        {
            this.context = context;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(Name = "ObtenerPersonas")]
        public async Task<ActionResult<IEnumerable<Persona>>> Get()
        {

            var personas = await context.Personas.ToListAsync();
            return personas;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{DNI}",Name="ObtenerPersona")]
        public async Task<ActionResult<Persona>> Get(string DNI)
        {
            var persona = await context.Personas.FirstOrDefaultAsync(x => x.DNI == DNI);
            if (persona == null)
            {
                return NotFound();
            }
            return persona;
        }
        [HttpPost(Name="CrearPersona")]
        public async Task<ActionResult<Persona>> Post([FromBody] Persona persona)
        {
            context.Add(persona);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("ObtenerPersona", new { id = persona.Id }, persona);
        }
        [HttpPut("{DNI}", Name = "ActualizarPersona")]
        public async Task<ActionResult> Put(string DNI, [FromBody] Persona personaActualizacion)
        {

            personaActualizacion.DNI = DNI;
            context.Entry(personaActualizacion).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{DNI}", Name = "ActualizarParcialmentePersona")]
        public async Task<ActionResult> Patch(string DNI, [FromBody] JsonPatchDocument<Persona> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var personaDeLaDB = await context.Personas.FirstOrDefaultAsync(x => x.DNI == DNI);
            if (personaDeLaDB == null)
            {
                return NotFound();
            }
            var persona = personaDeLaDB;

            patchDocument.ApplyTo(persona, ModelState);
            var isValid = TryValidateModel(personaDeLaDB);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }


            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{DNI}", Name = "BorrarPersona")]
        public async Task<ActionResult<Persona>> Delete(string DNI)
        {
            var personaDNI= await context.Personas.Select(x => x.DNI).FirstOrDefaultAsync(x => x == DNI);
            if (personaDNI == default(string))
            {
                return NotFound();
            }
            context.Remove(new Persona { DNI = personaDNI });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
