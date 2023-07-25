using System.ComponentModel.DataAnnotations;

namespace Jaxis.DrinkInventory.Reporting.Web2.Models
{
    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Keep me logged on")]
        public bool StayLoggedIn { get; set; }
    }
}