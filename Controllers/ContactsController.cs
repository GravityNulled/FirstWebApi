global using Microsoft.AspNetCore.Mvc;
global using WebApi.Data;
global using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController: Controller
{
    private readonly ApplicationDbContext _dbContext;
    public ContactsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet (Name = "Contact")]
    public async Task<ActionResult<Contact>> Get()
    {
        return Ok( await _dbContext.Contacts.ToListAsync());
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<Contact>> GetUser(Guid id)
    {
        var user = _dbContext.Contacts.FirstOrDefaultAsync(i => i.Id == id);
        if (user != null)
        {
            return Ok(await user);
        }
        return NotFound("User is not found!");
    }

    [HttpPost]
    public async Task<ActionResult> PostContact(AddContactRequest addContactRequest)
    {
        var contact = new Contact()
        {
            Id = new Guid(),
            FullName = addContactRequest.FullName,
            Email = addContactRequest.Email,
            Phone = addContactRequest.Phone,
            Address = addContactRequest.Address
        };
        
        await _dbContext.Contacts.AddAsync(contact);
        await _dbContext.SaveChangesAsync();
        return Ok(contact);
    }
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<ActionResult> UpdateUser([FromRoute]Guid id, UpdateContactRequest updateContactRequest)
    {
        var user = _dbContext.Contacts.Find(id);
        if (user != null)
        {
            user.FullName = updateContactRequest.FullName;
            user.Address = updateContactRequest.Address;
            user.Phone = updateContactRequest.Phone;
            user.Email = updateContactRequest.Email;
            
            await _dbContext.SaveChangesAsync();
            return Ok(user);
        }
        return NotFound("Contact Not Found!");
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<ActionResult<List<Contact>>> DeleteContact([FromRoute] Guid id)
    {
        var contact = _dbContext.Contacts.Find(id);
        if (contact != null)
        {
            _dbContext.Remove(contact);
            await _dbContext.SaveChangesAsync();
            return Ok(_dbContext.Contacts.ToList());
        }
        return NotFound();

    }
}