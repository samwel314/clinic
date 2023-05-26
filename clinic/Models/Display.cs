using System.ComponentModel.DataAnnotations;

namespace clinic.Models
{
    public class Display
    {
        public List<Doctor> doctors { get; set;  } 

        public List<CommentData>  comments  { get; set; }

       public string Comment { get; set; }  = string.Empty; 
        public string Email { get; set; } = string.Empty;

        public string D_Email { get; set; } = string.Empty;

    }
}
