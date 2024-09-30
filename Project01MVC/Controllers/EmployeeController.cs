using AutoMapper;
using Demo.BusinessLogicLayer.Intrerfaces;
using Demo.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc.PresentationLayer.ViewModels;

namespace Mvc.PresentationLayer.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string? searchValue)
        {
            //retrive all employee
            //var employees = _repository.GetAllWithDepartment();
            //var employeeViewModel = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);
            //return View(employeeViewModel);
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrWhiteSpace(searchValue))
                 employees = await _unitOfWork.Employees.GetAllWithDepartmentAsync();
            else employees = await _unitOfWork.Employees.GetAllAsync(searchValue);

            var employeeViewModel = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            
            return View(employeeViewModel);

        }
        public async Task<IActionResult> Create()
        {
            var department = await _unitOfWork.Departments.GetAllAsync();
            SelectList listItems = new SelectList(department,"Id","Name");
            ViewBag.Departments = listItems;
            return View();
        }
        [HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Create(EmployeeViewModel emplyeesVM)
        {
            //sever side validation
            var employee = _mapper.Map<EmployeeViewModel, Employee>(emplyeesVM);
            if (!ModelState.IsValid) return View(emplyeesVM);
			await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id) => await EmployeeControllerHandler(id, nameof(Details));
        public async Task<IActionResult> Edit(int? id) => await EmployeeControllerHandler(id, nameof(Edit));
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel emplyeesVM)
        {
            if (id != emplyeesVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var employee = _mapper.Map<EmployeeViewModel, Employee>(emplyeesVM);
                    _unitOfWork.Employees.Update(employee);
                    if (await _unitOfWork.SaveChangesAsync() >0)
                    {
                        TempData["Massage"] = $"employee {employee.Name} updated successfully";
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //log exeption 
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(emplyeesVM);

        }
        public async Task<IActionResult> Delete(int? id) => await EmployeeControllerHandler(id, nameof(Delete));

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDone(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employees = await _unitOfWork.Employees.GetAsync(id.Value);
            if (employees is null) return NotFound();
            try
            {
                _unitOfWork.Employees.Delete(employees);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(employees);
        }

        private async Task<IActionResult> EmployeeControllerHandler(int? id, string viewName)
        {
            //retrive department and send it to the view
            if(viewName == nameof(Edit))
            {
                var department = await _unitOfWork.Departments.GetAllAsync();
                SelectList listItems = new SelectList(department, "Id", "Name");
                ViewBag.Departments = listItems;
            }
            if (!id.HasValue) return BadRequest();
            var employees = await _unitOfWork.Employees.GetAsync(id.Value);

            if (employees is null) return NotFound();
            //var employeeVM = new EmployeeViewModel
            //{
            //    Address = employees.Address,
            //    Department = employees.Department,
            //    Age = employees.Age,
            //    Salary = employees.Salary,
            //    Email = employees.Email,
            //    IsActive = employees.IsActive,
            //    Name = employees.Name,
            //    Id = employees.Id,
            //    Phone = employees.Phone
            //};
            var employeeVM = _mapper.Map<EmployeeViewModel>(employees);

            return View(viewName, employeeVM);
        }
    }
}
