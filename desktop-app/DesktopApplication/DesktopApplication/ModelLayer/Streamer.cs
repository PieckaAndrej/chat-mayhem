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
        public string AccessToken { get; set; }
        public string Id { get; set; }
        public string RefreshToken { get; set; }

        public Streamer(string accessToken, string id, string refreshToken)
        {
            AccessToken = accessToken;
            Id = id;
            RefreshToken = refreshToken;
        }

        public override bool Equals(object? obj)
        {
            return obj is Streamer streamer &&
                   AccessToken == streamer.AccessToken &&
                   Id == streamer.Id &&
                   RefreshToken == streamer.RefreshToken;
        }
    }
}
