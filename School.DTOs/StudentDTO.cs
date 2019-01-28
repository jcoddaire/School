using System;
using System.Collections.Generic;

namespace School.DTOs
{
    public class StudentDTO
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? EnrollmentDate { get; set; }
        public List<StudentCourseDTO> Courses { get; set; }
    }
}
