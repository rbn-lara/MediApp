using MediApp.Models;
using MediApp.Services;
using MediApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientDetailsPage : ContentPage
    {
        public PatientDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewContent.Command.Execute(null);
        }

        private void ContentPage_Disappearing(object sender, EventArgs e)
        {
            btnClear.Command.Execute(null);
        }
    }
}
