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
    class AppointmentListViewModel : BaseViewModel
    {
        ObservableRangeCollection<Appointment> appointments = new ObservableRangeCollection<Appointment>();
        public ObservableRangeCollection<Appointment> Appointments { get { return appointments; } }
        public string User { get; set; }
        public AsyncCommand RefreshCommand { get; set; }
        public Xamarin.Forms.Command Clears { get; set; }
        IAppointmentService appointmentService;
        public AppointmentListViewModel()
        {
            RefreshCommand = new AsyncCommand(Refreshing);
            Clears = new Xamarin.Forms.Command(ClearData);
            appointmentService = DependencyService.Get<IAppointmentService>();
        }
        public async Task GettingTable()
        {
            try
            {
                List<Appointment> citas;
                citas = await appointmentService.GetAppointmentsOf(Preferences.Get("usrName", User));
                appointments.Clear();
                appointments.AddRange(citas);
                OnPropertyChanged();
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert(e.GetType().ToString(), e.Message, "ok");
            }
        }
        public async Task Refreshing()
        {
            IsBusy = true;
            await Task.Delay(1000);
            await GettingTable();
            IsBusy = false;
        }
        public void ClearData()
        {
            appointments.Clear();
        }
    }
}
