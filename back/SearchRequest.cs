namespace NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search
{
    public class SearchRequest
    {
        public List<FilterRule> Filters { get; set; } = [];
        public List<SortRule> Sort { get; set; } = [];
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }

    public class FilterRule
    {
        public string Field { get; set; } = "";
        public string Operator { get; set; } = "";
        public object? Value { get; set; }
    }

    public class SortRule
    {
        public string Field { get; set; } = "";
        public string Direction { get; set; } = "asc";
    }
}
