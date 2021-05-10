using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("paging")]
        public IActionResult GetPaging(int pageIndex, int pageSize,string filter)
        {
            var employees = _employeeService.GetPaging(pageIndex, pageSize, filter);
            if (employees.Count() > 0)
            {
                return Ok(employees);
            }
            else
            {
                return NoContent();
            }
        }
        [HttpGet("count-paging")]
        public IActionResult GetEmployeeCount(string filter)
        {
            var count = _employeeService.GetEmployeeCount(filter);
            if (count.Count() > 0)
            {
                return Ok(count);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
