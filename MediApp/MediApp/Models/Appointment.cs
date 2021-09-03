using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediApp.Models
{
    public class Appointment
    {
        [PrimaryKey]
        public string UserAndDate { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Service { get; set; }
        public Appointment()
        {

        }
    }
}
