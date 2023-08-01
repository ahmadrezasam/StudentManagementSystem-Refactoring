using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Course
    {
        // Course ID (assuming an integer ID)
        public int CourseId { get; set; }

        // Course name
        public string CourseName { get; set; }

        // Course description
        public string Description { get; set; }

        //Course section number autoincrement starting from 1 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Section { get; set; }

        // Start date of the course
        public DateTime StartDate { get; set; }

        // End date of the course
        public DateTime EndDate { get; set; }

        // Maximum number of students allowed in the course
        public int MaxCapacity { get; set; }

        // Course letter grades
        public String? LetterGrade { get; set; }

        // Credits of the course
        public int Credit { get; set; }

        // Current number of enrolled students in the course
        public int EnrolledStudents { get; set; }

        // Navigation property representing the instructors teaching the course
        [ForeignKey("FK_InstructorId")]
        public ICollection<Instructor>? Instructors { get; set; }
        
        // Navigation property representing the students enrolled in the course
        [ForeignKey("FK_StudentId")]
        public ICollection<Student>? Students { get; set; }
    }
}