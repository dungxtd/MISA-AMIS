using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interface.Repository;
using MISA.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Service
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        /// <summary>
        /// Validate
        /// </summary>
        IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        protected override void CustomValidate(Employee entity)
        {
            if (entity is Employee)
            {
                var employee = entity;
                //Tên khách hàng không được phép để trống
                if (string.IsNullOrEmpty(employee.EmployeeName))
                {
                    throw new BadRequestException("Tên khách hàng không được phép để trống");
                }

                //Check các thông tin bắt buộc nhập


                //Check trùng mã
                var isExits = _employeeRepository.CheckEmployeeCodeExits(employee.EmployeeCode);
                if (isExits)
                {
                    //throw new Exception("Mã khách hàng đã tồn tại trên hệ thống.");
                }
            }
        }

        public IEnumerable<Employee> GetPaging(int pageIndex, int pageSize, string filter)
        {
            if (pageIndex <= 0 || pageSize <= 0) throw new BadRequestException("Page size và page index phải là số nguyên");
            var employees = _employeeRepository.GetPaging(pageIndex, pageSize, filter);
            return employees;
        }
    }
}
