using Newtonsoft.Json;


namespace LAVI.QueueManager
{
    public class Agent
    {

        [JsonProperty("agentId")]
        public string agentId { get; set; }

        [JsonProperty("companyId")]
        public string companyId { get; set; }

        [JsonProperty("queueIds")]
        public string[] queueIds { get; set; }

        [JsonProperty("busyTillTimeInMilliseconds")]
        public int busyTillTimeInMilliseconds { get; set; }

        [JsonProperty("customersInServing")]
        public KioskRequest[] customersInServing { get; set; }
        [JsonProperty("customerToBeServed")]
        public KioskRequest[] customerToBeServed { get; set; }

    }
}

