using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.Media.Audiofx;
using App11Athletics.DHCToolkit;
using App11Athletics.Helpers;
using App11Athletics.ViewModels;
using FFImageLoading;
using FFImageLoading.Transformations;
using FFImageLoading.Forms;
using FFImageLoading.Work;
using Plugin.Connectivity;
using Syncfusion.SfCarousel.XForms;
using Xamarin.Forms;


namespace App11Athletics.Views
{
    [PropertyChanged.ImplementPropertyChanged]
    public partial class HomeMenuView : ContentPage
    {
        public bool disable = false;

        public HomeMenuView()
        {

            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizerBox_OnTapped;
            cacheImage = new CachedImage { Source = ProfileImage, Transformations = new List<ITransformation>() { new CircleTransformation(8.0, "#005EBF") }, Style = (Style)Application.Current.Resources["styleCachedImage"], Rotation = ImageRotation, GestureRecognizers = { tapGestureRecognizer }, HorizontalOptions = LayoutOptions.Center };
            Image TimerImage = new Image() { Source = "lightning.png", Aspect = Aspect.AspectFit, Rotation = 0.0, GestureRecognizers = { tapGestureRecognizer }, HorizontalOptions = LayoutOptions.Center };
            Debug.WriteLine(cacheImage.IsLoading.ToString());
            cacheImage.Error += CacheImage_OnError;
            cacheImage.Success += CacheImage_OnSuccess;
            cacheImage.Finish += CacheImage_OnFinish;
            cacheImage.FileWriteFinished += CacheImage_OnFileWriteFinished;
            //            carouselMain.SelectionChanged += CarouselMain_OnSelectionChanged;
            ObservableCollection<SfCarouselItem> collectionOfItems = new ObservableCollection<SfCarouselItem>();
            //           
            collectionOfItems.Add(new SfCarouselItem() { ItemContent = cacheImage });
            collectionOfItems.Add(new SfCarouselItem() { ItemContent = TimerImage });
            collectionOfItems.Add(new SfCarouselItem() { ItemContent = new Image() { Source = "document.png", Aspect = Aspect.AspectFit, Rotation = 0.0, GestureRecognizers = { tapGestureRecognizer }, }, HorizontalOptions = LayoutOptions.CenterAndExpand });
            collectionOfItems.Add(new SfCarouselItem() { ItemContent = new Image() { Source = "iconbevel.png", Aspect = Aspect.AspectFit, Rotation = 0.0, GestureRecognizers = { tapGestureRecognizer }, }, HorizontalOptions = LayoutOptions.Fill });
            this.sfCarouselX.DataSource = collectionOfItems;

            //            gridCara.Children.Add(SfCarousel);
            //            carouselMain.BindingContext = new CarouselViewModel();
            //            CarouselMain_OnSelectionChanged(null, null);
            //            MenuTitle = Width / 8;


        }

        //        private static SfCarousel carouselMain;
        public double MenuTitle { get; set; }

        public static bool running { get; set; }

        public double CircleWidth { get; set; }


        CachedImage cacheImage { get; set; }



        public double ImageRotation => Settings.UserProfileImageRotation;
        public Xamarin.Forms.ImageSource ProfileImage => (Xamarin.Forms.ImageSource)new Xamarin.Forms.ImageSourceConverter().ConvertFromInvariantString(
            Settings.UserPicture);

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect();
        }



        protected override async void OnAppearing()
        {
            base.OnAppearing();
            /*Opacity = 0;

             //            var cm = (CarouselViewModel)carouselMain.BindingContext;
             //            cm.ImageCollection[0].ProfileImage = Settings.UserPicture;
             await Task.Delay(1);

             gridSchedule.TranslationX = Width * -2;
             MenuTitle = Width / 8;
             imageBG.TranslationX = Width / 2;
             imageBG.TranslationY = -Height;
             this.FadeTo(1, 400U, Easing.CubicOut);
             AnimatePages.BgLogoTask(imageBG, Width / 2, Height / 2);

             await AnimatePages.AnimatePageIn(gridHomeMenu);
             await gridSchedule.TranslateTo(Width / -2.1, 0, 250U, Easing.CubicOut);
              */
            CarouselMain_OnSelectionChanged(null, null);
        }




