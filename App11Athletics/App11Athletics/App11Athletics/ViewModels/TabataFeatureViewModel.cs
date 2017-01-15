using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App11Athletics.ViewModels
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

        public override bool OnTimerTick()
        {
            if (TimerRunning)
            {
                TimerTimeSpan = DateTime.Now.Subtract(StartDateTime);
            }

            if (WorkRound)
                if (TimerTimeSpan < TotalRoundTimeTimeSpan)
                {
                    return TimerRunning;
                }
            WorkRound = false;
            if (!WorkRound)
            {
                if (TimerTimeSpan < TotalTimeOffTimeSpan)
                    return TimerRunning;
            }

            //            TimerTimeSpan = TimeSpan.Zero;
            CurrentRound = UpdateRound(CurrentRound, TotalRounds);
            StartDateTime = DateTime.Now;
            return TimerRunning;
        }

        public bool WorkRound { get; set; }
    }
}