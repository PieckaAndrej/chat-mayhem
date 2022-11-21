﻿using Newtonsoft.Json;
using System.Text;

namespace WebApp.Models
{
    public class TwitchValidate
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("scopes")]
        public List<string> Scopes { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

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
