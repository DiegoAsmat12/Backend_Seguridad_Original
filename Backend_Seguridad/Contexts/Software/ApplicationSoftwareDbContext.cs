using Backend_Seguridad.Entities.Software;
using Backend_Seguridad.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Seguridad.Contexts.Software
{
    public class ApplicationSoftwareDbContext: DbContext
    {
        public ApplicationSoftwareDbContext(DbContextOptions<ApplicationSoftwareDbContext> options)
            : base(options)
        {

        }
        public DbSet<Camara> Camaras { get; set; }
        public DbSet<Placa> Placas { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<ListaNegra> ListaNegra { get; set; }
        public DbSet<PlacaListaNegra> PlacasListaNegra { get; set; }
    }
}
