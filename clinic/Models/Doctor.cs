using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace clinic.Models
{
	public class Doctor
	{
		public int Id { get; set; }
        [Required(ErrorMessage = "This Filed is Required")]
        public string  ? Fname { get; set; }
        [Required(ErrorMessage = "This Filed is Required")]
        public string  ? Lname { get; set; }
        [Required(ErrorMessage = "This Filed is Required")]
        public string  ? Email { get; set; }
        [Required(ErrorMessage = "This Filed is Required")]
        public string  ? Password { get; set; }
        [Required(ErrorMessage = "This Filed is Required")]
        public string  ? Phone { get; set; }

       // public IFormFile file { get; set; }  
        public string   ? Dr_picture { get; set;  }

        public IEnumerable<Contact>  contacts  { get; set; } = new List<Contact>();
        public IEnumerable<Ticket>  tickets { get; set; } = new List<Ticket>();
        public IEnumerable<Admin>  admins { get; set; } = new List<Admin>();

    }
}
