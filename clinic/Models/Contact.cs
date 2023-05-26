using System.ComponentModel.DataAnnotations;

namespace clinic.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Your Name")]
        public string ? Name { get; set; }
        [Required(ErrorMessage = "Please Enter Your Message")]
        public string ?  Message { get; set; }
        [Required(ErrorMessage = "Please Enter Your Phone")]
        public string ?  Phone { get; set; }
        [Required(ErrorMessage = "Please Enter Your Email")]
        public string ?  Email { get; set; }

        public int  D_id { get; set; }
        public Doctor ? doctor { get; set; }  
    }
}
