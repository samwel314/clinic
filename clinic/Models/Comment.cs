namespace clinic.Models
{
    public class Comment
    {
        public int Id { get; set; }     
        public string Massege  { get; set; }
        public string D_Email { get; set; }  
        public int P_ID { get; set; }   
        public Patient patient { get; set; } = new Patient();  
    }
}
