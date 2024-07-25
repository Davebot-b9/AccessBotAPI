using AccessBotAPI.Controllers;

namespace AccessBotAPI.Models
{
    public class BaseUserRequest
    {
        public string UserId { get; set; }
    }

    public class SupportConversationState : BaseUserRequest
    {
        public string UserName { get; set; }
        public string UserBranch { get; set; }
        public string CurrentStep { get; set; }
    }

    public class UserResponse : BaseUserRequest
    {
        public string Message { get; set; }
    }

    //public class UserOptionResponse : BaseUserRequest
    //{
    //    public int Option { get; set; }
    //}

    //public class UserSubOptionResponse : BaseUserRequest
    //{
    //    public int SubOption { get; set; }
    //}

    //public class EvidenceUploadRequest : BaseUserRequest
    //{
    //    public string Evidence { get; set; }
    //}
    //using System.Text.Json.Serialization;
}