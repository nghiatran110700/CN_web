namespace Web.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("billDetail")]
    public partial class billDetail
    {
        public int id { get; set; }

        public int idBill { get; set; }

        public int idProduct { get; set; }

        public int? amount { get; set; }

        public int? total { get; set; }
        
        public virtual bill bill { get; set; }

        public virtual Product Product { get; set; }
    }
}
