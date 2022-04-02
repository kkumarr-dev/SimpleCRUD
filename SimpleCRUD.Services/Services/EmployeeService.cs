using SimpleCRUD.DTO;
using SimpleCRUD.Repos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCRUD.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;
        public EmployeeService(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<bool> AddEmployee(EmployeeDTO dto)
        {
            return await _employeeRepo.AddEmployee(dto);
        }

        public async Task<bool> DeleteEmployee(int userid)
        {
            return await _employeeRepo.DeleteEmployee(userid);
        }

        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            return await _employeeRepo.GetAllEmployees();
        }

        public async Task<EmployeeDTO> GetEmployeeById(int userid)
        {
            return await _employeeRepo.GetEmployeeById(userid);
        }
    }
}
