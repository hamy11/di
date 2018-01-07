using System;

namespace TagsCloudVisualisation
{
    public class ConsoleErrorHandler : IErrorHandler
    {
        public void Log(string error)
        {
            Console.WriteLine(error);
        }
    }
}