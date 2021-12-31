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
        public string TopicID { get; set; }

        [Required]
        [StringLength(50)]
        public string TopicName { get; set; }

        [Required]
        [StringLength(15)]
        public string CourseID { get; set; }

        public virtual Course Course { get; set; }
    }
}
