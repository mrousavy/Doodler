using System;

namespace DoodlerCore.Exceptions
{
    // TODO: Docs
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException(object id): base($"The object with the ID {id} could not be found!")
        { }
    }
}
