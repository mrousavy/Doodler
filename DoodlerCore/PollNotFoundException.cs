using System;

namespace DoodlerCore
{
    public class PollNotFoundException : Exception
    {
        public Guid Id { get; set; }

        public PollNotFoundException(Guid id) : base($"The poll with the id {id} could not be found!")
        {
            Id = id;
        }
    }
}
