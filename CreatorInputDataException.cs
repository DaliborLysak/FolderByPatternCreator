using System;

namespace FolderByPatternCreator
{
    public class CreatorInputDataException : Exception
    {
        public CreatorInputDataException()
        {
        }

        public CreatorInputDataException(string message)
            : base(message)
        {
        }

        public CreatorInputDataException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}