namespace Cieros.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Guardian")]
    public partial class Guardian
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Guardian()
        {
            Students = new HashSet<Student>();
        }

        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(100)]
        public string FatherName { get; set; }

        [StringLength(50)]
        public string MotherName { get; set; }

        [StringLength(50)]
        public string MotherPhoneNumber { get; set; }

        [StringLength(50)]
        public string FatherPhoneNumber { get; set; }

        [StringLength(50)]
        public string FatherEmail { get; set; }

        [StringLength(50)]
        public string MotherEmail { get; set; }

        [StringLength(50)]
        public string ContactAddress { get; set; }

        [StringLength(50)]
        public string WorkAddress { get; set; }

        public bool? ActiveStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
