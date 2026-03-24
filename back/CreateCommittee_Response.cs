namespace NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Committees
{
    public class CreateCommittee_Response
    {
        public int CommitteeId { get; set; }
        public short? CodesCycleId { get; set; }
        public string? CommitteeName { get; set; }
        public string? CommitteeShortName { get; set; }
        public byte CommitteeType { get; set; }
        public int? ParentCommitteeId { get; set; }
    }
}

