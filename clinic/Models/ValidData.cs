using clinic.Data;

namespace clinic.Models
{
    public static  class ValidData
    { 
        public static bool  CheckEmailPhoen (string Emaill , string Phone , AppDBContext _db)
        {
            var InPatients =
                _db.patients.FirstOrDefault(p=> p.Email == Emaill || p.Phone == Phone);
            var InDoctor =
                   _db.doctors.FirstOrDefault(p => p.Email == Emaill || p.Phone == Phone);
          var InAddmin =
                _db.admins.FirstOrDefault(p => p.Email == Emaill || p.Phone == Phone);
            
           
            
            
            if (InPatients != null || InDoctor != null || InAddmin !=null) 
            {
                return true;
            }
            return false ; 
        }

     
    }
}
