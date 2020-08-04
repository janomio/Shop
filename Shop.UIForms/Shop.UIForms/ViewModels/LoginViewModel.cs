namespace Shop.UIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Shop.UIForms.Views;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel 
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        public LoginViewModel()
        {
            this.Email = "janomio_11@hotmail.com";
            this.Password = "123";
        }

        private async void Login()
        {
            if(string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Favor ingresar el Email", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Favor ingresar el Password", "Aceptar");
                return;
            }

            if(!this.Email.Equals("janomio_11@hotmail.com") || !this.Password.Equals("123"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Tu email o contraseña son incorrectos", "Aceptar");
                return;
            }

            //await Application.Current.MainPage.DisplayAlert("Error", "OKI", "Aceptar");

            MainViewModel.GetInstance().Products = new ProductsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());

        }
    }
}
