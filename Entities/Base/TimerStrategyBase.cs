using AtomPresentationTimer.Enums;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace AtomPresentationTimer.Entities.Base
{
    internal abstract class TimerStrategyBase : ITimerStrategy, IDisposable
    {
        public event ITimerStrategy.OnTickHandler OnTick;
        public event ITimerStrategy.OnStateChangedHandler OnStateChanged;

        private readonly System.Timers.Timer _timer;
        private TimerStatus _status;
        private SynchronizationContext _syncContext;
        private int _counter = 1;

        public TimerStatus Status
        {
            get => _status;
            private set
            {
                _status = value;
                OnStateChanged?.Invoke(this, new TimerEventArgs($"Status changed to [{value}]"));
            }
        }

        public TimerStrategyBase()
        {
            _syncContext = SynchronizationContext.Current;


            _timer = new System.Timers.Timer();

            _timer.Interval = 1000;
            _timer.Elapsed += _timer_tick;
            _timer.AutoReset = true;
            _timer.Enabled = true;

            Status = TimerStatus.Paused;
        }

        private void _timer_tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Status == Enums.TimerStatus.Running)
                _syncContext.Send(state =>
            {
                Tick();
                OnTick?.Invoke(this, new TimerEventArgs($"Timer ticks[{string.Concat(Enumerable.Repeat(".", _counter > 3 ? _counter = 1 : _counter += 1))}]"));

            }, null);

        }

        protected abstract void Tick();

        public virtual void Pause() { this.Pause(() => { }); }
        protected void Pause(Action action)
        {
            if (Status == TimerStatus.Running)
            {
                action();
                this.Status = TimerStatus.Paused;
            }
        }

        public virtual void Reset() { this.Reset(() => { }); }
        protected void Reset(Action action)
        {
            if (Status == TimerStatus.Paused
                    || Status == TimerStatus.Stopped)
            {
                action();
                this.Status = TimerStatus.Paused;
            }
        }

        public virtual void Start() { this.Start(() => { }); }
        protected void Start(Action action)
        {
            if (Status == TimerStatus.Paused)
            {
                action();
                this.Status = TimerStatus.Running;
            }
        }

        public virtual void Stop() { this.Stop(() => { }); }
        protected void Stop(Action action)
        {
            if (Status == TimerStatus.Paused
                  || Status == TimerStatus.Running)
            {
                action();
                this.Status = TimerStatus.Stopped;
            }
        }

        public abstract OutputFormat Output();

        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
        }


    }
}