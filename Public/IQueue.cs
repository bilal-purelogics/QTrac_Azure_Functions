using Newtonsoft.Json;


namespace LAVI.QueueManager
{
    public class IQueue
    {

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("pk")]
        public string pk { get; set; }

        [JsonProperty("customers")]
        public IQueuedCustomer[] customers { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("estimatedWaitRangesSettings")]
        public estimatedWaitRangesSettings estimatedWaitRangesSettings { get; set; }

    }

    public partial class IQueuedCustomer
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("serviceId")]
        public string serviceId { get; set; }

        [JsonProperty("queueId")]
        public string queueId { get; set; }

        [JsonProperty("customerState")]
        public CustomerStateInQueue customerState { get; set; }

        [JsonProperty("estimateWaitTimeISOString")]
        public string estimateWaitTimeISOString { get; set; }
    }

    public partial class estimatedWaitRangesSettings
    {
        [JsonProperty("allowCalculateEstimateWaitTime")]
        public bool allowCalculateEstimateWaitTime { get; set; }

        [JsonProperty("isIncludeAppointmentsIntoConsiderations")]
        public bool isIncludeAppointmentsIntoConsiderations { get; set; }

        [JsonProperty("defaultRange")]
        public string defaultRange { get; set; }

        [JsonProperty("idleTimeBetweenServices")]
        public int idleTimeBetweenServices { get; set; }
        [JsonProperty("estimateWaitSettings")]
        public EstimateWaitTimeMessageRange estimateWaitSettings { get; set; }
    }

    public enum CustomerStateInQueue
    {
        WAITING = 0,
        CALLED = 1,
    }


}