using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventDemo.Models.EventViewModels
{
    public class EventGeneralViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        public int TimetableId { get; set; }
        public Timetable Timetable { get; set; }

        public SelectList TimetablesList { get; set; }
        public string Description { get; internal set; }
        public bool IsFullDay { get; set; }
    }
}
