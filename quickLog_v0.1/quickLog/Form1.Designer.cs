namespace quickLog
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            textBox_logConsole = new TextBox();
            groupBox_Main = new GroupBox();
            menuStrip1 = new MenuStrip();
            fIleToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            label_status = new Label();
            sfComboBox_UserID = new Syncfusion.WinForms.ListView.SfComboBox();
            label_sfCombobox_UserID = new Label();
            sfComboBox_EventID = new Syncfusion.WinForms.ListView.SfComboBox();
            label_sfComboBox_EventID = new Label();
            sfComboBox_MachineName = new Syncfusion.WinForms.ListView.SfComboBox();
            label_sfComboBox_MachineName = new Label();
            sfComboBox_Level = new Syncfusion.WinForms.ListView.SfComboBox();
            label_sfComboBox_Level = new Label();
            sfComboBox_LogName = new Syncfusion.WinForms.ListView.SfComboBox();
            label_sfComboBox_LogName = new Label();
            sfComboBox_Label = new Syncfusion.WinForms.ListView.SfComboBox();
            label_sfComboBox_Label = new Label();
            groupBox_CustomSearch = new GroupBox();
            label_btn_ShowCellToolTips = new Label();
            btn_ShowCellToolTips = new Button();
            label_Btn_GridFill = new Label();
            Btn_GridFill = new Button();
            Btn_ClearSearch = new Button();
            label_dtpEnd = new Label();
            label_dtpStart = new Label();
            dtpEnd = new DateTimePicker();
            dtpStart = new DateTimePicker();
            Btn_Seacrh = new Button();
            Label_textBox_SearchTerm = new Label();
            textBox_SearchTerm = new TextBox();
            richTextBox_Detail = new RichTextBox();
            dataGridView1 = new DataGridView();
            btn_AddComment = new Button();
            btn_ShowFormLabel = new Button();
            btn_AddLabel = new Button();
            btn_EraseLabel = new Button();
            btn_Make_Time_Range_Filter = new Button();
            btn_savetopdf = new Button();
            groupBox_LabelsAndComments = new GroupBox();
            btn_Time_filter_around = new Button();
            numericUpDown_TimeFilterAround = new NumericUpDown();
            groupBox_Time_Related_Filters = new GroupBox();
            btn_savetocsv = new Button();
            groupBox_Save_to = new GroupBox();
            btn_About = new Button();
            label_about = new Label();
            btn_ShowFontDialog = new Button();
            fontDialog1 = new FontDialog();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripProgressBar1 = new ToolStripProgressBar();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sfComboBox_UserID).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sfComboBox_EventID).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sfComboBox_MachineName).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sfComboBox_Level).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sfComboBox_LogName).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sfComboBox_Label).BeginInit();
            groupBox_CustomSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox_LabelsAndComments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_TimeFilterAround).BeginInit();
            groupBox_Time_Related_Filters.SuspendLayout();
            groupBox_Save_to.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox_logConsole
            // 
            textBox_logConsole.BackColor = SystemColors.Window;
            textBox_logConsole.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_logConsole.Location = new Point(360, 898);
            textBox_logConsole.Multiline = true;
            textBox_logConsole.Name = "textBox_logConsole";
            textBox_logConsole.ReadOnly = true;
            textBox_logConsole.ScrollBars = ScrollBars.Both;
            textBox_logConsole.Size = new Size(1036, 70);
            textBox_logConsole.TabIndex = 0;
            // 
            // groupBox_Main
            // 
            groupBox_Main.Enabled = false;
            groupBox_Main.Location = new Point(3, 130);
            groupBox_Main.Name = "groupBox_Main";
            groupBox_Main.Size = new Size(351, 839);
            groupBox_Main.TabIndex = 4;
            groupBox_Main.TabStop = false;
            groupBox_Main.Text = "Acquire And Basic Filters";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fIleToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1907, 24);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            fIleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, closeToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            fIleToolStripMenuItem.Size = new Size(37, 20);
            fIleToolStripMenuItem.Text = "FIle";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(164, 22);
            newToolStripMenuItem.Text = "New Workspace";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(164, 22);
            openToolStripMenuItem.Text = "Open Workspace";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(164, 22);
            closeToolStripMenuItem.Text = "Close Workspace";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(161, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(164, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // label_status
            // 
            label_status.AutoSize = true;
            label_status.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label_status.Location = new Point(360, 101);
            label_status.Name = "label_status";
            label_status.Size = new Size(440, 25);
            label_status.TabIndex = 7;
            label_status.Text = "Please create a workspace or open an existing one.";
            // 
            // sfComboBox_UserID
            // 
            sfComboBox_UserID.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.MultiSelection;
            sfComboBox_UserID.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            sfComboBox_UserID.Location = new Point(418, 33);
            sfComboBox_UserID.MaxDropDownItems = 10;
            sfComboBox_UserID.Name = "sfComboBox_UserID";
            sfComboBox_UserID.Size = new Size(152, 23);
            sfComboBox_UserID.Style.TokenStyle.CloseButtonBackColor = Color.FromArgb(255, 255, 255);
            sfComboBox_UserID.TabIndex = 8;
            sfComboBox_UserID.TabStop = false;
            // 
            // label_sfCombobox_UserID
            // 
            label_sfCombobox_UserID.AutoSize = true;
            label_sfCombobox_UserID.Location = new Point(468, 15);
            label_sfCombobox_UserID.Name = "label_sfCombobox_UserID";
            label_sfCombobox_UserID.Size = new Size(44, 15);
            label_sfCombobox_UserID.TabIndex = 9;
            label_sfCombobox_UserID.Text = "User ID";
            // 
            // sfComboBox_EventID
            // 
            sfComboBox_EventID.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.MultiSelection;
            sfComboBox_EventID.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            sfComboBox_EventID.Location = new Point(576, 33);
            sfComboBox_EventID.MaxDropDownItems = 10;
            sfComboBox_EventID.Name = "sfComboBox_EventID";
            sfComboBox_EventID.Size = new Size(121, 23);
            sfComboBox_EventID.Style.TokenStyle.CloseButtonBackColor = Color.FromArgb(255, 255, 255);
            sfComboBox_EventID.TabIndex = 8;
            sfComboBox_EventID.TabStop = false;
            // 
            // label_sfComboBox_EventID
            // 
            label_sfComboBox_EventID.AutoSize = true;
            label_sfComboBox_EventID.Location = new Point(612, 15);
            label_sfComboBox_EventID.Name = "label_sfComboBox_EventID";
            label_sfComboBox_EventID.Size = new Size(47, 15);
            label_sfComboBox_EventID.TabIndex = 11;
            label_sfComboBox_EventID.Text = "EventID";
            // 
            // sfComboBox_MachineName
            // 
            sfComboBox_MachineName.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.MultiSelection;
            sfComboBox_MachineName.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            sfComboBox_MachineName.Location = new Point(703, 33);
            sfComboBox_MachineName.Name = "sfComboBox_MachineName";
            sfComboBox_MachineName.Size = new Size(121, 23);
            sfComboBox_MachineName.Style.TokenStyle.CloseButtonBackColor = Color.FromArgb(255, 255, 255);
            sfComboBox_MachineName.TabIndex = 12;
            sfComboBox_MachineName.TabStop = false;
            // 
            // label_sfComboBox_MachineName
            // 
            label_sfComboBox_MachineName.AutoSize = true;
            label_sfComboBox_MachineName.Location = new Point(716, 15);
            label_sfComboBox_MachineName.Name = "label_sfComboBox_MachineName";
            label_sfComboBox_MachineName.Size = new Size(88, 15);
            label_sfComboBox_MachineName.TabIndex = 13;
            label_sfComboBox_MachineName.Text = "Machine Name";
            // 
            // sfComboBox_Level
            // 
            sfComboBox_Level.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.MultiSelection;
            sfComboBox_Level.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            sfComboBox_Level.Location = new Point(831, 33);
            sfComboBox_Level.Name = "sfComboBox_Level";
            sfComboBox_Level.Size = new Size(121, 23);
            sfComboBox_Level.Style.TokenStyle.CloseButtonBackColor = Color.FromArgb(255, 255, 255);
            sfComboBox_Level.TabIndex = 14;
            sfComboBox_Level.TabStop = false;
            // 
            // label_sfComboBox_Level
            // 
            label_sfComboBox_Level.AutoSize = true;
            label_sfComboBox_Level.Location = new Point(874, 15);
            label_sfComboBox_Level.Name = "label_sfComboBox_Level";
            label_sfComboBox_Level.Size = new Size(34, 15);
            label_sfComboBox_Level.TabIndex = 15;
            label_sfComboBox_Level.Text = "Level";
            // 
            // sfComboBox_LogName
            // 
            sfComboBox_LogName.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.MultiSelection;
            sfComboBox_LogName.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            sfComboBox_LogName.Location = new Point(958, 33);
            sfComboBox_LogName.Name = "sfComboBox_LogName";
            sfComboBox_LogName.Size = new Size(121, 23);
            sfComboBox_LogName.Style.TokenStyle.CloseButtonBackColor = Color.FromArgb(255, 255, 255);
            sfComboBox_LogName.TabIndex = 16;
            sfComboBox_LogName.TabStop = false;
            sfComboBox_LogName.SelectedIndexChanged += sfComboBox_LogName_SelectedIndexChanged;
            // 
            // label_sfComboBox_LogName
            // 
            label_sfComboBox_LogName.AutoSize = true;
            label_sfComboBox_LogName.Location = new Point(985, 15);
            label_sfComboBox_LogName.Name = "label_sfComboBox_LogName";
            label_sfComboBox_LogName.Size = new Size(62, 15);
            label_sfComboBox_LogName.TabIndex = 17;
            label_sfComboBox_LogName.Text = "Log Name";
            // 
            // sfComboBox_Label
            // 
            sfComboBox_Label.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.MultiSelection;
            sfComboBox_Label.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            sfComboBox_Label.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            sfComboBox_Label.Location = new Point(1085, 33);
            sfComboBox_Label.Name = "sfComboBox_Label";
            sfComboBox_Label.Size = new Size(121, 23);
            sfComboBox_Label.Style.TokenStyle.CloseButtonBackColor = Color.FromArgb(255, 255, 255);
            sfComboBox_Label.TabIndex = 18;
            // 
            // label_sfComboBox_Label
            // 
            label_sfComboBox_Label.AutoSize = true;
            label_sfComboBox_Label.Location = new Point(1120, 15);
            label_sfComboBox_Label.Name = "label_sfComboBox_Label";
            label_sfComboBox_Label.Size = new Size(35, 15);
            label_sfComboBox_Label.TabIndex = 19;
            label_sfComboBox_Label.Text = "Label";
            // 
            // groupBox_CustomSearch
            // 
            groupBox_CustomSearch.Controls.Add(label_btn_ShowCellToolTips);
            groupBox_CustomSearch.Controls.Add(btn_ShowCellToolTips);
            groupBox_CustomSearch.Controls.Add(label_Btn_GridFill);
            groupBox_CustomSearch.Controls.Add(Btn_GridFill);
            groupBox_CustomSearch.Controls.Add(Btn_ClearSearch);
            groupBox_CustomSearch.Controls.Add(label_dtpEnd);
            groupBox_CustomSearch.Controls.Add(label_dtpStart);
            groupBox_CustomSearch.Controls.Add(dtpEnd);
            groupBox_CustomSearch.Controls.Add(dtpStart);
            groupBox_CustomSearch.Controls.Add(Btn_Seacrh);
            groupBox_CustomSearch.Controls.Add(Label_textBox_SearchTerm);
            groupBox_CustomSearch.Controls.Add(textBox_SearchTerm);
            groupBox_CustomSearch.Controls.Add(label_sfCombobox_UserID);
            groupBox_CustomSearch.Controls.Add(label_sfComboBox_Label);
            groupBox_CustomSearch.Controls.Add(sfComboBox_Level);
            groupBox_CustomSearch.Controls.Add(sfComboBox_UserID);
            groupBox_CustomSearch.Controls.Add(label_sfComboBox_MachineName);
            groupBox_CustomSearch.Controls.Add(sfComboBox_Label);
            groupBox_CustomSearch.Controls.Add(label_sfComboBox_Level);
            groupBox_CustomSearch.Controls.Add(sfComboBox_EventID);
            groupBox_CustomSearch.Controls.Add(sfComboBox_MachineName);
            groupBox_CustomSearch.Controls.Add(label_sfComboBox_LogName);
            groupBox_CustomSearch.Controls.Add(sfComboBox_LogName);
            groupBox_CustomSearch.Controls.Add(label_sfComboBox_EventID);
            groupBox_CustomSearch.Enabled = false;
            groupBox_CustomSearch.Location = new Point(23, 27);
            groupBox_CustomSearch.Name = "groupBox_CustomSearch";
            groupBox_CustomSearch.Size = new Size(1848, 71);
            groupBox_CustomSearch.TabIndex = 20;
            groupBox_CustomSearch.TabStop = false;
            groupBox_CustomSearch.Text = "Custom Filter";
            // 
            // label_btn_ShowCellToolTips
            // 
            label_btn_ShowCellToolTips.AutoSize = true;
            label_btn_ShowCellToolTips.Location = new Point(1758, 16);
            label_btn_ShowCellToolTips.Name = "label_btn_ShowCellToolTips";
            label_btn_ShowCellToolTips.Size = new Size(46, 15);
            label_btn_ShowCellToolTips.TabIndex = 31;
            label_btn_ShowCellToolTips.Text = "Cell Tip";
            // 
            // btn_ShowCellToolTips
            // 
            btn_ShowCellToolTips.Location = new Point(1736, 33);
            btn_ShowCellToolTips.Name = "btn_ShowCellToolTips";
            btn_ShowCellToolTips.Size = new Size(92, 23);
            btn_ShowCellToolTips.TabIndex = 30;
            btn_ShowCellToolTips.Text = "O&ff";
            btn_ShowCellToolTips.UseVisualStyleBackColor = true;
            btn_ShowCellToolTips.Click += btn_ShowCellToolTips_Click;
            // 
            // label_Btn_GridFill
            // 
            label_Btn_GridFill.AutoSize = true;
            label_Btn_GridFill.Location = new Point(1657, 15);
            label_Btn_GridFill.Name = "label_Btn_GridFill";
            label_Btn_GridFill.Size = new Size(47, 15);
            label_Btn_GridFill.TabIndex = 29;
            label_Btn_GridFill.Text = "Grid Fill";
            // 
            // Btn_GridFill
            // 
            Btn_GridFill.Location = new Point(1638, 33);
            Btn_GridFill.Name = "Btn_GridFill";
            Btn_GridFill.Size = new Size(92, 23);
            Btn_GridFill.TabIndex = 28;
            Btn_GridFill.Text = "O&n";
            Btn_GridFill.UseVisualStyleBackColor = true;
            Btn_GridFill.Click += Btn_GridFill_Click;
            // 
            // Btn_ClearSearch
            // 
            Btn_ClearSearch.Location = new Point(1540, 33);
            Btn_ClearSearch.Name = "Btn_ClearSearch";
            Btn_ClearSearch.Size = new Size(92, 23);
            Btn_ClearSearch.TabIndex = 27;
            Btn_ClearSearch.Text = "&Clear Search";
            Btn_ClearSearch.UseVisualStyleBackColor = true;
            Btn_ClearSearch.Click += Btn_ClearSearch_Click;
            // 
            // label_dtpEnd
            // 
            label_dtpEnd.AutoSize = true;
            label_dtpEnd.Location = new Point(279, 15);
            label_dtpEnd.Name = "label_dtpEnd";
            label_dtpEnd.Size = new Size(54, 15);
            label_dtpEnd.TabIndex = 26;
            label_dtpEnd.Text = "End Date";
            // 
            // label_dtpStart
            // 
            label_dtpStart.AutoSize = true;
            label_dtpStart.Location = new Point(87, 15);
            label_dtpStart.Name = "label_dtpStart";
            label_dtpStart.Size = new Size(58, 15);
            label_dtpStart.TabIndex = 25;
            label_dtpStart.Text = "Start Date";
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(219, 33);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(192, 23);
            dtpEnd.TabIndex = 24;
            dtpEnd.ValueChanged += dtpEnd_ValueChanged;
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(21, 33);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(192, 23);
            dtpStart.TabIndex = 23;
            dtpStart.ValueChanged += dtpStart_ValueChanged;
            // 
            // Btn_Seacrh
            // 
            Btn_Seacrh.Location = new Point(1442, 33);
            Btn_Seacrh.Name = "Btn_Seacrh";
            Btn_Seacrh.Size = new Size(92, 23);
            Btn_Seacrh.TabIndex = 22;
            Btn_Seacrh.Text = "&Search";
            Btn_Seacrh.UseVisualStyleBackColor = true;
            Btn_Seacrh.Click += Btn_Seacrh_Click;
            // 
            // Label_textBox_SearchTerm
            // 
            Label_textBox_SearchTerm.AutoSize = true;
            Label_textBox_SearchTerm.Location = new Point(1287, 16);
            Label_textBox_SearchTerm.Name = "Label_textBox_SearchTerm";
            Label_textBox_SearchTerm.Size = new Size(73, 15);
            Label_textBox_SearchTerm.TabIndex = 21;
            Label_textBox_SearchTerm.Text = "Search term ";
            // 
            // textBox_SearchTerm
            // 
            textBox_SearchTerm.Location = new Point(1212, 34);
            textBox_SearchTerm.Name = "textBox_SearchTerm";
            textBox_SearchTerm.Size = new Size(225, 23);
            textBox_SearchTerm.TabIndex = 20;
            // 
            // richTextBox_Detail
            // 
            richTextBox_Detail.AutoWordSelection = true;
            richTextBox_Detail.BackColor = SystemColors.Window;
            richTextBox_Detail.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox_Detail.Location = new Point(1402, 138);
            richTextBox_Detail.Name = "richTextBox_Detail";
            richTextBox_Detail.ReadOnly = true;
            richTextBox_Detail.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox_Detail.Size = new Size(493, 623);
            richTextBox_Detail.TabIndex = 23;
            richTextBox_Detail.Text = "";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowDrop = true;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.ButtonFace;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(360, 138);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(1036, 754);
            dataGridView1.TabIndex = 25;
            // 
            // btn_AddComment
            // 
            btn_AddComment.Location = new Point(137, 51);
            btn_AddComment.Name = "btn_AddComment";
            btn_AddComment.Size = new Size(125, 23);
            btn_AddComment.TabIndex = 26;
            btn_AddComment.Text = "Add Co&mment";
            btn_AddComment.UseVisualStyleBackColor = true;
            btn_AddComment.Click += btn_AddComment_Click;
            // 
            // btn_ShowFormLabel
            // 
            btn_ShowFormLabel.Location = new Point(6, 22);
            btn_ShowFormLabel.Name = "btn_ShowFormLabel";
            btn_ShowFormLabel.Size = new Size(125, 23);
            btn_ShowFormLabel.TabIndex = 27;
            btn_ShowFormLabel.Text = "&Label Manager";
            btn_ShowFormLabel.UseVisualStyleBackColor = true;
            btn_ShowFormLabel.Click += btn_ShowFormLabel_Click;
            // 
            // btn_AddLabel
            // 
            btn_AddLabel.Location = new Point(6, 51);
            btn_AddLabel.Name = "btn_AddLabel";
            btn_AddLabel.Size = new Size(125, 23);
            btn_AddLabel.TabIndex = 28;
            btn_AddLabel.Text = "Add &Label";
            btn_AddLabel.UseVisualStyleBackColor = true;
            btn_AddLabel.Click += btn_AddLabel_Click;
            // 
            // btn_EraseLabel
            // 
            btn_EraseLabel.Location = new Point(137, 22);
            btn_EraseLabel.Name = "btn_EraseLabel";
            btn_EraseLabel.Size = new Size(125, 23);
            btn_EraseLabel.TabIndex = 29;
            btn_EraseLabel.Text = "&Erase Label";
            btn_EraseLabel.UseVisualStyleBackColor = true;
            btn_EraseLabel.Click += btn_EraseLabel_Click;
            // 
            // btn_Make_Time_Range_Filter
            // 
            btn_Make_Time_Range_Filter.Location = new Point(6, 22);
            btn_Make_Time_Range_Filter.Name = "btn_Make_Time_Range_Filter";
            btn_Make_Time_Range_Filter.Size = new Size(125, 23);
            btn_Make_Time_Range_Filter.TabIndex = 30;
            btn_Make_Time_Range_Filter.Text = "Time &Range";
            btn_Make_Time_Range_Filter.UseVisualStyleBackColor = true;
            btn_Make_Time_Range_Filter.Click += btn_Make_Time_Range_Filter_Click;
            // 
            // btn_savetopdf
            // 
            btn_savetopdf.Location = new Point(6, 22);
            btn_savetopdf.Name = "btn_savetopdf";
            btn_savetopdf.Size = new Size(125, 23);
            btn_savetopdf.TabIndex = 31;
            btn_savetopdf.Text = "Save to &PDF";
            btn_savetopdf.UseVisualStyleBackColor = true;
            btn_savetopdf.Click += btn_savetopdf_Click;
            // 
            // groupBox_LabelsAndComments
            // 
            groupBox_LabelsAndComments.Controls.Add(btn_ShowFormLabel);
            groupBox_LabelsAndComments.Controls.Add(btn_AddComment);
            groupBox_LabelsAndComments.Controls.Add(btn_AddLabel);
            groupBox_LabelsAndComments.Controls.Add(btn_EraseLabel);
            groupBox_LabelsAndComments.Enabled = false;
            groupBox_LabelsAndComments.Location = new Point(1409, 796);
            groupBox_LabelsAndComments.Name = "groupBox_LabelsAndComments";
            groupBox_LabelsAndComments.Size = new Size(275, 83);
            groupBox_LabelsAndComments.TabIndex = 32;
            groupBox_LabelsAndComments.TabStop = false;
            groupBox_LabelsAndComments.Text = "Labels and Comments";
            // 
            // btn_Time_filter_around
            // 
            btn_Time_filter_around.Location = new Point(6, 54);
            btn_Time_filter_around.Name = "btn_Time_filter_around";
            btn_Time_filter_around.Size = new Size(125, 23);
            btn_Time_filter_around.TabIndex = 33;
            btn_Time_filter_around.Text = "Minutes &Around";
            btn_Time_filter_around.UseVisualStyleBackColor = true;
            btn_Time_filter_around.Click += btn_Time_filter_around_Click;
            // 
            // numericUpDown_TimeFilterAround
            // 
            numericUpDown_TimeFilterAround.Location = new Point(137, 54);
            numericUpDown_TimeFilterAround.Name = "numericUpDown_TimeFilterAround";
            numericUpDown_TimeFilterAround.Size = new Size(125, 23);
            numericUpDown_TimeFilterAround.TabIndex = 34;
            numericUpDown_TimeFilterAround.ValueChanged += numericUpDown_TimeFilterAround_ValueChanged;
            // 
            // groupBox_Time_Related_Filters
            // 
            groupBox_Time_Related_Filters.Controls.Add(btn_Make_Time_Range_Filter);
            groupBox_Time_Related_Filters.Controls.Add(btn_Time_filter_around);
            groupBox_Time_Related_Filters.Controls.Add(numericUpDown_TimeFilterAround);
            groupBox_Time_Related_Filters.Enabled = false;
            groupBox_Time_Related_Filters.Location = new Point(1409, 885);
            groupBox_Time_Related_Filters.Name = "groupBox_Time_Related_Filters";
            groupBox_Time_Related_Filters.Size = new Size(275, 83);
            groupBox_Time_Related_Filters.TabIndex = 36;
            groupBox_Time_Related_Filters.TabStop = false;
            groupBox_Time_Related_Filters.Text = "Time Related Filters";
            // 
            // btn_savetocsv
            // 
            btn_savetocsv.Location = new Point(6, 51);
            btn_savetocsv.Name = "btn_savetocsv";
            btn_savetocsv.Size = new Size(125, 23);
            btn_savetocsv.TabIndex = 37;
            btn_savetocsv.Text = "Save to cs&v";
            btn_savetocsv.UseVisualStyleBackColor = true;
            btn_savetocsv.Click += btn_savetocsv_Click;
            // 
            // groupBox_Save_to
            // 
            groupBox_Save_to.Controls.Add(btn_savetopdf);
            groupBox_Save_to.Controls.Add(btn_savetocsv);
            groupBox_Save_to.Enabled = false;
            groupBox_Save_to.Location = new Point(1690, 796);
            groupBox_Save_to.Name = "groupBox_Save_to";
            groupBox_Save_to.Size = new Size(152, 83);
            groupBox_Save_to.TabIndex = 38;
            groupBox_Save_to.TabStop = false;
            groupBox_Save_to.Text = "Save to";
            // 
            // btn_About
            // 
            btn_About.Enabled = false;
            btn_About.Location = new Point(1711, 913);
            btn_About.Name = "btn_About";
            btn_About.Size = new Size(125, 23);
            btn_About.TabIndex = 39;
            btn_About.Text = "About";
            btn_About.UseVisualStyleBackColor = true;
            btn_About.Click += btn_About_Click;
            // 
            // label_about
            // 
            label_about.AutoSize = true;
            label_about.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label_about.Location = new Point(1690, 941);
            label_about.Name = "label_about";
            label_about.Size = new Size(184, 17);
            label_about.TabIndex = 41;
            label_about.Text = "www.internet-solutions.com.co";
            label_about.Click += label_about_Click;
            // 
            // btn_ShowFontDialog
            // 
            btn_ShowFontDialog.Enabled = false;
            btn_ShowFontDialog.Location = new Point(1415, 767);
            btn_ShowFontDialog.Name = "btn_ShowFontDialog";
            btn_ShowFontDialog.Size = new Size(125, 23);
            btn_ShowFontDialog.TabIndex = 42;
            btn_ShowFontDialog.Text = "&Font";
            btn_ShowFontDialog.UseVisualStyleBackColor = true;
            btn_ShowFontDialog.Click += btn_ShowFontDialog_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripProgressBar1 });
            statusStrip1.Location = new Point(0, 978);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1907, 22);
            statusStrip1.TabIndex = 43;
            statusStrip1.Text = "statusStrip1";
            statusStrip1.ItemClicked += statusStrip1_ItemClicked;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(87, 17);
            toolStripStatusLabel1.Text = "Progress Status";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(100, 16);
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            ClientSize = new Size(1907, 1000);
            Controls.Add(statusStrip1);
            Controls.Add(btn_ShowFontDialog);
            Controls.Add(label_about);
            Controls.Add(btn_About);
            Controls.Add(groupBox_Save_to);
            Controls.Add(groupBox_Time_Related_Filters);
            Controls.Add(groupBox_LabelsAndComments);
            Controls.Add(dataGridView1);
            Controls.Add(richTextBox_Detail);
            Controls.Add(groupBox_CustomSearch);
            Controls.Add(label_status);
            Controls.Add(groupBox_Main);
            Controls.Add(textBox_logConsole);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "FormMain";
            Text = "Quick Log";
            Load += FormMain_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)sfComboBox_UserID).EndInit();
            ((System.ComponentModel.ISupportInitialize)sfComboBox_EventID).EndInit();
            ((System.ComponentModel.ISupportInitialize)sfComboBox_MachineName).EndInit();
            ((System.ComponentModel.ISupportInitialize)sfComboBox_Level).EndInit();
            ((System.ComponentModel.ISupportInitialize)sfComboBox_LogName).EndInit();
            ((System.ComponentModel.ISupportInitialize)sfComboBox_Label).EndInit();
            groupBox_CustomSearch.ResumeLayout(false);
            groupBox_CustomSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox_LabelsAndComments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown_TimeFilterAround).EndInit();
            groupBox_Time_Related_Filters.ResumeLayout(false);
            groupBox_Save_to.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox_logConsole;
        private GroupBox groupBox_Main;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fIleToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Label label_status;
        private Syncfusion.WinForms.ListView.SfComboBox sfComboBox_UserID;
        private Label label_sfCombobox_UserID;
        private Syncfusion.WinForms.ListView.SfComboBox sfComboBox_EventID;
        private Label label_sfComboBox_EventID;
        private Syncfusion.WinForms.ListView.SfComboBox sfComboBox_MachineName;
        private Label label_sfComboBox_MachineName;
        private Syncfusion.WinForms.ListView.SfComboBox sfComboBox_Level;
        private Label label_sfComboBox_Level;
        private Syncfusion.WinForms.ListView.SfComboBox sfComboBox_LogName;
        private Label label_sfComboBox_LogName;
        private Syncfusion.WinForms.ListView.SfComboBox sfComboBox_Label;
        private Label label_sfComboBox_Label;
        private GroupBox groupBox_CustomSearch;
        private Button Btn_Seacrh;
        private Label Label_textBox_SearchTerm;
        private TextBox textBox_SearchTerm;
        private DateTimePicker dtpEnd;
        private DateTimePicker dtpStart;
        private Label label_dtpStart;
        private Label label_dtpEnd;
        private Button Btn_ClearSearch;
        private RichTextBox richTextBox_Detail;
        private DataGridView dataGridView1;
        private Button btn_AddComment;
        private Button btn_ShowFormLabel;
        private Button btn_AddLabel;
        private Button btn_EraseLabel;
        private Button btn_Make_Time_Range_Filter;
        private Button btn_savetopdf;
        private GroupBox groupBox_LabelsAndComments;
        private Button btn_Time_filter_around;
        private NumericUpDown numericUpDown_TimeFilterAround;
        private GroupBox groupBox_Time_Related_Filters;
        private Button btn_savetocsv;
        private Button Btn_GridFill;
        private Label label_Btn_GridFill;
        private Button btn_ShowCellToolTips;
        private Label label_btn_ShowCellToolTips;
        private GroupBox groupBox_Save_to;
        private Button btn_About;
        private Label label_about;
        private Button btn_ShowFontDialog;
        private FontDialog fontDialog1;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripStatusLabel toolStripStatusLabel1;
    }
}