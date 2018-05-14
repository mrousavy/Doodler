using System.Collections.Generic;
using System.Threading.Tasks;
using DoodlerCore;

namespace Doodler.Models
{
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