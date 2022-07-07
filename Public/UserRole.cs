using Newtonsoft.Json;


namespace LAVI.QueueManager
{
    public class UserRole
    {

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("pk")]
        public string pk { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("companyId")]
        public string companyId { get; set; }
        
        [JsonProperty("roleId")]
        public object roleId { get; set; }
        [JsonProperty("roleName")]
        public string roleName { get; set; }
    }
}