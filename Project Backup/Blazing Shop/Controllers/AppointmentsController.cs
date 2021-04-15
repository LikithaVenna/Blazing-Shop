using Blazing_Shop.Models;
using Blazing_Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blazing_Shop.Controllers
{
    public class AppointmentsController : Controller
    {

        public ApplicationDbContext _context;
        public AppointmentsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Appointments
        public ActionResult Index()
        {
            var apt = _context.Appointments.Include(p => p.Product).ToList();

            return View(apt);
        }

        public ActionResult Details(int id)
        {
            var apt = _context.Appointments.Include(p=>p.Product).SingleOrDefault(a => a.Id == id);
            return View(apt);
        }

        public ActionResult Create()
        {
            var product = _context.Products.ToList();
            var vm = new NewAppointmentViewModel
            {
                Products=product
            };
            return View(vm);
        }


        public ActionResult Save(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                var vm = new NewAppointmentViewModel(appointment)
                {
                    Products = _context.Products.ToList()
                };
                return View("Create", vm);
            }

            if (appointment.Id==0)
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return RedirectToAction("Done", "Appointments");
            }
            else
            {
                var aptInDB = _context.Appointments.Single(a => a.Id == appointment.Id);
                aptInDB.PersonName = appointment.PersonName;
                aptInDB.PhoneNumber = appointment.PhoneNumber;
                aptInDB.Email = appointment.Email;
                aptInDB.Date = appointment.Date;
                aptInDB.IsConfirmed = appointment.IsConfirmed;
                aptInDB.PId = appointment.PId;
                _context.SaveChanges();
                return RedirectToAction("Index", "Appointments");
            }
            
            
           
        }


        public ActionResult Edit(int id)
        {
            var apt = _context.Appointments.SingleOrDefault(a => a.Id == id);
            if (apt == null)
                return HttpNotFound();
            var vm = new NewAppointmentViewModel(apt)
            {
                Products = _context.Products.ToList()
            };
            return View("Create", vm);

        }



        public ActionResult Delete(int id)
        {

            var apt = _context.Appointments.Find(id);
            if (apt== null)
            {
                return HttpNotFound();
            }
            return View(apt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var apt= _context.Appointments.Find(id);
            _context.Appointments.Remove(apt);
            _context.SaveChanges();
            return RedirectToAction("Index", "Appointments");
        }

        public ActionResult Done()
        {

            ViewBag.Message = " ";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}