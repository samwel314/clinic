using System.Text;
using System.Xml.Linq;
using clinic.Data;
using clinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinic.Controllers
{


    public class AdminController : Controller
    {
        private readonly AppDBContext _dbcontext; 
        public AdminController(AppDBContext dBContext ) 
        { 
           _dbcontext = dBContext;
        }
        public IActionResult Edit(int T_ID)
        {
            var T = _dbcontext.tickets.First(t => t.Id == T_ID);
            return View(T);
        }
        [HttpPost]
        public IActionResult Edit(Ticket ticket )
        {
            if  (ticket.Note == "" || ticket.Note == null)
                ticket.Note = string.Empty; 
            _dbcontext.tickets.Update(ticket);
            _dbcontext.SaveChanges();   
            var data = HttpContext.Session.Get("A_ID");
            int id = Convert.ToInt32(Encoding.UTF8.GetString(data));
            var ad = _dbcontext.admins.First(t => t.Id == id);  
            return RedirectToAction("Show", ad.Id);
        }

        public IActionResult DashBord(Admin admin)
        {
            HttpContext.Session.SetString("A_ID", admin.Id.ToString());
            HttpContext.Session.SetString("Email", admin.Email!);
            HttpContext.Session.SetString("Name", admin.Fname!);
            TempData["name"] = Convert.ToString(Encoding.UTF8.GetString(HttpContext.Session.Get("Name")));

            return View(admin);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(int A_ID)
        {
			
            var data1 = _dbcontext.tickets
                  .Join(_dbcontext.patients
                  , t => t.P_id, p => p.Id, (t, p) =>
                  new TicketsData
                  {
                      DId = t.D_id,
                      Id = t.Id,
                      P_Name = String.Format($"{p.Fname} {p.Lname}"),
                      Note = t.Note,
                      Status = t.Status , 
                      A_id = A_ID 
                      
                  }) .ToList();

            var data = data1.Join
                (_dbcontext.doctors
                , v => v.DId, d => d.Id, delegate
                (TicketsData v, Doctor d)
                {
                    v.D_Name = String.Format($"{d.Fname} {d.Lname}"); 
                    return v;   
                }).ToList();
            if (data.Count == 0)
                data.Add(new TicketsData { A_id = A_ID }); 

			return View(data);  
        }

        public IActionResult actionResult(int A_id) 
        {
            var data = _dbcontext.admins.First(a=>a.Id == A_id);    
            return  RedirectToAction("DashBord", data); 
        }
                    

        public IActionResult Appointment () 
        {
            AdminTicket adminTicket = new AdminTicket();    
            adminTicket.Emails = _dbcontext.doctors.Select(a => a.Email).ToList()!;
            
            return View(adminTicket);  
        }
        [HttpPost]

        public IActionResult Appointment(AdminTicket adminTicket)
        {
            
            var data = HttpContext.Session.Get("A_ID");
            int id = Convert.ToInt32(Encoding.UTF8.GetString(data));
            adminTicket.Patient.Email = String.Empty;
            adminTicket.Patient.Password =
                adminTicket.Patient.Fname.First().
                ToString().ToUpper()+"a0000";
            adminTicket.Patient.Gender = String.Empty;  
                
            _dbcontext.patients.Add(adminTicket.Patient); 
            _dbcontext.SaveChanges();
            var x = _dbcontext.patients.Max(p => p.Id);    
            var p =  _dbcontext.patients.FirstOrDefault(p => p.Id == x);
            p.Email = p.GenerateRandomEmail();

            adminTicket.Ticket.A_id = id; 
            adminTicket.Ticket.P_id = p.Id;
            if (adminTicket.Ticket.Note == "" || adminTicket.Ticket.Note == null)
                adminTicket.Ticket.Note = string.Empty; 


 adminTicket.Ticket.D_id = 
                _dbcontext.doctors.First(d=>d.Email == adminTicket.D_Email).Id;
            _dbcontext.tickets.Add(adminTicket.Ticket); 
            _dbcontext.SaveChanges();
            var admin = _dbcontext.admins.First(a => a.Id == id);
            return RedirectToAction("Show", admin.Id);
        }

		public IActionResult Back()
		{
			var data = HttpContext.Session.Get("A_ID");
			int id = Convert.ToInt32(Encoding.UTF8.GetString(data));
			var admins = _dbcontext.admins.First(d => d.Id == id);
			return RedirectToAction("DashBord", admins);
		}



	}
}
