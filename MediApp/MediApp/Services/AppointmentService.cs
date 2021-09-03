using MediApp.Models;
using MediApp.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediApp.Services
{
    public class AppointmentService : IAppointmentService
    {
        SQLiteAsyncConnection db;
        async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<MedicalService>();
            await db.CreateTableAsync<Patient>();
            await db.CreateTableAsync<Appointment>();
        }
        public async Task AddAppointment(string usuario, string servicio, DateTime fecha)
        {
            await Init();
            var service = new Appointment
            {
                UserAndDate = usuario + fecha.ToString(),
                User = usuario,
                Date = fecha,
                Service = servicio
            };

            await db.InsertAsync(service);
        }
        public async Task<List<Appointment>> GetAppointmentsOf(string usuario)
        {
            await Init();
            try
            {
                return await db.Table<Appointment>().Where(u => u.User == usuario).ToListAsync();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
        public async Task<Appointment> AppointmentExist(string usuario, DateTime fecha)
        {
            await Init();
            try
            {
                return await db.GetAsync<Appointment>(u => u.UserAndDate == usuario + fecha.ToString());
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }

}
