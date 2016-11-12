using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventDemo.Data;
using EventDemo.Models.EventViewModels;
using Microsoft.EntityFrameworkCore;

namespace EventDemo.Services
{
   
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task DeleteEvent(EventGeneralViewModel model)
        {
            var item = await _context.Events.FirstOrDefaultAsync(x => x.Id == model.Id);
            _context.Events.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
