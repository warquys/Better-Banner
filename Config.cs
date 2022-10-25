using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace Better_Server_Banner
{
    public class Config : IConfig
    {
        [Description("↓Indicates whether the plugin is enabled or not")]
        public bool IsEnabled { get; set; } = true;

        [Description("↓Indicates the massage for the specifide Team")]
        public string ByDefault { get; set; } = string.Empty;
        public Dictionary<Team, string> TeamMessage { get; set; } = new Dictionary<Team, string>() 
        {
            { Team.MTF, "<color=red>Respect your superiors</color>" },
            { Team.CDP, "you have the right to kill other Class D" }
        };
    }
}
