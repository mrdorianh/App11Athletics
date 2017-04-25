using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Graphics;
using Android.Media;
using Android.OS;
using App11Athletics.Helpers;
using App11Athletics.DHCToolkit;
using ExifLib;
using FFImageLoading.Forms;
using FFImageLoading.Work;
using PCLStorage;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using PropertyChanged;
using Xamarin.Forms;
using XLabs;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;
using Debug = System.Diagnostics.Debug;
using Image = Xamarin.Forms.Image;
using ImageCircle = App11Athletics.DHCToolkit.ImageCircle;
using ImageSource = Xamarin.Forms.ImageSource;

namespace App11Athletics.Views
{
    [ImplementPropertyChanged]
    public partial class UserProfileView : ContentPage
    {
        public static string st;

        bool busy;
        public IResolver resolver;
        public UserProfileView()
        {

            InitializeComponent();
            Setup();
            //            dhcCircle.Children.Add(ProImage);
            //            var tapGestureRecognizer = new TapGestureRecognizer();
            //            tapGestureRecognizer.Tapped += ChangePhotoTapped;
            //            tapGestureRecognizer.Tapped -= ChangePhotoTapped;
            //            dhcCircle.GestureRecognizers.Add(tapGestureRecognizer);
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
            gridGenderOptions.TranslationY = 1500;
            gridAgeOptions.TranslationY = 1500;
            gridWeightOptions.TranslationY = 1500;
            gridHeightOptions.TranslationY = 1500;

            //           .Source = Settings.UserPicture;
            //            labelName.Text = Settings.UserGivenName;
            var ChangeNameTapper = new TapGestureRecognizer();
            ChangeNameTapper.Tapped += (sender, args) => entryLabelName.Focus();
            boxViewlabelnameTapper.GestureRecognizers.Add(ChangeNameTapper);

            //            entryLabelName.Text = Settings.UserGivenName;
            ActivityLevel = Settings.UserAlfString;
            labelWeight.Text = Settings.UserWeight;

            labelAge.Text = Settings.UserAge;
            labelHeightFt.Text = Settings.UserHeightFt;
            labelHeightIn.Text = Settings.UserHeightIn;
            stepperHeightFt.Value = Convert.ToDouble(Settings.UserHeightFt);
            stepperHeightIn.Value = Convert.ToDouble(Settings.UserHeightIn);

            //            changeOptions.HeightFtValue = Convert.ToDouble(labelHeightFt.Text);
            //            changeOptions.HeightInValue = Convert.ToDouble(labelHeightInch.Text);

            labelBmr.Text = Settings.UserBmr;
            labelDce.Text = Settings.UserDce;
            var g = Settings.UserGender;
            if (g == "male")
            {
                labelGender.Text = "M";
            }
            else if (g == "female")
            {
                labelGender.Text = "F";
            }
            var intro = (string.IsNullOrEmpty(labelBmr.Text) || string.IsNullOrEmpty(labelDce.Text));
            //            labelBmr.Text = Settings.UserBmr;
            //            labelDce.Text = Settings.UserDce;
            CalorieCalc(intro);

        }


        #region Overrides of Page
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
        #endregion


        public bool Disable { get; set; }


        public string GivenName { get; set; } = Settings.UserGivenName;
        public string HeightFt { get; set; }
        public string HeightIn { get; set; }
        public string ActivityLevel { get; set; }
        public string DCE { get; set; }
        public string BMR { get; set; }

        public List<Label> PageLabels { get; set; }
        public List<Label> BigGridLabels { get; set; }

        public ICommand labelchange { get; set; }

        public double DFontSize { get; private set; }
        public double OptionsTitleFontSize { get; set; }

        public double BGWidth { get; set; }

        public double ImageRotation { get; set; }

        public ImageSource ProfileImage { get; set; } = (ImageSource)new Xamarin.Forms.ImageSourceConverter().ConvertFromInvariantString(
                       Settings.UserPicture);

        //        public ImageSource ImageSource => Settings.UserPicture;

        #region Overrides of Page

        protected override async void OnAppearing()
        {
            Opacity = 0;
            base.OnAppearing();
            if (CheckingPermission)
                return;
            await Task.Delay(100);
            await Task.WhenAll(AnimatePages.AnimatePageIn(gridMain),
                this.FadeTo(1, 350, Easing.CubicOut));
            Disable = true;
        }

