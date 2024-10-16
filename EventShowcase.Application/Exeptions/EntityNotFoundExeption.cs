namespace EventShowcase.Application.Exeptions
{
    public class EntityNotFoundExeption : Exception
    {
        public EntityNotFoundExeption()
        {
        }

        public EntityNotFoundExeption(string? message) : base(message)
        {
        }

        public EntityNotFoundExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
