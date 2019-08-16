namespace Cieros.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MonthlySalary")]
    public partial class MonthlySalary
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string StaffID { get; set; }

        [StringLength(50)]
        public string Month { get; set; }

        [StringLength(50)]
        public string Year { get; set; }

        public decimal? BasicAmount { get; set; }

        public decimal? Additions { get; set; }

        public decimal? Deductions { get; set; }

        public decimal? Balance { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