        #endregion

        private static IMediaPicker Setup()
        {
            var device = Resolver.Resolve<IDevice>();
            ////RM: hack for working on windows phone? 
            var _mediapicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;
            return _mediapicker;
        }

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
            GC.Collect();

            //            {
            //                await Task.Run(() =>
            //                {
            //                    Content = null;
            //                    this.BindingContext = null;
            //                    GC.Collect();
            //                });
            //            }

            //            imageBG.ScaleTo(0, 350U, Easing.CubicOut);
            //            await AnimatePages.AnimatePageOut(gridMain);
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

        private async void CalorieCalc(bool intro = false)
        {
            //            Women: BMR = 655 + (4.35 x weight in pounds) + (4.7 x height in inches) - (4.7 x age in years)
            //            Men: BMR = 66 + (6.23 x weight in pounds) + (12.7 x height in inches) - (6.8 x age in years)
            await Task.Run(async () =>
            {
                if (string.IsNullOrEmpty(labelAge.Text) || string.IsNullOrEmpty(labelWeight.Text))
                {
                    labelBmr.Text = string.Empty;
                    labelDce.Text = string.Empty;
                    Settings.UserBmr = labelBmr.Text;
                    Settings.UserDce = labelDce.Text;
                    Settings.UserAge = labelAge.Text;
                    Settings.UserWeight = labelWeight.Text;
                    gridBmr.FadeTo(0.4, 400U, Easing.SinInOut);
                    await gridDce.FadeTo(0.4, 400U, Easing.SinInOut);
                    return;
                }

                var hF = Settings.UserHeightFt;
                var hI = Settings.UserHeightIn;
                var g = Settings.UserGender;
                var af = Settings.UserAlf;
                var h = Convert.ToDouble(hF) * 12 + Convert.ToDouble(hI);
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
                        bmr = 655 + 4.35 * Convert.ToDouble(userWeight) + 4.7 * h - 4.7 * Convert.ToDouble(userAge);
                        dce = af * bmr;
                        Settings.UserDce = $"{dce:#}";
                        Settings.UserBmr = $"{bmr:#}";
                        break;
                    case "male":
                        //                    Male BMR
                        bmr = 66 + 6.23 * Convert.ToDouble(userWeight) + 12.7 * h - 6.8 * Convert.ToDouble(userAge);
                        dce = af * bmr;
                        Settings.UserDce = $"{dce:#}";
                        Settings.UserBmr = $"{bmr:#}";
                        break;
                }
            });
            //            boxView.InputTransparent = true;
            await AnimateCalories(intro);
        }

        private async Task AnimateCalories(bool intro = false)
        {
            await Task.WhenAny(gridBmr.TranslateTo(1000, 0, 300U, Easing.CubicIn), Task.Delay(150));
            await Task.WhenAny(gridDce.TranslateTo(-1000, 0, 350U, Easing.CubicIn), Task.Delay(150));
            if (!intro)
            {
                gridBmr.Opacity = 1;
                gridDce.Opacity = 1;

                labelBmr.Text = Settings.UserBmr;
                labelDce.Text = Settings.UserDce;
            }
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
            await gridWeightOptions.TranslateTo(0, 0, 300U, Easing.CubicInOut);
            await Task.Delay(1);
            WeightOptionsEntry.Focus();
        }

        private async void TapGestureRecognizerAge_OnTapped(object sender, EventArgs e)
        {
            await gridAgeOptions.TranslateTo(0, 0, 300U, Easing.CubicInOut);
            await Task.Delay(1);
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
            if (Settings.UserGender != v || !string.IsNullOrEmpty(Settings.UserAge) || !string.IsNullOrEmpty(Settings.UserWeight))
                CalorieCalc();
        }

        private void AgeOptionsEntry_OnUnfocused(object sender, FocusEventArgs e)
        {
            gridAgeOptions.TranslateTo(0, 1500, 600U, Easing.CubicInOut);
            if (!string.IsNullOrEmpty(AgeOptionsEntry.Text) && !AgeOptionsEntry.Text.Contains("."))
                labelAge.Text = AgeOptionsEntry.Text;
            else
            {
                labelAge.Text = string.Empty;
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
                labelAge.Text = string.Empty;
            }
            if (Convert.ToDouble(labelAge.Text) < 10 || Convert.ToDouble(labelAge.Text) > 100)
                labelAge.Text = string.Empty;
            CalorieCalc();
        }

