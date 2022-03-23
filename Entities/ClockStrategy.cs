using AtomPresentationTimer.Entities.Base;
using System;

namespace AtomPresentationTimer.Entities
{
    internal class ClockStrategy : TimerStrategyBase
    {
        DateTime current = DateTime.Now;
        public ClockStrategy() : base()
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
                        return Settings.TimerSettings.Default.ColorRunning;
                default:
                    return System.Drawing.Color.Black;

            }

        }

        public override OutputFormat Output()
        {

            return new OutputFormat(current.ToString(Settings.TimerSettings.Default.ClockFormat), GetCurrentColor());
        }

        protected override void Tick()
        {
            current = DateTime.Now;
        }


        public override void Reset()
        {
            base.Reset(() =>
            {
                current = DateTime.Now;
            });
            
        }

        

      
    }
}