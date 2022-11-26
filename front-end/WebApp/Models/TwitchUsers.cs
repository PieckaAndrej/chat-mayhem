using System.Text.Json.Serialization;

namespace WebApp.Models
{
    public record UsersData
    {
        [JsonPropertyName("id")]
        public string? Id;

        [JsonPropertyName("login")]
        public string? Login;

        [JsonPropertyName("display_name")]
        public string? DisplayName;

        [JsonPropertyName("type")]
        public string? Type;

        [JsonPropertyName("broadcaster_type")]
        public string? BroadcasterType;

        [JsonPropertyName("description")]
        public string? Description;

        [JsonPropertyName("profile_image_url")]
        public string? ProfileImageUrl;

        [JsonPropertyName("offline_image_url")]
        public string? OfflineImageUrl;

        [JsonPropertyName("view_count")]
        public int? ViewCount;

        [JsonPropertyName("email")]
        public string? Email;

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt;
    }

    public record TwitchUsers
    {
        [JsonPropertyName("data")]
        public UsersData[]? Data;
    }
}
