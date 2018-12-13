using System;
using System.Collections.Generic;

namespace School.DTOs
{
    public partial class OfficeAssignmentDTO
    {
        public int InstructorID { get; set; }

        public string Location { get; set; }
        
        public byte[] Timestamp { get; set; }

        public virtual PersonDTO Person { get; set; }
    }
}
