namespace Stackular.EmployeeManagement.Application.Exceptions
{
    public class DependencyFailureException : Exception
    {
        public DependencyFailureException() { }

        public DependencyFailureException(string message) : base(message) { }

        public DependencyFailureException(string message, Exception? innerException) : base(message, innerException) { }
    }
}
