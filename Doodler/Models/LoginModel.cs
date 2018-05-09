using System.Security.Authentication;
using System.Threading.Tasks;

namespace Doodler.Models
{
    public class LoginModel
    {
        public async Task<bool> TryLoginAsync(string email, string password)
        {
            using (var service = Statics.NewService())
            {
                try
                {
                    var user = await service.LoginAsync(email, password);
                    return true;
                } catch (InvalidCredentialException)
                {
                    return false;
                }
            }
        }
    }
}
