using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RemoteBranding.Models
{
    public class BrandedBottle
    {
        [Display(Name = "Tag Number")]
        public string TagNumber { get; set; }
        [Display( Name="UPC Number")]
        public string UPC { get; set; }
        public Guid NozzelType { get; set; }
        public Guid LocationID { get; set; }
    }
}