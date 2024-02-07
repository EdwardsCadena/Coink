using Coink.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coink.Core.Interfaces
{
    public interface IMunicipalityRepository
    {
        Task<IEnumerable<Municipality>> GetMunicipalities();
        Task<Municipality> GetMunicipality(int id);
        Task InsertMunicipality(Municipality municipality);
        Task<bool> UpdateMunicipality(Municipality municipality);
        Task<bool> DeleteMunicipality(int id);
    }
}
