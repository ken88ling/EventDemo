using System.Threading.Tasks;
using EventDemo.Models.EventViewModels;

namespace EventDemo.Services
{
    public interface IEventService
    {
        Task DeleteEvent(EventGeneralViewModel model);
    }
}