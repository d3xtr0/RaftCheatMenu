using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaftCheatMenu.Overwrites
{
    class Weather : WeatherManager
    {
        protected override void Start()
        {
            base.Start();
            RCM.Cheat._WeatherConnection = this.weatherConnections;
        }
    }
}
