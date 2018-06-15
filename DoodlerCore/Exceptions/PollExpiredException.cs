using System;
namespace DoodlerCore.Exceptions
{
    public class PollExpiredException : Exception
    {
        public PollExpiredException(DateTime dateTime):
            base($"The Poll expired at {dateTime: dd.MM.yyyy H:mm}")
        { }
    }
}