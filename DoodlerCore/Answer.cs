namespace DoodlerCore
{
    /// <summary>
    ///     mrousavy: A possible answer to a poll (derived: see <see cref="TextAnswer"/> and <see cref="DateAnswer"/>)
    /// </summary>
    public abstract class Answer
    {
        public int Id { get; set; }

        public Poll Poll { get; set; }

        protected Answer(Poll poll)
        {
            Poll = poll;
        }

        protected Answer()
        {
        }
    }
}
