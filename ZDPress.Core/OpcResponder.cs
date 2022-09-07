using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace ZDPress.Opc
{
    /// <summary>
    /// Опрашивает ОПЦ сервер с заданным промежутком, результат опроса возвращает в callback.
    /// </summary>
    public class OpcResponder
    {
        private readonly Timer _timer;

        public List<string> Parameters;

        public string FakeString;

        public Action<string> OnReceivedDataAction;

        public int TimeIntervalInMilliseconds
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["OpcRequestInterval"]); }
        }

        public OpcResponder()
        {
            Logger.InitLogger();

            _timer = new Timer(OnTimerTick, null, Timeout.Infinite, Timeout.Infinite);

            //_opcServerManager = new OpcServerManager();
        }

        /// <summary>
        /// Вызвать callback и параметром отдать данные с OPC сервера.
        /// </summary>
        /// <param name="parameters"></param>
        private void OnReceivedData( string FakeString)
        {
            OnReceivedDataAction?.Invoke(FakeString);
        }

        /// <summary>
        /// Запустить таймер.
        /// </summary>
        public void TimerStart()
        {
            TimerIsRunning = true;

            _timer.Change(TimeIntervalInMilliseconds, Timeout.Infinite);
        }

        /// <summary>
        /// Остановить таймер.
        /// </summary>
        public void TimerStop()
        {
            TimerIsRunning = false;

            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public bool TimerIsRunning { get; private set; }

        private void OnTimerTick(object state)
        {
            try
            {
                DateTime now = DateTime.Now;
               
                int spendToWork = 0;

                OnReceivedData(FakeString);

                spendToWork = (int)(DateTime.Now - now).TotalMilliseconds;
                
                int nextAfter = TimeIntervalInMilliseconds - spendToWork;

                if (nextAfter < 0)
                {
                   nextAfter = 0;
                }

                if (TimerIsRunning)
                {
                    _timer.Change(nextAfter, Timeout.Infinite);
                }
            }
            catch (Exception ex)
            {
                TimerStop();
                Logger.Log.Error(ex.Message);
                
                TimerStart();
                //throw ex;
            }
        }
    }
}
