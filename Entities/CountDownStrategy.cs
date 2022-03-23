using AtomPresentationTimer.Entities.Base;
using System;

namespace AtomPresentationTimer.Entities
{
    internal class CountDownStrategy : TimerStrategyBase
    {
        private TimeSpan current;
        public CountDownStrategy() : base()
        {
            this.Reset();
        }

        private System.Drawing.Color GetCurrentColor() {
            switch (Status) {
                case Enums.TimerStatus.Paused:
                    return Settings.TimerSettings.Default.ColorPaused;
                    
                case Enums.TimerStatus.Stopped:
                    return Settings.TimerSettings.Default.ColorStoped;

                case Enums.TimerStatus.Running:
                    if(current.TotalSeconds>0 && current.TotalSeconds <= new TimeSpan(
                                                    (int)Settings.TimerSettings.Default.WarningHour,
                                                    (int)Settings.TimerSettings.Default.WarningMinute, 
                                                    (int)Settings.TimerSettings.Default.WarningSecond ).TotalSeconds)
                        return Settings.TimerSettings.Default.ColorWarning;
                    else if(current.TotalMilliseconds<0)
                        return Settings.TimerSettings.Default.ColorExpired;
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
            if (Status!= Enums.TimerStatus.Stopped && current.TotalSeconds == 0)
            {
                this.Stop();
                return;
            }
            if (Status == Enums.TimerStatus.Running)
            {
                current = current.Subtract(new TimeSpan(0, 0, 1));
            }                      

        }


        public override void Reset()
        {
            base.Reset(() =>
            {
                current = new TimeSpan(
                         (int)Settings.TimerSettings.Default.StartHour
                       , (int)Settings.TimerSettings.Default.StartMinute
                       , (int)Settings.TimerSettings.Default.StartSecond);
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