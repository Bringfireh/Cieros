namespace Cieros.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string Othernames { get; set; }

        [StringLength(50)]
        public string Sex { get; set; }

        //[Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(50)]
        public string StdClass { get; set; }

        [StringLength(50)]
        public string Label { get; set; }

        [StringLength(50)]
        public string AdmissionNumber { get; set; }

        [StringLength(50)]
        public string GuardianID { get; set; }

        public virtual Guardian Guardian { get; set; }
    }
}
