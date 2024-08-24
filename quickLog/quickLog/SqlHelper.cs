using System.Data.SQLite;

namespace quickLog
{
    internal class SqlHelper
    {
        private const string setPragma = "PRAGMA foreign_keys = ON;";

        private const string createTableLabels = @"
                        CREATE TABLE IF NOT EXISTS Labels (
                        Label_name TEXT PRIMARY KEY NOT NULL,
                        Label_color TEXT
                        )";


        private const string createTableQuery = @"
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

        private const string createTableResults = @"
                        CREATE TABLE IF NOT EXISTS Results (
                        Result TEXT PRIMARY KEY NOT NULL,
                        Result_Value INTEGER
                        )";

        private readonly IQuickLogLogger _logger;
        public void CreateDatabase(string ConnectionString)
        {
            using SQLiteConnection connection = new(ConnectionString);
            connection.Open();

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

        private void LogToConsole(string message) => _logger.LogToConsole(message);

        public SqlHelper(IQuickLogLogger logger)
        {
            _logger = logger;
        }
    }
}
