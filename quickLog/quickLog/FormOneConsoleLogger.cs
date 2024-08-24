namespace quickLog
{
    internal class FormOneConsoleLogger : IQuickLogLogger
    {
        private readonly FormMain _formMain;
        public void LogToConsole(string message)
        {
            _formMain.LogToConsole(message);
        }
        public FormOneConsoleLogger(FormMain formMain)
        {
            _formMain = formMain;
        }
    }
}
