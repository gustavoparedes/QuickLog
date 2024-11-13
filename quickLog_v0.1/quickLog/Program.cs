namespace quickLog
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            string filePath = args.Length > 0 ? args[0] : null;
            Application.Run(new FormMain(filePath));
       

        }
    }
}