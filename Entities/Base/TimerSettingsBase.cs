using Bluegrams.Application;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace AtomPresentationTimer.Entities.Base
{
    /// <summary>
    /// The timer settings base.
    /// </summary>
    public sealed  class TimerSettingsBase : ApplicationSettingsBase
    {
        private static TimerSettingsBase defaultInstance = new TimerSettingsBase();

        public static TimerSettingsBase Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [UserScopedSetting()]
        public String FormText
        {
            get { return (String)this["FormText"]; }
            set { this["FormText"] = value; }
        }
    }
}
