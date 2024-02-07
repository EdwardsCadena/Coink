using Coink.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coink.Core.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<Country> GetCountry(int id);
        Task InsertCountry(Country country);
        Task<bool> UpdateCountry(Country country);
        Task<bool> DeleteCountry(int id);
    }
}
