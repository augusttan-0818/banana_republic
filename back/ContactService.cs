using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Contact;
using NRC.Const.CodesAPI.Application.Interfaces;

namespace NRC.Const.CodesAPI.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnifiedContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactService(IUnifiedContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository= contactRepository;
            _mapper = mapper;
        }

        public async Task<ContactDto?> GetContactByIdAsync(int id)
        {
            var contact= await _contactRepository.GetUnifiedContactByIdAsync(id);
            return _mapper.Map<ContactDto>(contact);
        }

        public async Task<IEnumerable<ContactDto>> GetContactsAsync()
        {
            var contacts = await _contactRepository.GetUnifiedContactsAsync();
            return _mapper.Map<IEnumerable<ContactDto>>(contacts);

        }
    }
}
