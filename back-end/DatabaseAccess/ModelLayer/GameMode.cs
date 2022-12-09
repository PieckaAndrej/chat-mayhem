using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    public class GameMode
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }

        public GameMode(string description, string rules)
        {
            Description = description;
            Rules = rules;
        }

        public GameMode(int id, string description, string rules) : this(description, rules)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is GameMode mode &&
                   Id == mode.Id &&
                   Description == mode.Description &&
                   Rules == mode.Rules;
        }
    }
}
