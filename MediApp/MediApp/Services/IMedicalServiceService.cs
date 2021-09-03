using MediApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediApp.Services
{
    interface IMedicalServiceService
    {
        Task AddMedicalService(int i, string s);
        Task<List<MedicalService>> GetAllMedicalServices();
        Task<MedicalService> GetMedicalService(int id);
    }
}