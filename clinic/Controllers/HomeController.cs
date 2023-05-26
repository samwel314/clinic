using clinic.Data;
using clinic.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace clinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _dbContext;
        private readonly IWebHostEnvironment  _environment; 
        public HomeController(AppDBContext appDBContext , IWebHostEnvironment environment)
        {
            _dbContext = appDBContext;
            _environment = environment; 
        }
        public IActionResult Check()
        {
            var email =  Convert.ToString(Encoding.UTF8.GetString(HttpContext.Session.Get("Email")));
            var ispa = _dbContext.patients.FirstOrDefault(d => d.Email == email);
            if (ispa != null)
                return RedirectToAction("MainPage", "Patient", ispa);

            var isdcotor  = _dbContext.doctors.FirstOrDefault(d=>d.Email == email);
            
            if (isdcotor !=null)
                return RedirectToAction("DashBord" , "Doctor" , isdcotor);

          

            var ad = _dbContext.admins.FirstOrDefault(d => d.Email == email);

            return RedirectToAction("DashBord", "Admin", ad);
        }
        public IActionResult Index()
        {

            if (HttpContext.Session.Get("Email") != null)
                TempData["name"] = Convert.ToString(Encoding.UTF8.GetString(HttpContext.Session.Get("Name")));



            Display display = new Display();
            display.doctors= _dbContext.doctors.ToList(); 
            var data1 = _dbContext
                .comments.
                Join(_dbContext.patients  
                , c=>c.P_ID , p=>p.Id ,
                (c, p)=> new CommentData
                { D_Email =c.D_Email , 
                    Message =c.Massege  ,
                    P_Name = string.Format($"{p.Fname} {p.Lname}")
                } 
                     ).ToList();
            display.comments =
                _dbContext.doctors
                .Join(data1, d => d.Email, c => c.D_Email,

               delegate (Doctor d, CommentData c)
               {
                    c.D_Name = string.Format($"{d.Fname} {d.Lname}");
                    return c;
               }).Take(3).ToList(); 
          
            return View(display);
        }

        public IActionResult login()
        {
            return View();
        }
     
  
        [HttpPost]
        public IActionResult login(User user )
        {
            if (user.Email =="samwel@gmail.com" && user.Password=="Sa123147")
            {
                return RedirectToAction ("Registration" , "Doctor");  
            }

            if (ModelState.IsValid )
            {

                 var isPatient =
                 _dbContext.patients.FirstOrDefault
                 (p => p.Email == user.Email && p.Password == user.Password);

                if (isPatient != null)
                {
                    HttpContext.Session.SetString("Email", isPatient.Email!);
                    HttpContext.Session.SetString("Name", isPatient.Fname!);
                    TempData["name"] = isPatient.Fname!;    
                    return RedirectToAction("MainPage", "Patient", isPatient); 
                }
                var isDoctor =
                _dbContext.doctors.FirstOrDefault
                (p => p.Email == user.Email && p.Password == user.Password);
                if (isDoctor != null)
                {
                    HttpContext.Session.SetString("Email", isDoctor.Email!);
                    HttpContext.Session.SetString("Name", isDoctor.Fname!);
                    TempData["name"] = isDoctor.Fname!;
                    return RedirectToAction("DashBord", "Doctor", isDoctor);     
                }
                var isAdmin =
                _dbContext.admins.FirstOrDefault
                (p => p.Email == user.Email && p.Password == user.Password);
                if (isAdmin != null)
                {
                    HttpContext.Session.SetString("Email", isAdmin.Email!);
                    HttpContext.Session.SetString("Name", isAdmin.Fname!);
                    TempData["name"] = isAdmin.Fname!;
                    return RedirectToAction("DashBord", "Admin", isAdmin);
                }
                ModelState.AddModelError("Email", "The Email or Password not Right  ");
                
            }

            return View();
        }
   
        public IActionResult Comment ()
        {

            Display display = new Display();
            display.doctors = _dbContext.doctors.ToList();
            var data1 = _dbContext
                .comments.
                Join(_dbContext.patients
                , c => c.P_ID, p => p.Id,
                (c, p) => new CommentData
                {
                    D_Email = c.D_Email,
                    Message = c.Massege,
                    P_Name = string.Format($"{p.Fname} {p.Lname}")
                }
                     ).ToList();
            display.comments =
                _dbContext.doctors
                .Join(data1, d => d.Email, c => c.D_Email,

               delegate (Doctor d, CommentData c)
               {
                   c.D_Name = string.Format($"{d.Fname} {d.Lname}");
                   return c;
               }).ToList();
            if (HttpContext.Session.Get("Name") != null)
                TempData["name"] = Convert.ToString(Encoding.UTF8.GetString(HttpContext.Session.Get("Name")));


            return View(display);  
        }


        [HttpPost]
        public IActionResult Comment(Display display )
        {
            display.doctors = _dbContext.doctors.ToList();
            var data1 = _dbContext
                .comments.
                Join(_dbContext.patients
                , c => c.P_ID, p => p.Id,
                (c, p) => new CommentData
                {
                    D_Email = c.D_Email,
                    Message = c.Massege,
                    P_Name = string.Format($"{p.Fname} {p.Lname}")
                }
                     ).ToList();
            display.comments =
                _dbContext.doctors
                .Join(data1, d => d.Email, c => c.D_Email,

               delegate (Doctor d, CommentData c)
               {
                   c.D_Name = string.Format($"{d.Fname} {d.Lname}");
                   return c;
               }).ToList();
           var he =  _dbContext.patients
                .FirstOrDefault(p => p.Email == display.Email); 
            if (he == null)
            {
                ModelState.AddModelError("Email", "This Email Not Found Please Sign in ");
            }
            else
            {

                var c = new Comment
                {
                    D_Email = display.D_Email,
                    P_ID = he.Id,
                    Massege = display.Comment
                    ,patient = he 
                }; 

                _dbContext.comments.Add(c ); 
                _dbContext.SaveChanges();
                return RedirectToAction ("Thanks" , "Doctor"
                    , new Contact { Name = string.Format($"{he.Fname} {he.Lname}") });
                
            }
            if (HttpContext.Session.Get("Name") != null)
                TempData["name"] = Convert.ToString(Encoding.UTF8.GetString(HttpContext.Session.Get("Name")));

            return View(display);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Name");
            return RedirectToAction("Index"); 
        }
    }
}