        private void AgeOptionsEntry_OnFocused(object sender, FocusEventArgs e)
        {
            AgeOptionsEntry.Text = string.Empty;
        }

        private void WeightOptionsEntry_OnFocused(object sender, FocusEventArgs e) { WeightOptionsEntry.Text = string.Empty; }

        private async void WeightOptionsEntry_OnUnfocused(object sender, FocusEventArgs e)
        {
            gridWeightOptions.TranslateTo(0, 1500, 600U, Easing.CubicInOut);
            if (!string.IsNullOrEmpty(WeightOptionsEntry.Text) && !WeightOptionsEntry.Text.Contains("."))
                labelWeight.Text = WeightOptionsEntry.Text;
            else
            {
                labelWeight.Text = string.Empty;
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
                labelWeight.Text = string.Empty;
            }
            if (Convert.ToDouble(labelWeight.Text) < 85)
                labelWeight.Text = string.Empty;

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

        private async void ChangePhotoTapped(object sender, EventArgs e)
        {
            if (busy)
                return;
            busy = true;
            PermissionStatus status;
            CheckingPermission = true;
            Device.OnPlatform(async () =>
            {
                status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);
                if (status != PermissionStatus.Granted)
                {
                    Opacity = 0;
                    BackgroundColor = Xamarin.Forms.Color.Black;
                    status = (await CrossPermissions.Current.RequestPermissionsAsync(Permission.Photos))[Permission.Photos];
                }
                if (status != PermissionStatus.Granted)
                {
                    busy = false;
                    Opacity = 1;
                    BackgroundColor = Xamarin.Forms.Color.White;
                    CheckingPermission = false;
                    return;
                }
                status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    status = (await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera))[Permission.Camera];
                }
                if (status != PermissionStatus.Granted)
                {
                    busy = false;
                    Opacity = 1;
                    BackgroundColor = Xamarin.Forms.Color.White;
                    CheckingPermission = false;
                    return;
                }
                Opacity = 1;
                BackgroundColor = Xamarin.Forms.Color.White;
                CheckingPermission = false;
                await DisplayPhotoOptions();
            }, async () =>
            {
                status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    Opacity = 0;
                    BackgroundColor = Xamarin.Forms.Color.Black;
                    status = (await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage))[Permission.Storage];
                }
                if (status != PermissionStatus.Granted)
                {
                    busy = false;
                    Opacity = 1;
                    BackgroundColor = Xamarin.Forms.Color.White;
                    CheckingPermission = false;
                    return;
                }
                status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    status = (await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera))[Permission.Camera];
                }
                if (status != PermissionStatus.Granted)
                {
                    busy = false;
                    Opacity = 1;
                    BackgroundColor = Xamarin.Forms.Color.White;
                    return;

                }
                Opacity = 1;
                BackgroundColor = Xamarin.Forms.Color.White;
                CheckingPermission = false;
                await DisplayPhotoOptions();
            });



        }

        public bool CheckingPermission { get; set; }

        private async Task DisplayPhotoOptions()
        {
            var das = await DisplayActionSheet("Select an option", "Cancel", null, "Select Photo", "Use Camera", "Use Original");

            switch (das)
            {
                case "Select Photo":
                    SelectPhoto();
                    break;
                case "Use Camera":
                    TakePhoto();
                    break;
                case "Use Original":
                    GetOriginalPhoto();
                    busy = false;
                    break;
                default:
                    busy = false;
                    break;
            }
        }

        void GetOriginalPhoto()
        {
            if (string.IsNullOrEmpty(Settings.UserPictureOriginal))
                Settings.UserPictureOriginal = "iconbevel.png";
            Settings.UserPicture = Settings.UserPictureOriginal;
            Settings.UserProfileImageRotation = 0;
            ProfileImage = Settings.UserPictureOriginal;

            Navigation.RemovePage(Navigation.NavigationStack[0]);
            Navigation.InsertPageBefore(new HomeMenuView(), this);
            ImageRotation = 0;
        }

        private async void TakePhoto()
        {
            try
            {
                var photoCurrent = Settings.UserPicture;
                var mediaPicker = Setup();
                if (mediaPicker == null)
                    return;
                var mediaFile =
                    await mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions()
                    {
                        DefaultCamera = CameraDevice.Front,
                        SaveMediaOnCapture = true,
                    });
                ImageRotation = 0;
                ProfileImage = ImageSource.FromStream(() => mediaFile.Source);
                Settings.UserProfileImageRotation = UpdateImageRotation(mediaFile.Exif.Orientation);
                Settings.UserPicture = mediaFile.Path;

                if (Settings.UserPicture == photoCurrent)
                {
                    busy = false;
                    return;
                }
                Navigation.RemovePage(Navigation.NavigationStack[0]);
                Navigation.InsertPageBefore(new HomeMenuView(), this);
                busy = false;
            }
            catch (Exception e)
            {
                busy = false;
                return;
            }

        }

        private static bool StayOnPage = false;
        private async void SelectPhoto()
        {
            try
            {

                var photoCurrent = Settings.UserPicture;
                var mediaPicker = Setup();
                if (mediaPicker == null)
                    return;
                var mediaFile =
                    await mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions()
                    {
                        DefaultCamera = CameraDevice.Front,
                        SaveMediaOnCapture = true
                    });
                ProfileImage = ImageSource.FromStream(() => mediaFile.Source);
                ImageRotation = UpdateImageRotation(mediaFile.Exif.Orientation);
                Settings.UserPicture = mediaFile.Path;
                Settings.UserProfileImageRotation = ImageRotation;
                if (Settings.UserPicture == photoCurrent)
                {
                    busy = false;
                    return;
                }
                Navigation.RemovePage(Navigation.NavigationStack[0]);
                Navigation.InsertPageBefore(new HomeMenuView(), this);
                busy = false;
                //                Navigation.RemovePage(Navigation.NavigationStack[0]);
                //                Navigation.InsertPageBefore(new HomeMenuView(), this);
            }
            catch (System.Exception e)
            {
                busy = false;
                return;
            }

        }

        public double UpdateImageRotation(ExifOrientation sourceOrientation)
        {
            double imagerotation;
            if (Device.OS == TargetPlatform.iOS)
                return 0.0;
            switch (sourceOrientation)
            {
                case ExifOrientation.Undefined:
                    {
                        imagerotation = 0.0;
                    }
                    break;
                case ExifOrientation.TopLeft:
                    {
                        imagerotation = 0.0;
                    }
                    break;
                case ExifOrientation.TopRight:
                    {
                        imagerotation = 90.0;
                    }
                    break;
                case ExifOrientation.BottomLeft:
                    {
                        imagerotation = 270.0;
                    }
                    break;
                case ExifOrientation.BottomRight:
                    {
                        imagerotation = 180.0;
                    }
                    break;
                default:
                    imagerotation = 0.0;
                    break;
            }
            return imagerotation;
        }


        #region IDisposable

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        #endregion


        void CacheImage_OnError(object sender, CachedImageEvents.ErrorEventArgs e)
        {
            Debug.WriteLine("ERROR LOADING IMAGE");
        }

        void CacheImage_OnSuccess(object sender, CachedImageEvents.SuccessEventArgs e)
        {
            Debug.WriteLine("LOADED IMAGE SUCCESSFULLY");
            ImageRotation = Settings.UserProfileImageRotation;
        }

        void CacheImage_OnFileWriteFinished(object sender, CachedImageEvents.FileWriteFinishedEventArgs e)
        {
            Debug.WriteLine("IMAGE SAVED TO DISK");
        }

        void CacheImage_OnFinish(object sender, CachedImageEvents.FinishEventArgs e)
        {
            Debug.WriteLine("FINISHED LOADING IMAGE");
        }

        void EntryLabelName_OnFocused(object sender, FocusEventArgs e)
        {
            entryLabelName.Text = GivenName;
            Debug.WriteLine("Entry = " + entryLabelName.Text);
            Debug.WriteLine("Label = " + labelName.Text);
        }

        void EntryLabelName_OnUnfocused(object sender, FocusEventArgs e) { Settings.UserGivenName = GivenName; }

        void EntryLabelName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine("Entry = " + entryLabelName.Text);
            Debug.WriteLine("Label = " + labelName.Text);
            GivenName = entryLabelName.Text;


        }
    }
}