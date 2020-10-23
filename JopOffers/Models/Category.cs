using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JopOffers.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Type of job")]
        public String CategoryName { get; set; }
        [Required]
        [DisplayName("Type of Description")]
        public String CategoryDescription { get; set; }

        public virtual ICollection<Job> jobs { get; set; }

    }
}