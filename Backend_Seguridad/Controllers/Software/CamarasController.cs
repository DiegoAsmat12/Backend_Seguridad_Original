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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CamarasController :ControllerBase
    {
        private readonly ApplicationSoftwareDbContext context;
        private readonly IMapper mapper;

        public CamarasController(ApplicationSoftwareDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet(Name="ObtenerCamaras")]
        public async Task<ActionResult<IEnumerable<CamaraDTO>>> Get()
        {

            var camaras = await context.Camaras.ToListAsync();
            var camarasDTO = mapper.Map<List<CamaraDTO>>(camaras);
            return camarasDTO;
        }
        [HttpGet("{id}",Name="ObtenerCamara")]
        public async Task<ActionResult<CamaraDTO>> Get(int id)
        {
            var camara = await context.Camaras.FirstOrDefaultAsync(x => x.Id == id);
            if(camara == null)
            {
                return NotFound();
            }
            var camaraDTO = mapper.Map<CamaraDTO>(camara);
            return camaraDTO;
        }
        [HttpPost(Name = "CrearCamara")]
        public async Task<ActionResult> Post([FromBody] CamaraCreacionDTO camaraCreacion)
        {
            var camara = mapper.Map<Camara>(camaraCreacion);
            context.Add(camara);
            await context.SaveChangesAsync();
            var camaraDTO = mapper.Map<CamaraDTO>(camara);
            return new CreatedAtRouteResult("ObtenerCamara", new { id = camara.Id }, camaraDTO);
        }
        [HttpPut("{id}", Name= "ActualizarCamara")]
        public async Task<ActionResult> Put(int id, [FromBody] CamaraCreacionDTO camaraActualizacion)
        {
            var camara = mapper.Map<Camara>(camaraActualizacion);
            camara.Id = id;
            context.Entry(camara).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id}", Name = "ActualizarParcialmenteCamara")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<CamaraCreacionDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var camaraDeLaDB = await context.Camaras.FirstOrDefaultAsync(x => x.Id == id);
            if (camaraDeLaDB == null)
            {
                return NotFound();
            }
            var camaraDTO = mapper.Map<CamaraCreacionDTO>(camaraDeLaDB);

            patchDocument.ApplyTo(camaraDTO, ModelState);
            mapper.Map(camaraDTO, camaraDeLaDB);
            var isValid = TryValidateModel(camaraDeLaDB);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }


            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}", Name = "BorrarCamara")]
        public async Task<ActionResult<Camara>> Delete(int id)
        {
            var camaraId = await context.Camaras.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (camaraId == default(int))
            {
                return NotFound();
            }
            context.Remove(new Camara { Id = camaraId });
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
