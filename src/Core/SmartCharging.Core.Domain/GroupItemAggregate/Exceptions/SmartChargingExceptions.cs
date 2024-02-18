using SmartCharging.Core.Domain.Commons;

namespace SmartCharging.Core.Domain.GroupItemAggregate.Exceptions;
public static class SmartChargingExceptions
{
    public class InvalidEntityState : Exception
    {
        public IEnumerable<Error> Errors { get; set; }

        public InvalidEntityState(string error) =>
            Errors = new[] { new Error(error) };
    }

    public class RecordNotFound : Exception
    {
        public IEnumerable<Error> Errors { get; set; }

        public RecordNotFound(string error) =>
            Errors = new[] { new Error(error) };
    }
}
