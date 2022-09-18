using Snake.Core.Exceptions;

namespace Snake.Application.Exceptions
{
    public class SaveFileDontExistException : SnakeException
    {
        public string FileName { get; set; }
        public SaveFileDontExistException(string fileName) : base($"File {fileName} don't exist.")
        {
            FileName = fileName;
        }
    }
}
