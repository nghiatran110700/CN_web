namespace Web.Models.Entity
{
    using System.Web;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            billDetails = new HashSet<billDetail>();
        }

        [Key]
        public int idProduct { get; set; }

        [StringLength(50)]
        public string nameProduct { get; set; }

        public int price { get; set; }

        public int amuont { get; set; }

        public string photo { get; set; }

        public string description { get; set; }

        public int idCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<billDetail> billDetails { get; set; }

        public virtual category category { get; set; }

    }
}
