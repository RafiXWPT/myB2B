﻿using System.Threading.Tasks;

namespace MyB2B.Web.Infrastructure.Actions.Commands
{
    public interface IAsyncCommandHandler<in TCommand>
        where TCommand : Command
    {
        Task ExecuteAsync(TCommand command);
    }
}