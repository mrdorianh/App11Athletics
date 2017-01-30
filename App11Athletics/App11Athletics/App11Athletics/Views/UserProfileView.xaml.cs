using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App11Athletics.Helpers;
using App11Athletics.Models;
using App11Athletics.Models;
using App11Athletics.Templates;
using App11Athletics.DHCToolkit;
using App11Athletics.ViewModels;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class UserProfileView : ContentPage
    {
        public UserProfileView()
        {
            InitializeComponent();
            PageLabels = new List<Label>();
            BigGridLabels = new List<Label>();
            PageLabels.AddRange(new[]
            {
                labelAge, labelGender, labelHeightFt, labelHeightInMark, labelHeightInch, labelHeightftMark,
                labelWeight
            });
            BigGridLabels.AddRange(new[]
            {
                labelActivityLevel, labelBmr, labelDce
            });
            gridMain.BindingContext = new UserProfileModel();
            changeOptions.TranslationY = 1500;
            ActivityLevel = Settings.UserAlfString;
            labelWeight.Text = Settings.UserWeight;
            labelAge.Text = Settings.UserAge;
            labelHeightFt.Text = Settings.UserHeightFt;
            labelHeightInch.Text = Settings.UserHeightIn;
            var g = Settings.UserGender;
            if (g == "male")
            {
                labelGender.Text = "M";
            }
            else if (g == "female")
            {
                labelGender.Text = "F";
            }

        }

        public string GivenName => Settings.UserGivenName;
        public string HeightFt { get; set; }
        public string HeightIn { get; set; }
        public string ActivityLevel { get; set; }



        public static string st;

        #region Overrides of Page

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AnimatePages.AnimatePageIn(gridMain);

        }

        #endregion

        public List<Label> PageLabels { get; set; }
        public List<Label> BigGridLabels { get; set; }

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
            foreach (var label in BigGridLabels)
            {
                label.FontSize = DFontSize / 1.5;
            }
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            // await Navigation.PushModalAsync(new NavigationPage(new AuthUserSignIn()));
            await DependencyService.Get<IAuthSignIn>().AuthRefresh();
        }

        private async void Logout_OnClicked(object sender, EventArgs e)
        {
            await DependencyService.Get<IAuthSignIn>().AuthLogOut();
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var boxtap = (BoxView)sender;

            var cp = boxtap.StyleId;
            changeOptions.OptionsPlaceholder = cp;
            foreach (var view in gridMain.Children)
            {
                if (view.StyleId != "changeOptions")
                    view.FadeTo(0.4, 400U, Easing.SinInOut);
            }
            changeOptions.EntryVisible = false;
            changeOptions.GenderVisible = false;
            changeOptions.HeightVisible = false;
            switch (cp)
            {
                case "Gender":
                    {
                        changeOptions.GenderVisible = true;
                        changeOptions.MaleClicked += ChangeOptionsOnMaleClicked;
                        //                        changeOptions.GenderFocus();
                        break;
                    }
                case "Age":
                    {
                        changeOptions.OptionsInput = "";
                        changeOptions.OptionsMaxLength = 3;
                        changeOptions.OptionsKeyboard = Keyboard.Numeric;
                        changeOptions.EntryVisible = true;
                        changeOptions.AgeUnfocused += ChangeOptionsAgeUnfocused;
                        changeOptions.Focus();
                        break;
                    }
                case "Weight":
                    {
                        changeOptions.OptionsInput = "";
                        changeOptions.OptionsMaxLength = 3;
                        changeOptions.OptionsKeyboard = Keyboard.Numeric;
                        changeOptions.EntryVisible = true;
                        changeOptions.WeightUnfocused += ChangeOptionsOnWeightUnfocused;
                        changeOptions.Focus();
                        break;
                    }
                case "Height":
                    {
                        changeOptions.HeightVisible = true;
                        changeOptions.HeightClicked += ChangeOptionsOnHeightClicked;
                        break;
                    }

            }
            changeOptions.TranslateTo(0, 0, 400U, Easing.CubicInOut);
        }

        private void ChangeOptionsOnHeightClicked(object sender, EventArgs eventArgs)
        {

            foreach (var view in gridMain.Children)
            {
                view.FadeTo(1, 400U, Easing.SinInOut);
            }
            changeOptions.TranslateTo(0, 1500, 600U, Easing.CubicInOut);
            changeOptions.HeightClicked -= ChangeOptionsOnHeightClicked;
            labelHeightFt.Text = changeOptions.HeightFtOutput;
            labelHeightInch.Text = changeOptions.HeightInOutput;
            Settings.UserHeightFt = labelHeightFt.Text;
            Settings.UserHeightIn = labelHeightInch.Text;


        }

        private void ChangeOptionsOnWeightUnfocused(object sender, FocusEventArgs focusEventArgs)
        {

            foreach (var view in gridMain.Children)
            {
                view.FadeTo(1, 400U, Easing.SinInOut);
            }
            changeOptions.TranslateTo(0, 1500, 600U, Easing.CubicInOut);
            changeOptions.WeightUnfocused -= ChangeOptionsOnWeightUnfocused;
            labelWeight.Text = changeOptions.OptionsOutput;
            Settings.UserWeight = labelWeight.Text;

        }



        private void ChangeOptionsOnMaleClicked(object sender, EventArgs eventArgs)
        {
            foreach (var view in gridMain.Children)
            {
                view.FadeTo(1, 400U, Easing.SinInOut);
            }
            changeOptions.TranslateTo(0, 1500, 600U, Easing.CubicInOut);
            var g = changeOptions.GenderOutput;
            if (g == "male")
            {
                labelGender.Text = "M";
            }
            else if (g == "female")
            {
                labelGender.Text = "F";
            }
            changeOptions.MaleClicked -= ChangeOptionsOnMaleClicked;
            Settings.UserGender = g;

        }

        public void ChangeOptionsAgeUnfocused(object sender, FocusEventArgs e)
        {

            foreach (var view in gridMain.Children)
            {
                view.FadeTo(1, 400U, Easing.SinInOut);
            }
            changeOptions.TranslateTo(0, 1500, 600U, Easing.CubicInOut);
            changeOptions.AgeUnfocused -= ChangeOptionsAgeUnfocused;
            labelAge.Text = changeOptions.OptionsOutput;
            Settings.UserAge = labelAge.Text;
        }

        private void TapGestureRecognizerActivityLevel_OnTapped(object sender, EventArgs e)
        {
            alfPicker.Focus();
        }

        public void AlfPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var p = (Picker)sender;
            ActivityLevel = alfPicker.Items[alfPicker.SelectedIndex];
            labelActivityLevel.Text = ActivityLevel;
            Settings.UserAlfString = labelActivityLevel.Text;
            Settings.UserAlf = alfPicker.SelectedIndex;
        }

        private async void LPMenuItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginView());
        }
    }
}