using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TrainX.Models
{
    public partial class TrainXContext : DbContext
    {
        public TrainXContext()
        {
        }

        public TrainXContext(DbContextOptions<TrainXContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coach> Coaches { get; set; } = null!;
        public virtual DbSet<Sport> Sports { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Video> Videos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coach>(entity =>
            {
                entity.ToTable("Coach");

                entity.Property(e => e.Email).HasMaxLength(100);
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.ToTable("Sport");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Sport");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("Video");

                entity.Property(e => e.LastUpdate).HasColumnType("date");

                entity.Property(e => e.PublishingDate).HasColumnType("date");

                entity.HasOne(d => d.Coach)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.CoachId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Video_Coach");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.SportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Video_Sport");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
