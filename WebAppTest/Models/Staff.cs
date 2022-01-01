namespace WebAppTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [StringLength(15)]
        [Display(Name = "Staff ID")]
        public string StaffID { get; set; }

        [Required]
        [StringLength(15)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Full Name")]
        public string StaffName { get; set; }

        public int Age { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(150)]
        public string Address { get; set; }

        public virtual Account Account { get; set; }
    }
}
