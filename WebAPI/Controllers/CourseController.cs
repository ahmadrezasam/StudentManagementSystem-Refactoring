using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private DataContext _context;
        public CourseController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // GET: api/Course/StudentList
        [HttpGet("{id}/StudentList")]
        public async Task<ActionResult<Course>> GetCourseStudentList(int id)
        {
            var courseStudentList = await _context.Courses
                .Include(s => s.Students)
                .FirstOrDefaultAsync(s => s.CourseId == id);
            
            try
            {
                return courseStudentList;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }

        }

        // POST: api/Course
        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, course);
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}