using Backend_Seguridad.Models.Software;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Seguridad.Contexts.Software
{
    public class ApplicationSoftwareIdentityContext:IdentityDbContext<ApplicationSoftwareUser>
    {
        public ApplicationSoftwareIdentityContext(DbContextOptions<ApplicationSoftwareIdentityContext> options)
            : base(options)
        {

        }
    }
}
