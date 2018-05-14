using System;

namespace DoodlerCore.Exceptions
{
    // TODO: Docs
    public class PollNotFoundException : Exception
    {
        public PollNotFoundException(Guid id) : base($"The poll with the id {id} could not be found!")
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}