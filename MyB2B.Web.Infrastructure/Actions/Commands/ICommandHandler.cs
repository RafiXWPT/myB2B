namespace MyB2B.Web.Infrastructure.Actions.Commands
{
    public interface ICommandHandler<in TCommand>
        where TCommand : CommandBase
    {
        void Execute(TCommand command);
    }
}
