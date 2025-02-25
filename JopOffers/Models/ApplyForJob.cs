﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JopOffers.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        public String Message { get; set; }
        public DateTime ApplyDate { get; set; }
        public int JobId { get; set; }
        public String UserId { get; set; }
        
        public virtual Job job { get; set; }
        public virtual ApplicationUser user { get; set; }

    }
}