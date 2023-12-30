using Moneyguard_Xamarin_Sample.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Moneyguard_Xamarin_Sample.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
