using System;
using System.Collections.Generic;

namespace School.DTOs
{   
    public partial class StudentGradeDTO
    {        
        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }

        public decimal? Grade { get; set; }

        public virtual CourseDTO Course { get; set; }

        public virtual PersonDTO Person { get; set; }
    }
}
