using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ChatApp.Service.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<chat> chat { get; set; }
        public virtual DbSet<user> user { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<chat>(entity =>
            {
                entity.Property(e => e.creation_date).HasColumnType("date");

                entity.Property(e => e.record_updated).HasColumnType("datetime");

                entity.HasOne(d => d.first_member_)
                    .WithMany(p => p.chatfirst_member_)
                    .HasForeignKey(d => d.first_member_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_chat_first_user");

                entity.HasOne(d => d.second_member_)
                    .WithMany(p => p.chatsecond_member_)
                    .HasForeignKey(d => d.second_member_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_chat_second_user");
            });

            modelBuilder.Entity<user>(entity =>
            {
                entity.Property(e => e.creation_date).HasColumnType("datetime");

                entity.Property(e => e.email).IsRequired();

                entity.Property(e => e.name).IsRequired();

                entity.Property(e => e.password).IsRequired();

                entity.Property(e => e.record_updated).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
