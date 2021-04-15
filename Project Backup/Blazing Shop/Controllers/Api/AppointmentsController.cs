using Blazing_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blazing_Shop.Controllers.api
{
    public class AppointmentsController : ApiController
    {

        private ApplicationDbContext db;
        public AppointmentsController()
        {
            db = new ApplicationDbContext();
        }

        //Get /api/appointmentapi
        public IEnumerable<Appointment> GetAppointments()
        {
            return db.Appointments.Include(m => m.Product).ToList();

            
        }

        public Appointment GetAppointment(int id)
        {
            var app = db.Appointments.Include(c => c.Product).SingleOrDefault(c => c.Id == id);
            if (app == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return app;
        }

        //Post /api/appointmentapi
        [HttpPost]
        public Appointment CreateAppointment(Appointment app)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            db.Appointments.Add(app);
            db.SaveChanges();
            return app;
        }

        //Put /api/customerapi/1
        [HttpPut]
        public void UpdateAppointment(int id, Appointment app)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var appInDb = db.Appointments.Include(c => c.Product).SingleOrDefault(c => c.Id == id);
            if (appInDb == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            appInDb.PersonName = app.PersonName;
            appInDb.Email = app.Email;
            appInDb.PhoneNumber = app.PhoneNumber;
            appInDb.Date = app.Date;
            appInDb.PId = app.PId;

            db.SaveChanges();
           
        }

        //Delete /api/appointmentapi/1

        public void DeleteAppointment(int id)
        {
            
            var appInDb = db.Appointments.SingleOrDefault(c => c.Id == id);
            if (appInDb == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            db.Appointments.Remove(appInDb);
            db.SaveChanges();
            
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }

}

