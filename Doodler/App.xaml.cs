using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace Doodler
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void AppExit(object sender, ExitEventArgs e)
        {
            Preferences.Save(Statics.Preferences);
        }

        private void AppError(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //Logger.Critical(e.Exception);
            string message =
                "An unknown error occured which lead to a shutdown of the Doodler application.\n" +
                $"A log file has been generated under {Preferences.ConfigDir}.";

            try
            {
                MessageBox.Show(message, "Unknown error", MessageBoxButton.OK, MessageBoxImage.Error);
            } catch
            {
                // can't display anything UI related anymore
            }

            Console.WriteLine(message); // write to stdout
            Console.Error.WriteLine(message); // write to stderr
            Shutdown(1); // shutdown app with error code 1
            Process.GetCurrentProcess().Kill();
        }
    }
}