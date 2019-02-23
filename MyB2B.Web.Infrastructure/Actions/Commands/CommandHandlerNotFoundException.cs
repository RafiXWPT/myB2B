using System;
using System.Runtime.Serialization;

namespace MyB2B.Web.Infrastructure.Actions.Commands
{
    [Serializable]
    public class CommandHandlerNotFoundException : Exception
    {
        public CommandHandlerNotFoundException(Type handlerType, bool isAsync) : base($"{(isAsync ? "Async" : string.Empty)}CommandHandler for command: '{handlerType.FullName}' not found.")
        {
        }

        protected CommandHandlerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
