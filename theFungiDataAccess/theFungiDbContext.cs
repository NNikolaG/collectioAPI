﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theFungiDataAccess.Configurations;
using theFungiDomain;
using theFungiDomain.Entities;

namespace theFungiDataAccess
{
    public class theFungiDbContext : DbContext
    {

        //public theFungiDbContext(DbContextOptions options = null) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=tcp:thefungiapidbserver.database.windows.net,1433;Initial Catalog=theFungiAPI_db;Persist Security Info=False;User ID=nikolagutic;Password=Nikola2208;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-RN8A21H;Initial Catalog=theFungi;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfigurations());
            //modelBuilder.ApplyConfiguration(new CollectionItemInfosConfigurations());
            modelBuilder.ApplyConfiguration(new CollectionsConfigurations());
            modelBuilder.ApplyConfiguration(new CollectionItemConfiguration());

            modelBuilder.Entity<RoleUseCases>().HasKey(x => new { x.RoleId, x.UseCaseId });
            modelBuilder.Entity<Follows>().HasKey(x => new { x.CollectionId, x.UserId });
        }

        public DbSet<Collections> Collections { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<CollectionItems> CollectionItems { get; set; }
        public DbSet<CollectionItemInfos> CollectionItemInfos { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Follows> Follows { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<RoleUseCases> RoleUseCases { get; set; }
    }
}
