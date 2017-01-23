using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App11Athletics.Helpers;
using App11Athletics.Models;
using App11Athletics.Templates;
using App11Athletics.ViewModels;
using Xamarin.Forms;
using static App11Athletics.Models.UserProfileModel;

namespace App11Athletics.Views
{
    public partial class UserProfileView : ContentPage
    {
        public UserProfileView()
        {
            InitializeComponent();
            PageLabels = new List<Label>();

            PageLabels.AddRange(new[]
            {
                labelAge, labelGender, labelHeightFt, labelHeightInMark, labelHeightInch, labelHeightftMark,
                labelWeight, labelActivityLevel, labelBmr, labelDce
            });
            gridMain.BindingContext = new UserProfileModel();


        }

        public string GivenName => Settings.UserGivenName;

        public static string st;

        #region Overrides of Page

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        #endregion

        public List<Label> PageLabels { get; set; }

        public ICommand labelchange { get; set; }

        public double DFontSize { get; private set; }

        private void UserProfileView_OnSizeChanged(object sender, EventArgs e)
        {
            if (PageLabels == null)
                return;
            DFontSize = gridGender.Width / 3;
            var n = gridHeader.Width * 0.5 / 7;
            labelName.FontSize = n;
            foreach (var label in PageLabels)
            {
                label.FontSize = DFontSize;
            }
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            //            await Navigation.PushModalAsync(new NavigationPage(new AuthUserSignIn()));
            await DependencyService.Get<IAuthSignIn>().AuthRefresh();
        }

        private async void Logout_OnClicked(object sender, EventArgs e)
        {
            await DependencyService.Get<IAuthSignIn>().AuthLogOut();
        }
    }
}