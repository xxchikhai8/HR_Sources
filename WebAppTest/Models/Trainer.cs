namespace WebAppTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Trainer")]
    public partial class Trainer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trainer()
        {
            CourseDetail = new HashSet<CourseDetail>();
        }

        [StringLength(15)]
        [Display(Name = "Trainer ID")]
        public string TrainerID { get; set; }

        [Required]
        [StringLength(15)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public int Age { get; set; }

        [Required]
        [StringLength(100)]
        public string Specialty { get; set; }

        [Required]
        [StringLength(150)]
        public string Address { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseDetail> CourseDetail { get; set; }
    }
}
