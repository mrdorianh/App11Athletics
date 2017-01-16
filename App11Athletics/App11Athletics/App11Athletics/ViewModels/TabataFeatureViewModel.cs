using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11Athletics.Annotations;

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
            if (!TimerRunning)
                return TimerRunning;

            TimerTimeSpan = DateTime.Now.Subtract(StartDateTime);


            if (WorkRound)
                if (TimerTimeSpan < TotalRoundTimeTimeSpan)
                {
                    return TimerRunning;
                }
                else
                {
                    WorkRound = false;
                    StartDateTime = DateTime.Now;
                    return TimerRunning;
                }

            if (TimerTimeSpan < TotalTimeOffTimeSpan)
                return TimerRunning;


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

        public bool WorkRound { get; set; }

        public string DebugMessage => WorkRound.ToString();
    }
}