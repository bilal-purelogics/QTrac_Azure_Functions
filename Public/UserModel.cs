using Newtonsoft.Json;


namespace LAVI.QueueManager
{
    public class UserModel {
    
       [JsonProperty("pk")]
       public string Pk;
       
       [JsonProperty("type")]
       public string Type;
       
       [JsonProperty("companyId")]
       public string CompanyId;
       
       [JsonProperty("id")]
       public string Id;
       
       [JsonProperty("firstName")]
       public string FirstName;
       
       [JsonProperty("lastName")]
       public string LastName;
       
       [JsonProperty("profileImageUrl")]
       public string ProfileImageUrl;
       
       [JsonProperty("displayName")]
       public string DisplayName;
       
       [JsonProperty("email")]
       public string Email;
       
       [JsonProperty("alertEmail")]
       public string AlertEmail;
       
       [JsonProperty("alertPhoneNumber")]
       public string AlertPhoneNumber;
       
       [JsonProperty("roleId")]
       public string RoleId;
       
       [JsonProperty("password")]
       public string Password;
       
       [JsonProperty("userType")]
       public userType UserType;
       
       [JsonProperty("branches")]
       public UserBranchTag Branches;
       
       [JsonProperty("tags")]
       public string[] Tags;
       
       [JsonProperty("isUserInactive")]
       public bool? IsUserInactive;
       
       [JsonProperty("changePasswordInFirstLogin")]
       public bool? ChangePasswordInFirstLogin;
       
       [JsonProperty("isAllBranches")]
       public bool? IsAllBranches;
       
       [JsonProperty("isOverride")]
       public bool? IsOverride;
       
       [JsonProperty("agentTemplate")]
       public AgentTemplate AgentTemplate;
       
       [JsonProperty("acceptSMS")]
       public bool? AcceptSMS;
       
       [JsonProperty("mfaEnabled")]
       public bool? MfaEnabled;
       
       [JsonProperty("createdAt")]
       public string CreatedAt;
       
       [JsonProperty("createdBy")]
       public string CreatedBy;
       
       [JsonProperty("updatedAt")]
       public string UpdatedAt;
       
       [JsonProperty("updatedBy")]
       public string UpdatedBy;
       
       [JsonProperty("isOnlineAsAgent")]
       public bool? IsOnlineAsAgent;
       
       [JsonProperty("agentDeskSettings")]
       public AgentDeskSettings AgentDeskSettings;
       
       [JsonProperty("isSignedIn")]
       public bool? IsSignedIn;
       
    }
    
    public partial class AgentDeskSettings
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
    public partial class userType
    {
        [JsonProperty("value")]
        public string value { get; set; }
        [JsonProperty("text")]
        public string text { get; set; }
    }
    
    public partial class lookup {

        [JsonProperty("value")]
        public string Value;

        [JsonProperty("text")]
        public string Text;

    }
    
    public partial class AgentTemplate
    {
        [JsonProperty("agentId")]
        public string agentId { get; set; }
        [JsonProperty("queues")]
        public string[] queues { get; set; }
    }

    public partial class UserBranchTag {
        
        [JsonProperty("type")]
        public string Type;
        
        [JsonProperty("value")]
        public string Value;
        
        [JsonProperty("id")]
        public string Id;
        
        [JsonProperty("name")]
        public string Name;
        
    }
    
    
}