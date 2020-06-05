namespace Kurumsal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banner")]
    public partial class Banner
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Banner1 { get; set; }

        [StringLength(500)]
        public string ResimURL1 { get; set; }

        [StringLength(100)]
        public string Banner2 { get; set; }

        [StringLength(500)]
        public string ResimURL2 { get; set; }

        [StringLength(100)]
        public string Banner3 { get; set; }

        [StringLength(500)]
        public string ResimURL3 { get; set; }
    }
}
