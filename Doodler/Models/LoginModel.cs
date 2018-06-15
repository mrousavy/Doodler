using System.Data.SqlClient;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Doodler.Models
{
    /// <summary>
    ///     mrousavy: Model for logging in to the Doodler system
    /// </summary>
    public class LoginModel
    {
        public async Task<bool> TryLoginAsync(string email, string password)
        {
            try
            {
                using (var service = Statics.NewService())
                {
                    await Statics.CreateDatabaseAsync(service);
                    Statics.CurrentUser = await service.LoginAsync(email, password);
                    await service.SaveAsync();
                    return true;
                }
            } catch (InvalidCredentialException)
            {
                return false;
            } catch (SqlException)
            {
                return false;
            }
        }
    }
}