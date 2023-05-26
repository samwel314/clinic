namespace clinic.Models
{
    public class EmailToContact
    {
        public List <string ? > Emails { get; set; } =new List<string ? >();

        public Contact Contact { get; set; } = new Contact();

        public string D_Email { get; set; } =String.Empty;  

    }
}
