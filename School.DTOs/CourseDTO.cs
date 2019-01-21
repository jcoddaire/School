using System;
using System.Collections.Generic;

namespace School.DTOs
{    
    public partial class CourseDTO
    {                
        public int CourseID { get; set; }

        public string Name { get; set; }

        public int Credits { get; set; }

        public int DepartmentID { get; set; }
    }
}
