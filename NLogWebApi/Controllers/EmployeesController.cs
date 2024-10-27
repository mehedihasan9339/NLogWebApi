using Microsoft.AspNetCore.Mvc;
using NLog;
using NLogWebApi.Data;
using NLogWebApi.Models;

namespace NLogWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly MyDbContext _context;

        public EmployeesController(MyDbContext context)
        {
            _context = context;
            logger.Info("EmployeesController initialized.");
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                logger.Debug("Entering Get method.");
                for (var i = 0; i < 10; ++i)
                {
                    logger.Info("Hello, {Name}, on iteration {Counter}", Environment.UserName, i);
                }
                var employees = _context.Employees.ToList();
                logger.Info("Fetched employees successfully.");
                return Ok(employees);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred while fetching employees.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] EmployeeViewModel employeeViewModel)
        {
            logger.Info("Creating a new employee.");
            var employee = new Employee
            {
                Name = employeeViewModel.Name,
                Position = employeeViewModel.Position,
                Salary = employeeViewModel.Salary
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            logger.Info("Employee created successfully.");
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }
    }
}
