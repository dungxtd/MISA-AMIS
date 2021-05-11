using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interface.Service;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
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
        public IActionResult GetPaging(int pageIndex, int pageSize, string filter)
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

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            // query data from database  
            await Task.Yield();
            var list = _employeeService.GetAll();
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = "employees.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
