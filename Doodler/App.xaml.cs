using System.Windows;

namespace Doodler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void AppExit(object sender, ExitEventArgs e)
        {
            Preferences.Save(Statics.Preferences);
        }
    }
}
