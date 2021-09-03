using MediApp.Models;
using MediApp.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MediApp.ViewModels
{
    public class AppointmentViewModel : BaseViewModel
    {
        ObservableRangeCollection<MedicalService> medicalServices = new ObservableRangeCollection<MedicalService>();

        public ObservableRangeCollection<MedicalService> MedicalServices { get { return medicalServices; } }

        public TimeSpan Hora { get; set; }
        public DateTime Hoy { get { return DateTime.Today; } }
        public DateTime Fecha { get; set; }
        public string User { get; set; }
        public MedicalService Service { get; set; }
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand GetTableCommand { get; set; }
        public AsyncCommand AddAppointment { get; set; }
        IMedicalServiceService medicalServiceService;
        IAppointmentService appointmentService;
        public AppointmentViewModel()
        {
            RefreshCommand = new AsyncCommand(Refreshing);
            GetTableCommand = new AsyncCommand(GettingTable);
            AddAppointment = new AsyncCommand(AddCita);
            medicalServiceService = DependencyService.Get<IMedicalServiceService>();
            appointmentService = DependencyService.Get<IAppointmentService>();
        }
        public async Task Refreshing()
        {
            IsBusy = true;
            await Task.Delay(1000);
            await GettingTable();
            IsBusy = false;
        }

        public async Task GettingTable()
        {
            try
            {
                List<MedicalService> services;
                services = await medicalServiceService.GetAllMedicalServices();
                medicalServices.Clear();
                medicalServices.AddRange(services);
                OnPropertyChanged();
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert(e.GetType().ToString(), e.Message, "ok");
            }
        }

        public DateTime DiaHora()
        {
            return Fecha.Date.Add(new TimeSpan(Hora.Hours, 0, 0));
        }
        public async Task AddCita()
        {
            try
            {
                User = Preferences.Get("usrName", User);
                var result = await Shell.Current.DisplayAlert($"Confirma tu cita {User}", $"{Service.Name} el día {DiaHora()}", "Ok", "Cancelar");
                if (result)
                {
                    await appointmentService.AddAppointment(User, Service.Name, DiaHora());
                    await Shell.Current.DisplayAlert("Éxito", "Cita ingresada exitósamente", "Ok");
                    Service = null;
                }
            }
            catch (SQLite.SQLiteException)
            {
                await Shell.Current.DisplayAlert("Error", "proceso bloqueado", "Ok");
            }
            catch (NullReferenceException)
            {
                await Shell.Current.DisplayAlert("Error", "Seleccione un servicio", "Ok");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert(e.GetType().ToString(), e.Message, "Ok");
            }
        }

    }
}
