
using System.Data;
using System.Data.SQLite;


namespace quickLog
{
    public partial class Form_LabelManager : Form
    {

        private DataTable dataTable;
        private string connectionString;
        private int l_id;
        private DataGridViewRow sRow;

        public Form_LabelManager(string connectionStr, int Log_id, DataGridViewRow selectedRow)
        {
            InitializeComponent();
            InitializeDataGridView();
            connectionString = connectionStr;
            l_id = Log_id;
            sRow = selectedRow;
            //textBox_Labels.Text += l_id;
        }

        private void InitializeDataGridView()
        {

            dataGridView1.CellClick += dataGridView1_CellClick;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si se hizo clic en la columna "Seleccionar Color"
            if (e.ColumnIndex == dataGridView1.Columns["Label_color"].Index && e.RowIndex >= 0)
            {
                // Mostrar el cuadro de diálogo de selección de color
                ColorDialog colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtener el color seleccionado
                    Color selectedColor = colorDialog.Color;

                    //string colorHex = ColorTranslator.ToHtml(selectedColor);
                    int argbValue = selectedColor.ToArgb();
                    // Actualizar el valor y color de la celda correspondiente
                    dataGridView1.Rows[e.RowIndex].Cells["Label_color"].Value = argbValue;
                    dataGridView1.Rows[e.RowIndex].Cells["Label_color"].Style.BackColor = selectedColor;
                }
            }
        }


        private void Form_LabelManager_Load(object sender, EventArgs e)
        {

            LoadLabelsFromDataBase();

        }

