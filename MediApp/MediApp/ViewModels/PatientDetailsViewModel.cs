using MediApp.Models;
using MediApp.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MediApp.ViewModels
{
    [QueryProperty(nameof(User), nameof(User))]
    public class PatientDetailsViewModel : BaseViewModel
    {
        ObservableRangeCollection<PatientProperty> patientData = new ObservableRangeCollection<PatientProperty>();
        public ObservableRangeCollection<PatientProperty> PatientData { get { return patientData; } }
        public Patient SessionPatient { get; set; }
        public string User { get; set; }
        public AsyncCommand RefreshCommand { get; set; }
        public Xamarin.Forms.Command Clears { get; set; }
        IPatientService patientService;
        public PatientDetailsViewModel()
        {
            RefreshCommand = new AsyncCommand(Refreshing);
            Clears = new Xamarin.Forms.Command(ClearData);
            patientService = DependencyService.Get<IPatientService>();
        }
        public async Task Appears()
        {
            try
            {
                User = Preferences.Get("usrName", User);
                var activePatient = await patientService.GetPatient(User);
                SessionPatient = new Patient(activePatient);
                ClearData();
                patientData.Add(new PatientProperty("Usuario", SessionPatient.Usuario ));
                patientData.Add(new PatientProperty("Nombre", SessionPatient.Nombre ));
                patientData.Add(new PatientProperty("Primer Apellido", SessionPatient.PrimerApellido));
                patientData.Add(new PatientProperty("Segundo Apellido", SessionPatient.SegundoApellido));
                patientData.Add(new PatientProperty("Correo Electrónico", SessionPatient.Email));
                patientData.Add(new PatientProperty("N° Teléfono", SessionPatient.NumeroTelefono.ToString()));
                patientData.Add(new PatientProperty("Fecha de nacimiento", SessionPatient.FechaNacimiento.ToString().Substring(0, 9)));
            }
            catch (NullReferenceException e)
            {
                await Shell.Current.DisplayAlert(e.GetType().ToString(), e.Message, "Ok");
            }
        }

        public async Task Refreshing()
        {
            IsBusy = true;
            await Task.Delay(500);
            await Appears();
            IsBusy = false;
        }

        public void ClearData()
        {
            PatientData.Clear();
        }
    }
}
