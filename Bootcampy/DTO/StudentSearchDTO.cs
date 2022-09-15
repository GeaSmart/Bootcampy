using Bootcampy.Models;

namespace Bootcampy.DTO
{
    public class StudentSearchDTO
    {
        public Student Estudiante { get; set; }
        public List<Student> EstudiantesFiltrados { get; set; }
    }
}
