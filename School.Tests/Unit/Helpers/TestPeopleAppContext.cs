using School.Data;
using School.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace School.Tests.Unit.Helpers
{
    [ExcludeFromCodeCoverage]
    public class TestPeopleAppContext : ISchoolData
    {
        public List<PersonDTO> People { get; set; }

        public TestPeopleAppContext()
        {
            People = new List<PersonDTO>();
        }

        public IEnumerable<PersonDTO> GetAllPersons()
        {
            return People;
        }

        public PersonDTO GetPerson(int personID)
        {
            return People.Where(x => x.PersonID == personID).FirstOrDefault();
        }

        public PersonDTO CreatePerson(PersonDTO person)
        {
            person.PersonID = People.Max(x => x.PersonID) + 1;

            People.Add(person);

            return person;
        }

        public PersonDTO UpdatePerson(PersonDTO person)
        {
            People.Remove(People.Where(x => x.PersonID == person.PersonID).First());
            People.Add(person);

            return person;
        }

        public int DeletePerson(int personID)
        {
            if(People.Remove(People.Where(x => x.PersonID == personID).First()))
            {
                return 1;
            }
            return -1;
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

        OfficeAssignmentDTO ISchoolData.GetOfficeAssignment(int personID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OfficeAssignmentDTO> GetAllOfficeAssignments()
        {
            throw new NotImplementedException();
        }

        OfficeAssignmentDTO ISchoolData.CreateOfficeAssignment(OfficeAssignmentDTO assignment)
        {
            throw new NotImplementedException();
        }

        OfficeAssignmentDTO ISchoolData.UpdateOfficeAssignment(OfficeAssignmentDTO assignment)
        {
            throw new NotImplementedException();
        }

        int ISchoolData.DeleteOfficeAssignment(int personID)
        {
            throw new NotImplementedException();
        }
        
        public StudentGradeDTO GetStudentGrade(int enrollmentID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentGradeDTO> GetAllStudentGrades()
        {
            throw new NotImplementedException();
        }

        public StudentGradeDTO AddStudentGrade(StudentGradeDTO grade)
        {
            throw new NotImplementedException();
        }

        public StudentGradeDTO UpdateStudentGrade(StudentGradeDTO grade)
        {
            throw new NotImplementedException();
        }

        public int DeleteStudentGrade(int enrollmentID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OnlineCourseDTO> GetAllOnlineCourses()
        {
            throw new NotImplementedException();
        }

        public OnlineCourseDTO GetOnlineCourse(int courseID)
        {
            throw new NotImplementedException();
        }

        public OnlineCourseDTO AddOnlineCourse(OnlineCourseDTO course)
        {
            throw new NotImplementedException();
        }

        public OnlineCourseDTO UpdateOnlineCourse(OnlineCourseDTO course)
        {
            throw new NotImplementedException();
        }

        public int DeleteOnlineCourse(int courseID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OnsiteCourseDTO> GetAllOnsiteCourses()
        {
            throw new NotImplementedException();
        }

        public OnsiteCourseDTO GetOnsiteCourse(int courseID)
        {
            throw new NotImplementedException();
        }

        public OnsiteCourseDTO AddOnsiteCourse(OnsiteCourseDTO course)
        {
            throw new NotImplementedException();
        }

        public OnsiteCourseDTO UpdateOnsiteCourse(OnsiteCourseDTO course)
        {
            throw new NotImplementedException();
        }

        public int DeleteOnsiteCourse(int courseID)
        {
            throw new NotImplementedException();
        }
    }
}
