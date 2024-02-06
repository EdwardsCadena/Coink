using Coink.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Coink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Aquí se definen las dependencias que serán inyectadas en el controlador
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        // Este es el constructor del controlador, donde las dependencias son inyectadas
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            // Las dependencias inyectadas se asignan a las variables privadas para ser usadas en los métodos del controlador
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // Este método maneja las solicitudes GET a la ruta api/Users
        // Devuelve una lista de todos los usuarios
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDTOs>>> GetUsers()
        {
            // Obtiene los usuarios de la base de datos usando el repositorio
            var users = await _userRepository.GetUsers();

            // Mapea los usuarios a un DTO (Data Transfer Object)
            var usersDto = _mapper.Map<IEnumerable<UserDTOs>>(users);

            // Devuelve los usuarios como una respuesta HTTP 200 OK
            return Ok(usersDto);
        }

        // Este método maneja las solicitudes GET a la ruta api/Users/{id}
        // Devuelve un usuario específico basado en el ID proporcionado
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDTOs>> GetUser(int id)
        {
            // Obtiene el usuario de la base de datos usando el repositorio
            var user = await _userRepository.GetUser(id);

            // Si el usuario no se encuentra, devuelve una respuesta HTTP 404 Not Found
            if (user == null)
            {
                return NotFound();
            }

            // Mapea el usuario a un DTO (Data Transfer Object)
            var userDto = _mapper.Map<UserDTOs>(user);

            // Devuelve el usuario como una respuesta HTTP 200 OK
            return Ok(userDto);
        }

        // Este método maneja las solicitudes POST a la ruta api/Users
        // Crea un nuevo usuario con los datos proporcionados
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserDTOs>> PostUser(UserDTOs userDto)
        {
            // Mapea el DTO (Data Transfer Object) a una entidad User
            var user = _mapper.Map<User>(userDto);

            // Inserta el usuario en la base de datos usando el repositorio
            await _userRepository.InsertUser(user);

            // Devuelve una respuesta HTTP 201 Created, indicando que el usuario se ha creado con éxito
            return CreatedAtAction("GetUser", new { id = user.Id }, userDto);
        }

        // Este método maneja las solicitudes PUT a la ruta api/Users/{id}
        // Actualiza un usuario existente con los datos proporcionados
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(int id, UserDTOs userDto)
        {
            // Mapea el DTO (Data Transfer Object) a una entidad User
            var user = _mapper.Map<User>(userDto);

            // Asegura que el ID del usuario es el correcto
            user.Id = id;

            // Actualiza el usuario en la base de datos usando el repositorio
            await _userRepository.UpdateUser(user);

            // Devuelve una respuesta HTTP 204 No Content, indicando que el usuario se ha actualizado con éxito
            return NoContent();
        }

        // Este método maneja las solicitudes DELETE a la ruta api/Users/{id}
        // Elimina un usuario existente basado en el ID proporcionado
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // Elimina el usuario de la base de datos usando el repositorio
            await _userRepository.DeleteUser(id);

            // Devuelve una respuesta HTTP 204 No Content, indicando que el usuario se ha eliminado con éxito
            return NoContent();
        }
    }
}
