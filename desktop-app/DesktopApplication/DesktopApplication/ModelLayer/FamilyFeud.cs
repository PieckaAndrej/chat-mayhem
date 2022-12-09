using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.ModelLayer
{
    internal class FamilyFeud : GameMode
    {
        public List<string> teams { get; set; }

        public string currentTeam { get; set; }

        public int teamPoints { get; set; }

        public FamilyFeud (List<string> teams, string currentTeam, int teamPoints, int id, string description, string rules) : base(id, description, rules)
        {
            this.teams = teams;
            this.currentTeam = currentTeam;
            this.teamPoints = teamPoints;
        }
    }
}
