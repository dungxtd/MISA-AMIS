using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interface.Service;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        IDepartmentService _departmentService;
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService) : base(employeeService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        /// <summary>
        /// Hàm chia trang bằng filter
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns>response</returns>
        /// Created: TDDung
        /// Date: 10/5/2021
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
        /// <summary>
        /// Hàm đếm bản ghi
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
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
        /// <summary>
        /// Hàm xuất ra file excel
        /// </summary>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            // query data from database  
            await Task.Yield();
            var listEmployee = _employeeService.GetAll();
            var listDepartment = _departmentService.GetAll();
            var stream = new MemoryStream();
            var listJoin = listDepartment.Join(listEmployee,
                e => e.DepartmentId, //outerKeySelector 
                d => d.DepartmentId,     //innerKeySelector
            (de, em) => new // resultSelector 
            {
                EmployeeCode = em.EmployeeCode,
                EmployeeName = em.EmployeeName,
                Gender = em.Gender,
                DateOfBirth = em.DateOfBirth,
                EmployeePosition = em.EmployeePosition,
                DepartmentName = de.DepartmentName,
                BankAccountNumber = em.BankAccountNumber,
                BankName = em.BankName,
            }) ;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(Properties.Resources.nameExcelFile);
                
                workSheet.Column(1).Width = 5;
                workSheet.Column(2).Width = 15;
                workSheet.Column(3).Width = 30;
                workSheet.Column(4).Width = 10;
                workSheet.Column(5).Width = 15;
                workSheet.Column(6).Width = 30;
                workSheet.Column(7).Width = 30;
                workSheet.Column(8).Width = 15;
                workSheet.Column(9).Width = 30;

                // style cho excel.
                workSheet.Cells[1, 1].Value = Properties.Resources.cell1;
                workSheet.Cells[1, 2].Value = Properties.Resources.cell2;
                workSheet.Cells[1, 3].Value = Properties.Resources.cell3;
                workSheet.Cells[1, 4].Value = Properties.Resources.cell4;
                workSheet.Cells[1, 5].Value = Properties.Resources.cell5;
                workSheet.Cells[1, 6].Value = Properties.Resources.cell6;
                workSheet.Cells[1, 7].Value = Properties.Resources.cell7;
                workSheet.Cells[1, 8].Value = Properties.Resources.cell8;
                workSheet.Cells[1, 9].Value = Properties.Resources.cell9;

                using (var range = workSheet.Cells["A1:I1"])
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    range.Style.Font.Bold = true;
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                int i = 0;
                // đổ dữ liệu từ list vào.
                foreach (var e in listJoin)
                {
                    workSheet.Cells[i + 2, 1].Value = i + 1;
                    workSheet.Cells[i + 2, 2].Value = e.EmployeeCode;
                    workSheet.Cells[i + 2, 3].Value = e.EmployeeName;
                    workSheet.Cells[i + 2, 4].Value = e.Gender;
                    workSheet.Cells[i + 2, 5].Value = e.DateOfBirth != null ? e.DateOfBirth?.ToString("dd/MM/yyyy") : "";
                    workSheet.Cells[i + 2, 6].Value = e.EmployeePosition;
                    workSheet.Cells[i + 2, 7].Value = e.DepartmentName;
                    workSheet.Cells[i + 2, 8].Value = e.BankAccountNumber;
                    workSheet.Cells[i + 2, 9].Value = e.BankName;

                    using (var range = workSheet.Cells[i + 4, 1, i + 4, 9])
                    {
                        range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }

                    i++;
                }

                package.Save();
            }
            stream.Position = 0;
            string excelName = Properties.Resources.excelFileName;

            return File(stream, Properties.Resources.excelFileLink, excelName);
        }
        /// <summary>
        /// Hàm lấy bản ghi lớn nhất
        /// </summary>
        /// <returns></returns>
        /// Created: TDDung
        /// Date: 10/5/2021
        [HttpGet("getMaxCode")]
        public IActionResult GetMaxCode()
        {
            var count = _employeeService.GetMaxCode();
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
