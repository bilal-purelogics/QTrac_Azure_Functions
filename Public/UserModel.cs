using Newtonsoft.Json;


namespace LAVI.QueueManager
{
    public class UserModel
    {

        [JsonProperty("pk")]
        public string pk { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("companyId")]
        public string companyId { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("roleId")]
        public string roleId { get; set; }

        [JsonProperty("isOverride")]
        public bool isOverride { get; set; }

        [JsonProperty("isOnlineAsAgent")]
        public bool isOnlineAsAgent { get; set; }

        [JsonProperty("agentDeskSettings")]
        public agentDeskSettings agentDeskSettings { get; set; }

    }

    public partial class agentDeskSettings
    {
        [JsonProperty("branchId")]
        public string branchId { get; set; }

        [JsonProperty("deskName")]
        public string deskName { get; set; }

        [JsonProperty("templateId")]
        public string templateId { get; set; }

        [JsonProperty("loginAs")]
        public string loginAs { get; set; }
    }
}