        public void HomeMenuView_OnSizeChanged(object sender, EventArgs e)
        {
            MenuTitle = Width / 8;

        }
        //        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        //        {
        //            if (this.disable)
        //                return;
        //            var p = (Image)sender;
        //            var sI = Convert.ToInt32(p.StyleId);
        //            var cI = carouselMain.SelectedIndex.ToString();
        //
        //            if (p.StyleId != cI)
        //            {
        //                carouselMain.SelectedIndex = sI;
        //                CarouselMain_OnSelectionChanged(null, null);
        //                foreach (var view in griddots.Children)
        //                {
        //                    // if (!string.Equals(view.StyleId, cI, StringComparison.Ordinal))
        //                    if (view != p)
        //                    {
        //                        view.FadeTo(0.5);
        //                        view.ScaleTo(1);
        //                    }
        //                }
        //
        //                p.FadeTo(1);
        //                p.ScaleTo(2);
        //                if (running)
        //                {
        //                    running = false;
        //                    var ty = p.TranslationY;
        //                    this.AbortAnimation("AnimationDot");
        //                }
        //
        //                AnimateDot(p);
        //            }
        //            else
        //            {
        //                var index = carouselMain.SelectedIndex;
        //                disable = true;
        //                if (running)
        //                {
        //                    running = false;
        //                    this.AbortAnimation("AnimationDot");
        //                    this.AbortAnimation("AnimationText");
        //                }
        //                if (index == 0)
        //                {
        //                    var nav = new UserProfileView();
        //                    AnimateDotToNav(p, nav);
        //                }
        //                else if (index == 1)
        //                {
        //                    {
        //                        var nav = new StopwatchFeatureView();
        //                        AnimateDotToNav(p, nav);
        //                    }
        //                }
        //                else if (index == 2)
        //                {
        //                    {
        //                        var nav = new WorkoutLogListView();
        //                        AnimateDotToNav(p, nav);
        //                    }
        //                }
        //                else if (index == 3)
        //                {
        //                    {
        //                        var nav = new Discover11AthleticsView();
        //                        AnimateDotToNav(p, nav);
        //                    }
        //                }
        //
        //            }
        //        }

        private async void CarouselMain_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            disable = true;
            imageArrowsLeft.IsVisible = false;
            imageArrowsRight.IsVisible = false;
            var s = sfCarouselX.SelectedIndex;
            if (!running)
            {
                AnimateText(labelMenuTitle);

                //                this.AbortAnimation("AnimationText");
            }


            await Task.Delay(400);

