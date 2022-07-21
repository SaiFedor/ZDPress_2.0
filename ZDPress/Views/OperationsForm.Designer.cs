using ZDPress.UI.Controls;

namespace ZDPress.UI.Views
{
    partial class OperationListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.pagingUserControl1 = new ZDPress.UI.Controls.PagingUserControl();
            this.zdGrid1 = new ZDPress.UI.Controls.ZDGrid();
            this.factoryNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operationStartDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operationStopDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wheelNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diagramNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.axisNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wheelTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sideDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dWheelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dAxisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lengthStupDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.natiagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lengthSopriazhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxPowerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lengthLinesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pressOperationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.zdButton1 = new ZDPress.UI.Controls.ZDButton();
            this.zdButton2 = new ZDPress.UI.Controls.ZDButton();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zdGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pressOperationBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.pagingUserControl1, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.zdGrid1, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.zdButton1, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.zdButton2, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.03753F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.96247F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(967, 498);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // pagingUserControl1
            // 
            this.pagingUserControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanelMain.SetColumnSpan(this.pagingUserControl1, 2);
            this.pagingUserControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pagingUserControl1.Location = new System.Drawing.Point(170, 455);
            this.pagingUserControl1.Margin = new System.Windows.Forms.Padding(2);
            this.pagingUserControl1.Name = "pagingUserControl1";
            this.pagingUserControl1.PageIndexOrSizeChangedEventHandler = null;
            this.pagingUserControl1.PageSizes = null;
            this.pagingUserControl1.Size = new System.Drawing.Size(627, 31);
            this.pagingUserControl1.TabIndex = 0;
            // 
            // zdGrid1
            // 
            this.zdGrid1.AllowUserToAddRows = false;
            this.zdGrid1.AllowUserToDeleteRows = false;
            this.zdGrid1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.zdGrid1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.zdGrid1.AutoGenerateColumns = false;
            this.zdGrid1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.zdGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.zdGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.factoryNumberDataGridViewTextBoxColumn,
            this.operationStartDataGridViewTextBoxColumn,
            this.operationStopDataGridViewTextBoxColumn,
            this.wheelNumberDataGridViewTextBoxColumn,
            this.diagramNumberDataGridViewTextBoxColumn,
            this.axisNumberDataGridViewTextBoxColumn,
            this.wheelTypeDataGridViewTextBoxColumn,
            this.sideDataGridViewTextBoxColumn,
            this.dWheelDataGridViewTextBoxColumn,
            this.dAxisDataGridViewTextBoxColumn,
            this.lengthStupDataGridViewTextBoxColumn,
            this.natiagDataGridViewTextBoxColumn,
            this.lengthSopriazhDataGridViewTextBoxColumn,
            this.maxPowerDataGridViewTextBoxColumn,
            this.lengthLinesDataGridViewTextBoxColumn});
            this.tableLayoutPanelMain.SetColumnSpan(this.zdGrid1, 2);
            this.zdGrid1.DataSource = this.pressOperationBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.BlanchedAlmond;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.zdGrid1.DefaultCellStyle = dataGridViewCellStyle4;
            this.zdGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zdGrid1.Location = new System.Drawing.Point(3, 53);
            this.zdGrid1.MultiSelect = false;
            this.zdGrid1.Name = "zdGrid1";
            this.zdGrid1.ReadOnly = true;
            this.zdGrid1.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.BlanchedAlmond;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.zdGrid1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.zdGrid1.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.zdGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.zdGrid1.Size = new System.Drawing.Size(961, 397);
            this.zdGrid1.TabIndex = 1;
            // 
            // factoryNumberDataGridViewTextBoxColumn
            // 
            this.factoryNumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.factoryNumberDataGridViewTextBoxColumn.DataPropertyName = "FactoryNumber";
            this.factoryNumberDataGridViewTextBoxColumn.HeaderText = "Номер завода";
            this.factoryNumberDataGridViewTextBoxColumn.Name = "factoryNumberDataGridViewTextBoxColumn";
            this.factoryNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // operationStartDataGridViewTextBoxColumn
            // 
            this.operationStartDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.operationStartDataGridViewTextBoxColumn.DataPropertyName = "OperationStart";
            dataGridViewCellStyle2.Format = "dd.MM.yyyy HH.mm.ssss";
            this.operationStartDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.operationStartDataGridViewTextBoxColumn.HeaderText = "Время начала пресования";
            this.operationStartDataGridViewTextBoxColumn.Name = "operationStartDataGridViewTextBoxColumn";
            this.operationStartDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // operationStopDataGridViewTextBoxColumn
            // 
            this.operationStopDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.operationStopDataGridViewTextBoxColumn.DataPropertyName = "OperationStop";
            dataGridViewCellStyle3.Format = "dd.MM.yyyy HH.mm.ssss";
            this.operationStopDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.operationStopDataGridViewTextBoxColumn.HeaderText = "Время завершения пресования";
            this.operationStopDataGridViewTextBoxColumn.Name = "operationStopDataGridViewTextBoxColumn";
            this.operationStopDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // wheelNumberDataGridViewTextBoxColumn
            // 
            this.wheelNumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.wheelNumberDataGridViewTextBoxColumn.DataPropertyName = "WheelNumber";
            this.wheelNumberDataGridViewTextBoxColumn.HeaderText = "Номер колеса";
            this.wheelNumberDataGridViewTextBoxColumn.Name = "wheelNumberDataGridViewTextBoxColumn";
            this.wheelNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // diagramNumberDataGridViewTextBoxColumn
            // 
            this.diagramNumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.diagramNumberDataGridViewTextBoxColumn.DataPropertyName = "DiagramNumber";
            this.diagramNumberDataGridViewTextBoxColumn.HeaderText = "Номер диаграммы";
            this.diagramNumberDataGridViewTextBoxColumn.Name = "diagramNumberDataGridViewTextBoxColumn";
            this.diagramNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // axisNumberDataGridViewTextBoxColumn
            // 
            this.axisNumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.axisNumberDataGridViewTextBoxColumn.DataPropertyName = "AxisNumber";
            this.axisNumberDataGridViewTextBoxColumn.HeaderText = "Номер оси";
            this.axisNumberDataGridViewTextBoxColumn.Name = "axisNumberDataGridViewTextBoxColumn";
            this.axisNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // wheelTypeDataGridViewTextBoxColumn
            // 
            this.wheelTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.wheelTypeDataGridViewTextBoxColumn.DataPropertyName = "WheelType";
            this.wheelTypeDataGridViewTextBoxColumn.HeaderText = "Тип колесной пары";
            this.wheelTypeDataGridViewTextBoxColumn.Name = "wheelTypeDataGridViewTextBoxColumn";
            this.wheelTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sideDataGridViewTextBoxColumn
            // 
            this.sideDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sideDataGridViewTextBoxColumn.DataPropertyName = "Side";
            this.sideDataGridViewTextBoxColumn.HeaderText = "Сторона";
            this.sideDataGridViewTextBoxColumn.Name = "sideDataGridViewTextBoxColumn";
            this.sideDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dWheelDataGridViewTextBoxColumn
            // 
            this.dWheelDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dWheelDataGridViewTextBoxColumn.DataPropertyName = "DWheel";
            this.dWheelDataGridViewTextBoxColumn.HeaderText = "Диаметр подст. части";
            this.dWheelDataGridViewTextBoxColumn.Name = "dWheelDataGridViewTextBoxColumn";
            this.dWheelDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dAxisDataGridViewTextBoxColumn
            // 
            this.dAxisDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dAxisDataGridViewTextBoxColumn.DataPropertyName = "DAxis";
            this.dAxisDataGridViewTextBoxColumn.HeaderText = "Диаметр отв. cтупицы";
            this.dAxisDataGridViewTextBoxColumn.Name = "dAxisDataGridViewTextBoxColumn";
            this.dAxisDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lengthStupDataGridViewTextBoxColumn
            // 
            this.lengthStupDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.lengthStupDataGridViewTextBoxColumn.DataPropertyName = "LengthStup";
            this.lengthStupDataGridViewTextBoxColumn.HeaderText = "Длина ступицы";
            this.lengthStupDataGridViewTextBoxColumn.Name = "lengthStupDataGridViewTextBoxColumn";
            this.lengthStupDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // natiagDataGridViewTextBoxColumn
            // 
            this.natiagDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.natiagDataGridViewTextBoxColumn.DataPropertyName = "Natiag";
            this.natiagDataGridViewTextBoxColumn.HeaderText = "Натяг";
            this.natiagDataGridViewTextBoxColumn.Name = "natiagDataGridViewTextBoxColumn";
            this.natiagDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lengthSopriazhDataGridViewTextBoxColumn
            // 
            this.lengthSopriazhDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.lengthSopriazhDataGridViewTextBoxColumn.DataPropertyName = "LengthSopriazh";
            this.lengthSopriazhDataGridViewTextBoxColumn.HeaderText = "Длина сопряжения";
            this.lengthSopriazhDataGridViewTextBoxColumn.Name = "lengthSopriazhDataGridViewTextBoxColumn";
            this.lengthSopriazhDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // maxPowerDataGridViewTextBoxColumn
            // 
            this.maxPowerDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.maxPowerDataGridViewTextBoxColumn.DataPropertyName = "MaxPower";
            this.maxPowerDataGridViewTextBoxColumn.HeaderText = "Max усилие запрессовки";
            this.maxPowerDataGridViewTextBoxColumn.Name = "maxPowerDataGridViewTextBoxColumn";
            this.maxPowerDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lengthLinesDataGridViewTextBoxColumn
            // 
            this.lengthLinesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.lengthLinesDataGridViewTextBoxColumn.DataPropertyName = "LengthLines";
            this.lengthLinesDataGridViewTextBoxColumn.HeaderText = "Длина прямых участков";
            this.lengthLinesDataGridViewTextBoxColumn.Name = "lengthLinesDataGridViewTextBoxColumn";
            this.lengthLinesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pressOperationBindingSource
            // 
            this.pressOperationBindingSource.DataSource = typeof(ZDPress.Dal.Entities.PressOperation);
            // 
            // zdButton1
            // 
            this.zdButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton1.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton1.Location = new System.Drawing.Point(486, 3);
            this.zdButton1.MinimumSize = new System.Drawing.Size(0, 69);
            this.zdButton1.Name = "zdButton1";
            this.zdButton1.Size = new System.Drawing.Size(478, 69);
            this.zdButton1.TabIndex = 2;
            this.zdButton1.Text = "Назад";
            this.zdButton1.UseVisualStyleBackColor = false;
            this.zdButton1.Click += new System.EventHandler(this.zdButton1_Click);
            // 
            // zdButton2
            // 
            this.zdButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zdButton2.BackColor = System.Drawing.Color.AliceBlue;
            this.zdButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zdButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zdButton2.Location = new System.Drawing.Point(3, 3);
            this.zdButton2.MinimumSize = new System.Drawing.Size(0, 69);
            this.zdButton2.Name = "zdButton2";
            this.zdButton2.Size = new System.Drawing.Size(477, 69);
            this.zdButton2.TabIndex = 3;
            this.zdButton2.Text = "Открыть";
            this.zdButton2.UseVisualStyleBackColor = false;
            this.zdButton2.Click += new System.EventHandler(this.zdButton2_Click);
            // 
            // OperationListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 498);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "OperationListForm";
            this.Text = "OperationsForm";
            this.tableLayoutPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.zdGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pressOperationBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private  PagingUserControl pagingUserControl1;
        private Controls.ZDGrid zdGrid1;
        private System.Windows.Forms.BindingSource pressOperationBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn factoryNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operationStartDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operationStopDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wheelNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diagramNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn axisNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wheelTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sideDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dWheelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dAxisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lengthStupDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn natiagDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lengthSopriazhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn power100mmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxPowerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lengthLinesDataGridViewTextBoxColumn;
        private ZDButton zdButton1;
        private ZDButton zdButton2;
    }
}