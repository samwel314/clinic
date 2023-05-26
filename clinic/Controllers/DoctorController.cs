using System.Text;
using clinic.Data;
using clinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinic.Controllers
{
 
      public class DoctorController : Controller
    {
        private readonly AppDBContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public DoctorController(AppDBContext dBContext, IWebHostEnvironment environment)
        {
            _dbContext = dBContext;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contact()
        {
            EmailToContact emailToContact = new EmailToContact();
            emailToContact.Emails = _dbContext.doctors.Select(p => p.Email).ToList();
          if (HttpContext.Session.Get("Name")!=null)
            TempData["name"] = Convert.ToString(Encoding.UTF8.GetString(HttpContext.Session.Get("Name")));

            return View(emailToContact);
        }
        [HttpPost]
        public IActionResult Contact(EmailToContact emailToContact)
        {
            if (ModelState.IsValid)
            {
                emailToContact.Contact.D_id =
                     _dbContext.doctors.First(d => d.Email == emailToContact.D_Email).Id;
                _dbContext.contacts.Add(emailToContact.Contact);
                _dbContext.SaveChanges();
                return RedirectToAction("Thanks", emailToContact.Contact);
            }


            emailToContact.Emails = _dbContext.doctors.Select(p => p.Email).ToList();
            if (HttpContext.Session.Get("Name") != null)
                TempData["name"] = Convert.ToString(Encoding.UTF8.GetString(HttpContext.Session.Get("Name")));

            return View("contact", emailToContact);
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(Doctor doctor, IFormFile file)
        {
            if (ValidData.CheckEmailPhoen(doctor.Email, doctor.Phone, _dbContext) || doctor.Email == "samwel@gmail.com")
            {
                ModelState.AddModelError("Email", "Email Or Phone Is Used Before  ");
            }
            if (doctor.Phone  == null || doctor.Phone.Length !=11 )
            {
                ModelState.AddModelError("Phone", " Phone must be 11 Number  ");
            }
            if (ModelState.IsValid)
            {
                string path = Path.Combine(_environment.WebRootPath, "img");
                if (file != null)
                {
                    path = Path.Combine(path, file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                doctor.Dr_picture = file.FileName;
                _dbContext.doctors.Add(doctor);
                _dbContext.SaveChanges();
                return RedirectToAction("DashBord" , doctor);
            }
            return View();
        }
        public IActionResult Thanks(Contact contact)
        {
            return View(contact);
        }

        public IActionResult MyTickets (int D_ID)
        {
            TicketToDoctor ticketToDoctor = new TicketToDoctor();
            ticketToDoctor.Tickets = _dbContext.tickets
                .Where(t => t.D_id == D_ID)
                .ToList().Join(_dbContext.patients,
                t => t.P_id, p => p.Id,
                (t, p) => new TicketsData
                { Id = t.Id,
                    P_Name = String.Format($"{p.Fname} {p.Lname}"),
                    Note = t.Note == "" ? "No Note" : t.Note,
                    Status = t.Status
                }).ToList(); 
         
            ticketToDoctor.Doctor = _dbContext.doctors.First(d => d.Id == D_ID); 
            
            return View(ticketToDoctor); 
        }
        public   IActionResult actionResult(int D_ID)
        {
            var doctor = _dbContext.doctors.First(d => d.Id == D_ID);
            return RedirectToAction("DashBord", doctor); 
        }
        public IActionResult DashBord (Doctor doctor)
        {

            HttpContext.Session.SetString("Name", doctor.Fname.ToString());
            HttpContext.Session.SetString("Email", doctor.Email.ToString());

            HttpContext.Session.SetString("D_ID", doctor.Id.ToString());
            TempData["name"] = Convert.ToString(Encoding.UTF8.GetString(HttpContext.Session.Get("Name")));

            return View(doctor); 
        }

        public IActionResult Back ()
        {
            var data = HttpContext.Session.Get("D_ID");
            int id = Convert.ToInt32(Encoding.UTF8.GetString(data));
            var doctor  = _dbContext.doctors.First (d => d.Id == id);   
            return  RedirectToAction ("DashBord", doctor);  
        }
        public IActionResult AddAdmin (int D_ID )
        {
           
            return View(); 
        }
        [HttpPost]
        public IActionResult AddAdmin(Admin admin )
        {
            var data = HttpContext.Session.Get("D_ID");
            int id = Convert.ToInt32(Encoding.UTF8.GetString(data));
            admin.D_id = id;    
            if (ValidData.CheckEmailPhoen(admin.Email , admin.Phone , _dbContext ) || admin.Email == "samwel@gmail.com")
            {
                ModelState.AddModelError("Email", "Email or Phone Is Used Before "); 
            }

            if (ModelState.IsValid) 
            {
                _dbContext.admins.Add(admin);
                var doctor = _dbContext.doctors.First(d=>d.Id == id);
                _dbContext.SaveChanges();
                return RedirectToAction("DashBord", doctor); 
            }
            return View();
        }

        public IActionResult Messages (int D_ID) 
        {
            var data = _dbContext.contacts.Where(d=>d.D_id == D_ID).ToList();   

            return View(data); 
        }
    }
}
