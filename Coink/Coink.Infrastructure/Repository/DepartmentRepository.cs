using Coink.Core.Entities;
using Coink.Core.Interfaces;
using Coink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Coink.Infrastructure.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CoinkContext _context;

        public DepartmentRepository(CoinkContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            var dep = await _context.Departments.ToListAsync();
            return dep;
        }

        public async Task<Department> GetDepartment(int id)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
            return department;
        }

        public async Task InsertDepartment(Department department)
        {
            // Si CountryId es 0, lanza una excepción
            if (department.CountryId == 0)
            {
                throw new ArgumentException("El valor de CountryId no es válido.");
            }

            // Verifica si CountryId existe en la base de datos
            var country = await _context.Countries.FindAsync(department.CountryId);
            if (country == null)
            {
                throw new ArgumentException("El CountryId proporcionado no existe en la base de datos.");
            }

            // Crea una nueva entidad Department sin establecer el Id
            var newDepartment = new Department
            {
                Name = department.Name,
                CountryId = department.CountryId
            };

            // Agrega y guarda la nueva entidad
            _context.Departments.Add(newDepartment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateDepartment(Department department)
        {
            var existingDepartment = await _context.Departments.FindAsync(department.Id);
            if (existingDepartment == null)
            {
                throw new ArgumentException("El DepartmentId proporcionado no es válido.");
            }

            var country = await _context.Countries.FindAsync(department.CountryId);
            if (country == null)
            {
                throw new ArgumentException("El CountryId proporcionado no existe en la base de datos.");
            }

            existingDepartment.Name = department.Name;
            existingDepartment.CountryId = department.CountryId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var departmentToDelete = await GetDepartment(id);
            _context.Departments.Remove(departmentToDelete);
            int row = await _context.SaveChangesAsync();
            return row > 0;
        }
    }
}
