namespace NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search
{
    public class SearchResult<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int TotalCount { get; set; }
    }
}
