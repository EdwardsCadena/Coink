using AutoMapper;
using Coink.Core.DTOs;
using Coink.Core.Entities;
using Coink.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Coink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountrysController : ControllerBase
    {
        // Inyecta las dependencias que necesita este controlador
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        // El constructor del controlador se llama cuando se crea una nueva instancia de este controlador
        public CountrysController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        // Define el método GET para esta API
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _countryRepository.GetCountries();  // Recupera los países de la base de datos
            var countriesDto = _mapper.Map<IEnumerable<CountryDTOs>>(countries);  // Mapea los países a DTOs
            return Ok(countriesDto);  // Devuelve un código de estado 200 (OK) con los países
        }

        // Define el método GET con un parámetro para esta API
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry(int id)
        {
            var country = await _countryRepository.GetCountry(id);  // Recupera el país con el id especificado
            var countryDto = _mapper.Map<CountryDTOs>(country);  // Mapea el país a un DTO
            return Ok(countryDto);  // Devuelve un código de estado 200 (OK) con el país
        }

        // Define el método POST para esta API
        [HttpPost]
        public async Task<IActionResult> PostCountry(CountryDTOs countryDto)
        {
            var country = _mapper.Map<Country>(countryDto);  // Mapea el DTO a una entidad Country
            await _countryRepository.InsertCountry(country);  // Inserta el nuevo país en la base de datos
            var updatedto = new ApiResponse<CountryDTOs>(countryDto);  // Empaqueta el DTO en una ApiResponse
            return Ok(updatedto);  // Devuelve un código de estado 200 (OK) con la ApiResponse
        }

        // Define el método PUT para esta API
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, CountryDTOs countryDto)
        {
            var country = _mapper.Map<Country>(countryDto);  // Mapea el DTO a una entidad Country
            country.Id = id;  // Asegura que el id de la entidad Country es correcto
            var result = await _countryRepository.UpdateCountry(country);  // Actualiza el país en la base de datos
            var updatedto = new ApiResponse<bool>(result);  // Empaqueta el resultado en una ApiResponse
            return Ok(updatedto);  // Devuelve un código de estado 200 (OK) con la ApiResponse
        }

        // Define el método DELETE para esta API
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var result = await _countryRepository.DeleteCountry(id);  // Elimina el país con el id especificado
            var response = new ApiResponse<bool>(result);  // Empaqueta el resultado en una ApiResponse
            return Ok(response);  // Devuelve un código de estado 200 (OK) con la ApiResponse
        }
    }
}
