using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Domain.Exceptions
{
    public class InvalidTravelDaysException : PackItException
    {
        public int Days { get; }

        public InvalidTravelDaysException(int days) : base($"Value '{days}' is invalid travel days.") => Days = days;
    }
}