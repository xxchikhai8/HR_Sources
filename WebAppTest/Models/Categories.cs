namespace WebAppTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Categories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Categories()
        {
            Course = new HashSet<Course>();
        }

        [Key]
        [StringLength(15)]
        [Display(Name = "Category ID")]
        public string CatID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Category Name")]
        public string CatName { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [Display(Name = "Category Description")]
        public string CatDes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course> Course { get; set; }
    }
}
