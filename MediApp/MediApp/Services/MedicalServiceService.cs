using MediApp.Models;
using MediApp.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediApp.Services
{
    class MedicalServiceService : IMedicalServiceService
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

        public async Task AddMedicalService(int i, string s)
        {
            await Init();
            var service = new MedicalService
            {
                Id = i,
                Name = s
            };

            await db.InsertAsync(service);
        }
        public async Task<List<MedicalService>> GetAllMedicalServices()
        {
            await Init();
            try
            {
                return await db.Table<MedicalService>().ToListAsync();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
        public async Task<MedicalService> GetMedicalService(int id)
        {
            await Init();
            try
            {
                return await db.GetAsync<MedicalService>(id);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

    }
}
