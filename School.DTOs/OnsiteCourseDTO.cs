using System;
using System.Collections.Generic;

namespace School.DTOs
{    
    public partial class OnsiteCourseDTO
    {
        
        public int CourseID { get; set; }
        
        public string Location { get; set; }

        public string Days { get; set; }
                
        public DateTime Time { get; set; }

        public virtual CourseDTO Course { get; set; }
    }
}
