using System;

namespace AtomPresentationTimer.Entities
{
    internal class TimerEventArgs : EventArgs
    {
        public string Message { get; set; }

        public TimerEventArgs(string message)
        {
            Message = message;
        }
    }
}