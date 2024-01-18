using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Configuration;

namespace Portfolio.Data.Services
{
    public class ContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Contact>> GetAllContactDetails()
        {
            return await _context.Contacts.ToListAsync();
        }

        //Add New Record
        public async Task<bool> AddNewContactDetails(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return true;
        }
    
    }
}