using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Instructor : Person
    {
        public int InstructorId { get; set; }
        public String? Title { get; set; }
        public String? AcademicDegree { get; set; }
        public String? AcademicMajor { get; set; }

        [ForeignKey("FK_CourseId")]
        public ICollection<Course> Courses { get; set; } = new List<Course>(); 
        
    }
}