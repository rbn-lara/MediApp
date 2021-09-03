using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediApp.Models
{
    public class MedicalService
    {
        [PrimaryKey]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }

        public MedicalService()
        {

        }
    }
}
