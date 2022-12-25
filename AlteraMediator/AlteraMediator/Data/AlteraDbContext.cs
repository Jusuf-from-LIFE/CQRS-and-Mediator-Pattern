using AlteraMediator.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlteraMediator.Data
{
    public class AlteraDbContext : DbContext
    {
        public AlteraDbContext(DbContextOptions<AlteraDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
