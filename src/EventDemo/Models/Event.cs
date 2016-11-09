using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventDemo.Models
{
    public class Event
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
    }
}

