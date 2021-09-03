using MediApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: Dependency(typeof(AppointmentService))]
[assembly: Dependency(typeof(PatientService))]
[assembly: Dependency(typeof(MedicalServiceService))]