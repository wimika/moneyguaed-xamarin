using Moneyguard_Xamarin_Sample.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Moneyguard_Xamarin_Sample.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}