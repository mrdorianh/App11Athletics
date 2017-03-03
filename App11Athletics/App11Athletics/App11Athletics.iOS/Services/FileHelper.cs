using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using App11Athletics.Helpers;
using App11Athletics.iOS.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace App11Athletics.iOS.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}