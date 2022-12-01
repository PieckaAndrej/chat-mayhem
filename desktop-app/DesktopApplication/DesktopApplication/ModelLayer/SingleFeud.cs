using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    internal class SingleFeud : GameMode
    {
        public int points { get; set; }

        public SingleFeud (int points, int id, string description, string rules) : base(id, description, rules)
        {
            this.points = points;
        }   
    }
}
