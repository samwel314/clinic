namespace clinic.Models
{
    public class TicketToDoctor
    {
        public List<TicketsData> Tickets  { get; set; } = new List<TicketsData>();


        public Doctor Doctor { get; set; } = new Doctor();   
    }
}
