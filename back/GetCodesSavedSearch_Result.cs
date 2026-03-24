
namespace NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesSavedSearches
{
  public class GetCodesSavedSearch_Result
  {
    public int SavedSearchId { get; set; }
    public string Name { get; set; }
    public string Context { get; set; }
    public string Visibility { get; set; }

    public string Params { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedByName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}

