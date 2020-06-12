﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Seguridad.Contexts.Android
{
    public class ApplicationAndroidDbContext:DbContext
    {
        public ApplicationAndroidDbContext(DbContextOptions<ApplicationAndroidDbContext> options)
            : base(options)
        {

        }

        
    }
}
