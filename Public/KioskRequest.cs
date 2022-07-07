using Newtonsoft.Json;


namespace LAVI.QueueManager
{
    public class KioskRequest
    {

        [JsonProperty("queueId")]
        public string queueId { get; set; }

        [JsonProperty("branchId")]
        public string branchId { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("pk")]
        public string pk { get; set; }
        [JsonProperty("ticketNumber")]
        public string ticketNumber { get; set; }

        [JsonProperty("workflow")]
        public Workflow workflow { get; set; }

        [JsonProperty("companyId")]
        public string companyId { get; set; }
        
    }
    public partial class Workflow
    {
        [JsonProperty("workFlowId")]
        public string workFlowId { get; set; }
        [JsonProperty("incomingWorkflowEstimateWaitSettings")]
        public IncomingWorkflowEstimateWaitSettings incomingWorkflowEstimateWaitSettings { get; set; }
        [JsonProperty("services")]
        public Services services { get; set; }
        
    }
    public partial class Services
    {
        [JsonProperty("ServiceId")]
        public string ServiceId { get; set; }
        [JsonProperty("averageWaitTime")]
        public int averageWaitTime { get; set; }
        
    }
}