using quickLog.Resources;
using System.Diagnostics.Eventing.Reader;
using System.Data;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = System.Drawing.Font;
using Document = iTextSharp.text.Document;
using System.Text.RegularExpressions;


//La sgte linea se agrgo sola al tratar de poner en modo fill el datagrd programaticamente me genero errores en los lables de los menus en todos, ams de 100
//using System.Windows.Forms.VisualStyles;
// Ese error se arreglo usando la sgte linE, como no use ninguno pero los dejo comentados por si sucediera otra vez el error
//using ContentAlignment = System.Drawing.ContentAlignment;

namespace quickLog
{
    public partial class FormMain : Form
    {


        //Variables para el tiemo

        public DateTime StartDate = DateTime.Now.AddYears(-20);
        public DateTime EndDate = DateTime.Now;

        //Variable para la cadena de conexion a sqlite

        public string ConnectionString = "";
        public string dbname = "";

        // Entero contador de los resultados de busqueda

        public int HitNumber;

        //Variable para el cuadro de busqueda

        string searchTerm = "";

        //Variable para la busqueda si va con and o con or, segundo termino de busqueda

        string searchTerm2 = "";

        //Variable para saber si hay algo en el cuadro de busqueda

        bool searchTermExists, search2TermExists = false;

        //Variables para almacenar el Log_id, indice de fila (row) y fila seleccionada (row)

        public int Log_id;
        public int rowIndex;
        public DataGridViewRow? selectedRow;

        //Diccionario para cargar los resultados procesados

        Dictionary<string, int> resultsDictionary = new Dictionary<string, int>();

        //Variables para calcular totales de sub menus y menu


        // Enteros del primer menu: General Interest

        public int Viewed_Users, Log_on_Log_off, Failed_to_Log_on, Power_On_Off, Installed_Software, Installed_Service,
            Volume_Shadow_Copy, Create_Local_Users, Deleted_Users, Renamed_Users, General_Interest, All_Logs;

        

        // Enteros del segundo menu: User Account

        public int User_was_created, User_was_enabled, User_chg_pass, User_reset_pass, User_was_disabled, User_was_deleted, User_was_changed, UserAccount;

        // Enteros tercer menu: Sec Enbl Glob Grp

        public int Globgrp_Group_Was_Created, Globgrp_Group_Was_Deleted, Globgrp_Group_Was_Changed, Globgrp_AMember_Was_Added, Globgrp_AMember_Was_Removed,
            Sec_Enbl_Glob_Grp;

        // Enteros cuarto menu: Sec Enbl Local Grp

        public int LocalGrp_Group_Was_Created, LocalGrp_Group_Was_Deleted, LocalGrp_Group_Was_Changed, LocalGrp_AMember_Was_Added, LocalGrp_AMember_Was_Removed,
            LocalGrp_Group_Was_Enumerated, Sec_Enbl_Local_Grp;

        // Enteros quinto menu: Sec enbl Univ Grp

        public int UnivGrp_Group_Was_Created, UnivGrp_Group_Was_Deleted, UnivGrp_Group_Was_Changed, UnivGrp_AMember_Was_Added, UnivGrp_AMember_Was_Removed, Sec_Enbl_Univ_Grp;

        //Enteros sexto Menu : A computer account

        public int Computer_Account_Was_Created, Computer_Account_Was_Deleted, Computer_Account_Was_Changed, Computer_Account;

        // Enteros sptimo menu: Network Share

        public int Network_Share_Accessed, Network_Share_Added, Network_Share_Modified, Network_Share_Deleted, Network_Share_Checked, Network_Share;

        // Enteros octavo menu: Scheduled Task Activity

        public int Scheduled_Task_Created, Scheduled_Task_Updated, Scheduled_Task_Deleted, Scheduled_Task_Executed, Scheduled_Task_Completed,
            Scheduled_Task_Activity;

        // Enteros noveno menu: Obj Acc Aud Sched Task 

        public int Obj_Scheduled_Task_Created, Obj_Scheduled_Task_Deleted, Obj_Scheduled_Task_Enabled, Obj_Scheduled_Task_Disabled, Obj_Scheduled_Task_Updated,
            Object_Access_Auditing;

        // Enteros decimo menu: Object Handle Event

        public int Handle_toAn_Object_Requested, Registry_Value_Modified, Handle_toAn_Object_Closed, Object_Was_Deleted, Attempt_Made_to_Acc_Object, Object_Handle_Event;

        // Enteros del menu 11: Auditing Windows Services


        public int Event_Log_Service_Started, Event_Log_Service_Stoped, Service_Terminated_Unexpectedly, Service_Start_Stop, Start_Type_For_Service_Changed,
            Service_Installed_bySystem, Auditing_Windows_Services;


        // Enteros menu doce: Wi-Fi Connection

        public int Wlan_Successfully_Connected, Wlan_Failed_To_Connect, Wlan_Connection;

        // Enteros menu trece: Windows Filtering Platform

        public int Block_App_Accept_Incom_Conn, Bloked_a_Packet, Permited_Incommig_Conn, Allowed_Connection, Blocked_Connection, Permited_Bind_a_Local_Port,
            Block_Bind_a_Local_Port, Windows_Filtering_Platform;


        // Enteros menu catorce: Windows Defender

        public int Found_Malware, Action_Protect_System, Failed_Action_Protect_System, Deleted_History_Malware, Detected_Suspicious_Behavior, Detected_Malware,
            Critical_Error_In_Action_On_Malware, Real_Time_Protection_Disabled, Real_Time_Protection_Cfg_Changed, Platform_Cfg_Changed, Scanning_Malware_Disabled,
            Scanning_Virus_Disabled, Windows_Defender;


        // Enteros menu quince: Sysmon

        public int Process_Creation, Proc_Chng_File_Creation_Time, Network_Connection, Sysmon_Service_State_Changed, Process_Terminated, Driver_Loaded, Image_Loaded,
            Create_Remote_Thread, Raw_Access_Read, Process_Access, File_Create, Reg_Key_Value_Created_Deleted, Registry_Value_Modification, Registry_Key_Value_Renamed,
            File_Create_Stream_Hash, Sysmon_Configuration_Change, Named_Pipe_Created, Named_Pipe_Connected, WMIEventFilter_Activity_Detected, WMIEventConsumer_Activity_Detected,
            WMIEventConsumerToFilter_Activity_Detected, DNS_Query_Event, Sysmon_Error, Sysmon;


        //Enteros menu 16 Terminal Server

        public int Terminal_Server_Log_On, Terminal_Server_Shell_Start, Terminal_Server_Log_Off, Terminal_Server_Session_Disconnected,
         Terminal_Server_Session_Reconnection, Terminal_Server_Session_Disconnected_Code, Terminal_Server_Session_Disconnected_by_Session, Terminal_Server;


        public FormMain()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmpCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXtfdHZUQ2FfVUFxXEU=");

            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;

            //Inicializacion de los data picker

            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpStart.Value = StartDate;


            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpEnd.Value = EndDate;

            dtpStart.Value = StartDate; dtpEnd.Value = EndDate;
            
            //Menu de la izquierda
            AcordeonMenu();

            //Inicializacion del datagrid

            dataGridView1.ShowCellToolTips = false;

            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, 10);

            dataGridView1.CellFormatting += DataGridView_CellFormatting;

            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            //Falta implementacion de parsers para los XML que muestren la informacion relevante traduciendo los codigos a el mensaje o razon
            //La funcion que se inicializa abajo lo hara

            //richTextBox_Detail.TextChanged += richtextBox_Detail_TextChanged;

            dataGridView1.Sorted += DataGridView1_Sorted;

            //Boton para Fill On / Off Grid
            // Se desconfigura despues que query en sql carga la datagridview1
            Btn_GridFill.Text = "On";

            // Inicializacion del numericUpDown

            numericUpDown_TimeFilterAround.Minimum = 1;

            numericUpDown_TimeFilterAround.Maximum = 1440;

            numericUpDown_TimeFilterAround.Value = 1;

            numericUpDown_TimeFilterAround.Increment = 1;

            //Inicializando sfconbobox

            sfComboBox_UserID.AllowSelectAll = true;
            sfComboBox_EventID.AllowSelectAll = true;
            sfComboBox_MachineName.AllowSelectAll = true;
            sfComboBox_Level.AllowSelectAll = true;
            sfComboBox_LogName.AllowSelectAll = true;
            sfComboBox_Label.AllowSelectAll = true;

            // Activacion tooltip
            Tool_Tip();

