using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App11Athletics.Helpers;
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

            imageBG.Opacity = 0;
            gridMain.Opacity = 0;
            PageLabels = new List<Label>();
            BigGridLabels = new List<Label>();
            PageLabels.AddRange(new[]
            {
                labelAge, labelGender, labelHeightFt, labelHeightInMark, labelHeightIn, labelHeightftMark,
                labelWeight
            });
            BigGridLabels.AddRange(new[]
            {
                labelActivityLevel, labelBmr, labelDce
            });
            //                        gridMain.BindingContext = new UserProfileModel();

            gridGenderOptions.TranslationY = 1500;
            gridAgeOptions.TranslationY = 1500;
            gridWeightOptions.TranslationY = 1500;
            gridHeightOptions.TranslationY = 1500;

            circleImage.Source = Settings.UserPicture;
            labelName.Text = Settings.UserGivenName;
            ActivityLevel = Settings.UserAlfString;
            labelWeight.Text = Settings.UserWeight;
            labelAge.Text = Settings.UserAge;
            labelHeightFt.Text = Settings.UserHeightFt;
            labelHeightIn.Text = Settings.UserHeightIn;
            stepperHeightFt.Value = Convert.ToDouble(Settings.UserHeightFt);
            stepperHeightIn.Value = Convert.ToDouble(Settings.UserHeightIn);

            //            changeOptions.HeightFtValue = Convert.ToDouble(labelHeightFt.Text);
            //            changeOptions.HeightInValue = Convert.ToDouble(labelHeightInch.Text);
            var g = Settings.UserGender;

            if (g == "male")
            {
                labelGender.Text = "M";
            }
            else if (g == "female")
            {
                labelGender.Text = "F";
            }
            //            labelBmr.Text = Settings.UserBmr;
            //            labelDce.Text = Settings.UserDce;
            CalorieCalc();

        }

        public bool Disable { get; set; }

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
            imageBG.Opacity = 0;
            gridMain.Opacity = 0;
            imageBG.Scale = 0;
            imageBG.Opacity = 0.4;
            base.OnAppearing();
            await Task.Delay(100);
            await imageBG.ScaleTo(0.8, 400U, Easing.SpringOut);

            AnimatePages.AnimatePageIn(gridMain, imageBG);
            await gridMain.FadeTo(1, 350, Easing.CubicOut);
            //            Disable = true;
        }

        #endregion

        public List<Label> PageLabels { get; set; }
        public List<Label> BigGridLabels { get; set; }

        public ICommand labelchange { get; set; }

        public double DFontSize { get; private set; }
        public double OptionsTitleFontSize { get; set; }

        public double BGWidth { get; set; }



        private void UserProfileView_OnSizeChanged(object sender, EventArgs e)
        {
            BGWidth = Width / 2;
            OptionsTitleFontSize = Width / 6;
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
            GenderTitle.FontSize = OptionsTitleFontSize;
            AgeTitle.FontSize = OptionsTitleFontSize;
            labelAgeEntry.FontSize = OptionsTitleFontSize / 1.2;
            WeightTitle.FontSize = OptionsTitleFontSize;
            labelWeightEntry.FontSize = OptionsTitleFontSize / 1.2;
            HeightTitle.FontSize = OptionsTitleFontSize;

        }

        private async void Logout_OnClicked(object sender, EventArgs e)
        {
            var das = await DisplayActionSheet("Are you sure you want to Logout?", "Cancel", null, "Yes");

            switch (das)
            {
                case "Yes":
                    await DependencyService.Get<IAuthSignIn>().AuthLogOut();
                    Navigation.InsertPageBefore(new LoginView(), Navigation.NavigationStack[0]);
                    await Task.Delay(100);
                    await Navigation.PopToRootAsync();
                    break;
            }
            //            await DependencyService.Get<IAuthSignIn>().AuthLogOut();
            //            Navigation.InsertPageBefore(new LoginView(), Navigation.NavigationStack[0]);
            //            await Task.Delay(100);
            //            await Navigation.PopToRootAsync();

        }

        #region Overrides of Page

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            //            imageBG.ScaleTo(0, 350U, Easing.CubicOut);
            await AnimatePages.AnimatePageOut(gridMain, imageBG);
        }

        #endregion

        private async void TapGestureRecognizerActivityLevel_OnTapped(object sender, EventArgs e)
        {
            //            Disable = false;
            if (alfPicker.IsFocused)
                return;
            await Task.Delay(100);
            alfPicker.Focus();
        }

        private async void CalorieCalc()
        {
            //            Women: BMR = 655 + (4.35 x weight in pounds) + (4.7 x height in inches) - (4.7 x age in years)
            //            Men: BMR = 66 + (6.23 x weight in pounds) + (12.7 x height in inches) - (6.8 x age in years)
            if (labelAge.Text == "" || labelAge.Text == "--" || labelWeight.Text == "" ||
                labelWeight.Text == "--")
            {
                labelBmr.Text = "";
                labelDce.Text = "";
                Settings.UserBmr = labelBmr.Text;
                Settings.UserDce = labelDce.Text;
                Settings.UserAge = labelAge.Text;
                Settings.UserWeight = labelWeight.Text;
                gridBmr.FadeTo(0.4, 400U, Easing.SinInOut);
                await gridDce.FadeTo(0.4, 400U, Easing.SinInOut);
                return;
            }
            await Task.Run(() =>
            {
                var hF = Settings.UserHeightFt;
                var hI = Settings.UserHeightIn;
                var g = Settings.UserGender;
                var af = Settings.UserAlf;
                var h = (Convert.ToDouble(hF) * 12) + Convert.ToDouble(hI);
                var userWeight = Settings.UserWeight;

                var userAge = Settings.UserAge;
                var listD = new List<double> { af, h, Convert.ToDouble(userWeight), Convert.ToDouble(userAge) };
                var listS = new List<string> { hF, hI, g };
                foreach (var s in listS)
                {
                    if (string.IsNullOrEmpty(s))
                        return;
                }
                foreach (var d in listD)
                {
                    if (d <= 0)
                        return;
                }
                double bmr;
                double dce;

                switch (g.ToLower())
                {
                    case "female":
                        //                    Female BMR
                        bmr = 655 + (4.35 * Convert.ToDouble(userWeight)) + (4.7 * h) - (4.7 * Convert.ToDouble(userAge));
                        dce = af * bmr;
                        Settings.UserDce = $"{dce:#}";
                        Settings.UserBmr = $"{bmr:#}";
                        break;
                    case "male":
                        //                    Male BMR
                        bmr = 66 + (6.23 * Convert.ToDouble(userWeight)) + (12.7 * h) - (6.8 * Convert.ToDouble(userAge));
                        dce = af * bmr;
                        Settings.UserDce = $"{dce:#}";
                        Settings.UserBmr = $"{bmr:#}";
                        break;
                }
            });
            //            boxView.InputTransparent = true;
            await AnimateCalories();
        }

        private async Task AnimateCalories()
        {
            await Task.WhenAny(gridBmr.TranslateTo(1000, 0, 300U, Easing.CubicIn), Task.Delay(150));
            await Task.WhenAny(gridDce.TranslateTo(-1000, 0, 350U, Easing.CubicIn), Task.Delay(150));
            gridBmr.Opacity = 1;
            gridDce.Opacity = 1;
            labelBmr.Text = Settings.UserBmr;
            labelDce.Text = Settings.UserDce;
            await Task.Delay(250);
            await Task.WhenAny(gridBmr.TranslateTo(0, 0, 300U, Easing.CubicOut), Task.Delay(150));
            await Task.WhenAny(gridDce.TranslateTo(0, 0, 350U, Easing.CubicOut), Task.Delay(150));

        }

        private void AlfPicker_OnUnfocused(object sender, FocusEventArgs e)
        {
            //            Disable = true;
            if (alfPicker.SelectedIndex != -1)
                ActivityLevel = alfPicker.Items[alfPicker.SelectedIndex];
            Device.BeginInvokeOnMainThread(() =>
            {
                labelActivityLevel.Text = ActivityLevel;
                Settings.UserAlfString = labelActivityLevel.Text;
            }
        );
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
            //            Disable = false;
        }

        private async void TapGestureRecognizerHeight_OnTapped(object sender, EventArgs e)
        {
            await gridHeightOptions.TranslateTo(0, 0, 400U, Easing.CubicInOut);
        }

        private async void TapGestureRecognizerWeight_OnTapped(object sender, EventArgs e)
        {
            await gridWeightOptions.TranslateTo(0, 0, 400U, Easing.CubicInOut);
            await Task.Delay(50);
            WeightOptionsEntry.Focus();
        }

        private async void TapGestureRecognizerAge_OnTapped(object sender, EventArgs e)
        {
            await gridAgeOptions.TranslateTo(0, 0, 400U, Easing.CubicInOut);
            await Task.Delay(50);
            AgeOptionsEntry.Focus();
        }

        private async void TapGestureRecognizerGender_OnTapped(object sender, EventArgs e)
        {
            await gridGenderOptions.TranslateTo(0, 0, 400U, Easing.CubicInOut);
        }

        private async void GenderMaleButton_OnClicked(object sender, EventArgs e)
        {
            labelGender.Text = "M";
            var v = Settings.UserGender;
            var g = "male";
            Settings.UserGender = g;
            await gridGenderOptions.TranslateTo(0, 1500, 400U, Easing.CubicInOut);
            if (Settings.UserGender != v)
                CalorieCalc();
        }

        private async void GenderFemaleButton_OnClicked(object sender, EventArgs e)
        {
            labelGender.Text = "F";
            var v = Settings.UserGender;
            var g = "female";
            Settings.UserGender = g;
            await gridGenderOptions.TranslateTo(0, 1500, 400U, Easing.CubicInOut);
            if (Settings.UserGender != v)
                CalorieCalc();
        }

        private void AgeOptionsEntry_OnUnfocused(object sender, FocusEventArgs e)
        {
            gridAgeOptions.TranslateTo(0, 1500, 600U, Easing.CubicInOut);
            if (!string.IsNullOrEmpty(AgeOptionsEntry.Text) && !AgeOptionsEntry.Text.Contains("."))
                labelAge.Text = AgeOptionsEntry.Text;
            else
            {
                labelAge.Text = "--";
                CalorieCalc();
                return;
            }
            var v = Settings.UserAge;
            if (!string.IsNullOrEmpty(labelAge.Text))
            {
                Settings.UserAge = labelAge.Text;
                if (Settings.UserAge == v)
                    return;
            }
            else
            {
                labelAge.Text = "--";
            }
            if (Convert.ToDouble(labelAge.Text) < 10 || Convert.ToDouble(labelAge.Text) > 100)
                labelAge.Text = "--";
            CalorieCalc();
        }

        private void AgeOptionsEntry_OnFocused(object sender, FocusEventArgs e)
        {
            AgeOptionsEntry.Text = string.Empty;
        }

        private void WeightOptionsEntry_OnFocused(object sender, FocusEventArgs e) { WeightOptionsEntry.Text = string.Empty; }

        private void WeightOptionsEntry_OnUnfocused(object sender, FocusEventArgs e)
        {
            gridWeightOptions.TranslateTo(0, 1500, 600U, Easing.CubicInOut);
            if (!string.IsNullOrEmpty(WeightOptionsEntry.Text) && !WeightOptionsEntry.Text.Contains("."))
                labelWeight.Text = WeightOptionsEntry.Text;
            else
            {
                labelWeight.Text = "--";
                CalorieCalc();
                return;
            }
            var v = Settings.UserWeight;
            if (!string.IsNullOrEmpty(labelWeight.Text))
            {
                Settings.UserWeight = labelWeight.Text;
                if (Settings.UserWeight == v)
                    return;
            }
            else
            {
                labelWeight.Text = "--";
            }
            if (Convert.ToDouble(labelWeight.Text) < 85)
                labelWeight.Text = "--";

            CalorieCalc();
        }

        private void ButtonEnterHeight_OnClicked(object sender, EventArgs e)
        {
            HeightFt = stepperHeightFt.Value.ToString();
            HeightIn = stepperHeightIn.Value.ToString();
            var hf = Settings.UserHeightFt;
            var hi = Settings.UserHeightIn;
            var b = false;
            if (hf != HeightFt || hi != HeightIn)
            {
                Settings.UserHeightFt = HeightFt;
                Settings.UserHeightIn = HeightIn;
                b = true;
            }
            labelHeightFt.Text = HeightFt;
            labelHeightIn.Text = HeightIn;
            gridHeightOptions.TranslateTo(0, 1500, 600U, Easing.CubicInOut);
            if (b)
                CalorieCalc();
        }
    }
}