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
        public string roleId { get; set; }
        [JsonProperty("roleName")]
        public string roleName { get; set; }
        [JsonProperty("homeInterfaceId")]
        public string homeInterfaceId { get; set; }
        [JsonProperty("roleAction")]
        public RoleAction[] roleAction { get; set; }
        [JsonProperty("agentTemplates")]
        public AgentTemplateLookup[] agentTemplates { get; set; }
        [JsonProperty("createdAt")]
        public string createdAt { get; set; }
        [JsonProperty("createdBy")]
        public string createdBy { get; set; }
        [JsonProperty("updatedAt")]
        public string updatedAt { get; set; }
        [JsonProperty("updatedBy")]
        public string updatedBy { get; set; }
    }

    public partial class RoleAction
    {

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("IsActive")]
        public bool? IsActive;

        [JsonProperty("actionTypeName")]
        public string ActionTypeName;

        [JsonProperty("action")]
        public UserRoleAction[] Action;

        [JsonProperty("id")]
        public string Id;

    }

    public partial class UserRoleAction
    {

        [JsonProperty("actionName")]
        public string ActionName;

        [JsonProperty("viewName")]
        public string ViewName;

        [JsonProperty("addEdit")]
        public bool? AddEdit;

        [JsonProperty("view")]
        public bool? View;

        [JsonProperty("delete")]
        public bool? Delete;

        [JsonProperty("run")]
        public bool? Run;

    }

    public class AgentTemplateLookup
    {

        [JsonProperty("agentId")]
        public string AgentId;

        [JsonProperty("queues")]
        public string[] Queues;

    }



}