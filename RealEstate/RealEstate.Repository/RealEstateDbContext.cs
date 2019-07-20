using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RealEstate.Core.Entities;
using System;

namespace RealEstate.Repository
{
    public class RealEstateDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration configuration;

        public RealEstateDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        //public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options, IConfiguration configuration) :base(options)
        //{
        //    this.configuration = configuration;
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PropertyImage>()
        .HasKey(PI => new { PI.ImageId, PI.PropertyId });
            modelBuilder.Entity<PropertyImage>()
                .HasOne(p => p.Property)
                .WithMany(PI=>PI.PropertyImages)
                .HasForeignKey(p=>p.PropertyId);
            modelBuilder.Entity<PropertyImage>()
                .HasOne(i=>i.Image)
                .WithMany(PI => PI.PropertyImages)
                .HasForeignKey(PI => PI.ImageId);

            modelBuilder.Entity<Agent>()
            .Property(a => a.Isverified)
            .HasDefaultValue(false);

        
        }
        public DbSet<Agent> Agent { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<PropertyImage> PropertyImage { get; set; }
        public DbSet<PropertyStatus> PropertyStatus { get; set; }
        public DbSet<PropertyType> PropertyType { get; set; }






    }
}
