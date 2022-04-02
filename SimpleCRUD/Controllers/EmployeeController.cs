using Microsoft.AspNetCore.Mvc;
using SimpleCRUD.DTO;
using SimpleCRUD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Employees()
        {
            var data = await _employeeService.GetAllEmployees();
            return View(data);
        }
        public IActionResult Create()
        {
            return PartialView("AddEmployeesPartial");
        }
        public async Task<IActionResult> Edit(int userid)
        {
            var data = await _employeeService.GetEmployeeById(userid);
            return PartialView("EditEmployeesPartial", data);
        }
        public async Task<IActionResult> AddEmployee(EmployeeDTO dto)
        {
            var data = await _employeeService.AddEmployee(dto);
            return Ok(data);
        }
        public async Task<IActionResult> Delete(int userid)
        {
            var data = await _employeeService.DeleteEmployee(userid);
            return Ok(data);
        }
    }
}
