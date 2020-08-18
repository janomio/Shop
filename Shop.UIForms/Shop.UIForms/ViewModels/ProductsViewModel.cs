namespace Shop.UIForms.ViewModels
{
    using Common.Models;
    using Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private ObservableCollection<Product> products;
        private bool isRefreshing;

        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.IsRefreshing = true;
            this.LoadProducts();
            this.IsRefreshing = false;
        }

        private async void LoadProducts()
        {

            //this.IsRefreshing = true;
            //var response = await this.apiService.GetListAsync<Product>(
            //    "http://192.168.1.101/",
            //    "/ShopWeb/api",
            //    "/Products");

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Product>(
                url,
                "/ShopWeb/api",
                "/Products",
                "bearer",
                MainViewModel.GetInstance().Token.Token);
                        
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                //this.IsRefreshing = false;
                return;
            }

            var myProducts = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(
                myProducts.OrderBy(p => p.Name));
            //this.IsRefreshing = false;
        }

    }
}
