using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11Athletics.ViewModels;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class HomeMenuView : ContentPage
    {
        public HomeMenuView()
        {
            InitializeComponent();
            carousel.BindingContext = new CarouselViewModel();
        }
    }
}
