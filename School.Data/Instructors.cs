using System;
using System.Collections.Generic;

namespace School.Data
{
    public partial class Instructors
    {
        public Instructors()
        {
            CourseInstructors = new HashSet<CourseInstructors>();
        }

        public int InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? HireDate { get; set; }
        public bool Terminated { get; set; }

        public virtual ICollection<CourseInstructors> CourseInstructors { get; set; }
    }
}
