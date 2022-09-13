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
 
    }
}
