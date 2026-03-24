namespace NRC.Const.CodesAPI.Domain.Entities.Core
{
    public class CodesCCRSortingOutput
    {
        public short SortingOutputId { get; set; }
        public string? SortingOutputTitle_En { get; set; }
        public string? SortingOutputTitle_Fr { get; set; }
        public short SortOrder { get; set; }
    }
}
