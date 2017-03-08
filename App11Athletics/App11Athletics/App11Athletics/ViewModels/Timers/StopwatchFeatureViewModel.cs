using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11Athletics.ViewModels.Timers
{
    public class StopwatchFeatureViewModel : BaseTimerViewModel
    {
        public StopwatchFeatureViewModel()
        {
            LapTime = new ObservableCollection<string>();
            LapTime.Insert(0, TimerTimeSpan.ToString(""));
            LapTime.Clear();
            LapTimerCommand = new Command(() =>
            {
                if (TimerRunning)
                    InsertLapTime();
            }, () => TimerRunning);
            ResetTimerCommand = new Command(() =>
            {
                LapTime.Clear();
                TimerRunning = false;
                TimerTimeSpan = TimeSpan.Zero;
                ResetTimer();
                RefreshCanExecutes();
            }, () => !Reset && !TimerRunning);
        }

        private async void InsertLapTime()
        {

            await Task.Run(() => LapTime.Insert(0, TimerTimeSpan.ToString("hh':'mm':'ss':'ff")));
        }

        public ObservableCollection<string> LapTime { get; set; }

        #region Overrides of BaseTimerViewModel

        protected override void RefreshCanExecutes()
        {
            base.RefreshCanExecutes();
            ((Command)LapTimerCommand).ChangeCanExecute();
        }

        #endregion
    }
}