using Snake.Core.Exceptions;

namespace Snake.Application.Exceptions
{
    public class InvalidJsonDeserializationException : SnakeException
    {
        public string FileName { get; private set; }

        public InvalidJsonDeserializationException(string fileName) : base($"Data in {fileName} file is invalid.")
        {
            FileName = fileName;
        }

    }
}
