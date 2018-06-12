using System;
using System.Diagnostics;
using System.IO;
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
            string message =
                "An unknown error occured which lead to a shutdown of the Doodler application.\n" +
                $"A log file has been generated under {Preferences.ConfigDir}.\n" +
                $"Error message: {e.Exception.Message}";

            Console.WriteLine(message); // write to stdout
            Console.Error.WriteLine(message); // write to stderr

            try
            {
                File.WriteAllText(Statics.Preferences.LogFile, $"Critical error: {e.Exception.Message}\n{e.Exception.StackTrace}");
                MessageBox.Show(message, "Unknown error", MessageBoxButton.OK, MessageBoxImage.Error);
            } catch
            {
                // can't display anything UI related anymore
            }
            Shutdown(1); // shutdown app with error code 1
            Process.GetCurrentProcess().Kill();
        }
    }
}