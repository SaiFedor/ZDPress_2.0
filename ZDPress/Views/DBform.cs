using log4net;
using log4net.Appender;
using System;
using System.Configuration;
using System.Windows.Forms;
using ZDPress.Dal;
using ZDPress.Dal.Entities;
using ZDPress.UI.ViewModels;

namespace ZDPress.UI.Views
{
    public partial class DBform : Form
    {
        private object plc;

        public ZdPressDal Dal { get; set; }

        public DBform()
        {
            InitializeComponent();

            Dal = new ZdPressDal();
            ViewModel = new DBformViewModel();
        }

        public DBformViewModel ViewModel { get; set; }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            zdLabel27.DataBindings.Add(new Binding("BackColor", ViewModel, "PlcConnectState", true, DataSourceUpdateMode.OnPropertyChanged));
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            zdLabel17.Text = ConfigurationManager.AppSettings["PrinterName"];
            zdLabel22.Text = ConfigurationManager.AppSettings["RegisterArhivePath"]; 
            
            zdLabel26.Text = ConfigurationManager.AppSettings["PassportsArhivePath"];
            ViewModel.StartArchiveDate = this.dateTimePicker1.Value.Date;
            ViewModel.EndArchiveDate = this.dateTimePicker2.Value.Date;
            ViewModel.StartCleareDate = this.dateTimePicker3.Value.Date;
            ViewModel.EndClearDate = this.dateTimePicker4.Value.Date;
            zdLabel10.Text = ConfigurationManager.AppSettings["AutoBackupPath"];
            comboBox1.SelectedItem = ConfigurationManager.AppSettings["BackupDate"];

            log4net.Repository.Hierarchy.Hierarchy logHierarchy =
(log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();

            IAppender appender = logHierarchy.Root.Appenders[0];

            FileAppender fa = (FileAppender)appender;

            zdLabel21.Text = fa.File;

            ViewModel.RunAutoUpdateParameters(2000);
        }

        private void SelectLogFilePath(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.json)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    zdLabel21.Text = openFileDialog.FileName;
                    log4net.Repository.Hierarchy.Hierarchy logHierarchy =
(log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();
                    IAppender appender = logHierarchy.Root.Appenders[0];

                    FileAppender fa = (FileAppender)appender;

                    fa.File = zdLabel21.Text;
                    fa.ActivateOptions();
                }
            }
        }

        private void backupDateChanged(object sender, EventArgs e)
        {
            int day = 0;
            int.TryParse(comboBox1.SelectedItem.ToString(), out day);
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["BackupDate"].Value = day.ToString(); 
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

        private void SelectAutoBackupPath(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {               
                zdLabel10.Text = fbd.SelectedPath;
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["AutoBackupPath"].Value = fbd.SelectedPath;
                configuration.Save();

                ConfigurationManager.RefreshSection("appSettings");
            }

        }

        private void OnBackClick()
        {
            Form form = UiHelper.GetFormSingle(typeof(DBform));
            form.Hide();
        }

        private void zdButton4_Click(object sender, EventArgs e)
        {
            OnBackClick();
        }
        // Выбор начальной даты архивирования
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ViewModel.StartArchiveDate = this.dateTimePicker1.Value.Date;
        }
        // Выбор конечной даты архивирования
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            ViewModel.EndArchiveDate = this.dateTimePicker2.Value.Date;
            this.dateTimePicker1.MaxDate = this.dateTimePicker2.Value.Date;
        }
        // Выбор начальной даты очистки
        private void dateTimePicker3_ValueChanged_1(object sender, EventArgs e)
        {
            ViewModel.StartCleareDate = this.dateTimePicker3.Value.Date;
        }
        // Выбор конечной даты очистки
        private void dateTimePicker4_ValueChanged_1(object sender, EventArgs e)
        {
            ViewModel.EndClearDate = this.dateTimePicker4.Value.Date;
            this.dateTimePicker3.MaxDate = this.dateTimePicker4.Value.Date;
        }

        // Сделать бэкап
        private void zdButton5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(zdLabel12.Text))
            {
                //MessageBox.Show("Подождите. Идёт создание архива базы данных! Это может занять несколько минут.");

                bool result = Dal.BackupJsonOperationAsync(ViewModel.StartArchiveDate, ViewModel.EndArchiveDate, zdLabel12.Text);

                if (result)
                {
                    MessageBox.Show(@"Создание архива выполнено успешно!");
                }
                else
                {
                    MessageBox.Show(@"Ошибка создания архива данных! Проверьте правильность пути сохранения и соединения с БД!");
                }
            }
            else
            {
                MessageBox.Show(@"Ошибка! Выберите папку для сохранения файла!");
            }
        }
        //Воостановить БД из файла
        private void zdButton8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(zdLabel13.Text))
            {
                //MessageBox.Show("Подождите. Идёт восстановление базы данных из архива! Это может занять несколько минут.");
                RestoreResult restoreResult = Dal.RestoreBackupJsonOperation(zdLabel13.Text);
                if (restoreResult.result)
                {
                    MessageBox.Show(@"Восстановление данных выполнено успешно! Восстановленно  " + restoreResult.restoreCount.ToString() + "  операций.");
                }
                else
                {
                    MessageBox.Show(@"Ошибка восстановления данных! Проверьте правильность пути файла и расширения файла!");
                }
            }
            else
            {
                MessageBox.Show(@"Ошибка! Выберите файл для восстановления!");
            }

        }

        // Очистака таблиц за выбранный период
        private void zdButton3_Click(object sender, EventArgs e)
        {
            Dal.ClearOperationsTables(ViewModel.StartCleareDate, ViewModel.EndClearDate);
            if (Dal.ClearOperationsTables(ViewModel.StartCleareDate, ViewModel.EndClearDate))
            {
                MessageBox.Show(@"Очистка базы данных выполнена упешно!");
            }
            else
            {
                MessageBox.Show(@"Ошибка выполнения очистки базы данных!");
            }
        }

        // выбора папки сохранения архива
        private void zdLabel12_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ViewModel.saveFilePath = fbd.SelectedPath;
                zdLabel12.Text = fbd.SelectedPath;
            }
        }

        // Выбор файла восстановления архива
        private void zdLabel13_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    ViewModel.RestoreFilePath = openFileDialog.FileName;
                    zdLabel13.Text = openFileDialog.FileName;
                }
            }
        }

        // Выбор принтера 
        private void zdLabel17_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            var ps = printDialog.PrinterSettings;
            printDialog.ShowDialog();
            zdLabel17.Text = ps.PrinterName;

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["PrinterName"].Value = ps.PrinterName; ;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

        // Выбор папки журнала
        private void zdLabel22_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    zdLabel22.Text = fbd.SelectedPath;
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    configuration.AppSettings.Settings["RegisterArhivePath"].Value = fbd.SelectedPath;
                    configuration.Save();

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        // Выбор папки сохранения паспорта
        private void zdLabel26_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    ViewModel.saveFilePath = fbd.SelectedPath;
                    zdLabel26.Text = fbd.SelectedPath;
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    configuration.AppSettings.Settings["PassportsArhivePath"].Value = fbd.SelectedPath;
                    configuration.Save();

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }
    }
}
