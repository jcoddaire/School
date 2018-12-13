using System;
using System.Collections.Generic;

namespace School.DTOs
{
    public partial class DepartmentDTO
    {        
        public DepartmentDTO()
        {
            Courses = new HashSet<CourseDTO>();
        }
                
        public int DepartmentID { get; set; }
        
        public string Name { get; set; }
                
        public decimal Budget { get; set; }

        public DateTime StartDate { get; set; }

        public int? Administrator { get; set; }
                
        public virtual ICollection<CourseDTO> Courses { get; set; }
    }
}
