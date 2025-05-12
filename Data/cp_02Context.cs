using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using cp_02.Domain.Entity;

namespace cp_02.Data
{
    public class cp_02Context : DbContext
    {
        public cp_02Context (DbContextOptions<cp_02Context> options)
            : base(options)
        {
        }

        public DbSet<cp_02.Domain.Entity.Vehicle> Vehicle { get; set; } = default!;
    }
}
