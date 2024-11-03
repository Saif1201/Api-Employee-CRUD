using ApiCore.Models;
using ApiCore.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ApiCore.Controllers
{
    [Route("api/saif")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeesController> _logger;
        private readonly ApiResponse _apiResponse;

        #region Constructor
        public EmployeesController(ApplicationDbContext context, ILogger<EmployeesController> logger)
        {
            _context = context;
            _logger = logger;
            _apiResponse = new ApiResponse();
        }
        #endregion

        #region GetAll
        [HttpGet]
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<EmployeeDto> employeeDtos1 = await _context.Employees.Select(e => new EmployeeDto { Id = e.Id, Name = e.Name, Salary = e.Salary, UAN = e.UAN }).ToListAsync();
                _logger.LogInformation("This is Serilog for GetAll bro");

                _apiResponse.IsSuccess = true;
                _apiResponse.Result = employeeDtos1;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Message = ex.Message;
                _apiResponse.IsSuccess = false;

                return BadRequest(_apiResponse);
            }

        }
        #endregion

        #region GetById
        [HttpGet("{id:int}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();

            try
            {
                Employee? employee = await _context.Employees.FindAsync(id);

                if (employee == null) return NotFound();

                EmployeeDto employeeDto = new()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Salary = employee.Salary,
                    UAN = employee.UAN
                };

                _logger.LogInformation("This is serilog for GetById");

                _apiResponse.IsSuccess = true;
                _apiResponse.Result = employeeDto;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to retrieve employee list by Id");

                _apiResponse.IsSuccess = false;
                _apiResponse.Message = ex.Message;
                _apiResponse.StatusCode = HttpStatusCode.NotFound;

                return BadRequest(_apiResponse);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null) return BadRequest();

            Employee employee = new()
            {
                Name = employeeDto.Name,
                Salary = employeeDto.Salary,
                UAN = employeeDto.UAN,
                CreatedDate = DateTime.UtcNow,
            };

            if (employee == null) return NotFound();

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            employeeDto.Id = employee.Id;

            return CreatedAtRoute("GetById", new { id = employee.Id }, employeeDto);
        }
        #endregion

        #region PUT
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int? id, [FromBody] EmployeeDto employeeDto)
        {
            if (id == null || id == 0 || employeeDto.Id != id)
            {
                return BadRequest();
            }

            Employee? employeeFromDb = await _context.Employees.FindAsync(id);

            if (employeeFromDb == null) return NotFound();

            employeeFromDb.Name = employeeDto.Name;
            employeeFromDb.Salary = employeeDto.Salary;
            employeeFromDb.UAN = employeeDto.UAN;

            _context.Employees.Update(employeeFromDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region DELETE
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id == null) return BadRequest();

            Employee? employee = await _context.Employees.FindAsync(id);

            if (employee == null) return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }
        #endregion

        #region PATCH
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartial(int id, JsonPatchDocument<EmployeeDto> jsonPatch)
        {
            if (id == 0 || jsonPatch == null) return BadRequest();

            // Fetch employee from database
            Employee? employeeFromDb = await _context.Employees.FindAsync(id);

            if (employeeFromDb == null) return NotFound();

            // Map Employee entity to DTO
            EmployeeDto employeeDto = new()
            {
                Id = employeeFromDb.Id,
                Name = employeeFromDb.Name,
                UAN = employeeFromDb.UAN,
                Salary = employeeFromDb.Salary
            };

            // Apply JSON patch to the DTO
            jsonPatch.ApplyTo(employeeDto);

            employeeFromDb.Name = employeeDto.Name;
            employeeFromDb.Salary = employeeDto.Salary;
            employeeFromDb.UAN = employeeDto.UAN;

            _context.Employees.Update(employeeFromDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

    }
}
