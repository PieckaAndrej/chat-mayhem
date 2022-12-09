﻿using System.Text;
using System.Text.Json.Serialization;

namespace WebApp.Models
{
    public record TwitchValidate
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("scopes")]
        public List<string> Scopes { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
