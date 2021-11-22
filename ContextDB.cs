using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDEMONSTRATIONAPP
{
    public class Notes
    {
        public int Id { get; set; }

        public String Name {get; set; }

        public int Nominal { get; set; }

        public String SerialNo { get; set; }

        public String Dest { get; set; }

        public String Version { get; set; }

        public string uAttribute { get; set; }

        public string date_time { get; set; }
    }

    public class NotesDbContext : DbContext
    {
        public NotesDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=BVSDESKTOP.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //entities
        public DbSet<Notes> Notes { get; set; }
    }
}
