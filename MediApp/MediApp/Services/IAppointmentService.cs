using MediApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediApp.Services
{
    public interface IAppointmentService
    {
        Task AddAppointment(string usuario, string servicio, DateTime fecha);
        Task<Appointment> AppointmentExist(string usuario, DateTime fecha);
        Task<List<Appointment>> GetAppointmentsOf(string usuario);
    }
}