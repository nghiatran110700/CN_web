using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Web.Models.Entity
{
    public partial class ShopModel : DbContext
    {
        public ShopModel()
            : base("name=ShopModel")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<bill> bills { get; set; }
        public virtual DbSet<billDetail> billDetails { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<bill>()
                .HasMany(e => e.billDetails)
                .WithRequired(e => e.bill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.photo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.billDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);
        }

        //public System.Data.Entity.DbSet<Web.Models.Entity.HangBan> HangBans { get; set; }
    }
}
