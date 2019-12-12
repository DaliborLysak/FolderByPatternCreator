using System;

namespace FolderByPatternCreator
{
    public class SequenceDefinitionException : Exception
    {
        public SequenceDefinitionException()
        {
        }

        public SequenceDefinitionException(string message)
            : base(message)
        {
        }

        public SequenceDefinitionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}