using back_end.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Producto> Producto { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
    }
}
