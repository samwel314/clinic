namespace clinic.Models
{
    public class AdminTicket
    {
        public Ticket Ticket { get; set; } = new Ticket();  

        public string D_Email { get; set; } = string.Empty; 

        public Patient Patient { get; set; } = new Patient();

        public List<string> Emails { get; set; } = new List<string>(); 
    }
}
