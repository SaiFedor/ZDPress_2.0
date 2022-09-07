using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ZDPress.Dal;
using ZDPress.Dal.Entities;
using ZDPress.Opc;
using ZDPress.UI.Common;
using ZDPress.UI.Reports;
using ZDPress.UI.ViewModels;
using System.Security.Principal;

namespace ZDPress.UI.Views
{
    public partial class ChartForm : Form
    {
        public ChartFormShowMode Mode { get; set; }
        
        public ZdPressDal Dal { get; set; }
        public String TestString { get; set; }
        public Timer TimerForUpdate { get; set; }

        public ChartForm()
        {
            InitializeComponent();

            CustomizeChart();
            Dal = new ZdPressDal();
            _tooltip = new ToolTip();

            Mode = ChartFormShowMode.ShowSavedOperation;

            TimerForUpdate = new Timer {Interval = 1000};

            TimerForUpdate.Tick += TimerForUpdate_Tick;

            OpcResponderSingleton.Instance.OnReceivedDataAction += OnReceivedData;
           
        }

        private void OnReceivedData(string FakeStr)
        {
            PressOperationData data = PressOperationData.ConvertToPressDataItem();

            if (zdButton5.Enabled != !data.ShowGraph)
            {
                zdButton5.Invoke(new Action(() =>
                {
                    zdButton5.Enabled = !data.ShowGraph;//активность кнопки (назад)
                }));
            }
           
        }

        private void TimerForUpdate_Tick(object sender, EventArgs e)
        {
            Tuple<List<PressOperationData>, int> data = Dal.GetOperationData(0, 0);

            
                if (zdGrid1.Rows.Count != data.Item1.Count)
                {
                    zdChart2.Invoke(new Action(() =>
                    {
                        zdChart2.DataSource = data.Item1;
                        zdChart2.DataBind();
                    }));
                }

                zdGrid1.Invoke(new Action(() =>
                {
                    if (zdGrid1.Rows.Count != data.Item1.Count)
                    {
                        zdGrid1.DataSource = data.Item1;
                        zdGrid1.Refresh();
                    }
                    
                }));

                if (data.Item1 != null && data.Item1.Any())
	            {
                    textBoxSopr.Invoke(new Action(() => 
                    {
                        _viewModel.UpdateMaxSoprFromDb(data.Item1.First().PressOperationId);
                    }));

                    textBoxMaxUsil.Invoke(new Action(() =>
                    {
                        _viewModel.UpdateMaxUsilZapreFromDb(data.Item1.First().PressOperationId);
                    }));

                    textBoxUsil100.Invoke(new Action(() =>
                    {
                        textBoxUsil100.Text = _viewModel.PressOperation.Power100Mm.ToString(CultureInfo.InvariantCulture);
                    }));


                    textBoxDlinaPramUch.Invoke(new Action(() =>
                    {
                        _viewModel.UpdateDlinaPramUchFromDb(data.Item1.First().PressOperationId);
                    }));
	            }

            _viewModel.UpdatePlcStatus();
        }


        private readonly ToolTip _tooltip;


        private Point? _clickPosition;


        private void zdChart2_MouseClick(object sender, MouseEventArgs e)
        {
            Point pos = e.Location;

            _clickPosition = pos;

            HitTestResult[] results = zdChart2.HitTest(pos.X, pos.Y, false, ChartElementType.PlottingArea);

            foreach (HitTestResult result in results)
            {
                if (result.ChartElementType != ChartElementType.PlottingArea) continue;

                double xVal = result.ChartArea.AxisX.PixelPositionToValue(pos.X);
                double yVal = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);

                _tooltip.Show("X=" + xVal + ", Y=" + yVal, zdChart2, e.Location.X, e.Location.Y - 15);
            }
        }


        private void zdChart2_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (!_clickPosition.HasValue || e.Location == _clickPosition) return;

            _tooltip.RemoveAll();

