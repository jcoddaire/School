using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data
{
    public partial class Courses
    {
        public Courses()
        {
            CourseInstructors = new HashSet<CourseInstructors>();
            StudentCourses = new HashSet<StudentCourses>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }

        public virtual Departments Department { get; set; }
        public virtual ICollection<CourseInstructors> CourseInstructors { get; set; }
        public virtual ICollection<StudentCourses> StudentCourses { get; set; }
    }
}
