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

        private readonly OpcServerManager _opcServerManager;

        private OpcServerDescription _opcServerDescription;

        private TreeNode<string> _opcNode;

        public List<string> Parameters;

        public Action<string> OnServerShutdownAction;

        public Action<List<OpcParameter>> OnReceivedDataAction;


        public void AddParameter(string parameter)
        {
            string par = Parameters.FirstOrDefault(p => p == parameter);

            if (par == null)
            {
                Parameters.Add(parameter);
            }
        }

        public void ClearParameters()
        {
            if (Parameters != null && Parameters.Any())
            {
                Parameters.Clear();
            }
        }

        public void RemoveParameter(string parameter)
        {
            if (Parameters == null || !Parameters.Any()) return;

            string par = Parameters.FirstOrDefault(p => p == parameter);

            if (par != null)
            {
                Parameters.ToList().Remove(parameter);
            }
        }

        public int TimeIntervalInMilliseconds
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["OpcRequestInterval"]); }
        }

        public OpcResponder()
        {
            Logger.InitLogger();

            _timer = new Timer(OnTimerTick, null, Timeout.Infinite, Timeout.Infinite);

            _opcServerManager = new OpcServerManager();

            _opcServerManager.OnServerShutdown += OnServerShutdown;
        }


        /// <summary>
        /// Ищет OPC сервер, берет первый найденный сервер.
        /// На сервере берет первый узел. (в дереве) 
        /// </summary>
        /// <returns></returns>
        public void ConfigureProcessor()
        {
            List<OpcServerDescription> servers = _opcServerManager.SelectServer();

            if (servers == null)
            {
                throw new Exception("opc servers is null");
            }

            if (!servers.Any())
            {
                throw new Exception("Не найден OPC сервер");
            }

            _opcServerDescription = servers[1];// TODO: find by name

            _opcServerManager.DoInit(_opcServerDescription);

            if (!_opcServerManager.OpcNamespacesTree.Children.Any())
            {
                throw new Exception("Не найден ни один узел OPC сервера");
            }

            _opcNode = _opcServerManager.OpcNamespacesTree.Children.First();// select firt child

            Parameters = _opcServerManager.BrowseToNodeInOpc(_opcNode.FullPath).Cast<string>().ToList();

            if (Parameters == null || Parameters.Count == 0)
            {
                throw new Exception("Не найден ни один параметр");
            }

            for (int i = 0; i < Parameters.Count; i++)
            {
                Parameters[i] = string.Format("PLC.PLC.{0}", Parameters[i]);
            }
        }


        /// <summary>
        /// Вызвать callback и параметром отдать данные с OPC сервера.
        /// </summary>
        /// <param name="parameters"></param>
        private void OnReceivedData(List<OpcParameter> parameters)
        {
            if (OnReceivedDataAction != null)
            {
                OnReceivedDataAction(parameters);
            }
        }
        

        private void OnServerShutdown(string errorText)
        {
            if (OnServerShutdownAction != null)
            {
                OnServerShutdownAction(errorText);
            }
        }


        public void ViewItem(string opcid) 
        {
            _opcServerManager.ViewItem(opcid);
        }

        public void ViewItems(List<string> opcids) 
        {
            _opcServerManager.ViewItems(opcids);
        }



        

        /// <summary>
        /// Остановит таймер, сделает ViewItem для ViewItem, запишет значение, Запустит таймет.
        /// </summary>
        public void WriteBitToOpc(BitParameters bitParameter, bool paramValue)
        {
            TimerStop();

            ViewItem(OpcConsts.Bits);

            List<OpcParameter> parameters = ProcessParameters(Parameters);

            OpcParameter parameter = parameters.FirstOrDefault(p => p.ParameterName == OpcConsts.Bits);

            if (parameter == null)
            {
                throw new Exception(string.Format("Can not process bit parameter {0}", bitParameter));
            }

            BitParameters bitParameters = (BitParameters)Convert.ToInt32(parameter.ParameterValue);

            if (paramValue)
            {
                bitParameters = bitParameters | bitParameter;
            }
            else
            {
                bitParameters = bitParameters & ~bitParameter;
            }

            int bitParametersAsInt = (int)bitParameters;

            _opcServerManager.WriteToOpc(bitParametersAsInt.ToString());

            TimerStart();
        }

        /// <summary>
        /// Остановит таймер, сделает ViewItem для ViewItem, запишет значение, Запустит таймет.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        public void WriteToOpc(string value, string paramName)
        {
            TimerStop();

            ViewItem(paramName);

            _opcServerManager.WriteToOpc(value);

            TimerStart();
        }

        public void WriteToOpcList(Dictionary<string, string> nameValueParameters) 
        {
            TimerStop();

            foreach (KeyValuePair<string, string> pair in nameValueParameters)
            {
                string paramName = pair.Key;
                string value = pair.Value;
                ViewItem(paramName);
                _opcServerManager.WriteToOpc(value);
            }

            TimerStart();
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


        public void OpcServerClose()
        {
            if (_opcServerManager != null)
            {
                _opcServerManager.Close();
            }
        }


        public List<OpcParameter> ProcessParameters(ICollection parameters)
        {
            return _opcServerManager.ProcessOpcNodeParams(new ArrayList(parameters));
        }

        

        private void OnTimerTick(object state)
        {
            try
            {
                DateTime now = DateTime.Now;
               

                int spendToWork = 0;

                //_opcServerManager.GrpRefresh2();

                _opcServerManager.ViewItems(Parameters);//обновляем параметры у опц сервера
                //или  узнать что лучше
                //ViewParametersByOneItem(_parameters);


                List<OpcParameter> parameters = ProcessParameters(Parameters);//получаем значения параметров

                OnReceivedData(parameters);

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
                OpcServerClose();
                ConfigureProcessor();
                TimerStart();
                //throw ex;
            }
        }

        private void ViewParametersByOneItem(IEnumerable<string> parameters)
        {
            foreach (string p in parameters)
            {
                _opcServerManager.ViewItem(p);
            }
        }
    }
}
