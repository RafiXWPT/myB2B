using MyB2B.Domain.Identity;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Queries;
using MyB2B.Web.Infrastructure.Controllers;

namespace MyB2B.Web.Controllers.Logic
{
    public interface IControllerLogic { }

    public abstract class ControllerLogic : IControllerLogic
    {
        protected ICommandProcessor CommandProcessor { get; }
        protected IQueryProcessor QueryProcessor { get; }   
        protected IApplicationPrincipal CurrentPrincipal { get; private set; }

        protected ControllerLogic(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            CommandProcessor = commandProcessor;
            QueryProcessor = queryProcessor;
        }

        public void SetPrincipal(IApplicationPrincipal principal)
        {
            CurrentPrincipal = principal;
        }
    }

    public abstract class LogicController<TLogic> : BaseController where TLogic: ControllerLogic 
    {
        protected TLogic Logic { get; }

        protected LogicController(TLogic logic)
        {
            Logic = logic;
            Logic.SetPrincipal(User);
        }
    }
}
