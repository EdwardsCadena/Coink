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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _departmentRepository.GetDepartments();
            var departmentsDto = _mapper.Map<IEnumerable<DepartmentDTOs>>(departments);
            return Ok(departmentsDto);
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await _departmentRepository.GetDepartment(id);
            var departmentDto = _mapper.Map<DepartmentDTOs>(department);
            return Ok(departmentDto);
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public async Task<IActionResult> PostDepartment(DepartmentDTOs departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            await _departmentRepository.InsertDepartment(department);
            var updatedto = new ApiResponse<DepartmentDTOs>(departmentDto);
            return Ok(updatedto);
        }

        // PUT api/<DepartmentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, DepartmentDTOs departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            department.Id = id;
            var Update = await _departmentRepository.UpdateDepartment(department);
            var updatedto = new ApiResponse<bool>(Update);
            return Ok(updatedto);
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await _departmentRepository.DeleteDepartment(id);
            var delete = new ApiResponse<bool>(result);
            return Ok(delete);
        }
    }
}
