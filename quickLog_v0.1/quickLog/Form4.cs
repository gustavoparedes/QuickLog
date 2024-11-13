using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quickLog
{
    public partial class Form_SetLabel : Form
    {


        private string connectionString;
        private List<int> l_idlist;
        private DataGridViewSelectedRowCollection sRows;
        private string labelName;
        private string labelColor;

        public Form_SetLabel(string connectionStr, List<int> logidlist, DataGridViewSelectedRowCollection sectRows)
        {
            InitializeComponent();
            connectionString = connectionStr;
            l_idlist = logidlist;
            sRows = sectRows;
        }

        private void Form_SetLabel_Load(object sender, EventArgs e)
        {
            LoadLabelsFromDataBase();

        }

        private void LoadLabelsFromDataBase()
        {

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Este codigo viene de el formulario principal donde ya habia una valriable datatable, por eslo la tuve q declarar abajo pq no existe
                DataTable dataTable = new DataTable();

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

        private void btn_SetLabel_Click(object sender, EventArgs e)
        {


            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Index < dataGridView1.Rows.Count - 1)
            {

                // Obtener la fila seleccionada
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];


                // Obtener el valor de la celda en la columna "Label_name" que sera la forma de obntener el registro de la etiqueta con ese nombre unico
                string label_name = selectedRow.Cells["Label_name"].Value.ToString();
                string query = $"SELECT * from Labels WHERE Label_name is '{label_name}'";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // Crear un comando SQL
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        try
                        {
                            // Ejecutar la consulta y leer el resultado
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Obtener los valores de los campos

                                    labelName = reader.GetString(0);     // label_name
                                    labelColor = reader.GetString(1);    // label_color
                                    if (labelName.Contains('\'')) { MessageBox.Show("Contiene caracter no soportado"); }
                                    conn.Close();

                                    // Hacer algo con los valores obtenidos
                                    //textBox1.Text = $"label_name: {labelName}, label_color: {labelColor}";

                                }
                                /*else
                                {
                                    conn.Close();
                                }*/

                            }
                        }

                        catch (SQLiteException ex)
                        {
                            MessageBox.Show("Error SQLite: " + ex.Message);
                            // Considera loguear el valor de 'query' aquí para revisarlo
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error General: " + ex.Message);
                        }

                    }
                    conn.Close();
                }

                using (SQLiteConnection conn2 = new SQLiteConnection(connectionString))
                {
                    conn2.Open();
                    foreach (int logID in l_idlist)
                    {

                        string setLabelQuery = $"UPDATE LogData SET Label = '{label_name}' WHERE Log_id = {logID}";
                        using (SQLiteCommand cmd = new SQLiteCommand(setLabelQuery, conn2))
                        {
                            try
                            {
                                //conn2.Open();
                                cmd.ExecuteNonQuery();
                                //conn2.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    conn2.Close();
                }

            }


            foreach (DataGridViewRow row in sRows)
            {
                row.Cells["Label"].Value = labelName;
                int colorValue = Convert.ToInt32(labelColor);
                Color color = Color.FromArgb(colorValue);
                row.Cells["Label"].Style.BackColor = color;
                //textBox1.Text += row.Cells["Log_id"].Value.ToString() + Environment.NewLine;
            }


        }
    }
}
