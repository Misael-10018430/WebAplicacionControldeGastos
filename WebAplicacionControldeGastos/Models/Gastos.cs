namespace WebAplicacionControldeGastos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Gastos
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Concepto { get; set; }

        public decimal Monto { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int CategoriaId { get; set; }

        public virtual Categorias Categorias { get; set; }
    }
}
