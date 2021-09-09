using System;
using GameStore.Domain.Enums;
using GameStore.Domain.Models;
using GameStore.Infrastructure.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Data.Context
{
    public class GameStoreContext : DbContext
    {
        public GameStoreContext(DbContextOptions options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Platform> Platforms { get; set; }

        public DbSet<GamePlatform> GamePlatforms { get; set; }

        public DbSet<GameGenre> GameGenres { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasKey(x => x.Id)
                .IsClustered();

            modelBuilder.Entity<Game>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Game>()
                .HasAlternateKey(x => x.Key);

            modelBuilder.Entity<Genre>()
                .HasKey(x => x.Id)
                .IsClustered();

            modelBuilder.Entity<Genre>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Genre>()
                .HasMany(x => x.Genres)
                .WithOne(y => y.ParentGenre)
                .HasForeignKey(x => x.ParentGenreId);

            modelBuilder.Entity<Platform>()
                .HasKey(x => x.Id)
                .IsClustered();

            modelBuilder.Entity<Platform>()
                .HasAlternateKey(x => x.Name);

            modelBuilder.Entity<Platform>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<GameGenre>()
                .HasKey(x => new { x.GameId, x.GenreId });

            modelBuilder.Entity<GameGenre>()
                .HasOne(x => x.Game)
                .WithMany(y => y.GameGenres)
                .HasForeignKey(x => x.GameId);

            modelBuilder.Entity<GamePlatform>()
                .HasKey(x => new { x.GameId, x.PlatformId });

            modelBuilder.Entity<GamePlatform>()
                .HasOne(x => x.Game)
                .WithMany(y => y.GamePlatforms)
                .HasForeignKey(x => x.GameId);

            modelBuilder.Entity<GamePlatform>()
                .HasOne(x => x.Platform)
                .WithMany(y => y.GamePlatforms)
                .HasForeignKey(x => x.PlatformId);

            modelBuilder.Entity<Comment>()
                .HasKey(x => x.Id)
                .IsClustered();

            modelBuilder.Entity<Comment>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Publisher>()
                .HasKey(x => x.Id)
                .IsClustered();

            modelBuilder.Entity<Publisher>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Publisher>()
                .HasMany(x => x.Games)
                .WithOne(y => y.Publisher)
                .HasForeignKey(x => x.PublisherId);

            modelBuilder.Entity<OrderDetails>()
                .HasKey(x => x.Id)
                .IsClustered();

            modelBuilder.Entity<OrderDetails>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<OrderDetails>()
                .HasOne(s => s.Order)
                .WithMany(g => g.OrderDetails)
                .HasForeignKey(s => s.OrderId);

            modelBuilder.Entity<Order>()
                .HasKey(x => x.Id)
                .IsClustered();

            modelBuilder.Entity<Order>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>()
                .Property(x => x.Status)
                .HasConversion(
                    y => y.ToString(),
                    y => (OrderStatus)Enum.Parse(typeof(OrderStatus), y));

            modelBuilder.Seed();
        }
    }
}
