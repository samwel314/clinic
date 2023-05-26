namespace clinic.Models
{
    public class Ticket_Doctor_Patient
    {
        public  EmailToContact EmailToContact { get; set; }    = new EmailToContact();
        public Ticket Ticket { get; set; } = new Ticket();
        public Patient Patient { get; set; }  = new Patient();  
    }
}
