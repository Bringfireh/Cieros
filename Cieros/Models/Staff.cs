namespace Cieros.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            MonthlySalaries = new HashSet<MonthlySalary>();
        }

        [StringLength(50)]
        public string StaffID { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string Othernames { get; set; }

        [StringLength(50)]
        public string Sex { get; set; }

        [StringLength(50)]
        public string MaritalStatus { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(50)]
        public string MaidenName { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string StatOfOrigin { get; set; }

        [StringLength(50)]
        public string LocalGovt { get; set; }

        [StringLength(50)]
        public string Religion { get; set; }

        [StringLength(50)]
        public string Discipline { get; set; }

        [StringLength(50)]
        public string Qualification { get; set; }

        //[Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfAppointment { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(50)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [StringLength(50)]
        public string AccountType { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string EmailAddress { get; set; }

        [StringLength(50)]
        public string NextOfKinName { get; set; }

        [StringLength(50)]
        public string NextOfKinAddress { get; set; }

        [StringLength(50)]
        public string NextOfKinPhone { get; set; }

        [StringLength(50)]
        public string NextOfKinEmail { get; set; }

        [StringLength(50)]
        public string NextOfKinRelationship { get; set; }

        public bool? ActiveStatus { get; set; }

        public decimal? BasicSalary { get; set; }

        public decimal? MonthlySalary { get; set; }
       

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MonthlySalary> MonthlySalaries { get; set; }
    }
}
