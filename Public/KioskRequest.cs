using System;
using Newtonsoft.Json;


namespace LAVI.QueueManager
{
    public class KioskRequest
    {

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("branchId")]
        public string branchId;

        [JsonProperty("queueId")]
        public string queueId;

        [JsonProperty("serviceId")]
        public string serviceId;

        [JsonProperty("pk")]
        public string pk;

        [JsonProperty("type")]
        public string type;

        [JsonProperty("workflowId")]
        public string workflowId;

        [JsonProperty("requestTimeUTC")]
        public DateTime requestTimeUTC;

        [JsonProperty("sortPosition")]
        public int sortPosition;

        [JsonProperty("ticketNumber")]
        public string ticketNumber;
        [JsonProperty("workflow")]
        public Workflow workflow;

        [JsonProperty("languageId")]
        public string languageId;

        [JsonProperty("preServiceQuestions")]
        public PreServiceQuestion[] preServiceQuestions;

        [JsonProperty("serviceQuestions")]
        public ServiceQuestion[] serviceQuestions;

        [JsonProperty("requestTimeUTCString")]
        public string requestTimeUTCString;

        [JsonProperty("averageWaitTimeAsPerServiceAtTimeOfRequestRegistration")]
        public int averageWaitTimeAsPerServiceAtTimeOfRequestRegistration;

        [JsonProperty("appointmentTimeAndCheckInTimeDifferenceInMinutes")]
        public int appointmentTimeAndCheckInTimeDifferenceInMinutes;

        [JsonProperty("companyId")]
        public string companyId;

        [JsonProperty("requestSourceId")]
        public string requestSourceId;

        [JsonProperty("requestSourceType")]
        public string requestSourceType;

        [JsonProperty("selectedLanguageId")]
        public string selectedLanguageId;

        [JsonProperty("customerState")]
        public CustomerState customerState;

        [JsonProperty("groups")]
        public string[] groups;

        [JsonProperty("isWalking")]
        public bool isWalking;

        [JsonProperty("isAppointment")]
        public bool isAppointment;

        [JsonProperty("isCalled")]
        public bool isCalled;

        [JsonProperty("calledById")]
        public string calledById;

        [JsonProperty("calledByName")]
        public string calledByName;

        [JsonProperty("calledAtDesk")]
        public string calledAtDesk;

        [JsonProperty("calledCount")]
        public int calledCount;

        [JsonProperty("calledRequeueCount")]
        public int calledRequeueCount;

        [JsonProperty("calledLog")]
        public CalledDetails calledLog;

        [JsonProperty("ruleActionNotificationDetails")]
        public RuleActionNotificationDetails ruleActionNotificationDetails;

        [JsonProperty("estimatedServingTimeUTCString")]
        public string estimatedServingTimeUTCString;

        [JsonProperty("estimatedWaitRangesSettings")]
        public IncomingWorkflowEstimateWaitSettings estimatedWaitRangesSettings;

        [JsonProperty("isTransferred")]
        public bool isTransferred;

        [JsonProperty("isDeleted")]
        public bool isDeleted;

        [JsonProperty("queue")]
        public Queue queue;

        [JsonProperty("personName")]
        public string personName;

        [JsonProperty("mobileInterfaceId")]
        public string mobileInterfaceId;

        [JsonProperty("actionHistory")]
        public ActionHistory[] actionHistory;

        [JsonProperty("appointmentTimeUTCString")]
        public string appointmentTimeUTCString;

        [JsonProperty("appointmentId")]
        public string appointmentId;

        [JsonProperty("servingAgentId")]
        public string servingAgentId;

        [JsonProperty("ttl")]
        public int ttl;

        [JsonProperty("transferredFromRequestId")]
        public string transferredFromRequestId;

        [JsonProperty("lastSMSMessage")]
        public string lastSMSMessage;

        [JsonProperty("lastNote")]
        public string lastNote;

        [JsonProperty("servingAgentName")]
        public string servingAgentName;

        [JsonProperty("servingStartTimeUTC")]
        public string servingStartTimeUTC;

        [JsonProperty("servingAtDesk")]
        public string servingAtDesk;

        [JsonProperty("_etag")]
        public string _etag;

    }
    public partial class PreServiceQuestion
    {

        [JsonProperty("questionId")]
        public string questionId;

        [JsonProperty("questionText")]
        public string questionText;

        [JsonProperty("answer")]
        public object answer;

        [JsonProperty("questionType")]
        public string questionType;

    }
    public partial class Workflow
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("pk")]
        public string Pk;

        [JsonProperty("workFlowId")]
        public string WorkFlowId;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("companyId")]
        public string companyId;

        [JsonProperty("isPublished")]
        public bool IsPublished;
        [JsonProperty("estimateWaitSettings")]
        public IncomingWorkflowEstimateWaitSettings estimateWaitSettings { get; set; }
        [JsonProperty("services")]
        public Services[] services { get; set; }
        [JsonProperty("queue")]
        public Queue[] queue { get; set; }
        [JsonProperty("createdAt")]
        public string CreatedAt;

        [JsonProperty("updatedAt")]
        public string UpdatedAt;

        [JsonProperty("createdBy")]
        public string CreatedBy;

        [JsonProperty("updatedBy")]
        public string UpdatedBy;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("isDeleted")]
        public bool IsDeleted;

        [JsonProperty("defaultPriority")]
        public int DefaultPriority;

        [JsonProperty("enablePriority")]
        public bool EnablePriority;

    }
    public partial class Services
    {
        [JsonProperty("ServiceId")]
        public string ServiceId { get; set; }
        [JsonProperty("averageWaitTime")]
        public int? averageWaitTime { get; set; }

    }

    public partial class Queue
    {

        [JsonProperty("id")]
        public string id;

        [JsonProperty("name")]
        public string name;

        [JsonProperty("numberingRule")]
        public NumberingRule numberingRule;

        [JsonProperty("averageWaitTime")]
        public int? averageWaitTime;

        [JsonProperty("isDeleted")]
        public bool? isDeleted;

        [JsonProperty("priority")]
        public int? priority;

    }

    public partial class RuleActionNotificationDetails
    {

        [JsonProperty("color")]
        public object color;

        [JsonProperty("customerEmail")]
        public string customerEmail;

        [JsonProperty("customerSMS")]
        public string customerSMS;

    }
    public partial class ActionHistory
    {

        [JsonProperty("actionHistoryId")]
        public string actionHistoryId;

        [JsonProperty("action")]
        public string action;

        [JsonProperty("actionDateTimeUTC")]
        public string actionDateTimeUTC;

        [JsonProperty("triggerType")]
        public int triggerType;
        [JsonProperty("agentId")]
        public string agentId;

    }
    public partial class CalledDetails
    {

        [JsonProperty("calledById")]
        public string calledById;

        [JsonProperty("calledByName")]
        public string calledByName;

        [JsonProperty("calledAtDesk")]
        public string calledAtDesk;

        [JsonProperty("calledTimeUTCString")]
        public string calledTimeUTCString;

    }

    public partial class NumberingRule
    {

        [JsonProperty("prefix")]
        public string prefix;

        [JsonProperty("middlefix")]
        public string middlefix;

        [JsonProperty("postfix")]
        public string postfix;

    }
    public partial class ServiceQuestion
    {

        [JsonProperty("questionId")]
        public string questionId;

        [JsonProperty("answer")]
        public string answer;

        [JsonProperty("questionType")]
        public string questionType;

    }
    public enum CustomerState
    {
        WAITING,
        CALLED,
        SERVING,
        SERVED,
        CANCELEDBYAGENT,
        CANCELEDBYUSER
    }



}