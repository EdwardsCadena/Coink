using Coink.Core.Entities;
using Coink.Core.Interfaces;
using Coink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Coink.Infrastructure.Repository
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly CoinkContext _context;

        public MunicipalityRepository(CoinkContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Municipality>> GetMunicipalities()
        {
            return await _context.Municipalities.ToListAsync();
        }

        public async Task<Municipality> GetMunicipality(int id)
        {
            var municipality = await _context.Municipalities.FirstOrDefaultAsync(x => x.Id == id);
            return municipality;
        }

        public async Task InsertMunicipality(Municipality municipality)
        {
            // Si DepartmentId es 0, lanza una excepción
            if (municipality.DepartmentId == 0)
            {
                throw new ArgumentException("El valor de DepartmentId no es válido.");
            }

            // Verifica si DepartmentId existe en la base de datos
            var department = await _context.Departments.FindAsync(municipality.DepartmentId);
            if (department == null)
            {
                throw new ArgumentException("El DepartmentId proporcionado no existe en la base de datos.");
            }

            // Crea una nueva entidad Municipality sin establecer el Id
            var newMunicipality = new Municipality
            {
                Name = municipality.Name,
                DepartmentId = municipality.DepartmentId
            };

            // Agrega y guarda la nueva entidad
            _context.Municipalities.Add(newMunicipality);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateMunicipality(Municipality municipality)
        {
            var existingMunicipality = await _context.Municipalities.FindAsync(municipality.Id);
            if (existingMunicipality == null)
            {
                throw new ArgumentException("El MunicipalityId proporcionado no es válido.");
            }

            var department = await _context.Departments.FindAsync(municipality.DepartmentId);
            if (department == null)
            {
                throw new ArgumentException("El DepartmentId proporcionado no existe en la base de datos.");
            }

            existingMunicipality.Name = municipality.Name;
            existingMunicipality.DepartmentId = municipality.DepartmentId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteMunicipality(int id)
        {
            var municipalityToDelete = await GetMunicipality(id);
            _context.Municipalities.Remove(municipalityToDelete);
            int row = await _context.SaveChangesAsync();
            return row > 0;
        }
    }
}
