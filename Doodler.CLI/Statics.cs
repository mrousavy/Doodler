using DoodlerCore;
using System.Threading.Tasks;

namespace Doodler.CLI
{
    public static class Statics
    {
        public static Preferences Preferences { get; } = Preferences.Load();

        public static string Database => Preferences.Database;
        public static string Server => Preferences.Server;
        public static string Username => Preferences.Username;

        // TODO: BAD RAW PASSWORD STRING
        public static string Password => Preferences.Password;

        public static string LastEmail => Preferences.LastEmail;
        public static string LastPassword => Preferences.LastPassword;

        public static User CurrentUser { get; set; }

        public static async Task<User> LoginUser()
        {
            if (CurrentUser == null)
            {
                using (var service = NewService())
                {
                    CurrentUser = await service.LoginAsync(LastEmail, LastPassword);
                }
            }
            return CurrentUser;
        }

        public static IDataService NewService() => new DataService(Database, Server, Username, Password);
        public static Task CreateDatabaseAsync(IDataService service) => service.EnsureCreatedAsync();
    }
}