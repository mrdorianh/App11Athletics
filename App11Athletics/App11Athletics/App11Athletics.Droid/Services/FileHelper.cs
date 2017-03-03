using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App11Athletics.Droid.Services;
using App11Athletics.Helpers;
using Xamarin.Forms;
using App11Athletics.Droid;
using System.IO;
using Environment = System.Environment;

[assembly: Dependency(typeof(FileHelper))]
namespace App11Athletics.Droid.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}