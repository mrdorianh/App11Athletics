using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using App11Athletics.DHCToolkit;
using App11Athletics.ViewModels;
using App11Athletics.Views.Timers;
using ImageCircle.Forms.Plugin.Abstractions;
using Syncfusion.SfCarousel;
using Syncfusion.SfCarousel.XForms;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class HomeMenuView : ContentPage
    {
        public bool disable = false;

        public HomeMenuView()
        {
            InitializeComponent();
            carouselMain.BindingContext = new CarouselViewModel();
            CarouselMain_OnSelectionChanged(null, null);

        }




        public double MenuTitle { get; set; }

        public static bool running { get; set; }


        #region Overrides of Page

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            foreach (var view in griddots.Children)
            {
                // if (!string.Equals(view.StyleId, cI, StringComparison.Ordinal))
                if (view.StyleId != carouselMain.SelectedIndex.ToString())
                {
                    view.FadeTo(0.5);
                    view.ScaleTo(1);
                }
                else
                {
                    view.FadeTo(1);
                    view.ScaleTo(2);
                    AnimateDot((Image)view);
                }
            }
            await AnimatePages.AnimatePageIn(gridHomeMenu);
        }



        #endregion

        public void HomeMenuView_OnSizeChanged(object sender, EventArgs e)
        {
            MenuTitle = gridLabel.Width / 8;
        }


        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (this.disable)
                return;
            var p = (Image)sender;
            var sI = Convert.ToInt32(p.StyleId);
            var cI = carouselMain.SelectedIndex.ToString();
            if (p.StyleId != cI)
            {
                carouselMain.SelectedIndex = sI;
                CarouselMain_OnSelectionChanged(null, null);
                foreach (var view in griddots.Children)
                {
                    // if (!string.Equals(view.StyleId, cI, StringComparison.Ordinal))
                    if (view != p)
                    {
                        view.FadeTo(0.5);
                        view.ScaleTo(1);
                    }
                }

                p.FadeTo(1);
                p.ScaleTo(2);
                if (running)
                {
                    running = false;
                    var ty = p.TranslationY;
                    this.AbortAnimation("AnimationDot");
                }

                AnimateDot(p);
            }
            else
            {
                disable = true;
                if (running)
                {
                    running = false;
                    this.AbortAnimation("AnimationDot");
                    this.AbortAnimation("AnimationText");
                }

                AnimateDotToNav(p);
            }
        }



        private void AnimateDotToNav(Image p)
        {
            Animation parentAnimation = new Animation();
            Animation upAnimation = new Animation(v => p.TranslationY = v, 0, -100, Easing.CubicInOut,
                () => Debug.WriteLine("up finished"));
            parentAnimation.Add(0, 0.6, upAnimation);
            parentAnimation.Commit(this, "AnimationDotToNav", 16, 1200, null, async (v, c) =>
            {
                await AnimatePages.AnimatePageOut(gridHomeMenu);
                await Task.Delay(250);
                await Navigation.PushAsync(new Discover11AthleticsView());
                p.Scale = 1;
                p.TranslationY = 0;
                disable = false;
            });
        }



        private void AnimateDot(Image p)
        {
            running = true;
            Animation parentAnimation = new Animation();

            // Create "up" animation and add to parent. 
            Animation upAnimation = new Animation(v => p.TranslationY = v, 0, -10, Easing.CubicInOut,
                () => Debug.WriteLine("up finished"));
            parentAnimation.Add(0, 0.6, upAnimation);

            // Create "down" animation and add to parent.
            Animation downAnimation = new Animation(v => p.TranslationY = v, -10, 0, Easing.CubicOut,
                () => Debug.WriteLine("down finished"));
            parentAnimation.Insert(0.4, 1, downAnimation);
            parentAnimation.Commit(this, "AnimationDot", 16, 1200, null, (v, c) =>
            {
                p.TranslateTo(0, 0, 600U, Easing.SinOut);
                if (running)
                    AnimateDot(p);
            });
        }

        private async void CarouselMain_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AnimateText(labelMenuTitle);
        }

        private void AnimateText(Label label)
        {
            running = true;
            Animation parentAnimation = new Animation();

            // Create "up" animation and add to parent. 
            Animation upAnimation = new Animation(v => label.TranslationY = v, 0, -10, Easing.CubicInOut);

            parentAnimation.Add(0, 0.5, upAnimation);
            Animation fadeOutAnimation = new Animation(v => label.Opacity = v, 1, 0, Easing.CubicInOut,
                () =>
                {
                    ChangeTitleText(carouselMain.SelectedIndex, labelMenuTitle, "Profile", "Timers",
                        "Workout Log", "Discover 11");

                });
            parentAnimation.Add(0, 0.5, fadeOutAnimation);
            Animation fadeInAnimation = new Animation(v => label.Opacity = v, 0, 1, Easing.CubicInOut);
            parentAnimation.Add(0.5, 1, fadeInAnimation);

            // Create "down" animation and add to parent.
            Animation downAnimation = new Animation(v => label.TranslationY = v, -10, 0, Easing.CubicOut,
                () => Debug.WriteLine("down finished"));
            parentAnimation.Insert(0.5, 1, downAnimation);
            parentAnimation.Commit(this, "AnimationText", 16, 250U, null, (v, c) =>
            {
                label.Opacity = 1;
                //                ChangeTitleText(carouselMain.SelectedIndex, labelMenuTitle, "Profile", "Timers", "Workout Log",
                //                    "Discover 11");
            });
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


    }
}