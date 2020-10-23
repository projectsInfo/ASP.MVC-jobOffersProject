using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JopOffers.Models
{
    public class ContactModel
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public string SubJect { get; set; }
        [Required]
        public String Message { get; set; }
    }
}