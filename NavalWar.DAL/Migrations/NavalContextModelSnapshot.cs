﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NavalWar.DAL;

#nullable disable

namespace NavalWar.DAL.Migrations
{
    [DbContext(typeof(NavalContext))]
    partial class NavalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NavalWar.DAL.Models.Game", b =>
                {
                    b.Property<int>("IdGame")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdGame"));

                    b.Property<float>("Duration")
                        .HasColumnType("real");

                    b.Property<string>("Map0")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Map1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Result")
                        .HasColumnType("int");

                    b.Property<int>("WinnerId")
                        .HasColumnType("int");

                    b.HasKey("IdGame");

                    b.ToTable("Game", (string)null);
                });

            modelBuilder.Entity("NavalWar.DAL.Models.Map", b =>
                {
                    b.Property<int>("IdMap")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMap"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Column")
                        .HasColumnType("int");

                    b.Property<int>("IdInGame")
                        .HasColumnType("int");

                    b.Property<int>("Line")
                        .HasColumnType("int");

                    b.Property<string>("ListTarget")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("_associatedPlayerId")
                        .HasColumnType("int");

                    b.HasKey("IdMap");

                    b.HasIndex("_associatedPlayerId");

                    b.ToTable("Map", (string)null);
                });

            modelBuilder.Entity("NavalWar.DAL.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Player", (string)null);
                });

            modelBuilder.Entity("NavalWar.DAL.Models.Map", b =>
                {
                    b.HasOne("NavalWar.DAL.Models.Player", "_associatedPlayer")
                        .WithMany()
                        .HasForeignKey("_associatedPlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_associatedPlayer");
                });
#pragma warning restore 612, 618
        }
    }
}
