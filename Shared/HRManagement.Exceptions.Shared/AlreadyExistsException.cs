namespace HRManagement.Exceptions.Shared
{
    public class AlreadyExistsException:Exception
    {
        public AlreadyExistsException(string message) : base(message) { }
    }
}
