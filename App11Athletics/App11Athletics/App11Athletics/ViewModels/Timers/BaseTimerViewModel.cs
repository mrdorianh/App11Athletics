using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App11Athletics.ViewModels.Timers
{
    public class BaseTimerViewModel : INotifyPropertyChanged
    {
        public BaseTimerViewModel()
        {
            // Initialize any properties
            TimerTimeSpan = TimeSpan.Zero;

            // Initialize your Commands
            StartTimerCommand = new Command(() =>
            {
                TimerRunning = true;
                StartTimer();
                RefreshCanExecutes();
            }, () => !TimerRunning);
            StopTimerCommand = new Command(() =>
            {
                TimerRunning = false;
                StopTimer();
                RefreshCanExecutes();
            }, () => TimerRunning);
            ResetTimerCommand = new Command(ResetCommandMethod, () => !TimerRunning && !Reset);

        }

        public void ResetCommandMethod()
        {
            TimerRunning = false;
            TimerTimeSpan = TimeSpan.Zero;
            ResetTimer();
            RefreshCanExecutes();
        }

        public DateTime StartDateTime { get; set; }
        public TimeSpan ResumeDateTime { get; set; }
        public TimeSpan TimerTimeSpan { get; set; }
        public bool TimerRunning { get; set; }
        public bool Reset => TimerTimeSpan == TimeSpan.Zero;

        public ICommand ResetTimerCommand { get; set; }
        public ICommand StopTimerCommand { get; set; }
        public ICommand StartTimerCommand { get; set; }
        public ICommand LapTimerCommand { get; set; }

        #region INotifyPropertyChanged Members

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #endregion

        protected virtual void RefreshCanExecutes()
        {
            ((Command)StartTimerCommand).ChangeCanExecute();
            ((Command)StopTimerCommand).ChangeCanExecute();
            ((Command)ResetTimerCommand).ChangeCanExecute();

        }

        public virtual void StartTimer()
        {
            StartDateTime = Reset ? DateTime.Now : DateTime.Now.Subtract(ResumeDateTime);
            Device.StartTimer(TimeSpan.FromMilliseconds(15), OnTimerTick);
        }

        public virtual bool OnTimerTick()
        {
            if (TimerRunning)
                TimerTimeSpan = DateTime.Now.Subtract(StartDateTime);
            return TimerRunning;
        }

        public virtual void StopTimer()
        {
            ResumeDateTime = TimerTimeSpan;
        }

        public virtual void ResetTimer()
        {

        }
    }
}