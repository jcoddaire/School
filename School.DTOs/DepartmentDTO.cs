using System;
using System.Collections.Generic;

namespace School.DTOs
{
    public partial class DepartmentDTO
    {           
        public int DepartmentID { get; set; }
        
        public string Name { get; set; }
                
        public decimal Budget { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
