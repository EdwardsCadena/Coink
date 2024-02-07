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
    public class MunicipalityController : ControllerBase
    {
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly IMapper _mapper;

        public MunicipalityController(IMunicipalityRepository municipalityRepository, IMapper mapper)
        {
            _municipalityRepository = municipalityRepository;
            _mapper = mapper;
        }

        // GET: api/<MunicipalityController>
        [HttpGet]
        public async Task<IActionResult> GetMunicipalities()
        {
            var municipalities = await _municipalityRepository.GetMunicipalities();
            var municipalitiesDto = _mapper.Map<IEnumerable<MunicipalityDTOs>>(municipalities);
            return Ok(municipalitiesDto);
        }

        // GET api/<MunicipalityController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMunicipality(int id)
        {
            var municipality = await _municipalityRepository.GetMunicipality(id);
            var municipalityDto = _mapper.Map<MunicipalityDTOs>(municipality);
            return Ok(municipalityDto);
        }

        // POST api/<MunicipalityController>
        [HttpPost]
        public async Task<IActionResult> PostMunicipality(MunicipalityDTOs municipalityDto)
        {
            var municipality = _mapper.Map<Municipality>(municipalityDto);
            await _municipalityRepository.InsertMunicipality(municipality);
            var updatedto = new ApiResponse<MunicipalityDTOs>(municipalityDto);
            return Ok(updatedto);
        }

        // PUT api/<MunicipalityController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMunicipality(int id, MunicipalityDTOs municipalityDto)
        {
            var municipality = _mapper.Map<Municipality>(municipalityDto);
            municipality.Id = id;
            var Update = await _municipalityRepository.UpdateMunicipality(municipality);
            var updatedto = new ApiResponse<bool>(Update);
            return Ok(updatedto);
        }

        // DELETE api/<MunicipalityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMunicipality(int id)
        {
            var result = await _municipalityRepository.DeleteMunicipality(id);
            var delete = new ApiResponse<bool>(result);
            return Ok(delete);
        }
    }
}
