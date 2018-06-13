
namespace DoodlerCore
{
    /// <summary>
    ///     mrousavy: A possible answer to a poll containing a string/text
    /// </summary>
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
        public TextAnswer(string text)
        {
            Text = text;
        }
        public TextAnswer()
        {
        }
    }
}
