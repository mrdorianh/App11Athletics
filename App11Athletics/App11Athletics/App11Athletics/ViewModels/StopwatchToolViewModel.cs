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
    public class StopwatchToolViewModel : INotifyPropertyChanged
    {
        public StopwatchToolViewModel()
        {
            // Initialize any properties
            TimerDateTime = TimeSpan.Zero;

            // Initialize your Commands
            StartTimerCommand = new Command(execute: async () =>
            {
                TimerRunning = true;
                await StartTimerAsync();
                 RefreshCanExecutes(); 
            }, canExecute: () => !TimerRunning);

            StopTimerCommand = new Command( () =>
                {
                    TimerRunning = false;
                    StopTimer();
                    RefreshCanExecutes(); 
                }, canExecute:
                () => TimerRunning);
        
            ResetTimerCommand = new Command(async () =>
            {
                TimerRunning = false;
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

        #region Properties

        public DateTime StartDateTime { get; set; }
        public TimeSpan ResumeDateTime { get; set; }
        public TimeSpan TimerDateTime { get; set; }
       
        public bool TimerRunning { get; set; }
        public bool Reset => TimerDateTime == TimeSpan.Zero;
        public bool StartNew => true;
        public bool Resume => true;

        #endregion

        #region Implementation of ICommand

        public ICommand StartTimerCommand { get; set; }

        public ICommand StopTimerCommand { get; set; }

        public ICommand ResetTimerCommand { get; set; }

        #endregion

        #region Methods

        private async Task StartTimerAsync()
        {
            await Task.Run(() => StartTimer());
        }

        private void StartTimer()
        {
            // Check if the timer is already reset
            StartDateTime = Reset ? DateTime.Now : DateTime.Now.Subtract(ResumeDateTime);
            
            Device.StartTimer(TimeSpan.FromMilliseconds(15), OnTimerTick);
        }

        private bool OnTimerTick()
        {
            if (TimerRunning)
            {
                TimerDateTime = DateTime.Now.Subtract(StartDateTime);
            }
            return TimerRunning;
        }

        private async Task StopTimerAsync()
        {


            await Task.Run(() => StopTimer());
        }

        public void StopTimer()
        {
            ResumeDateTime = TimerDateTime;
        }

        private async Task ResetTimerAsync()
        {
          
           await Task.Run(() => ResetTimer());
                
            
        }

        private void ResetTimer()
        {
            TimerDateTime = TimeSpan.Zero;
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #endregion
    }
}