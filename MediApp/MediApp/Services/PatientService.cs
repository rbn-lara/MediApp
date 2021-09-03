using MediApp.Models;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MediApp.Services
{
    public class PatientService : IPatientService
    {
        static SQLiteAsyncConnection db;
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
        public async Task AddPatient(string usuario, string nombre, string primerApellido, string segundoApellido, string email, string password, int telefono, DateTime fecha)
        {
            await Init();
            var patient = new Patient
            {
                Usuario = usuario,
                Nombre = nombre,
                PrimerApellido = primerApellido,
                SegundoApellido = segundoApellido,
                Email = email,
                Password = password,
                NumeroTelefono = telefono,
                FechaNacimiento = fecha
            };

            await db.InsertAsync(patient);
        }

        public async Task<Patient> GetPatient(string usuario)
        {
            await Init();
            try
            {
                return await db.GetAsync<Patient>(usuario);
            }
            catch(InvalidOperationException)
            {
                return null;
            }
        }
        public async Task<bool> IsUserValidAsync(string user)
        {
            var result = await GetPatient(user);
            return result != null;
        }
    }
}
