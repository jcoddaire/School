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

        #region People Methods

        /// <summary>
        /// Gets all people.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PersonDTO> GetAllPersons()
        {
            var sourcePeople = Database.Person.Select(
                x => new PersonDTO()
                {
                    PersonID = x.PersonId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    HireDate = x.HireDate,
                    EnrollmentDate = x.EnrollmentDate

                }).ToList();

            return sourcePeople;
        }

        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <param name="personID">The person identifier.</param>
        /// <returns>
        /// A person DTO object. If no person was found, returns null.
        /// </returns>
        public PersonDTO GetPerson(int personID)
        {
            if(personID <= 0)
            {
                return null;
            }

            var personOfInterest = Database.Person.Where(p => p.PersonId == personID).Select(
                x => new PersonDTO()
                {
                    PersonID = x.PersonId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    HireDate = x.HireDate,
                    EnrollmentDate = x.EnrollmentDate

                }).FirstOrDefault();

            if(personOfInterest != null && personOfInterest.PersonID > 0)
            {
                return personOfInterest;
            }

            return null;
        }

        /// <summary>
        /// Creates the person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        public PersonDTO CreatePerson(PersonDTO person)
        {
            var newPeep = new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                EnrollmentDate = person.EnrollmentDate,
                HireDate = person.HireDate
            };

            Database.Person.Add(newPeep);
            Database.SaveChanges();

            //leap of faith, apparently the newPeep object has the ID.
            person.PersonID = newPeep.PersonId;

            return person;
        }

        /// <summary>
        /// Updates the person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        public PersonDTO UpdatePerson(PersonDTO person)
        {
            var changedPeep = Database.Person.Where(p => p.PersonId == person.PersonID).FirstOrDefault();
            if(changedPeep == null || changedPeep.PersonId != person.PersonID)
            {
                throw new KeyNotFoundException("Could not find a matching item in the dataset.");
            }

            changedPeep.FirstName = person.FirstName;
            changedPeep.LastName = person.LastName;
            changedPeep.EnrollmentDate = person.EnrollmentDate;
            changedPeep.HireDate = person.HireDate;
            
            Database.Person.Attach(changedPeep);

            var entry = Database.Entry(changedPeep);
            entry.Property(e => e.FirstName).IsModified = true;
            entry.Property(e => e.LastName).IsModified = true;
            entry.Property(e => e.EnrollmentDate).IsModified = true;
            entry.Property(e => e.HireDate).IsModified = true;

            Database.SaveChanges();

            return person;
        }

        /// <summary>
        /// Deletes the person. If the person has Student Grades, this also removes the grades.
        /// </summary>
        /// <param name="personID">The person identifier.</param>
        /// <returns>
        /// The number of rows affected.
        /// If something failed, returns -1.
        /// </returns>
        public int DeletePerson(int personID)
        {
            var target = Database.Person.Where(x => x.PersonId == personID).FirstOrDefault();
            
            if(target != null && target.PersonId == personID)
            {
                Database.StudentGrade.RemoveRange(Database.StudentGrade.Where(x => x.StudentId == personID).ToList());
                Database.OfficeAssignment.RemoveRange(Database.OfficeAssignment.Where(x => x.InstructorId == personID).ToList());
                Database.Person.Remove(target);                
                return Database.SaveChanges();
            }
            return -1;
        }

        #endregion

        #region Department Methods

        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            var source = Database.Department.Select(
                x => new DepartmentDTO()
                {
                    DepartmentID = x.DepartmentId,
                    Name = x.Name,
                    Budget = x.Budget,
                    StartDate = x.StartDate,
                    Administrator = x.Administrator

                }).ToList();

            return source;
        }

        public DepartmentDTO GetDepartment(int departmentID)
        {
            if (departmentID <= 0)
            {
                return null;
            }

            var target = Database.Department.Where(d => d.DepartmentId == departmentID).Select(
                x => new DepartmentDTO()
                {
                    DepartmentID = x.DepartmentId,
                    Name = x.Name,
                    Budget = x.Budget,
                    StartDate = x.StartDate,
                    Administrator = x.Administrator

                }).FirstOrDefault();

            if (target != null && target.DepartmentID > 0)
            {
                return target;
            }

            return null;
        }

        public DepartmentDTO CreateDepartment(DepartmentDTO department)
        {
            var topID = GetAllDepartments().Max(x => x.DepartmentID) + 1;

            var obj = new Department
            {
                DepartmentId = topID,
                Name = department.Name,
                Budget = department.Budget,
                StartDate = department.StartDate,
                Administrator = department.Administrator
            };

            Database.Department.Add(obj);
            Database.SaveChanges();

            department.DepartmentID = topID;

            return department;
        }

        public DepartmentDTO UpdateDepartment(DepartmentDTO department)
        {
            var changed = Database.Department.Where(d => d.DepartmentId == department.DepartmentID).FirstOrDefault();
            if (changed == null || changed.DepartmentId != department.DepartmentID)
            {
                throw new KeyNotFoundException("Could not find a matching item in the dataset.");
            }

            changed.Name = department.Name;
            changed.Budget = department.Budget;
            changed.StartDate = department.StartDate;
            changed.Administrator = department.Administrator;

            Database.Department.Attach(changed);

            var entry = Database.Entry(changed);
            entry.Property(e => e.Name).IsModified = true;
            entry.Property(e => e.Budget).IsModified = true;
            entry.Property(e => e.StartDate).IsModified = true;
            entry.Property(e => e.Administrator).IsModified = true;

            Database.SaveChanges();

            return department;
        }

        public int DeleteDepartment(int departmentID)
        {

            var target = Database.Department.Where(x => x.DepartmentId == departmentID).FirstOrDefault();

            if (target != null && target.DepartmentId == departmentID)
            {
                Database.Department.Remove(target);
                return Database.SaveChanges();
            }
            return -1;
        }
        #endregion

        #region Course Methods
        
        /// <summary>
        /// Gets all courses.
        /// </summary>
        /// <returns>A list of courses.</returns>
        public IEnumerable<CourseDTO> GetAllCourses()
        {
            var courses = Database.Course.Select(
                x => new CourseDTO()
                {
                    CourseID = x.CourseId,
                    Title = x.Title,
                    Credits = x.Credits,
                    DepartmentID = x.DepartmentId
                    
                }).ToList();

            return courses;
        }

        /// <summary>
        /// Gets the course.
        /// </summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns>The Course DTO.</returns>
        public CourseDTO GetCourse(int courseID)
        {
            if (courseID <= 0)
            {
                return null;
            }

            var target = Database.Course.Where(c => c.CourseId == courseID).Select(
                x => new CourseDTO()
                {
                    CourseID = x.CourseId,
                    Title = x.Title,
                    Credits = x.Credits,
                    DepartmentID = x.DepartmentId

                }).FirstOrDefault();

            if (target != null && target.CourseID > 0)
            {
                return target;
            }

            return null;
        }

        /// <summary>
        /// Creates the course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns>The course.</returns>
        public CourseDTO CreateCourse(CourseDTO course)
        {
            var newItem = new Course
            {
                CourseId = course.CourseID,
                Title = course.Title,
                Credits = course.Credits,
                DepartmentId = course.DepartmentID
            };

            Database.Course.Add(newItem);
            Database.SaveChanges();

            return course;
        }

        /// <summary>
        /// Updates the course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Could not find a matching item in the dataset.</exception>
        public CourseDTO UpdateCourse(CourseDTO course)
        {
            var changedTarget = Database.Course.Where(p => p.CourseId == course.CourseID).FirstOrDefault();
            if (changedTarget == null || changedTarget.CourseId != course.CourseID)
            {
                throw new KeyNotFoundException("Could not find a matching item in the dataset.");
            }

            //changedTarget.CourseID = course.CourseID; //this should never happen. Otherwise that ^^^ check will fail.
            changedTarget.Title = course.Title;
            changedTarget.Credits = course.Credits;
            changedTarget.DepartmentId = course.DepartmentID;

            Database.Course.Attach(changedTarget);

            var entry = Database.Entry(changedTarget);
            //entry.Property(e => e.CourseID).IsModified = true;
            entry.Property(e => e.Title).IsModified = true;
            entry.Property(e => e.Credits).IsModified = true;
            entry.Property(e => e.DepartmentId).IsModified = true;

            Database.SaveChanges();

            return course;
        }

        /// <summary>
        /// Deletes the course.
        /// </summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns>The number of rows affected. Returns -1 if an error occured.</returns>
        public int DeleteCourse(int courseID)
        {
            var target = Database.Course.Where(x => x.CourseId == courseID).FirstOrDefault();

            if (target != null && target.CourseId == courseID)
            {
                Database.Course.Remove(target);
                return Database.SaveChanges();
            }
            return -1;
        }
        #endregion

        #region Office Assignments
        
        /// <summary>
        /// Gets the office assignment.
        /// </summary>
        /// <param name="instructorID">The person identifier.</param>
        /// <returns></returns>
        public OfficeAssignmentDTO GetOfficeAssignment(int instructorID)
        {
            if (instructorID <= 0)
            {
                return null;
            }

            var target = Database.OfficeAssignment.Where(c => c.InstructorId == instructorID).Select(
                x => new OfficeAssignmentDTO()
                {
                    InstructorID = x.InstructorId,
                    Location = x.Location,
                    Timestamp = x.Timestamp

                }).FirstOrDefault();

            if (target != null && target.InstructorID > 0)
            {
                return target;
            }

            return null;
        }

        /// <summary>
        /// Gets all office assignments.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OfficeAssignmentDTO> GetAllOfficeAssignments()
        {
            var results = Database.OfficeAssignment.Select(
               x => new OfficeAssignmentDTO()
               {
                   InstructorID = x.InstructorId,
                   Location = x.Location,
                   Timestamp = x.Timestamp

               }).ToList();

            return results;
        }

        /// <summary>
        /// Creates the office assignment.
        /// </summary>
        /// <param name="assignment">The assignment.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">assignment - Assignment cannot be null</exception>
        /// <exception cref="ArgumentOutOfRangeException">assignment.InstructorID - Must be greater than 0!</exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public OfficeAssignmentDTO CreateOfficeAssignment(OfficeAssignmentDTO assignment)
        {
            if(assignment == null)
            {
                throw new ArgumentNullException("assignment", "Assignment cannot be null");
            }
            if(assignment.InstructorID <= 0)
            {
                throw new ArgumentOutOfRangeException("assignment.InstructorID", "Must be greater than 0!");
            }

            if(!Database.Person.Any(x => x.PersonId == assignment.InstructorID))
            {
                throw new KeyNotFoundException($"The InstructorID '{assignment.InstructorID}' was not found in the system.");
            }

            DeleteOfficeAssignment(assignment.InstructorID);

            var newOfficeAssignment = new OfficeAssignment
            {
                InstructorId = assignment.InstructorID,
                Location = assignment.Location,
                Timestamp = assignment.Timestamp
            };

            Database.OfficeAssignment.Add(newOfficeAssignment);
            Database.SaveChanges();

            return assignment;
        }

        /// <summary>
        /// Updates the office assignment.
        /// </summary>
        /// <param name="assignment">The assignment.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OfficeAssignmentDTO UpdateOfficeAssignment(OfficeAssignmentDTO assignment)
        {
            var changedTarget = Database.OfficeAssignment.Where(p => p.InstructorId == assignment.InstructorID).FirstOrDefault();
            if (changedTarget == null || changedTarget.InstructorId != assignment.InstructorID)
            {
                throw new KeyNotFoundException("Could not find a matching item in the dataset.");
            }
                        
            if(DeleteOfficeAssignment(assignment.InstructorID) > 0)
            {
                assignment = CreateOfficeAssignment(assignment);
            }            

            return assignment;
        }

        /// <summary>
        /// Deletes the office assignment.
        /// </summary>
        /// <param name="personID">The person identifier.</param>
        /// <returns></returns>
        public int DeleteOfficeAssignment(int personID)
        {
            var target = Database.OfficeAssignment.Where(x => x.InstructorId == personID).FirstOrDefault();

            if (target != null && target.InstructorId == personID)
            {
                Database.OfficeAssignment.Remove(target);
                return Database.SaveChanges();
            }
            return -1;
        }
        #endregion

        #region Student Grade
        /// <summary>
        /// Gets the student grade.
        /// </summary>
        /// <param name="enrollmentID">The enrollment identifier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">enrollmentID - Must be greater than 0!</exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public StudentGradeDTO GetStudentGrade(int enrollmentID)
        {
            if (enrollmentID <= 0)
            {
                return null;
            }
            if (!Database.StudentGrade.Any(x => x.EnrollmentId == enrollmentID))
            {
                return null;
            }

            var result = Database.StudentGrade.Where(x => x.EnrollmentId == enrollmentID).FirstOrDefault();
            if(result != null)
            {
                var newResult = new StudentGradeDTO
                {
                    EnrollmentID = result.EnrollmentId,
                    CourseID = result.CourseId,
                    StudentID = result.StudentId,
                    Grade = result.Grade
                };

                return newResult;
            }
            return null; //this should never happen.
        }

        /// <summary>
        /// Gets all student grades.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StudentGradeDTO> GetAllStudentGrades()
        {
            var results = Database.StudentGrade.Select(
                x => new StudentGradeDTO()
                {
                    EnrollmentID = x.EnrollmentId,
                    CourseID = x.CourseId,
                    StudentID = x.StudentId,
                    Grade = x.Grade

                }).ToList();

            return results;
        }

        /// <summary>
        /// Adds the student grade.
        /// </summary>
        /// <param name="grade">The grade.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">grade - grade cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// grade.CourseID - Must be greater than 0!
        /// or
        /// grade.StudentID - Must be greater than 0!
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// </exception>
        public StudentGradeDTO AddStudentGrade(StudentGradeDTO grade)
        {
            if(grade == null)
            {
                throw new ArgumentNullException("grade", "grade cannot be null.");
            }

            if (grade.CourseID <= 0)
            {
                throw new ArgumentOutOfRangeException("grade.CourseID", "Must be greater than 0!");
            }
            if (!Database.Course.Any(x => x.CourseId == grade.CourseID))
            {
                throw new KeyNotFoundException($"The courseID '{grade.CourseID}' was not found in the system.");
            }

            if (grade.StudentID <= 0)
            {
                throw new ArgumentOutOfRangeException("grade.StudentID", "Must be greater than 0!");
            }
            if (!Database.Person.Any(x => x.PersonId == grade.StudentID))
            {
                throw new KeyNotFoundException($"The studentID '{grade.StudentID}' was not found in the system.");
            }
            
            var newTarget = new StudentGrade
            {
                CourseId = grade.CourseID,
                StudentId = grade.StudentID,
                Grade = grade.Grade
            };

            Database.StudentGrade.Add(newTarget);
            Database.SaveChanges();
                        
            grade.EnrollmentID = newTarget.EnrollmentId;

            return grade;
        }

        /// <summary>
        /// Updates the student grade.
        /// </summary>
        /// <param name="grade">The grade.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">grade - grade cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// grade.EnrollmentID - Must be greater than 0!
        /// or
        /// grade.CourseID - Must be greater than 0!
        /// or
        /// grade.StudentID - Must be greater than 0!
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// Could not find a matching item in the dataset.
        /// </exception>
        public StudentGradeDTO UpdateStudentGrade(StudentGradeDTO grade)
        {
            if (grade == null)
            {
                throw new ArgumentNullException("grade", "grade cannot be null.");
            }

            if (grade.EnrollmentID <= 0)
            {
                throw new ArgumentOutOfRangeException("grade.EnrollmentID", "Must be greater than 0!");
            }
            if (!Database.StudentGrade.Any(x => x.EnrollmentId == grade.EnrollmentID))
            {
                throw new KeyNotFoundException($"The enrollmentID '{grade.EnrollmentID}' was not found in the system.");
            }

            if (grade.CourseID <= 0)
            {
                throw new ArgumentOutOfRangeException("grade.CourseID", "Must be greater than 0!");
            }
            if (!Database.Course.Any(x => x.CourseId == grade.CourseID))
            {
                throw new KeyNotFoundException($"The courseID '{grade.CourseID}' was not found in the system.");
            }

            if (grade.StudentID <= 0)
            {
                throw new ArgumentOutOfRangeException("grade.StudentID", "Must be greater than 0!");
            }
            if (!Database.Person.Any(x => x.PersonId == grade.StudentID))
            {
                throw new KeyNotFoundException($"The studentID '{grade.StudentID}' was not found in the system.");
            }

            var changed = Database.StudentGrade.Where(p => p.EnrollmentId == grade.EnrollmentID).FirstOrDefault();
            if (changed == null || changed.EnrollmentId != grade.EnrollmentID)
            {
                //this should never happen.
                throw new KeyNotFoundException("Could not find a matching item in the dataset.");
            }

            changed.StudentId = grade.StudentID;
            changed.CourseId = grade.CourseID;
            changed.Grade = grade.Grade;

            Database.StudentGrade.Attach(changed);

            var entry = Database.Entry(changed);
            entry.Property(e => e.StudentId).IsModified = true;
            entry.Property(e => e.CourseId).IsModified = true;
            entry.Property(e => e.Grade).IsModified = true;            

            Database.SaveChanges();

            return grade;
        }

        /// <summary>
        /// Deletes the student grade.
        /// </summary>
        /// <param name="enrollmentID">The enrollment identifier.</param>
        /// <returns></returns>
        public int DeleteStudentGrade(int enrollmentID)
        {
            var target = Database.StudentGrade.Where(x => x.EnrollmentId == enrollmentID).FirstOrDefault();

            if (target != null && target.EnrollmentId == enrollmentID)
            {
                Database.StudentGrade.Remove(target);
                return Database.SaveChanges();
            }
            return -1;
        }

        #endregion

        #region Online Courses

        /// <summary>
        /// Gets all online courses.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OnlineCourseDTO> GetAllOnlineCourses()
        {
            var results = Database.OnlineCourse.Select(
               x => new OnlineCourseDTO()
               {                   
                   CourseID = x.CourseId,
                   URL = x.Url

               }).ToList();

            return results;
        }

        /// <summary>
        /// Gets the online course.
        /// </summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns></returns>
        public OnlineCourseDTO GetOnlineCourse(int courseID)
        {
            if (courseID <= 0)
            {
                return null;
            }
            if (!Database.OnlineCourse.Any(x => x.CourseId == courseID))
            {
                return null;
            }

            var result = Database.OnlineCourse.Where(x => x.CourseId == courseID).FirstOrDefault();
            if (result != null)
            {
                var newResult = new OnlineCourseDTO
                {
                    CourseID = result.CourseId,
                    URL = result.Url
                };

                return newResult;
            }
            return null; //this should never happen.
        }

        /// <summary>
        /// Adds the online course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        public OnlineCourseDTO AddOnlineCourse(OnlineCourseDTO course)
        {
            if(course == null)
            {
                throw new ArgumentNullException("course");
            }
            if (Database.Course.Any(x => x.CourseId == course.CourseID) == false)
            {
                throw new KeyNotFoundException($"The course ID {course.CourseID} was not found in the system.");
            }
            if (Database.OnlineCourse.Any(x => x.CourseId == course.CourseID))
            {
                DeleteOnlineCourse(course.CourseID);
            }

            var newItem = new OnlineCourse
            {
                CourseId = course.CourseID,
                Url = course.URL
            };

            Database.OnlineCourse.Add(newItem);
            Database.SaveChanges();

            return course;
        }

        /// <summary>
        /// Updates the online course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public OnlineCourseDTO UpdateOnlineCourse(OnlineCourseDTO course)
        {
            if(course == null)
            {
                throw new ArgumentNullException("course");
            }
            if (Database.Course.Any(x => x.CourseId == course.CourseID) == false)
            {
                throw new KeyNotFoundException($"The course ID {course.CourseID} was not found in the system.");
            }
            if (Database.OnlineCourse.Any(x => x.CourseId == course.CourseID))
            {
                DeleteOnlineCourse(course.CourseID);
            }
            
            AddOnlineCourse(course);

            return course;
        }

        /// <summary>
        /// Deletes the online course.
        /// </summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns></returns>
        public int DeleteOnlineCourse(int courseID)
        {
            var target = Database.OnlineCourse.Where(x => x.CourseId == courseID).FirstOrDefault();

            if (target != null && target.CourseId == courseID)
            {
                Database.OnlineCourse.Remove(target);
                return Database.SaveChanges();
            }
            return -1;
        }

        #endregion

        #region Onsite Courses

        /// <summary>
        /// Gets all onsite courses.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OnsiteCourseDTO> GetAllOnsiteCourses()
        {
            var results = Database.OnsiteCourse.Select(
              x => new OnsiteCourseDTO()
              {
                  CourseID = x.CourseId,
                  Location = x.Location,
                  Days = x.Days,
                  Time = x.Time

              }).ToList();

            return results;
        }

        /// <summary>
        /// Gets the onsite course.
        /// </summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns></returns>
        public OnsiteCourseDTO GetOnsiteCourse(int courseID)
        {
            if (courseID <= 0)
            {
                return null;
            }
            if (!Database.OnsiteCourse.Any(x => x.CourseId == courseID))
            {
                return null;
            }

            var result = Database.OnsiteCourse.Where(x => x.CourseId == courseID).FirstOrDefault();
            if (result != null)
            {
                var newResult = new OnsiteCourseDTO
                {
                    CourseID = result.CourseId,
                    Location = result.Location,
                    Days = result.Days,
                    Time = result.Time
                };

                return newResult;
            }
            return null; //this should never happen.
        }

        /// <summary>
        /// Adds the onsite course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">course</exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public OnsiteCourseDTO AddOnsiteCourse(OnsiteCourseDTO course)
        {
            if (course == null)
            {
                throw new ArgumentNullException("course");
            }
            if (Database.Course.Any(x => x.CourseId == course.CourseID) == false)
            {
                throw new KeyNotFoundException($"The course ID {course.CourseID} was not found in the system.");
            }
            if (Database.OnsiteCourse.Any(x => x.CourseId == course.CourseID))
            {
                DeleteOnsiteCourse(course.CourseID);
            }

            var newItem = new OnsiteCourse
            {
                CourseId = course.CourseID,
                Location = course.Location,
                Days = course.Days,
                Time = course.Time
            };

            Database.OnsiteCourse.Add(newItem);
            Database.SaveChanges();

            return course;
        }

        /// <summary>
        /// Updates the onsite course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public OnsiteCourseDTO UpdateOnsiteCourse(OnsiteCourseDTO course)
        {
            if(course == null)
            {
                throw new ArgumentNullException("course");
            }
            if (Database.Course.Any(x => x.CourseId == course.CourseID) == false)
            {
                throw new KeyNotFoundException($"The course ID {course.CourseID} was not found in the system.");
            }
            if (Database.OnsiteCourse.Any(x => x.CourseId == course.CourseID))
            {
                DeleteOnsiteCourse(course.CourseID);
            }

            AddOnsiteCourse(course);

            return course;
        }

        /// <summary>
        /// Deletes the onsite course.
        /// </summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns></returns>
        public int DeleteOnsiteCourse(int courseID)
        {
            var target = Database.OnsiteCourse.Where(x => x.CourseId == courseID).FirstOrDefault();

            if (target != null && target.CourseId == courseID)
            {
                Database.OnsiteCourse.Remove(target);
                return Database.SaveChanges();
            }
            return -1;
        }

        #endregion
    }
}
