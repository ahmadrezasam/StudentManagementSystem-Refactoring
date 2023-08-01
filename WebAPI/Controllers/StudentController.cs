using API.API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly DataContext _context;
        public StudentController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students
            .Include(s => s.Courses)
            .ToListAsync();
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students
                    .Include(s => s.Courses)
                    .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            return student;
        }

        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Student/EnrollCourse/101/1
        [HttpPost("EnrollCourse/{studentId}/{courseId}")]
        public async Task<IActionResult> EnrollCourse(int studentId, int courseId)
        {
            var student = await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);

            var course = await _context.Courses.FindAsync(courseId);

            if (student == null || course == null)
            {
                return NotFound("Student or Course not found.");
            }

            if (course.EnrolledStudents >= course.MaxCapacity)
            {
                return BadRequest("Course is already full.");
            }

            // Check if the student is already enrolled in the course
            if (student.Courses.Any(c => c.CourseId == courseId))
            {
                return BadRequest("Student is already enrolled in the course.");
            }

            // Enroll the student in the course
            student.Courses.Add(course);
            course.EnrolledStudents++;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle any potential database update errors
                return StatusCode(500, $"Error enrolling student in the course: {ex.Message}");
            }

            return Ok($"Student with ID {studentId} successfully enrolled in the course with ID {courseId}.");
        }
    }
}