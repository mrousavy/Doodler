
using System;

namespace DoodlerCore
{
    // TODO: Docs
    public class Vote
    {
        public int Id { get; set; }

        public User User { get; set; }
        public Poll Poll { get; set; }
        public Answer Answer { get; set; }

        public Vote(User user, Poll poll, Answer answer)
        {
            User = user;
            Poll = poll;
            Answer = answer;
        }
        public Vote()
        {
        }
    }
}
