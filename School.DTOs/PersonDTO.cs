using System;
using System.Collections.Generic;

namespace School.DTOs
{
    public class PersonDTO
    {
        public int PersonID { get; set; }
        
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime? HireDate { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        public virtual OfficeAssignmentDTO OfficeAssignment { get; set; }
        
        public virtual ICollection<StudentGradeDTO> StudentGrades { get; set; }

        public virtual ICollection<CourseDTO> Courses { get; set; }
    }
}
