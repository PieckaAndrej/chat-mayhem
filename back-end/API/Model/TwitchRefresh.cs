﻿using System.Text.Json.Serialization;

namespace API.Model
{
    public record TwitchRefresh
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("scope")]
        public List<string>? Scope { get; set; }

        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
    }
}
