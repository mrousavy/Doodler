using System;

namespace DoodlerCore
{
    /// <summary>
    ///     mrousavy: A possible answer to a poll containing a date
    /// </summary>
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
        public DateAnswer(DateTime date)
        {
            Date = date;
        }
        public DateAnswer()
        {
        }
    }
}
