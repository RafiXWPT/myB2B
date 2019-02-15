using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace myB2B.Web.Infrastructure.Actions.Commands
{
    public interface ICommandHandler<in TCommand>
        where TCommand : Command
    {
        void Execute(TCommand command);
        Task ExecuteAsync(TCommand command);
    }
}
