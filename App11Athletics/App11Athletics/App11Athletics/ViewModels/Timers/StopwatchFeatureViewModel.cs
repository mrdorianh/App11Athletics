using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App11Athletics.ViewModels.Timers
{
    public class StopwatchFeatureViewModel : BaseTimerViewModel
    {
        public StopwatchFeatureViewModel()
        {

            LapTimerCommand = new Command(() =>
            {
                if (TimerRunning)
                    LapTime.Insert(0, TimerTimeSpan.ToString("hh':'mm':'ss':'ff"));
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

        public ObservableCollection<string> LapTime { get; set; } = new ObservableCollection<string>();

        #region Overrides of BaseTimerViewModel

        protected override void RefreshCanExecutes()
        {
            base.RefreshCanExecutes();
            ((Command)LapTimerCommand).ChangeCanExecute();
        }

        #endregion
    }
}