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
    public partial class AppointmentListPage : ContentPage
    {
        public AppointmentListPage()
        {
            InitializeComponent();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            btnClear.Command.Execute(null);
        }

    }
}