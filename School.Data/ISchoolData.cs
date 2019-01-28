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
        /// <summary>
        /// Gets all course instructors.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CourseInstructorDTO> GetAllCourseInstructors();

        /// <summary>Gets a list of courses that instructors teach.</summary>
        /// <param name="instructorID">The instructor ID that teaches the courses.</param>
        IEnumerable<CourseInstructorDTO> GetCoursesByInstructor(int instructorID);

        /// <summary>
        /// Creates the course instructor.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        CourseInstructorDTO AssignCourseToInstructor(CourseInstructorDTO course);
        
        /// <summary>
        /// Deletes the course instructor.
        /// </summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns></returns>
        int RemoveCourseFromInstructor(CourseInstructorDTO course);

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

        #region Instructor Methods
        /// <summary>
        /// Gets all instructors.
        /// </summary>
        /// <returns></returns>
        IEnumerable<InstructorDTO> GetAllInstructors();

        /// <summary>
        /// Gets the instructor.
        /// </summary>
        /// <param name="instructorID">The instructor identifier.</param>
        /// <returns>
        /// A Instructor DTO object. If no instructor was found, returns null.
        /// </returns>
        InstructorDTO GetInstructor(int instructorID);

        /// <summary>
        /// Creates the instructor.
        /// </summary>
        /// <param name="instructor">The instructor.</param>
        /// <returns></returns>
        InstructorDTO CreateInstructor(InstructorDTO instructor);

        /// <summary>
        /// Updates the instructor.
        /// </summary>
        /// <param name="instructor">The instructor.</param>
        /// <returns></returns>
        InstructorDTO UpdateInstructor(InstructorDTO instructor);

        /// <summary>
        /// Deletes the instructor.
        /// </summary>
        /// <param name="instructorID">The instructor identifier.</param>
        /// <returns>
        /// The number of rows affected.
        /// If something failed, returns -1.
        /// </returns>
        int DeleteInstructor(int instructorID);
        #endregion

        #region Student Methods
        /// <summary>
        /// Gets all Students.
        /// </summary>
        /// <returns></returns>
        IEnumerable<StudentDTO> GetAllStudents();

        /// <summary>
        /// Gets the Student.
        /// </summary>
        /// <param name="StudentID">The Student identifier.</param>
        /// <returns>
        /// A Student DTO object. If no Student was found, returns null.
        /// </returns>
        StudentDTO GetStudent(int StudentID);

        /// <summary>
        /// Creates the Student.
        /// </summary>
        /// <param name="Student">The Student.</param>
        /// <returns></returns>
        StudentDTO CreateStudent(StudentDTO Student);

        /// <summary>
        /// Updates the Student.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        StudentDTO UpdateStudent(StudentDTO student);

        /// <summary>
        /// Deletes the Student.
        /// </summary>
        /// <param name="studentID">The Student identifier.</param>
        /// <returns>
        /// The number of rows affected.
        /// If something failed, returns -1.
        /// </returns>
        int DeleteStudent(int StudentID);
        #endregion

        #region StudentCourses Methods
        /// <summary>
        /// Gets all StudentCourses.
        /// </summary>
        /// <returns></returns>
        IEnumerable<StudentCourseDTO> GetAllStudentCourses();

        /// <summary>
        /// Gets all courses associated with the given student.
        /// </summary>
        /// <param name="studentID">The Student ID.</param>
        /// <returns>
        /// A StudentCourse DTO object. If no StudentCourse was found, returns null.
        /// </returns>
        IEnumerable<StudentCourseDTO> GetStudentCourse(int studentID);

        /// <summary>
        /// Creates the StudentCourse.
        /// </summary>
        /// <param name="StudentCourse">The StudentCourse.</param>
        /// <returns></returns>
        StudentCourseDTO CreateStudentCourse(StudentCourseDTO StudentCourse);
        
        /// <summary>
        /// Deletes the StudentCourse.
        /// </summary>
        /// <param name="StudentCourseID">The StudentCourse identifier.</param>
        /// <returns>
        /// The number of rows affected.
        /// If something failed, returns -1.
        /// </returns>
        int DeleteStudentCourse(int studentID, int cousrseID, int enrolledYear, string enrolledSemester);
        #endregion
    }
}
