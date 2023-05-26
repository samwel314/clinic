namespace	clinic.Models
{
	public class Ticket
	{
		public int Id { get; set; }

		public string Note { get; set; }

		public bool Status { get; set; } = false;	

		public DateTime D_ofTic { get; set; } = DateTime.Now;	
		// ticket > Patient 
		public int P_id { get; set; }
		public Patient  patient { get;set; }
        // ticket > Admin 
        public int ?  A_id { get; set; }
      
        public Admin ? admin { get; set; }

		public int D_id { get; set; }	

		public Doctor Doctor { get; set; }	

	}
}
