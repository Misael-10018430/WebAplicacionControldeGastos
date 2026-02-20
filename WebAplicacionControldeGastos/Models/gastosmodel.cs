using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebAplicacionControldeGastos.Models
{
    public partial class gastosmodel : DbContext
    {
        public gastosmodel()
            : base("name=gastosmodel")
        {
        }

        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Gastos> Gastos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorias>()
                .HasMany(e => e.Gastos)
                .WithRequired(e => e.Categorias)
                .HasForeignKey(e => e.CategoriaId)
                .WillCascadeOnDelete(false);
        }
    }
}