        private void LoadLabelsFromDataBase()
        {

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Crear un adaptador de datos para ejecutar la consulta y llenar un DataTable
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT Label_name, Label_color FROM Labels", connection))
                {
                    if (dataTable == null)
                    {
                        dataTable = new DataTable();
                    }

                    // Llenar el DataTable con los resultados de la consulta
                    dataAdapter.Fill(dataTable);

                    // Asignar el DataTable como DataSource del DataGridView
                    //dataGridView1.DataSource = dataTable;
                    dataGridView1.Columns.Add("Label_name", "Name");
                    dataGridView1.Columns.Add("Label_color", "Color");
                    //dataGridView1.Columns["Label_name"].HeaderText = "Name";
                    //dataGridView1.Columns["Label_color"].HeaderText = "Color";
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Obtener los valores de cada columna en la fila
                        string labelName = row["Label_name"].ToString();
                        int colorValue = Convert.ToInt32(row["Label_color"]);
                        Color color = Color.FromArgb(colorValue);

                        // Agregar una nueva fila al DataGridView
                        int rowIndex = dataGridView1.Rows.Add(labelName, "");

                        // Establecer el color de fondo de la celda directamente
                        dataGridView1.Rows[rowIndex].Cells["Label_color"].Style.BackColor = color;
                    }

                }
            }

            //Esta funcion sirve pero no pone el color de fondo a la columna color

            /*using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Crear un adaptador de datos para ejecutar la consulta y llenar un DataTable
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT Label_name, Label_color FROM Labels", connection))
                {
                    if (dataTable == null)
                    {
                        dataTable = new DataTable();
                    }

                    // Llenar el DataTable con los resultados de la consulta
                    dataAdapter.Fill(dataTable);

                    // Asignar el DataTable como DataSource del DataGridView
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.Columns["Label_name"].HeaderText = "Name";
                    dataGridView1.Columns["Label_color"].HeaderText = "Color";
                }
            }*/
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            // Verificar si hay al menos una fila seleccionada
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Index < dataGridView1.Rows.Count - 1)
            {

                // Obtener la fila seleccionada
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Obtener el valor de la celda en la columna "Name"
                string name = selectedRow.Cells["Label_name"].Value.ToString();

                // Puedes mostrar un mensaje de confirmación o proceder directamente al borrado
                DialogResult result = MessageBox.Show($"Surely you want to delete the record with the name '{name}'?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Realizar la operación de borrado aquí
                    deleteLabel(name);
                }
            }
        }

        private void deleteLabel(string name)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM Labels WHERE Label_name = @name";
                    command.Parameters.AddWithValue("@name", name);

                    // Ejecutar la consulta de borrado
                    command.ExecuteNonQuery();
                    command.CommandText = $"UPDATE LogData SET Label = NULL WHERE Label = @Label";
                    command.Parameters.AddWithValue("@Label", name);
                    command.ExecuteNonQuery();
                    // Volver a cargar los datos en el DataGridView si es necesario
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();
                    dataTable.Clear();
                    dataTable.Columns.Clear();
                    LoadLabelsFromDataBase();
                }

                connection.Close();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {


            string name = "";
            string color = "";
            string insertQuery = @"INSERT OR IGNORE INTO Labels (Label_name, Label_color) VALUES (@Label_name, @Label_color)";


            var connection = new SQLiteConnection(connectionString);
            connection.Open();



            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (row.IsNewRow || row.Cells["Label_name"] == null || row.Cells["Label_name"].Value == null || string.IsNullOrEmpty(row.Cells["Label_name"].Value.ToString()))

                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.Columns.Clear();
                        dataGridView1.Rows.Clear();
                        dataTable.Clear();
                        dataTable.Columns.Clear();
                        LoadLabelsFromDataBase();
                        return;
                    }


                    name = row.Cells["Label_name"].Value.ToString();

                    if (row.IsNewRow || row.Cells["Label_color"] == null || row.Cells["Label_color"].Value == null || string.IsNullOrEmpty(row.Cells["Label_color"].Value.ToString()))
                    {
                        color = "-1";
                    }
                    else
                    {
                        color = row.Cells["Label_color"].Value.ToString();
                    }

                    //color = row.Cells["Label_color"].Value.ToString();

                    using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                    {
                        // Se debe cambiar TimeGenerated DATETIME a TimeCreated
                        command.Parameters.AddWithValue("@Label_name", name);
                        command.Parameters.AddWithValue("@Label_color", color);
                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataTable.Clear();
            dataTable.Columns.Clear();

            LoadLabelsFromDataBase();

            /*try 
            { 
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {


                    if (row.Cells["Label_name"].Value != null && row.Cells["Label_color"].Value != null &&
                    !string.IsNullOrEmpty(row.Cells["Label_name"].Value.ToString()) &&
                    row.Cells["Label_color"].Value.ToString().Trim() != "")
                    {

                        object Label = row.Cells["Label_name"].Value;
                        object selectedColor = row.Cells["Label_color"].Value;
                        name = Label.ToString();
                        string getColor = selectedColor.ToString();
                        if (getColor.Contains("Select"))
                         { 
                            color = "White";
                            textBox1.Text += name + " " + color + Environment.NewLine;
                         }
                        //continue;
                        if (getColor.Contains("A="))
                        {
                            textBox1.Text += name + " " + ParseColorString(row.Cells["Label_color"].Value.ToString()) + Environment.NewLine;
                            color = ParseColorString(row.Cells["Label_color"].Value.ToString());
                        }
                        //Con un if y la condicion se va el error en que cae aqui    
                        if (getColor.Contains("[") && !getColor.Contains("A="))
                        {
                            textBox1.Text += name + " " + GetColorName(row.Cells["Label_color"].Value.ToString()) + Environment.NewLine;
                            color = GetColorName(row.Cells["Label_color"].Value.ToString());
                        }

                        using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                        {
                            // Se debe cambiar TimeGenerated DATETIME a TimeCreated
                            command.Parameters.AddWithValue("@Label_name", name);
                            command.Parameters.AddWithValue("@Label_color", color);                       
                            command.ExecuteNonQuery();
                        }


                    }

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }*/






        }
    }
}