            _clickPosition = null;
        }



        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);

                if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    Program.PressContext.CurrentUser.Role = ControlBasedRightsManager.InitializeRoleByLogin(AppUser.Admin);
                }
                else
                {
                    Program.PressContext.CurrentUser.Role = ControlBasedRightsManager.InitializeRoleByLogin(AppUser.User);
                }
            }

            ControlBasedRightsManager.ConfigureControlsByRole(this, 
                Mode == ChartFormShowMode.ShowSavedOperation ? Program.PressContext.CurrentUser.Role : null);


            if (zdChart2.Series[0].Points.Count == 0)
            {
                zdChart2.Series[0].Points.AddXY(0, 0);
            }

            zdChart2.Select();//chart get focus

            zdChart2.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            zdChart2.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);

            if (Mode == ChartFormShowMode.ShowCurrentOperation)
            {
                TimerForUpdate.Start();
            }
            else
            {
                TimerForUpdate.Stop();
            }
        }

      


        private ChartFormViewModel _viewModel;
        public ChartFormViewModel ViewModel
        {
            get 
            {
                //if (_viewModel != null && _viewModel.PressOperation !=  null)
                //{
                //    textBoxPods.Invoke(new Action(() => { _viewModel.PressOperation.DWheel = string.IsNullOrWhiteSpace(textBoxPods.Text) ? 0.0M : decimal.Parse(textBoxPods.Text.Replace('.', ',')); }));//ViewModel.PressOperation.DWheel.ToString(CultureInfo.InvariantCulture),}));
                //    textBoxOtv.Invoke(new Action(() => { _viewModel.PressOperation.DAxis = string.IsNullOrWhiteSpace(textBoxOtv.Text) ? 0.0M : decimal.Parse(textBoxOtv.Text.Replace('.', ','));  }));//ViewModel.PressOperation.DAxis.ToString(CultureInfo.InvariantCulture),}));
                //    textBoxDlin.Invoke(new Action(() => { _viewModel.PressOperation.LengthStup = string.IsNullOrWhiteSpace(textBoxDlin.Text) ? 0 : int.Parse(textBoxDlin.Text); }));//ViewModel.PressOperation.LengthStup.ToString(),)); 
                //}
                return _viewModel; 
            }
            set
            {
               // if (_viewModel != null && _viewModel.PressOperation != null)
                {
                    //textBoxPods.Invoke(new Action(() => { textBoxPods.Text = _viewModel.PressOperation.DWheel.ToString();}));//ViewModel.PressOperation.DWheel.ToString(CultureInfo.InvariantCulture),}));
                    //textBoxOtv.Invoke(new Action(() => { _viewModel.PressOperation.DAxis = string.IsNullOrWhiteSpace(textBoxOtv.Text) ? 0.0M : decimal.Parse(textBoxOtv.Text.Replace('.', ',')); }));//ViewModel.PressOperation.DAxis.ToString(CultureInfo.InvariantCulture),}));
                    //textBoxDlin.Invoke(new Action(() => { _viewModel.PressOperation.LengthStup = string.IsNullOrWhiteSpace(textBoxDlin.Text) ? 0 : int.Parse(textBoxDlin.Text); }));//ViewModel.PressOperation.LengthStup.ToString(),)); 
                }
                _viewModel = value;
                BindViewModel();//TODO: разобраться почему не обновляется биндинг
            }
        }



        private void ClearBinding()
        {
            textBoxNomerDiagrammi.DataBindings.Clear();
            textBoxNomerZavoda.DataBindings.Clear();
            textBoxNomerOsi.DataBindings.Clear();
            comboBoxTipColesPary.DataBindings.Clear();
            comboBoxStorona.DataBindings.Clear();
            textBoxNomerKlesa.DataBindings.Clear();
            textBoxPods.DataBindings.Clear();
            textBoxOtv.DataBindings.Clear();
            textBoxDlin.DataBindings.Clear();
            textBoxNatag.DataBindings.Clear();
            textBoxSopr.DataBindings.Clear();
            textBoxUsil100.DataBindings.Clear();
            textBoxMaxUsil.DataBindings.Clear();
            textBoxDlinaPramUch.DataBindings.Clear();
            buttonSaveOperation.DataBindings.Clear();
            zdLabel27.DataBindings.Clear();
            label13.DataBindings.Clear();
        }


        public void BindViewModel()
        {
            ClearBinding();

            textBoxNomerDiagrammi.DataBindings.Add(new Binding("Text", ViewModel, "PressOperation.DiagramNumber", true, DataSourceUpdateMode.OnPropertyChanged));
            textBoxNomerZavoda.DataBindings.Add(new Binding("Text", ViewModel, "PressOperation.FactoryNumber", true, DataSourceUpdateMode.OnPropertyChanged));
            textBoxNomerOsi.DataBindings.Add(new Binding("Text", ViewModel, "PressOperation.AxisNumber", true, DataSourceUpdateMode.OnPropertyChanged));
            comboBoxTipColesPary.DataBindings.Add(new Binding("SelectedItem", ViewModel, "PressOperation.WheelType", true, DataSourceUpdateMode.OnPropertyChanged));
            comboBoxStorona.DataBindings.Add(new Binding("SelectedItem", ViewModel, "PressOperation.Side", true, DataSourceUpdateMode.OnPropertyChanged));
            textBoxNomerKlesa.DataBindings.Add(new Binding("Text", ViewModel, "PressOperation.WheelNumber", true, DataSourceUpdateMode.OnPropertyChanged));
            //TODO: прыгает курсор
            textBoxPods.DataBindings.Add(new Binding("Text", ViewModel, "PressOperation.DWheel", true, DataSourceUpdateMode.OnPropertyChanged));
            textBoxOtv.DataBindings.Add(new Binding("Text", ViewModel, "PressOperation.DAxis", true, DataSourceUpdateMode.OnPropertyChanged));
            textBoxDlin.DataBindings.Add(new Binding("Text", ViewModel, "PressOperation.LengthStup", true, DataSourceUpdateMode.OnPropertyChanged));
            textBoxNatag.DataBindings.Add(new Binding("Text", ViewModel, "PressOperation.Natiag", true, DataSourceUpdateMode.OnPropertyChanged));
            textBoxSopr.DataBindings.Add(new Binding("Text", ViewModel, "PressOperation.LengthSopriazh", true, DataSourceUpdateMode.OnPropertyChanged));
            textBoxUsil100.DataBindings.Add(new Binding("Text", ViewModel, "PressOperation.Power100Mm", true, DataSourceUpdateMode.OnPropertyChanged));
            textBoxMaxUsil.DataBindings.Add(new Binding("Text", ViewModel, "PressOperation.MaxPower", true, DataSourceUpdateMode.OnPropertyChanged));
            textBoxDlinaPramUch.DataBindings.Add(new Binding("Text", ViewModel, "PressOperation.LengthLines", true, DataSourceUpdateMode.OnPropertyChanged));
            zdLabel27.DataBindings.Add(new Binding("BackColor", ViewModel, "PlcConnectState", true, DataSourceUpdateMode.OnPropertyChanged));
            textBoxNatag.TextChanged += changeColor;
            if (!ViewModel.CanSaveOperation)
            {
                label13.DataBindings.Add(new Binding("Text", ViewModel, "PressOperation.Id", true, DataSourceUpdateMode.OnPropertyChanged));
                label13.TextChanged += PrintCmd1;
            }
            
            //TODO: разобраться почему не работает биндинг
            //buttonSaveOperation.DataBindings.Add(new Binding("Visible", ViewModel, "CanSaveOperation", true, DataSourceUpdateMode.OnPropertyChanged));

            if (_viewModel != null && _viewModel.PressOperation != null)
            {
                //refresh chart
                zdChart2.DataSource = _viewModel.PressOperation.PressOperationData.Where(p => p.DlinaSopr%2 == 0);
                zdChart2.DataBind();

                // refresh grid
                zdGrid1.DataSource = _viewModel.PressOperation.PressOperationData;
                zdGrid1.Refresh();
            }
        }

        private void changeColor(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxNatag.Text))
            {
                textBoxNatag.BackColor = Convert.ToDecimal(textBoxNatag.Text) > 2 ? System.Drawing.Color.Red : System.Drawing.Color.White;
            }
        }



        private void CustomizeChart()
        {
            zdChart2.Series[0]["LineTension"] = "0";
            zdChart2.Series[0].BorderWidth = 2; //вот
            //zdChart2.Series[0]. = 2;//хз может оно
            //zdChart2.Series[0]. = 2;//хз может оно
            zdChart2.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Solid;
            zdChart2.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Solid;

            zdChart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            zdChart2.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            
            var dfd = zdChart2.ChartAreas[0].AxisY.LineWidth;
            zdChart2.ChartAreas[0].AxisY.LineWidth = 1;


            zdChart2.ChartAreas[0].AxisX.Maximum = 240;
            zdChart2.ChartAreas[0].AxisX.Minimum = 0;
            zdChart2.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Horizontal;


            zdChart2.ChartAreas[0].AxisX.Interval = 10;
            zdChart2.ChartAreas[0].AxisY.Interval = 1;


            zdChart2.ChartAreas[0].AxisY.Maximum = 200;
            zdChart2.ChartAreas[0].AxisY.Minimum = 0;
            zdChart2.ChartAreas[0].AxisY.Interval = 5;
            zdChart2.ChartAreas[0].AxisY.LabelStyle.Interval = 10;
            zdChart2.ChartAreas[0].AxisY.LabelStyle.IntervalType = DateTimeIntervalType.Number;
            zdChart2.ChartAreas[0].AxisY.Title = "т.с.";
            zdChart2.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated90;

            zdChart2.ChartAreas[0].AxisX.IsMarginVisible = false;
            zdChart2.ChartAreas[0].AxisY.IsMarginVisible = false;

            zdChart2.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            zdChart2.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;// LineDashStyle = ChartDashStyle.Dot;
            zdChart2.ChartAreas[0].AxisY2.IntervalAutoMode = IntervalAutoMode.VariableCount;
            zdChart2.ChartAreas[0].AxisY2.IntervalType = DateTimeIntervalType.Number;

            zdChart2.ChartAreas[0].AxisY2.IsMarginVisible = true;
            zdChart2.ChartAreas[0].AxisY2.Maximum = 64;
            zdChart2.ChartAreas[0].AxisY2.Interval = 4;
            zdChart2.ChartAreas[0].AxisY2.LabelStyle.Interval = 8;
            zdChart2.ChartAreas[0].AxisY2.Title = "кг/см2";
            zdChart2.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;

            zdChart2.Series[0].Color = Color.Black;

            zdChart2.MouseWheel +=zdChart2_MouseWheel;
            

            zdChart2.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            zdChart2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;


            
            zdChart2.Select();


            zdChart2.MouseMove += zdChart2_MouseMove;    

        }


        public void zdChart2_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    zdChart2.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    zdChart2.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }

                if (e.Delta > 0)
                {
                    double xMin = zdChart2.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    double xMax = zdChart2.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    double yMin = zdChart2.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    double yMax = zdChart2.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    double posXStart = zdChart2.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 1.5;
                    double posXFinish = zdChart2.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 1.5;
                    double posYStart = zdChart2.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 1.5;
                    double posYFinish = zdChart2.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 1.5;

                    zdChart2.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    zdChart2.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }          
        }


        private void OnBackClick()
        {
            if (!ViewModel.CanSaveOperation)
            {
                label13.TextChanged -= PrintCmd1;
            }
            Hide();
        }

        
        private void zdButton5_Click(object sender, EventArgs e)
        {
            OnBackClick();

            if (TimerForUpdate.Enabled) 
            {
                TimerForUpdate.Stop();
            }
        }


        public void PrintCmd1(object sender, EventArgs ev)
        {
            if (ViewModel.PressOperation.Id > 0)
            {
                ReportForm reportForm = new ReportForm();
                ReportDto reportDto = GetReportDto();
                reportForm.ReportDto = reportDto;
                reportForm.ShowDialog();
            }
           
        }


        public void button1_Click(object sender, EventArgs e)
        {
            ReportDto reportDto = GetReportDto();
    
            ReportForm reportForm = new ReportForm();

            reportForm.ReportDto = reportDto;
            
            reportForm.ShowDialog();
        }

        public ReportDto GetReportDto()
        {
            byte[] bytes = null;

            using (MemoryStream chartimage = new MemoryStream())
            {
                zdChart2.Invoke(new Action(() => 
                {
                    zdChart2.SaveImage(chartimage, ChartImageFormat.Jpeg);
                    bytes = chartimage.GetBuffer();
                }));
                
            }


            //TODO: сделать с биндингом по нормальносму
            ReportDto reportDto = new ReportDto();
            
                reportDto.ImageAsBase64 = Convert.ToBase64String(bytes);
                textBoxNomerDiagrammi.Invoke(new Action(() => 
                {
                    reportDto.NomerDiag = this.textBoxNomerDiagrammi.Text;//ViewModel.PressOperation.DiagramNumber,
                }));

            
               reportDto.DateTimeCreate = DateTime.Now.ToString(); 
               textBoxNomerOsi.Invoke(new Action(() => {reportDto.NomerOsi = textBoxNomerOsi.Text;}));//ViewModel.PressOperation.AxisNumber,})); 
               textBoxNomerZavoda.Invoke(new Action(() => {reportDto.NomerZavoda = textBoxNomerZavoda.Text;}));//ViewModel.PressOperation.FactoryNumber,})); 
               comboBoxTipColesPary.Invoke(new Action(() => { reportDto.TipKolesPar = comboBoxTipColesPary.Text; }));//ViewModel.PressOperation.WheelType,})); 
               comboBoxStorona.Invoke(new Action(() => {reportDto.Storona = comboBoxStorona.Text;}));//ViewModel.PressOperation.Side,})); 
               textBoxNomerKlesa.Invoke(new Action(() => {reportDto.NomerKolesa = textBoxNomerKlesa.Text;}));//ViewModel.PressOperation.WheelNumber,)); 
               textBoxPods.Invoke(new Action(() => { reportDto.DiametrPodsChasti = textBoxPods.Text;}));//ViewModel.PressOperation.DWheel.ToString(CultureInfo.InvariantCulture),}));
               textBoxOtv.Invoke(new Action(() => {reportDto.DiametrOtvStupici = textBoxOtv.Text;}));//ViewModel.PressOperation.DAxis.ToString(CultureInfo.InvariantCulture),}));
               textBoxDlin.Invoke(new Action(() => {reportDto.DlinaStupici = textBoxDlin.Text;}));//ViewModel.PressOperation.LengthStup.ToString(),)); 
               textBoxNatag.Invoke(new Action(() => {reportDto.Natag = textBoxNatag.Text;}));//ViewModel.PressOperation.Natiag.ToString(CultureInfo.InvariantCulture),})); 
               textBoxSopr.Invoke(new Action(() => { reportDto.DlinaSoprag = textBoxSopr.Text;}));//ViewModel.PressOperation.LengthSopriazh.ToString(),
               textBoxUsil100.Invoke(new Action(() => { reportDto.UsilZapres100 = textBoxUsil100.Text;}));//String.Format("{0:0.##}", ViewModel.PressOperation.Power100Mm),
                //UsilZapres100 = ViewModel.UsilZapres100.ToString(CultureInfo.InvariantCulture),
               textBoxMaxUsil.Invoke(new Action(() => { reportDto.MaxUsilZapres = textBoxMaxUsil.Text;}));// ViewModel.PressOperation.MaxPower.ToString(CultureInfo.InvariantCulture),
               textBoxDlinaPramUch.Invoke(new Action(() => { reportDto.DlinaPramUch = textBoxDlinaPramUch.Text; })); //ViewModel.PressOperation.LengthLines.ToString()
            
            return reportDto;
        }

        private void zdButton1_Click(object sender, EventArgs e)
        {
            OnWriteExcel();
        }


        private void OnWriteExcel()
        {
            PressOperation operation = ViewModel.PressOperation;

            if (operation != null)
            {
                if (new ExcelReport().CreateExcel(operation))
                {
                     MessageBox.Show(@"Ok");
                }                
            }
            else
            {
                MessageBox.Show(@"Пожалуйста, укажите операцию");
            }
        }


        private void zdButton4_Click(object sender, EventArgs e)
        {
            CreatePassport();
        }


        private void CreatePassport()
        {
            PressOperation operation1 = ViewModel.PressOperation;

            PressOperation operation2 = Dal.GetOperationForPassport(operation1.AxisNumber, operation1.Side);

            if (operation1.Side == @"левая")
            {
                if (new ExcelReport().CreatePassportExcel(operation1, operation2))
                    MessageBox.Show(@"Ok");
            }
            else
            {
                if (new ExcelReport().CreatePassportExcel(operation2, operation1))
                    MessageBox.Show(@"Ok");
            }
        }

       
        private void zdChart2_MouseMove(object sender, MouseEventArgs e)
        {
            bool chartIsAcccessable = zdChart2.IsAccessible;

            if (!chartIsAcccessable)
            {
                return;
            }

            Point mousePoint = new Point(e.X, e.Y);
            zdChart2.ChartAreas[0].CursorX.Interval = 0;
            zdChart2.ChartAreas[0].CursorY.Interval = 0;

            zdChart2.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, true);
            zdChart2.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, true);

            
            labelUsilie.Text = string.Format(@"Длина:{0}", zdChart2.ChartAreas[0].AxisX.PixelPositionToValue(e.X));
            labelUsilie.Text = string.Format(@"Усилие запресовки:{0}", zdChart2.ChartAreas[0].AxisY.PixelPositionToValue(e.Y));
        }


        private void zdGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void buttonSaveOperation_Click(object sender, EventArgs e)
        {
            OnSaveOperation();
        }


        private void OnSaveOperation()
        {
            PressOperation operation = ViewModel.PressOperation;

            if (operation.IsNew)
            {
                MessageBox.Show(@"Обновление данной операции невозможно");
                return;
            }

            Dal.SaveOrUpdatePressOperation(operation);

            MessageBox.Show(@"Опенрация сохранена");
        }
    }


    public enum ChartFormShowMode
    {
        ShowSavedOperation,
        ShowCurrentOperation
    }
}
