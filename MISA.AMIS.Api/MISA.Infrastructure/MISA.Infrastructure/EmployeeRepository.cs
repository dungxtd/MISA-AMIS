using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interface.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.MISA.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public bool CheckEmployeeCodeExits(string employeeCode)
        {
            // Khởi tạo kết nối
            //Check dữ liệu
            return true;
        }

        public bool CheckEmployeePhoneNumberExits(string phoneNumber)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Employee> GetPaging(int pageIndex, int pageSize, string filter)
        {

            using (dbConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@m_PageIndex", pageIndex);
                dynamicParameters.Add("@m_PageSize", pageSize);
                dynamicParameters.Add("@filter", filter);
                var employees = dbConnection.Query<Employee>("Proc_EmployeeFilter", param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return employees;
            }
        }
    }
}
