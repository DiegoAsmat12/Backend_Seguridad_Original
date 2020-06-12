using Backend_Seguridad.Models.Android;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Seguridad.Contexts.Android
{
    public class ApplicationAndroidIdentityContext:IdentityDbContext<ApplicationAndroidUser>
    {
        public ApplicationAndroidIdentityContext(DbContextOptions<ApplicationAndroidIdentityContext> options)
            : base(options)
        {

        }
    }
}
