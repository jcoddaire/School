using System;
using System.Collections.Generic;

namespace School.DTOs
{    
    public partial class CourseDTO
    {        
        public CourseDTO()
        {
            StudentGrades = new HashSet<StudentGradeDTO>();
            People = new HashSet<PersonDTO>();
        }
                
        public int CourseID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public int DepartmentID { get; set; }

        public virtual DepartmentDTO Department { get; set; }

        public virtual OnlineCourseDTO OnlineCourse { get; set; }

        public virtual OnsiteCourseDTO OnsiteCourse { get; set; }
        
        public virtual ICollection<StudentGradeDTO> StudentGrades { get; set; }
        
        public virtual ICollection<PersonDTO> People { get; set; }
    }
}
