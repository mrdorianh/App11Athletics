using System;

namespace App11Athletics.ViewModels.Timers
{
    public class RoundCounterFeatureViewModel : BaseTimerViewModel
    {
        public RoundCounterFeatureViewModel()
        {
            TotalRoundTimeTimeSpan = TimeSpan.Zero;
            ElapsedTimeSpan = TimeSpan.Zero;
            CurrentRound = 1;
            TotalRounds = 1;
        }

        public TimeSpan TotalRoundTimeTimeSpan { get; set; }
        public double RoundTimeHoursTimeSpan { get; set; }
        public double RoundTimeMinutesTimeSpan { get; set; }
        public double RoundTimeSecondsTimeSpan { get; set; }
        public int CurrentRound { get; set; }
        public int TotalRounds { get; set; }
        public bool Stop { get; set; }
        public bool Paused { get; set; }

        public TimeSpan ElapsedTimeSpan { get; set; }

        public override bool OnTimerTick()
        {

            if (TimerRunning)
            {
                //                ElapsedTimeSpan = !Stop ? TimeSpan.Zero : DateTime.Now.Subtract(StartDateTime);
                TimerTimeSpan = ElapsedTimeSpan - DateTime.Now.Subtract(StartDateTime);
                //                TimerTimeSpan = DateTime.Now.Subtract(StartDateTime);
            }
            if (TimerTimeSpan < TotalRoundTimeTimeSpan && TimerTimeSpan > TimeSpan.Zero)
                return TimerRunning;

            //            Reset to original time
            TimerTimeSpan = TimeSpan.Zero;
            ElapsedTimeSpan = TotalRoundTimeTimeSpan;

            if (CurrentRound == TotalRounds)
            {
                ResetCommandMethod();
                return Stop;
            }
            CurrentRound = UpdateRound(CurrentRound, TotalRounds);
            StartDateTime = DateTime.Now;
            return TimerRunning;
        }

        #region Overrides of BaseTimerViewModel

        public override void ResetTimer()
        {
            CurrentRound = 1;
            ElapsedTimeSpan = TotalRoundTimeTimeSpan;

        }

        #region Overrides of BaseTimerViewModel

        #region Overrides of BaseTimerViewModel

        public override void StopTimer()
        {
            //            Stop = true;
            ElapsedTimeSpan = TimerTimeSpan;

            //            StartDateTime = DateTime.Now;
        }

        #endregion

        #endregion

        #endregion

        public static int UpdateRound(int currentRound, int totalRounds)
        {
            if (currentRound >= totalRounds)
            {
                return currentRound;

            }

            //            CrossVibrate.Current.Vibration();
            return currentRound + 1;
        }
    }
}