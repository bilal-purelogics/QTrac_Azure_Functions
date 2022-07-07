using Newtonsoft.Json;


namespace LAVI.QueueManager
{
    public class IncomingWorkflowEstimateWaitSettings
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

    public partial class EstimateWaitTimeMessageRange
    {

        [JsonProperty("from")]
        public int from { get; set; }

        [JsonProperty("to")]
        public int to { get; set; }

        [JsonProperty("messages")]
        public Translations messages { get; set; }
    }

    public partial class Translations
    {
        [JsonProperty("languageId")]
        public string languageId { get; set; }

        [JsonProperty("text")]
        public string text { get; set; }
    }
}