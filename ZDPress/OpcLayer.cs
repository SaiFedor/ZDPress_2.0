using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using ZDPress.Dal;
using System.Data;
using ZDPress.Dal.Entities;
using ZDPress.Opc;
using ZDPress.UI.Views;
using ZDPress.UI.Reports;

namespace ZDPress.UI
{
    public class OpcLayer
    {
        private static ZdPressDal _dal;

        public static Action<PressOperation> OperationCreated;

        public static Action<PressOperationData> OperationDataCreated;

        public bool WithFakeData { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["WithFakeOpc"]); } }

        public static System.Windows.Forms.Timer FakeTestTimer;
        public Random Rundomizer { get; set; }
        private int cc;
        public void StartWork()
        {
            _dal = new ZdPressDal();


            if (WithFakeData)
            {
                Rundomizer = new Random(1);

                FakeTestTimer = new System.Windows.Forms.Timer { Interval = 3000 };

                FakeTestTimer.Tick += (e, a) =>
                {
                    PressOperationData od = new PressOperationData
                    {
                        ShowGraph = true,
                        DlinaSopr = cc++ * 20 + Rundomizer.Next(2),
                        DispPress = cc + Rundomizer.Next(200) * 10
                    };

                    if (cc % 10 == 0)
                    {
                        od.ShowGraph = false;
                        cc = 0;
                    }

                    if (CurrentPressOperation.PressOperationData == null)
                    {
                        CurrentPressOperation.PressOperationData = new BindingList<PressOperationData>();
                    }

                    FireOperationDataCreated(od);

                    SavePressOperationData(od);
                };

                FakeTestTimer.Start();
            }
            else
            {
                MainConstant.StartRead();
                OpcResponderSingleton.Instance.OnReceivedDataAction += OnReceivedData;
                OpcResponderSingleton.Instance.TimerStart();
            }
        }


        public static PressOperation CurrentPressOperation = new PressOperation
        {
            WheelType = "РУ1Ш",
            PressOperationData = new BindingList<PressOperationData>()
        };


        private static void OnReceivedData(string FakeString)
        {
            ProcessReceivedData();
        }
        private static void ProcessReceivedData()
        {
            PressOperationData data = PressOperationData.ConvertToPressDataItem();

            SavePressOperationData(data);
        }

        private static int? _lastDlinaSopr = null;

        private static void SavePressOperationData(PressOperationData data)
        {
            bool needCreatePressOperation = data.ShowGraph && CurrentPressOperation.Id == 0;

            if (needCreatePressOperation)
            {
                CurrentPressOperation = CreatePressOperation();

            }

            bool changeSopr = _lastDlinaSopr != data.DlinaSopr && data.DlinaSopr < 300;

            System.Diagnostics.Trace.WriteLine(string.Format("_lastDlinaSopr"));

            _lastDlinaSopr = data.DlinaSopr;

            bool needCreatePressOperationData = data.ShowGraph && changeSopr;
            
            if (needCreatePressOperationData)
            {
                if (CurrentPressOperation.Id == 0)
                {
                    throw new Exception("Press operaion was not created!");
                }

                data.PressOperationId = CurrentPressOperation.Id;

                data.DispPress = data.DispPress / 100;//потому что надо

                _dal.InsertPressOperationData(data);

                if (CurrentPressOperation.PressOperationData == null)
                {
                    CurrentPressOperation.PressOperationData = new BindingList<PressOperationData>();
                }
            }

            bool pressOperationWasFinished = !data.ShowGraph && CurrentPressOperation.Id != 0;

            if (pressOperationWasFinished)
            {
                // TODO: проверить что обновилось на форме
                ChartForm form = (ChartForm)UiHelper.GetFormSingle(typeof(ChartForm));
                if (form.ViewModel != null)
                {
                    var operation = form.ViewModel.PressOperation;
                    ReportDto reportDto = form.GetReportDto();

                    operation.Id = CurrentPressOperation.Id;

                    _dal.UpdatePressOperationFieldTotal("OperationStop", operation, DateTime.Now, DbType.DateTime);

                    _lastDlinaSopr = 0;
                    CurrentPressOperation.Id = 0;
                }
               
            }
        }

        private static PressOperation CreatePressOperation()
        {
            PressOperation operation = new PressOperation
            {
                PressOperationData = new BindingList<PressOperationData>(),
                OperationStart = DateTime.Now
            };

            operation.Id = _dal.SaveOrUpdatePressOperation(operation);

            FireOperationCreated(operation);

            return operation;
        }


        private static void FireOperationCreated(PressOperation operation)
        {
            if (OperationCreated != null) OperationCreated(operation);
        }

        
        private static void FireOperationDataCreated(PressOperationData data)
        {
            if (OperationDataCreated != null) OperationDataCreated(data);
        }
    }
}
