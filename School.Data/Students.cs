using System;
using System.Collections.Generic;

namespace School.Data
{
    public partial class Students
    {
        public Students()
        {
            StudentCourses = new HashSet<StudentCourses>();
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? EnrollmentDate { get; set; }

        public virtual ICollection<StudentCourses> StudentCourses { get; set; }
    }
}
