using Data.ModelLayer;
using System.Text.Json.Serialization;
using static MongoDB.Driver.WriteConcern;

namespace WebApp.Models
{
    public record Game
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("streamer")]
        public object Streamer { get; set; }

        [JsonPropertyName("mode")]
        public GameMode Mode { get; set; }

        [JsonPropertyName("timeLimit")]
        public int TimeLimit { get; set; }

        [JsonPropertyName("questionPack")]
        public QuestionPack QuestionPack { get; set; }
    }
}
