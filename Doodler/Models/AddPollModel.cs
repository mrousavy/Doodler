using DoodlerCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doodler.Models
{
    /// <summary>
    ///     mrousavy: Model for Adding a Poll
    /// </summary>
    public class AddPollModel
    {
        public async Task CreatePollAsync<TAnswer>(User creator, string title, DateTime endDate,
            IEnumerable<TAnswer> answers)
            where TAnswer : Answer
        {
            using (var service = Statics.NewService())
            {
                await service.CreatePollAsync(creator, title, endDate, answers);
                await service.SaveAsync();
            }
        }
    }
}