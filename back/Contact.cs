namespace NRC.Const.CodesAPI.Domain.Entities.Core
{
    public class Contact
    {
        public int ContactId { get; set; }

        public int? Salutation { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string? Designation { get; set; }

        public string? Organization { get; set; }

        public string? Telephone { get; set; }

        public string? Extension { get; set; }

        public string? Fax { get; set; }

        public string? Email { get; set; }

        public string? Language { get; set; }

        public int? Province { get; set; }

        public string? Postalcode { get; set; }

        public string? OtherFunction { get; set; }

        public string? Affiliation { get; set; }

        public string? Website { get; set; }

        public string? TollFreePhone { get; set; }

        public string? TollFreeFax { get; set; }

        public string? GeneralPhone { get; set; }

        public int? Country { get; set; }

        public int? Region { get; set; }

        public int? SendAs { get; set; }

        public string? CellPhone { get; set; }

        public bool? IsCCMC { get; set; }

        public bool? CCMCPrimaryFlag { get; set; }

        public bool? CCMCBillingFlag { get; set; }

        public int? Org_Id { get; set; }

        public string? City { get; set; }

        public string? Title { get; set; }

        public string? Address { get; set; }
    }
}

