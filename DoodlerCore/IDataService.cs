using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoodlerCore
{
    public interface IDataService
    {
        #region User
        Task<User> LoginAsync(string email, string password);
        Task<User> RegisterAsync(string email, string name, string password);
        Task DeleteUserAsync(User user);
        Task EditUserAsync(User user);
        Task<IList<User>> GetAllUsersAsync();
        Task<IList<User>> GetAllUsersForPollAsync(Poll poll);
        #endregion

        #region Poll
        Task<Poll> CreatePollAsync(string name);
        Task<Poll> DeletePollAsync();
        Task<IList<Poll>> GetAllPollsAsync();
        Task<IList<Poll>> GetAllPollsForUserAsync(User user);
        #endregion
    }
}
