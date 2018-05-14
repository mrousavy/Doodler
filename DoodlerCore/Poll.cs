using System;

namespace DoodlerCore
{
    // TODO: Docs
    public class Poll
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndsAt { get; set; }
    
        public User Creator { get; set; }
    }
}
