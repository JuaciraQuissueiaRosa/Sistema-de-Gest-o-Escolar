using Syncfusion.Licensing;

namespace Sistema_de_Gestão_Escolar
{
    internal static class Program
    {

        public static GestorEscola gestorEscola;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            // Caminho absoluto para o arquivo de licença
            string licenseFilePath = @"C:\POO\Sistema de Gestão Escolar\Sistema de Gestão Escolar\License\SyncfusionLisence.txt";

            // Registra a licença a partir do arquivo de licença
            SyncfusionLicenseProvider.RegisterLicense(File.ReadAllText(licenseFilePath).Trim());

            ApplicationConfiguration.Initialize();
            gestorEscola = new GestorEscola(); // ✅ Carrega os dados ao iniciar

            Application.Run(new FormPrincipal());
        }
    }
}