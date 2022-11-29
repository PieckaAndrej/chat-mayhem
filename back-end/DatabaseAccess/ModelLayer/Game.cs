using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    public class Game
    {
        public int Id { get; set; }
        public Streamer? Streamer { get; set; }
        public GameMode? Mode { get; set; }
        public int TimeLimit { get; set; }
        public QuestionPack? QuestionPack { get; set; }

        public Game() { }

        public Game(Streamer streamer, GameMode mode, int timeLimit, QuestionPack questionPack)
        {
            Streamer = streamer;
            Mode = mode;
            TimeLimit = timeLimit;
            QuestionPack = questionPack;
        }

        public Game(Streamer streamer, GameMode mode, int timeLimit,
            QuestionPack questionPack, int id)
            : this(streamer, mode, timeLimit, questionPack)
        {
            Id = id;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (System.Reflection.PropertyInfo property in this.GetType().GetProperties())
            {
                sb.Append(property.Name);
                sb.Append(": ");
                if (property.GetIndexParameters().Length > 0)
                {
                    sb.Append("Indexed Property cannot be used");
                }
                else
                {
                    sb.Append(property.GetValue(this, null));
                }

                sb.Append(System.Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
