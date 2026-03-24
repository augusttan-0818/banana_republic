using System.ComponentModel.DataAnnotations.Schema;

namespace NRC.Const.CodesAPI.Domain.Entities.ReferenceDocumentUpdate
{
    [Table("Agency")]
    public class Agency
    {
        public required string Id { get; set; } 
        public required string Name { get; set; } 
        public string? NameFR { get; set; }
        public required string Abbr { get; set; }
        public string? AbbrFR { get; set; }
        public bool? SCC { get; set; }
    }
}
