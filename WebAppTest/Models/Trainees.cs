namespace WebAppTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Trainees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trainees()
        {
            CourseDetail = new HashSet<CourseDetail>();
        }

        [Key]
        [StringLength(15)]
        [Display(Name = "Trainee ID")]
        public string TraineeID { get; set; }

        [Required]
        [StringLength(15)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Full Name")]
        public string TraineeName { get; set; }

        public int Age { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DofB { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Education")]
        public string Edu { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseDetail> CourseDetail { get; set; }
    }
}
