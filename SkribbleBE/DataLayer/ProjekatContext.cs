using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DataLayer
{
    public class ProjekatContext :DbContext
    { 
        public ProjekatContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<Rec> Reci { get; set; }
        public DbSet<RecPoKategoriji> ReciPoKategorijama { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Kategorija>()
            //    .HasMany(k => k.ReciPoKategorijama)
            //    .WithOne(recPoKategoriji => recPoKategoriji.Kategorija).IsRequired();

            //modelBuilder.Entity<Rec>()
            //    .HasMany(k=>k.RecPoKategoriji)
            //    .WithOne(RecPoKategoriji=>RecPoKategoriji.Rec).IsRequired();

            //modelBuilder.Entity<RecPoKategoriji>()
            //    .HasOne(x => x.Kategorija)
            //    .WithMany(x => x.ReciPoKategorijama).IsRequired();

            //modelBuilder.Entity<RecPoKategoriji>()
            //     .HasOne(x => x.Rec)
            //    .WithMany(x => x.RecPoKategoriji).IsRequired();
        }
    }
}
