namespace WebAppTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseDetail")]
    public partial class CourseDetail
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string CourseID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string TraineeID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(15)]
        public string TrainerID { get; set; }

        public virtual Course Course { get; set; }

        public virtual Trainer Trainer { get; set; }

        public virtual Trainees Trainees { get; set; }
    }
}
