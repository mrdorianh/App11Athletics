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
            TimerDateTime = TimeSpan.Zero;
            StartTimerCommand = new Command(async () => { await StartTimerAsync(); });
            StopTimerCommand = new Command(async () => { await StopTimerAsync(); });
            ResetTimerCommand = new Command(async () => { await ResetTimerAsync(); });
        }

        public DateTime StartDateTime { get; set; }
        public TimeSpan TimerDateTime { get; set; }
        public bool TimerRunning { get; set; }
        public bool Reset { get; set; }

        public ICommand StartTimerCommand { get; set; }

        public ICommand StopTimerCommand { get; set; }

        public ICommand ResetTimerCommand { get; set; }

        private async Task StartTimerAsync()
        {
            await Task.Run(() => StartTimer());
        }

        private void StartTimer()
        {
            StartDateTime = DateTime.Now;
            TimerRunning = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(15), OnTimerTick);
        }

        private async Task StopTimerAsync()
        {
            await Task.Run(() => StopTimer());
        }

        public void StopTimer()
        {
            TimerDateTime = TimerDateTime;
            TimerRunning = false;
        }

        private async Task ResetTimerAsync()
        {
          var t = await Task.Run(() =>TimerRunning = false);
           ResetTimer();
            
        }

        private void ResetTimer()
        {  
            TimerDateTime = TimeSpan.Zero;
        }

        private bool OnTimerTick()
        {
            TimerDateTime = DateTime.Now.Subtract(StartDateTime);
            return TimerRunning;
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}