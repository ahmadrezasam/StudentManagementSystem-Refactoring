using API.API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : ControllerBase
    {
        private DataContext _context;

        public InstructorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Instructor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instructor>>> GetInstructors()
        {
            return await _context.Instructors
            .Include(i => i.Courses)
            .ToListAsync();
        }

        // GET: api/Instructor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instructor>> GetInstructor(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }

            return instructor;
        }

        // POST: api/Instructor
        [HttpPost]
        public async Task<ActionResult<Instructor>> CreateInstructor(Instructor instructor)
        {
            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInstructor), new { id = instructor.InstructorId }, instructor);
        }

        // DELETE: api/Instructor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var instructor = await _context.Instructors
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(i => i.InstructorId == id);

            if (instructor == null)
            {
                return NotFound();
            }

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Instructor/AssignCourse/instructorId/courseId
        [HttpPost("AssignCourse/{instructorId}/{courseId}")]
        public async Task<IActionResult> AssignCourse(int instructorId, int courseId)
        {
            var instructor = await _context.Instructors
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(i => i.InstructorId == instructorId);
            var course = await _context.Courses.FindAsync(courseId);

            if (instructor == null || course == null)
            {
                return NotFound("Instructor or Course not found.");
            }

            // Check if the course is already assigned to the instructor
            if (instructor.Courses.Any(c => c.CourseId == courseId))
            {
                return BadRequest("Course is already assigned to the instructor.");
            }

            // Assign the course to the instructor
            instructor.Courses.Add(course);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle any potential database update errors
                return StatusCode(500, $"Error assigning course to the instructor: {ex.Message}");
            }

            return Ok($"Course with ID {courseId} successfully assigned to the instructor with ID {instructorId}.");
        }

        // POST: api/lettergrade/courseId/students/studentId/grade
        [HttpPost("{instructorId}/lettergrades/{courseId}/students/{studentId}/grade")]
        public async Task<IActionResult> AddLetterGrade(int studentId, int courseId, int instructorId, string letterGrade)
        {
            // Find the student and course
            var course = await _context.Courses.FindAsync(courseId);
            var student = await _context.Students.FindAsync(studentId);
            var instructor = await _context.Instructors.FindAsync(instructorId);

            if (student == null || course == null)
            {
                return NotFound("Student or Course not found.");
            }

            var enrollment = student.Courses.FirstOrDefault(c => c.CourseId == courseId);
            // Check if the student is enrolled in the course
            if (enrollment == null)
            {
                return BadRequest("Student is not enrolled in the course.");
            }

            // Check if the instructor is authorized to add grades (you can add your authorization logic here)


            if (instructor?.Courses.Any(c => c.CourseId == courseId) != true)
            {
                return BadRequest("Instructor does not teach the course.");
            }

            enrollment.LetterGrade = LetterGrade.GetLetterGrade(letterGrade);
            await _context.SaveChangesAsync();

            return Ok($"Successfully added letter grade '{letterGrade}' for student with ID {studentId} in the course with ID {courseId}.");
        }
    }
}