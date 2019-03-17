using System;
using System.Collections.Generic;
using System.Text;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Queries;

namespace MyB2B.Web.Controllers.Logic
{
    public abstract class ControllerLogic
    {
        protected ICommandProcessor CommandProcessor { get; }
        protected IQueryProcessor QueryProcessor { get; }

        protected ControllerLogic(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            CommandProcessor = commandProcessor;
            QueryProcessor = queryProcessor;
        }
    }
}
