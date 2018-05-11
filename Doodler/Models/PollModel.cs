using Doodler.Implementation;
using DoodlerCore;
using System;
using System.Threading.Tasks;

namespace Doodler.Models
{
    public class PollModel
    {
        public Poll Poll { get; }

        public PollModel(Poll poll)
        {
            Poll = poll;
        }

        public async Task VoteAsync(Answer answer)
        {
            using (var service = Statics.NewService())
            {
                await service.VoteOnPoll(Statics.CurrentUser, Poll, answer);
                await service.SaveAsync();
            }
        }

        public class AnswerWrapper : ViewModelBase
        {
            private object _value;
            private bool _selected;

            private Answer _answer;

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


            public AnswerWrapper(Answer answer)
            {
                Answer = answer;
                if (answer is TextAnswer textAnswer)
                    Value = textAnswer.Text;
                else if (answer is DateAnswer dateAnswer)
                    Value = dateAnswer.Date;
                else
                    throw new ArgumentException(nameof(answer));
            }
        }
    }
}
