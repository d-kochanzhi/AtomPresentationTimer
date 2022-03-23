using AtomPresentationTimer.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomPresentationTimer.Controllers
{
    internal class TimerController
    {
        public ITimerStrategy Strategy;

        #region singleton

        private static object syncRoot = new object();
        private static volatile TimerController _instance;
        public static TimerController GetInstance()
        {

            if (_instance == null)
            {
                lock (syncRoot)
                {
                    _instance = new TimerController();
                }
            }
            return _instance;
        }

        #endregion
        public void SetStrategy(ITimerStrategy strategy)
        {
            this.Strategy = strategy;
        }
    }
}
