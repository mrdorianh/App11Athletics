using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;

namespace App11Athletics.ViewModels
{
    public class BaseTimerViewModel : INotifyPropertyChanged
    {
        public BaseTimerViewModel()
        {
            // Initialize any properties
            TimerTimeSpan = TimeSpan.Zero;

            // Initialize your Commands
            StartTimerCommand = new Command(execute: () =>
            {
                TimerRunning = true;
                StartTimer();
                RefreshCanExecutes();
            }, canExecute: () => !TimerRunning);
            StopTimerCommand = new Command(() =>
            {
                TimerRunning = false;
                StopTimer();
                RefreshCanExecutes();
            }, canExecute: () => TimerRunning);
            ResetTimerCommand = new Command(() =>
            {
                TimerRunning = false;
                TimerTimeSpan = TimeSpan.Zero;
                ResetTimer();
                RefreshCanExecutes();
            }, () => !Reset || TimerRunning);
        }

        private void RefreshCanExecutes()
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
            {
                TimerTimeSpan = DateTime.Now.Subtract(StartDateTime);
            }

            return TimerRunning;
        }

        public void StopTimer()
        {
            ResumeDateTime = TimerTimeSpan;
        }

        public virtual void ResetTimer()
        {

        }

        public DateTime StartDateTime { get; set; }
        public TimeSpan ResumeDateTime { get; set; }
        public TimeSpan TimerTimeSpan { get; set; }
        public bool TimerRunning { get; set; }
        public bool Reset => TimerTimeSpan == TimeSpan.Zero;

        public ICommand ResetTimerCommand { get; set; }
        public ICommand StopTimerCommand { get; set; }
        public ICommand StartTimerCommand { get; set; }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}