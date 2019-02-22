using System;
using System.Runtime.Serialization;

namespace MyB2B.Web.Infrastructure.Actions.Queries
{
    [Serializable]
    public class QueryHandlerNotFoundException : Exception
    {
        public QueryHandlerNotFoundException(Type handlerType, bool isAsync) : base($"{(isAsync ? "Async" : string.Empty)}QueryHandler for query: '{handlerType.FullName}' not found.")
        {
        }

        protected QueryHandlerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
