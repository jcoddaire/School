using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.DTOs;

namespace School.Data
{
    public interface ISchoolData
    {
        #region Course Methods

        /// <summary>
        /// Gets all courses.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CourseDTO> GetAllCourses();

        /// <summary>
        /// Gets the course.
        /// </summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns></returns>
        CourseDTO GetCourse(int courseID);

        /// <summary>
        /// Creates the course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        CourseDTO CreateCourse(CourseDTO course);

        /// <summary>
        /// Updates the course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        CourseDTO UpdateCourse(CourseDTO course);

        /// <summary>
        /// Deletes the course.
        /// </summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns></returns>
        int DeleteCourse(int courseID);

        #endregion

        #region Course Instructor Methods


        #endregion

        #region Department Methods

        /// <summary>
        /// Gets all departments.
        /// </summary>
        /// <returns></returns>
        IEnumerable<DepartmentDTO> GetAllDepartments();

        /// <summary>
        /// Gets the department.
        /// </summary>
        /// <param name="departmentID">The department identifier.</param>
        /// <returns></returns>
        DepartmentDTO GetDepartment(int departmentID);

        /// <summary>
        /// Creates the department.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        DepartmentDTO CreateDepartment(DepartmentDTO department);

        /// <summary>
        /// Updates the department.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        DepartmentDTO UpdateDepartment(DepartmentDTO department);

        /// <summary>
        /// Deletes the department.
        /// </summary>
        /// <param name="departmentID">The department identifier.</param>
        /// <returns></returns>
        int DeleteDepartment(int departmentID);

        #endregion

        #region Office Assignment Methods

        /// <summary>
        /// Gets all office assignments.
        /// </summary>
        /// <returns></returns>
        IEnumerable<OfficeAssignmentDTO> GetAllOfficeAssignments();

        /// <summary>
        /// Gets the office assignment.
        /// </summary>
        /// <param name="instructorID">The person identifier.</param>
        /// <returns></returns>
        OfficeAssignmentDTO GetOfficeAssignment(int instructorID);

        /// <summary>
        /// Creates the office assignment.
        /// If the InstructorID is already assigned to another office, this method
        /// will delete the instructor's prior assignment, then assign the new location.
        /// </summary>
        /// <param name="assignment">The assignment.</param>
        /// <returns></returns>
        OfficeAssignmentDTO CreateOfficeAssignment(OfficeAssignmentDTO assignment);

        /// <summary>
        /// Updates the office assignment.
        /// </summary>
        /// <param name="assignment">The assignment.</param>
        /// <returns></returns>
        OfficeAssignmentDTO UpdateOfficeAssignment(OfficeAssignmentDTO assignment);

        /// <summary>
        /// Deletes the office assignment.
        /// </summary>
        /// <param name="personID">The person identifier.</param>
        /// <returns></returns>
        int DeleteOfficeAssignment(int personID);

        #endregion

        #region Online Course Methods

        /// <summary>
        /// Gets all online courses.
        /// </summary>
        /// <returns></returns>
        IEnumerable<OnlineCourseDTO> GetAllOnlineCourses();

        /// <summary>
        /// Gets the online course.
        /// </summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns></returns>
        OnlineCourseDTO GetOnlineCourse(int courseID);

        /// <summary>
        /// Adds the online course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        OnlineCourseDTO AddOnlineCourse(OnlineCourseDTO course);

        /// <summary>
        /// Updates the online course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        OnlineCourseDTO UpdateOnlineCourse(OnlineCourseDTO course);

        /// <summary>
        /// Deletes the online course.
        /// </summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns></returns>
        int DeleteOnlineCourse(int courseID);

        #endregion

        #region Onsite Course Methods

        /// <summary>
        /// Gets all onsite courses.
        /// </summary>
        /// <returns></returns>
        IEnumerable<OnsiteCourseDTO> GetAllOnsiteCourses();

        /// <summary>
        /// Gets the onsite course.
        /// </summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns></returns>
        OnsiteCourseDTO GetOnsiteCourse(int courseID);

        /// <summary>
        /// Adds the onsite course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        OnsiteCourseDTO AddOnsiteCourse(OnsiteCourseDTO course);

        /// <summary>
        /// Updates the onsite course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        OnsiteCourseDTO UpdateOnsiteCourse(OnsiteCourseDTO course);

        /// <summary>
        /// Deletes the onsite course.
        /// </summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns></returns>
        int DeleteOnsiteCourse(int courseID);

        #endregion

        #region Person Methods
        /// <summary>
        /// Gets all people.
        /// </summary>
        /// <returns></returns>
        IEnumerable<PersonDTO> GetAllPersons();

        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <param name="personID">The person identifier.</param>
        /// <returns>
        /// A person DTO object. If no person was found, returns null.
        /// </returns>
        PersonDTO GetPerson(int personID);

        /// <summary>
        /// Creates the person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        PersonDTO CreatePerson(PersonDTO person);

        /// <summary>
        /// Updates the person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        PersonDTO UpdatePerson(PersonDTO person);

        /// <summary>
        /// Deletes the person.
        /// </summary>
        /// <param name="personID">The person identifier.</param>
        /// <returns>
        /// The number of rows affected.
        /// If something failed, returns -1.
        /// </returns>
        int DeletePerson(int personID);
        #endregion

        #region Student Grade Methods

        /// <summary>
        /// Gets the student grade.
        /// </summary>
        /// <param name="enrollmentID">The enrollment identifier.</param>
        /// <returns></returns>
        StudentGradeDTO GetStudentGrade(int enrollmentID);

        /// <summary>
        /// Gets all student grades.
        /// </summary>
        /// <returns></returns>
        IEnumerable<StudentGradeDTO> GetAllStudentGrades();

        /// <summary>
        /// Adds the student grade.
        /// </summary>
        /// <param name="grade">The grade.</param>
        /// <returns></returns>
        StudentGradeDTO AddStudentGrade(StudentGradeDTO grade);

        /// <summary>
        /// Updates the student grade.
        /// </summary>
        /// <param name="grade">The grade.</param>
        /// <returns></returns>
        StudentGradeDTO UpdateStudentGrade(StudentGradeDTO grade);

        /// <summary>
        /// Deletes the student grade.
        /// </summary>
        /// <param name="enrollmentID">The enrollment identifier.</param>
        /// <returns></returns>
        int DeleteStudentGrade(int enrollmentID);

        #endregion
    }
}
