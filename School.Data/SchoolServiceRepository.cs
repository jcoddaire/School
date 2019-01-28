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
            var s = Database.Courses.Select(
                a => new CourseDTO()
                {
                    CourseID = a.CourseId,
                    Name = a.Name,
                    Credits = a.Credits,
                    DepartmentID = a.Department.DepartmentId
                }).AsEnumerable<CourseDTO>();

            return s;
        }

        public CourseDTO GetCourse(int courseID)
        {
            if(courseID <= 0)
            {
                return null;
            }

            var target = Database.Courses.Where(c => c.CourseId == courseID)
                .Select(a => new CourseDTO()
                {
                    CourseID = a.CourseId,
                    Name = a.Name,
                    Credits = a.Credits,
                    DepartmentID = a.Department.DepartmentId

                }).FirstOrDefault();

            return target;
        }

        public CourseDTO CreateCourse(CourseDTO course)
        {
            var newObj = new Courses
            {
                CourseId = course.CourseID,
                Name = course.Name,
                Credits = course.Credits,
                DepartmentId = course.DepartmentID
            };

            Database.Courses.Add(newObj);
            Database.SaveChanges();

            course.CourseID = newObj.CourseId;

            return course;
        }

        public CourseDTO UpdateCourse(CourseDTO course)
        {
            var changedObj = Database.Courses.Where(p => p.CourseId == course.CourseID).FirstOrDefault();
            if (changedObj == null || changedObj.CourseId != course.CourseID)
            {
                throw new KeyNotFoundException("Could not find a matching course in the system.");
            }

            changedObj.CourseId = course.CourseID;
            changedObj.Name = course.Name;
            changedObj.Credits = course.Credits;
            changedObj.DepartmentId = course.DepartmentID;

            Database.Courses.Attach(changedObj);

            var entry = Database.Entry(changedObj);
            entry.Property(e => e.Name).IsModified = true;
            entry.Property(e => e.Credits).IsModified = true;
            entry.Property(e => e.DepartmentId).IsModified = true;

            Database.SaveChanges();

            return course;
        }

        public int DeleteCourse(int courseID)
        {
            var target = Database.Courses.Where(x => x.CourseId == courseID).FirstOrDefault();

            if (target != null && target.CourseId == courseID)
            {
                Database.Courses.Remove(target);
                return Database.SaveChanges();
            }
            return -1;
        }

        public IEnumerable<CourseInstructorDTO> GetAllCourseInstructors()
        {
            var s = Database.CourseInstructors.Select(
                a => new CourseInstructorDTO()
                {
                    CourseID = a.CourseId,
                    InstructorID = a.InstructorId

                }).AsEnumerable<CourseInstructorDTO>();

            return s;
        }

        public CourseInstructorDTO CreateCourseInstructor(CourseInstructorDTO course)
        {
            var newObj = new CourseInstructors
            {
                CourseId = course.CourseID,
                InstructorId = course.InstructorID
            };

            Database.CourseInstructors.Add(newObj);
            Database.SaveChanges();                       

            return course;
        }
        
        public int DeleteCourseInstructor(CourseInstructorDTO course)
        {
            var target = Database.CourseInstructors.Where(x => x.CourseId == course.CourseID && x.InstructorId == course.InstructorID).FirstOrDefault();

            if (target != null && target.CourseId == course.CourseID && target.InstructorId == course.InstructorID)
            {
                Database.CourseInstructors.Remove(target);
                return Database.SaveChanges();
            }
            return -1;
        }

        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            var s = Database.Departments.Select(
                a => new DepartmentDTO()
                {
                    DepartmentID = a.DepartmentId,
                    Name = a.Name,
                    Budget = a.Budget,
                    CreatedDate = a.CreatedDate

                }).AsEnumerable<DepartmentDTO>();

            return s;
        }

        public DepartmentDTO GetDepartment(int departmentID)
        {
            if (departmentID <= 0)
            {
                return null;
            }

            var target = Database.Departments.Where(c => c.DepartmentId == departmentID)
                .Select(a => new DepartmentDTO()
                {
                    DepartmentID = a.DepartmentId,
                    Name = a.Name,
                    Budget = a.Budget,
                    CreatedDate = a.CreatedDate

                }).FirstOrDefault();

            return target;
        }

        public DepartmentDTO CreateDepartment(DepartmentDTO department)
        {

            var newObj = new Departments
            {
                DepartmentId = department.DepartmentID,
                Name = department.Name,
                Budget = department.Budget,
                CreatedDate = department.CreatedDate
            };

            Database.Departments.Add(newObj);
            Database.SaveChanges();

            department.DepartmentID = newObj.DepartmentId;

            return department;
        }

        public DepartmentDTO UpdateDepartment(DepartmentDTO department)
        {
            var changedObj = Database.Departments.Where(p => p.DepartmentId == department.DepartmentID).FirstOrDefault();
            if (changedObj == null || changedObj.DepartmentId != department.DepartmentID)
            {
                throw new KeyNotFoundException("Could not find a matching department in the system.");
            }

            changedObj.DepartmentId = department.DepartmentID;
            changedObj.Name = department.Name;
            changedObj.Budget = department.Budget;
            changedObj.CreatedDate = department.CreatedDate;

            Database.Departments.Attach(changedObj);

            var entry = Database.Entry(changedObj);
            entry.Property(e => e.Name).IsModified = true;
            entry.Property(e => e.Budget).IsModified = true;
            entry.Property(e => e.CreatedDate).IsModified = true;

            Database.SaveChanges();

            return department;
        }

        public int DeleteDepartment(int departmentID)
        {
            var target = Database.Departments.Where(x => x.DepartmentId == departmentID).FirstOrDefault();

            if (target != null && target.DepartmentId == departmentID)
            {
                Database.Departments.Remove(target);
                return Database.SaveChanges();
            }
            return -1;
        }

        public IEnumerable<InstructorDTO> GetAllInstructors()
        {
            var s = Database.Instructors.Select(
                a => new InstructorDTO()
                {
                    InstructorID = a.InstructorId,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    HireDate = a.HireDate,
                    Terminated = a.Terminated

                }).AsEnumerable<InstructorDTO>();


            var allCourseInstructors = Database.CourseInstructors.AsEnumerable();
            var allCourses = GetAllCourses();

            if (allCourseInstructors == null || allCourseInstructors.Count() <= 0)
            {
                return s;
            }
            if (allCourses == null || allCourses.Count() <= 0)
            {
                return s;
            }

            foreach (var i in s)
            {
                i.Courses = new List<CourseDTO>();
                var currentCourses = allCourseInstructors.Where(c => c.InstructorId == i.InstructorID).AsEnumerable();
                if(currentCourses != null && currentCourses.Count() > 0)
                {
                    foreach(var cc in currentCourses)
                    {
                        var dto = new CourseDTO();
                        dto.CourseID = cc.CourseId;

                        var courseDetail = allCourses.Where(x => x.CourseID == cc.CourseId).FirstOrDefault();
                        if(courseDetail != null)
                        {
                            dto.Name = courseDetail.Name;
                            dto.Credits = courseDetail.Credits;
                            dto.DepartmentID = courseDetail.DepartmentID;
                        }

                        i.Courses.Add(dto);
                    }
                }
            }

            return s;
        }

        public InstructorDTO GetInstructor(int instructorID)
        {
            if (instructorID <= 0)
            {
                return null;
            }

            var target = Database.Instructors.Where(c => c.InstructorId == instructorID)
                .Select(a => new InstructorDTO()
                {
                    InstructorID = a.InstructorId,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    HireDate = a.HireDate,
                    Terminated = a.Terminated

                }).FirstOrDefault();

            return target;
        }

        public InstructorDTO CreateInstructor(InstructorDTO instructor)
        {
            var newObj = new Instructors
            {
                InstructorId = instructor.InstructorID,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                HireDate = instructor.HireDate,
                Terminated = instructor.Terminated
            };

            Database.Instructors.Add(newObj);
            Database.SaveChanges();

            instructor.InstructorID = newObj.InstructorId;

            return instructor;
        }

        public InstructorDTO UpdateInstructor(InstructorDTO instructor)
        {
            var changedObj = Database.Instructors.Where(p => p.InstructorId == instructor.InstructorID).FirstOrDefault();
            if (changedObj == null || changedObj.InstructorId != instructor.InstructorID)
            {
                throw new KeyNotFoundException("Could not find a matching instructor in the system.");
            }

            changedObj.InstructorId = instructor.InstructorID;
            changedObj.FirstName = instructor.FirstName;
            changedObj.LastName = instructor.LastName;
            changedObj.HireDate = instructor.HireDate;
            changedObj.Terminated = instructor.Terminated;

            Database.Instructors.Attach(changedObj);

            var entry = Database.Entry(changedObj);
            entry.Property(e => e.FirstName).IsModified = true;
            entry.Property(e => e.LastName).IsModified = true;
            entry.Property(e => e.HireDate).IsModified = true;
            entry.Property(e => e.Terminated).IsModified = true;

            Database.SaveChanges();

            return instructor;
        }

        public int DeleteInstructor(int instructorID)
        {
            var target = Database.Instructors.Where(x => x.InstructorId == instructorID).FirstOrDefault();

            if (target != null && target.InstructorId == instructorID)
            {
                Database.Instructors.Remove(target);
                return Database.SaveChanges();
            }
            return -1;
        }

        public IEnumerable<StudentDTO> GetAllStudents()
        {
            var s = Database.Students.Select(
                a => new StudentDTO()
                {
                    StudentID = a.StudentId,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    EnrollmentDate = a.EnrollmentDate

                }).AsEnumerable<StudentDTO>();

            return s;
        }

        public StudentDTO GetStudent(int StudentID)
        {
            if (StudentID <= 0)
            {
                return null;
            }

            var target = Database.Students.Where(c => c.StudentId == StudentID)
                .Select(a => new StudentDTO()
                {
                    StudentID = a.StudentId,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    EnrollmentDate = a.EnrollmentDate

                }).FirstOrDefault();

            return target;
        }

        public StudentDTO CreateStudent(StudentDTO Student)
        {
            var newObj = new Students
            {
                FirstName = Student.FirstName,
                LastName = Student.LastName,
                EnrollmentDate = Student.EnrollmentDate

            };

            Database.Students.Add(newObj);
            Database.SaveChanges();

            Student.StudentID = newObj.StudentId;

            return Student;
        }

        public StudentDTO UpdateStudent(StudentDTO student)
        {
            var changedObj = Database.Students.Where(p => p.StudentId == student.StudentID).FirstOrDefault();
            if (changedObj == null || changedObj.StudentId != student.StudentID)
            {
                throw new KeyNotFoundException("Could not find a matching student in the system.");
            }

            changedObj.StudentId = student.StudentID;
            changedObj.FirstName = student.FirstName;
            changedObj.LastName = student.LastName;
            changedObj.EnrollmentDate = student.EnrollmentDate;

            Database.Students.Attach(changedObj);

            var entry = Database.Entry(changedObj);
            entry.Property(e => e.FirstName).IsModified = true;
            entry.Property(e => e.LastName).IsModified = true;
            entry.Property(e => e.EnrollmentDate).IsModified = true;

            Database.SaveChanges();

            return student;
        }

        public int DeleteStudent(int StudentID)
        {
            var result = 0;

            var target = Database.Students.Where(x => x.StudentId == StudentID).FirstOrDefault();

            if (target != null && target.StudentId == StudentID)
            {
                //remove all student courses objects.
                var courses = GetStudentCourse(StudentID);
                if(courses != null && courses.Count() > 0)
                {
                    foreach(var c in courses)
                    {
                        result = DeleteStudentCourse(c.StudentID, c.CourseID, c.EnrolledYear, c.EnrolledSemester);
                        if(result <= 0)
                        {
                            return -1;
                        }
                    }
                    Database.SaveChanges();
                }


                Database.Students.Remove(target);
                return Database.SaveChanges();
            }
            return -1;
        }

        public IEnumerable<StudentCourseDTO> GetAllStudentCourses()
        {

            var s = Database.StudentCourses.Select(
                a => new StudentCourseDTO()
                {
                    StudentID = a.StudentId,
                    CourseID = a.CourseId,
                    Grade = a.Grade,
                    EnrolledYear = a.EnrolledYear,
                    EnrolledSemester = a.EnrolledSemester,
                    Completed = a.Completed,
                    Dropped = a.Dropped,
                    DroppedTime = a.DroppedTime

                }).AsEnumerable<StudentCourseDTO>();

            return s;
        }

        public IEnumerable<StudentCourseDTO> GetStudentCourse(int studentID)
        {
            var target = GetAllStudentCourses().Where(x => x.StudentID == studentID).AsEnumerable();
            return target;
        }

        public StudentCourseDTO CreateStudentCourse(StudentCourseDTO StudentCourse)
        {
            var newObj = new StudentCourses
            {
                StudentId = StudentCourse.StudentID,
                CourseId = StudentCourse.CourseID,
                Grade = StudentCourse.Grade,
                EnrolledYear = StudentCourse.EnrolledYear,
                EnrolledSemester = StudentCourse.EnrolledSemester,
                Completed = StudentCourse.Completed,
                Dropped = StudentCourse.Dropped,
                DroppedTime = StudentCourse.DroppedTime

            };

            Database.StudentCourses.Add(newObj);
            Database.SaveChanges();

            return StudentCourse;
        }

        public int DeleteStudentCourse(int studentID, int cousrseID, int enrolledYear, string enrolledSemester)
        {
            var target = Database.StudentCourses.Where(x => x.CourseId == cousrseID && x.StudentId == studentID && x.EnrolledYear == enrolledYear && x.EnrolledSemester == enrolledSemester).FirstOrDefault();

            if (target != null
                 && target.StudentId == studentID
                 && target.CourseId == cousrseID
                 && target.EnrolledYear == enrolledYear
                 && target.EnrolledSemester == enrolledSemester
                 )
            {
                Database.StudentCourses.Remove(target);
                return Database.SaveChanges();
            }
            return -1;
        }
    }
}
