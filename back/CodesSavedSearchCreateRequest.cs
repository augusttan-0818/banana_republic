using System.Text.Json.Serialization;

namespace NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesSavedSearches
{
    public class CodesSavedSearchCreateRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("Context")]
        public string Context { get; set; }
        [JsonPropertyName("visibility")]
        public string Visibility { get; set; }
        [JsonPropertyName("params")]
        public string Params { get; set; }
        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }
        [JsonPropertyName("createdName")]
        public string CreatedByName { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt {get; set;}
    }
}
