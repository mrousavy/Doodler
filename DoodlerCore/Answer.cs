namespace DoodlerCore
{
    // TODO: Docs
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
