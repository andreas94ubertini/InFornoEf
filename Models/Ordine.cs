namespace InForno.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordine")]
    public partial class Ordine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ordine()
        {
            DettaglioOrdine = new HashSet<DettaglioOrdine>();
        }

        [Key]
        public int IdOrdine { get; set; }

        public bool Evaso { get; set; }

        public bool Concluso { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        [StringLength(50)]
        public string TempoConsegna { get; set; }

        [Required]
        public string Commento { get; set; }

        public string Indirizzo { get; set; }

        [Column(TypeName = "money")]
        public decimal Importo { get; set; }

        public int IdUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettaglioOrdine> DettaglioOrdine { get; set; }

        public virtual User User { get; set; }
    }
}
