using System;
using System.Collections.Generic;

namespace School.DTOs
{
    public class StudentCourseDTO
    {
        public int StudentID { get; set; }        
        public int CourseID { get; set; }
        public CourseDTO Course { get; set; }
        public decimal Grade { get; set; }
        public int EnrolledYear { get; set; }
        public string EnrolledSemester { get; set; }
        public bool Completed { get; set; }
        public bool Dropped { get; set; }
        public DateTime? DroppedTime { get; set; }
    }
}
