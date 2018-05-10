using System.Threading.Tasks;
using DoodlerCore;

namespace Doodler
{
    public static class Statics
    {
        public static Preferences Preferences { get; } = Preferences.Load();

        public static string Database => Preferences.Database;
        public static string Server => Preferences.Server;
        public static string Username => Preferences.Username;

        // TODO: BAD RAW PASSWORD STORING
        public static string Password => Preferences.Password;

        public static string LastEmail => Preferences.LastEmail;
        public static string LastPassword => Preferences.LastPassword;

        public static IDataService NewService() => new DataService(Database, Server, Username, Password);
        public static Task CreateDatabaseAsync(IDataService service) => Task.Run(() => service.Create());
    }
}
