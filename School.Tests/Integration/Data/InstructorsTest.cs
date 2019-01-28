using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using School.DTOs;
using School.Data;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace School.Tests.Integration.Data
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class InstructorsTest : TestBase
    {   
        [TestMethod]
        public void AddInstructor_Test_no_courses()
        {
            var currentPeopleCount = Repository.GetAllInstructors().Count();

            var obj = new InstructorDTO();
            obj.FirstName = "Test";
            obj.LastName = "Subject";
            
            obj = Repository.CreateInstructor(obj);

            Assert.IsTrue(Repository.GetAllInstructors().Count() > currentPeopleCount);

            InstructorsTest.DeleteTestInstructor(obj, Repository);
        }
        
        [TestMethod]
        public void AddInstructor_Test_one_course()
        {
            const int EXPECTED_COURSE_COUNT = 1;

            var currentPeopleCount = Repository.GetAllInstructors().Count();            
            var course = CourseTest.CreateTestCourse(Repository);

            var targetUnderTest = new InstructorDTO();
            targetUnderTest.FirstName = "Test";
            targetUnderTest.LastName = "Subject";
            targetUnderTest.Courses = new List<CourseDTO>();
            targetUnderTest.Courses.Add(course);

            try
            {
                targetUnderTest = Repository.CreateInstructor(targetUnderTest);
                Assert.IsTrue(Repository.GetAllInstructors().Count() > currentPeopleCount);
                Assert.AreEqual(EXPECTED_COURSE_COUNT, Repository.GetCoursesByInstructor(targetUnderTest.InstructorID).Count());

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                InstructorsTest.DeleteTestInstructor(targetUnderTest, Repository);
                CourseTest.DeleteTestObject(course, Repository);
                Repository.DeleteDepartment(course.DepartmentID);
            }
        }
        
        [TestMethod]
        public void AddInstructor_Test_two_courses()
        {
            const int EXPECTED_COURSE_COUNT = 2;

            var currentPeopleCount = Repository.GetAllInstructors().Count();
            var course1 = CourseTest.CreateTestCourse(Repository);
            var course2 = CourseTest.CreateTestCourse(Repository);

            var targetUnderTest = new InstructorDTO();
            targetUnderTest.FirstName = "Test";
            targetUnderTest.LastName = "Subject";
            targetUnderTest.Courses = new List<CourseDTO>();
            targetUnderTest.Courses.Add(course1);
            targetUnderTest.Courses.Add(course2);

            try
            {
                targetUnderTest = Repository.CreateInstructor(targetUnderTest);
                Assert.IsTrue(Repository.GetAllInstructors().Count() > currentPeopleCount);
                Assert.AreEqual(EXPECTED_COURSE_COUNT, Repository.GetCoursesByInstructor(targetUnderTest.InstructorID).Count());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                InstructorsTest.DeleteTestInstructor(targetUnderTest, Repository);
                CourseTest.DeleteTestObject(course1, Repository);
                CourseTest.DeleteTestObject(course2, Repository);
                Repository.DeleteDepartment(course1.DepartmentID);
                Repository.DeleteDepartment(course2.DepartmentID);
            }
        }

        [TestMethod]
        public void AddInstructor_Test_two_courses_to_zero()
        {
            const int BASE_COURSE_COUNT = 2;
            const int EXPECTED_COURSE_COUNT = 0;

            var currentPeopleCount = Repository.GetAllInstructors().Count();
            var course1 = CourseTest.CreateTestCourse(Repository);
            var course2 = CourseTest.CreateTestCourse(Repository);

            var targetUnderTest = new InstructorDTO();
            targetUnderTest.FirstName = "Test";
            targetUnderTest.LastName = "Subject";
            targetUnderTest.Courses = new List<CourseDTO>();
            targetUnderTest.Courses.Add(course1);
            targetUnderTest.Courses.Add(course2);

            try
            {
                //Add the courses. Make sure they add correctly.
                targetUnderTest = Repository.CreateInstructor(targetUnderTest);
                Assert.IsTrue(Repository.GetAllInstructors().Count() > currentPeopleCount);
                Assert.AreEqual(BASE_COURSE_COUNT, Repository.GetCoursesByInstructor(targetUnderTest.InstructorID).Count());

                //Remove the courses.
                targetUnderTest.Courses = new List<CourseDTO>();

                //Save it. Verify the course count updates.
                targetUnderTest = Repository.UpdateInstructor(targetUnderTest);
                Assert.IsTrue(Repository.GetAllInstructors().Count() > currentPeopleCount);
                Assert.AreEqual(EXPECTED_COURSE_COUNT, Repository.GetCoursesByInstructor(targetUnderTest.InstructorID).Count());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                InstructorsTest.DeleteTestInstructor(targetUnderTest, Repository);
                CourseTest.DeleteTestObject(course1, Repository);
                CourseTest.DeleteTestObject(course2, Repository);
                Repository.DeleteDepartment(course1.DepartmentID);
                Repository.DeleteDepartment(course2.DepartmentID);
            }
        }

        [TestMethod]
        public void AddInstructor_Test_two_courses_to_one()
        {
            const int BASE_COURSE_COUNT = 2;
            const int EXPECTED_COURSE_COUNT = 1;

            var currentPeopleCount = Repository.GetAllInstructors().Count();
            var course1 = CourseTest.CreateTestCourse(Repository);
            var course2 = CourseTest.CreateTestCourse(Repository);

            var targetUnderTest = new InstructorDTO();
            targetUnderTest.FirstName = "Test";
            targetUnderTest.LastName = "Subject";
            targetUnderTest.Courses = new List<CourseDTO>();
            targetUnderTest.Courses.Add(course1);
            targetUnderTest.Courses.Add(course2);

            try
            {
                //Add the courses. Make sure they add correctly.
                targetUnderTest = Repository.CreateInstructor(targetUnderTest);
                Assert.IsTrue(Repository.GetAllInstructors().Count() > currentPeopleCount);
                Assert.AreEqual(BASE_COURSE_COUNT, Repository.GetCoursesByInstructor(targetUnderTest.InstructorID).Count());

                //Remove one course.
                targetUnderTest.Courses.Remove(targetUnderTest.Courses.Where(x => x.CourseID == course1.CourseID).First());

                //Save it. Verify the course count updates.
                targetUnderTest = Repository.UpdateInstructor(targetUnderTest);
                Assert.IsTrue(Repository.GetAllInstructors().Count() > currentPeopleCount);
                Assert.AreEqual(EXPECTED_COURSE_COUNT, Repository.GetCoursesByInstructor(targetUnderTest.InstructorID).Count());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                InstructorsTest.DeleteTestInstructor(targetUnderTest, Repository);
                CourseTest.DeleteTestObject(course1, Repository);
                CourseTest.DeleteTestObject(course2, Repository);
                Repository.DeleteDepartment(course1.DepartmentID);
                Repository.DeleteDepartment(course2.DepartmentID);
            }
        }


        [TestMethod]
        public void GetInstructorTest_No_Classes()
        {            
            var obj = new InstructorDTO();
            obj.FirstName = "Test";
            obj.LastName = "Subject";

            Assert.AreEqual(0, obj.InstructorID);

            obj = Repository.CreateInstructor(obj);

            Assert.IsTrue(obj.InstructorID > 0);
            Assert.AreEqual("Test", obj.FirstName);
            Assert.AreEqual("Subject", obj.LastName);
            Assert.IsNull(obj.HireDate);

            var allInstructors = Repository.GetAllInstructors();
            Assert.IsNotNull(allInstructors);
            Assert.IsTrue(allInstructors.Count() > 0);

            InstructorsTest.DeleteTestInstructor(obj, Repository);
        }

        [TestMethod]
        public void GetInstructorTest_One_Class()
        {            
            const int EXPECTED_COURSE_COUNT = 1;
                        
            var course1 = CourseTest.CreateTestCourse(Repository);

            var targetUnderTest = new InstructorDTO();
            targetUnderTest.FirstName = "Test";
            targetUnderTest.LastName = "Subject";
            targetUnderTest.Courses = new List<CourseDTO>();
            targetUnderTest.Courses.Add(course1);

            try
            {
                //Add the courses. Make sure they add correctly.
                targetUnderTest = Repository.CreateInstructor(targetUnderTest);                
                Assert.AreEqual(EXPECTED_COURSE_COUNT, Repository.GetCoursesByInstructor(targetUnderTest.InstructorID).Count());

                var allData = Repository.GetAllInstructors().Where(x => x.InstructorID == targetUnderTest.InstructorID).First();
                Assert.IsNotNull(allData);
                Assert.IsNotNull(allData.Courses);
                Assert.AreEqual(EXPECTED_COURSE_COUNT, allData.Courses.Count());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                InstructorsTest.DeleteTestInstructor(targetUnderTest, Repository);
                CourseTest.DeleteTestObject(course1, Repository);
                Repository.DeleteDepartment(course1.DepartmentID);
            }
        }

        [TestMethod]
        public void GetInstructorTest_two_Classes()
        {
            const int EXPECTED_COURSE_COUNT = 2;

            var course1 = CourseTest.CreateTestCourse(Repository);
            var course2 = CourseTest.CreateTestCourse(Repository);

            var targetUnderTest = new InstructorDTO();
            targetUnderTest.FirstName = "Test";
            targetUnderTest.LastName = "Subject";
            targetUnderTest.Courses = new List<CourseDTO>();
            targetUnderTest.Courses.Add(course1);
            targetUnderTest.Courses.Add(course2);

            try
            {
                //Add the courses. Make sure they add correctly.
                targetUnderTest = Repository.CreateInstructor(targetUnderTest);
                Assert.AreEqual(EXPECTED_COURSE_COUNT, Repository.GetCoursesByInstructor(targetUnderTest.InstructorID).Count());

                var allData = Repository.GetAllInstructors().Where(x => x.InstructorID == targetUnderTest.InstructorID).First();
                Assert.IsNotNull(allData);
                Assert.IsNotNull(allData.Courses);
                Assert.AreEqual(EXPECTED_COURSE_COUNT, allData.Courses.Count());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                InstructorsTest.DeleteTestInstructor(targetUnderTest, Repository);
                CourseTest.DeleteTestObject(course1, Repository);
                CourseTest.DeleteTestObject(course2, Repository);
                Repository.DeleteDepartment(course1.DepartmentID);
                Repository.DeleteDepartment(course2.DepartmentID);
            }
        }

        [TestMethod]
        public void DeleteInstructor_Test()
        {
            var obj = new InstructorDTO();
            obj.FirstName = "Test";
            obj.LastName = "Subject";

            obj = Repository.CreateInstructor(obj);
            
            var currentCount = Repository.GetAllInstructors().Count();

            InstructorsTest.DeleteTestInstructor(obj, Repository);

            Assert.IsTrue(Repository.GetAllInstructors().Count() < currentCount);
        }

        [TestMethod]
        public void UpdateInstructor_Test_LastName()
        {
            var obj = CreateTestInstructor(Repository);
            //confirm they are saved in the database.
            Assert.IsTrue(obj.InstructorID > 0);

            try
            {
                var randomName = Guid.NewGuid().ToString();

                obj.LastName = randomName;

                obj = Repository.UpdateInstructor(obj);

                //confirm the object was updated.
                var updatedInstructor = Repository.GetInstructor(obj.InstructorID);

                Assert.IsNotNull(updatedInstructor);
                Assert.AreEqual(randomName, updatedInstructor.LastName);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //Remove the test data.
                InstructorsTest.DeleteTestInstructor(obj, Repository);
            }            
        }

        [TestMethod]
        public void UpdateInstructor_Test_FirstName()
        {
            var obj = CreateTestInstructor(Repository);

            //confirm they are saved in the database.
            Assert.IsTrue(obj.InstructorID > 0);
            try
            {

                var randomName = Guid.NewGuid().ToString();

                obj.FirstName = randomName;


                obj = Repository.UpdateInstructor(obj);

                //confirm the object was updated.
                var updatedInstructor = Repository.GetInstructor(obj.InstructorID);

                Assert.IsNotNull(updatedInstructor);
                Assert.AreEqual(randomName, updatedInstructor.FirstName);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Remove the test data.
                InstructorsTest.DeleteTestInstructor(obj, Repository);
            }
        }

        [TestMethod]
        public void UpdateInstructor_Test_HireDate()
        {
            var obj = CreateTestInstructor(Repository);

            //confirm they are saved in the database.
            Assert.IsTrue(obj.InstructorID > 0);

            try
            {
                var randomDate = DateTime.Today;

                obj.HireDate = randomDate;

                obj = Repository.UpdateInstructor(obj);

                //confirm the object was updated.
                var updatedInstructor = Repository.GetInstructor(obj.InstructorID);

                Assert.IsNotNull(updatedInstructor);
                Assert.AreEqual(randomDate, updatedInstructor.HireDate);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Remove the test data.
                InstructorsTest.DeleteTestInstructor(obj, Repository);
            }
        }

        [TestMethod]
        public void UpdateInstructor_Test_Terminated_False_True()
        {
            var obj = CreateTestInstructor(Repository);
            //confirm they are saved in the database.
            Assert.IsTrue(obj.InstructorID > 0);

            try
            {
                var terminatedStatus = true;

                obj.Terminated = terminatedStatus;

                obj = Repository.UpdateInstructor(obj);

                //confirm the object was updated.
                var updatedInstructor = Repository.GetInstructor(obj.InstructorID);

                Assert.IsNotNull(updatedInstructor);
                Assert.IsTrue(updatedInstructor.Terminated);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //Remove the test data.
                InstructorsTest.DeleteTestInstructor(obj, Repository);
            }
        }


        [TestMethod]
        public void UpdateInstructor_Test_Terminated_True_False()
        {
            var obj = CreateTestInstructor(Repository);
            //confirm they are saved in the database.
            Assert.IsTrue(obj.InstructorID > 0);

            //the default sets the Terminated Status to false. Update it to true first before running the test.
            obj.Terminated = true;
            obj = Repository.UpdateInstructor(obj);


            try
            {
                Assert.IsTrue(obj.Terminated);

                var terminatedStatus = false;

                obj.Terminated = terminatedStatus;

                obj = Repository.UpdateInstructor(obj);

                //confirm the object was updated.
                var updatedInstructor = Repository.GetInstructor(obj.InstructorID);

                Assert.IsNotNull(updatedInstructor);
                Assert.IsFalse(updatedInstructor.Terminated);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //Remove the test data.
                InstructorsTest.DeleteTestInstructor(obj, Repository);
            }
        }

        /// <summary>
        /// Creates the test Instructor.
        /// </summary>
        /// <param name="Repository">The repository.</param>
        /// <returns></returns>
        public static InstructorDTO CreateTestInstructor(ISchoolData Repository)
        {
            var firstName = "Test";
            var lastName = "Subject";
            DateTime? hireDate = null;
            DateTime? HireDate = null;

            var Instructor = new InstructorDTO();
            Instructor.FirstName = firstName;
            Instructor.LastName = lastName;
            Instructor.HireDate = hireDate;
            Instructor.HireDate = HireDate;
            Instructor.Terminated = false;

            Instructor = Repository.CreateInstructor(Instructor);

            return Instructor;
        }
        
        /// <summary>
        /// Deletes the test object.
        /// </summary>
        /// <param name="toDelete">The object to delete.</param>
        public static void DeleteTestInstructor(InstructorDTO toDelete, ISchoolData Repository)
        {
            Repository.DeleteInstructor(toDelete.InstructorID);            
        }

        /// <summary>
        /// Finds an Instructor ID that is not in the system.
        /// </summary>
        /// <param name="_repository">The repository.</param>
        /// <returns></returns>
        public static int GetUnusedInstructorID(ISchoolData _repository)
        {
            var assignments = _repository.GetAllInstructors();

            for (int ii = 1; ii < Int32.MaxValue; ii++)
            {
                bool isUsed = false;

                foreach (var assignment in assignments)
                {
                    if (assignment.InstructorID.Equals(ii))
                    {
                        isUsed = true;
                        break;
                    }
                }

                if (!isUsed)
                {
                    return ii;
                }
            }
            return 0;
        }
    }
}
