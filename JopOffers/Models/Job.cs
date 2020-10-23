using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JopOffers.Models
{
    public class Job
    {
        public int Id { get; set; }
        [DisplayName("Job Name")]
        public String JobTitle { get; set; }
        [DisplayName("Job Description")]
        [AllowHtml]
        public String JobContent { get; set; }
        [DisplayName("Job Image")]
        public string jobImage { get; set; }
        //Forgen key from category
        [DisplayName("Job Type")]
        public int CategoryId { get; set; }

        public String UserID{ get; set; }


        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}