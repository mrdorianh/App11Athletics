using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App11Athletics.Helpers;
using FFImageLoading.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using PropertyChanged;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;


namespace App11Athletics.Views
{
    [ImplementPropertyChanged]
    public partial class debug : ContentPage
    {
        private IMediaPicker _mediaPicker;
        public IResolver resolver;

        public debug()
        {
            InitializeComponent();
            var tap = new TapGestureRecognizer();
            tap.Tapped += TapOnTapped;
            foreach (var imageView in this.grid.Children)
            {
                if ((imageView.GetType() != typeof(Image)) && (imageView.GetType() != typeof(CachedImage)))
                    return;

                imageView.GestureRecognizers.Add(tap);
            }




        }

        public ImageSource ProfileImage { get; set; }
        public string ImageInfo { get; set; } = "Hello There";

        private void CacheSuccess(object sender, CachedImageEvents.SuccessEventArgs e)
        {
            var n = e.ImageInformation.ToString();
            var h = e.ImageInformation.OriginalHeight;
            var w = e.ImageInformation.OriginalWidth;
            var s = e.ImageInformation.CurrentHeight;
            var sw = e.ImageInformation.CurrentWidth;

            var k = e.ImageInformation.CacheKey;

            //         var im =  FFImageLoading.ImageService.Instance.LoadFile(Settings.UserPicture);
            //            FFImageLoading.TaskParameterExtensions.PreloadAsync(parameters: im);
            this.ImageInfo = h.ToString() + "\r\n" + w.ToString() + "\r\n" + "\r\n" + s.ToString() + "\r\n" + sw.ToString() + "\r\n" + "\r\n" + k.ToString();
        }

        private void CachBaseeSuccess(object sender, CachedImageEvents.SuccessEventArgs e)
        {
            var n = e.ImageInformation.ToString();
            var h = e.ImageInformation.OriginalHeight;
            var w = e.ImageInformation.OriginalWidth;
            var s = e.ImageInformation.CurrentHeight;
            var sw = e.ImageInformation.CurrentWidth;

            var k = e.ImageInformation.CacheKey;

            this.ImageBaseInfo = h.ToString() + "\r\n" + w.ToString() + "\r\n" + "\r\n" + s.ToString() + "\r\n" + sw.ToString() + "\r\n" + "\r\n" + k.ToString();
        }

        public string ImageBaseInfo { get; set; } = "Hellllloooooo";

        private async void TapOnTapped(object sender, EventArgs eventArgs)
        {
            var d = await Task.Run(function: () => FFImageLoading.ImageService.Instance.Config.DiskCache);
            System.Diagnostics.Debug.WriteLine(d);
            await FFImageLoading.ImageService.Instance.InvalidateCacheAsync(cacheType: FFImageLoading.Cache.CacheType.All);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (status != PermissionStatus.Granted)
                status = (await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage))[Permission.Storage];
            if (status != PermissionStatus.Granted)
                return;

            await SelectPhoto();
        }

        private async Task SelectPhoto()
        {
            Setup();
            try
            {
                var mediaFile = await this._mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions { DefaultCamera = CameraDevice.Front, MaxPixelDimension = 400, });
                this.ProfileImage = mediaFile.Path;
            }
            catch (Exception exception) { }
        }

        private void Setup()
        {
            if (this._mediaPicker != null)
                return;

            var device = Resolver.Resolve<IDevice>();

            ////RM: hack for working on windows phone? 
            this._mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;
        }
    }
}