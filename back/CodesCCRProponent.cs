namespace NRC.Const.CodesAPI.Domain.Entities.Core
{
    public class CodesCCRProponent
    {
        public int CCRProponentId { get; set; }

        public byte? Salutation { get; set; }

        public string? FirstName { get; set; }

        public string? Initial { get; set; }

        public string? LastName { get; set; }

        public string? Designation { get; set; }

        public string? Title { get; set; }

        public string? Organization { get; set; }

        public string? AddressLine { get; set; }

        public string? City { get; set; }

        public string? Province { get; set; }

        public string? Country { get; set; }

        public string? PostalCode { get; set; }

        public string? Phone { get; set; }

        public string? PhoneExtension { get; set; }

        public string? AlternatePhone { get; set; }

        public string? AlternatePhoneExtension { get; set; }

        public string? AlternatePhoneType { get; set; }

        public string? Fax { get; set; }

        public string? Email { get; set; }

        public string? LanguageCode { get; set; }

        public int? UnifiedContactId { get; set; }

        public int? ExternalCCRProponentId { get; set; }

        public bool? IsSnapshotFromUnifiedContact { get; set; }

        public string ProponentFullName =>
            $"{LastName}, {FirstName} {Initial}" +
            (string.IsNullOrWhiteSpace(Organization)
                ? ""
                : $" ({Organization})");
    }
}
