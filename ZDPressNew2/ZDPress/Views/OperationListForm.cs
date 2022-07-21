using System;
using System.Windows.Forms;
using ZDPress.Dal;
using ZDPress.Dal.Entities;
using ZDPress.UI.Controls;
using ZDPress.UI.ViewModels;

namespace ZDPress.UI.Views
{
    public partial class OperationListForm : Form
    {

        private Timer _timer;

        public PressOperation SelectedEntity
        {
            get
            {
                return zdGrid1.SelectedRows.Count > 0 ? zdGrid1.SelectedRows[0].DataBoundItem as PressOperation : null;
            }
        }


        public ZdPressDal Dal { get; set; }


        public OperationListForm()
        {
            InitializeComponent();
            zdGrid1.RowTemplate.Height = 29;
            pagingUserControl1.PageIndexOrSizeChangedEventHandler += OnPageChahge;
            Dal = new ZdPressDal();
            zdGrid1.DoubleClick += zdGrid1_DoubleClick;

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += (d, k) =>
            {
                if (pagingUserControl1.CurrentPageIndex == 1)
                {
                    RefreshPage(1);
                }
            };

           // _timer.Start();
        }

        void zdGrid1_DoubleClick(object sender, EventArgs e)
        {
            OnOpenClick();
        }



        public delegate void LoadCompletedEventHandler();
        public event LoadCompletedEventHandler LoadCompleted;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Shown += DoAfterShow;
            LoadCompleted += OnLoadCompleted;
        }

        public void DoAfterShow(object sender, EventArgs e)
        {
            Application.DoEvents();

            if (LoadCompleted != null)
            {
                LoadCompleted();
            }
        }

        internal virtual void OnLoadCompleted()
        {
            RefreshPage(1);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (zdGrid1.RowCount > 0)
            {
                RefreshPage(1);
            }
        }

        private void UpdateGrid(int currentPage, int pageRows)
        {
            zdGrid1.DataSource = Dal.PageOperation(pageRows, currentPage);
            zdGrid1.Refresh();
        }


        public void OnPageChahge(object sender, PageChangedEventArgs args)
        {
            RefreshPage(args.CurrentPageIndex);
        }

        public void RefreshPage(int pageIndex)
        {
            Cursor.Current = Cursors.WaitCursor;

            int selectedRowIndex = zdGrid1.SelectedRows.Count > 0 ? zdGrid1.SelectedRows[0].Index : 0;// индекс выбранной в гриде строки

            int pageCount = SelectedEntity == null ? 
                1
                :
                SelectedEntity.TotalRows / pagingUserControl1.PageSize + (SelectedEntity.TotalRows % pagingUserControl1.PageSize > 0 ? 1 : 0);

            pagingUserControl1.SetPagesCount(pageCount);

            if (pageCount == 0)
            {
                ClearGrid();
            }
            else
            {
                if (pageCount < pageIndex)
                {
                    pageIndex = pageCount;
                }

                UpdateGrid(pageIndex, pagingUserControl1.PageSize);

                if (zdGrid1.Rows.Count <= selectedRowIndex)
                {
                    selectedRowIndex = zdGrid1.Rows.Count - 1;
                }

                if (zdGrid1.Rows.Count > 0)
                {
                    zdGrid1.Rows[selectedRowIndex].Selected = true;
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void ClearGrid()
        {
            zdGrid1.DataSource = null;
            zdGrid1.Refresh();
        }
      

        private void zdButton1_Click(object sender, EventArgs e)
        {
            OnBackClick();
        }

        private void OnBackClick()
        {
            Hide();
        }

        private void zdButton2_Click(object sender, EventArgs e)
        {
            OnOpenClick();
        }

        private void OnOpenClick()
        {
            PressOperation selectedEntity = SelectedEntity;

            if (selectedEntity == null)
            {
                MessageBox.Show(@"Пожалуйста, выберите операцию.");
                return;
            }

            if (selectedEntity.PressOperationData == null)
            {
                Dal.LoadPressOperationData(selectedEntity);//загрузим данные длял опреции
            }

            ChartForm form = (ChartForm)UiHelper.GetFormSingle(typeof(ChartForm));

            form.Mode = ChartFormShowMode.ShowSavedOperation;

            ChartFormViewModel viewModel = new ChartFormViewModel {PressOperation = selectedEntity};
            form.buttonSaveOperation.Visible = true;
            viewModel.CanSaveOperation = true;
            form.ViewModel = viewModel;

            UiHelper.ShowForm(form, UiHelper.GetMdiContainer(this));
        }
    }
}
