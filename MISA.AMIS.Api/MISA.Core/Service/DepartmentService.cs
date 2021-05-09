using MISA.Core.Entities;
using MISA.Core.Interface.Repository;
using MISA.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Service
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;

        }
        protected override void CustomValidate(Department entity)
        {
        }
        //public int Delete(Guid customerGroupId)
        //{
        //    var rowsAffect = _customerGroupRepository.Delete(customerGroupId);
        //    return rowsAffect;
        //}

        //public IEnumerable<CustomerGroup> GetAll()
        //{
        //    return _customerGroupRepository.GetAll();
        //}

        //public CustomerGroup GetById(Guid customerGroupId)
        //{
        //    return _customerGroupRepository.GetById(customerGroupId);
        //}

        //public int Insert(CustomerGroup customerGroup)
        //{
        //    var rowsAffect = _customerGroupRepository.Insert(customerGroup);
        //    return rowsAffect;
        //}

        //public int Update(CustomerGroup customerGroup)
        //{
        //    var rowsAffect = _customerGroupRepository.Update(customerGroup);
        //    return rowsAffect;
        //}
    }
}
