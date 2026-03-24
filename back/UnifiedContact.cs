using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NRC.Const.CodesAPI.Domain.Entities.Core
{
    public class UnifiedContact
    {
        public required int UnifiedContactId { get; set; }
        public required string Email { get; set; } = null!;
        public string NormalizedEmail => Email.ToLowerInvariant().Trim();

        public long? ExternalUserId { get; set; }

        public short? Salutation { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleInitial { get; set; }

        public string? LastName { get; set; }

        public string FullName => string.Join(" ", $"{LastName},", FirstName, MiddleInitial).Trim() + (string.IsNullOrWhiteSpace(OrganizationName) ? "" : $" ({OrganizationName})");
        public string FullNameFr => string.Join(" ", $"{LastName},", FirstName, MiddleInitial).Trim() + (string.IsNullOrWhiteSpace(OrganizationName) ? "" : $" ({OrganizationNameFr})");
        public string? Title { get; set; }

        public string? Designation { get; set; }

        public string? DisplayName { get; set; }

        public string? OrganizationName { get; set; }

        public string? OrganizationNameFr { get; set; }

        public int? OrgId { get; set; }

        public long? CustomerAccountNumber { get; set; }

        public bool? IsPrimaryContact { get; set; }

        public bool? IsBillingContact { get; set; }

        public string? AddressLine { get; set; }

        public string? City { get; set; }

        public string? PostalCode { get; set; }

        public string? CountryCode { get; set; }

        public string? RegionCode { get; set; }

        public string? ProvinceCode { get; set; }

        public string? Telephone { get; set; }

        public string? TelephoneExtension { get; set; }

        public string? CellPhone { get; set; }

        public string? Fax { get; set; }

        public string? PreferredLanguage { get; set; }

        public int? NotifyMethodPrefCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public bool IsActive { get; set; }

        public int? ExternalUnifiedContactId { get; set; }
    }
}
