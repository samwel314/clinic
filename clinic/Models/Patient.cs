using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace clinic.Models
{
	public class Patient
	{
		
		public int  Id { get; set; }
		[Required (ErrorMessage = "This Filed is Required")]
		public string  ? Fname { get; set; }
        [Required(ErrorMessage = "This Filed is Required")]
        public string ?  Lname { get; set; }
		[Range (16 , 80)]
		public int  ? Age { get; set; }
        [Required(ErrorMessage = "This Filed is Required")]
        public string ?  Email { get; set; }
        [Required(ErrorMessage = "This Filed is Required")]

        public string ?  Gender { get; set; }
        [Required(ErrorMessage = "This Filed is Required")]
        public string  ? Password { get; set; }
        [Required]
        public string ?  Phone { get; set; }
		[NotMapped]
        [Compare("Password",ErrorMessage ="mis")]
		public string  ? Confirm_Password { get; set; }	
        //navigations 
         public IEnumerable<Ticket>  ? tickets { get; set;  }

        public IEnumerable<Comment>?  comments { get; set; }
        public string GenerateRandomEmail()
        {
            string[] domains = { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com" };
            string[] names = { this.Fname , this.Lname   };
            Random rand = new Random();
            string name = names[rand.Next(names.Length)];
            string domain = domains[rand.Next(domains.Length)];
            return $"{name}{this.Id}@{domain}";
        }


    }
}
