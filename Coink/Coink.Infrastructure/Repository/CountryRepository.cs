using Coink.Core.Entities;
using Coink.Core.Interfaces;
using Coink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Coink.Infrastructure.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly CoinkContext _context;

        public CountryRepository(CoinkContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
            
        }

        public async Task<Country> GetCountry(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            return country;
        }

        public async Task InsertCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateCountry(Country country)
        {
            var existingCountry = await _context.Countries.FindAsync(country.Id);
            if (existingCountry == null)
            {
                throw new ArgumentException("El CountryId proporcionado no es válido.");
            }

            existingCountry.Name = country.Name;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteCountry(int id)
        {
            var countryToDelete = await GetCountry(id);
            _context.Countries.Remove(countryToDelete);
            int row = await _context.SaveChangesAsync();
            return row > 0;
        }
    }
}
