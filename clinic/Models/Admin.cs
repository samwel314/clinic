using System.ComponentModel;

namespace clinic.Models
{
    public class Admin
	{
		public int Id { get; set; }
		public string Fname { get; set; }
		public string Lname { get; set; }
		public string Email { get; set; }
		public string Password { get; set;}
		public string Phone { get; set; }
        public IEnumerable<Ticket> ?  tickets  { get; set; }
        public int D_id { get; set;}
		
		public Doctor  ?  doctor { get; set; }
	}
}
