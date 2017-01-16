using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;

namespace App11Athletics.ViewModels
{
    public class RoundCounterFeatureViewModel : BaseTimerViewModel
    {
        public RoundCounterFeatureViewModel()
        {
            TotalRoundTimeTimeSpan = TimeSpan.FromSeconds(10);
            CurrentRound = 1;
            TotalRounds = 69;
        }

        public override bool OnTimerTick()
        {
            if (TimerRunning)
            {
                TimerTimeSpan = DateTime.Now.Subtract(StartDateTime);
            }
            if (TimerTimeSpan < TotalRoundTimeTimeSpan)
                return TimerRunning;

            TimerTimeSpan = TimeSpan.Zero;
            CurrentRound = UpdateRound(CurrentRound, TotalRounds);
            StartDateTime = DateTime.Now;
            return TimerRunning;
        }

        #region Overrides of BaseTimerViewModel

        public override void ResetTimer()
        {
            CurrentRound = 1;
        }

        #endregion

        public static int UpdateRound(int currentRound, int totalRounds)
        {
            if (currentRound >= totalRounds)
            {
                return currentRound;

            }
            return currentRound + 1;
        }



        public TimeSpan TotalRoundTimeTimeSpan { get; set; }


        public double RoundTimeHoursTimeSpan { get; set; }
        public double RoundTimeMinutesTimeSpan { get; set; }
        public double RoundTimeSecondsTimeSpan { get; set; }
        public int CurrentRound { get; set; }
        public int TotalRounds { get; set; }

    }
}