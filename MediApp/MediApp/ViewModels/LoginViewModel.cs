using MediApp.Services;
using MediApp.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MediApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginValid { get; }
        public ICommand GoToRegistration { get; }
        private string user;
        private string password;
        IPatientService patientService;
        
        public LoginViewModel()
        {
            LoginValid = new AsyncCommand(OnLogin);
            GoToRegistration = new AsyncCommand(OnRegistration);
            patientService = DependencyService.Get<IPatientService>();
        }

        public string User
        {
            set => user = value;
        }
        public string Password
        {
            set => password = value;
        }

        private async Task OnLogin()
        {
            try
            {
                if (await patientService.IsUserValidAsync(user))
                {
                    var pacienteLogin = await patientService.GetPatient(user);
                    string hashedString = MethodService.GetHash(password);
                    if (pacienteLogin.Usuario.Equals(user) && pacienteLogin.Password.Equals(hashedString))
                    {
                        Preferences.Set("usrName", user);
                        await Shell.Current.GoToAsync($"//{nameof(PatientDetailsPage)}?User={user}");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Aviso", "Usuario o contraseña incorrecta", "Cancelar");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Aviso", "Usuario o contraseña incorrecta", "Cancelar");
                }
            }
            catch(Exception e)
            {
                await Shell.Current.DisplayAlert(e.GetType().ToString(), e.Message.ToString() + e.StackTrace, "Salir");
            }

        }

        private async Task OnRegistration()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}/{nameof(RegisterPage)}");
        }
    }

}
