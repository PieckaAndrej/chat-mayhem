using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public override bool Equals(object? obj)
        {
            return obj is Streamer streamer &&
                   Id == streamer.Id &&
                   OAuth == streamer.OAuth;
        }
    }
}
