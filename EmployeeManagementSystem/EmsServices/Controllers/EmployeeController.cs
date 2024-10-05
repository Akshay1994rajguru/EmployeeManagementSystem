using EmsServices.Model;
using EmsServices.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmsServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployee employee, ILogger<EmployeeController> logger)
        {
            _employee = employee ??
           throw new ArgumentNullException(nameof(employee));
            _logger = logger;
        }



        [HttpGet]
        [Route("GetAllUsers")]
        
        public async Task<IActionResult> Get()
        {
            return Ok(await _employee.GetEmployees());
        }
        //Get details for a specific Users
        [HttpGet]
        [Route("GetEmployeeByID/{ID}")]
        public async Task<IActionResult> GetEmployeeByID(int ID)
        {
            var result = await _employee.GetEmployeeByID(ID);
            if (result is null)
            {
                return NotFound();
            }
            else
                return Ok(result);
        }

        //Add a new Users
        [HttpPost]
        [Route("AddUsers")]
        public async Task<IActionResult> Post(Employee employee)
        {
            var result = await _employee.AddEmployee(employee);
            if (!(result is null))
            {
                return Ok("Added Successfully");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }

        }
        //Update details for a Users
        [HttpPut]
        [Route("UpdateUsers")]
        public async Task<IActionResult> Put(Employee Users)
        {
            await _employee.UpdateEmployee(Users);
            return Ok("Updated Successfully");
        }
        //Delete a Users
        [HttpDelete]
        [Route("DeleteEmployee")]
        public IActionResult Delete(int id)
        {
            var result = _employee.DeleteEmployee(id);
            if (result)
                return NoContent();
            else
                return NotFound();
        }
    }
}
