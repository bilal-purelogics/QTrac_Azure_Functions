using Newtonsoft.Json;


namespace LAVI.QueueManager
{
    public class CustomerRequest
    {

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("branchId")]
        public string branchId { get; set; }

        [JsonProperty("workflowId")]
        public string workflowId { get; set; }

        [JsonProperty("serviceId")]
        public string serviceId { get; set; }

        [JsonProperty("queueId")]
        public string queueId { get; set; }

        [JsonProperty("DocumentType")]
        public string DocumentType { get; set; }
        [JsonProperty("servingAgentId")]
        public string servingAgentId { get; set; }
    }
}