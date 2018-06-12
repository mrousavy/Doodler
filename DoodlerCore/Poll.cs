using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


        public Poll(string title, User creator, DateTime endsAt)
        {
            Title = title;
            Creator = creator;
            CreatedAt = DateTime.Now;
            EndsAt = endsAt;
        }
        public Poll()
        {
            CreatedAt = DateTime.Now;
        }

    }
}
