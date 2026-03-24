using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Contact;
using NRC.Const.CodesAPI.Application.Interfaces;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/contacts/")]
    [ApiController]
    //[Authorize]
    [ApiVersion(1)]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // -------------------------------------------------
        // GET: api/v1/contacts
        // -------------------------------------------------
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContactsAsync()
        {
            var result = await _contactService.GetContactsAsync();
            return Ok(result);
        }

        // -------------------------------------------------
        // GET: api/v1/contacts/{id}
        // -------------------------------------------------
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        public async Task<ActionResult<ContactDto>> GetContactByIdAsync(int id)
        {
            var result = await _contactService.GetContactByIdAsync(id);

            if (result == null)
                return NotFound($"Contact with Id {id} not found.");

            return Ok(result);
        }


    }
}
