using System;
using System.Collections.Generic;
using System.Text;

namespace AtomPresentationTimer.Enums
{
    [Flags] enum TimerStatus 
    {
        Paused=1,
        Running=2,
        Stopped=4,
        //RunningWarning=8,
        //RunningExpired=16
    }
}
