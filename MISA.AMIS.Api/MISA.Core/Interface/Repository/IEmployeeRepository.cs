using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interface.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        public bool CheckEmployeeCodeExits(String employeeCode);
        public bool CheckEmployeePhoneNumberExits(String phoneNumber);
        IEnumerable<Employee> GetPaging(int pageIndex, int pageSize,string filter);
        public IEnumerable<int> GetEmployeeCount(string filter);
        public IEnumerable<String> GetMaxCode();
    }
}
