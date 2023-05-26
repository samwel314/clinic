using System.Text;
using clinic.Data;
using clinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinic.Controllers
{
    
    public class PatientController : Controller
    {
        private readonly AppDBContext _dbContext;
       
        public PatientController(AppDBContext dBContext ) 
        {
            _dbContext = dBContext; 
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult sign()
        {
            return View();
        }
        [HttpPost]
        public IActionResult sign(Patient patient)
        {
            if (patient.Password != patient.Confirm_Password)
            {
                ModelState.AddModelError("Password", "Password And Confirm Must Be The Same ");
            }

            if ( patient.Phone== null ||  patient.Phone.Length !=11   )
            {
                ModelState.AddModelError("Phone", "Phone Should be 11 Number ");
            }
            if (ValidData.CheckEmailPhoen(patient.Email! , patient.Phone! , _dbContext) || patient.Email == "samwel@gmail.com")
            {
                ModelState.AddModelError("Email", "Email Or Phone Is Used Before  ");
            }

            if (ModelState.IsValid)
            {
                _dbContext.patients.Add(patient);
                _dbContext.SaveChanges();
                return RedirectToAction("MainPage", patient);
            }

            return View();
        }

        public IActionResult MainPage(Patient patient)
        {
            HttpContext.Session.SetString("P_id", patient.Id.ToString());
            HttpContext.Session.SetString("Email", patient.Email!.ToString());
            HttpContext.Session.SetString("Name", patient.Fname!.ToString());
            TempData["name"] = Convert.ToString(Encoding.UTF8.GetString(HttpContext.Session.Get("Name")));

            return View(patient);
        }

        public IActionResult Ticket (int P_ID )
        {
            Ticket_Doctor_Patient ticket_Doctor_Patient = new Ticket_Doctor_Patient();
            ticket_Doctor_Patient.Patient.Id = P_ID;
            ticket_Doctor_Patient.EmailToContact.Emails
                = _dbContext.doctors.Select
                (x => x.Email).ToList();
                 
            return View(ticket_Doctor_Patient);  
        }
        [HttpPost]
        public IActionResult Ticket (Ticket_Doctor_Patient ticket_Doctor_Patient)
        {
            var data = HttpContext.Session.Get("P_id");
            int id = Convert.ToInt32(Encoding.UTF8.GetString(data)); 
            ticket_Doctor_Patient.Ticket.D_id = _dbContext.doctors.
                FirstOrDefault
                (p => p.Email == ticket_Doctor_Patient.EmailToContact.D_Email)!.Id; 
            ticket_Doctor_Patient.Ticket.A_id = null;
            ticket_Doctor_Patient.Ticket.P_id = id; 
           
            if (ticket_Doctor_Patient.Ticket.Note == null)
                  ticket_Doctor_Patient.Ticket.Note = String.Empty;
            
            ticket_Doctor_Patient.Patient = _dbContext.patients.FirstOrDefault(p => p.Id == id)!;
            _dbContext.tickets.Add(ticket_Doctor_Patient.Ticket);
            _dbContext.SaveChanges(); 
            return RedirectToAction("MainPage", ticket_Doctor_Patient.Patient);
        }

        public IActionResult actionResult(int P_ID)
        {
            var patient = _dbContext.patients.First(d => d.Id == P_ID);
            return RedirectToAction("MainPage", patient);
        }
        public IActionResult MyTickets (int P_ID)
        {
            var tickets =
                _dbContext.tickets.Where(t=>t.P_id == P_ID ).ToList();
     
            if (tickets.Count == 0)
            {
                tickets.Add(new Ticket { P_id = P_ID }); 
            }

            return View(tickets);  
        }
        public IActionResult Back()
        {
            var data = HttpContext.Session.Get("P_id");
            int id = Convert.ToInt32(Encoding.UTF8.GetString(data));
            var pa = _dbContext.patients.First(d => d.Id == id);
            return RedirectToAction("MainPage", pa);
        }
    }
}
