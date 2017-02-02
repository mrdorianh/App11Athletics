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
            changeOptions.HeightFtValue = Convert.ToDouble(labelHeightFt.Text);
            changeOptions.HeightInValue = Convert.ToDouble(labelHeightInch.Text);
            var g = Settings.UserGender;
            if (g == "male")
            {
                labelGender.Text = "M";
            }
            else if (g == "female")
            {
                labelGender.Text = "F";
            }
            labelBmr.Text = Settings.UserBmr;
            labelDce.Text = Settings.UserDce;
        }

        public string GivenName => Settings.UserGivenName;
        public string HeightFt { get; set; }
        public string HeightIn { get; set; }
        public string ActivityLevel { get; set; }
        public string DCE { get; set; }
        public string BMR { get; set; }
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
            var vf = Settings.UserHeightFt;
            var vi = Settings.UserHeightIn;
            Settings.UserHeightFt = labelHeightFt.Text;
            Settings.UserHeightIn = labelHeightInch.Text;
            if (Settings.UserHeightFt != vf || vi != Settings.UserHeightIn)
                CalorieCalc();

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
            var v = Settings.UserWeight;
            Settings.UserWeight = labelWeight.Text;
            if (Settings.UserWeight != v)
                CalorieCalc();
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
            var v = Settings.UserGender;
            Settings.UserGender = g;
            if (Settings.UserGender != v)
                CalorieCalc();
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
            var v = Settings.UserAge;
            Settings.UserAge = labelAge.Text;
            if (Settings.UserAge != v)
                CalorieCalc();
        }

        private void TapGestureRecognizerActivityLevel_OnTapped(object sender, EventArgs e)
        {
            alfPicker.Focus();
        }

        private async void CalorieCalc()
        {
            //            Women: BMR = 655 + (4.35 x weight in pounds) + (4.7 x height in inches) - (4.7 x age in years)
            //            Men: BMR = 66 + (6.23 x weight in pounds) + (12.7 x height in inches) - (6.8 x age in years)
            await Task.Run(() =>
            {
                var w = Settings.UserWeight;
                var hF = Settings.UserHeightFt;
                var hI = Settings.UserHeightIn;
                var a = Settings.UserAge;
                var g = Settings.UserGender;
                var af = Settings.UserAlf;
                var h = (Convert.ToDouble(hF) * 12) + Convert.ToDouble(hI);
                var weight = Convert.ToDouble(w);
                var age = Convert.ToDouble(a);
                double bmr;
                double dce;
                switch (g.ToLower())
                {
                    case "female":
                        //                    Female BMR
                        bmr = 655 + (4.35 * weight) + (4.7 * h) - (4.7 * age);
                        dce = af * bmr;
                        Settings.UserDce = dce.ToString();
                        Settings.UserBmr = bmr.ToString();
                        break;
                    case "male":
                        //                    Male BMR
                        bmr = 66 + (6.23 * weight) + (12.7 * h) - (6.8 * age);
                        dce = af * bmr;
                        Settings.UserDce = dce.ToString();
                        Settings.UserBmr = bmr.ToString();
                        break;
                }
            });
            await AnimateCalories();
        }

        private async Task AnimateCalories()
        {
            await Task.WhenAny(gridBmr.TranslateTo(1000, 0, 300U, Easing.CubicIn), Task.Delay(150));
            await Task.WhenAny(gridDce.TranslateTo(-1000, 0, 350U, Easing.CubicIn), Task.Delay(150));
            labelBmr.Text = Settings.UserBmr;
            labelDce.Text = Settings.UserDce;
            await Task.Delay(250);
            await Task.WhenAny(gridBmr.TranslateTo(0, 0, 300U, Easing.CubicOut), Task.Delay(150));
            await Task.WhenAny(gridDce.TranslateTo(0, 0, 350U, Easing.CubicOut), Task.Delay(150));

        }

        public void AlfPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var p = (Picker)sender;
            ActivityLevel = alfPicker.Items[alfPicker.SelectedIndex];
            labelActivityLevel.Text = ActivityLevel;
            var v = Settings.UserAlf;
            Settings.UserAlfString = labelActivityLevel.Text;

            if (alfPicker.SelectedIndex == -1)
            { }
            else
            {
                double ALF;
                if (alfPicker.SelectedIndex == 0)
                {
                    ALF = 1.2;
                    Settings.UserAlf = ALF;
                }
                else if (alfPicker.SelectedIndex == 1)
                {
                    ALF = 1.375;
                    Settings.UserAlf = ALF;
                }
                else if (alfPicker.SelectedIndex == 2)
                {
                    ALF = 1.55;
                    Settings.UserAlf = ALF;
                }
                else if (alfPicker.SelectedIndex == 3)
                {
                    ALF = 1.725;
                    Settings.UserAlf = ALF;
                }
                else if (alfPicker.SelectedIndex == 4)
                {
                    ALF = 1.9;
                    Settings.UserAlf = ALF;
                }
            }
            CalorieCalc();
        }

        private async void LPMenuItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginView());
        }
    }
}