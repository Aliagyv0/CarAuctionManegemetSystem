using CarAuctionApi.Core.Models;
using CarAuctionApi.Core.Models.BaseEntities;
using CarAuctionApi.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CarAuctionApi.Data.Context
{
    public class CarAuctionDbContext : IdentityDbContext<User>
    {
        public CarAuctionDbContext()
        {
        }

        public DbSet<Ban> Bans { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarAuctionDetail> CarAuctionDetails { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsImage> NewsImages { get; set; }
        public DbSet<NewsTag> NewsTags { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public CarAuctionDbContext(DbContextOptions<CarAuctionDbContext> options) : base(options)
        {
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entities = ChangeTracker.Entries<BaseEntity>();

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                    entity.Entity.CreatedAt = DateTime.Now;

                else if (entity.State == EntityState.Modified)
                    entity.Entity.UpdatedAt = DateTime.Now;
            }


            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}