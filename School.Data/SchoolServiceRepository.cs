using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.DTOs;

namespace School.Data
{
    public class SchoolServiceRepository : ISchoolData
    {
        private SchoolContext _Database = null;

        private SchoolContext Database
        {
            get
            {
                if (_Database == null)
                {
                    _Database = new SchoolContext();
                }

                return _Database;
            }
        }
        public SchoolServiceRepository(SchoolContext db)
        {
            _Database = db;
        }

        public IEnumerable<CourseDTO> GetAllCourses()
        {
            throw new NotImplementedException();
        }

        public CourseDTO GetCourse(int courseID)
        {
            throw new NotImplementedException();
        }

        public CourseDTO CreateCourse(CourseDTO course)
        {
            throw new NotImplementedException();
        }

        public CourseDTO UpdateCourse(CourseDTO course)
        {
            throw new NotImplementedException();
        }

        public int DeleteCourse(int courseID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CourseInstructorDTO> GetAllCourseInstructors()
        {
            throw new NotImplementedException();
        }

        public CourseInstructorDTO CreateCourseInstructor(CourseInstructorDTO course)
        {
            throw new NotImplementedException();
        }

        public CourseInstructorDTO UpdateCourseInstructor(CourseInstructorDTO course)
        {
            throw new NotImplementedException();
        }

        public int DeleteCourseInstructor(CourseInstructorDTO course)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            throw new NotImplementedException();
        }

        public DepartmentDTO GetDepartment(int departmentID)
        {
            throw new NotImplementedException();
        }

        public DepartmentDTO CreateDepartment(DepartmentDTO department)
        {
            throw new NotImplementedException();
        }

        public DepartmentDTO UpdateDepartment(DepartmentDTO department)
        {
            throw new NotImplementedException();
        }

        public int DeleteDepartment(int departmentID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InstructorDTO> GetAllInstructors()
        {
            throw new NotImplementedException();
        }

        public InstructorDTO GetInstructor(int instructorID)
        {
            throw new NotImplementedException();
        }

        public InstructorDTO CreateInstructor(InstructorDTO instructor)
        {
            throw new NotImplementedException();
        }

        public InstructorDTO UpdateInstructor(InstructorDTO instructor)
        {
            throw new NotImplementedException();
        }

        public int DeleteInstructor(int instructorID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentDTO> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        public StudentDTO GetStudent(int StudentID)
        {
            throw new NotImplementedException();
        }

        public StudentDTO CreateStudent(StudentDTO Student)
        {
            throw new NotImplementedException();
        }

        public StudentDTO UpdateStudent(StudentDTO student)
        {
            throw new NotImplementedException();
        }

        public int DeleteStudent(int StudentID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentCourseDTO> GetAllStudentCourses()
        {
            throw new NotImplementedException();
        }

        public StudentCourseDTO GetStudentCourse(int StudentCourseID)
        {
            throw new NotImplementedException();
        }

        public StudentCourseDTO CreateStudentCourse(StudentCourseDTO StudentCourse)
        {
            throw new NotImplementedException();
        }

        public StudentCourseDTO UpdateStudentCourse(StudentCourseDTO StudentCourse)
        {
            throw new NotImplementedException();
        }

        public int DeleteStudentCourse(int StudentCourseID)
        {
            throw new NotImplementedException();
        }
    }
}
