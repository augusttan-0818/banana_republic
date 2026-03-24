using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Committees;
namespace NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Committees
{
    public class GetCommittees_Response
    {
        public int CommitteeId { get; set; }
        public short? CodesCycleId { get; set; }
        public string? CommitteeName { get; set; }
        public string? CommitteeShortName { get; set; }
        public byte CommitteeType { get; set; }
        public int? ParentCommitteeId { get; set; }
        public string? CodesCycleName { get; set; }
        public string? CodesCommitteeTypeName { get; set; }
        public string? ParentCommitteeName { get; set; }
        // Navigation properties
        public CodesCommittee? ParentCommittee { get; set; } // Self-referencing parent
        public ICollection<CodesCommittee>? SubCommittees { get; set; } // Self-referencing children
        //public CodesCycle? CodesCycle { get; set; }
        public CodesCommitteeType? CodesCommitteeType { get; set; }

        public string? CombinedName {get;set;}

    }
}