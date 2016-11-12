using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventDemo.Data;
using EventDemo.Models;
using EventDemo.Models.EventViewModels;
using EventDemo.Services;

namespace EventDemo.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEventService _service;

        public EventsController(ApplicationDbContext context,IEventService service)
        {
            _context = context;
            _service = service;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Events.Include(x => x.Timetable);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            var model = new EventGeneralViewModel();
           model.TimetablesList = new SelectList(_context.Timetables, "TimetableId", "Description");
            return View(model);
        }


        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventGeneralViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item =  new Event()
                {
                    Id=model.Id,
                    Title = model.Title,
                    TimetableId = model.TimetableId,
                    StartDate = model.StartDate,
                    EndTime = model.EndTime,
                };
                _context.Events.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            model.TimetablesList = new SelectList(_context.Timetables, "TimetableId", "Description", model.TimetableId);
            return View(model);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getEvent =await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
            if (getEvent == null)
            {
                return NotFound();
            }


            var model = new EventGeneralViewModel()
            {
                Id = getEvent.Id,
                Title = getEvent.Title,
                StartDate = getEvent.StartDate,
                EndTime = getEvent.EndTime,
                TimetableId = getEvent.TimetableId
            };
            model.TimetablesList = new SelectList(_context.Timetables, "TimetableId", "Description",getEvent.TimetableId);

            return View(model);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // to solve this issue, so I was binding to viewModel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EventGeneralViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var item = new Event()
                    {
                        Id = model.Id,
                        Title = model.Title,
                        StartDate = model.StartDate,
                        EndTime = model.EndTime,
                        TimetableId = model.TimetableId
                    };
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            
            model.TimetablesList = new SelectList(_context.Timetables, model.Timetable.Description, model.Timetable.Description, model.TimetableId);
            return View(model);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            var model = new EventGeneralViewModel()
            {
                Id = item.Id,
                Title = item.Title,
                TimetableId = item.TimetableId,
                StartDate = item.StartDate,
                EndTime = item.EndTime
            };

            return View(model);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = new EventGeneralViewModel()
            {
                Id = id
            };

            await _service.DeleteEvent(model);
            return RedirectToAction("Index");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
