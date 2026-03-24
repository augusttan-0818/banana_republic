using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Contact;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetContactsAsync();
        Task<ContactDto?> GetContactByIdAsync(int id);
    }
}
