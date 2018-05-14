using System;

namespace DoodlerCore
{
    // TODO: Docs
    public class DateAnswer : Answer
    {
        public DateTime Date { get; set; }

        public DateAnswer(Poll poll, DateTime date) : base(poll)
        {
            Date = date;
        }
        public DateAnswer(Poll poll) : base(poll)
        {
        }
        public DateAnswer()
        {
        }
    }
}
