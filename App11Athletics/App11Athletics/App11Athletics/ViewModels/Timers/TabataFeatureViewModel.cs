using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11Athletics.Views.Timers;
using App11Athletics.Views;
using Xamarin.Forms;


namespace App11Athletics.ViewModels.Timers
{
    public class TabataFeatureViewModel : RoundCounterFeatureViewModel
    {
        public TabataFeatureViewModel()
        {

            WorkRound = true;
            TotalRoundTimeTimeSpan = TimeSpan.FromSeconds(6);
            TotalTimeOffTimeSpan = TimeSpan.FromSeconds(5);
            CurrentRound = 1;
            TotalRounds = 5;

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
        public string WorkTime { get; set; }
        public bool Stop { get; private set; }

        //public string WorkTime
        //        {
        //            get
        //            {
        //                switch (WorkRound)
        //                {
        //                    case true:
        //                        return "Work Time!";
        //                    case false:
        //                        return "Rest Time";
        //                    default:
        //                        return string.Empty;
        //                }
        //            }
        //        }

        public override bool OnTimerTick()
        {
            if (CurrentRound == 1 && WorkRound && WorkTime != "WorkTime")
            {
                WorkTime = "WorkTime";

            }
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
                WorkTime = "Rest Time!";

                StartDateTime = DateTime.Now;
                return TimerRunning;
            }


            if (TimerTimeSpan < TotalTimeOffTimeSpan)
                return TimerRunning;

            TimerTimeSpan = TimeSpan.Zero;
            if (CurrentRound == TotalRounds)
            {
                ResetCommandMethod();
                WorkTime = "Finished!";
                return Stop;
            }
            CurrentRound = UpdateRound(CurrentRound, TotalRounds);
            WorkRound = true;
            WorkTime = "Work Time!";
            StartDateTime = DateTime.Now;
            return TimerRunning;
        }

        #region Overrides of RoundCounterFeatureViewModel

        public override void ResetTimer()
        {
            WorkRound = true;
            WorkTime = null;

            base.ResetTimer();
        }

        #endregion
    }
}