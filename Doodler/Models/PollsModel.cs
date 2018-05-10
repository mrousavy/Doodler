using DoodlerCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}
