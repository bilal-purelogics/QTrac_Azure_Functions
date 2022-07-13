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
        [JsonProperty("servingAgentId")]
        public string servingAgentId { get; set; }
        
    }
    public partial class Workflow
    {
        [JsonProperty("workFlowId")]
        public string workFlowId { get; set; }
        [JsonProperty("incomingWorkflowEstimateWaitSettings")]
        public IncomingWorkflowEstimateWaitSettings incomingWorkflowEstimateWaitSettings { get; set; }
        [JsonProperty("services")]
        public Services services { get; set; }
        [JsonProperty("queue")]
        public Queue[] queue { get; set; }
        
    }
    public partial class Services
    {
        [JsonProperty("ServiceId")]
        public string ServiceId { get; set; }
        [JsonProperty("averageWaitTime")]
        public int averageWaitTime { get; set; }
        
    }

    public class Queue {
    
        [JsonProperty("id")]
        public string Id;
        
        [JsonProperty("name")]
        public string Name;
        
        [JsonProperty("numberingRule")]
        public NumberingRule NumberingRule;
        
        [JsonProperty("averageWaitTime")]
        public int AverageWaitTime;
        
        [JsonProperty("isDeleted")]
        public bool IsDeleted;
        
        [JsonProperty("priority")]
        public int Priority;
    
    }
    
    public class NumberingRule {

        [JsonProperty("prefix")]
        public string Prefix;

        [JsonProperty("middlefix")]
        public string Middlefix;

        [JsonProperty("postfix")]
        public string Postfix;

    }
    
}