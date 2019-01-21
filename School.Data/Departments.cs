using System;
using System.Collections.Generic;

namespace School.Data
{
    public partial class Departments
    {
        public Departments()
        {
            Courses = new HashSet<Courses>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Courses> Courses { get; set; }
    }
}
