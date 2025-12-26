using System;
using System.Windows;

namespace RegionConfigApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Globaler Exception Handler
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                var exception = args.ExceptionObject as Exception;
                MessageBox.Show($"Ein unerwarteter Fehler ist aufgetreten: {exception?.Message}", 
                    "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            };
        }
    }
}
