using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrasctruture.Context
{
    public class APIContext :DbContext
    {
        public APIContext(DbContextOptions<APIContext> options):base(options) {}
        public DbSet<Enterprise> Enterprise { get; set; }
    }
}
