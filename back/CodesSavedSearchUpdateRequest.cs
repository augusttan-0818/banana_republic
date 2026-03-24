using System.Text.Json.Serialization;

namespace NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesSavedSearches
{
    public class CodesSavedSearchUpdateRequest
    {
        [JsonPropertyName("savedSearchId")]
        public int SavedSearchId {get;set;}
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("visibility")]
        public string? Visibility { get; set; }
        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt {get; set;}

    }
}
