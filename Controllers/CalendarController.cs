using Calendar.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Calendar.Controllers
{
    public class CalendarController : Controller
    {
        public static List<Event> events = new List<Event>();
        private readonly CalendarContext _context;

        public CalendarController(CalendarContext context)
        {
            _context = context;
        }


        public async  Task<IActionResult> Index()
        {
            //ViewBag.Events = events;
            //return View();
            var events = await _context.Events.ToListAsync();
            return View(events);
        }

        /* To create event */
        public async Task<IActionResult> Create()
        {
            
            return View();
        }

        [HttpPost]

        public IActionResult Create(Event newEvent)
        {
            if (ModelState.IsValid)
            {
                /*  newEvent.Id = events.Count + 1; */     // Generate a unique ID for the event.
                /*                events.Add(newEvent);*/                  // Add the event to the list.
                /* return RedirectToAction("Index"); */           // Redirect to the calendar after adding the event.

                _context.Events.Add(newEvent);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // If ModelState is not valid, redisplay the form with validation errors.
            return View(newEvent);
        }
/*
        To send events to screen*//**/
        [HttpGet]
        public IActionResult GetEvents()
        {

            /* // Query the events from the database
             var events = _context.Events.ToList();

             // Convert the events to the view model and select the necessary properties
             var calendarEvents = events.Select(e => new CalendarEventViewModel
             {
                 Title = e.Title,
                 Start = e.Start.ToString("yyyy-MM-ddTHH:mm:ss"), // Ensure the correct date format
                 End = e.End.ToString("yyyy-MM-ddTHH:mm:ss"),
                 // Optionally, you can include other properties such as Url
             });

             return Json(calendarEvents);
         }*/





            var events = _context.Events.ToList();
            var calendarEvents = events.Select(e => new CalendarEventViewModel
            
            {
                Title = e.Title,
                Start = e.Date,
                End = e.Date,
               /* Url = Url.Action("Details", new { id = e.Id })*/
            });

            return Json(calendarEvents);

        }
      /*  To edit and delete event*/

        [HttpGet]
        public IActionResult Edit(int id)
        {
            /* var existingEvent = events.FirstOrDefault(e => e.Id == id);

             if (existingEvent == null)
             {
                 return NotFound();
             }

             return View(existingEvent);*/


            var existingEvent = _context.Events.Find(id);

            if (existingEvent == null)
            {
                return NotFound();
            }

            return View(existingEvent);
        }

        [HttpPost]
        public IActionResult Edit(Event updatedEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(updatedEvent).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(updatedEvent);


            /* if (ModelState.IsValid)
             {
                 var existingEvent = events.FirstOrDefault(e => e.Id == updatedEvent.Id);

                 if (existingEvent == null)
                 {
                     return NotFound();
                 }

                 existingEvent.Title = updatedEvent.Title;
                 existingEvent.Date = updatedEvent.Date;
                 existingEvent.Description = updatedEvent.Description;

                 return RedirectToAction("Index");
             }

             return View(updatedEvent);*/



        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            /*var existingEvent = events.FirstOrDefault(e => e.Id == id);*/

            var existingEvent = _context.Events.Find(id);

            if (existingEvent == null)
            {
                return NotFound();
            }

            return View(existingEvent);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            /*ar existingEvent = events.FirstOrDefault(e => e.Id == id);

            if (existingEvent != null)
            {
                events.Remove(existingEvent);
            }

            return RedirectToAction("Index");*/



                var existingEvent = _context.Events.Find(id);

            if (existingEvent != null)
            {
                _context.Events.Remove(existingEvent);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }



        /*To view event and delete and edit it.*/
        public IActionResult ViewEvent()
        {
            /*ViewBag.Events = _context.events;

            return View();*/

            var events = _context.Events.ToList(); // Query events from the database
            ViewBag.Events = events;
            return View(events);


            

        }
    }
}
