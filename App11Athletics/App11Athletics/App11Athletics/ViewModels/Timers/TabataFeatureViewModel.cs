using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11Athletics.ViewModels.Timers
{
    public class TabataFeatureViewModel : RoundCounterFeatureViewModel
    {
        public TabataFeatureViewModel()
        {

            WorkRound = true;
            TotalRoundTimeTimeSpan = TimeSpan.FromSeconds(10);
            TotalTimeOffTimeSpan = TimeSpan.FromSeconds(5);
            CurrentRound = 1;
            TotalRounds = 15;
        }

        public TimeSpan TotalTimeOffTimeSpan { get; set; }

        public int TotalOffRounds
        {
            get { return TotalRounds - 1; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        public bool WorkRound { get; set; }
        public string WorkTime
        {
            get
            {
                switch (WorkRound)
                {
                    case true:
                        return "Work Time!";
                    case false:
                        return "RestTime";
                    default:
                        return string.Empty;
                }
            }
        }


        public override bool OnTimerTick()
        {
            if (TimerRunning)
            {
                TimerTimeSpan = DateTime.Now.Subtract(StartDateTime);
            }

            if (WorkRound)
            {
                if (TimerTimeSpan < TotalRoundTimeTimeSpan)
                {
                    return TimerRunning;
                }
                TimerTimeSpan = TimeSpan.Zero;
                WorkRound = false;
                StartDateTime = DateTime.Now;
                return TimerRunning;
            }


            if (TimerTimeSpan < TotalTimeOffTimeSpan)
                return TimerRunning;

            TimerTimeSpan = TimeSpan.Zero;
            CurrentRound = UpdateRound(CurrentRound, TotalRounds);
            WorkRound = true;
            StartDateTime = DateTime.Now;
            return TimerRunning;
        }

        #region Overrides of RoundCounterFeatureViewModel

        public override void ResetTimer()
        {
            WorkRound = true;
            base.ResetTimer();
        }

        #endregion
    }
}