            //Drag and drop de logs o folder con logs
            dataGridView1.DragEnter += new DragEventHandler(DataGridView1_DragEnter);
            dataGridView1.DragDrop += new DragEventHandler(DataGridView1_DragDrop);
        }



        public FormMain(string filePath) : this()
        {

            if (!string.IsNullOrEmpty(filePath))
            {
                // Guardar la ruta de la base de datos
                DateTime start_process = DateTime.Now;
                dbname = filePath;
                this.ConnectionString = $"Data Source={dbname};Version=3;";
                this.Text = "Quick Log v 0.1 is working on:   " + dbname;
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Maximum = 16;
                Load_SfCombobox();
                LoadValues();
                //await ProcessAll();
                enable_buttons();
                DateTime end_process = DateTime.Now;
                TimeSpan Total_time = end_process - start_process;
                LogToConsole($"Total {Total_time.Days} days {Total_time.Hours} hours {Total_time.Minutes} minutes and {Total_time.Seconds} seconds");
                label_status.Text = "Viewed Users";
                //LogToConsole(Viewed_Users.ToString());
            }
        }

        private void DataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private async void DataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            
            if (ConnectionString != "")
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {

                    string insertQuery = @"INSERT INTO LogData (TimeCreated, UserID, EventID, MachineName, Level, LogName, EventMessage, EventMessageXml, ActivityID) 
                                                VALUES (@TimeCreated, @UserID, @EventID, @MachineName, @Level, @LogName, @EventMessage, @EventMessageXml, @ActivityID)";

                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    var selectedFiles = new List<string>(files);

                    foreach (string file in selectedFiles)
                    {
                        if (File.Exists(file))
                        {

                            DateTime start_process = DateTime.Now;
                            disable_buttons();
                            int filesnumber = selectedFiles.Count;
                            toolStripProgressBar1.Value = 0;
                            toolStripProgressBar1.Maximum = filesnumber;
                            label_status.Text = $"Working on {filesnumber} logs please wait...";
                            Application.DoEvents();
                            LogToConsole("Working please wait...");
                            await Task.Run(() => ProcessLogs(selectedFiles, insertQuery));
                            toolStripProgressBar1.Value = 0;
                            toolStripProgressBar1.Maximum = 16;
                            label_status.Text = "Processing logs, please wait.";
                            Load_SfCombobox();
                            await ProcessAll();
                            enable_buttons();
                            DateTime end_process = DateTime.Now;
                            TimeSpan Total_time = end_process - start_process;
                            LogToConsole($"Total {Total_time.Days} days {Total_time.Hours} hours {Total_time.Minutes} minutes and {Total_time.Seconds} seconds");
                            //Solo necesitamos procesar kos archivos una vez.
                            break;
                        }
                        else if (Directory.Exists(file))
                        {
                            //Procesar un directorio

                            DateTime start_process = DateTime.Now;
                            List<string> selecFiles = GetAllEvtxFiles(file);
                            disable_buttons();
                            int filesnumber = selecFiles.Count;
                            toolStripProgressBar1.Value = 0;
                            toolStripProgressBar1.Maximum = filesnumber;
                            label_status.Text = $"Working on {filesnumber} logs please wait...";
                            Application.DoEvents();
                            LogToConsole("Working please wait...");
                            await Task.Run(() => ProcessLogs(selecFiles, insertQuery));
                            toolStripProgressBar1.Value = 0;
                            toolStripProgressBar1.Maximum = 16;
                            label_status.Text = "Processing logs, please wait.";
                            Load_SfCombobox();
                            await ProcessAll();
                            enable_buttons();
                            DateTime end_process = DateTime.Now;
                            TimeSpan Total_time = end_process - start_process;
                            LogToConsole($"Total {Total_time.Days} days {Total_time.Hours} hours {Total_time.Minutes} minutes and {Total_time.Seconds} seconds");                            
                        }
                    }
                }
            }
        }

        // Tool Tip


        private void Tool_Tip()
        {
            // sfComboBox_UserID sfComboBox_EventID sfComboBox_MachineName sfComboBox_Level
            // sfComboBox_LogName

            // Crear el ToolTip
            ToolTip toolTip_sfComboBox_UserID = new ToolTip();

            // Configura el tooltip sfComboBox_UserID
            toolTip_sfComboBox_UserID.AutoPopDelay = 5000;
            toolTip_sfComboBox_UserID.InitialDelay = 1000;
            toolTip_sfComboBox_UserID.ReshowDelay = 500;
            toolTip_sfComboBox_UserID.ShowAlways = true;

            // Establece el texto del tooltip para el botón
            toolTip_sfComboBox_UserID.SetToolTip(this.sfComboBox_UserID, "All users in the logs");

            // Crear el ToolTip
            ToolTip toolTip_sfComboBox_EventID = new ToolTip();

            // Configura el tooltip sfComboBox_EventID
            toolTip_sfComboBox_EventID.AutoPopDelay = 5000;
            toolTip_sfComboBox_EventID.InitialDelay = 1000;
            toolTip_sfComboBox_EventID.ReshowDelay = 500;
            toolTip_sfComboBox_EventID.ShowAlways = true;

            // Establece el texto del tooltip para el botón
            toolTip_sfComboBox_EventID.SetToolTip(this.sfComboBox_EventID, "All existing events in the logs");



            // Crear el ToolTip
            ToolTip toolTip_sfComboBox_MachineName = new ToolTip();

            // Configura el tooltip sfComboBox_UserID
            toolTip_sfComboBox_MachineName.AutoPopDelay = 5000;
            toolTip_sfComboBox_MachineName.InitialDelay = 1000;
            toolTip_sfComboBox_MachineName.ReshowDelay = 500;
            toolTip_sfComboBox_MachineName.ShowAlways = true;

            // Establece el texto del tooltip para el botón
            toolTip_sfComboBox_MachineName.SetToolTip(this.sfComboBox_MachineName, "All existing machine names in the logs");



            // Crear el ToolTip
            ToolTip toolTip_sfComboBox_Level = new ToolTip();

            // Configura el tooltip sfComboBox_UserID
            toolTip_sfComboBox_Level.AutoPopDelay = 5000;
            toolTip_sfComboBox_Level.InitialDelay = 1000;
            toolTip_sfComboBox_Level.ReshowDelay = 500;
            toolTip_sfComboBox_Level.ShowAlways = true;

            // Establece el texto del tooltip para el botón
            toolTip_sfComboBox_Level.SetToolTip(this.sfComboBox_Level, "Event level represents the severity of the recorded event log");




            // Crear el ToolTip
            ToolTip toolTip_sfComboBox_LogName = new ToolTip();

            // Configura el tooltip sfComboBox_UserID
            toolTip_sfComboBox_LogName.AutoPopDelay = 5000;
            toolTip_sfComboBox_LogName.InitialDelay = 1000;
            toolTip_sfComboBox_LogName.ReshowDelay = 500;
            toolTip_sfComboBox_LogName.ShowAlways = true;

            // Establece el texto del tooltip para el botón
            toolTip_sfComboBox_LogName.SetToolTip(this.sfComboBox_LogName, "Names of log files containing records");


        }

        // repintar la datagrid despues de ordenada

        private void DataGridView1_Sorted(object sender, EventArgs e)
        {
            PaintGridView();
        }


        //Formatear la columna de tiempo con formato 24 hrs minutos y segundos
        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Asegúrate de que estás formateando la columna correcta
            if (dataGridView1.Columns[e.ColumnIndex].Name == "TimeCreated" && e.Value is DateTime)
            {
                
                // Con 7 cifras de millonesimas de segundos, si se dejan solo segundos no se puede ver en esta columna que evento ocurrio primero
                // para el caso de avrios eventos en el mismo segundo
                e.Value = ((DateTime)e.Value).ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                //e.Value = ((DateTime)e.Value).ToString("yyyy-MM-dd HH:mm:ss");
                e.FormattingApplied = true;
            }
        }



        //Esta funcion actualiza el contenido de las celdas a meidda que se seleccionan en el richtextbox
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedCells.Count > 0)
            {
               
                object cellValue = dataGridView1.SelectedCells[0].Value;

                // Actualiza el contenido del TextBox
                //textBox_Detail.Text = cellValue?.ToString();
                richTextBox_Detail.Text = cellValue?.ToString();
                // Funcion de resaltar el texto de la busqueda, amarillo para una busqueda y amarillo y magenta para dos busquedas / palabras 
                // respectivamente.
                highlight_Text();
            }
        }



        // Funcion para pintar la datagrid



        private void PaintGridView()

        {
            if (ConnectionString != "")
            {

                // Dioccionaria ara poner las labes
                Dictionary<string, int> labelColorDictionary = new Dictionary<string, int>();

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    DataTable dataTable = new DataTable();

                    // Crear un adaptador de datos para leer los labes desde la tabla en la base de datos sqlite
                    using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Labels", connection))
                    {


                        dataAdapter.Fill(dataTable);
                        foreach (System.Data.DataRow row in dataTable.Rows)
                        {

                            labelColorDictionary[row["Label_name"].ToString()] = int.Parse(row["Label_Color"].ToString());



                        }
                    }
                    connection.Close();
                }


                
                bool hasColumn = false;

                // Iterar a través de las columnas del DataGridView y verificar que si tiene la columna label.
                foreach (DataGridViewColumn columna in dataGridView1.Columns)
                {
                    if (columna.Name == "Label")
                    {
                        hasColumn = true;
                        break;
                    }
                }


                if (dataGridView1.RowCount <= 30000 && hasColumn == true)

                {

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {


                        if (row.Cells["Label"].Value == null || row.Cells["Label"].Value == DBNull.Value ||
    !labelColorDictionary.ContainsKey(row.Cells["Label"].Value.ToString()))
                        {

                            continue;

                        }

                        string Lbl = row.Cells["Label"].Value.ToString();
                        int colorValue = Convert.ToInt32(labelColorDictionary[Lbl]);
                        Color Color = Color.FromArgb(colorValue);
                        //textBox2.Text += Lbl;
                        row.Cells["Label"].Style.BackColor = Color;

                    }
                }
            }

        }









        //Esta funcion hace un parser del XML, hay que ajutarla para que muestre lo que es relevante lee el cambio de texto en el textbox

        /*private void richtextBox_Detail_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox_Detail.Text.Contains("xmlns="))
            {
                textBox_SubDetail.Text = "Hay un XML aqui!" + Environment.NewLine;

                try
                {
                    // Parsear el contenido XML del TextBox
                    XDocument doc = XDocument.Parse(richTextBox_Detail.Text);



                    // Iterar todos los elementos del documento XML
                    foreach (XElement element in doc.Descendants())
                    {
                        string elementName = element.Name.LocalName;
                        string elementValue = element.Value;
                        // Considera si necesitas mostrar el nombre del elemento, su valor, o ambos
                        textBox_SubDetail.Text += $"Nombre Elemento : {elementName}" + Environment.NewLine + $"Valor Elemento : {elementValue}" + Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al parsear XML: {ex.Message}");
                }
            }

            else
            {
                textBox_SubDetail.Text = "No hay un XML aqui!";
            }

        }*/


        //Funcion para sobrayar el texto buscado


        private void highlight_Text()
        {

            if (searchTermExists == true)
            {
                string text = searchTerm;
                int startIndex = 0;
                while (startIndex < richTextBox_Detail.TextLength)
                {
                    int wordStartIndex = richTextBox_Detail.Find(text, startIndex, RichTextBoxFinds.None);
                    if (wordStartIndex != -1)
                    {
                        //richTextBox_Detail.Select(wordStartIndex, text.Length);
                        richTextBox_Detail.SelectionStart = wordStartIndex;
                        richTextBox_Detail.SelectionLength = text.Length;
                        richTextBox_Detail.SelectionBackColor = Color.Yellow;
                        richTextBox_Detail.Refresh();
                        startIndex = wordStartIndex + text.Length;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (search2TermExists)
            {

                string text = searchTerm;
                int startIndex = 0;
                while (startIndex < richTextBox_Detail.TextLength)
                {
                    int wordStartIndex = richTextBox_Detail.Find(text, startIndex, RichTextBoxFinds.None);
                    if (wordStartIndex != -1)
                    {
                        //richTextBox_Detail.Select(wordStartIndex, text.Length);
                        richTextBox_Detail.SelectionStart = wordStartIndex;
                        richTextBox_Detail.SelectionLength = text.Length;
                        richTextBox_Detail.SelectionBackColor = Color.Yellow;
                        richTextBox_Detail.Refresh();

                        startIndex = wordStartIndex + text.Length;
                    }
                    else
                    {
                        break;
                    }
                }


                text = searchTerm2;
                startIndex = 0;
                while (startIndex < richTextBox_Detail.TextLength)
                {
                    int wordStartIndex = richTextBox_Detail.Find(text, startIndex, RichTextBoxFinds.None);
                    if (wordStartIndex != -1)
                    {
                        //richTextBox_Detail.Select(wordStartIndex, text.Length);
                        richTextBox_Detail.SelectionStart = wordStartIndex;
                        richTextBox_Detail.SelectionLength = text.Length;
                        richTextBox_Detail.SelectionBackColor = Color.LightPink;
                        richTextBox_Detail.Refresh();

                        startIndex = wordStartIndex + text.Length;
                    }
                    else
                    {
                        break;
                    }
                }




            }




        }









        // Aqui se cargan los Combobox con la informacion de la base de datos

        #region

        private void Load_sfComboBox_UserID()
        {

            List<string> UserID_List = new List<string>();

            string query = "SELECT DISTINCT UserID from LogData;";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {

                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {


                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // Paso 3: Rellenar el ComboBox con los resultados del SELECT
                            while (reader.Read())
                            {
                                //comboBox_UserID.Items.Add(reader["UserID"].ToString());
                                UserID_List.Add(reader["UserID"].ToString());
                            }
                        }
                        sfComboBox_UserID.DataSource = UserID_List;
                    }
                    catch (System.Data.SQLite.SQLiteException Ex)
                    {
                        MessageBox.Show("Data base unsupported: " + Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);

                    }

                }
                connection.Close();


            }
        }


        private void Load_sfComboBox_EventID()
        {

            List<string> EventID_List = new List<string>();

            string query = "SELECT DISTINCT EventID from LogData ORDER BY EventID;";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {


                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // Paso 3: Rellenar el ComboBox con los resultados del SELECT
                            while (reader.Read())
                            {
                                //comboBox_EventID.Items.Add(reader["EventID"].ToString());
                                EventID_List.Add(reader["EventID"].ToString());
                            }
                        }
                        sfComboBox_EventID.DataSource = EventID_List;
                    }
                    catch (System.Data.SQLite.SQLiteException Ex)
                    {
                        MessageBox.Show("Data base unsupported: " + Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }

                }
                connection.Close();
            }



        }



        private void Load_sfComboBox_MachineName()
        {

            List<string> MachineName_List = new List<string>();

            string query = "SELECT DISTINCT MachineName from LogData;";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {


                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // Paso 3: Rellenar el ComboBox con los resultados del SELECT
                            while (reader.Read())
                            {
                                //comboBox_EventID.Items.Add(reader["EventID"].ToString());
                                MachineName_List.Add(reader["MachineName"].ToString());
                            }
                        }
                        sfComboBox_MachineName.DataSource = MachineName_List;
                    }
                    catch (System.Data.SQLite.SQLiteException Ex)
                    {
                        MessageBox.Show("Data base unsupported: " + Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }

                }

                connection.Close();
            }



        }


        private void Load_sfComboBox_Level()
        {


            List<string> Level_List = new List<string>();

            string query = "SELECT DISTINCT Level from LogData ORDER BY Level;";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {


                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // Paso 3: Rellenar el ComboBox con los resultados del SELECT
                            while (reader.Read())
                            {
                                //comboBox_EventID.Items.Add(reader["EventID"].ToString());
                                Level_List.Add(reader["Level"].ToString());
                            }
                        }
                        sfComboBox_Level.DataSource = Level_List;
                    }
                    catch (System.Data.SQLite.SQLiteException Ex)
                    {
                        MessageBox.Show("Data base unsupported: " + Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }

                }
                connection.Close();
            }




        }




        private void Load_sfComboBox_LogName()
        {


            List<string> LogName_List = new List<string>();

            string query = "SELECT DISTINCT LogName from LogData ORDER BY LogName;";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {


                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // Paso 3: Rellenar el ComboBox con los resultados del SELECT
                            while (reader.Read())
                            {
                                //comboBox_EventID.Items.Add(reader["EventID"].ToString());
                                LogName_List.Add(reader["LogName"].ToString());
                            }
                        }
                        sfComboBox_LogName.DataSource = LogName_List;
                    }
                    catch (System.Data.SQLite.SQLiteException Ex)
                    {
                        MessageBox.Show("Data base unsupported: " + Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }

                }
                connection.Close();
            }



        }



        private void Load_sfComboBox_Label()
        {


            List<string> Label_List = new List<string>();

            string query = "SELECT  Label_name from Labels ORDER BY Label_name;";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {


                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // Paso 3: Rellenar el ComboBox con los resultados del SELECT
                            while (reader.Read())
                            {
                                //comboBox_EventID.Items.Add(reader["EventID"].ToString());
                                Label_List.Add(reader["Label_name"].ToString());
                            }
                        }
                        sfComboBox_Label.DataSource = Label_List;
                    }
                    catch (System.Data.SQLite.SQLiteException Ex)
                    {
                        MessageBox.Show("Data base unsupported: " + Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }

                }
                connection.Close();
            }




        }


        #endregion



        // Aqui empiezan las funciones que muestran en el datagrid los hits de los sub menu


        #region

        // Inicio primer boton / label menu View All Logs
        private void ShowAllLogs()
        {
            string query = "SELECT * from logdata;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "All Logs " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        // Inicio segundo sub menu General interest

        private void viewed_users()
        {
            string query = "SELECT DISTINCT UserID from LogData WHERE UserID != 'N/A';";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ShowQueryOnDataGridView(query);

            label_status.Text = "Viewed Users " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void LogOn_logOff()
        {

            string query = "SELECT * from LogData WHERE (EventID is 4624 or EventID is 4647) and LogName is \"Security\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Log on / Log off " + " ( " + HitNumber.ToString() + " search hits" + " ) ";
        }


        private void FailTo_LogOn()
        {

            string query = "SELECT * from LogData WHERE EventID is 4625 AND LogName is \"Security\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Failed to log on " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void PowerOn_Off()
        {
            string query = "SELECT * from LogData WHERE EventID is 4608 or EventID is 1074;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Power On / Off " + " ( " + HitNumber.ToString() + " search hits" + " ) ";
        }

        private void InstalledSoftware()
        {
            string query = "SELECT * from LogData WHERE (EventID is 1040 or EventID is 1042) And LogName is \"Application\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Installed Software " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void InstalledService()
        {

            string query = "select * from LogData WHERE EventID is 7045;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Installed Service " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void VSC()
        {

            string query = "SELECT * FROM LogData WHERE EventID is 8300 or EventID is 8301 or EventID is 8302;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Volume Shadow Copy " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void CreateLocalUser()
        {

            string query = "select * from LogData WHERE EventID is 4720;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "\tA user account was created. " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void DeletedUser()
        {
            string query = "select * from LogData WHERE EventID is 4726;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Deleted User " + " ( " + HitNumber.ToString() + " search hits" + " ) ";
        }

        private void RenamedUser()
        {

            string query = "select * from LogData WHERE EventID is 4781;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Renamed User " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        /*private void TerminalServerLogOn()
        {

            string query = "SELECT * from LogData WHERE EventID is 21 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Terminal server log on " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void TerminalServerShellStart()
        {

            string query = "SELECT * from LogData WHERE EventID is 22 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Remote Desktop Services: Shell start notification received: " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void TerminalServerLogOf()
        {

            string query = "SELECT * from LogData WHERE EventID is 23 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Terminal server log off " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void TerminalServerSessionDisconnected()
        {

            string query = "SELECT * from LogData WHERE EventID is 24 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Remote Desktop Services: Session has been disconnected: " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void TerminalServerSessionReconnection()
        {

            string query = "SELECT * from LogData WHERE EventID is 25 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Remote Desktop Services: Session reconnection succeeded: " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }*/





        //Fin primer sub menu


        // Inicio segundo sub menu : A User Account

        // El primero de usuariosc reados es igual que el CreateLocalUser(), se usara ese

        private void UserWasEnabled()
        {

            string query = "select * from LogData WHERE EventID is 4722;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A user account was enabled. " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void UserChangePassword()
        {

            string query = "select * from LogData WHERE EventID is 4723;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A user attempted to change an account’s password. " + " ( " + HitNumber.ToString() + " search hits" + " ) ";
        }


        private void UserResetPassword()
        {


            string query = "select * from LogData WHERE EventID is 4724;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "An attempt was made to reset an account’s password. " + " ( " + HitNumber.ToString() + " search hits" + " ) ";
        }

        private void UserAccountDisabled()
        {

            string query = "select * from LogData WHERE EventID is 4725;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A user account was disabled. " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void UserAcountDeleted()
        {

            string query = "select * from LogData WHERE EventID is 4726;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A user account was deleted. " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void UserAcountChanged()
        {

            string query = "select * from LogData WHERE EventID is 4738;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A user account was changed.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        // FIn del sub menu dos

        // Inicio Menu tres : Sec Enabl Global Grp

        private void GblGroupWasCreated()
        {

            string query = "select * from LogData WHERE EventID is 4727;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A security-enabled global group was created.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void GblGroupWasDeleted()
        {

            string query = "select * from LogData WHERE EventID is 4730;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A security-enabled global group was deleted.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void GblGroupWasChanged()
        {

            string query = "select * from LogData WHERE EventID is 4737;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A security-enabled global group was changed.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void GblGroupAMemberWasAdded()
        {

            string query = "select * from LogData WHERE EventID is 4728;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A member was added to a security-enabled global group.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void GblGroupAMemberWasRemoved()
        {

            string query = "select * from LogData WHERE EventID is 4729;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A member was removed from a security-enabled global group.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        // Inicio Menu Cuatro: Sec Enbl Local Grp

        private void LocalGroupWasCreated()
        {

            string query = "select * from LogData WHERE EventID is 4731;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A security - enabled local group was created     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void LocalGroupWasDeleted()
        {

            string query = "select * from LogData WHERE EventID is 4734;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A security-enabled local group was deleted     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void LocalGroupWasChanged()
        {

            string query = "select * from LogData WHERE EventID is 4735;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A security-enabled local group was changed     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void LocalAMemberWasAdded()
        {

            string query = "select * from LogData WHERE EventID is 4732;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A member was added to a security-enabled local group     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void LocalAMemberWasRemoved()
        {

            string query = "select * from LogData WHERE EventID is 4733;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A member was removed from a security-enabled local group     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void LocalGroupWasEnumerated()
        {

            string query = "select * from LogData WHERE EventID is 4799;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A security-enabled local group membership was enumerated     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        //Inicio menu Quinto : Sec Enbl Univ Grp



        private void UnivGroupWasCreated()
        {

            string query = "select * from LogData WHERE EventID is 4754;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A security-enabled universal group was created     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void UnivGroupWasDeleted()
        {

            string query = "select * from LogData WHERE EventID is 4758;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A security-enabled universal group was deleted     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void UnivGroupWasChanged()
        {

            string query = "select * from LogData WHERE EventID is 4755;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A security-enabled universal group was changed     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void UnivGroupAMemberWasAdded()
        {

            string query = "select * from LogData WHERE EventID is 4756;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A member was added to a security-enabled universal group     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void UnivGroupAMemberWasRemoved()
        {

            string query = "select * from LogData WHERE EventID is 4757;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A member was removed from a security-enabled universal group     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        //Inicio sexto menu : A computer account

        private void ComputerAccountWasCreated()
        {

            string query = "select * from LogData WHERE EventID is 4741;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A computer account was created     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void ComputerAccountWasDeleted()
        {

            string query = "select * from LogData WHERE EventID is 4743;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A computer account was deleted     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void ComputerAccountWasChanged()
        {

            string query = "select * from LogData WHERE EventID is 4742;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A computer account was changed     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }



        //Inicio septimo menu : Network Share 

        private void NetworkShareAccessed()
        {

            string query = "select * from LogData WHERE EventID is 5140;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A network share object was accessed     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void NetworkShareAdded()
        {

            string query = "select * from LogData WHERE EventID is 5142;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A network share object was added     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void NetworkShareModified()
        {

            string query = "select * from LogData WHERE EventID is 5143;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A network share object was modified     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void NetworkShareDeleted()
        {

            string query = "select * from LogData WHERE EventID is 5144;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A network share object was deleted    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void NetworkShareChecked()
        {

            string query = "select * from LogData WHERE EventID is 5145;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A network share object was checked    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        //Inicio octavo menu : Scheduled Task Activity


        private void ScheduledTaskCreated()
        {

            string query = "SELECT * from LogData WHERE EventID is 106 AND LogName is \"Microsoft-Windows-TaskScheduler/Operational\"; ";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Scheduled Task Created    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void ScheduledTaskUpdated()
        {

            string query = "SELECT * from LogData WHERE EventID is 140 AND LogName is \"Microsoft-Windows-TaskScheduler/Operational\"; ";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Scheduled Task Updated    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void ScheduledTaskDeleted()
        {

            string query = "SELECT * from LogData WHERE EventID is 141 AND LogName is \"Microsoft-Windows-TaskScheduler/Operational\"; ";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Scheduled Task Deleted    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void ScheduledTaskExecuted()
        {

            string query = "SELECT * from LogData WHERE EventID is 200 AND LogName is \"Microsoft-Windows-TaskScheduler/Operational\"; ";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Scheduled Task Executed    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void ScheduledTaskCompleted()
        {

            string query = "SELECT * from LogData WHERE EventID is 201 AND LogName is \"Microsoft-Windows-TaskScheduler/Operational\"; ";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Scheduled Task Completed    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        //Inicio noveno menu

        private void ObjScheduledTaskCreated()
        {

            string query = "select * from LogData WHERE EventID is 4698;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A scheduled task was created    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void ObjScheduledTaskDeleted()
        {

            string query = "select * from LogData WHERE EventID is 4699;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A scheduled task was deleted    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void ObjScheduledTaskEnabled()
        {

            string query = "select * from LogData WHERE EventID is 4700;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A scheduled task was enabled    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void ObjScheduledTaskDisabled()
        {

            string query = "select * from LogData WHERE EventID is 4701;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A scheduled task was disabled    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void ObjScheduledTaskUpdated()
        {

            string query = "select * from LogData WHERE EventID is 4702;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A scheduled task was updated    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        // Inicio decimo menu


        private void HandletoAnObjectRequested()
        {

            string query = "select * from LogData WHERE EventID is 4656;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A handle to an object was requested    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void RegistryValueModified()
        {

            string query = "select * from LogData WHERE EventID is 4657;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A registry value was modified    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void HandletoAnObjectClosed()
        {

            string query = "select * from LogData WHERE EventID is 4658;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The handle to an object was closed    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void ObjectWasDeleted()
        {

            string query = "select * from LogData WHERE EventID is 4660;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "An object was deleted    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void AttemptMadetoAccObject()
        {

            string query = "select * from LogData WHERE EventID is 4663;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "An attempt was made to access an object    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        // Inicio menu once


        private void EventLogServiceStarted()
        {

            string query = "select * from LogData WHERE EventID is 6005  AND LogName is 'System';";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The event log service was started    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void EventLogServiceStoped()
        {

            string query = "select * from LogData WHERE EventID is 6006  AND LogName is 'System';";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The event log service was stopped    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void ServiceTerminatedUnexpectedly()
        {

            string query = "select * from LogData WHERE EventID is 7034  AND LogName is 'System';";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A service terminated unexpectedly    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void ServiceStartStop()
        {

            string query = "select * from LogData WHERE EventID is 7036  AND LogName is 'System';";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A service was stopped or started    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void StartTypeForServiceChanged()
        {

            string query = "select * from LogData WHERE EventID is 7040  AND LogName is 'System';";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The start type for a service was changed    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void ServiceInstalledbySystem()
        {

            string query = "select * from LogData WHERE EventID is 7045  AND LogName is 'System';";

            ShowQueryOnDataGridView(query);

            label_status.Text = "A service was installed by the system    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        // Inicio del menu doce: Wi-Fi Connection

        private void WlanSuccessfullyConnected()
        {

            string query = "select * from LogData WHERE EventID is 8001 AND LogName is \"Microsoft-Windows-WLAN-AutoConfig/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "WLAN service has successfully connected    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void WlanFailedToConnect()
        {

            string query = "select * from LogData WHERE EventID is 8002 AND LogName is \"Microsoft-Windows-WLAN-AutoConfig/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "WLAN service failed to connect to a wireless network    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        // Inicio del menu trece: Windows Filtering Platform

        private void BlockAppAcceptIncomConn()
        {

            string query = "select * from LogData WHERE EventID is 5031;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The Windows Firewall Service blocked an application from accepting incoming connections on the network.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void BlokedaPacket()
        {

            string query = "select * from LogData WHERE EventID is 5152;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The WFP blocked a packet.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void PermitedIncommigConn()
        {

            string query = "select * from LogData WHERE EventID is 5154;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The WFP has permitted an application or service to listen on a port for incoming connections.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void AllowedConnection()
        {

            string query = "select * from LogData WHERE EventID is 5156;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The WFP has allowed a connection.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void BlockedConnection()
        {

            string query = "select * from LogData WHERE EventID is 5157;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The WFP has blocked a connection.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void PermitedBindaLocalPort()
        {

            string query = "select * from LogData WHERE EventID is 5158;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The WFP has permitted a bind to a local port.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void BlockBindaLocalPort()
        {

            string query = "select * from LogData WHERE EventID is 5159;";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The WFP has blocked a bind to a local port.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        // Inicio del menu catorce: Windows Defender



        private void FoundMalware()
        {

            string query = "SELECT * FROM LogData WHERE EventID is 1006 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The antimalware engine found malware or other potentially unwanted software.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void ActionProtectSystem()
        {

            string query = "SELECT * FROM LogData WHERE EventID is 1007 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The antimalware platform performed an action to protect your system from malware.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void FailedActionProtectSystem()
        {

            string query = "SELECT * FROM LogData WHERE EventID is 1008 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The antimalware platform attempted to perform an action to protect your system, but the action failed.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void DeletedHistoryMalware()
        {

            string query = "SELECT * FROM LogData WHERE EventID is 1013 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The antimalware platform deleted history of malware and other potentially unwanted software.   " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void DetectedSuspiciousBehavior()
        {

            string query = "SELECT * FROM LogData WHERE EventID is 1015 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The antimalware platform detected suspicious behavior.   " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void DetectedMalware()
        {

            string query = "SELECT * FROM LogData WHERE EventID is 1116 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The antimalware platform detected malware or other potentially unwanted software.   " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void CriticalErrorInActionOnMalware()
        {

            string query = "SELECT * FROM LogData WHERE EventID is 1119 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The antimalware platform encountered a critical error when trying to take action on malware.   " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void RealTimeProtectionDisabled()
        {

            string query = "SELECT * FROM LogData WHERE EventID is 5001 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Real-time protection is disabled.   " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void RealTimeProtectionCfgChanged()
        {

            string query = "SELECT * FROM LogData WHERE EventID is 5004 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The real-time protection configuration changed.   " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void PlatformCfgChanged()
        {

            string query = "SELECT * FROM LogData WHERE EventID is 5007 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";

            ShowQueryOnDataGridView(query);

            label_status.Text = "The antimalware platform configuration changed.   " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void ScanningMalwareDisabled()
        {

            string query = "SELECT * FROM LogData WHERE EventID is 5010 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Scanning for malware and other potentially unwanted software is disabled.   " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void ScanningVirusDisabled()
        {

            string query = "SELECT * FROM LogData WHERE EventID is 5012 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Scanning for viruses is disabled.   " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }



        // Inicio del menu quince: Sysmon


        private void ProcessCreation()
        {

            string query = "select * from LogData WHERE EventID is 1 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Sysmon Process creation.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }



        private void ProcChngFileCreationTime()
        {

            string query = "select * from LogData WHERE EventID is 2 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";"; ;

            ShowQueryOnDataGridView(query);

            label_status.Text = "A process changed a file creation time.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void NetworkConnection()
        {

            string query = "select * from LogData WHERE EventID is 3 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Network connection.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void SysmonServiceStateChanged()
        {

            string query = "select * from LogData WHERE EventID is 4 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Sysmon service state changed.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void ProcessTerminated()
        {

            string query = "select * from LogData WHERE EventID is 5 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Sysmon service state changed.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void DriverLoaded()
        {

            string query = "select * from LogData WHERE EventID is 6 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Driver loaded.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }



        private void ImageLoaded()
        {

            string query = "select * from LogData WHERE EventID is 7 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Image loaded (records when a module is loaded in a specific process).     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void CreateRemoteThread()
        {

            string query = "select * from LogData WHERE EventID is 8 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Create Remote Thread (creating a thread in another process).     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void RawAccessRead()
        {

            string query = "select * from LogData WHERE EventID is 9 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Raw Access Read (raw access to drive data using \\\\.\\ notation).     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }



        private void ProcessAccess()
        {

            string query = "select * from LogData WHERE EventID is 10 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Process Access (opening access to another process’s memory space).     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void FileCreate()
        {

            string query = "select * from LogData WHERE EventID is 11 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "File Create (creating or overwriting a file).     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void RegKeyValueCreatedDeleted()
        {

            string query = "select * from LogData WHERE EventID is 12 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Registry key or value created or deleted.     " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void RegistryValueModification()
        {

            string query = "select * from LogData WHERE EventID is 13 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Registry value modification.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void RegistryKeyValueRenamed()
        {

            string query = "select * from LogData WHERE EventID is 14 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Registry key or value renamed.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }




        private void FileCreateStreamHash()
        {

            string query = "select * from LogData WHERE EventID is 15 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "File Create Stream Hash (creation of an alternate data stream).    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void SysmonConfigurationChange()
        {

            string query = "select * from LogData WHERE EventID is 16 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Sysmon configuration change.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void NamedPipeCreated()
        {

            string query = "select * from LogData WHERE EventID is 17 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Named pipe created.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void NamedPipeConnected()
        {

            string query = "select * from LogData WHERE EventID is 18 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Named pipe connected.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void WMIEventFilterActivityDetected()
        {

            string query = "select * from LogData WHERE EventID is 19 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "WMI Event Filter activity detected.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void WMIEventConsumerActivityDetected()
        {

            string query = "select * from LogData WHERE EventID is 20 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "WMI Event Consumer activity detected.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void WMIEventConsumerToFilterActivityDetected()
        {

            string query = "select * from LogData WHERE EventID is 21 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "WMI Event Consumer To Filter activity detected.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void DNSQueryEvent()
        {

            string query = "select * from LogData WHERE EventID is 22 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "DNS query event.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }


        private void SysmonError()
        {

            string query = "select * from LogData WHERE EventID is 255 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Sysmon error.    " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }



        // Inicio menu 16 Terminal Server


        private void TerminalServerLogOn()
        {

            string query = "SELECT * from LogData WHERE EventID is 21 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Terminal server log on " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void TerminalServerShellStart()
        {

            string query = "SELECT * from LogData WHERE EventID is 22 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Remote Desktop Services: Shell start notification received: " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void TerminalServerLogOf()
        {

            string query = "SELECT * from LogData WHERE EventID is 23 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Terminal server log off " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void TerminalServerSessionDisconnected()
        {

            string query = "SELECT * from LogData WHERE EventID is 24 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Remote Desktop Services: Session has been disconnected: " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void TerminalServerSessionReconnection()
        {

            string query = "SELECT * from LogData WHERE EventID is 25 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Remote Desktop Services: Session reconnection succeeded: " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }


        private void TerminalServerSessionDisconnectedbySession()
        {

            string query = "SELECT * from LogData WHERE EventID is 39 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Session disconnected by another session " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }

        private void TerminalServerSessionDisconnectedCode()
        {

            string query = "SELECT * from LogData WHERE EventID is 40 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";

            ShowQueryOnDataGridView(query);

            label_status.Text = "Session has been disconnected reason / code " + " ( " + HitNumber.ToString() + " search hits" + " ) ";


        }




        // Aqui terminan las funciones que muestran los hits de cada sub menu en la datagrid
        //
        //
        //
        //*********************************************************************

        #endregion




        // Menu de la izqu
        private void AcordeonMenu()
        {

            Font fm = new Font(Font.FontFamily, 11);
            Font fs = new Font(Font.FontFamily, 10);

            // Main menu body
            // Primer Flow
            FlowLayoutPanel flwMain = new FlowLayoutPanel()
            {
                BackColor = this.BackColor,
                BorderStyle = BorderStyle.FixedSingle,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Width = 350,
                Margin = new Padding(0, 300, 0, 0),
                WrapContents = false,
                AutoScroll = true,
                //HorizontalScroll.Visible = false;

            };
            // Logo
            PictureBox picLogo = new PictureBox()
            {
                Image = Resource1.Icon32,
                Height = 150,
                SizeMode = PictureBoxSizeMode.CenterImage,
                Width = flwMain.Width
            };

            // Primer label button

            Label lblBtn01 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Preview_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Preview Logs ",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            // Segundo label button

            Label lblBtn02 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Download_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Acquire Logs ",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            // Tercer label button

            Label lblBtn03 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.All_Logs_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "View All Logs   " + All_Logs.ToString(),
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            // Cuarto label button

            Label lblBtn04 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Full_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Process Log Folder ",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };




            // Primer Label "desplegable"



            Label lblMnu01 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Important_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "General Interest   " + General_Interest.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };


            // Sub flow para primer sub menu
            FlowLayoutPanel flwSub01 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            //Arranco labes sub menu uno aqui

            Label lblSub0101 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Viewed Users  " + Viewed_Users.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0102 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Log on / Log off   " + Log_on_Log_off.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            Label lblSub0103 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Failed to Log on  " + Failed_to_Log_on.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            Label lblSub0104 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Power On / Off   " + Power_On_Off.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            Label lblSub0105 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Installed Software   " + Installed_Software.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            Label lblSub0106 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Installed a Service   " + Installed_Service.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            Label lblSub0107 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Volume Shadow Copy   " + Volume_Shadow_Copy.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            Label lblSub0108 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Create Local Users   " + Create_Local_Users.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            Label lblSub0109 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Deleted Users   " + Deleted_Users.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            Label lblSub0110 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Renamed Users   " + Renamed_Users.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0111 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Terminal Server Log On   " + Terminal_Server_Log_On.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            /*Label lblSub0112 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Terminal Server Shell Start   " + Terminal_Server_Shell_Start.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };*/

            Label lblSub0113 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Terminal Server Log Off   " + Terminal_Server_Log_Off.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            /*Label lblSub0114 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Terminal Server Session Disconnected   " + Terminal_Server_Session_Disconnected.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };*/

            /*Label lblSub0115 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Terminal Server Session Reconnection   " + Terminal_Server_Session_Reconnection.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };*/




            // Aqui seguir con terminal server etc... 



            // Segundo label desplegable

            Label lblMnu02 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.User_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "A user account   " + UserAccount.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };


            // flw panel contenedor de submenus 02

            FlowLayoutPanel flwSub02 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub0201 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "was created   " + User_was_created.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0202 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "was enabled   " + User_was_enabled.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0203 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "change password   " + User_chg_pass.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0204 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "reset password   " + User_reset_pass.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0205 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "was disabled   " + User_was_disabled.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0206 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "was deleted   " + User_was_deleted.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0207 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "was changed   " + User_was_changed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };



            // Tercer Label "desplegable"


            Label lblMnu03 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.GlobalGroup_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Sec enbl Glob Grp   " + Sec_Enbl_Glob_Grp.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub03 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub0301 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Group was created " + Globgrp_Group_Was_Created.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0302 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Group was deleted " + Globgrp_Group_Was_Deleted.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0303 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Group was changed " + Globgrp_Group_Was_Changed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0304 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "A member was added " + Globgrp_AMember_Was_Added.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0305 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "A member was removed " + Globgrp_AMember_Was_Removed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };



            // Cuarto Label "desplegable"


            Label lblMnu04 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.LocalGroup_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Sec enbl Local Grp   " + Sec_Enbl_Local_Grp.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub04 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub0401 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Group was created   " + LocalGrp_Group_Was_Created.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0402 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Group was deleted   " + LocalGrp_Group_Was_Deleted.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0403 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Group was changed   " + LocalGrp_Group_Was_Changed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0404 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "A member was added   " + LocalGrp_AMember_Was_Added.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0405 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "A member was removed   " + LocalGrp_AMember_Was_Removed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0406 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Group was enumerated   " + LocalGrp_Group_Was_Enumerated.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };



            // Quinto Label "desplegable"


            Label lblMnu05 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.UniversalGroup_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Sec enbl Univ Grp " + Sec_Enbl_Univ_Grp.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub05 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub0501 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Group was created   " + UnivGrp_Group_Was_Created.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0502 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Group was deleted   " + UnivGrp_Group_Was_Deleted.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0503 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Group was changed   " + UnivGrp_Group_Was_Changed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0504 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "A member was added   " + UnivGrp_AMember_Was_Added.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0505 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "A member was removed   " + UnivGrp_AMember_Was_Removed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            // Sexto Label "desplegable"


            Label lblMnu06 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Computer_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "A computer account   " + Computer_Account.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub06 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub0601 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Computer was created   " + Computer_Account_Was_Created.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0602 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Computer was deleted   " + Computer_Account_Was_Deleted.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0603 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Computer was changed   " + Computer_Account_Was_Changed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            // Septimo Label "desplegable"


            Label lblMnu07 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Shared_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Network Share " + Network_Share.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub07 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub0701 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Network share accessed   " + Network_Share_Accessed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0702 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Network share added   " + Network_Share_Added.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0703 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Network share modified   " + Network_Share_Modified.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0704 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Network share deleted   " + Network_Share_Deleted.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0705 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Network share checked  " + Network_Share_Checked.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };



            // Octavo Label "desplegable"


            Label lblMnu08 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Task_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Scheduled Task Activity   " + Scheduled_Task_Activity.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub08 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub0801 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Scheduled Task Created   " + Scheduled_Task_Created.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0802 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Scheduled Task Updated   " + Scheduled_Task_Updated.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0803 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Scheduled Task Deleted   " + Scheduled_Task_Deleted.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0804 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Scheduled Task Executed   " + Scheduled_Task_Executed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0805 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Scheduled Task Completed   " + Scheduled_Task_Completed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            // Aqui empieza noveno menu
            //Object Access Auditing - Scheduled Task Event IDs


            Label lblMnu09 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Task_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Object Access Auditing   " + Object_Access_Auditing.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub09 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub0901 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Scheduled Task Created   " + Obj_Scheduled_Task_Created.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0902 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Scheduled Task Deleted   " + Obj_Scheduled_Task_Deleted.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0903 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Scheduled Task Enabled   " + Obj_Scheduled_Task_Enabled.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0904 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Scheduled Task Disabled   " + Obj_Scheduled_Task_Disabled.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub0905 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Scheduled Task Updated   " + Obj_Scheduled_Task_Updated.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };






            //Aqui decimo menu
            //Audit Policy Changes

            Label lblMnu10 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Object_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Object Handle Event   " + Object_Handle_Event.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub10 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub1001 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Handle to an Object Requested   " + Handle_toAn_Object_Requested.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1002 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Registry Value Modified   " + Registry_Value_Modified.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1003 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Handle to an Object Closed   " + Handle_toAn_Object_Closed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1004 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Object Was Deleted   " + Object_Was_Deleted.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1005 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Attempt Made to Access Object   " + Attempt_Made_to_Acc_Object.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };





            //Aqui empieza menu 11
            // Auditing Windows Services



            Label lblMnu11 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Audit_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Auditing Windows Services  " + Auditing_Windows_Services.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub11 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub1101 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Event Log Service Started   " + Event_Log_Service_Started.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1102 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Event Log Service Stoped   " + Event_Log_Service_Stoped.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1103 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Service Terminated Unexpectedly   " + Service_Terminated_Unexpectedly.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1104 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Service Stopped / Started   " + Service_Start_Stop.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1105 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Start Type For Service Changed   " + Start_Type_For_Service_Changed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1106 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Service Installed by System   " + Service_Installed_bySystem.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };



            //Aqui empieza menu 12
            // Wireless LAN Auditing


            Label lblMnu12 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Wifi_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Wi-Fi Connection  " + Wlan_Connection.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub12 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub1201 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "WLAN Successfully Connected   " + Wlan_Successfully_Connected.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1202 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "WLAN Failed to Connect   " + Wlan_Failed_To_Connect.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            //Aqui empieza menu 13
            //Windows Filtering Platform

            Label lblMnu13 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Filtering_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Windows Filtering Platform   " + Windows_Filtering_Platform.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub13 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub1301 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Block App Accept Incom Conn   " + Block_App_Accept_Incom_Conn.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1302 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Blocked a Packet   " + Bloked_a_Packet.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1303 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Permitted Incoming Conn   " + Permited_Incommig_Conn.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1304 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Allowed Connection   " + Allowed_Connection.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1305 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Blocked Connection   " + Blocked_Connection.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1306 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Permitted Bind Local Port   " + Permited_Bind_a_Local_Port.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1307 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Block Bind Local Port   " + Block_Bind_a_Local_Port.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };



            //Aqui empieza menu 14
            // Windows Defender Suspicious Event IDs


            Label lblMnu14 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Defender_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Windows Defender   " + Windows_Defender.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub14 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub1401 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Found Malware / Unwanted Software   " + Found_Malware.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1402 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Action Protect System   " + Action_Protect_System.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1403 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Failed Action Protect System   " + Failed_Action_Protect_System.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1404 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Deleted History Malware / Unwanted Sw " + Deleted_History_Malware.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1405 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Detected Suspicious Behavior   " + Detected_Suspicious_Behavior.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1406 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Detected Malware / Unwanted Sw   " + Detected_Malware.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };



            //De la fuente https://cybersecuritynews.com/windows-event-log-analysis/
            // Hubo que saltar dos en este grupo pq estaban repetidos


            Label lblSub1407 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Critical Error in Action on Malware   " + Critical_Error_In_Action_On_Malware.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1408 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Real-time Protection Disabled   " + Real_Time_Protection_Disabled.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1409 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Real-time Protection Cfg Changed   " + Real_Time_Protection_Cfg_Changed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1410 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Platform Cfg Changed   " + Platform_Cfg_Changed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1411 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Scanning Malware Disabled   " + Scanning_Malware_Disabled.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1412 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Scanning Viruses Disabled.   " + Scanning_Virus_Disabled.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };



            //Aqui empieza menu 15
            // Sysmon

            Label lblMnu15 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.Monitor_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Sysmon   " + Sysmon.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub15 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };

            Label lblSub1501 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Process Creation   " + Process_Creation.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1502 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Proc Chng File Creation Time   " + Proc_Chng_File_Creation_Time.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1503 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Network Connection   " + Network_Connection.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1504 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Sysmon Service State Changed " + Sysmon_Service_State_Changed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1505 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Process Terminated   " + Process_Terminated.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1506 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Driver Loaded  " + Driver_Loaded.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1507 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Image Loaded   " + Image_Loaded.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1508 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Create Remote Thread   " + Create_Remote_Thread.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1509 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Raw Access Read   " + Raw_Access_Read.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1510 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Process Access   " + Process_Access.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1511 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "File Create    " + File_Create.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1512 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Reg Key / Value Created / Deleted   " + Reg_Key_Value_Created_Deleted.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1513 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Registry Value Modification   " + Registry_Value_Modification.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1514 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Registry Key or Value Renamed   " + Registry_Key_Value_Renamed.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1515 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "File Create Stream Hash   " + File_Create_Stream_Hash.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1516 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Sysmon Configuration Change   " + Sysmon_Configuration_Change.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            Label lblSub1517 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Named Pipe Created   " + Named_Pipe_Created.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1518 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Named Pipe Connected   " + Named_Pipe_Connected.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1519 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "WMIEventFilter Activity   " + WMIEventFilter_Activity_Detected.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1520 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "WMIEventConsumer  Activity   " + WMIEventConsumer_Activity_Detected.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1521 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "WMIEventConsumerToFilter Activity   " + WMIEventConsumerToFilter_Activity_Detected.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1522 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "DNS Query Event   " + DNS_Query_Event.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1523 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Sysmon error   " + Sysmon_Error.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };


            //Aqui empieza menu 16
            //Terminal Server

            Label lblMnu16 = new Label()
            {
                BackColor = Color.SteelBlue,
                Font = fm,
                ForeColor = Color.White,
                Height = 48,
                Image = Resource1.remote_desktop_24,
                ImageAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 1, 3, 1),
                Padding = new Padding(12, 3, 0, 3),
                Text = "Terminal Server   " + Terminal_Server.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = flwMain.Width
            };

            FlowLayoutPanel flwSub16 = new FlowLayoutPanel()
            {
                AutoSize = true,
                BackColor = Color.Beige,
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Left,
                Visible = false,
                Width = 250
            };


            Label lblSub1601 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Terminal Server Log On   " + Terminal_Server_Log_On.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1602 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Terminal Server Shell Start   " + Terminal_Server_Shell_Start.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1603 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Terminal Server Log Off   " + Terminal_Server_Log_Off.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1604 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Terminal Server Session Disconnected   " + Terminal_Server_Session_Disconnected.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1605 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "Terminal Server Session Reconnection   " + Terminal_Server_Session_Reconnection.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1606 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "TS Session Disconnected by Session   " + Terminal_Server_Session_Disconnected_by_Session.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };

            Label lblSub1607 = new Label()
            {
                BackColor = Color.Transparent,
                Font = fs,
                ForeColor = Color.Black,
                Padding = new Padding(32, 1, 0, 1),
                Text = "TS Session Disconnected   " + Terminal_Server_Session_Disconnected_Code.ToString() + " hits",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = flwMain.Width
            };






            //Aqui empieza menu 17
            //Auditing PowerShell Use 





            //Aqui realmente solo adiciona las etiquetas de los respectivos sub menu
            //Monta primer panel de sub items
            flwSub01.Controls.Add(lblSub0101);
            flwSub01.Controls.Add(lblSub0102);
            flwSub01.Controls.Add(lblSub0103);
            flwSub01.Controls.Add(lblSub0104);
            flwSub01.Controls.Add(lblSub0105);
            flwSub01.Controls.Add(lblSub0106);
            flwSub01.Controls.Add(lblSub0107);
            flwSub01.Controls.Add(lblSub0108);
            flwSub01.Controls.Add(lblSub0109);
            flwSub01.Controls.Add(lblSub0110);
            flwSub01.Controls.Add(lblSub0111);
            //flwSub01.Controls.Add(lblSub0112);
            flwSub01.Controls.Add(lblSub0113);
            //flwSub01.Controls.Add(lblSub0114);
            //flwSub01.Controls.Add(lblSub0115);




            //Segundo panel de sub items

            flwSub02.Controls.Add(lblSub0201);
            flwSub02.Controls.Add(lblSub0202);
            flwSub02.Controls.Add(lblSub0203);
            flwSub02.Controls.Add(lblSub0204);
            flwSub02.Controls.Add(lblSub0205);
            flwSub02.Controls.Add(lblSub0206);
            flwSub02.Controls.Add(lblSub0207);



            //Tercer panel de subitems
            flwSub03.Controls.Add(lblSub0301);
            flwSub03.Controls.Add(lblSub0302);
            flwSub03.Controls.Add(lblSub0303);
            flwSub03.Controls.Add(lblSub0304);
            flwSub03.Controls.Add(lblSub0305);


            //Cuarto panel de sub items

            flwSub04.Controls.Add(lblSub0401);
            flwSub04.Controls.Add(lblSub0402);
            flwSub04.Controls.Add(lblSub0403);
            flwSub04.Controls.Add(lblSub0404);
            flwSub04.Controls.Add(lblSub0405);
            flwSub04.Controls.Add(lblSub0406);


            // Quinto panel de sub items

            flwSub05.Controls.Add(lblSub0501);
            flwSub05.Controls.Add(lblSub0502);
            flwSub05.Controls.Add(lblSub0503);
            flwSub05.Controls.Add(lblSub0504);
            flwSub05.Controls.Add(lblSub0505);


            // Sexto panel de sub items

            flwSub06.Controls.Add(lblSub0601);
            flwSub06.Controls.Add(lblSub0602);
            flwSub06.Controls.Add(lblSub0603);


            // Septimo panel de sub items

            flwSub07.Controls.Add(lblSub0701);
            flwSub07.Controls.Add(lblSub0702);
            flwSub07.Controls.Add(lblSub0703);
            flwSub07.Controls.Add(lblSub0704);
            flwSub07.Controls.Add(lblSub0705);



            // Octavo panel de sub items

            flwSub08.Controls.Add(lblSub0801);
            flwSub08.Controls.Add(lblSub0802);
            flwSub08.Controls.Add(lblSub0803);
            flwSub08.Controls.Add(lblSub0804);
            flwSub08.Controls.Add(lblSub0805);

            // Noveno panel de sub items

            flwSub09.Controls.Add(lblSub0901);
            flwSub09.Controls.Add(lblSub0902);
            flwSub09.Controls.Add(lblSub0903);
            flwSub09.Controls.Add(lblSub0904);
            flwSub09.Controls.Add(lblSub0905);



            // Decimo panel de sub items

            flwSub10.Controls.Add(lblSub1001);
            flwSub10.Controls.Add(lblSub1002);
            flwSub10.Controls.Add(lblSub1003);
            flwSub10.Controls.Add(lblSub1004);
            flwSub10.Controls.Add(lblSub1005);


            // Once panel de sub items

            flwSub11.Controls.Add(lblSub1101);
            flwSub11.Controls.Add(lblSub1102);
            flwSub11.Controls.Add(lblSub1103);
            flwSub11.Controls.Add(lblSub1104);
            flwSub11.Controls.Add(lblSub1105);
            flwSub11.Controls.Add(lblSub1106);

            // Doce panel de sub items

            flwSub12.Controls.Add(lblSub1201);
            flwSub12.Controls.Add(lblSub1202);

            // Trece panel de sub items

            flwSub13.Controls.Add(lblSub1301);
            flwSub13.Controls.Add(lblSub1302);
            flwSub13.Controls.Add(lblSub1303);
            flwSub13.Controls.Add(lblSub1304);
            flwSub13.Controls.Add(lblSub1305);
            flwSub13.Controls.Add(lblSub1306);
            flwSub13.Controls.Add(lblSub1307);


            // Catorce panel de sub items

            flwSub14.Controls.Add(lblSub1401);
            flwSub14.Controls.Add(lblSub1402);
            flwSub14.Controls.Add(lblSub1403);
            flwSub14.Controls.Add(lblSub1404);
            flwSub14.Controls.Add(lblSub1405);
            flwSub14.Controls.Add(lblSub1406);
            flwSub14.Controls.Add(lblSub1407);
            flwSub14.Controls.Add(lblSub1408);
            flwSub14.Controls.Add(lblSub1409);
            flwSub14.Controls.Add(lblSub1410);
            flwSub14.Controls.Add(lblSub1411);
            flwSub14.Controls.Add(lblSub1412);


            // Quince panel de sub items

            flwSub15.Controls.Add(lblSub1501);
            flwSub15.Controls.Add(lblSub1502);
            flwSub15.Controls.Add(lblSub1503);
            flwSub15.Controls.Add(lblSub1504);
            flwSub15.Controls.Add(lblSub1505);
            flwSub15.Controls.Add(lblSub1506);
            flwSub15.Controls.Add(lblSub1507);
            flwSub15.Controls.Add(lblSub1508);
            flwSub15.Controls.Add(lblSub1509);
            flwSub15.Controls.Add(lblSub1510);
            flwSub15.Controls.Add(lblSub1511);
            flwSub15.Controls.Add(lblSub1512);
            flwSub15.Controls.Add(lblSub1513);
            flwSub15.Controls.Add(lblSub1514);
            flwSub15.Controls.Add(lblSub1515);
            flwSub15.Controls.Add(lblSub1516);
            flwSub15.Controls.Add(lblSub1517);
            flwSub15.Controls.Add(lblSub1518);
            flwSub15.Controls.Add(lblSub1519);
            flwSub15.Controls.Add(lblSub1520);
            flwSub15.Controls.Add(lblSub1521);
            flwSub15.Controls.Add(lblSub1522);
            flwSub15.Controls.Add(lblSub1523);

            // Dies y seis panel sub items


            flwSub16.Controls.Add(lblSub1601);
            flwSub16.Controls.Add(lblSub1602);
            flwSub16.Controls.Add(lblSub1603);
            flwSub16.Controls.Add(lblSub1604);
            flwSub16.Controls.Add(lblSub1605);
            flwSub16.Controls.Add(lblSub1606);
            flwSub16.Controls.Add(lblSub1607);





            //Adiciona panel ppal al formulario
            //this.Controls.Add(flwMain);
            groupBox_Main.Controls.Add(flwMain);

            // Adiciona el picbox
            //flwMain.Controls.Add(picLogo);

            // Adiciona LabelBtn01 y LabelBtn02

            flwMain.Controls.Add(lblBtn01);
            flwMain.Controls.Add(lblBtn02);
            flwMain.Controls.Add(lblBtn04);
            flwMain.Controls.Add(lblBtn03);

            //Adiciona Primer conjunto a nivel de label
            flwMain.Controls.Add(lblMnu01);
            //Panel 1er flowpanel que contiene las etiquedas de subitems adicionadas arriba
            flwMain.Controls.Add(flwSub01);

            //Adiciona seg item
            flwMain.Controls.Add(lblMnu02);
            //Panel 2do item
            flwMain.Controls.Add(flwSub02);

            //Adiciona tercer item
            flwMain.Controls.Add(lblMnu03);
            //Panel 3er item
            flwMain.Controls.Add(flwSub03);

            //Adiciona cuarto item
            flwMain.Controls.Add(lblMnu04);
            flwMain.Controls.Add(flwSub04);

            //Adiciona quinto item
            flwMain.Controls.Add(lblMnu05);
            flwMain.Controls.Add(flwSub05);

            //Adiciona sexto item
            flwMain.Controls.Add(lblMnu06);
            flwMain.Controls.Add(flwSub06);

            //Adiciona septimo item
            flwMain.Controls.Add(lblMnu07);
            flwMain.Controls.Add(flwSub07);

            //Adiciona octavo item
            flwMain.Controls.Add(lblMnu08);
            flwMain.Controls.Add(flwSub08);

            //Adiciona noveno item
            flwMain.Controls.Add(lblMnu09);
            flwMain.Controls.Add(flwSub09);

            //Adiciona decimo item
            flwMain.Controls.Add(lblMnu10);
            flwMain.Controls.Add(flwSub10);

            //Adiciona once item
            flwMain.Controls.Add(lblMnu11);
            flwMain.Controls.Add(flwSub11);

            //Adiciona doce item
            flwMain.Controls.Add(lblMnu12);
            flwMain.Controls.Add(flwSub12);

            //Adiciona diez y seis item
            flwMain.Controls.Add(lblMnu16);
            flwMain.Controls.Add(flwSub16);

            //Adiciona trece item
            flwMain.Controls.Add(lblMnu13);
            flwMain.Controls.Add(flwSub13);

            //Adiciona catorce item
            flwMain.Controls.Add(lblMnu14);
            flwMain.Controls.Add(flwSub14);

            //Adiciona catorce item
            flwMain.Controls.Add(lblMnu15);
            flwMain.Controls.Add(flwSub15);




            //Acciones sobre los Menus

            lblMnu01.Click += (Object sender, EventArgs e) => { flwSub01.Visible = !flwSub01.Visible; };
            lblMnu01.MouseHover += (Object sender, EventArgs e) => { lblMnu01.BackColor = Color.LightBlue; };
            lblMnu01.MouseLeave += (Object sender, EventArgs e) => { lblMnu01.BackColor = Color.SteelBlue; };

            lblMnu02.Click += (Object sender, EventArgs e) => { flwSub02.Visible = !flwSub02.Visible; };
            lblMnu02.MouseHover += (Object sender, EventArgs e) => { lblMnu02.BackColor = Color.LightBlue; };
            lblMnu02.MouseLeave += (Object sender, EventArgs e) => { lblMnu02.BackColor = Color.SteelBlue; };

            lblMnu03.Click += (Object sender, EventArgs e) => { flwSub03.Visible = !flwSub03.Visible; };
            lblMnu03.MouseHover += (Object sender, EventArgs e) => { lblMnu03.BackColor = Color.LightBlue; };
            lblMnu03.MouseLeave += (Object sender, EventArgs e) => { lblMnu03.BackColor = Color.SteelBlue; };

            lblMnu04.Click += (Object sender, EventArgs e) => { flwSub04.Visible = !flwSub04.Visible; };
            lblMnu04.MouseHover += (Object sender, EventArgs e) => { lblMnu04.BackColor = Color.LightBlue; };
            lblMnu04.MouseLeave += (Object sender, EventArgs e) => { lblMnu04.BackColor = Color.SteelBlue; };

            lblMnu05.Click += (Object sender, EventArgs e) => { flwSub05.Visible = !flwSub05.Visible; };
            lblMnu05.MouseHover += (Object sender, EventArgs e) => { lblMnu05.BackColor = Color.LightBlue; };
            lblMnu05.MouseLeave += (Object sender, EventArgs e) => { lblMnu05.BackColor = Color.SteelBlue; };

            lblMnu06.Click += (Object sender, EventArgs e) => { flwSub06.Visible = !flwSub06.Visible; };
            lblMnu06.MouseHover += (Object sender, EventArgs e) => { lblMnu06.BackColor = Color.LightBlue; };
            lblMnu06.MouseLeave += (Object sender, EventArgs e) => { lblMnu06.BackColor = Color.SteelBlue; };

            lblMnu07.Click += (Object sender, EventArgs e) => { flwSub07.Visible = !flwSub07.Visible; };
            lblMnu07.MouseHover += (Object sender, EventArgs e) => { lblMnu07.BackColor = Color.LightBlue; };
            lblMnu07.MouseLeave += (Object sender, EventArgs e) => { lblMnu07.BackColor = Color.SteelBlue; };

            lblMnu08.Click += (Object sender, EventArgs e) => { flwSub08.Visible = !flwSub08.Visible; };
            lblMnu08.MouseHover += (Object sender, EventArgs e) => { lblMnu08.BackColor = Color.LightBlue; };
            lblMnu08.MouseLeave += (Object sender, EventArgs e) => { lblMnu08.BackColor = Color.SteelBlue; };


            lblMnu09.Click += (Object sender, EventArgs e) => { flwSub09.Visible = !flwSub09.Visible; };
            lblMnu09.MouseHover += (Object sender, EventArgs e) => { lblMnu09.BackColor = Color.LightBlue; };
            lblMnu09.MouseLeave += (Object sender, EventArgs e) => { lblMnu09.BackColor = Color.SteelBlue; };

            lblMnu10.Click += (Object sender, EventArgs e) => { flwSub10.Visible = !flwSub10.Visible; };
            lblMnu10.MouseHover += (Object sender, EventArgs e) => { lblMnu10.BackColor = Color.LightBlue; };
            lblMnu10.MouseLeave += (Object sender, EventArgs e) => { lblMnu10.BackColor = Color.SteelBlue; };

            lblMnu11.Click += (Object sender, EventArgs e) => { flwSub11.Visible = !flwSub11.Visible; };
            lblMnu11.MouseHover += (Object sender, EventArgs e) => { lblMnu11.BackColor = Color.LightBlue; };
            lblMnu11.MouseLeave += (Object sender, EventArgs e) => { lblMnu11.BackColor = Color.SteelBlue; };

            lblMnu12.Click += (Object sender, EventArgs e) => { flwSub12.Visible = !flwSub12.Visible; };
            lblMnu12.MouseHover += (Object sender, EventArgs e) => { lblMnu12.BackColor = Color.LightBlue; };
            lblMnu12.MouseLeave += (Object sender, EventArgs e) => { lblMnu12.BackColor = Color.SteelBlue; };

            lblMnu13.Click += (Object sender, EventArgs e) => { flwSub13.Visible = !flwSub13.Visible; };
            lblMnu13.MouseHover += (Object sender, EventArgs e) => { lblMnu13.BackColor = Color.LightBlue; };
            lblMnu13.MouseLeave += (Object sender, EventArgs e) => { lblMnu13.BackColor = Color.SteelBlue; };

            lblMnu14.Click += (Object sender, EventArgs e) => { flwSub14.Visible = !flwSub14.Visible; };
            lblMnu14.MouseHover += (Object sender, EventArgs e) => { lblMnu14.BackColor = Color.LightBlue; };
            lblMnu14.MouseLeave += (Object sender, EventArgs e) => { lblMnu14.BackColor = Color.SteelBlue; };

            lblMnu15.Click += (Object sender, EventArgs e) => { flwSub15.Visible = !flwSub15.Visible; };
            lblMnu15.MouseHover += (Object sender, EventArgs e) => { lblMnu15.BackColor = Color.LightBlue; };
            lblMnu15.MouseLeave += (Object sender, EventArgs e) => { lblMnu15.BackColor = Color.SteelBlue; };

            lblMnu16.Click += (Object sender, EventArgs e) => { flwSub16.Visible = !flwSub16.Visible; };
            lblMnu16.MouseHover += (Object sender, EventArgs e) => { lblMnu16.BackColor = Color.LightBlue; };
            lblMnu16.MouseLeave += (Object sender, EventArgs e) => { lblMnu16.BackColor = Color.SteelBlue; };











            // Estos son labels botones

            lblBtn01.Click += (Object sender, EventArgs e) => { PreviewLogs(); };
            lblBtn01.MouseHover += (Object sender, EventArgs e) => { lblBtn01.BackColor = Color.LightBlue; };
            lblBtn01.MouseLeave += (Object sender, EventArgs e) => { lblBtn01.BackColor = Color.SteelBlue; };

            lblBtn02.Click += (Object sender, EventArgs e) => { AcquireLogs(); };
            lblBtn02.MouseHover += (Object sender, EventArgs e) => { lblBtn02.BackColor = Color.LightBlue; };
            lblBtn02.MouseLeave += (Object sender, EventArgs e) => { lblBtn02.BackColor = Color.SteelBlue; };

            lblBtn03.Click += (Object sender, EventArgs e) => { ShowAllLogs(); };
            lblBtn03.MouseHover += (Object sender, EventArgs e) => { lblBtn03.BackColor = Color.LightBlue; };
            lblBtn03.MouseLeave += (Object sender, EventArgs e) => { lblBtn03.BackColor = Color.SteelBlue; };

            lblBtn04.Click += (Object sender, EventArgs e) => { AcquireLogs_Folder(); };
            lblBtn04.MouseHover += (Object sender, EventArgs e) => { lblBtn04.BackColor = Color.LightBlue; };
            lblBtn04.MouseLeave += (Object sender, EventArgs e) => { lblBtn04.BackColor = Color.SteelBlue; };



            //Acciones sobre los sub menus del primer menu


            lblSub0101.Click += (Object sender, EventArgs e) => { viewed_users(); };
            lblSub0101.MouseHover += (Object sender, EventArgs e) => { lblSub0101.BackColor = Color.LightBlue; };
            lblSub0101.MouseLeave += (Object sender, EventArgs e) => { lblSub0101.BackColor = Color.Transparent; };

            lblSub0102.Click += (Object sender, EventArgs e) => { LogOn_logOff(); };
            lblSub0102.MouseHover += (Object sender, EventArgs e) => { lblSub0102.BackColor = Color.LightBlue; };
            lblSub0102.MouseLeave += (Object sender, EventArgs e) => { lblSub0102.BackColor = Color.Transparent; };

            lblSub0103.Click += (Object sender, EventArgs e) => { FailTo_LogOn(); };
            lblSub0103.MouseHover += (Object sender, EventArgs e) => { lblSub0103.BackColor = Color.LightBlue; };
            lblSub0103.MouseLeave += (Object sender, EventArgs e) => { lblSub0103.BackColor = Color.Transparent; };

            lblSub0104.Click += (Object sender, EventArgs e) => { PowerOn_Off(); };
            lblSub0104.MouseHover += (Object sender, EventArgs e) => { lblSub0104.BackColor = Color.LightBlue; };
            lblSub0104.MouseLeave += (Object sender, EventArgs e) => { lblSub0104.BackColor = Color.Transparent; };

            lblSub0105.Click += (Object sender, EventArgs e) => { InstalledSoftware(); };
            lblSub0105.MouseHover += (Object sender, EventArgs e) => { lblSub0105.BackColor = Color.LightBlue; };
            lblSub0105.MouseLeave += (Object sender, EventArgs e) => { lblSub0105.BackColor = Color.Transparent; };

            lblSub0106.Click += (Object sender, EventArgs e) => { InstalledService(); };
            lblSub0106.MouseHover += (Object sender, EventArgs e) => { lblSub0106.BackColor = Color.LightBlue; };
            lblSub0106.MouseLeave += (Object sender, EventArgs e) => { lblSub0106.BackColor = Color.Transparent; };

            lblSub0107.Click += (Object sender, EventArgs e) => { VSC(); };
            lblSub0107.MouseHover += (Object sender, EventArgs e) => { lblSub0107.BackColor = Color.LightBlue; };
            lblSub0107.MouseLeave += (Object sender, EventArgs e) => { lblSub0107.BackColor = Color.Transparent; };

            lblSub0108.Click += (Object sender, EventArgs e) => { CreateLocalUser(); };
            lblSub0108.MouseHover += (Object sender, EventArgs e) => { lblSub0108.BackColor = Color.LightBlue; };
            lblSub0108.MouseLeave += (Object sender, EventArgs e) => { lblSub0108.BackColor = Color.Transparent; };

            lblSub0109.Click += (Object sender, EventArgs e) => { DeletedUser(); };
            lblSub0109.MouseHover += (Object sender, EventArgs e) => { lblSub0109.BackColor = Color.LightBlue; };
            lblSub0109.MouseLeave += (Object sender, EventArgs e) => { lblSub0109.BackColor = Color.Transparent; };

            lblSub0110.Click += (Object sender, EventArgs e) => { RenamedUser(); };
            lblSub0110.MouseHover += (Object sender, EventArgs e) => { lblSub0110.BackColor = Color.LightBlue; };
            lblSub0110.MouseLeave += (Object sender, EventArgs e) => { lblSub0110.BackColor = Color.Transparent; };


            lblSub0111.Click += (Object sender, EventArgs e) => { TerminalServerLogOn(); };
            lblSub0111.MouseHover += (Object sender, EventArgs e) => { lblSub0111.BackColor = Color.LightBlue; };
            lblSub0111.MouseLeave += (Object sender, EventArgs e) => { lblSub0111.BackColor = Color.Transparent; };

            /*lblSub0112.Click += (Object sender, EventArgs e) => { TerminalServerShellStart(); };
            lblSub0112.MouseHover += (Object sender, EventArgs e) => { lblSub0112.BackColor = Color.LightBlue; };
            lblSub0112.MouseLeave += (Object sender, EventArgs e) => { lblSub0112.BackColor = Color.Transparent; };*/

            lblSub0113.Click += (Object sender, EventArgs e) => { TerminalServerLogOf(); };
            lblSub0113.MouseHover += (Object sender, EventArgs e) => { lblSub0113.BackColor = Color.LightBlue; };
            lblSub0113.MouseLeave += (Object sender, EventArgs e) => { lblSub0113.BackColor = Color.Transparent; };

            /*lblSub0114.Click += (Object sender, EventArgs e) => { TerminalServerSessionDisconnected(); };
            lblSub0114.MouseHover += (Object sender, EventArgs e) => { lblSub0114.BackColor = Color.LightBlue; };
            lblSub0114.MouseLeave += (Object sender, EventArgs e) => { lblSub0114.BackColor = Color.Transparent; };

            lblSub0115.Click += (Object sender, EventArgs e) => { TerminalServerSessionReconnection(); };
            lblSub0115.MouseHover += (Object sender, EventArgs e) => { lblSub0115.BackColor = Color.LightBlue; };
            lblSub0115.MouseLeave += (Object sender, EventArgs e) => { lblSub0115.BackColor = Color.Transparent; };*/






            //Acciones sobre los soubmenu del segundo menu

            lblSub0201.Click += (Object sender, EventArgs e) => { CreateLocalUser(); };
            lblSub0201.MouseHover += (Object sender, EventArgs e) => { lblSub0201.BackColor = Color.LightBlue; };
            lblSub0201.MouseLeave += (Object sender, EventArgs e) => { lblSub0201.BackColor = Color.Transparent; };

            lblSub0202.Click += (Object sender, EventArgs e) => { UserWasEnabled(); };
            lblSub0202.MouseHover += (Object sender, EventArgs e) => { lblSub0202.BackColor = Color.LightBlue; };
            lblSub0202.MouseLeave += (Object sender, EventArgs e) => { lblSub0202.BackColor = Color.Transparent; };

            lblSub0203.Click += (Object sender, EventArgs e) => { UserChangePassword(); };
            lblSub0203.MouseHover += (Object sender, EventArgs e) => { lblSub0203.BackColor = Color.LightBlue; };
            lblSub0203.MouseLeave += (Object sender, EventArgs e) => { lblSub0203.BackColor = Color.Transparent; };

            lblSub0204.Click += (Object sender, EventArgs e) => { UserResetPassword(); };
            lblSub0204.MouseHover += (Object sender, EventArgs e) => { lblSub0204.BackColor = Color.LightBlue; };
            lblSub0204.MouseLeave += (Object sender, EventArgs e) => { lblSub0204.BackColor = Color.Transparent; };

            lblSub0205.Click += (Object sender, EventArgs e) => { UserAccountDisabled(); };
            lblSub0205.MouseHover += (Object sender, EventArgs e) => { lblSub0205.BackColor = Color.LightBlue; };
            lblSub0205.MouseLeave += (Object sender, EventArgs e) => { lblSub0205.BackColor = Color.Transparent; };

            lblSub0206.Click += (Object sender, EventArgs e) => { UserAcountDeleted(); };
            lblSub0206.MouseHover += (Object sender, EventArgs e) => { lblSub0206.BackColor = Color.LightBlue; };
            lblSub0206.MouseLeave += (Object sender, EventArgs e) => { lblSub0206.BackColor = Color.Transparent; };

            lblSub0207.Click += (Object sender, EventArgs e) => { UserAcountChanged(); };
            lblSub0207.MouseHover += (Object sender, EventArgs e) => { lblSub0207.BackColor = Color.LightBlue; };
            lblSub0207.MouseLeave += (Object sender, EventArgs e) => { lblSub0207.BackColor = Color.Transparent; };

            // Acciones sobre el tercer menu

            lblSub0301.Click += (Object sender, EventArgs e) => { GblGroupWasCreated(); };
            lblSub0301.MouseHover += (Object sender, EventArgs e) => { lblSub0301.BackColor = Color.LightBlue; };
            lblSub0301.MouseLeave += (Object sender, EventArgs e) => { lblSub0301.BackColor = Color.Transparent; };

            lblSub0302.Click += (Object sender, EventArgs e) => { GblGroupWasDeleted(); };
            lblSub0302.MouseHover += (Object sender, EventArgs e) => { lblSub0302.BackColor = Color.LightBlue; };
            lblSub0302.MouseLeave += (Object sender, EventArgs e) => { lblSub0302.BackColor = Color.Transparent; };

            lblSub0303.Click += (Object sender, EventArgs e) => { GblGroupWasChanged(); };
            lblSub0303.MouseHover += (Object sender, EventArgs e) => { lblSub0303.BackColor = Color.LightBlue; };
            lblSub0303.MouseLeave += (Object sender, EventArgs e) => { lblSub0303.BackColor = Color.Transparent; };

            lblSub0304.Click += (Object sender, EventArgs e) => { GblGroupAMemberWasAdded(); };
            lblSub0304.MouseHover += (Object sender, EventArgs e) => { lblSub0304.BackColor = Color.LightBlue; };
            lblSub0304.MouseLeave += (Object sender, EventArgs e) => { lblSub0304.BackColor = Color.Transparent; };

            lblSub0305.Click += (Object sender, EventArgs e) => { GblGroupAMemberWasRemoved(); };
            lblSub0305.MouseHover += (Object sender, EventArgs e) => { lblSub0305.BackColor = Color.LightBlue; };
            lblSub0305.MouseLeave += (Object sender, EventArgs e) => { lblSub0305.BackColor = Color.Transparent; };


            //Acciones sobre cuarto menu


            lblSub0401.Click += (Object sender, EventArgs e) => { LocalGroupWasCreated(); };
            lblSub0401.MouseHover += (Object sender, EventArgs e) => { lblSub0401.BackColor = Color.LightBlue; };
            lblSub0401.MouseLeave += (Object sender, EventArgs e) => { lblSub0401.BackColor = Color.Transparent; };

            lblSub0402.Click += (Object sender, EventArgs e) => { LocalGroupWasDeleted(); };
            lblSub0402.MouseHover += (Object sender, EventArgs e) => { lblSub0402.BackColor = Color.LightBlue; };
            lblSub0402.MouseLeave += (Object sender, EventArgs e) => { lblSub0402.BackColor = Color.Transparent; };

            lblSub0403.Click += (Object sender, EventArgs e) => { LocalGroupWasChanged(); };
            lblSub0403.MouseHover += (Object sender, EventArgs e) => { lblSub0403.BackColor = Color.LightBlue; };
            lblSub0403.MouseLeave += (Object sender, EventArgs e) => { lblSub0403.BackColor = Color.Transparent; };

            lblSub0404.Click += (Object sender, EventArgs e) => { LocalAMemberWasAdded(); };
            lblSub0404.MouseHover += (Object sender, EventArgs e) => { lblSub0404.BackColor = Color.LightBlue; };
            lblSub0404.MouseLeave += (Object sender, EventArgs e) => { lblSub0404.BackColor = Color.Transparent; };

            lblSub0405.Click += (Object sender, EventArgs e) => { LocalAMemberWasRemoved(); };
            lblSub0405.MouseHover += (Object sender, EventArgs e) => { lblSub0405.BackColor = Color.LightBlue; };
            lblSub0405.MouseLeave += (Object sender, EventArgs e) => { lblSub0405.BackColor = Color.Transparent; };

            lblSub0406.Click += (Object sender, EventArgs e) => { LocalGroupWasEnumerated(); };
            lblSub0406.MouseHover += (Object sender, EventArgs e) => { lblSub0406.BackColor = Color.LightBlue; };
            lblSub0406.MouseLeave += (Object sender, EventArgs e) => { lblSub0406.BackColor = Color.Transparent; };

            //Acciones sobre quinto menu

            lblSub0501.Click += (Object sender, EventArgs e) => { UnivGroupWasCreated(); };
            lblSub0501.MouseHover += (Object sender, EventArgs e) => { lblSub0501.BackColor = Color.LightBlue; };
            lblSub0501.MouseLeave += (Object sender, EventArgs e) => { lblSub0501.BackColor = Color.Transparent; };

            lblSub0502.Click += (Object sender, EventArgs e) => { UnivGroupWasDeleted(); };
            lblSub0502.MouseHover += (Object sender, EventArgs e) => { lblSub0502.BackColor = Color.LightBlue; };
            lblSub0502.MouseLeave += (Object sender, EventArgs e) => { lblSub0502.BackColor = Color.Transparent; };

            lblSub0503.Click += (Object sender, EventArgs e) => { UnivGroupWasChanged(); };
            lblSub0503.MouseHover += (Object sender, EventArgs e) => { lblSub0503.BackColor = Color.LightBlue; };
            lblSub0503.MouseLeave += (Object sender, EventArgs e) => { lblSub0503.BackColor = Color.Transparent; };

            lblSub0504.Click += (Object sender, EventArgs e) => { UnivGroupAMemberWasAdded(); };
            lblSub0504.MouseHover += (Object sender, EventArgs e) => { lblSub0504.BackColor = Color.LightBlue; };
            lblSub0504.MouseLeave += (Object sender, EventArgs e) => { lblSub0504.BackColor = Color.Transparent; };

            lblSub0505.Click += (Object sender, EventArgs e) => { UnivGroupAMemberWasRemoved(); };
            lblSub0505.MouseHover += (Object sender, EventArgs e) => { lblSub0505.BackColor = Color.LightBlue; };
            lblSub0505.MouseLeave += (Object sender, EventArgs e) => { lblSub0505.BackColor = Color.Transparent; };


            //Acciones sobre sexto menu

            lblSub0601.Click += (Object sender, EventArgs e) => { ComputerAccountWasCreated(); };
            lblSub0601.MouseHover += (Object sender, EventArgs e) => { lblSub0601.BackColor = Color.LightBlue; };
            lblSub0601.MouseLeave += (Object sender, EventArgs e) => { lblSub0601.BackColor = Color.Transparent; };

            lblSub0602.Click += (Object sender, EventArgs e) => { ComputerAccountWasDeleted(); };
            lblSub0602.MouseHover += (Object sender, EventArgs e) => { lblSub0602.BackColor = Color.LightBlue; };
            lblSub0602.MouseLeave += (Object sender, EventArgs e) => { lblSub0602.BackColor = Color.Transparent; };

            lblSub0603.Click += (Object sender, EventArgs e) => { ComputerAccountWasChanged(); };
            lblSub0603.MouseHover += (Object sender, EventArgs e) => { lblSub0603.BackColor = Color.LightBlue; };
            lblSub0603.MouseLeave += (Object sender, EventArgs e) => { lblSub0603.BackColor = Color.Transparent; };



            //Acciones sobre septimo menu

            lblSub0701.Click += (Object sender, EventArgs e) => { NetworkShareAccessed(); };
            lblSub0701.MouseHover += (Object sender, EventArgs e) => { lblSub0701.BackColor = Color.LightBlue; };
            lblSub0701.MouseLeave += (Object sender, EventArgs e) => { lblSub0701.BackColor = Color.Transparent; };

            lblSub0702.Click += (Object sender, EventArgs e) => { NetworkShareAdded(); };
            lblSub0702.MouseHover += (Object sender, EventArgs e) => { lblSub0702.BackColor = Color.LightBlue; };
            lblSub0702.MouseLeave += (Object sender, EventArgs e) => { lblSub0702.BackColor = Color.Transparent; };

            lblSub0703.Click += (Object sender, EventArgs e) => { NetworkShareModified(); };
            lblSub0703.MouseHover += (Object sender, EventArgs e) => { lblSub0703.BackColor = Color.LightBlue; };
            lblSub0703.MouseLeave += (Object sender, EventArgs e) => { lblSub0703.BackColor = Color.Transparent; };

            lblSub0704.Click += (Object sender, EventArgs e) => { NetworkShareDeleted(); };
            lblSub0704.MouseHover += (Object sender, EventArgs e) => { lblSub0704.BackColor = Color.LightBlue; };
            lblSub0704.MouseLeave += (Object sender, EventArgs e) => { lblSub0704.BackColor = Color.Transparent; };

            lblSub0705.Click += (Object sender, EventArgs e) => { NetworkShareChecked(); };
            lblSub0705.MouseHover += (Object sender, EventArgs e) => { lblSub0705.BackColor = Color.LightBlue; };
            lblSub0705.MouseLeave += (Object sender, EventArgs e) => { lblSub0705.BackColor = Color.Transparent; };

            //Acciones sobre octavo menu

            lblSub0801.Click += (Object sender, EventArgs e) => { ScheduledTaskCreated(); };
            lblSub0801.MouseHover += (Object sender, EventArgs e) => { lblSub0801.BackColor = Color.LightBlue; };
            lblSub0801.MouseLeave += (Object sender, EventArgs e) => { lblSub0801.BackColor = Color.Transparent; };

            lblSub0802.Click += (Object sender, EventArgs e) => { ScheduledTaskUpdated(); };
            lblSub0802.MouseHover += (Object sender, EventArgs e) => { lblSub0802.BackColor = Color.LightBlue; };
            lblSub0802.MouseLeave += (Object sender, EventArgs e) => { lblSub0802.BackColor = Color.Transparent; };

            lblSub0803.Click += (Object sender, EventArgs e) => { ScheduledTaskDeleted(); };
            lblSub0803.MouseHover += (Object sender, EventArgs e) => { lblSub0803.BackColor = Color.LightBlue; };
            lblSub0803.MouseLeave += (Object sender, EventArgs e) => { lblSub0803.BackColor = Color.Transparent; };

            lblSub0804.Click += (Object sender, EventArgs e) => { ScheduledTaskExecuted(); };
            lblSub0804.MouseHover += (Object sender, EventArgs e) => { lblSub0804.BackColor = Color.LightBlue; };
            lblSub0804.MouseLeave += (Object sender, EventArgs e) => { lblSub0804.BackColor = Color.Transparent; };

            lblSub0805.Click += (Object sender, EventArgs e) => { ScheduledTaskCompleted(); };
            lblSub0805.MouseHover += (Object sender, EventArgs e) => { lblSub0805.BackColor = Color.LightBlue; };
            lblSub0805.MouseLeave += (Object sender, EventArgs e) => { lblSub0805.BackColor = Color.Transparent; };



            //Acciones sobre noveno menu

            lblSub0901.Click += (Object sender, EventArgs e) => { ObjScheduledTaskCreated(); };
            lblSub0901.MouseHover += (Object sender, EventArgs e) => { lblSub0901.BackColor = Color.LightBlue; };
            lblSub0901.MouseLeave += (Object sender, EventArgs e) => { lblSub0901.BackColor = Color.Transparent; };

            lblSub0902.Click += (Object sender, EventArgs e) => { ObjScheduledTaskDeleted(); };
            lblSub0902.MouseHover += (Object sender, EventArgs e) => { lblSub0902.BackColor = Color.LightBlue; };
            lblSub0902.MouseLeave += (Object sender, EventArgs e) => { lblSub0902.BackColor = Color.Transparent; };

            lblSub0903.Click += (Object sender, EventArgs e) => { ObjScheduledTaskEnabled(); };
            lblSub0903.MouseHover += (Object sender, EventArgs e) => { lblSub0903.BackColor = Color.LightBlue; };
            lblSub0903.MouseLeave += (Object sender, EventArgs e) => { lblSub0903.BackColor = Color.Transparent; };

            lblSub0904.Click += (Object sender, EventArgs e) => { ObjScheduledTaskDisabled(); };
            lblSub0904.MouseHover += (Object sender, EventArgs e) => { lblSub0904.BackColor = Color.LightBlue; };
            lblSub0904.MouseLeave += (Object sender, EventArgs e) => { lblSub0904.BackColor = Color.Transparent; };

            lblSub0905.Click += (Object sender, EventArgs e) => { ObjScheduledTaskUpdated(); };
            lblSub0905.MouseHover += (Object sender, EventArgs e) => { lblSub0905.BackColor = Color.LightBlue; };
            lblSub0905.MouseLeave += (Object sender, EventArgs e) => { lblSub0905.BackColor = Color.Transparent; };

            //Acciones sobre decimo menu

            lblSub1001.Click += (Object sender, EventArgs e) => { HandletoAnObjectRequested(); };
            lblSub1001.MouseHover += (Object sender, EventArgs e) => { lblSub1001.BackColor = Color.LightBlue; };
            lblSub1001.MouseLeave += (Object sender, EventArgs e) => { lblSub1001.BackColor = Color.Transparent; };

            lblSub1002.Click += (Object sender, EventArgs e) => { RegistryValueModified(); };
            lblSub1002.MouseHover += (Object sender, EventArgs e) => { lblSub1002.BackColor = Color.LightBlue; };
            lblSub1002.MouseLeave += (Object sender, EventArgs e) => { lblSub1002.BackColor = Color.Transparent; };

            lblSub1003.Click += (Object sender, EventArgs e) => { HandletoAnObjectClosed(); };
            lblSub1003.MouseHover += (Object sender, EventArgs e) => { lblSub1003.BackColor = Color.LightBlue; };
            lblSub1003.MouseLeave += (Object sender, EventArgs e) => { lblSub1003.BackColor = Color.Transparent; };

            lblSub1004.Click += (Object sender, EventArgs e) => { ObjectWasDeleted(); };
            lblSub1004.MouseHover += (Object sender, EventArgs e) => { lblSub1004.BackColor = Color.LightBlue; };
            lblSub1004.MouseLeave += (Object sender, EventArgs e) => { lblSub1004.BackColor = Color.Transparent; };

            lblSub1005.Click += (Object sender, EventArgs e) => { AttemptMadetoAccObject(); };
            lblSub1005.MouseHover += (Object sender, EventArgs e) => { lblSub1005.BackColor = Color.LightBlue; };
            lblSub1005.MouseLeave += (Object sender, EventArgs e) => { lblSub1005.BackColor = Color.Transparent; };



            //Acciones sobre once menu


            lblSub1101.Click += (Object sender, EventArgs e) => { EventLogServiceStarted(); };
            lblSub1101.MouseHover += (Object sender, EventArgs e) => { lblSub1101.BackColor = Color.LightBlue; };
            lblSub1101.MouseLeave += (Object sender, EventArgs e) => { lblSub1101.BackColor = Color.Transparent; };

            lblSub1102.Click += (Object sender, EventArgs e) => { EventLogServiceStoped(); };
            lblSub1102.MouseHover += (Object sender, EventArgs e) => { lblSub1102.BackColor = Color.LightBlue; };
            lblSub1102.MouseLeave += (Object sender, EventArgs e) => { lblSub1102.BackColor = Color.Transparent; };

            lblSub1103.Click += (Object sender, EventArgs e) => { ServiceTerminatedUnexpectedly(); };
            lblSub1103.MouseHover += (Object sender, EventArgs e) => { lblSub1103.BackColor = Color.LightBlue; };
            lblSub1103.MouseLeave += (Object sender, EventArgs e) => { lblSub1103.BackColor = Color.Transparent; };

            lblSub1104.Click += (Object sender, EventArgs e) => { ServiceStartStop(); };
            lblSub1104.MouseHover += (Object sender, EventArgs e) => { lblSub1104.BackColor = Color.LightBlue; };
            lblSub1104.MouseLeave += (Object sender, EventArgs e) => { lblSub1104.BackColor = Color.Transparent; };

            lblSub1105.Click += (Object sender, EventArgs e) => { StartTypeForServiceChanged(); };
            lblSub1105.MouseHover += (Object sender, EventArgs e) => { lblSub1105.BackColor = Color.LightBlue; };
            lblSub1105.MouseLeave += (Object sender, EventArgs e) => { lblSub1105.BackColor = Color.Transparent; };

            lblSub1106.Click += (Object sender, EventArgs e) => { ServiceInstalledbySystem(); };
            lblSub1106.MouseHover += (Object sender, EventArgs e) => { lblSub1106.BackColor = Color.LightBlue; };
            lblSub1106.MouseLeave += (Object sender, EventArgs e) => { lblSub1106.BackColor = Color.Transparent; };


            //Acciones sobre doce menu


            lblSub1201.Click += (Object sender, EventArgs e) => { WlanSuccessfullyConnected(); };
            lblSub1201.MouseHover += (Object sender, EventArgs e) => { lblSub1201.BackColor = Color.LightBlue; };
            lblSub1201.MouseLeave += (Object sender, EventArgs e) => { lblSub1201.BackColor = Color.Transparent; };

            lblSub1202.Click += (Object sender, EventArgs e) => { WlanFailedToConnect(); };
            lblSub1202.MouseHover += (Object sender, EventArgs e) => { lblSub1202.BackColor = Color.LightBlue; };
            lblSub1202.MouseLeave += (Object sender, EventArgs e) => { lblSub1202.BackColor = Color.Transparent; };


            //Acciones sobre trece menu


            lblSub1301.Click += (Object sender, EventArgs e) => { BlockAppAcceptIncomConn(); };
            lblSub1301.MouseHover += (Object sender, EventArgs e) => { lblSub1301.BackColor = Color.LightBlue; };
            lblSub1301.MouseLeave += (Object sender, EventArgs e) => { lblSub1301.BackColor = Color.Transparent; };

            lblSub1302.Click += (Object sender, EventArgs e) => { BlokedaPacket(); };
            lblSub1302.MouseHover += (Object sender, EventArgs e) => { lblSub1302.BackColor = Color.LightBlue; };
            lblSub1302.MouseLeave += (Object sender, EventArgs e) => { lblSub1302.BackColor = Color.Transparent; };

            lblSub1303.Click += (Object sender, EventArgs e) => { PermitedIncommigConn(); };
            lblSub1303.MouseHover += (Object sender, EventArgs e) => { lblSub1303.BackColor = Color.LightBlue; };
            lblSub1303.MouseLeave += (Object sender, EventArgs e) => { lblSub1303.BackColor = Color.Transparent; };

            lblSub1304.Click += (Object sender, EventArgs e) => { AllowedConnection(); };
            lblSub1304.MouseHover += (Object sender, EventArgs e) => { lblSub1304.BackColor = Color.LightBlue; };
            lblSub1304.MouseLeave += (Object sender, EventArgs e) => { lblSub1304.BackColor = Color.Transparent; };

            lblSub1305.Click += (Object sender, EventArgs e) => { BlockedConnection(); };
            lblSub1305.MouseHover += (Object sender, EventArgs e) => { lblSub1305.BackColor = Color.LightBlue; };
            lblSub1305.MouseLeave += (Object sender, EventArgs e) => { lblSub1305.BackColor = Color.Transparent; };

            lblSub1306.Click += (Object sender, EventArgs e) => { PermitedBindaLocalPort(); };
            lblSub1306.MouseHover += (Object sender, EventArgs e) => { lblSub1306.BackColor = Color.LightBlue; };
            lblSub1306.MouseLeave += (Object sender, EventArgs e) => { lblSub1306.BackColor = Color.Transparent; };

            lblSub1307.Click += (Object sender, EventArgs e) => { BlockBindaLocalPort(); };
            lblSub1307.MouseHover += (Object sender, EventArgs e) => { lblSub1307.BackColor = Color.LightBlue; };
            lblSub1307.MouseLeave += (Object sender, EventArgs e) => { lblSub1307.BackColor = Color.Transparent; };



            //Acciones sobre catorce menu


            lblSub1401.Click += (Object sender, EventArgs e) => { FoundMalware(); };
            lblSub1401.MouseHover += (Object sender, EventArgs e) => { lblSub1401.BackColor = Color.LightBlue; };
            lblSub1401.MouseLeave += (Object sender, EventArgs e) => { lblSub1401.BackColor = Color.Transparent; };

            lblSub1402.Click += (Object sender, EventArgs e) => { ActionProtectSystem(); };
            lblSub1402.MouseHover += (Object sender, EventArgs e) => { lblSub1402.BackColor = Color.LightBlue; };
            lblSub1402.MouseLeave += (Object sender, EventArgs e) => { lblSub1402.BackColor = Color.Transparent; };

            lblSub1403.Click += (Object sender, EventArgs e) => { FailedActionProtectSystem(); };
            lblSub1403.MouseHover += (Object sender, EventArgs e) => { lblSub1403.BackColor = Color.LightBlue; };
            lblSub1403.MouseLeave += (Object sender, EventArgs e) => { lblSub1403.BackColor = Color.Transparent; };

            lblSub1404.Click += (Object sender, EventArgs e) => { DeletedHistoryMalware(); };
            lblSub1404.MouseHover += (Object sender, EventArgs e) => { lblSub1404.BackColor = Color.LightBlue; };
            lblSub1404.MouseLeave += (Object sender, EventArgs e) => { lblSub1404.BackColor = Color.Transparent; };

            lblSub1405.Click += (Object sender, EventArgs e) => { DetectedSuspiciousBehavior(); };
            lblSub1405.MouseHover += (Object sender, EventArgs e) => { lblSub1405.BackColor = Color.LightBlue; };
            lblSub1405.MouseLeave += (Object sender, EventArgs e) => { lblSub1405.BackColor = Color.Transparent; };

            lblSub1406.Click += (Object sender, EventArgs e) => { DetectedMalware(); };
            lblSub1406.MouseHover += (Object sender, EventArgs e) => { lblSub1406.BackColor = Color.LightBlue; };
            lblSub1406.MouseLeave += (Object sender, EventArgs e) => { lblSub1406.BackColor = Color.Transparent; };

            lblSub1407.Click += (Object sender, EventArgs e) => { CriticalErrorInActionOnMalware(); };
            lblSub1407.MouseHover += (Object sender, EventArgs e) => { lblSub1407.BackColor = Color.LightBlue; };
            lblSub1407.MouseLeave += (Object sender, EventArgs e) => { lblSub1407.BackColor = Color.Transparent; };

            lblSub1408.Click += (Object sender, EventArgs e) => { RealTimeProtectionDisabled(); };
            lblSub1408.MouseHover += (Object sender, EventArgs e) => { lblSub1408.BackColor = Color.LightBlue; };
            lblSub1408.MouseLeave += (Object sender, EventArgs e) => { lblSub1408.BackColor = Color.Transparent; };

            lblSub1409.Click += (Object sender, EventArgs e) => { RealTimeProtectionCfgChanged(); };
            lblSub1409.MouseHover += (Object sender, EventArgs e) => { lblSub1409.BackColor = Color.LightBlue; };
            lblSub1409.MouseLeave += (Object sender, EventArgs e) => { lblSub1409.BackColor = Color.Transparent; };

            lblSub1410.Click += (Object sender, EventArgs e) => { PlatformCfgChanged(); };
            lblSub1410.MouseHover += (Object sender, EventArgs e) => { lblSub1410.BackColor = Color.LightBlue; };
            lblSub1410.MouseLeave += (Object sender, EventArgs e) => { lblSub1410.BackColor = Color.Transparent; };

            lblSub1411.Click += (Object sender, EventArgs e) => { ScanningMalwareDisabled(); };
            lblSub1411.MouseHover += (Object sender, EventArgs e) => { lblSub1411.BackColor = Color.LightBlue; };
            lblSub1411.MouseLeave += (Object sender, EventArgs e) => { lblSub1411.BackColor = Color.Transparent; };

            lblSub1412.Click += (Object sender, EventArgs e) => { ScanningVirusDisabled(); };
            lblSub1412.MouseHover += (Object sender, EventArgs e) => { lblSub1412.BackColor = Color.LightBlue; };
            lblSub1412.MouseLeave += (Object sender, EventArgs e) => { lblSub1412.BackColor = Color.Transparent; };




            //Acciones sobre quince menu


            lblSub1501.Click += (Object sender, EventArgs e) => { ProcessCreation(); };
            lblSub1501.MouseHover += (Object sender, EventArgs e) => { lblSub1501.BackColor = Color.LightBlue; };
            lblSub1501.MouseLeave += (Object sender, EventArgs e) => { lblSub1501.BackColor = Color.Transparent; };

            lblSub1502.Click += (Object sender, EventArgs e) => { ProcChngFileCreationTime(); };
            lblSub1502.MouseHover += (Object sender, EventArgs e) => { lblSub1502.BackColor = Color.LightBlue; };
            lblSub1502.MouseLeave += (Object sender, EventArgs e) => { lblSub1502.BackColor = Color.Transparent; };

            lblSub1503.Click += (Object sender, EventArgs e) => { NetworkConnection(); };
            lblSub1503.MouseHover += (Object sender, EventArgs e) => { lblSub1503.BackColor = Color.LightBlue; };
            lblSub1503.MouseLeave += (Object sender, EventArgs e) => { lblSub1503.BackColor = Color.Transparent; };

            lblSub1504.Click += (Object sender, EventArgs e) => { SysmonServiceStateChanged(); };
            lblSub1504.MouseHover += (Object sender, EventArgs e) => { lblSub1504.BackColor = Color.LightBlue; };
            lblSub1504.MouseLeave += (Object sender, EventArgs e) => { lblSub1504.BackColor = Color.Transparent; };

            lblSub1505.Click += (Object sender, EventArgs e) => { ProcessTerminated(); };
            lblSub1505.MouseHover += (Object sender, EventArgs e) => { lblSub1505.BackColor = Color.LightBlue; };
            lblSub1505.MouseLeave += (Object sender, EventArgs e) => { lblSub1505.BackColor = Color.Transparent; };

            lblSub1506.Click += (Object sender, EventArgs e) => { DriverLoaded(); };
            lblSub1506.MouseHover += (Object sender, EventArgs e) => { lblSub1506.BackColor = Color.LightBlue; };
            lblSub1506.MouseLeave += (Object sender, EventArgs e) => { lblSub1506.BackColor = Color.Transparent; };

            lblSub1507.Click += (Object sender, EventArgs e) => { ImageLoaded(); };
            lblSub1507.MouseHover += (Object sender, EventArgs e) => { lblSub1507.BackColor = Color.LightBlue; };
            lblSub1507.MouseLeave += (Object sender, EventArgs e) => { lblSub1507.BackColor = Color.Transparent; };

            lblSub1508.Click += (Object sender, EventArgs e) => { CreateRemoteThread(); };
            lblSub1508.MouseHover += (Object sender, EventArgs e) => { lblSub1508.BackColor = Color.LightBlue; };
            lblSub1508.MouseLeave += (Object sender, EventArgs e) => { lblSub1508.BackColor = Color.Transparent; };

            lblSub1509.Click += (Object sender, EventArgs e) => { RawAccessRead(); };
            lblSub1509.MouseHover += (Object sender, EventArgs e) => { lblSub1509.BackColor = Color.LightBlue; };
            lblSub1509.MouseLeave += (Object sender, EventArgs e) => { lblSub1509.BackColor = Color.Transparent; };

            lblSub1510.Click += (Object sender, EventArgs e) => { ProcessAccess(); };
            lblSub1510.MouseHover += (Object sender, EventArgs e) => { lblSub1510.BackColor = Color.LightBlue; };
            lblSub1510.MouseLeave += (Object sender, EventArgs e) => { lblSub1510.BackColor = Color.Transparent; };

            lblSub1511.Click += (Object sender, EventArgs e) => { FileCreate(); };
            lblSub1511.MouseHover += (Object sender, EventArgs e) => { lblSub1511.BackColor = Color.LightBlue; };
            lblSub1511.MouseLeave += (Object sender, EventArgs e) => { lblSub1511.BackColor = Color.Transparent; };

            lblSub1512.Click += (Object sender, EventArgs e) => { RegKeyValueCreatedDeleted(); };
            lblSub1512.MouseHover += (Object sender, EventArgs e) => { lblSub1512.BackColor = Color.LightBlue; };
            lblSub1512.MouseLeave += (Object sender, EventArgs e) => { lblSub1512.BackColor = Color.Transparent; };

            lblSub1513.Click += (Object sender, EventArgs e) => { RegistryValueModification(); };
            lblSub1513.MouseHover += (Object sender, EventArgs e) => { lblSub1513.BackColor = Color.LightBlue; };
            lblSub1513.MouseLeave += (Object sender, EventArgs e) => { lblSub1513.BackColor = Color.Transparent; };

            lblSub1514.Click += (Object sender, EventArgs e) => { RegistryKeyValueRenamed(); };
            lblSub1514.MouseHover += (Object sender, EventArgs e) => { lblSub1514.BackColor = Color.LightBlue; };
            lblSub1514.MouseLeave += (Object sender, EventArgs e) => { lblSub1514.BackColor = Color.Transparent; };

            lblSub1515.Click += (Object sender, EventArgs e) => { FileCreateStreamHash(); };
            lblSub1515.MouseHover += (Object sender, EventArgs e) => { lblSub1515.BackColor = Color.LightBlue; };
            lblSub1515.MouseLeave += (Object sender, EventArgs e) => { lblSub1515.BackColor = Color.Transparent; };

            lblSub1516.Click += (Object sender, EventArgs e) => { SysmonConfigurationChange(); };
            lblSub1516.MouseHover += (Object sender, EventArgs e) => { lblSub1516.BackColor = Color.LightBlue; };
            lblSub1516.MouseLeave += (Object sender, EventArgs e) => { lblSub1516.BackColor = Color.Transparent; };

            lblSub1517.Click += (Object sender, EventArgs e) => { NamedPipeCreated(); };
            lblSub1517.MouseHover += (Object sender, EventArgs e) => { lblSub1517.BackColor = Color.LightBlue; };
            lblSub1517.MouseLeave += (Object sender, EventArgs e) => { lblSub1517.BackColor = Color.Transparent; };

            lblSub1518.Click += (Object sender, EventArgs e) => { NamedPipeConnected(); };
            lblSub1518.MouseHover += (Object sender, EventArgs e) => { lblSub1518.BackColor = Color.LightBlue; };
            lblSub1518.MouseLeave += (Object sender, EventArgs e) => { lblSub1518.BackColor = Color.Transparent; };

            lblSub1519.Click += (Object sender, EventArgs e) => { WMIEventFilterActivityDetected(); };
            lblSub1519.MouseHover += (Object sender, EventArgs e) => { lblSub1519.BackColor = Color.LightBlue; };
            lblSub1519.MouseLeave += (Object sender, EventArgs e) => { lblSub1519.BackColor = Color.Transparent; };

            lblSub1520.Click += (Object sender, EventArgs e) => { WMIEventConsumerActivityDetected(); };
            lblSub1520.MouseHover += (Object sender, EventArgs e) => { lblSub1520.BackColor = Color.LightBlue; };
            lblSub1520.MouseLeave += (Object sender, EventArgs e) => { lblSub1520.BackColor = Color.Transparent; };


            lblSub1521.Click += (Object sender, EventArgs e) => { WMIEventConsumerToFilterActivityDetected(); };
            lblSub1521.MouseHover += (Object sender, EventArgs e) => { lblSub1521.BackColor = Color.LightBlue; };
            lblSub1521.MouseLeave += (Object sender, EventArgs e) => { lblSub1521.BackColor = Color.Transparent; };

            lblSub1522.Click += (Object sender, EventArgs e) => { DNSQueryEvent(); };
            lblSub1522.MouseHover += (Object sender, EventArgs e) => { lblSub1522.BackColor = Color.LightBlue; };
            lblSub1522.MouseLeave += (Object sender, EventArgs e) => { lblSub1522.BackColor = Color.Transparent; };

            lblSub1523.Click += (Object sender, EventArgs e) => { SysmonError(); };
            lblSub1523.MouseHover += (Object sender, EventArgs e) => { lblSub1523.BackColor = Color.LightBlue; };
            lblSub1523.MouseLeave += (Object sender, EventArgs e) => { lblSub1523.BackColor = Color.Transparent; };


            //Acciones sobre diez y seis


            lblSub1601.Click += (Object sender, EventArgs e) => { TerminalServerLogOn(); };
            lblSub1601.MouseHover += (Object sender, EventArgs e) => { lblSub1601.BackColor = Color.LightBlue; };
            lblSub1601.MouseLeave += (Object sender, EventArgs e) => { lblSub1601.BackColor = Color.Transparent; };

            lblSub1602.Click += (Object sender, EventArgs e) => { TerminalServerShellStart(); };
            lblSub1602.MouseHover += (Object sender, EventArgs e) => { lblSub1602.BackColor = Color.LightBlue; };
            lblSub1602.MouseLeave += (Object sender, EventArgs e) => { lblSub1602.BackColor = Color.Transparent; };

            lblSub1603.Click += (Object sender, EventArgs e) => { TerminalServerLogOf(); };
            lblSub1603.MouseHover += (Object sender, EventArgs e) => { lblSub1603.BackColor = Color.LightBlue; };
            lblSub1603.MouseLeave += (Object sender, EventArgs e) => { lblSub1603.BackColor = Color.Transparent; };

            lblSub1604.Click += (Object sender, EventArgs e) => { TerminalServerSessionDisconnected(); };
            lblSub1604.MouseHover += (Object sender, EventArgs e) => { lblSub1604.BackColor = Color.LightBlue; };
            lblSub1604.MouseLeave += (Object sender, EventArgs e) => { lblSub1604.BackColor = Color.Transparent; };

            lblSub1605.Click += (Object sender, EventArgs e) => { TerminalServerSessionReconnection(); };
            lblSub1605.MouseHover += (Object sender, EventArgs e) => { lblSub1605.BackColor = Color.LightBlue; };
            lblSub1605.MouseLeave += (Object sender, EventArgs e) => { lblSub1605.BackColor = Color.Transparent; };

            lblSub1606.Click += (Object sender, EventArgs e) => { TerminalServerSessionDisconnectedbySession(); };
            lblSub1606.MouseHover += (Object sender, EventArgs e) => { lblSub1606.BackColor = Color.LightBlue; };
            lblSub1606.MouseLeave += (Object sender, EventArgs e) => { lblSub1606.BackColor = Color.Transparent; };

            lblSub1607.Click += (Object sender, EventArgs e) => { TerminalServerSessionDisconnectedCode(); };
            lblSub1607.MouseHover += (Object sender, EventArgs e) => { lblSub1607.BackColor = Color.LightBlue; };
            lblSub1607.MouseLeave += (Object sender, EventArgs e) => { lblSub1607.BackColor = Color.Transparent; };


        }



        private void LogToConsole(string message)
        {
            string timestampedMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {message}";
            //string timestampedMessage = $"{DateTime.Now:HH:mm:ss} {message}";

            if (textBox_logConsole.InvokeRequired)
            {
                textBox_logConsole.Invoke(new Action(() => LogToConsole(message)));
            }
            else
            {

                textBox_logConsole.AppendText(timestampedMessage + Environment.NewLine);
                textBox_logConsole.SelectionStart = textBox_logConsole.Text.Length;
                textBox_logConsole.ScrollToCaret();
                //label_status.Text = message;
                WriteToLogFile(timestampedMessage);
            }

        }

        private object lockObject = new object();

        private void WriteToLogFile(string message)
        {
            string logFilePath = $"{dbname}.log";

            try
            {
                lock (lockObject)
                {
                    using (StreamWriter sw = new StreamWriter(logFilePath, true))
                    {
                        sw.WriteLine(message);
                        sw.Flush(); // Forzar la escritura del buffer al disco
                    }
                    Thread.Sleep(50);
                }
            }
            catch (IOException ex) // Manejar específicamente IOException por bloqueo de archivo u otros problemas de E/S
            {
                MessageBox.Show($"Error writing to the log file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Intentar una estrategia de recuperación si es necesario
            }
            catch (Exception ex) // Manejar cualquier otro tipo de excepción
            {
                MessageBox.Show($"Error writing to the log file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //private void WriteToLogFile(string message)
        //{
        //    string logFilePath = $"{dbname}.log";

        //    try
        //    {
        //        using (StreamWriter sw = new StreamWriter(logFilePath, true))
        //        {
        //            sw.WriteLine(message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error writing to the log file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        /*private void LogToConsole(string message)
        {
            textBox_logConsole.AppendText(message + Environment.NewLine);
            textBox_logConsole.SelectionStart = textBox_logConsole.Text.Length;
            textBox_logConsole.ScrollToCaret();
            label_status.Text = message;


        }*/

        private void PreviewLogs()
        {

            DataTable logTable = new DataTable();

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "evtx files (*.evxt)|*.evtx|All files (*.*)|*.*";
            openFileDialog1.Title = "Open Windows Log File";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                logTable.Columns.Add("id", typeof(int));
                logTable.Columns.Add("TimeCreated", typeof(string));
                logTable.Columns.Add("UserID", typeof(string));
                logTable.Columns.Add("EventID", typeof(int));
                logTable.Columns.Add("MachineName  ", typeof(string));
                logTable.Columns.Add("Level", typeof(int));
                logTable.Columns.Add("LogName", typeof(string));
                logTable.Columns.Add("EventMessage", typeof(string));
                logTable.Columns.Add("EventMessageXML", typeof(string));
                logTable.Columns.Add("ActivityId", typeof(string));

                List<string> selectedFiles = new List<string>(openFileDialog1.FileNames);
                int cnt = 0;

                foreach (string FullPath in selectedFiles)
                {
                    //EventLogQuery query = new EventLogQuery(FullPath, PathType.FilePath);

                    int cnt2 = 0;
                    try
                    {                        
                        using (EventLogReader logReader = new EventLogReader(FullPath, PathType.FilePath))

                        {
                            EventRecord entry;
                            
                            while ((entry = logReader.ReadEvent()) != null)
                            {

                                using (entry)
                                {
                                    // Obtener información del evento
                                    cnt += 1;
                                    cnt2 += 1;

                                    DateTime time = entry.TimeCreated ?? DateTime.MinValue;
                                    string formattedTime = time.ToString("yyyy-MM-dd HH:mm:ss");
                                    string user = "N/A";
                                    if (entry.UserId != null)
                                    {
                                        user = entry.UserId.ToString();
                                    }
                                    string eventId = entry.Id.ToString();
                                    string machine = entry.MachineName.ToString();
                                    string level = entry.Level.ToString();                                    
                                    string source = entry.LogName.ToString();
                                    string message = "N/A";                                  
                                    try
                                    {
                                        if (entry.FormatDescription() != null)
                                        {
                                            message = entry.FormatDescription();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        message = ex.Message;
                                    }

                                    string messageXML = entry.ToXml();
                                    string ActId = entry.ActivityId.ToString();

                                    logTable.Rows.Add(cnt, formattedTime, user, eventId, machine, level, source, message, messageXML, ActId);

                                }
                            }

                            //textBox2.Text += cnt.ToString() + Environment.NewLine;
                        }
                    }

                    catch (EventLogNotFoundException)
                    {

                        LogToConsole("The log file '{FullPath}' was not found." + Environment.NewLine);
                        //textBox2.Text = $"The log file '{FullPath}' was not found." + Environment.NewLine;

                    }

                    catch (UnauthorizedAccessException)
                    {
                        LogToConsole("You do not have permissions to access the log '{FullPath}'." + Environment.NewLine);
                        //textBox2.Text = $"You do not have permissions to access the log '{FullPath}'." + Environment.NewLine;
                    }

                    catch (Exception Ex)
                    {
                        LogToConsole(Ex.Message + Environment.NewLine);
                        //textBox2.Text = Ex.Message + Environment.NewLine;
                    }

                    string records_number = $" with {cnt2} records.";
                    LogToConsole(FullPath + records_number);
                    //textBox2.Text += FullPath + Environment.NewLine;
                }

            }
            label_status.Text = "Get / View Logs non persistent";


            dataGridView1.DataSource = logTable.DefaultView;




        }


        private async Task AcquireLogs_Folder()
        {


            if (ConnectionString != "")
            {
                string insertQuery = @"
            INSERT INTO LogData (TimeCreated, UserID, EventID, MachineName, Level, LogName, EventMessage, EventMessageXml, ActivityID) 
            VALUES (@TimeCreated, @UserID, @EventID, @MachineName, @Level, @LogName, @EventMessage, @EventMessageXml, @ActivityID)";

                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.Description = "Select Folder Containing EVTX Files";
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        DateTime start_process = DateTime.Now;
                        disable_buttons();
                        var selectedFolder = folderBrowserDialog.SelectedPath;
                        List<string> selectedFiles = GetAllEvtxFiles(selectedFolder);

                        int filesnumber = selectedFiles.Count;

                        //progressBar.Value = 0;
                        toolStripProgressBar1.Value = 0;
                        //progressBar.Maximum = filesnumber;
                        toolStripProgressBar1.Maximum = filesnumber;
                        label_status.Text = $"Working on {filesnumber} logs, please wait...";
                        Application.DoEvents();
                        LogToConsole($"Working on {filesnumber} logs, please wait...");

                        await Task.Run(() => ProcessLogs(selectedFiles, insertQuery));

                        // Resetear valores porgressbar que se cambiaran en ProcessAll
                        //progressBar.Value = 0;
                        toolStripProgressBar1.Value = 0;
                        //progressBar.Maximum = 16;
                        toolStripProgressBar1.Maximum = 16;
                        label_status.Text = "Processing logs, please wait.";
                        Load_SfCombobox();
                        await ProcessAll();

                        enable_buttons();
                        DateTime end_process = DateTime.Now;
                        TimeSpan Total_time = end_process - start_process;
                        LogToConsole($"Total {Total_time.Days} days {Total_time.Hours} hours {Total_time.Minutes} minutes and {Total_time.Seconds} seconds");
                    }
                }
            }
            else
            {
                MessageBox.Show("Open or create a new case first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }




        }

        private List<string> GetAllEvtxFiles(string folderPath)
        {
            List<string> evtxFiles = new List<string>();
            try
            {
                foreach (string file in Directory.GetFiles(folderPath, "*.evtx", SearchOption.AllDirectories))
                {
                    evtxFiles.Add(file);
                }
            }
            catch (Exception ex)
            {
                LogToConsole($"Error accessing folder: {ex.Message}");
            }
            return evtxFiles;
        }

        private async Task AcquireLogs()
        {
            if (ConnectionString != "")
            {
                string insertQuery = @"
            INSERT INTO LogData (TimeCreated, UserID, EventID, MachineName, Level, LogName, EventMessage, EventMessageXml, ActivityID) 
            VALUES (@TimeCreated, @UserID, @EventID, @MachineName, @Level, @LogName, @EventMessage, @EventMessageXml, @ActivityID)";

                OpenFileDialog openFileDialog1 = new OpenFileDialog
                {
                    Filter = "evtx files (*.evtx)|*.evtx",
                    Title = "Open Windows Log File",
                    Multiselect = true
                };

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    DateTime start_process = DateTime.Now;
                    disable_buttons();
                    var selectedFiles = new List<string>(openFileDialog1.FileNames);
                    int filesnumber = selectedFiles.Count;
                    toolStripProgressBar1.Value = 0;
                    toolStripProgressBar1.Maximum = filesnumber;
                    label_status.Text = $"Working on {filesnumber} logs please wait...";
                    Application.DoEvents();
                    LogToConsole($"Working on {filesnumber} logs please wait...");
                    await Task.Run(() => ProcessLogs(selectedFiles, insertQuery));
                    toolStripProgressBar1.Value = 0;
                    toolStripProgressBar1.Maximum = 16;
                    label_status.Text = "Processing logs, please wait.";
                    Load_SfCombobox();
                    await ProcessAll();
                    enable_buttons();
                    DateTime end_process = DateTime.Now;
                    TimeSpan Total_time = end_process - start_process;
                    LogToConsole($"Total {Total_time.Days} days {Total_time.Hours} hours {Total_time.Minutes} minutes and {Total_time.Seconds} seconds");
                }
            }
            else
            {
                MessageBox.Show("Open or create a new case first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ProcessLogs(List<string> selectedFiles, string insertQuery)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    int cnt = 0;
                    foreach (string file in selectedFiles)
                    {
                        EventLogQuery query = new EventLogQuery(file, PathType.FilePath);
                        try
                        {
                            using (EventLogReader logReader = new EventLogReader(query))
                            {
                                EventRecord entry;
                                while ((entry = logReader.ReadEvent()) != null)
                                {
                                    cnt += 1;


                                    DateTime time = entry.TimeCreated ?? DateTime.MinValue;
                                    //string formattedTime = time.ToString("yyyy-MM-dd HH:mm:ss");
                                    string user = "N/A";
                                    if (entry.UserId != null)
                                    {
                                        user = entry.UserId.ToString();
                                    }
                                    string eventId = entry.Id.ToString();
                                    string machine = entry.MachineName.ToString();
                                    string level = entry.Level.ToString();

                                    string source = entry.LogName.ToString();
                                    string message = "N/A";



                                    try
                                    {
                                        if (entry.FormatDescription() != null)
                                        {
                                            message = entry.FormatDescription();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        message = ex.Message;
                                    }
                                    string messageXML = entry.ToXml();
                                    string ActId = entry.ActivityId.ToString();



                                    //DateTime time = entry.TimeCreated ?? DateTime.MinValue;
                                    //string user = entry.UserId?.ToString() ?? "N/A";
                                    //string eventId = entry.Id.ToString();
                                    //string machine = entry.MachineName ?? "N/A";
                                    //string level = entry.LevelDisplayName ?? "N/A";
                                    //string source = entry.LogName ?? "N/A";
                                    //string message = entry.FormatDescription() ?? "N/A";
                                    //string messageXML = entry.ToXml();
                                    //string ActId = entry.ActivityId?.ToString() ?? "N/A";


                                    using (var command = new SQLiteCommand(insertQuery, connection, transaction))
                                    {
                                        command.Parameters.AddWithValue("@TimeCreated", time);
                                        command.Parameters.AddWithValue("@UserID", user);
                                        command.Parameters.AddWithValue("@EventID", eventId);
                                        command.Parameters.AddWithValue("@MachineName", machine);
                                        command.Parameters.AddWithValue("@Level", level);
                                        command.Parameters.AddWithValue("@LogName", source);
                                        command.Parameters.AddWithValue("@EventMessage", message);
                                        command.Parameters.AddWithValue("@EventMessageXml", messageXML);
                                        command.Parameters.AddWithValue("@ActivityID", ActId);
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogToConsole($"************************************ Error processing file {file}: {ex.Message} ************************************");
                            return;
                        }

                        //UpdateProgressBar();
                        UpdatetoolStripProgressBar();

                        LogToConsole($"Successfully processed file: {file} with {cnt} logs");
                        cnt = 0;
                    }
                    transaction.Commit();
                }
                connection.Close();
            }
        }







        //Aqui comienzan las funciones que cuentan los items de cada submenu

        #region

        private void General_Interest_Menu()
        {

            string sql_query;

            sql_query = "SELECT DISTINCT UserID from LogData;";
            Viewed_Users = Query_count(sql_query);
            save_query_count("Viewed_Users", Viewed_Users);
            LogToConsole("Viewed users: " + Viewed_Users.ToString());

            sql_query = "SELECT * from LogData WHERE (EventID is 4624 or EventID is 4647) and LogName is \"Security\";";
            Log_on_Log_off = Query_count(sql_query);
            save_query_count("Log_on_Log_off", Log_on_Log_off);
            LogToConsole("Log on / Log off " + Log_on_Log_off.ToString());

            sql_query = "SELECT * from LogData WHERE EventID is 4625 AND LogName is \"Security\";";
            Failed_to_Log_on = Query_count(sql_query);
            save_query_count("Failed_to_Log_on", Failed_to_Log_on);
            LogToConsole("Failed to Log on " + Failed_to_Log_on.ToString());

            sql_query = "SELECT * from LogData WHERE EventID is 4608 or EventID is 1074;";
            Power_On_Off = Query_count(sql_query);
            save_query_count("Power_On_Off", Power_On_Off);
            LogToConsole("Power On / Off " + Power_On_Off.ToString());

            sql_query = "SELECT * from LogData WHERE (EventID is 1040 or EventID is 1042) And LogName is \"Application\";";
            Installed_Software = Query_count(sql_query);
            save_query_count("Installed_Software", Installed_Software);
            LogToConsole("Installed Software " + Installed_Software.ToString());

            sql_query = "select * from LogData WHERE EventID is 7045;";
            Installed_Service = Query_count(sql_query);
            save_query_count("Installed_Service", Installed_Service);
            LogToConsole("Installed Service " + Installed_Service.ToString());

            sql_query = "SELECT * FROM LogData WHERE EventID is 8300 or EventID is 8301 or EventID is 8302;";
            Volume_Shadow_Copy = Query_count(sql_query);
            save_query_count("Volume_Shadow_Copy", Volume_Shadow_Copy);
            LogToConsole("Volume Shadow Copy " + Volume_Shadow_Copy.ToString());

            sql_query = "select * from LogData WHERE EventID is 4720;";
            Create_Local_Users = Query_count(sql_query);
            save_query_count("Create_Local_Users", Create_Local_Users);
            LogToConsole("Create Local Users " + Create_Local_Users.ToString());

            sql_query = "select * from LogData WHERE EventID is 4726;";
            Deleted_Users = Query_count(sql_query);
            save_query_count("Deleted_Users", Deleted_Users);
            LogToConsole("Deleted Users " + Deleted_Users.ToString());

            sql_query = "select * from LogData WHERE EventID is 4781;";
            Renamed_Users = Query_count(sql_query);
            save_query_count("Renamed_Users", Renamed_Users);
            LogToConsole("Renamed Users " + Renamed_Users.ToString());


            sql_query = "SELECT * from LogData WHERE EventID is 21 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";
            Terminal_Server_Log_On = Query_count(sql_query);
            save_query_count("Terminal_Server_Log_On", Terminal_Server_Log_On);
            LogToConsole("Terminal server log on " + Terminal_Server_Log_On.ToString());

            /*sql_query = "SELECT * from LogData WHERE EventID is 22 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";
            Terminal_Server_Shell_Start = Query_count(sql_query);
            save_query_count("Terminal_Server_Shell_Start", Terminal_Server_Shell_Start);
            LogToConsole("Remote Desktop Services: Shell start notification received: " + Terminal_Server_Shell_Start.ToString());*/

            sql_query = "SELECT * from LogData WHERE EventID is 23 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";
            Terminal_Server_Log_Off = Query_count(sql_query);
            save_query_count("Terminal_Server_Log_Off", Terminal_Server_Log_Off);
            LogToConsole("Terminal server log off " + Terminal_Server_Log_Off.ToString());

            /*sql_query = "SELECT * from LogData WHERE EventID is 24 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";
            Terminal_Server_Session_Disconnected = Query_count(sql_query);
            save_query_count("Terminal_Server_Session_Disconnected", Terminal_Server_Session_Disconnected);
            LogToConsole("Remote Desktop Services: Session has been disconnected: " + Terminal_Server_Session_Disconnected.ToString());*/

            /*sql_query = "SELECT * from LogData WHERE EventID is 25 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";
            Terminal_Server_Session_Reconnection = Query_count(sql_query);
            save_query_count("Terminal_Server_Session_Reconnection", Terminal_Server_Session_Reconnection);
            LogToConsole("Remote Desktop Services: Session reconnection succeeded: " + Terminal_Server_Session_Reconnection.ToString());*/



            General_Interest = Viewed_Users + Log_on_Log_off + Failed_to_Log_on + Power_On_Off + Installed_Software + Installed_Service + Volume_Shadow_Copy
               + Create_Local_Users + Deleted_Users + Renamed_Users + Terminal_Server_Log_On + +Terminal_Server_Log_Off;



            save_query_count("General_Interest", General_Interest);

        }

        private void User_Account_Menu()
        {
            string sql_query;

            sql_query = "select * from LogData WHERE EventID is 4720;";
            User_was_created = Query_count(sql_query);
            save_query_count("User_was_created", User_was_created);
            LogToConsole("A user account was created. " + User_was_created.ToString());


            sql_query = "select * from LogData WHERE EventID is 4722;";
            User_was_enabled = Query_count(sql_query);
            save_query_count("User_was_enabled", User_was_enabled);
            LogToConsole("A user account was enabled. " + User_was_enabled.ToString());

            sql_query = "select * from LogData WHERE EventID is 4723;";
            User_chg_pass = Query_count(sql_query);
            save_query_count("User_chg_pass", User_chg_pass);
            LogToConsole("A user attempted to change an account’s password. " + User_chg_pass.ToString());

            sql_query = "select * from LogData WHERE EventID is 4724;";
            User_reset_pass = Query_count(sql_query);
            save_query_count("User_reset_pass", User_reset_pass);
            LogToConsole("An attempt was made to reset an account’s password.  " + User_reset_pass.ToString());

            sql_query = "select * from LogData WHERE EventID is 4725;";
            User_was_disabled = Query_count(sql_query);
            save_query_count("User_was_disabled", User_was_disabled);
            LogToConsole("An attempt was made to reset an account’s password.  " + User_was_disabled.ToString());

            sql_query = "select * from LogData WHERE EventID is 4726;";
            User_was_deleted = Query_count(sql_query);
            save_query_count("User_was_deleted", User_was_deleted);
            LogToConsole("A user account was deleted.  " + User_was_deleted.ToString());

            sql_query = "select * from LogData WHERE EventID is 4738;";
            User_was_changed = Query_count(sql_query);
            save_query_count("User_was_changed", User_was_changed);
            LogToConsole("A user account was changed.  " + User_was_changed.ToString());

            UserAccount = User_was_created + User_was_enabled + User_chg_pass + User_reset_pass + User_was_disabled + User_was_deleted + User_was_changed;

            save_query_count("UserAccount", UserAccount);

        }

        private void Sec_Enbl_Glob_Grp_Menu()
        {

            string sql_query;

            sql_query = "select * from LogData WHERE EventID is 4727;";
            Globgrp_Group_Was_Created = Query_count(sql_query);
            save_query_count("Globgrp_Group_Was_Created", Globgrp_Group_Was_Created);
            LogToConsole("A security-enabled global group was created.  " + Globgrp_Group_Was_Created.ToString());


            sql_query = "select * from LogData WHERE EventID is 4730;";
            Globgrp_Group_Was_Deleted = Query_count(sql_query);
            save_query_count("Globgrp_Group_Was_Deleted", Globgrp_Group_Was_Deleted);
            LogToConsole("A security-enabled global group was deleted.  " + Globgrp_Group_Was_Deleted.ToString());

            sql_query = "select * from LogData WHERE EventID is 4737;";
            Globgrp_Group_Was_Changed = Query_count(sql_query);
            save_query_count("Globgrp_Group_Was_Changed", Globgrp_Group_Was_Changed);
            LogToConsole("A security-enabled global group was changed.  " + Globgrp_Group_Was_Changed.ToString());

            sql_query = "select * from LogData WHERE EventID is 4728;";
            Globgrp_AMember_Was_Added = Query_count(sql_query);
            save_query_count("Globgrp_AMember_Was_Added", Globgrp_AMember_Was_Added);
            LogToConsole("A member was added to a security-enabled global group.  " + Globgrp_AMember_Was_Added.ToString());

            sql_query = "select * from LogData WHERE EventID is 4729;";
            Globgrp_AMember_Was_Removed = Query_count(sql_query);
            save_query_count("Globgrp_AMember_Was_Removed", Globgrp_AMember_Was_Removed);
            LogToConsole("A member was added to a security-enabled global group.  " + Globgrp_AMember_Was_Removed.ToString());

            Sec_Enbl_Glob_Grp = Globgrp_Group_Was_Created + Globgrp_Group_Was_Deleted + Globgrp_Group_Was_Changed + Globgrp_AMember_Was_Added + Globgrp_AMember_Was_Removed;

            save_query_count("Sec_Enbl_Glob_Grp", Sec_Enbl_Glob_Grp);

        }

        private void Sec_Enbl_Local_Grp_Menu()
        {

            string sql_query;

            sql_query = "select * from LogData WHERE EventID is 4731;";
            LocalGrp_Group_Was_Created = Query_count(sql_query);
            save_query_count("LocalGrp_Group_Was_Created", LocalGrp_Group_Was_Created);
            LogToConsole("A security - enabled local group was created  " + LocalGrp_Group_Was_Created.ToString());

            sql_query = "select * from LogData WHERE EventID is 4734;";
            LocalGrp_Group_Was_Deleted = Query_count(sql_query);
            save_query_count("LocalGrp_Group_Was_Deleted", LocalGrp_Group_Was_Deleted);
            LogToConsole("A security-enabled local group was deleted  " + LocalGrp_Group_Was_Deleted.ToString());

            sql_query = "select * from LogData WHERE EventID is 4735;";
            LocalGrp_Group_Was_Changed = Query_count(sql_query);
            save_query_count("LocalGrp_Group_Was_Changed", LocalGrp_Group_Was_Changed);
            LogToConsole("A security-enabled local group was changed  " + LocalGrp_Group_Was_Changed.ToString());

            sql_query = "select * from LogData WHERE EventID is 4732;";
            LocalGrp_AMember_Was_Added = Query_count(sql_query);
            save_query_count("LocalGrp_AMember_Was_Added", LocalGrp_AMember_Was_Added);
            LogToConsole("A member was added to a security-enabled local group  " + LocalGrp_AMember_Was_Added.ToString());

            sql_query = "select * from LogData WHERE EventID is 4733;";
            LocalGrp_AMember_Was_Removed = Query_count(sql_query);
            save_query_count("LocalGrp_AMember_Was_Removed", LocalGrp_AMember_Was_Removed);
            LogToConsole("A member was removed from a security-enabled local group  " + LocalGrp_AMember_Was_Removed.ToString());

            sql_query = "select * from LogData WHERE EventID is 4799;";
            LocalGrp_Group_Was_Enumerated = Query_count(sql_query);
            save_query_count("LocalGrp_Group_Was_Enumerated", LocalGrp_Group_Was_Enumerated);
            LogToConsole("A security-enabled local group membership was enumerated  " + LocalGrp_Group_Was_Enumerated.ToString());

            Sec_Enbl_Local_Grp = LocalGrp_Group_Was_Created + LocalGrp_Group_Was_Deleted + LocalGrp_Group_Was_Changed + LocalGrp_AMember_Was_Added + LocalGrp_AMember_Was_Removed
                + LocalGrp_Group_Was_Enumerated;

            save_query_count("Sec_Enbl_Local_Grp", Sec_Enbl_Local_Grp);

        }

        private void Sec_Enbl_Univ_Grp_menu()
        {

            string sql_query;

            sql_query = "select * from LogData WHERE EventID is 4754;";
            UnivGrp_Group_Was_Created = Query_count(sql_query);
            save_query_count("UnivGrp_Group_Was_Created", UnivGrp_Group_Was_Created);
            LogToConsole("A security-enabled universal group was created  " + UnivGrp_Group_Was_Created.ToString());

            sql_query = "select * from LogData WHERE EventID is 4758;";
            UnivGrp_Group_Was_Deleted = Query_count(sql_query);
            save_query_count("UnivGrp_Group_Was_Deleted", UnivGrp_Group_Was_Deleted);
            LogToConsole("A security-enabled universal group was deleted  " + UnivGrp_Group_Was_Deleted.ToString());

            sql_query = "select * from LogData WHERE EventID is 4755;";
            UnivGrp_Group_Was_Changed = Query_count(sql_query);
            save_query_count("UnivGrp_Group_Was_Changed", UnivGrp_Group_Was_Changed);
            LogToConsole("A security-enabled universal group was changed  " + UnivGrp_Group_Was_Changed.ToString());

            sql_query = "select * from LogData WHERE EventID is 4756;";
            UnivGrp_AMember_Was_Added = Query_count(sql_query);
            save_query_count("UnivGrp_AMember_Was_Added", UnivGrp_AMember_Was_Added);
            LogToConsole("A member was added to a security-enabled universal group  " + UnivGrp_AMember_Was_Added.ToString());

            sql_query = "select * from LogData WHERE EventID is 4757;";
            UnivGrp_AMember_Was_Removed = Query_count(sql_query);
            save_query_count("UnivGrp_AMember_Was_Removed", UnivGrp_AMember_Was_Removed);
            LogToConsole("A member was removed from a security-enabled universal group  " + UnivGrp_AMember_Was_Removed.ToString());


            Sec_Enbl_Univ_Grp = UnivGrp_Group_Was_Created + UnivGrp_Group_Was_Deleted + UnivGrp_Group_Was_Changed + UnivGrp_AMember_Was_Added + UnivGrp_AMember_Was_Removed;

            save_query_count("Sec_Enbl_Univ_Grp", Sec_Enbl_Univ_Grp);

        }

        private void Computer_Account_Menu()
        {

            string sql_query;

            sql_query = "select * from LogData WHERE EventID is 4741;";
            Computer_Account_Was_Created = Query_count(sql_query);
            save_query_count("Computer_Account_Was_Created", Computer_Account_Was_Created);
            LogToConsole("A computer account was created   " + Computer_Account_Was_Created.ToString());

            sql_query = "select * from LogData WHERE EventID is 4743;";
            Computer_Account_Was_Deleted = Query_count(sql_query);
            save_query_count("Computer_Account_Was_Deleted", Computer_Account_Was_Deleted);
            LogToConsole("A computer account was deleted   " + Computer_Account_Was_Deleted.ToString());

            sql_query = "select * from LogData WHERE EventID is 4742;";
            Computer_Account_Was_Changed = Query_count(sql_query);
            save_query_count("Computer_Account_Was_Changed", Computer_Account_Was_Changed);
            LogToConsole("A computer account was deleted   " + Computer_Account_Was_Changed.ToString());

            Computer_Account = Computer_Account_Was_Created + Computer_Account_Was_Deleted + Computer_Account_Was_Changed;

            save_query_count("Computer_Account", Computer_Account);

        }

        private void Network_Share_Menu()
        {
            string sql_query;

            sql_query = "select * from LogData WHERE EventID is 5140;";
            Network_Share_Accessed = Query_count(sql_query);
            save_query_count("Network_Share_Accessed", Network_Share_Accessed);
            LogToConsole("A network share object was accessed   " + Network_Share_Accessed.ToString());

            sql_query = "select * from LogData WHERE EventID is 5142;";
            Network_Share_Added = Query_count(sql_query);
            save_query_count("Network_Share_Added", Network_Share_Added);
            LogToConsole("A network share object was added   " + Network_Share_Added.ToString());

            sql_query = "select * from LogData WHERE EventID is 5143;";
            Network_Share_Modified = Query_count(sql_query);
            save_query_count("Network_Share_Modified", Network_Share_Modified);
            LogToConsole("A network share object was modified   " + Network_Share_Modified.ToString());

            sql_query = "select * from LogData WHERE EventID is 5144;";
            Network_Share_Deleted = Query_count(sql_query);
            save_query_count("Network_Share_Deleted", Network_Share_Deleted);
            LogToConsole("A network share object was deleted   " + Network_Share_Deleted.ToString());

            sql_query = "select * from LogData WHERE EventID is 5145;";
            Network_Share_Checked = Query_count(sql_query);
            save_query_count("Network_Share_Checked", Network_Share_Checked);
            LogToConsole("A network share object was checked   " + Network_Share_Checked.ToString());

            Network_Share = Network_Share_Accessed + Network_Share_Added + Network_Share_Modified + Network_Share_Deleted + Network_Share_Checked;

            save_query_count("Network_Share", Network_Share);


        }

        private void Scheduled_Task_Activity_Menu()
        {

            string sql_query;

            sql_query = "SELECT * from LogData WHERE EventID is 106 AND LogName is \"Microsoft-Windows-TaskScheduler/Operational\"; ";
            Scheduled_Task_Created = Query_count(sql_query);
            save_query_count("Scheduled_Task_Created", Scheduled_Task_Created);
            LogToConsole("Scheduled Task Created   " + Scheduled_Task_Created.ToString());

            sql_query = "SELECT * from LogData WHERE EventID is 140 AND LogName is \"Microsoft-Windows-TaskScheduler/Operational\"; ";
            Scheduled_Task_Updated = Query_count(sql_query);
            save_query_count("Scheduled_Task_Updated", Scheduled_Task_Updated);
            LogToConsole("Scheduled Task Updated   " + Scheduled_Task_Updated.ToString());

            sql_query = "SELECT * from LogData WHERE EventID is 141 AND LogName is \"Microsoft-Windows-TaskScheduler/Operational\"; ";
            Scheduled_Task_Deleted = Query_count(sql_query);
            save_query_count("Scheduled_Task_Deleted", Scheduled_Task_Deleted);
            LogToConsole("Scheduled Task Deleted   " + Scheduled_Task_Deleted.ToString());

            sql_query = "SELECT * from LogData WHERE EventID is 200 AND LogName is \"Microsoft-Windows-TaskScheduler/Operational\"; ";
            Scheduled_Task_Executed = Query_count(sql_query);
            save_query_count("Scheduled_Task_Executed", Scheduled_Task_Executed);
            LogToConsole("Scheduled Task Executed   " + Scheduled_Task_Executed.ToString());

            sql_query = "SELECT * from LogData WHERE EventID is 201 AND LogName is \"Microsoft-Windows-TaskScheduler/Operational\"; ";
            Scheduled_Task_Completed = Query_count(sql_query);
            save_query_count("Scheduled_Task_Completed", Scheduled_Task_Completed);
            LogToConsole("Scheduled Task Completed   " + Scheduled_Task_Completed.ToString());

            Scheduled_Task_Activity = Scheduled_Task_Created + Scheduled_Task_Updated + Scheduled_Task_Deleted + Scheduled_Task_Executed + Scheduled_Task_Completed;

            save_query_count("Scheduled_Task_Activity", Scheduled_Task_Activity);

        }

        private void Object_Access_Auditing_Menu()
        {
            string sql_query;

            sql_query = "select * from LogData WHERE EventID is 4698;";
            Obj_Scheduled_Task_Created = Query_count(sql_query);
            save_query_count("Obj_Scheduled_Task_Created", Obj_Scheduled_Task_Created);
            LogToConsole("A scheduled task was created   " + Obj_Scheduled_Task_Created.ToString());

            sql_query = "select * from LogData WHERE EventID is 4699;";
            Obj_Scheduled_Task_Deleted = Query_count(sql_query);
            save_query_count("Obj_Scheduled_Task_Deleted", Obj_Scheduled_Task_Deleted);
            LogToConsole("A scheduled task was deleted   " + Obj_Scheduled_Task_Deleted.ToString());

            sql_query = "select * from LogData WHERE EventID is 4700;";
            Obj_Scheduled_Task_Enabled = Query_count(sql_query);
            save_query_count("Obj_Scheduled_Task_Enabled", Obj_Scheduled_Task_Enabled);
            LogToConsole("A scheduled task was enabled   " + Obj_Scheduled_Task_Enabled.ToString());

            sql_query = "select * from LogData WHERE EventID is 4701;";
            Obj_Scheduled_Task_Disabled = Query_count(sql_query);
            save_query_count("Obj_Scheduled_Task_Disabled", Obj_Scheduled_Task_Disabled);
            LogToConsole("A scheduled task was disabled   " + Obj_Scheduled_Task_Disabled.ToString());

            sql_query = "select * from LogData WHERE EventID is 4702;";
            Obj_Scheduled_Task_Updated = Query_count(sql_query);
            save_query_count("Obj_Scheduled_Task_Updated", Obj_Scheduled_Task_Updated);
            LogToConsole("A scheduled task was updated   " + Obj_Scheduled_Task_Updated.ToString());


            Object_Access_Auditing = Obj_Scheduled_Task_Created + Obj_Scheduled_Task_Deleted + Obj_Scheduled_Task_Enabled + Obj_Scheduled_Task_Disabled + Obj_Scheduled_Task_Updated;

            save_query_count("Object_Access_Auditing", Object_Access_Auditing);

        }

        private void Object_Handle_Event_Menu()
        {
            string sql_query;

            sql_query = "select * from LogData WHERE EventID is 4656;";
            Handle_toAn_Object_Requested = Query_count(sql_query);
            save_query_count("Handle_toAn_Object_Requested", Handle_toAn_Object_Requested);
            LogToConsole("A handle to an object was requested   " + Handle_toAn_Object_Requested.ToString());

            sql_query = "select * from LogData WHERE EventID is 4657;";
            Registry_Value_Modified = Query_count(sql_query);
            save_query_count("Registry_Value_Modified", Registry_Value_Modified);
            LogToConsole("A registry value was modified   " + Registry_Value_Modified.ToString());

            sql_query = "select * from LogData WHERE EventID is 4658;";
            Handle_toAn_Object_Closed = Query_count(sql_query);
            save_query_count("Handle_toAn_Object_Closed", Handle_toAn_Object_Closed);
            LogToConsole("The handle to an object was closed   " + Handle_toAn_Object_Closed.ToString());

            sql_query = "select * from LogData WHERE EventID is 4660;";
            Object_Was_Deleted = Query_count(sql_query);
            save_query_count("Object_Was_Deleted", Object_Was_Deleted);
            LogToConsole("An object was deleted   " + Object_Was_Deleted.ToString());

            sql_query = "select * from LogData WHERE EventID is 4663;";
            Attempt_Made_to_Acc_Object = Query_count(sql_query);
            save_query_count("Attempt_Made_to_Acc_Object", Attempt_Made_to_Acc_Object);
            LogToConsole("An attempt was made to access an object   " + Attempt_Made_to_Acc_Object.ToString());

            Object_Handle_Event = Handle_toAn_Object_Requested + Registry_Value_Modified + Handle_toAn_Object_Closed + Object_Was_Deleted + Attempt_Made_to_Acc_Object;

            save_query_count("Object_Handle_Event", Object_Handle_Event);

        }


        private void Auditing_Windows_Services_Menu()
        {

            string sql_query;

            sql_query = "select * from LogData WHERE EventID is 6005 AND LogName is 'System';";
            Event_Log_Service_Started = Query_count(sql_query);
            save_query_count("Event_Log_Service_Started", Event_Log_Service_Started);
            LogToConsole("The event log service was started   " + Event_Log_Service_Started.ToString());

            sql_query = "select * from LogData WHERE EventID is 6006 AND LogName is 'System';";
            Event_Log_Service_Stoped = Query_count(sql_query);
            save_query_count("Event_Log_Service_Stoped", Event_Log_Service_Stoped);
            LogToConsole("The event log service was stopped   " + Event_Log_Service_Stoped.ToString());

            sql_query = "select * from LogData WHERE EventID is 7034 AND LogName is 'System';";
            Service_Terminated_Unexpectedly = Query_count(sql_query);
            save_query_count("Service_Terminated_Unexpectedly", Service_Terminated_Unexpectedly);
            LogToConsole("A service terminated unexpectedly   " + Service_Terminated_Unexpectedly.ToString());

            sql_query = "select * from LogData WHERE EventID is 7036 AND LogName is 'System';";
            Service_Start_Stop = Query_count(sql_query);
            save_query_count("Service_Start_Stop", Service_Start_Stop);
            LogToConsole("A service was stopped or started   " + Service_Start_Stop.ToString());

            sql_query = "select * from LogData WHERE EventID is 7040 AND LogName is 'System';";
            Start_Type_For_Service_Changed = Query_count(sql_query);
            save_query_count("Start_Type_For_Service_Changed", Start_Type_For_Service_Changed);
            LogToConsole("The start type for a service was changed   " + Start_Type_For_Service_Changed.ToString());

            sql_query = "select * from LogData WHERE EventID is 7045 AND LogName is 'System';";
            Service_Installed_bySystem = Query_count(sql_query);
            save_query_count("Service_Installed_bySystem", Service_Installed_bySystem);
            LogToConsole("A service was installed by the system   " + Service_Installed_bySystem.ToString());


            Auditing_Windows_Services = Event_Log_Service_Started + Event_Log_Service_Stoped + Service_Terminated_Unexpectedly + Service_Start_Stop
                + Start_Type_For_Service_Changed + Service_Installed_bySystem;

            save_query_count("Auditing_Windows_Services", Auditing_Windows_Services);

        }

        private void Wlan_Connection_Menu()
        {

            string sql_query;

            sql_query = "select * from LogData WHERE EventID is 8001 AND LogName is \"Microsoft-Windows-WLAN-AutoConfig/Operational\";";
            Wlan_Successfully_Connected = Query_count(sql_query);
            save_query_count("Wlan_Successfully_Connected", Wlan_Successfully_Connected);
            LogToConsole("WLAN service has successfully connected   " + Wlan_Successfully_Connected.ToString());

            sql_query = "select * from LogData WHERE EventID is 8002 AND LogName is \"Microsoft-Windows-WLAN-AutoConfig/Operational\";";
            Wlan_Failed_To_Connect = Query_count(sql_query);
            save_query_count("Wlan_Failed_To_Connect", Wlan_Failed_To_Connect);
            LogToConsole("WLAN service failed to connect to a wireless network   " + Wlan_Failed_To_Connect.ToString());

            Wlan_Connection = Wlan_Successfully_Connected + Wlan_Failed_To_Connect;

            save_query_count("Wlan_Connection", Wlan_Connection);

        }



        private void Windows_Filtering_Platform_Menu()
        {
            string sql_query;

            sql_query = "select * from LogData WHERE EventID is 5031;";
            Block_App_Accept_Incom_Conn = Query_count(sql_query);
            save_query_count("Block_App_Accept_Incom_Conn", Block_App_Accept_Incom_Conn);
            LogToConsole("The Windows Firewall Service blocked an application from accepting incoming connections on the network.   " + Block_App_Accept_Incom_Conn.ToString());


            sql_query = "select * from LogData WHERE EventID is 5152;";
            Bloked_a_Packet = Query_count(sql_query);
            save_query_count("Bloked_a_Packet", Bloked_a_Packet);
            LogToConsole("The WFP blocked a packet.   " + Bloked_a_Packet.ToString());


            sql_query = "select * from LogData WHERE EventID is 5154;";
            Permited_Incommig_Conn = Query_count(sql_query);
            save_query_count("Permited_Incommig_Conn", Permited_Incommig_Conn);
            LogToConsole("The WFP has permitted an application or service to listen on a port for incoming connections.   " + Permited_Incommig_Conn.ToString());


            sql_query = "select * from LogData WHERE EventID is 5156;";
            Allowed_Connection = Query_count(sql_query);
            save_query_count("Allowed_Connection", Allowed_Connection);
            LogToConsole("The WFP has allowed a connection.   " + Allowed_Connection.ToString());


            sql_query = "select * from LogData WHERE EventID is 5157;";
            Blocked_Connection = Query_count(sql_query);
            save_query_count("Blocked_Connection", Blocked_Connection);
            LogToConsole("The WFP has blocked a connection.   " + Blocked_Connection.ToString());


            sql_query = "select * from LogData WHERE EventID is 5158;";
            Permited_Bind_a_Local_Port = Query_count(sql_query);
            save_query_count("Permited_Bind_a_Local_Port", Permited_Bind_a_Local_Port);
            LogToConsole("The WFP has permitted a bind to a local port.   " + Permited_Bind_a_Local_Port.ToString());


            sql_query = "select * from LogData WHERE EventID is 5159;";
            Block_Bind_a_Local_Port = Query_count(sql_query);
            save_query_count("Block_Bind_a_Local_Port", Block_Bind_a_Local_Port);
            LogToConsole("The WFP has blocked a bind to a local port.   " + Block_Bind_a_Local_Port.ToString());



            Windows_Filtering_Platform = Block_App_Accept_Incom_Conn + Bloked_a_Packet + Permited_Incommig_Conn + Allowed_Connection + Blocked_Connection
                + Permited_Bind_a_Local_Port + Block_Bind_a_Local_Port;


            save_query_count("Windows_Filtering_Platform", Windows_Filtering_Platform);


        }


        private void Windows_Defender_Menu()
        {

            string sql_query;

            sql_query = "SELECT * FROM LogData WHERE EventID is 1006 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";
            Found_Malware = Query_count(sql_query);
            save_query_count("Found_Malware", Found_Malware);
            LogToConsole("The antimalware engine found malware or other potentially unwanted software.   " + Found_Malware.ToString());


            sql_query = "SELECT * FROM LogData WHERE EventID is 1007 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";
            Action_Protect_System = Query_count(sql_query);
            save_query_count("Action_Protect_System", Action_Protect_System);
            LogToConsole("The antimalware platform performed an action to protect your system from malware   " + Action_Protect_System.ToString());


            sql_query = "SELECT * FROM LogData WHERE EventID is 1008 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";
            Failed_Action_Protect_System = Query_count(sql_query);
            save_query_count("Failed_Action_Protect_System", Failed_Action_Protect_System);
            LogToConsole("The antimalware platform attempted to perform an action to protect your system, but the action failed.    " + Failed_Action_Protect_System.ToString());


            sql_query = "SELECT * FROM LogData WHERE EventID is 1013 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";
            Deleted_History_Malware = Query_count(sql_query);
            LogToConsole("The antimalware platform deleted history of malware and other potentially unwanted software   " + Deleted_History_Malware.ToString());


            sql_query = "SELECT * FROM LogData WHERE EventID is 1015 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";
            Detected_Suspicious_Behavior = Query_count(sql_query);
            save_query_count("Detected_Suspicious_Behavior", Detected_Suspicious_Behavior);
            LogToConsole("The antimalware platform detected suspicious behavior.   " + Detected_Suspicious_Behavior.ToString());


            sql_query = "SELECT * FROM LogData WHERE EventID is 1116 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";
            Detected_Malware = Query_count(sql_query);
            save_query_count("Detected_Malware", Detected_Malware);
            LogToConsole("The antimalware platform detected malware or other potentially unwanted software.   " + Detected_Malware.ToString());


            sql_query = "SELECT * FROM LogData WHERE EventID is 1119 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";
            Critical_Error_In_Action_On_Malware = Query_count(sql_query);
            LogToConsole("The antimalware platform encountered a critical error when trying to take action on malware.   " + Critical_Error_In_Action_On_Malware.ToString());


            sql_query = "SELECT * FROM LogData WHERE EventID is 5001 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";
            Real_Time_Protection_Disabled = Query_count(sql_query);
            save_query_count("Real_Time_Protection_Disabled", Real_Time_Protection_Disabled);
            LogToConsole("Real-time protection is disabled.   " + Real_Time_Protection_Disabled.ToString());


            sql_query = "SELECT * FROM LogData WHERE EventID is 5004 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";
            Real_Time_Protection_Cfg_Changed = Query_count(sql_query);
            save_query_count("Real_Time_Protection_Cfg_Changed", Real_Time_Protection_Cfg_Changed);
            LogToConsole("The real-time protection configuration changed.   " + Real_Time_Protection_Cfg_Changed.ToString());


            sql_query = "SELECT * FROM LogData WHERE EventID is 5007 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";
            Platform_Cfg_Changed = Query_count(sql_query);
            save_query_count("Platform_Cfg_Changed", Platform_Cfg_Changed);
            LogToConsole("The antimalware platform configuration changed.   " + Platform_Cfg_Changed.ToString());


            sql_query = "SELECT * FROM LogData WHERE EventID is 5010 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";
            Scanning_Malware_Disabled = Query_count(sql_query);
            save_query_count("Scanning_Malware_Disabled", Scanning_Malware_Disabled);
            LogToConsole("Scanning for malware and other potentially unwanted software is disabled   " + Scanning_Malware_Disabled.ToString());


            sql_query = "SELECT * FROM LogData WHERE EventID is 5012 AND (LogName is \"Microsoft-Windows-Security-Mitigations/KernelMode\" or LogName is \"Microsoft-Windows-Security-Mitigations/UserMode\" or LogName is \"Microsoft-Windows-Windows Defender/Operational\");";
            Scanning_Virus_Disabled = Query_count(sql_query);
            save_query_count("Scanning_Virus_Disabled", Scanning_Virus_Disabled);
            LogToConsole("Scanning for viruses is disabled.   " + Scanning_Virus_Disabled.ToString());


            Windows_Defender = Found_Malware + Action_Protect_System + Failed_Action_Protect_System + Deleted_History_Malware + Detected_Suspicious_Behavior + Detected_Malware
                + Critical_Error_In_Action_On_Malware + Real_Time_Protection_Disabled + Real_Time_Protection_Cfg_Changed + Platform_Cfg_Changed + Scanning_Malware_Disabled
                + Scanning_Virus_Disabled;

            save_query_count("Windows_Defender", Windows_Defender);
        }


        private void Sysmon_Menu()
        {
            string sql_query;

            sql_query = "select * from LogData WHERE EventID is 1 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Process_Creation = Query_count(sql_query);
            save_query_count("Process_Creation", Process_Creation);
            LogToConsole("Process creation.   " + Process_Creation.ToString());

            sql_query = "select * from LogData WHERE EventID is 2 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Proc_Chng_File_Creation_Time = Query_count(sql_query);
            save_query_count("Proc_Chng_File_Creation_Time", Proc_Chng_File_Creation_Time);
            LogToConsole("A process changed a file creation time.  " + Proc_Chng_File_Creation_Time.ToString());

            sql_query = "select * from LogData WHERE EventID is 3 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Network_Connection = Query_count(sql_query);
            save_query_count("Network_Connection", Network_Connection);
            LogToConsole("Network connection.  " + Network_Connection.ToString());

            sql_query = "select * from LogData WHERE EventID is 4 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Sysmon_Service_State_Changed = Query_count(sql_query);
            save_query_count("Sysmon_Service_State_Changed", Sysmon_Service_State_Changed);
            LogToConsole("Sysmon service state changed.  " + Sysmon_Service_State_Changed.ToString());

            sql_query = "select * from LogData WHERE EventID is 5 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Process_Terminated = Query_count(sql_query);
            save_query_count("Process_Terminated", Process_Terminated);
            LogToConsole("Process terminated.  " + Process_Terminated.ToString());

            sql_query = "select * from LogData WHERE EventID is 6 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Driver_Loaded = Query_count(sql_query);
            save_query_count("Driver_Loaded", Driver_Loaded);
            LogToConsole("Driver loaded.  " + Driver_Loaded.ToString());

            sql_query = "select * from LogData WHERE EventID is 7 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Image_Loaded = Query_count(sql_query);
            save_query_count("Image_Loaded", Image_Loaded);
            LogToConsole("Image loaded (records when a module is loaded in a specific process).  " + Image_Loaded.ToString());

            sql_query = "select * from LogData WHERE EventID is 8 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Create_Remote_Thread = Query_count(sql_query);
            save_query_count("Create_Remote_Thread", Create_Remote_Thread);
            LogToConsole("CreateRemoteThread (creating a thread in another process).  " + Create_Remote_Thread.ToString());

            sql_query = "select * from LogData WHERE EventID is 9 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Raw_Access_Read = Query_count(sql_query);
            save_query_count("Raw_Access_Read", Raw_Access_Read);
            LogToConsole("RawAccessRead (raw access to drive data using \\\\.\\ notation).  " + Raw_Access_Read.ToString());

            sql_query = "select * from LogData WHERE EventID is 10 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Process_Access = Query_count(sql_query);
            save_query_count("Process_Access", Process_Access);
            LogToConsole("ProcessAccess (opening access to another process’s memory space).  " + Process_Access.ToString());

            sql_query = "select * from LogData WHERE EventID is 11 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            File_Create = Query_count(sql_query);
            save_query_count("File_Create", File_Create);
            LogToConsole("FileCreate (creating or overwriting a file).  " + File_Create.ToString());

            sql_query = "select * from LogData WHERE EventID is 12 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Reg_Key_Value_Created_Deleted = Query_count(sql_query);
            save_query_count("Reg_Key_Value_Created_Deleted", Reg_Key_Value_Created_Deleted);
            LogToConsole("Registry key or value created or deleted.  " + Reg_Key_Value_Created_Deleted.ToString());

            sql_query = "select * from LogData WHERE EventID is 13 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Registry_Value_Modification = Query_count(sql_query);
            save_query_count("Registry_Value_Modification", Registry_Value_Modification);
            LogToConsole("Registry value modification.  " + Registry_Value_Modification.ToString());

            sql_query = "select * from LogData WHERE EventID is 14 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Registry_Key_Value_Renamed = Query_count(sql_query);
            save_query_count("Registry_Key_Value_Renamed", Registry_Key_Value_Renamed);
            LogToConsole("Registry key or value renamed.  " + Registry_Key_Value_Renamed.ToString());

            sql_query = "select * from LogData WHERE EventID is 15 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            File_Create_Stream_Hash = Query_count(sql_query);
            save_query_count("File_Create_Stream_Hash", File_Create_Stream_Hash);
            LogToConsole("FileCreateStreamHash (creation of an alternate data stream).  " + File_Create_Stream_Hash.ToString());

            sql_query = "select * from LogData WHERE EventID is 16 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Sysmon_Configuration_Change = Query_count(sql_query);
            save_query_count("Sysmon_Configuration_Change", Sysmon_Configuration_Change);
            LogToConsole("Sysmon configuration change.  " + Sysmon_Configuration_Change.ToString());

            sql_query = "select * from LogData WHERE EventID is 17 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Named_Pipe_Created = Query_count(sql_query);
            save_query_count("Named_Pipe_Created", Named_Pipe_Created);
            LogToConsole("Named pipe created.  " + Named_Pipe_Created.ToString());

            sql_query = "select * from LogData WHERE EventID is 18 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Named_Pipe_Connected = Query_count(sql_query);
            save_query_count("Named_Pipe_Connected", Named_Pipe_Connected);
            LogToConsole("Named pipe connected.  " + Named_Pipe_Connected.ToString());

            sql_query = "select * from LogData WHERE EventID is 19 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            WMIEventFilter_Activity_Detected = Query_count(sql_query);
            save_query_count("WMIEventFilter_Activity_Detected", WMIEventFilter_Activity_Detected);
            LogToConsole("WMI Event Filter activity detected.  " + WMIEventFilter_Activity_Detected.ToString());

            sql_query = "select * from LogData WHERE EventID is 20 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            WMIEventConsumer_Activity_Detected = Query_count(sql_query);
            save_query_count("WMIEventConsumer_Activity_Detected", WMIEventConsumer_Activity_Detected);
            LogToConsole("WMI Event Consumer activity detected.  " + WMIEventConsumer_Activity_Detected.ToString());

            sql_query = "select * from LogData WHERE EventID is 21 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            WMIEventConsumerToFilter_Activity_Detected = Query_count(sql_query);
            save_query_count("WMIEventConsumerToFilter_Activity_Detected", WMIEventConsumerToFilter_Activity_Detected);
            LogToConsole("WMI Event Consumer To Filter activity detected.  " + WMIEventConsumerToFilter_Activity_Detected.ToString());

            sql_query = "select * from LogData WHERE EventID is 22 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            DNS_Query_Event = Query_count(sql_query);
            save_query_count("DNS_Query_Event", DNS_Query_Event);
            LogToConsole("DNS query event.  " + DNS_Query_Event.ToString());

            sql_query = "select * from LogData WHERE EventID is 255 AND LogName is \"Microsoft-Windows-Sysmon/Operational\";";
            Sysmon_Error = Query_count(sql_query);
            save_query_count("Sysmon_Error", Sysmon_Error);
            LogToConsole("Sysmon error.  " + Sysmon_Error.ToString());

            Sysmon = Process_Creation + Proc_Chng_File_Creation_Time + Network_Connection + Sysmon_Service_State_Changed + Process_Terminated + Driver_Loaded + Image_Loaded +
            Create_Remote_Thread + Raw_Access_Read + Process_Access + File_Create + Reg_Key_Value_Created_Deleted + Registry_Value_Modification + Registry_Key_Value_Renamed +
            File_Create_Stream_Hash + Sysmon_Configuration_Change + Named_Pipe_Created + Named_Pipe_Connected + WMIEventFilter_Activity_Detected + WMIEventConsumer_Activity_Detected +
            WMIEventConsumerToFilter_Activity_Detected + DNS_Query_Event + Sysmon_Error;

            save_query_count("Sysmon", Sysmon);

        }

        private void Terminal_Server_Menu()
        {
            string sql_query;

            sql_query = "SELECT * from LogData WHERE EventID is 21 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";
            Terminal_Server_Log_On = Query_count(sql_query);
            save_query_count("Terminal_Server_Log_On", Terminal_Server_Log_On);
            LogToConsole("Terminal server log on " + Terminal_Server_Log_On.ToString());

            sql_query = "SELECT * from LogData WHERE EventID is 22 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";
            Terminal_Server_Shell_Start = Query_count(sql_query);
            save_query_count("Terminal_Server_Shell_Start", Terminal_Server_Shell_Start);
            LogToConsole("Remote Desktop Services: Shell start notification received: " + Terminal_Server_Shell_Start.ToString());

            sql_query = "SELECT * from LogData WHERE EventID is 23 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";
            Terminal_Server_Log_Off = Query_count(sql_query);
            save_query_count("Terminal_Server_Log_Off", Terminal_Server_Log_Off);
            LogToConsole("Terminal server log off " + Terminal_Server_Log_Off.ToString());

            sql_query = "SELECT * from LogData WHERE EventID is 24 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";
            Terminal_Server_Session_Disconnected = Query_count(sql_query);
            save_query_count("Terminal_Server_Session_Disconnected", Terminal_Server_Session_Disconnected);
            LogToConsole("Remote Desktop Services: Session has been disconnected: " + Terminal_Server_Session_Disconnected.ToString());

            sql_query = "SELECT * from LogData WHERE EventID is 25 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";
            Terminal_Server_Session_Reconnection = Query_count(sql_query);
            save_query_count("Terminal_Server_Session_Reconnection", Terminal_Server_Session_Reconnection);
            LogToConsole("Remote Desktop Services: Session reconnection succeeded: " + Terminal_Server_Session_Reconnection.ToString());

            sql_query = "SELECT * from LogData WHERE EventID is 39 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";
            Terminal_Server_Session_Disconnected_by_Session = Query_count(sql_query);
            save_query_count("Terminal_Server_Session_Disconnected_by_Session", Terminal_Server_Session_Disconnected_by_Session);
            LogToConsole("Session disconnected by another session " + Terminal_Server_Session_Disconnected_by_Session.ToString());

            sql_query = "SELECT * from LogData WHERE EventID is 40 AND LogName is \"Microsoft-Windows-TerminalServices-LocalSessionManager/Operational\";";
            Terminal_Server_Session_Disconnected_Code = Query_count(sql_query);
            save_query_count("Terminal_Server_Session_Disconnected_Code", Terminal_Server_Session_Disconnected_Code);
            LogToConsole("Session has been disconnected reason / code " + Terminal_Server_Session_Disconnected_Code.ToString());


            Terminal_Server = Terminal_Server_Log_On + Terminal_Server_Shell_Start + Terminal_Server_Log_Off + Terminal_Server_Session_Disconnected
                + Terminal_Server_Session_Reconnection + Terminal_Server_Session_Disconnected_by_Session + Terminal_Server_Session_Disconnected_Code;


            save_query_count("Terminal_Server", Terminal_Server);

        }


        #endregion


        private void ShowQueryOnDataGridView(string query)
        {


            if (ConnectionString != "")
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                        {
                            try
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                // Agregar la columna itemcount de primero
                                DataColumn itemCountColumn = new DataColumn("ItemCount", typeof(int));
                                dataTable.Columns.Add(itemCountColumn);

                                // Establecer los valores en la columna "ItemCount"
                                for (int i = 0; i < dataTable.Rows.Count; i++)
                                {
                                    dataTable.Rows[i]["ItemCount"] = i + 1;
                                }

                                // Reordenar las columnas para poner "ItemCount" al principio
                                dataTable.Columns["ItemCount"].SetOrdinal(0);
                                //Aqui terminamos lo de agregar la columna ItemCount primero



                                // Asigna el DataTable al DataGridView
                                dataGridView1.DataSource = dataTable;
                                HitNumber = dataTable.Rows.Count;
                            }
                            catch (System.Data.SQLite.SQLiteException Ex)
                            {
                                MessageBox.Show("Invalid query or Data base unsupported: " + Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            catch (Exception Ex)
                            {
                                MessageBox.Show(Ex.Message);
                            }
                        }


                    }
                    connection.Close();
                }
                PaintGridView();
            }
        }


        public int Query_count(string sql)
        {
            if (ConnectionString != "")
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                    {
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                        {
                            try
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                HitNumber = dataTable.Rows.Count;

                            }
                            catch (System.Data.SQLite.SQLiteException Ex)
                            {
                                MessageBox.Show("Invalid query or Data base unsupported: " + Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            catch (Exception Ex)
                            {
                                MessageBox.Show(Ex.Message);
                            }
                        }
                    }
                    connection.Close();
                }

            }
            return HitNumber;
        }



        void save_query_count(string result, int count)
        {

            if (ConnectionString != "")
            {

                string insertQuery = @"
                INSERT INTO Results (Result, Result_Value)
                VALUES (@Result, @Result_Value)
                ON CONFLICT(Result) DO UPDATE SET
                Result_Value = excluded.Result_Value;";

                var connection = new SQLiteConnection(ConnectionString);
                connection.Open();


                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Result", result);
                    command.Parameters.AddWithValue("@Result_Value", count);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }






        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Configurar propiedades del SaveFileDialog
                //saveFileDialog.Filter = "qlog files (*.qlog)|*.qlog|All files (*.*)|*.*";
                saveFileDialog.Filter = "Quick Log files (*.qlog)|*.qlog";
                saveFileDialog.Title = "Create Project";
                saveFileDialog.FileName = "Default.qlog"; // Nombre predeterminado del archivo

                // Mostrar el cuadro de diálogo y obtener el resultado
                DialogResult result = saveFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    //Limpiar los cuadros de seleccion de los filtros

                    Clear_Search();
                    Close_All();
                    // Obtener la ruta del archivo seleccionado
                    dbname = saveFileDialog.FileName;
                    this.ConnectionString = $"Data Source={dbname};Version=3;";
                    this.Text = "Quick Log v 0.1 is working on:   " + dbname;
                    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))

                    {
                        connection.Open();

                        string setPragma = "PRAGMA foreign_keys = ON;";

                        string createTableLabels = @"
                        CREATE TABLE IF NOT EXISTS Labels (
                        Label_name TEXT PRIMARY KEY NOT NULL,
                        Label_color TEXT
                        )";


                        string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS LogData (
                        Log_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        TimeCreated DATETIME, 
                        UserID TEXT,
                        EventID INTEGER,
                        MachineName TEXT,
                        Level INTEGER,
                        LogName TEXT,
                        EventMessage TEXT,
                        EventMessageXml TEXT,                        
                        ActivityID  TEXT,
                        Label    TEXT,
                        Comment TEXT,                      
                        FOREIGN KEY (Label) REFERENCES Labels(Label_name)
                        )";

                        //Crear tabla de resultados para guardar resultados y no calccular cada vez que se abre

                        string createTableResults = @"
                        CREATE TABLE IF NOT EXISTS Results (
                        Result TEXT PRIMARY KEY NOT NULL,
                        Result_Value INTEGER
                        )";



                        using (SQLiteCommand command = new SQLiteCommand(setPragma, connection))
                        {
                            command.ExecuteNonQuery();
                            LogToConsole("Pragma On Ok.");
                        }

                        using (SQLiteCommand command = new SQLiteCommand(createTableLabels, connection))
                        {
                            command.ExecuteNonQuery();
                            LogToConsole("Table created Ok.");
                        }


                        using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                        {
                            command.ExecuteNonQuery();
                            LogToConsole("Table created Ok.");
                        }

                        using (SQLiteCommand command = new SQLiteCommand(createTableResults, connection))
                        {
                            command.ExecuteNonQuery();
                            LogToConsole("Table created Ok.");
                        }

                        connection.Close();

                    }
                    LogToConsole($"Workspace {dbname} created.");
                    enable_buttons();
                    label_status.Text = "Add logs using the acquire logs button or drag and drop";

                }
            }
        }



        private async void openToolStripMenuItem_Click(object sender, EventArgs e)
        {


            using (OpenFileDialog OpenFileDialog = new OpenFileDialog())
            {

                
                OpenFileDialog.Filter = "Quick Log files (*.qlog)|*.qlog";
                OpenFileDialog.Title = "Open Project";
                DialogResult result = OpenFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    DateTime start_process = DateTime.Now;
                    label_status.Text = "Opening Workspace...";
                    Application.DoEvents();
                    LogToConsole("Opening Workspace...");
                    Clear_Search();
                    //Close_All();
                    disable_buttons();
                    dbname = OpenFileDialog.FileName;
                    this.ConnectionString = $"Data Source={dbname};Version=3;";
                    this.Text = "Quick Log v 0.1 is working on:   " + dbname;
                    // Inicializar la barra de progreso
                    
                    toolStripProgressBar1.Value = 0;
                   
                    toolStripProgressBar1.Maximum = 16;
                    Load_SfCombobox();
                    LoadValues();
                    //await ProcessAll();
                    enable_buttons();
                    DateTime end_process = DateTime.Now;
                    TimeSpan Total_time = end_process - start_process;
                    LogToConsole($"Total {Total_time.Days} days {Total_time.Hours} hours {Total_time.Minutes} minutes and {Total_time.Seconds} seconds");
                    label_status.Text = "Viewed Users";
                    //LogToConsole(Viewed_Users.ToString());


                }

            }
        }

        private void Load_SfCombobox()
        {
            Load_sfComboBox_UserID();
            Load_sfComboBox_EventID();
            Load_sfComboBox_MachineName();
            Load_sfComboBox_Level();
            Load_sfComboBox_LogName();
            Load_sfComboBox_Label();
        }



        private async Task LoadValues()
        {



            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                string query = "SELECT Result, Result_Value FROM Results";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string result = reader["Result"].ToString();
                            int resultValue = Convert.ToInt32(reader["Result_Value"]);
                            resultsDictionary[result] = resultValue;
                        }
                    }
                }
            }

            var tasks = new List<Func<Task>>
        {

            () => Task.Run(() => LoadlValues_General_Interest()),
            () => Task.Run(() => LoadlValues_User_Account()),
            () => Task.Run(() => LoadlValues_Sec_Enbl_Glob_Grp()),
            () => Task.Run(() => LoadlValues_Sec_Enbl_Local_Grp()),
            () => Task.Run(() => LoadlValues_Sec_Enbl_Univ_Grp()),
            () => Task.Run(() => LoadlValues_Computer_Account()),
            () => Task.Run(() => LoadlValues_Network_Share()),
            () => Task.Run(() => LoadlValues_Scheduled_Task_Activity()),
            () => Task.Run(() => LoadlValues_Object_Access_Auditing()),
            () => Task.Run(() => LoadlValues_Object_Handle_Event()),
            () => Task.Run(() => LoadlValues_Auditing_Windows_Services()),
            () => Task.Run(() => LoadlValues_Wlan_Connection()),
            () => Task.Run(() => LoadlValues_Terminal_Server()),
            () => Task.Run(() => LoadlValues_Windows_Filtering_Platform()),
            () => Task.Run(() => LoadlValues_Windows_Defender()),
            () => Task.Run(() => LoadlValues_Sysmon())
        };


            foreach (var task in tasks)
            {
                await task();
                UpdatetoolStripProgressBar();
            }

            viewed_users();
            AcordeonMenu();
        }


       

        #region


        private void LoadlValues_General_Interest()
        {

            int value;

            resultsDictionary.TryGetValue("Viewed_Users", out value);
            Viewed_Users = value;

            resultsDictionary.TryGetValue("Log_on_Log_off", out value);
            Log_on_Log_off = value;

            resultsDictionary.TryGetValue("Failed_to_Log_on", out value);
            Failed_to_Log_on = value;

            resultsDictionary.TryGetValue("Power_On_Off", out value);
            Power_On_Off = value;

            resultsDictionary.TryGetValue("Installed_Software", out value);
            Installed_Software = value;

            resultsDictionary.TryGetValue("Installed_Service", out value);
            Installed_Service = value;

            resultsDictionary.TryGetValue("Volume_Shadow_Copy", out value);
            Volume_Shadow_Copy = value;

            resultsDictionary.TryGetValue("Create_Local_Users", out value);
            Create_Local_Users = value;

            resultsDictionary.TryGetValue("Deleted_Users", out value);
            Deleted_Users = value;

            resultsDictionary.TryGetValue("Renamed_Users", out value);
            Renamed_Users = value;

            resultsDictionary.TryGetValue("General_Interest", out value);
            General_Interest = value;

            resultsDictionary.TryGetValue("All_Logs", out value);
            All_Logs = value;

        }

        // Cargar los datos ya procesados de seg menu User Account
        private void LoadlValues_User_Account()
        {
            int value;

            resultsDictionary.TryGetValue("User_was_created", out value);
            User_was_created = value;

            resultsDictionary.TryGetValue("User_was_enabled", out value);
            User_was_enabled = value;

            resultsDictionary.TryGetValue("User_chg_pass", out value);
            User_chg_pass = value;

            resultsDictionary.TryGetValue("User_reset_pass", out value);
            User_reset_pass = value;

            resultsDictionary.TryGetValue("User_was_disabled", out value);
            User_was_disabled = value;

            resultsDictionary.TryGetValue("User_was_deleted", out value);
            User_was_deleted = value;

            resultsDictionary.TryGetValue("User_was_changed", out value);
            User_was_changed = value;

            resultsDictionary.TryGetValue("UserAccount", out value);
            UserAccount = value;

        }

        // Cargar los datos ya procesados de tercer menu Sec_Enbl_Glob_Grp
        private void LoadlValues_Sec_Enbl_Glob_Grp()
        {
            int value;

            resultsDictionary.TryGetValue("Globgrp_Group_Was_Created", out value);
            Globgrp_Group_Was_Created = value;

            resultsDictionary.TryGetValue("Globgrp_Group_Was_Deleted", out value);
            Globgrp_Group_Was_Deleted = value;

            resultsDictionary.TryGetValue("Globgrp_Group_Was_Changed", out value);
            Globgrp_Group_Was_Changed = value;

            resultsDictionary.TryGetValue("Globgrp_AMember_Was_Added", out value);
            Globgrp_AMember_Was_Added = value;

            resultsDictionary.TryGetValue("Globgrp_AMember_Was_Removed", out value);
            Globgrp_AMember_Was_Removed = value;

            resultsDictionary.TryGetValue("Sec_Enbl_Glob_Grp", out value);
            Sec_Enbl_Glob_Grp = value;


        }


        // Cargar los datos ya procesados de cuarto menu Sec_Enbl_Local_Grp

        private void LoadlValues_Sec_Enbl_Local_Grp()
        {
            int value;

            resultsDictionary.TryGetValue("LocalGrp_Group_Was_Created", out value);
            LocalGrp_Group_Was_Created = value;

            resultsDictionary.TryGetValue("LocalGrp_Group_Was_Deleted", out value);
            LocalGrp_Group_Was_Deleted = value;

            resultsDictionary.TryGetValue("LocalGrp_Group_Was_Changed", out value);
            LocalGrp_Group_Was_Changed = value;

            resultsDictionary.TryGetValue("LocalGrp_AMember_Was_Added", out value);
            LocalGrp_AMember_Was_Added = value;

            resultsDictionary.TryGetValue("LocalGrp_AMember_Was_Removed", out value);
            LocalGrp_AMember_Was_Removed = value;

            resultsDictionary.TryGetValue("LocalGrp_Group_Was_Enumerated", out value);
            LocalGrp_Group_Was_Enumerated = value;

            resultsDictionary.TryGetValue("Sec_Enbl_Local_Grp", out value);
            Sec_Enbl_Local_Grp = value;

        }

        // Cargar los datos ya procesados de quinto menu Sec_Enbl_Univ_Grp

        private void LoadlValues_Sec_Enbl_Univ_Grp()
        {

            int value;

            resultsDictionary.TryGetValue("UnivGrp_Group_Was_Created", out value);
            UnivGrp_Group_Was_Created = value;

            resultsDictionary.TryGetValue("UnivGrp_Group_Was_Deleted", out value);
            UnivGrp_Group_Was_Deleted = value;

            resultsDictionary.TryGetValue("UnivGrp_Group_Was_Changed", out value);
            UnivGrp_Group_Was_Changed = value;

            resultsDictionary.TryGetValue("UnivGrp_AMember_Was_Added", out value);
            UnivGrp_AMember_Was_Added = value;

            resultsDictionary.TryGetValue("UnivGrp_AMember_Was_Removed", out value);
            UnivGrp_AMember_Was_Removed = value;

            resultsDictionary.TryGetValue("Sec_Enbl_Univ_Grp", out value);
            Sec_Enbl_Univ_Grp = value;

        }

        // Cargar los datos ya procesados de sexto menu Sec_Enbl_Univ_Grp

        private void LoadlValues_Computer_Account()
        {


            int value;

            resultsDictionary.TryGetValue("Computer_Account_Was_Created", out value);
            Computer_Account_Was_Created = value;

            resultsDictionary.TryGetValue("Computer_Account_Was_Deleted", out value);
            Computer_Account_Was_Deleted = value;

            resultsDictionary.TryGetValue("Computer_Account_Was_Changed", out value);
            Computer_Account_Was_Changed = value;

            resultsDictionary.TryGetValue("Computer_Account", out value);
            Computer_Account = value;

        }


        //  aqui sptimo menu: Network Share


        private void LoadlValues_Network_Share()
        {

            int value;

            resultsDictionary.TryGetValue("Network_Share_Accessed", out value);
            Network_Share_Accessed = value;

            resultsDictionary.TryGetValue("Network_Share_Added", out value);
            Network_Share_Added = value;

            resultsDictionary.TryGetValue("Network_Share_Modified", out value);
            Network_Share_Modified = value;

            resultsDictionary.TryGetValue("Network_Share_Deleted", out value);
            Network_Share_Deleted = value;

            resultsDictionary.TryGetValue("Network_Share_Checked", out value);
            Network_Share_Checked = value;

            resultsDictionary.TryGetValue("Network_Share", out value);
            Network_Share = value;

        }

        // octavo menu: Scheduled Task Activity

        private void LoadlValues_Scheduled_Task_Activity()
        {

            int value;

            resultsDictionary.TryGetValue("Scheduled_Task_Created", out value);
            Scheduled_Task_Created = value;

            resultsDictionary.TryGetValue("Scheduled_Task_Updated", out value);
            Scheduled_Task_Updated = value;

            resultsDictionary.TryGetValue("Scheduled_Task_Deleted", out value);
            Scheduled_Task_Deleted = value;

            resultsDictionary.TryGetValue("Scheduled_Task_Executed", out value);
            Scheduled_Task_Executed = value;

            resultsDictionary.TryGetValue("Scheduled_Task_Completed", out value);
            Scheduled_Task_Completed = value;

            resultsDictionary.TryGetValue("Scheduled_Task_Activity", out value);
            Scheduled_Task_Activity = value;

        }

        // noveno menu: Obj Acc Aud Sched Task 

        private void LoadlValues_Object_Access_Auditing()
        {

            int value;

            resultsDictionary.TryGetValue("Obj_Scheduled_Task_Created", out value);
            Obj_Scheduled_Task_Created = value;

            resultsDictionary.TryGetValue("Obj_Scheduled_Task_Deleted", out value);
            Obj_Scheduled_Task_Deleted = value;

            resultsDictionary.TryGetValue("Obj_Scheduled_Task_Enabled", out value);
            Obj_Scheduled_Task_Enabled = value;

            resultsDictionary.TryGetValue("Obj_Scheduled_Task_Disabled", out value);
            Obj_Scheduled_Task_Disabled = value;

            resultsDictionary.TryGetValue("Obj_Scheduled_Task_Updated", out value);
            Obj_Scheduled_Task_Updated = value;

            resultsDictionary.TryGetValue("Object_Access_Auditing", out value);
            Object_Access_Auditing = value;

        }


        // decimo menu: Object Handle Event

        private void LoadlValues_Object_Handle_Event()
        {

            int value;

            resultsDictionary.TryGetValue("Handle_toAn_Object_Requested", out value);
            Handle_toAn_Object_Requested = value;

            resultsDictionary.TryGetValue("Registry_Value_Modified", out value);
            Registry_Value_Modified = value;

            resultsDictionary.TryGetValue("Handle_toAn_Object_Closed", out value);
            Handle_toAn_Object_Closed = value;

            resultsDictionary.TryGetValue("Object_Was_Deleted", out value);
            Object_Was_Deleted = value;

            resultsDictionary.TryGetValue("Attempt_Made_to_Acc_Object", out value);
            Attempt_Made_to_Acc_Object = value;

            resultsDictionary.TryGetValue("Object_Handle_Event", out value);
            Object_Handle_Event = value;

        }

        // Menu 11: Auditing Windows Services


        private void LoadlValues_Auditing_Windows_Services()
        {

            int value;

            resultsDictionary.TryGetValue("Event_Log_Service_Started", out value);
            Event_Log_Service_Started = value;

            resultsDictionary.TryGetValue("Event_Log_Service_Stoped", out value);
            Event_Log_Service_Stoped = value;

            resultsDictionary.TryGetValue("Service_Terminated_Unexpectedly", out value);
            Service_Terminated_Unexpectedly = value;

            resultsDictionary.TryGetValue("Service_Start_Stop", out value);
            Service_Start_Stop = value;

            resultsDictionary.TryGetValue("Start_Type_For_Service_Changed", out value);
            Start_Type_For_Service_Changed = value;

            resultsDictionary.TryGetValue("Service_Installed_bySystem", out value);
            Service_Installed_bySystem = value;

            resultsDictionary.TryGetValue("Auditing_Windows_Services", out value);
            Auditing_Windows_Services = value;

        }

        //menu doce: Wi-Fi Connection


        private void LoadlValues_Wlan_Connection()
        {

            int value;

            resultsDictionary.TryGetValue("Wlan_Successfully_Connected", out value);
            Wlan_Successfully_Connected = value;

            resultsDictionary.TryGetValue("Wlan_Failed_To_Connect", out value);
            Wlan_Failed_To_Connect = value;

            resultsDictionary.TryGetValue("Wlan_Connection", out value);
            Wlan_Connection = value;

        }

        // menu trece: Windows Filtering Platform


        private void LoadlValues_Windows_Filtering_Platform()
        {


            int value;

            resultsDictionary.TryGetValue("Block_App_Accept_Incom_Conn", out value);
            Block_App_Accept_Incom_Conn = value;

            resultsDictionary.TryGetValue("Bloked_a_Packet", out value);
            Bloked_a_Packet = value;

            resultsDictionary.TryGetValue("Permited_Incommig_Conn", out value);
            Permited_Incommig_Conn = value;

            resultsDictionary.TryGetValue("Allowed_Connection", out value);
            Allowed_Connection = value;

            resultsDictionary.TryGetValue("Blocked_Connection", out value);
            Blocked_Connection = value;

            resultsDictionary.TryGetValue("Permited_Bind_a_Local_Port", out value);
            Permited_Bind_a_Local_Port = value;

            resultsDictionary.TryGetValue("Block_Bind_a_Local_Port", out value);
            Block_Bind_a_Local_Port = value;

            resultsDictionary.TryGetValue("Windows_Filtering_Platform", out value);
            Windows_Filtering_Platform = value;


        }

        // Menu catorce: Windows Defender


        private void LoadlValues_Windows_Defender()
        {



            int value;

            resultsDictionary.TryGetValue("Found_Malware", out value);
            Found_Malware = value;

            resultsDictionary.TryGetValue("Action_Protect_System", out value);
            Action_Protect_System = value;

            resultsDictionary.TryGetValue("Failed_Action_Protect_System", out value);
            Failed_Action_Protect_System = value;

            resultsDictionary.TryGetValue("Deleted_History_Malware", out value);
            Deleted_History_Malware = value;

            resultsDictionary.TryGetValue("Detected_Suspicious_Behavior", out value);
            Detected_Suspicious_Behavior = value;

            resultsDictionary.TryGetValue("Detected_Malware", out value);
            Detected_Malware = value;

            resultsDictionary.TryGetValue("Critical_Error_In_Action_On_Malware", out value);
            Critical_Error_In_Action_On_Malware = value;

            resultsDictionary.TryGetValue("Real_Time_Protection_Disabled", out value);
            Real_Time_Protection_Disabled = value;

            resultsDictionary.TryGetValue("Real_Time_Protection_Cfg_Changed", out value);
            Real_Time_Protection_Cfg_Changed = value;

            resultsDictionary.TryGetValue("Platform_Cfg_Changed", out value);
            Platform_Cfg_Changed = value;

            resultsDictionary.TryGetValue("Scanning_Malware_Disabled", out value);
            Scanning_Malware_Disabled = value;

            resultsDictionary.TryGetValue("Scanning_Virus_Disabled", out value);
            Scanning_Virus_Disabled = value;

            resultsDictionary.TryGetValue("Windows_Defender", out value);
            Windows_Defender = value;


        }

        //  menu quince: sysmon


        private void LoadlValues_Sysmon()
        {




            int value;

            resultsDictionary.TryGetValue("Process_Creation", out value);
            Process_Creation = value;

            resultsDictionary.TryGetValue("Proc_Chng_File_Creation_Time", out value);
            Proc_Chng_File_Creation_Time = value;

            resultsDictionary.TryGetValue("Network_Connection", out value);
            Network_Connection = value;

            resultsDictionary.TryGetValue("Sysmon_Service_State_Changed", out value);
            Sysmon_Service_State_Changed = value;

            resultsDictionary.TryGetValue("Process_Terminated", out value);
            Process_Terminated = value;

            resultsDictionary.TryGetValue("Driver_Loaded", out value);
            Driver_Loaded = value;

            resultsDictionary.TryGetValue("Image_Loaded", out value);
            Image_Loaded = value;

            resultsDictionary.TryGetValue("Create_Remote_Thread", out value);
            Create_Remote_Thread = value;

            resultsDictionary.TryGetValue("Raw_Access_Read", out value);
            Raw_Access_Read = value;

            resultsDictionary.TryGetValue("Process_Access", out value);
            Process_Access = value;

            resultsDictionary.TryGetValue("File_Create", out value);
            File_Create = value;

            resultsDictionary.TryGetValue("Reg_Key_Value_Created_Deleted", out value);
            Reg_Key_Value_Created_Deleted = value;

            resultsDictionary.TryGetValue("Registry_Value_Modification", out value);
            Registry_Value_Modification = value;

            resultsDictionary.TryGetValue("Registry_Key_Value_Renamed", out value);
            Registry_Key_Value_Renamed = value;

            resultsDictionary.TryGetValue("File_Create_Stream_Hash", out value);
            File_Create_Stream_Hash = value;

            resultsDictionary.TryGetValue("Sysmon_Configuration_Change", out value);
            Sysmon_Configuration_Change = value;

            resultsDictionary.TryGetValue("Named_Pipe_Created", out value);
            Named_Pipe_Created = value;

            resultsDictionary.TryGetValue("Named_Pipe_Connected", out value);
            Named_Pipe_Connected = value;

            resultsDictionary.TryGetValue("WMIEventFilter_Activity_Detected", out value);
            WMIEventFilter_Activity_Detected = value;

            resultsDictionary.TryGetValue("WMIEventConsumer_Activity_Detected", out value);
            WMIEventConsumer_Activity_Detected = value;

            resultsDictionary.TryGetValue("WMIEventConsumerToFilter_Activity_Detected", out value);
            WMIEventConsumerToFilter_Activity_Detected = value;

            resultsDictionary.TryGetValue("DNS_Query_Event", out value);
            DNS_Query_Event = value;

            resultsDictionary.TryGetValue("Sysmon_Error", out value);
            Sysmon_Error = value;

            resultsDictionary.TryGetValue("Sysmon", out value);
            Sysmon = value;


        }


        //Menu 16 Terminal Server

        private void LoadlValues_Terminal_Server()
        {

            int value;

            resultsDictionary.TryGetValue("Terminal_Server_Log_On", out value);
            Terminal_Server_Log_On = value;

            resultsDictionary.TryGetValue("Terminal_Server_Shell_Start", out value);
            Terminal_Server_Shell_Start = value;

            resultsDictionary.TryGetValue("Terminal_Server_Log_Off", out value);
            Terminal_Server_Log_Off = value;

            resultsDictionary.TryGetValue("Terminal_Server_Session_Disconnected", out value);
            Terminal_Server_Session_Disconnected = value;

            resultsDictionary.TryGetValue("Terminal_Server_Session_Reconnection", out value);
            Terminal_Server_Session_Reconnection = value;

            resultsDictionary.TryGetValue("Terminal_Server_Session_Disconnected_Code", out value);
            Terminal_Server_Session_Disconnected_Code = value;

            resultsDictionary.TryGetValue("Terminal_Server_Session_Disconnected_by_Session", out value);
            Terminal_Server_Session_Disconnected_by_Session = value;

            resultsDictionary.TryGetValue("Terminal_Server", out value);
            Terminal_Server = value;




        }



        #endregion









        private async Task ProcessAll()
        {
            LogToConsole("Working, please wait...");

            // Lista de tareas a ejecutar
            var tasks = new List<Func<Task>>
        {

            () => Task.Run(() => General_Interest_Menu()),
            () => Task.Run(() => User_Account_Menu()),
            () => Task.Run(() => Sec_Enbl_Glob_Grp_Menu()),
            () => Task.Run(() => Sec_Enbl_Local_Grp_Menu()),
            () => Task.Run(() => Sec_Enbl_Univ_Grp_menu()),
            () => Task.Run(() => Computer_Account_Menu()),
            () => Task.Run(() => Network_Share_Menu()),
            () => Task.Run(() => Scheduled_Task_Activity_Menu()),
            () => Task.Run(() => Object_Access_Auditing_Menu()),
            () => Task.Run(() => Object_Handle_Event_Menu()),
            () => Task.Run(() => Auditing_Windows_Services_Menu()),
            () => Task.Run(() => Wlan_Connection_Menu()),
            () => Task.Run(() => Terminal_Server_Menu()),
            () => Task.Run(() => Windows_Filtering_Platform_Menu()),
            () => Task.Run(() => Windows_Defender_Menu()),
            () => Task.Run(() => Sysmon_Menu())
        };

            foreach (var task in tasks)
            {
                await task();
                //UpdateProgressBar();
                UpdatetoolStripProgressBar();
            }

            string query = "SELECT * from logdata;";
            All_Logs = Query_count(query);
            save_query_count("All_Logs", All_Logs);
            viewed_users();
            AcordeonMenu();
        }


        //private void UpdateProgressBar()
        //{
        //    if (progressBar.InvokeRequired)
        //    {
        //        progressBar.Invoke(new Action(UpdateProgressBar));
        //    }
        //    else
        //    {
        //        progressBar.Value += 1;
        //    }
        //}



        // toolStripProgressBar1 aqui

        private void UpdatetoolStripProgressBar()
        {
            // Usamos el control padre del ToolStripProgressBar para verificar si se requiere la invocación
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke(new Action(UpdatetoolStripProgressBar));
            }
            else
            {
                toolStripProgressBar1.Value += 1;
            }
        }



        


        private void enable_buttons()
        {

            groupBox_LabelsAndComments.Enabled = true;
            groupBox_Time_Related_Filters.Enabled = true;
            groupBox_Save_to.Enabled = true;
            groupBox_Main.Enabled = true;
            groupBox_CustomSearch.Enabled = true;

            btn_About.Enabled = true;
            dataGridView1.Enabled = true;
            richTextBox_Detail.Enabled = true;
            btn_ShowFontDialog.Enabled = true;
           


        }


        private void disable_buttons()

        {


            if (InvokeRequired)
            {
                Invoke(new Action(disable_buttons));
                return;
            }

            groupBox_LabelsAndComments.Enabled = false;
            groupBox_Time_Related_Filters.Enabled = false;
            groupBox_Save_to.Enabled = false;
            groupBox_Main.Enabled = false;
            groupBox_CustomSearch.Enabled = false;

            btn_About.Enabled = false;
            dataGridView1.Enabled = false;
            richTextBox_Detail.Enabled = false;
            btn_ShowFontDialog.Enabled = false;


        }


        private void Btn_Seacrh_Click(object sender, EventArgs e)
        {
            searchTerm = "";
            searchTerm2 = "";
            searchTermExists = false;
            search2TermExists = false;
            if (ConnectionString != "")
            {


                //groupBox_CustomSearch.BackColor = Color.LightSkyBlue;
                label_status.Text = "";
                //LogToConsole("Searching...");



                //Capturamos el rango de fechas
                if (StartDate > EndDate)
                {
                    MessageBox.Show("The start date must be less than the end date.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpEnd.Value = DateTime.Now;
                    dtpStart.Value = DateTime.Now.AddYears(-20);
                    return;
                }


                //Esta variable solo esta por debug
                int cnt = 0;


                // Tomamos rango de tiempo de busqueda
                string sd = StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                string ed = EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                string sql_query_time = $"SELECT * from LogData WHERE (TimeCreated >='{sd}' AND TimeCreated <='{ed}')";

                string sql_query_UserID = "";




                //Lista para ir construyendo el comando sql

                List<string> SelectedItems = new List<string>();

                //Capturamos el userID

                ObservableCollection<object> Checked_Items = sfComboBox_UserID.CheckedItems;

                foreach (var item in Checked_Items)
                {
                    cnt += 1;
                    SelectedItems.Add(item.ToString());

                }


                if (SelectedItems.Count > 0)
                {
                    if (SelectedItems.Count == 1)
                    {
                        sql_query_UserID = $" and (UserID is \"{SelectedItems[0]}\")";
                    }
                    else
                    {
                        for (int i = 0; i < SelectedItems.Count; i++)
                        {

                            if (i == 0)
                            {
                                sql_query_UserID += $" and (UserID is \"{SelectedItems[0]}\"";
                            }
                            if (i == SelectedItems.Count - 1)
                            {
                                sql_query_UserID += $" or UserID is \"{SelectedItems[i]}\")";
                            }
                            if (i != 0 && i != SelectedItems.Count - 1)
                            {
                                sql_query_UserID += $" or UserID is \"{SelectedItems[i]}\"";
                            }


                        }
                    }
                }



                //Capturamos el EventID

                string sql_query_EventID = "";

                SelectedItems.Clear();

                Checked_Items = sfComboBox_EventID.CheckedItems;

                foreach (var item in Checked_Items)
                {
                    cnt += 1;
                    SelectedItems.Add(item.ToString());

                }


                if (SelectedItems.Count > 0)
                {
                    if (SelectedItems.Count == 1)
                    {
                        sql_query_EventID = $" and (EventID is \"{SelectedItems[0]}\")";
                    }
                    else
                    {
                        for (int i = 0; i < SelectedItems.Count; i++)
                        {

                            if (i == 0)
                            {
                                sql_query_EventID += $" and (EventID is \"{SelectedItems[0]}\"";
                            }
                            if (i == SelectedItems.Count - 1)
                            {
                                sql_query_EventID += $" or EventID is \"{SelectedItems[i]}\")";
                            }
                            if (i != 0 && i != SelectedItems.Count - 1)
                            {
                                sql_query_EventID += $" or EventID is \"{SelectedItems[i]}\"";
                            }


                        }
                    }
                }

                // Capturamos el MachineName

                string sql_query_MachineName = "";

                SelectedItems.Clear();

                Checked_Items = sfComboBox_MachineName.CheckedItems;

                foreach (var item in Checked_Items)
                {
                    cnt += 1;
                    SelectedItems.Add(item.ToString());

                }




                if (SelectedItems.Count > 0)
                {
                    if (SelectedItems.Count == 1)
                    {
                        sql_query_MachineName = $" and (MachineName is \"{SelectedItems[0]}\")";
                    }
                    else
                    {
                        for (int i = 0; i < SelectedItems.Count; i++)
                        {

                            if (i == 0)
                            {
                                sql_query_MachineName += $" and (MachineName is \"{SelectedItems[0]}\"";
                            }
                            if (i == SelectedItems.Count - 1)
                            {
                                sql_query_MachineName += $" or MachineName is \"{SelectedItems[i]}\")";
                            }
                            if (i != 0 && i != SelectedItems.Count - 1)
                            {
                                sql_query_MachineName += $" or MachineName is \"{SelectedItems[i]}\"";
                            }


                        }
                    }
                }





                // Capturamos el Level

                string sql_query_Level = "";

                SelectedItems.Clear();

                Checked_Items = sfComboBox_Level.CheckedItems;

                foreach (var item in Checked_Items)
                {
                    cnt += 1;
                    SelectedItems.Add(item.ToString());

                }




                if (SelectedItems.Count > 0)
                {
                    if (SelectedItems.Count == 1)
                    {
                        sql_query_Level = $" and (Level is \"{SelectedItems[0]}\")";
                    }
                    else
                    {
                        for (int i = 0; i < SelectedItems.Count; i++)
                        {

                            if (i == 0)
                            {
                                sql_query_Level += $" and (Level is \"{SelectedItems[0]}\"";
                            }
                            if (i == SelectedItems.Count - 1)
                            {
                                sql_query_Level += $" or Level is \"{SelectedItems[i]}\")";
                            }
                            if (i != 0 && i != SelectedItems.Count - 1)
                            {
                                sql_query_Level += $" or Level is \"{SelectedItems[i]}\"";
                            }


                        }
                    }
                }





                // Capturamos el LogName

                string sql_query_LogName = "";

                SelectedItems.Clear();

                Checked_Items = sfComboBox_LogName.CheckedItems;

                foreach (var item in Checked_Items)
                {
                    cnt += 1;
                    SelectedItems.Add(item.ToString());

                }




                if (SelectedItems.Count > 0)
                {
                    if (SelectedItems.Count == 1)
                    {
                        sql_query_LogName = $" and (LogName is \"{SelectedItems[0]}\")";
                    }
                    else
                    {
                        for (int i = 0; i < SelectedItems.Count; i++)
                        {

                            if (i == 0)
                            {
                                sql_query_LogName += $" and (LogName is \"{SelectedItems[0]}\"";
                            }
                            if (i == SelectedItems.Count - 1)
                            {
                                sql_query_LogName += $" or LogName is \"{SelectedItems[i]}\")";
                            }
                            if (i != 0 && i != SelectedItems.Count - 1)
                            {
                                sql_query_LogName += $" or LogName is \"{SelectedItems[i]}\"";
                            }


                        }
                    }
                }



                // Capturamos el Label

                string sql_query_LabelName = "";

                SelectedItems.Clear();

                Checked_Items = sfComboBox_Label.CheckedItems;

                foreach (var item in Checked_Items)
                {
                    cnt += 1;
                    SelectedItems.Add(item.ToString());

                }




                if (SelectedItems.Count > 0)
                {
                    if (SelectedItems.Count == 1)
                    {
                        sql_query_LabelName = $" and (Label is \"{SelectedItems[0]}\")";
                    }
                    else
                    {
                        for (int i = 0; i < SelectedItems.Count; i++)
                        {

                            if (i == 0)
                            {
                                sql_query_LabelName += $" and (Label is \"{SelectedItems[0]}\"";
                            }
                            if (i == SelectedItems.Count - 1)
                            {
                                sql_query_LabelName += $" or Label is \"{SelectedItems[i]}\")";
                            }
                            if (i != 0 && i != SelectedItems.Count - 1)
                            {
                                sql_query_LabelName += $" or Label is \"{SelectedItems[i]}\"";
                            }


                        }
                    }
                }





                //Capturamos la seleccion de EventMessage y EnventMessageXml


                string sql_query_EventMessage = "";

                //string searchTerm;

                //if (textBox_SearchTerm.Text != "")
                if (textBox_SearchTerm.Text != "")
                {
                    if (!textBox_SearchTerm.Text.Contains("\""))
                    {
                        int Count_And = Regex.Matches(textBox_SearchTerm.Text, @"\band\b", RegexOptions.IgnoreCase).Count;

                        int Count_Or = Regex.Matches(textBox_SearchTerm.Text, @"\bor\b", RegexOptions.IgnoreCase).Count;


                        if (Count_And == 0 && Count_Or == 0)
                        {
                            searchTermExists = true;
                            searchTerm = textBox_SearchTerm.Text;
                            sql_query_EventMessage += $" and (EventMessage like \"%{searchTerm}%\" OR EventMessageXML like \"%{searchTerm}%\")";
                            //sql_query_EventMessage += $" and (EventMessage like \"%{searchTerm}%\")";

                        }

                        if (Count_And > 0 || Count_Or > 0)
                        {

                            if (Count_And == 1 && Count_Or == 0)
                            {
                                //MessageBox.Show(" AND ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                search2TermExists = true;
                                searchTerm = textBox_SearchTerm.Text;

                                string[] parts = Regex.Split(searchTerm, @"\band\b", RegexOptions.IgnoreCase);
                                //LogToConsole(parts[0] + parts[1]);
                                string first = searchTerm = parts[0].Trim();
                                string second = searchTerm2 = parts[1].Trim();
                                sql_query_EventMessage += $" and ((EventMessage like \"%{first}%\" AND EventMessage like \"%{second}%\") or (EventMessageXml like \"%{first}%\" AND EventMessageXml like \"%{second}%\"))";
                                //return;
                            }

                            else if (Count_And == 0 && Count_Or == 1)
                            {
                                //MessageBox.Show(" OR ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                search2TermExists = true;
                                searchTerm = textBox_SearchTerm.Text;

                                string[] parts = Regex.Split(searchTerm, @"\bor\b", RegexOptions.IgnoreCase);
                                //LogToConsole(parts[0] + parts[1]);
                                string first = searchTerm = parts[0].Trim();
                                string second = searchTerm2 = parts[1].Trim();
                                sql_query_EventMessage += $" and ((EventMessage like \"%{first}%\" OR EventMessage like \"%{second}%\") or (EventMessageXml like \"%{first}%\" OR EventMessageXml like \"%{second}%\"))";

                                //return;

                            }

                            else
                            {
                                MessageBox.Show("Only one AND o OR please", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;

                            }

                        }
                    }
                    else
                    {

                        MessageBox.Show(" Illegal character \"", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }



                string Full_Sql_Query = "";

                Full_Sql_Query = sql_query_time + sql_query_UserID + sql_query_EventID + sql_query_MachineName + sql_query_Level + sql_query_LogName + sql_query_LabelName
                    + sql_query_EventMessage;

                ShowQueryOnDataGridView(Full_Sql_Query);
                
                //Mostrar el comando sql en la consola
                //LogToConsole(sql_query_time + sql_query_UserID + sql_query_EventID + sql_query_MachineName + sql_query_Level + sql_query_LogName + sql_query_LabelName + sql_query_EventMessage);






                label_status.Text = "Custom Search " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

            }
            else
            {
                MessageBox.Show("Open or create a new case first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }




        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            StartDate = dtpStart.Value;
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            EndDate = dtpEnd.Value;

        }

        private void Btn_ClearSearch_Click(object sender, EventArgs e)
        {
            searchTermExists = false;
            search2TermExists = false;
            //groupBox_CustomSearch.BackColor = System.Drawing.SystemColors.Control;
            sfComboBox_UserID.CheckedItems.Clear(); sfComboBox_UserID.Text = string.Empty;
            sfComboBox_EventID.CheckedItems.Clear(); sfComboBox_EventID.Text = string.Empty;
            sfComboBox_MachineName.CheckedItems.Clear(); sfComboBox_MachineName.Text = string.Empty;
            sfComboBox_Level.CheckedItems.Clear(); sfComboBox_Level.Text = string.Empty;
            sfComboBox_LogName.CheckedItems.Clear(); sfComboBox_LogName.Text = string.Empty;
            sfComboBox_Label.CheckedItems.Clear(); sfComboBox_Label.Text = string.Empty;

            textBox_SearchTerm.Text = "";
            richTextBox_Detail.Text = "";
            if (label_status.Text.Contains("Custom Search "))
            {
                label_status.Text = "";
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
            }

            viewed_users();
            //string query = "SELECT * from logdata;";
            //ShowQueryOnDataGridView(query);
            //label_status.Text = "All Logs " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

        }

        private void Clear_Search()
        {
            searchTermExists = false;
            search2TermExists = false;
            //groupBox_CustomSearch.BackColor = System.Drawing.SystemColors.Control;
            sfComboBox_UserID.CheckedItems.Clear(); sfComboBox_UserID.Text = string.Empty;
            sfComboBox_EventID.CheckedItems.Clear(); sfComboBox_EventID.Text = string.Empty;
            sfComboBox_MachineName.CheckedItems.Clear(); sfComboBox_MachineName.Text = string.Empty;
            sfComboBox_Level.CheckedItems.Clear(); sfComboBox_Level.Text = string.Empty;
            sfComboBox_LogName.CheckedItems.Clear(); sfComboBox_LogName.Text = string.Empty;
            sfComboBox_Label.CheckedItems.Clear(); sfComboBox_Label.Text = string.Empty;
            textBox_SearchTerm.Text = "";
            richTextBox_Detail.Text = "";
        }


        private void btn_AddComment_Click(object sender, EventArgs e)
        {

            Form OpenForm = Application.OpenForms.OfType<Form_Comment>().FirstOrDefault();

            if (OpenForm != null)
            {
                OpenForm.BringToFront();  // Si se encuentra, tráelo al frente
            }
            else
            {



                bool hasColumn = false;

                // Iterar a través de las columnas del DataGridView y verificar que si tiene la columna label ,se reventaba en viewd users por esto.
                foreach (DataGridViewColumn columna in dataGridView1.Columns)
                {
                    if (columna.Name == "Label")
                    {
                        hasColumn = true;
                        break;
                    }
                }



                if (hasColumn)
                {

                    // Verificar si hay al menos una fila seleccionada
                    if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Index < dataGridView1.Rows.Count - 1)
                    {

                        // Obtener la fila seleccionada
                        selectedRow = dataGridView1.SelectedRows[0];

                        // Obtener el valor de la celda en la columna "LogID"
                        string name = selectedRow.Cells["Log_id"].Value.ToString();


                        //LogToConsole(name);
                        Log_id = int.Parse(name);
                        Form_Comment Form_Cmt = new Form_Comment(ConnectionString, Log_id, selectedRow);
                        Form_Cmt.Show();
                    }
                    else
                    {
                        MessageBox.Show("One event must be chosen...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                else
                {
                    MessageBox.Show("One event must be chosen...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void btn_ShowFormLabel_Click(object sender, EventArgs e)
        {
            Form OpenForm = Application.OpenForms.OfType<Form_LabelManager>().FirstOrDefault();

            if (OpenForm != null)
            {
                OpenForm.BringToFront();  // Si se encuentra, tráelo al frente
            }
            else
            {
                Form_LabelManager Form_Label = new Form_LabelManager(ConnectionString, Log_id, selectedRow);
                Form_Label.Show();
                Form_Label.FormClosed += (sender, e) =>
                {
                    // Esta función se ejecutará cuando el formulario se cierre
                    Update_sfComboBox_Label();
                    PaintGridView();
                };
            }

        }

        private void Update_sfComboBox_Label()
        {
            sfComboBox_Label.DataSource = null;

            Load_sfComboBox_Label();
        }

        private void btn_AddLabel_Click(object sender, EventArgs e)
        {

            Form OpenForm = Application.OpenForms.OfType<Form_SetLabel>().FirstOrDefault();


            if (OpenForm != null)
            {
                OpenForm.BringToFront();  // Si se encuentra, tráelo al frente
            }
            else
            {


                List<int> LogsIdList = new List<int>();


                bool hasColumn = false;

                // Iterar a través de las columnas del DataGridView y verificar que si tiene la columna label ,se reventaba en viewd users por esto.
                foreach (DataGridViewColumn columna in dataGridView1.Columns)
                {
                    if (columna.Name == "Label")
                    {
                        hasColumn = true;
                        break;
                    }
                }

                //Tiene la columna de Log_id
                if (hasColumn == true)
                {
                    // Verificar si hay al menos una fila seleccionada

                    if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Index < dataGridView1.Rows.Count - 1)
                    {
                        DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;


                        foreach (DataGridViewRow row in selectedRows)
                        {


                            string logid_tmp = row.Cells["Log_id"].Value.ToString();
                            Log_id = int.Parse(logid_tmp);
                            LogsIdList.Add(Log_id);


                        }
                        // EL foreach de abajo solo para ver que la lista tiene los enteros logid
                        /*foreach (int a in LogsIdList)
                        {
                            textBox2.Text += a;
                        }*/

                        Form_SetLabel Form_sLabel = new Form_SetLabel(ConnectionString, LogsIdList, selectedRows);
                        Form_sLabel.FormClosed += (sender, e) =>
                        {
                            // Esta función se ejecutará cuando el formulario se cierre
                            Update_sfComboBox_Label();
                        };

                        Form_sLabel.Show();
                    }
                    else
                    {
                        
                        MessageBox.Show("One or more events must be chosen...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    
                    MessageBox.Show("One or more events must be chosen...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }



        }

        private void btn_EraseLabel_Click(object sender, EventArgs e)
        {


            //textBox2.Text = string.Empty;
            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;

            List<int> LogsIdList = new List<int>();

            bool hasColumn = false;

            // Iterar a través de las columnas del DataGridView y verificar que si tiene la columna label ,se reventaba en viewd users por esto.
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                if (columna.Name == "Label")
                {
                    hasColumn = true;
                    break;
                }
            }

            if (hasColumn)
            {


                // Verificar si hay al menos una fila seleccionada

                if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Index < dataGridView1.Rows.Count - 1)
                {



                    foreach (DataGridViewRow row in selectedRows)
                    {


                        string logid_tmp = row.Cells["Log_id"].Value.ToString();
                        Log_id = int.Parse(logid_tmp);
                        LogsIdList.Add(Log_id);


                    }


                    using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                    {
                        conn.Open();
                        foreach (int logID in LogsIdList)
                        {

                            string delLabelQuery = $"UPDATE LogData SET Label = NULL WHERE Log_id = {logID}";
                            using (SQLiteCommand cmd = new SQLiteCommand(delLabelQuery, conn))
                            {
                                try
                                {
                                    
                                    cmd.ExecuteNonQuery();
                                    
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                        conn.Close();
                    }

                    foreach (DataGridViewRow row in selectedRows)
                    {
                        row.Cells["Label"].Value = "";
                        row.Cells["Label"].Style.BackColor = Color.White;
                       

                    }
                }

                else
                {
                    MessageBox.Show("One or more events must be chosen...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }

            else
            {
                MessageBox.Show("One or more events must be chosen...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Make_Time_Range_Filter_Click(object sender, EventArgs e)
        {



            bool hasColumn = false;

            // Iterar a través de las columnas del DataGridView y verificar que si tiene la columna label ,se reventaba en viewd users por esto.
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                if (columna.Name == "TimeCreated")
                {
                    hasColumn = true;
                    break;
                }
            }



            DateTime start;
            DateTime end;
            DateTime tmp;
            string stringstart;
            string stringend;

            if (dataGridView1.SelectedRows.Count == 2 && hasColumn == true)
            {
                // Obtén los índices de las filas seleccionadas

                start = DateTime.Parse(dataGridView1.SelectedRows[0].Cells["Timecreated"].Value.ToString());
                end = DateTime.Parse(dataGridView1.SelectedRows[1].Cells["Timecreated"].Value.ToString());


                if (start < end)
                {
                    end = end.AddSeconds(1);
                    stringstart = start.ToString("yyyy-MM-dd HH:mm:ss");
                    stringend = end.ToString("yyyy-MM-dd HH:mm:ss");
                    //Hacer query aqui
                    string query = $"SELECT * from LogData WHERE (TimeCreated >= '{stringstart}' AND TimeCreated <= '{stringend}')";
                    LogToConsole(query);
                    ShowQueryOnDataGridView(query);
                    label_status.Text = "Time Rage Filter " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

                }
                else
                {
                    tmp = start;
                    start = end;
                    end = tmp;
                    end = end.AddSeconds(1);
                    stringstart = start.ToString("yyyy-MM-dd HH:mm:ss");
                    stringend = end.ToString("yyyy-MM-dd HH:mm:ss");
                    string query = $"SELECT * from LogData WHERE (TimeCreated >= '{stringstart}' AND TimeCreated <= '{stringend}')";
                    LogToConsole(query);
                    ShowQueryOnDataGridView(query);
                    label_status.Text = "Time Rage Filter " + " ( " + HitNumber.ToString() + " search hits" + " ) ";
                }

                // Muestra los índices de las filas seleccionadas
                //MessageBox.Show("Índices de las filas seleccionadas: " + index1 + " y " + index2 + "y los timepos fueron " + tmp2 + " y " +  tmp3);
                //textBox2.Text = "Índices de las filas seleccionadas: " + index1 + " y " + index2 + "y los timepos fueron " + tmp2 + " y " + tmp3;
            }
            else
            {
                MessageBox.Show("Two events must be chosen...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btn_savetopdf_Click(object sender, EventArgs e)
        {

            bool hasColumn = false;

            // Iterar a través de las columnas del DataGridView y verificar que si tiene la columna label ,se reventaba en viewd users por esto.
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                if (columna.Name == "Label")
                {
                    hasColumn = true;
                    break;
                }
            }

            Document document = new Document(PageSize.A4, 10, 10, 10, 10);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files |*.pdf";
            saveFileDialog.Title = "Save as PDF";

            if (hasColumn)
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {

                    try
                    {

                        string filename = saveFileDialog.FileName;

                        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));

                        // Abrir el documento para escribir
                        document.Open();

                        // Crear una tabla PDF con el número de columnas especificado (8 en este caso)
                        PdfPTable table = new PdfPTable(8);

                        // Ajustar el ancho de la tabla al 100% del documento
                        table.WidthPercentage = 100;

                        // Añadir las cabeceras de las columnas seleccionadas al PDF
                        for (int i = 2; i <= 9; i++)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(cell);
                        }

                        // Añadir los datos de las filas al PDF
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                for (int i = 2; i <= 9; i++) // Seleccionar desde la columna 2 hasta la 9 (índices 1-8)
                                {
                                    PdfPCell cell = new PdfPCell(new Phrase(row.Cells[i].Value?.ToString()));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    table.AddCell(cell);
                                }
                            }
                        }

                        // Añadir la tabla al documento
                        document.Add(table);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error when exporting to PDF: " + ex.Message);
                    }
                    finally
                    {
                        // Cerrar el documento
                        document.Close();
                    }

                    MessageBox.Show("Export to PDF completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }









            // Crea un nuevo documento PDF
            /*Document doc = new Document();

            try
            {
                // Establece la ubicación del archivo PDF de salida
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivos PDF|*.pdf";
                saveFileDialog.Title = "Guardar como PDF";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Crea un objeto PdfWriter para escribir en el archivo PDF
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

                    // Abre el documento para escribir
                    doc.Open();

                    // Agrega un encabezado
                    doc.Add(new Paragraph("Datos del DataGridView"));

                    // Crea una tabla y configura su formato
                    PdfPTable table = new PdfPTable(dataGridView1.ColumnCount);
                    table.WidthPercentage = 100;

                    // Agrega las cabeceras de las columnas
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        if (column.Name == "Log_id" || column.Name == "itemcount") continue;
                        else
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                            table.AddCell(cell);
                        }
                    }

                    // Agrega las filas y celdas con los datos del DataGridView
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {

                            if (cell.Value != null)
                            {
                                table.AddCell(cell.Value.ToString());
                            }
                            else
                            {
                                table.AddCell(""); // Agrega una celda vacía si cell.Value es nulo
                            }


                        }
                    }

                    // Agrega la tabla al documento
                    doc.Add(table);

                    // Cierra el documento
                    doc.Close();

                    MessageBox.Show("Datos del DataGridView exportados a PDF correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al exportar los datos a PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/

        }

        private void btn_Time_filter_around_Click(object sender, EventArgs e)
        {

            bool hasColumn = false;

            // Iterar a través de las columnas del DataGridView y verificar que si tiene la columna label ,se reventaba en viewd users por esto.
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                if (columna.Name == "TimeCreated")
                {
                    hasColumn = true;
                    break;
                }
            }



            DateTime start;
            DateTime end;
            DateTime tmp;
            string stringstart;
            string stringend;
            int aroundtime;
            aroundtime = int.Parse(numericUpDown_TimeFilterAround.Value.ToString());

            if (dataGridView1.SelectedRows.Count == 1 && hasColumn == true)
            {
                // Obtén los índices de las filas seleccionadas

                tmp = DateTime.Parse(dataGridView1.SelectedRows[0].Cells["Timecreated"].Value.ToString());
                start = tmp.AddMinutes(-aroundtime);
                end = tmp.AddMinutes(aroundtime);



                stringstart = start.ToString("yyyy-MM-dd HH:mm:ss");
                stringend = end.ToString("yyyy-MM-dd HH:mm:ss");
                //Hacer query aqui
                string query = $"SELECT * from LogData WHERE (TimeCreated >= '{stringstart}' AND TimeCreated <= '{stringend}')";
                LogToConsole(query);
                ShowQueryOnDataGridView(query);
                label_status.Text = "Time Around Filter " + " ( " + HitNumber.ToString() + " search hits" + " ) ";

            }
            else
            {
                MessageBox.Show("One event must be chosen...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void numericUpDown_TimeFilterAround_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Close_All()
        {
            ConnectionString = "";
            searchTermExists = false;
            search2TermExists = false;
            textBox_logConsole.Text = string.Empty;
            //progressBar.Value = 0;
            toolStripProgressBar1.Value = 0;

            Viewed_Users = Log_on_Log_off = Failed_to_Log_on = Power_On_Off = Installed_Software = Installed_Service =
    Volume_Shadow_Copy = Create_Local_Users = Deleted_Users = Renamed_Users = General_Interest = All_Logs = User_was_created =
    User_was_enabled = User_chg_pass = User_reset_pass = User_was_disabled = User_was_deleted = User_was_changed = UserAccount =
    Globgrp_Group_Was_Created = Globgrp_Group_Was_Deleted = Globgrp_Group_Was_Changed = Globgrp_AMember_Was_Added = Globgrp_AMember_Was_Removed =
    Sec_Enbl_Glob_Grp = LocalGrp_Group_Was_Created = LocalGrp_Group_Was_Deleted = LocalGrp_Group_Was_Changed = LocalGrp_AMember_Was_Added = LocalGrp_AMember_Was_Removed =
    LocalGrp_Group_Was_Enumerated = Sec_Enbl_Local_Grp = UnivGrp_Group_Was_Created = UnivGrp_Group_Was_Deleted = UnivGrp_Group_Was_Changed = UnivGrp_AMember_Was_Added =
    UnivGrp_AMember_Was_Removed = Sec_Enbl_Univ_Grp = Computer_Account_Was_Created = Computer_Account_Was_Deleted = Computer_Account_Was_Changed = Computer_Account =
    Network_Share_Accessed = Network_Share_Added = Network_Share_Modified = Network_Share_Deleted = Network_Share_Checked = Network_Share =
    Scheduled_Task_Created = Scheduled_Task_Updated = Scheduled_Task_Deleted = Scheduled_Task_Executed = Scheduled_Task_Completed =
    Scheduled_Task_Activity = Obj_Scheduled_Task_Created = Obj_Scheduled_Task_Deleted = Obj_Scheduled_Task_Enabled = Obj_Scheduled_Task_Disabled = Obj_Scheduled_Task_Updated =
    Object_Access_Auditing = Handle_toAn_Object_Requested = Registry_Value_Modified = Handle_toAn_Object_Closed = Object_Was_Deleted = Attempt_Made_to_Acc_Object =
    Object_Handle_Event = Event_Log_Service_Started = Event_Log_Service_Stoped = Service_Terminated_Unexpectedly = Service_Start_Stop = Start_Type_For_Service_Changed =
    Service_Installed_bySystem = Auditing_Windows_Services = Wlan_Successfully_Connected = Wlan_Failed_To_Connect = Wlan_Connection =
    Block_App_Accept_Incom_Conn = Bloked_a_Packet = Permited_Incommig_Conn = Allowed_Connection = Blocked_Connection = Permited_Bind_a_Local_Port =
    Block_Bind_a_Local_Port = Windows_Filtering_Platform = Found_Malware = Action_Protect_System = Failed_Action_Protect_System = Deleted_History_Malware = Detected_Suspicious_Behavior = Detected_Malware =
    Critical_Error_In_Action_On_Malware = Real_Time_Protection_Disabled = Real_Time_Protection_Cfg_Changed = Platform_Cfg_Changed = Scanning_Malware_Disabled =
    Scanning_Virus_Disabled = Windows_Defender = Process_Creation = Proc_Chng_File_Creation_Time = Network_Connection = Sysmon_Service_State_Changed = Process_Terminated = Driver_Loaded = Image_Loaded =
    Create_Remote_Thread = Raw_Access_Read = Process_Access = File_Create = Reg_Key_Value_Created_Deleted = Registry_Value_Modification = Registry_Key_Value_Renamed =
    File_Create_Stream_Hash = Sysmon_Configuration_Change = Named_Pipe_Created = Named_Pipe_Connected = WMIEventFilter_Activity_Detected = WMIEventConsumer_Activity_Detected =
    WMIEventConsumerToFilter_Activity_Detected = DNS_Query_Event = Sysmon_Error = Sysmon = HitNumber = Terminal_Server_Log_On = Terminal_Server_Shell_Start = Terminal_Server_Log_Off =
    Terminal_Server_Session_Disconnected = Terminal_Server_Session_Reconnection = Terminal_Server_Session_Disconnected_Code = Terminal_Server_Session_Disconnected_by_Session = Terminal_Server = 0;

            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

            label_status.Text = "Please create a workspace or open an existing one.";
            //LogToConsole("Ready to create a workspace or open an existing one");
            this.Text = "Quick Log";
            Clear_Search();
            disable_buttons();
            AcordeonMenu();



        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close_All();
            LogToConsole("Closing Workspace...");
        }

        private void btn_savetocsv_Click(object sender, EventArgs e)
        {

            
                // Establece la ubicación del archivo CSV de salida
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Save to CSV |*.csv";
                saveFileDialog.Title = "Save as CSV";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {

                        string filePath = saveFileDialog.FileName;
                        string filename = "";
                        using (StreamWriter file = new StreamWriter(filePath))
                        {
                            // Escribir las cabeceras de las columnas
                            string[] columnNames = dataGridView1.Columns
                                                      .Cast<DataGridViewColumn>()
                                                      .Select(column => "\"" + column.HeaderText + "\"")
                                                      .ToArray();

                            string header = string.Join(",", columnNames);
                            file.WriteLine(header);

                            // Escribir los datos de cada fila
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (!row.IsNewRow) // Ignorar la fila de nueva entrada en un DataGridView editable
                                {
                                    string[] cells = row.Cells
                                                        .Cast<DataGridViewCell>()
                                                        .Select(cell => "\"" + cell.Value?.ToString() + "\"")
                                                        .ToArray();

                                    string line = string.Join(",", cells);
                                    file.WriteLine(line);
                                }
                            }
                        }

                        MessageBox.Show("Data exported to CSV correctly.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred when exporting the data to CSV: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }

        }

        private void Btn_GridFill_Click(object sender, EventArgs e)
        {



            // Alternar el estado del AutoSizeColumnsMode
            if (dataGridView1.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                Btn_GridFill.Text = "Off";  // Cambiar el texto del botón a "Off"
            }
            else
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Btn_GridFill.Text = "On";  // Cambiar el texto del botón a "On"
            }

        }

        private void btn_ShowCellToolTips_Click(object sender, EventArgs e)
        {


            if (dataGridView1.ShowCellToolTips)
            {
                dataGridView1.ShowCellToolTips = false;
                btn_ShowCellToolTips.Text = "Off";  // Cambiar el texto del botón a "Tooltips Off"
            }
            else
            {
                dataGridView1.ShowCellToolTips = true;
                btn_ShowCellToolTips.Text = "On";  // Cambiar el texto del botón a "Tooltips On"
            }





        }

        private void sfComboBox_LogName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_About_Click(object sender, EventArgs e)
        {

            Form OpenForm = Application.OpenForms.OfType<About_Form>().FirstOrDefault();

            if (OpenForm != null)
            {
                OpenForm.BringToFront();  // Si se encuentra, tráelo al frente
            }
            else
            {
                // Si no, crea una nueva instancia y muéstrala

                About_Form Form_about = new About_Form();
                Form_about.Show();
            }


            //About_Form Form_about = new About_Form();
            //Form_about.Show();





        }

        private void label_about_Click(object sender, EventArgs e)
        {


            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.internet-solutions.com.co/",
                UseShellExecute = true
            });


        }

        private void btn_ShowFontDialog_Click(object sender, EventArgs e)
        {

            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox_Detail.SelectAll();
                richTextBox_Detail.SelectionFont = fontDialog1.Font;
                richTextBox_Detail.DeselectAll();

                // Establecer la nueva fuente como predeterminada para cualquier texto nuevo
                richTextBox_Detail.Font = fontDialog1.Font;
            }

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
