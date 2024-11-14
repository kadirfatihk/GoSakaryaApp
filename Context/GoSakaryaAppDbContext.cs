using GoSakaryaApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSakaryaApp.Data.Context
{
    public class GoSakaryaAppDbContext :DbContext
    {
        public GoSakaryaAppDbContext(DbContextOptions<GoSakaryaAppDbContext> options) : base(options)
        {

        }

        // Veritabanı tablolarının yapılandırıldığı metot. Fluent API kullanılır.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new AreaConfiguration());

            // SettingEntity için başlangıç verisi eklenir. Bakım modu varsayılan olarak kapalıdır.
            modelBuilder.Entity<SettingEntity>().HasData(new SettingEntity
            {
                Id = 1,
                MaintenenceMode = false,
            });

            base.OnModelCreating(modelBuilder);
        }

        // Veritabanı tablolarını temsil eden DbSet özellikleri.
        public DbSet<AreaEntity> Areas => Set<AreaEntity>();
        public DbSet<CommentEntity> Comments => Set<CommentEntity>();
        public DbSet<EventEntity> Events => Set<EventEntity>();
        public DbSet<EventTicketEntity> Tickets => Set<EventTicketEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();

        public DbSet<SettingEntity> Settings { get; set; }
    }
}
