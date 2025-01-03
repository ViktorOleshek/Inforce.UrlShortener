﻿using Inforce.UrlShortener.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inforce.UrlShortener.DAL.Data
{
    public class UrlShortenerContext : DbContext
    {
        public UrlShortenerContext()
        {
        }

        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Url> Records { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);

                e.Property(u => u.Login)
                 .IsRequired()
                 .HasMaxLength(80);

                e.Property(u => u.Password)
                 .IsRequired()
                 .HasMaxLength(255);

                e.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                e.Property(u => u.PhoneNumber)
                    .HasMaxLength(20);

                e.HasIndex(u => u.Login)
                 .IsUnique();

                e.HasOne(u => u.Role)
                 .WithOne()
                 .HasForeignKey<User>(u => u.RoleId);
            });

            modelBuilder.Entity<Role>(e =>
            {
                e.HasKey(r => r.Id);

                e.Property(r => r.RoleName)
                 .IsRequired()
                 .HasMaxLength(100);

                e.HasIndex(u => u.RoleName)
                 .IsUnique();
            });

            modelBuilder.Entity<Url>(e =>
            {
                e.HasKey(u => u.Id);

                e.Property(u => u.OriginalUrl)
                 .IsRequired();

                e.Property(u => u.ShortUrl)
                 .IsRequired()
                 .HasMaxLength(255);

                e.Property(u => u.CreatedDate)
                 .IsRequired()
                 .HasDefaultValueSql("GETDATE()");

                e.HasIndex(u => u.OriginalUrl)
                 .IsUnique();
                e.HasIndex(u => u.ShortUrl)
                 .IsUnique();

                e.HasOne(u => u.User)
                 .WithMany(u => u.Urls)
                 .HasForeignKey(u => u.CreatedBy);
            });
        }
    }
}
