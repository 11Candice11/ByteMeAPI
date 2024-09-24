using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Xml;

namespace ByteMeAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define your DbSets (tables) here
        public DbSet<MyEntity> MyEntities { get; set; }

        // Add more DbSet properties for other entities
    }
}
