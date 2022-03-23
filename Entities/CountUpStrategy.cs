using AtomPresentationTimer.Entities.Base;
using System;

namespace AtomPresentationTimer.Entities
{
    internal class CountUpStrategy : TimerStrategyBase
    {
        private TimeSpan current;
        public CountUpStrategy() : base()
        {
            this.Reset();
        }

        private System.Drawing.Color GetCurrentColor()
        {
            switch (Status)
            {
                case Enums.TimerStatus.Paused:
                    return Settings.TimerSettings.Default.ColorPaused;

                case Enums.TimerStatus.Stopped:
                    return Settings.TimerSettings.Default.ColorStoped;

                case Enums.TimerStatus.Running:
                    if (current.TotalSeconds >= new TimeSpan(
                                                    (int)Settings.TimerSettings.Default.UpWarningHour,
                                                    (int)Settings.TimerSettings.Default.UpWarningMinute,
                                                    (int)Settings.TimerSettings.Default.UpWarningSecond).TotalSeconds
                                                    &&
                                                    new TimeSpan(
                                                    (int)Settings.TimerSettings.Default.UpWarningHour,
                                                    (int)Settings.TimerSettings.Default.UpWarningMinute,
                                                    (int)Settings.TimerSettings.Default.UpWarningSecond).TotalSeconds>0)
                        return Settings.TimerSettings.Default.ColorWarning;                 
                    else
                        return Settings.TimerSettings.Default.ColorRunning;
                default:
                    return System.Drawing.Color.Black;

            }

        }

        public override OutputFormat Output()
        {

            return new OutputFormat(current.ToString(@"hh\:mm\:ss"), GetCurrentColor());
        }

        protected override void Tick()
        {           
            if (Status == Enums.TimerStatus.Running)
            {
                current = current.Add(new TimeSpan(0, 0, 1));
            }
        }


        public override void Reset()
        {
            base.Reset(() =>
            {
                current = new TimeSpan(
                         (int)Settings.TimerSettings.Default.UpStartHour
                       , (int)Settings.TimerSettings.Default.UpStartMinute
                       , (int)Settings.TimerSettings.Default.UpStartSecond);
            });
            
        }

        public override void Stop()
        {
            base.Stop(() =>
            {
                current = new TimeSpan(0, 0, 0);
            });
        }

      
    }
}