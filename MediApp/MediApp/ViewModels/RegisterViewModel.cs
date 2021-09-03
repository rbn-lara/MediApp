using System;
using System.Threading.Tasks;
using MediApp.Services;
using MediApp.Views;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace MediApp.ViewModels
{
    public class RegisterViewModel : BindableObject
    {
        public int telefono;
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Email { get; set; }
        public string Password { private get; set; }
        public string PasswordAgain { private get; set; }
        public DateTime FechaNacimiento { get; set; }
        public AsyncCommand AddCommand { get; }
        public string Telefono
        {
            get
            {
                return telefono.ToString();
            }
            set
            {
                if (int.TryParse(value, out int result))
                    telefono = result;
                else
                    telefono = 0;
            }
        }
        IPatientService patientService;

        public RegisterViewModel()
        {
            AddCommand = new AsyncCommand(Add);
            patientService = DependencyService.Get<IPatientService>();
        }

        public bool IsRegistrationValid()
        {
            try
            {
                if (!Password.Equals(PasswordAgain))
                {
                    MethodService.AlertInvalidation("La contraseña no coincide");
                    return false;
                }
                if (Usuario != null && Usuario.Length > 0 &&
                    Nombre != null && Nombre.Length > 0 &&
                    PrimerApellido != null && PrimerApellido.Length > 0 &&
                    SegundoApellido != null && SegundoApellido.Length > 0 &&
                    Email != null && Email.Length > 0 &&
                    Password != null && Password.Length > 0)
                {
                    return true;
                }
                else
                {
                    MethodService.AlertInvalidation("Llene los campos para registrarse");
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                MethodService.AlertInvalidation("Llene los campos para registrarse");
                return false;
            }
        }

        public async Task Add()
        {
            if (!await patientService.IsUserValidAsync(Usuario))
            {
                if (IsRegistrationValid())
                {
                    string securePassword = MethodService.GetHash(Password);
                    await patientService.AddPatient(Usuario, Nombre, PrimerApellido, SegundoApellido, Email, securePassword, telefono, FechaNacimiento);
                    await Shell.Current.DisplayAlert("Éxito", "Se ha registrado correctamente, inicie sesión", "Ok");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Aviso", "El Usuario ingresado es inválido o ya existe", "Ok");
            }
        }


    }
}
