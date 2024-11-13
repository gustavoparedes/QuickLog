
using System.Data.SQLite;


namespace quickLog
{
    public partial class Form_Comment : Form
    {

        private string connectionString;
        private int l_id;
        DataGridViewRow sRow;

        public Form_Comment(string connectionStr, int Log_id, DataGridViewRow selectedRow)
        {
            InitializeComponent();
            connectionString = connectionStr;
            l_id = Log_id;
            sRow = selectedRow;
        }

        private void Form_Comment_Load(object sender, EventArgs e)
        {

        }

        private void btn_SaveComment_Click(object sender, EventArgs e)
        {

            var connection = new SQLiteConnection(connectionString);
            //textBox1.Text =  connectionString + Environment.NewLine + l_id;
            string newComment = textBox_Comments.Text;
            string updatequiery = $"UPDATE LogData SET Comment = '{newComment}' WHERE Log_id = {l_id}";
            //color = row.Cells["Label_color"].Value.ToString();

            try
            {
                if (string.IsNullOrEmpty(newComment)) { newComment = ""; }
                using (SQLiteCommand cmd = new SQLiteCommand(updatequiery, connection))
                {

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sRow.Cells["Comment"].Value = newComment;


        }
    }
}
