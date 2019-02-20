using System;
using System.Collections.Generic;
using System.Text;

namespace myB2B.Domain.Identity
{
    public class ApplicationRight : ApplicationEntity
    {
        public string Symbol { get; set; }
        public string DisplayName { get; set; }
    }
}
