
namespace DoodlerCore
{
    // TODO: Docs
    public class TextAnswer : Answer
    {
        public string Text { get; set; }

        public TextAnswer(Poll poll, string text) : base(poll)
        {
            Text = text;
        }
        public TextAnswer(Poll poll) : base(poll)
        {
        }
        public TextAnswer()
        {
        }
    }
}
