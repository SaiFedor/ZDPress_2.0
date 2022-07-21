using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace ZDPress.UI.Controls
{
    public partial class PagingUserControl : UserControl
    {
        public PagingUserControl()
        {
            InitializeComponent();
            InitializePagingUserControl();
        }


        public List<int> PageSizes { get; set; }


        public delegate void PageChangedDelegate(object sender, PageChangedEventArgs args);
        public PageChangedDelegate PageIndexOrSizeChangedEventHandler { get; set; }

        /// <summary>
        /// Индекс текущей страницы.
        /// </summary>
        public int CurrentPageIndex { get; private set; }

        /// <summary>
        /// Размер страницы.
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Количество страниц.
        /// </summary>
        public int PagesCount { get; private set; }



        public void SetPagesCount(int pageCount)
        {
            PagesCount = pageCount;
            RefreshPagingInfo();
        }

        private void RefreshPageInfoString()
        {
            labelPageInfo.Text = string.Format("Страница {0} из {1}", CurrentPageIndex, PagesCount);
        }

        private void RefreshPagingInfo()
        {
            RefreshPaginationButtons();
            RefreshPageInfoString();
        }

        private void pageSizeListComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }



        private void InitializePagingUserControl()
        {
            CurrentPageIndex = 1;

            btnFirst.Click += ToolStripButtonClick;
            btnBackward.Click += ToolStripButtonClick;
            toolStripButton1.Click += ToolStripButtonClick;
            toolStripButton2.Click += ToolStripButtonClick;
            toolStripButton3.Click += ToolStripButtonClick;
            toolStripButton4.Click += ToolStripButtonClick;
            toolStripButton5.Click += ToolStripButtonClick;
            toolStripButton6.Click += ToolStripButtonClick;
            toolStripButton7.Click += ToolStripButtonClick;
            toolStripButton8.Click += ToolStripButtonClick;
            toolStripButton9.Click += ToolStripButtonClick;
            toolStripButton10.Click += ToolStripButtonClick;
            btnForward.Click += ToolStripButtonClick;
            btnLast.Click += ToolStripButtonClick;

            pageSizeListComboBox.MouseWheel += pageSizeListComboBox_MouseWheel;

            PageSize = 20;
            pageSizeListComboBox.DataSource = PageSizes ?? new List<int>() { 20, 30, 50, 100 };
            pageSizeListComboBox.SelectedItem = 20;
        }


        private void ToolStripButtonClick(object sender, EventArgs e)
        {
            ToolStripButton toolStripButton = (ToolStripButton)sender;

            //Determining the current page
            if (toolStripButton == btnBackward)
                CurrentPageIndex--;
            else if (toolStripButton == btnForward)
                CurrentPageIndex++;
            else if (toolStripButton == btnLast)
                CurrentPageIndex = PagesCount;
            else if (toolStripButton == btnFirst)
                CurrentPageIndex = 1;
            else
                CurrentPageIndex = Convert.ToInt32(toolStripButton.Text, CultureInfo.InvariantCulture);

            if (CurrentPageIndex < 1)
                CurrentPageIndex = 1;
            else if (CurrentPageIndex > PagesCount)
                CurrentPageIndex = PagesCount;

            if (PageIndexOrSizeChangedEventHandler != null)
            {
                //Rebind the Datagridview with the data.
                PageIndexOrSizeChangedEventHandler(this, new PageChangedEventArgs
                {
                    CurrentPageIndex = CurrentPageIndex,
                    PageSize = PageSize,
                    PageSizeChanged = false
                });
            }

            //Change the pagiantions buttons according to page number
            RefreshPagingInfo();
        }




        private void RefreshPaginationButtons()
        {
            ToolStripButton[] items = { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5, toolStripButton6, toolStripButton7, toolStripButton8, toolStripButton9, toolStripButton10 };


            //pageStartIndex contains the first button number of pagination.
            int pageStartIndex = 1;

            if (PagesCount > 10 && CurrentPageIndex > 4)
                pageStartIndex = CurrentPageIndex - 4;

            if (PagesCount > 10 && CurrentPageIndex > PagesCount - 4)
                pageStartIndex = PagesCount - 9;

            for (int i = pageStartIndex; i < pageStartIndex + 10; i++)
            {
                if (i > PagesCount)
                {
                    items[i - pageStartIndex].Visible = false;
                }
                else
                {
                    items[i - pageStartIndex].Visible = true;
                    //Changing the page numbers
                    items[i - pageStartIndex].Text = i.ToString(CultureInfo.InvariantCulture);
                    items[i - pageStartIndex].ToolTipText = string.Format("Страница {0}", i.ToString(CultureInfo.InvariantCulture));

                    //Setting the Appearance of the page number buttons
                    if (i == CurrentPageIndex)
                    {
                        items[i - pageStartIndex].BackColor = Color.MidnightBlue;
                        items[i - pageStartIndex].ForeColor = Color.White;
                    }
                    else
                    {
                        items[i - pageStartIndex].BackColor = Color.White;
                        items[i - pageStartIndex].ForeColor = Color.MidnightBlue;
                    }
                }
            }

            //Enabling or Disalbing pagination first, last, previous , next buttons
            if (CurrentPageIndex == 1)
                btnBackward.Enabled = btnFirst.Enabled = false;
            else
                btnBackward.Enabled = btnFirst.Enabled = true;


            if (CurrentPageIndex == PagesCount)
                btnForward.Enabled = btnLast.Enabled = false;
            else
                btnForward.Enabled = btnLast.Enabled = true;
        }




        private void pageSizeListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PageIndexOrSizeChangedEventHandler != null)
            {
                int pageRows = int.Parse(pageSizeListComboBox.SelectedValue.ToString());
                CurrentPageIndex = 1;//после того как поменяли размер страницы будем показывать 1ую страницу.
                PageSize = pageRows;
                PageIndexOrSizeChangedEventHandler(this, new PageChangedEventArgs { CurrentPageIndex = 1, PageSize = pageRows, PageSizeChanged = true });
            }
        }
    }

    public class PageChangedEventArgs : EventArgs
    {
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public bool PageSizeChanged { get; set; }
    }
}
