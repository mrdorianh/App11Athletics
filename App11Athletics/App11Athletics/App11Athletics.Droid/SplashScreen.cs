using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using App11Athletics;
namespace App11Athletics.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : Activity, ISurfaceHolderCallback
    {
        VideoView videoView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SplashVideoLayout);

            videoView = FindViewById<VideoView>(Resource.Id.SampleVideoView);
            //videoView.StartAnimation(AnimationUtils.LoadAnimation(this, Resource.Animation.abc_fade_in));
            play("SplashVideo/Splash11athletics.mp4");





            //await Task.Delay(player.Duration);
            player.Completion += vidComplete;



        }

        private async void vidComplete(object sender, EventArgs e)
        {
            videoView.SetBackgroundColor(Color.White);
            await Task.Delay(150);
            StartActivity(typeof(MainActivity));
        }

        MediaPlayer player;


        void play(string fullPath)
        {
            ISurfaceHolder holder = videoView.Holder;
            holder.SetType(SurfaceType.PushBuffers);
            holder.AddCallback(this);
            player = new MediaPlayer();

            Android.Content.Res.AssetFileDescriptor afd = this.Assets.OpenFd(fullPath);
            if (afd != null)
            {
                player.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
                player.Prepare();
                player.Start();
                //player.Reset();

            }

        }
        public void SurfaceCreated(ISurfaceHolder holder)
        {
            Console.WriteLine("SurfaceCreated");
            player.SetDisplay(holder);
        }
        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            Console.WriteLine("SurfaceDestroyed");
        }
        public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int w, int h)
        {
            Console.WriteLine("SurfaceChanged");
        }
    }
}