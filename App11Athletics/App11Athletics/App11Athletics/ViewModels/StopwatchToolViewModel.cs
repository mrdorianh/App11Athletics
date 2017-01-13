using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;

namespace App11Athletics.ViewModels
{
    public class StopwatchToolViewModel : INotifyPropertyChanged
    {
        public StopwatchToolViewModel()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(15), OnTimerTick);
            DownloadCommand = new Command(async () => await DownloadAsync());
        }

        public Command DownloadCommand { get; set; }

        bool OnTimerTick()
        {
            DateTime = DateTime.Now;
            return true;
        }

        public DateTime DateTime { get; set; }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        async Task DownloadAsync()
        {
            await Task.Run(() => Download());
        }

        void Download() {}
    }
}