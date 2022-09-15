using Bootcampy.DTO;
using Bootcampy.Models;
using Bootcampy.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Expressions;

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
            var dto = new StudentSearchDTO();
            if (TempData["myObject"] != null)
            {
                dto = JsonConvert.DeserializeObject<StudentSearchDTO>((string)TempData["myObject"]);
            }
            else
            {
                dto.EstudiantesFiltrados = await repository.ReadAllAsync();
            }
            return View(dto);
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
                Expression<Func<Student, bool>> filter = m => m.Email.Contains(student.Email);
                var estudiantesFiltrados = await repository.ReadAllAsync(filter);
                if (estudiantesFiltrados.Count >0)
                {
                    ModelState.AddModelError("", "Ya existe un estudiante registrado con ese email");
                    return View(student);
                }                

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

        [HttpPost]
        public async Task<IActionResult> Search([FromForm] StudentSearchDTO studentDTO)
        {
            var listado = new List<Student>();
            Expression<Func<Student, bool>> filter = m => m.Name.Contains(studentDTO.Estudiante.Name);

            if (string.IsNullOrEmpty(studentDTO.Estudiante.Name))
                listado = await repository.ReadAllAsync();
            else
                listado = await repository.ReadAllAsync(filter);

            var listadoBusqueda = new StudentSearchDTO() { Estudiante = studentDTO.Estudiante, EstudiantesFiltrados = listado };
            TempData["myObject"] = JsonConvert.SerializeObject(listadoBusqueda);

            return RedirectToAction("Index");
        }
    }
}
