using MediApp.Models;
using MediApp.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediApp.ViewModels
{
    public class MedicalServiceViewModel : BaseViewModel
    {
        ObservableRangeCollection<MedicalService> medicalServices = new ObservableRangeCollection<MedicalService>();
        public ObservableRangeCollection<MedicalService> MedicalServices { get { return medicalServices; } }
        public AsyncCommand VerServicios { get; set; }
        public AsyncCommand IngresarServicio { get; set; }
        public AsyncCommand RefreshCommand { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        IMedicalServiceService medicalServiceService;
        public MedicalServiceViewModel()
        {
            VerServicios = new AsyncCommand(GettingTable);
            IngresarServicio = new AsyncCommand(AddingService);
            RefreshCommand = new AsyncCommand(Refreshing);
            medicalServiceService = DependencyService.Get<IMedicalServiceService>();
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
            catch(Exception e)
            {
                await Shell.Current.DisplayAlert(e.GetType().ToString(), e.Message, "ok");
            }

        }
        public async Task AddingService()
        {
            try
            {
                if(!Name.Trim().Equals(""))
                {
                    await medicalServiceService.AddMedicalService(int.Parse(Id), Name);
                    await Shell.Current.DisplayAlert("Éxito", $"Servicio '{Name}' ingresado correctamente", "Ok");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Aviso", "Llene los campos antes de ingresar servicio", "Ok");
                }
            }
            catch (NullReferenceException)
            {
                await Shell.Current.DisplayAlert("Aviso", "Llene los campos antes de ingresar servicio", "Ok");
            }
            catch (ArgumentNullException)
            {
                await Shell.Current.DisplayAlert("Aviso", "Llene los campos antes de ingresar servicio", "Ok");
            }
            catch (SQLiteException e)
            {
                if (e.Message.Contains("UNIQUE"))
                {
                    await Shell.Current.DisplayAlert("Aviso", "El código ya existe", "Ok");
                }
                else
                {
                    await Shell.Current.DisplayAlert(e.GetType().ToString(), e.Message, "ok");
                }

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
    }
}
