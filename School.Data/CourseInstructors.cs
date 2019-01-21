using System;
using System.Collections.Generic;

namespace School.Data
{
    public partial class CourseInstructors
    {
        public int CourseId { get; set; }
        public int InstructorId { get; set; }

        public virtual Courses Course { get; set; }
        public virtual Instructors Instructor { get; set; }
    }
}
