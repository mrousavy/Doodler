using DoodlerCore;
using System.Threading.Tasks;

namespace Doodler.Models
{
    public class RegisterModel
    {
        public async Task<bool> TryRegisterAsync(string email, string username, string password)
        {
            using (var service = Statics.NewService())
            {
                try
                {
                    await Statics.CreateDatabaseAsync(service);
                    var user = await service.RegisterAsync(email, username, password);
                    return true;
                } catch (EmailExistsException)
                {
                    return false;
                }
            }
        }
    }
}
