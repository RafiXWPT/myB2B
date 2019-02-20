using System;
using System.Collections.Generic;
using System.Text;

namespace myB2B.Web.Infrastructure.Actions.Commands
{
    public abstract class Command
    {
        public ActionResult<object> Output { get; set; } = ActionResult<object>.Done();
    }
}
