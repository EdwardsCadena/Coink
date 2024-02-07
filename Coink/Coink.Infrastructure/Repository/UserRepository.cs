using Coink.Core.Entities;
using Coink.Core.Interfaces;
using Coink.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coink.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        // Inyección de dependencias: pasamos el contexto de la base de datos al constructor
        private readonly CoinkContext _context;

        public UserRepository(CoinkContext context)
        {
            _context = context;
        }

        // Método para recuperar todos los usuarios utilizando el procedimiento almacenado 'GetAllUsers'
        public async Task<IEnumerable<User>> GetUsers()
        {
            // Ejecuta el procedimiento almacenado y convierte el resultado en una lista
            return await _context.Users.FromSqlRaw("EXEC GetAllUsers").ToListAsync();
        }

        // Método para recuperar un usuario específico por ID utilizando el procedimiento almacenado 'GetUserById'
        public async Task<User> GetUser(int id)
        {
            // Ejecuta el procedimiento almacenado con el ID proporcionado y convierte el resultado en una lista
            var users = await _context.Users.FromSqlRaw("EXEC GetUserById {0}", id).ToListAsync();
            // Devuelve el primer usuario de la lista (o null si la lista está vacía)
            return users.FirstOrDefault();
        }

        // Método para insertar un nuevo usuario utilizando el procedimiento almacenado 'InsertUser'
        public async Task InsertUser(User user)
        {
            // Si CountryId, DepartmentId o MunicipalityId son 0, lanza una excepción
            if (user.CountryId == 0 || user.DepartmentId == 0 || user.MunicipalityId == 0)
            {
                throw new ArgumentException("El valor de CountryId, DepartmentId o MunicipalityId no es válido.");
            }

            // Ejecuta el procedimiento almacenado con los datos del usuario proporcionado
            await _context.Database.ExecuteSqlRawAsync("EXEC CreateUser {0}, {1}, {2}, {3}, {4}, {5}",
                user.Name, user.Phone, user.Address, user.CountryId, user.DepartmentId, user.MunicipalityId);
        }

        // Método para actualizar un usuario existente utilizando el procedimiento almacenado 'UpdateUser'
        public async Task<bool> UpdateUser(User user)
        {
            // Si CountryId, DepartmentId o MunicipalityId son 0, lanza una excepción
            if (user.CountryId == 0 || user.DepartmentId == 0 || user.MunicipalityId == 0)
            {
                throw new ArgumentException("El valor de CountryId, DepartmentId o MunicipalityId no es válido.");
            }

            // Verifica si los Ids existen en la base de datos
            var country = await _context.Countries.FindAsync(user.CountryId);
            var department = await _context.Departments.FindAsync(user.DepartmentId);
            var municipality = await _context.Municipalities.FindAsync(user.MunicipalityId);

            if (country == null || department == null || municipality == null)
            {
                throw new ArgumentException("El CountryId, DepartmentId o MunicipalityId proporcionado no existe en la base de datos.");
            }

            // Ejecuta el procedimiento almacenado con los datos del usuario proporcionado y obtiene el número de filas afectadas
            var rowsAffected = await _context.Database.ExecuteSqlRawAsync("EXEC UpdateUser {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                user.Id, user.Name, user.Phone, user.Address, user.CountryId, user.DepartmentId, user.MunicipalityId);

            // Si al menos una fila fue afectada (es decir, el usuario fue actualizado), devuelve true; de lo contrario, devuelve false
            return rowsAffected > 0;
        }

        // Método para eliminar un usuario existente utilizando el procedimiento almacenado 'DeleteUser'
        public async Task<bool> DeleteUser(int id)
        {
            // Ejecuta el procedimiento almacenado con el ID proporcionado y obtiene el número de filas afectadas
            var rowsAffected = await _context.Database.ExecuteSqlRawAsync("EXEC DeleteUser @Id", new SqlParameter("@Id", id));

            // Si al menos una fila fue afectada (es decir, el usuario fue eliminado), devuelve true; de lo contrario, devuelve false
            return rowsAffected > 0;
        }
    }
}
