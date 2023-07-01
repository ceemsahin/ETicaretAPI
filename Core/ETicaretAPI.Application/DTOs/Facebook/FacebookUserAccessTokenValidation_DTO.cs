using System.Text.Json.Serialization;

namespace ETicaretAPI.Application.DTOs.Facebook
{
    public class FacebookUserAccessTokenValidation_DTO
    {
        [JsonPropertyName("data")]

        public FacebookUserAccessTokenValidationData_DTO Data { get; set; }
    }

    public class FacebookUserAccessTokenValidationData_DTO
    {
        [JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }


        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }



}
