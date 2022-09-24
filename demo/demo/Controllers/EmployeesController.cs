using demo.Data;
using Demo.Models;
using Demo.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MyDBContext dbContext;
        public EmployeesController(MyDBContext dbContext)
        {
            this.dbContext = dbContext;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await dbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeReq)
        {
            var employee = new Employee() 
            { 
                Id =   Guid.NewGuid(),
                Name = addEmployeeReq.Name,
                Email = addEmployeeReq.Email,
                Salary = addEmployeeReq.Salary,
                Department = addEmployeeReq.Department,
                DOB = addEmployeeReq.DOB,
            };

            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {

                var ViewEmployee = new UpdateViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DOB = employee.DOB,
            };
                return await Task.Run(() => View("View", ViewEmployee));
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> View(UpdateViewModel model)
        {
            var employee = await dbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DOB = model.DOB;
                employee.Department = model.Department;

                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index") ;
        }

    }
}
