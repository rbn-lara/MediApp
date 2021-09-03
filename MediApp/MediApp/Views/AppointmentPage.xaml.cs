using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentPage : ContentPage
    {
        public AppointmentPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RfrshView.Command.Execute(null);
        }
    }
}