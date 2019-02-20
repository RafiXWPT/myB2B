using System;
using System.Collections.Generic;
using System.Text;

namespace myB2B.Domain.Identity
{
    public class ApplicationRole : ApplicationEntity
    {
        public string Symbol { get; private set; }
        public string DisplayName { get; private set; }

        public List<ApplicationRight> Rights { get; private set; }

        public static ApplicationRole Create(string symbol, string displayName)
        {
            return new ApplicationRole {Symbol = symbol, DisplayName = displayName};
        }

        public void SetRights(List<ApplicationRight> newRights)
        {
            Rights.Clear();
            Rights = newRights;
        }
    }
}
