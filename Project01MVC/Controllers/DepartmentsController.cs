using Demo.BusinessLogicLayer.Intrerfaces;
using Demo.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.PresentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
        //IGenaricRepository<Department> _repository;
        private IDepartmentRepository _repository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _repository = departmentRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            //retrive all departments
            var department =await _repository.GetAllAsync();
            return View(department);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Create(Department department)
        {
            //sever side validation
            if(!ModelState.IsValid) return View(department);
			 _repository.AddAsync(department);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id) => await DepartmentControllerHandler(id, nameof(Details));
        public async Task<IActionResult> Edit(int? id) => await DepartmentControllerHandler(id, nameof(Edit));
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Edit([FromRoute]int id,Department department)
        {
            if (id != department.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //log exeption 
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(department);
            
        }
        public async Task<IActionResult> Delete(int? id) => await DepartmentControllerHandler(id,nameof(Delete));

        [HttpPost,ActionName("Delete")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteDone(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = await _repository.GetAsync(id.Value);
            if (department is null) return NotFound();
            try
            {
                _repository.Delete(department);
                return RedirectToAction(nameof(Index));
                //
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(department);
        }

        private async Task<IActionResult> DepartmentControllerHandler(int? id ,string viewName)
        {
            //retrive department and send it to the view
            if (!id.HasValue) return BadRequest();
            var department =await _repository.GetAsync(id.Value);

            if (department is null) return NotFound();

            return View(viewName, department);
        }
    }
}
