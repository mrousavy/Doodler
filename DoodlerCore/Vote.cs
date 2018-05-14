
namespace DoodlerCore
{
    // TODO: Docs
    public class Vote
    {
        public int Id { get; set; }
    
        public User User { get; set; }
        public Answer Answer { get; set; }
        public Poll Poll { get; set; }
    }
}
