namespace Cieros.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string Recepient { get; set; }

        public string MessageBody { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateSent { get; set; }

        [StringLength(50)]
        public string MessageType { get; set; }

        [StringLength(50)]
        public string MessageTitle { get; set; }
    }
}
