using System;
using System.Collections.Generic;

namespace School.Data
{
    public partial class StudentCourses
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal Grade { get; set; }
        public int EnrolledYear { get; set; }
        public string EnrolledSemester { get; set; }
        public bool Completed { get; set; }
        public bool Dropped { get; set; }
        public DateTime? DroppedTime { get; set; }

        public virtual Courses Course { get; set; }
        public virtual Students Student { get; set; }
    }
}
