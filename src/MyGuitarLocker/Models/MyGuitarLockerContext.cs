using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace MyGuitarLocker.Models
{
    public class MyGuitarLockerContext : IdentityDbContext<MyGuitarLockerUser>
    {
        public MyGuitarLockerContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<SoundClip> SoundClips { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Startup.Configuration["Data:MyGuitarLockerContextConnection"];
            optionsBuilder.UseSqlServer(connString);
            base.OnConfiguring(optionsBuilder); 
        }
    }
}
