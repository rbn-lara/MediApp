using MediApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediApp.Services
{
    public interface IPatientService
    {
        Task AddPatient(string usuario, string nombre, string primerApellido, string segundoApellido, string email, string password, int telefono, DateTime fecha);
        Task<Patient> GetPatient(string usuario);
        Task<bool> IsUserValidAsync(string user);
    }
}
