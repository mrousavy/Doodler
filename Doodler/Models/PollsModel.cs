using DoodlerCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doodler.Models
{
    /// <summary>
    ///     mrousavy: Model for viewing all polls this user has access to
    /// </summary>
    public class PollsModel
    {
        public async Task<IEnumerable<Poll>> GetAllPollsAsync()
        {
            using (var service = Statics.NewService())
            {
                return await service.GetAllPollsAsync();
            }
        }

        public async Task<int> GetUsersCountAsync()
        {
            using (var service = Statics.NewService())
            {
                return await service.GetUsersCountAsync();
            }
        }
    }
}