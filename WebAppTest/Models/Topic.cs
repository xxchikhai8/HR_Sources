namespace WebAppTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Topic")]
    public partial class Topic
    {
        [StringLength(15)]
        [Display(Name = "Topic ID")]
        public string TopicID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Topic Name")]
        public string TopicName { get; set; }

        [Required]
        [StringLength(15)]
        public string CourseID { get; set; }

        public virtual Course Course { get; set; }
    }
}
