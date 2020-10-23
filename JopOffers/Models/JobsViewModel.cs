using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JopOffers.Models
{
    public class JobsViewModel
    {
        public String JobTitle { get; set; }
        public IEnumerable<ApplyForJob> Items { get; set; }
    }
}