using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Student : Person
    {

        // Student ID (assuming an integer ID)
        public int StudentId { get; set; }

        // Student's course enrollment status
        public bool IsEnrolled { get; set; }

        // Student's current grade (assuming numeric grades)
        public double CGPA { get; set; }

        // Date when the student's enrollment started
        public DateTime EnrollmentStartDate { get; set; }

        // Date when the student's enrollment ended
        public DateTime? EnrollmentEndDate { get; set; }
            
        // Navigation property representing the courses the student is enrolled in
        [ForeignKey("FK_CourseId")]
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}