            if (s == sfCarouselX.SelectedIndex)
            {
                ChangeTitleText(sfCarouselX.SelectedIndex, labelMenuTitle, "Profile", "Timers",
                     "Workout Log", "Discover 11");
                await AnimateTextIn(labelMenuTitle);
            }
            if (sfCarouselX.SelectedIndex <= 0)
            {
                imageArrowsLeft.IsVisible = false;
                imageArrowsRight.IsVisible = true;
            }
            else if (sfCarouselX.SelectedIndex >= 3)
            {
                imageArrowsLeft.IsVisible = true;
                imageArrowsRight.IsVisible = false;
            }
            else
            {
                imageArrowsLeft.IsVisible = true;
                imageArrowsRight.IsVisible = true;
            }


        }



        private async void AnimateText(Label label)
        {

            running = true;
            Animation parentAnimation = new Animation();


            // Create "up" animation and add to parent. 
            Animation upAnimation = new Animation(v => label.TranslationY = v, 0, -10, Easing.CubicInOut);

            parentAnimation.Add(0, 1, upAnimation);
            Animation fadeOutAnimation = new Animation(v => label.Opacity = v, 1, 0, Easing.CubicInOut,
                () =>
                {


                });
            parentAnimation.Add(0, 1, fadeOutAnimation);

            parentAnimation.Commit(this, "AnimationText", 16, 350U, Easing.CubicInOut, async (v, c) =>
            {
                label.Opacity = 0;
                //                ChangeTitleText(carouselMain.SelectedIndex, labelMenuTitle, "Profile", "Timers", "Workout Log",
                //                    "Discover 11");
            });
        }

        private async Task AnimateTextIn(Label label)
        {
            Animation parent2Animation = new Animation();
            Animation fadeInAnimation = new Animation(v => label.Opacity = v, 0, 1, Easing.CubicInOut);
            parent2Animation.Add(0, 1, fadeInAnimation);

            // Create "down" animation and add to parent.
            Animation downAnimation = new Animation(v => label.TranslationY = v, -10, 0, Easing.CubicOut,
                () => Debug.WriteLine("down finished"));
            parent2Animation.Add(0, 1, downAnimation);
            parent2Animation.Commit(this, "Animation2Text", 16, 350U, Easing.CubicInOut);
            running = false;
            disable = false;
        }

        private async void HoldTextFade(Animation parent2Animation)
        {
            var s = sfCarouselX.SelectedIndex;
            await Task.Delay(700);
            if (s == sfCarouselX.SelectedIndex)
                parent2Animation.Commit(this, "Animation2Text", 16, 350U, Easing.CubicInOut);

        }

        public async void ChangeTitleText(int selectedIndex, Label label, string title1 = null,
            string title2 = null, string title3 = null, string title4 = null)
        {

            var s = selectedIndex;

            switch (s)
            {
                case 0:
                    {

                        label.Text = title1;
                        //                        await GetValue(px, py);
                        //                        CurrentTitles.Remove(label.Text);
                        break;
                    }

                case 1:
                    {

                        label.Text = title2;
                        //                        await GetValue(px, py);
                        //                        CurrentTitles.Remove(label.Text);
                        break;
                    }

                case 2:
                    {
                        label.Text = title3;
                        //                        await GetValue(px, py);
                        //                        CurrentTitles.Remove(label.Text);
                        break;
                    }

                case 3:
                    {
                        label.Text = title4;
                        //                        await GetValue(px, py);
                        //                        CurrentTitles.Remove(label.Text);
                        break;
                    }
            }
        }

        private async void TapGestureRecognizerBox_OnTapped(object sender, EventArgs e)
        {

            if (running || disable)
                return;
            disable = true;
            var index = sfCarouselX.SelectedIndex;
            disable = true;
            if (running)
            {
                running = false;
                this.AbortAnimation("AnimationText");
            }

            /* AnimatePages.BgLogoTaskOut(imageBG, Width / 2, -Height * 1.5);
             await gridSchedule.TranslateTo(Width * 2, gridSchedule.TranslationY, 350U, Easing.SpringIn);
             await AnimatePages.AnimatePageOut(gridHomeMenu);*/
            switch (index)
            {
                case 0:
                    {
                        var nav = new UserProfileView();
                        await Navigation.PushAsync(nav);
                        disable = false;
                    }
                    break;
                case 1:
                    {

                        var nav = new TimerMenu();
                        await Navigation.PushAsync(nav);
                        disable = false;

                    }
                    break;
                case 2:
                    {

                        var nav = new WorkoutLogCalendar();
                        await Navigation.PushAsync(nav);
                        disable = false;
                    }
                    break;
                case 3:
                    {

                        var nav = new Discover11AthleticsView();
                        await Navigation.PushAsync(nav);
                        disable = false;
                    }
                    break;
            }
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return;
            var x = gridSchedule.TranslationX;
            Root.RaiseChild(gridSchedule);
            await gridSchedule.TranslateTo(x + 50, gridSchedule.TranslationY, 350U, Easing.CubicOut);
            gridSchedule.TranslateTo(Width * -2, gridSchedule.TranslationY, 300U, Easing.CubicIn);
            await Task.WhenAll(AnimatePages.BgLogoTaskOut(imageBG, Width / 2, -Height * 1.5),
                AnimatePages.AnimatePageOut(gridHomeMenu));
            await Navigation.PushAsync(new ScheduleView());

        }


        void CacheImage_OnError(object sender, CachedImageEvents.ErrorEventArgs e)
        {
            Debug.WriteLine("ERROR LOADING IMAGE");
        }

        void CacheImage_OnSuccess(object sender, CachedImageEvents.SuccessEventArgs e)
        {
            Debug.WriteLine("LOADED IMAGE SUCCESSFULLY");
        }

        void CacheImage_OnFileWriteFinished(object sender, CachedImageEvents.FileWriteFinishedEventArgs e)
        {
            Debug.WriteLine("IMAGE SAVED TO DISK");
        }

        void CacheImage_OnFinish(object sender, CachedImageEvents.FinishEventArgs e)
        {
            Debug.WriteLine("FINISHED LOADING IMAGE");
        }
    }
}