using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    public class Streamer
    {

        public int Id { get; set; }
        public string OAuth { get; set; }

        public Streamer(string OAuth)
        {
            this.OAuth = OAuth;
        }

        public Streamer(int id, string OAuth) : this(OAuth)
        {
            Id = id;
        }
    }
}
