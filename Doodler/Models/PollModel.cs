using Doodler.Implementation;
using DoodlerCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doodler.Models
{
    public class PollModel
    {

        public async Task VoteAsync(Poll poll, Answer answer)
        {
            using (var service = Statics.NewService())
            {
                await service.VoteOnPoll(Statics.CurrentUser, poll, answer);
                await service.SaveAsync();
            }
        }

        public async Task<IList<Vote>> GetVotesAsync(Poll poll)
        {
            using (var service = Statics.NewService())
            {
                return await service.GetVotesForPollAsync(poll);
            }
        }

        public async Task<IList<Answer>> GetAnswersAsync(Poll poll)
        {
            using (var service = Statics.NewService())
            {
                return await service.GetAnswersForPollAsync(poll);
            }
        }

        public class AnswerWrapper : ViewModelBase
        {
            private object _value;
            private bool _selected;
            private Answer _answer;
            private int _votes;

            public int Votes
            {
                get => _votes;
                set => Set(ref _votes, value);
            }
            public Answer Answer
            {
                get => _answer;
                set => Set(ref _answer, value);
            }
            public bool Selected
            {
                get => _selected;
                set => Set(ref _selected, value);
            }
            public object Value
            {
                get => _value;
                set => Set(ref _value, value);
            }


            public AnswerWrapper(Answer answer, int votes, bool selected)
            {
                Answer = answer;
                switch (answer)
                {
                    case TextAnswer textAnswer:
                        Value = textAnswer.Text;
                        break;
                    case DateAnswer dateAnswer:
                        Value = dateAnswer.Date;
                        break;
                    default:
                        throw new ArgumentException(nameof(answer));
                }

                if (votes < 0)
                    throw new ArgumentException(nameof(votes));
                Votes = votes;

                Selected = selected;
            }
        }
    }
}
