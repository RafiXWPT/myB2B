using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace myB2B.Web.Infrastructure.Actions.Queries
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
