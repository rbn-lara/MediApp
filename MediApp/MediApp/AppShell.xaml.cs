using MediApp.Views;
using System;
using Xamarin.Forms;

namespace MediApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public string User { get; set; }
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute($"{nameof(LoginPage)}", typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(AppointmentPage), typeof(AppointmentPage));
            Routing.RegisterRoute(nameof(PatientDetailsPage), typeof(PatientDetailsPage));
            Routing.RegisterRoute(nameof(MedicalServicePage), typeof(MedicalServicePage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
