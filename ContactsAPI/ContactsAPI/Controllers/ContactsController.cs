using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsApiDbContext _dbContext;


        public ContactsController(ContactsApiDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        // get all contacts
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await this._dbContext.Contacts.ToListAsync());
        }


        // get contact by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(Guid id)
        {
            var contact = await this._dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // add new contact
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] ContactRequest request)
        {
            var newContact = new Contact
            {
                Id = Guid.NewGuid(),
                Fullname = request.Fullname ?? "Unknown",
                Phone = request.Phone,
                Email = request.Email ?? "Unknown@email.com",
                Address = request.Address ?? "Not Provided"
            };

            await this._dbContext.Contacts.AddAsync(newContact);
            await this._dbContext.SaveChangesAsync();

            return Ok(newContact);
        }



        // update contact by id
        //[HttpPut]
        //[Route("{id:guid}")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, [FromBody] ContactRequest request)
        {
            var contact = await this._dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            contact.Fullname = request.Fullname;
            contact.Phone = request.Phone;
            contact.Email = request.Email;
            contact.Address = request.Address;
            contact.Nickname = request.Nickname;

            await this._dbContext.SaveChangesAsync();

            return Ok(contact);
        }


        // delete contact by id
        [HttpDelete("{id}")]
        public IActionResult DeleteContact(Guid id)
        {
            var contact = this._dbContext.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            this._dbContext.Contacts.Remove(contact);
            this._dbContext.SaveChanges();

            return Ok();
        }


    }
}
