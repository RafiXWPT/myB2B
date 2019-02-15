using System;
using System.Collections.Generic;
using System.Text;

namespace myB2B.Web.Infrastructure.Actions.Commands
{
    public interface ICommandHandler<in TCommand>
        where TCommand : Command
    {
        void Execute(TCommand command);
    }
}
