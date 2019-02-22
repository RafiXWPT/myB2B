namespace MyB2B.Web.Infrastructure.Actions.Commands
{
    public abstract class Command
    {
        public ActionResult<object> Output { get; set; } = ActionResult<object>.Done();
    }
}
