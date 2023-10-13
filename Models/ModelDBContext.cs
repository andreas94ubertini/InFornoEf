using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace InForno.Models
{
    public partial class ModelDBContext : DbContext
    {
        public ModelDBContext()
            : base("name=ModelDBContext")
        {
        }

        public virtual DbSet<DettaglioOrdine> DettaglioOrdine { get; set; }
        public virtual DbSet<Ordine> Ordine { get; set; }
        public virtual DbSet<Prodotto> Prodotto { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ordine>()
                .Property(e => e.Importo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Ordine>()
                .HasMany(e => e.DettaglioOrdine)
                .WithRequired(e => e.Ordine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prodotto>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prodotto>()
                .HasMany(e => e.DettaglioOrdine)
                .WithRequired(e => e.Prodotto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Ordine)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
