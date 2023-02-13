using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavalWar.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace NavalWar.DAL
{
    public class NavalContext : DbContext
    {
        public NavalContext(DbContextOptions<NavalContext> op) : base(op) { }

        public DbSet<Map> Maps { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<Player>().ToTable("Player"); 
            modelBuilder.Entity<Map>().ToTable("Map");

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Map1)
                .WithMany()
                .HasForeignKey(g => g.idMap1)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Map0)
                .WithMany()
                .HasForeignKey(g => g.idMap0)
                .OnDelete(DeleteBehavior.NoAction);

            /*modelBuilder.Entity<Map>()
               .HasOne(m => m._associatedPlayer)
               .WithMany()
               .HasForeignKey(m => m.idPlayer)
               .OnDelete(DeleteBehavior.NoAction);*/
        }


        public void DeleteMaps(int id)
        {
            try
            {
                Maps.Remove(Maps.Find(id));
                SaveChanges();
            }
            catch
            {
                //Does nothing
            }
        }

    }
}
