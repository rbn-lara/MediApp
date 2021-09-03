using System;
using System.Collections.Generic;
using System.Text;

namespace MediApp.Models
{
    public class PatientProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public PatientProperty(string N, string V)
        {
            Name = N;
            Value = V;
        }

    }
}
