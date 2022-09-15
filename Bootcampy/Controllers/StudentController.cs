using Bootcampy.Models;
using Bootcampy.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Bootcampy.Controllers
{
    public class StudentController : Controller
    {
        private readonly IRepository<Student> repository;

        public StudentController(IRepository<Student> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var estudiantes = await repository.ReadAllAsync();
            return View(estudiantes);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            var student = await repository.ReadOneAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit([FromForm] Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.Id == 0)
                    await repository.CreateAsync(student);
                else
                    await repository.UpdateAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
