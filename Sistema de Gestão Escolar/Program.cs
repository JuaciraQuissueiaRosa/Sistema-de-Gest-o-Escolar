

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

         

            ApplicationConfiguration.Initialize();
            gestorEscola = new GestorEscola(); // ✅ Carrega os dados ao iniciar

            Application.Run(new FormPrincipal());
        }
    }
}