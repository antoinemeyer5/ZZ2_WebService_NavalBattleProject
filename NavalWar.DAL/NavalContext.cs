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
            //Renommage de table ?
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<Player>().ToTable("Player"); 
            modelBuilder.Entity<Map>().ToTable("Map");
            modelBuilder
                .Entity<Map>()
                .Property(elt => elt.Body)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<List<int>>>(v, (JsonSerializerOptions)null));

            // We save game only if we have Map right ? Or the game doesn't have any sense
            /*modelBuilder
                .Entity<Game>()
                .Property(elt => elt.Map0)
                .IsRequired();
            modelBuilder
                .Entity<Game>()
                .Property(elt => elt.Map1)
                .IsRequired();*/

            // Ask the t-shirt why this part is necessary ? We already have a method to Serialize Map list so why isn't it automatically used for ?
            modelBuilder.Entity<Game>()
                .Property(elt => elt.Map0)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<Map>(v, (JsonSerializerOptions)null));
            modelBuilder.Entity<Game>()
                .Property(elt => elt.Map1)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<Map>(v, (JsonSerializerOptions)null));



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
