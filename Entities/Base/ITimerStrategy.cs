using AtomPresentationTimer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomPresentationTimer.Entities.Base
{
    internal interface ITimerStrategy
    {
        delegate void OnTickHandler(object sender, TimerEventArgs e);
        delegate void OnStateChangedHandler(object sender, TimerEventArgs e);

        event OnTickHandler OnTick;
        event OnStateChangedHandler OnStateChanged;

        TimerStatus  Status { get;  }

        void Stop();
        void Start();
        void Pause();
        void Reset();
        OutputFormat Output();
    }
}
