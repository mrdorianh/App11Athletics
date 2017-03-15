using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class UpdateTimerStatusAnimation : ContentView
    {
        public UpdateTimerStatusAnimation()
        {
            InitializeComponent();
            AnimationDotsImages = new List<View>();
            if (gridAnimateStatus.Children != null)
                AnimationDotsImages.AddRange(gridAnimateStatus.Children);
            StopAnimations();
        }



        public double DotSize { get; set; }
        public string Status { get; set; }
        public List<View> AnimationDotsImages { get; set; }

        private void UpdateTimerStatusAnimation_OnSizeChanged(object sender, EventArgs e)
        {
            DotSize = Height / 6;
        }

        public void StopAnimations()
        {
            foreach (var dot in AnimationDotsImages)
            {

                var a = "AnimationText" + dot?.StyleId;
                if (this.AnimationIsRunning(a))
                    this.AbortAnimation(a);
                if (dot != null)
                    dot.Scale = 0;
            }
        }
        public static bool WorkTimeBool { get; set; }
        public static string WorkTimeText { get; set; } = string.Empty;
        public static string WorkTimePrevious { get; set; } = string.Empty;

        public async Task AnimateWorkTimeTask(int work)
        {
            var random = new Random();
            var gymax = gridAnimateStatus.Height;
            var gymin = gridAnimateStatus.Height / 3;
            var gymaxi = Convert.ToInt32(gymax);
            var gymini = Convert.ToInt32(gymin);

            foreach (var dot in AnimationDotsImages)
            {
                var a = "AnimationText" + dot.StyleId;
                if (this.AnimationIsRunning(a))
                    this.AbortAnimation(a);
            }
            if (work == 0)
            {
                foreach (var dot in AnimationDotsImages)
                {
                    var x = Width * -1;
                    var xt = Width / 4;
                    dot.TranslationX = x;
                    dot.TranslationY = gymin;
                    dot.Scale = 2;
                    Animation parentAnimation = new Animation();
                    Animation scaleInAnimation = new Animation(v => dot.Scale = v, 0, 2, Easing.CubicInOut);
                    parentAnimation.Add(0, 0.2, scaleInAnimation);
                    Animation slideOutAnimation = new Animation(v => dot.TranslationX = v, -xt, x * -1, Easing.CubicOut);
                    parentAnimation.Add(0.1, 1, slideOutAnimation);
                    parentAnimation.Commit(this, "AnimationText" + dot.StyleId, 16, 2500U, Easing.CubicInOut);
                    await Task.Delay(150);
                }
            }
            else
            {
                foreach (var dot in AnimationDotsImages)
                {
                    var x = Width * -1;
                    var xt = Width / 4;
                    dot.TranslationX = x;
                    dot.TranslationY = gymin;
                    dot.Scale = 2;
                    Animation parentAnimation = new Animation();
                    Animation scaleOutAnimation = new Animation(v => dot.Scale = v, 2, 0, Easing.CubicInOut);
                    parentAnimation.Add(0.6, 1, scaleOutAnimation);
                    Animation slideinAnimation = new Animation(v => dot.TranslationX = v, x, xt, Easing.CubicOut);
                    parentAnimation.Add(0, 1, slideinAnimation);
                    parentAnimation.Commit(this, "AnimationText" + dot.StyleId, 16, 2000U, Easing.CubicInOut);
                    await Task.Delay(150);
                }
            }
            //            foreach (var dot in AnimationDotsImages)
            //            {
            //                await dot.FadeTo(1, 150U, Easing.CubicOut);
            //            }

            //            await Task.Delay(500);
            //            foreach (var dot in AnimationDotsImages)
            //            {
            //                dot.FadeTo(0, 250U, Easing.CubicOut);
            //                dot.TranslateTo(1000, 0);
            //            }
        }


    }

}