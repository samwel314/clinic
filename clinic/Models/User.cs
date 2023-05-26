using System.ComponentModel.DataAnnotations;

namespace clinic.Models
{
    public class User
    {
        [Required (ErrorMessage = "Please Enter Your Email")]
        public string   ? Email { set; get; }
        [Required(ErrorMessage = "Please Enter Your Password")]
        public string ? Password { set; get; }    
    }
}
