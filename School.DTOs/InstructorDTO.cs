using System;
using System.Collections.Generic;

namespace School.DTOs
{
    public class InstructorDTO
    {
        public int InstructorID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? HireDate { get; set; }

        public bool Terminated { get; set; }
    }
}
