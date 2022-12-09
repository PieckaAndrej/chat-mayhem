using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.ModelLayer
{
    internal class Team
    {
        public string Name { get; set; }

        public List<Streamer> Streamers { get; set; }

        public Team(string name, List<Streamer> streamers)
        {
            Name = name;
            Streamers = streamers;
        }
    }
}
