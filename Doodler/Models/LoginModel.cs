using System.Data.SqlClient;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Doodler.Models
{
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