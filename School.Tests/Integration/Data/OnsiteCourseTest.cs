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
    public class OnsiteCourseTest : TestBase
    {
        [TestMethod]
        public void GetAllOnsiteCoursesTest()
        {
            var result = Repository.GetAllOnsiteCourses();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);

            foreach(var item in result)
            {
                Assert.IsNotNull(item.CourseID);
                Assert.IsNotNull(item.Location);
                Assert.IsNotNull(item.Days);
                Assert.IsNotNull(item.Time);
            }
        }

        [TestMethod]
        public void GetOnsiteCourse_Invalid_Zero()
        {
            var onlineCourse = Repository.GetOnsiteCourse(0);
            Assert.IsNull(onlineCourse);
        }

        [TestMethod]
        public void GetOnsiteCourse_Invalid_does_not_exist()
        {
            var nonexistentID = GetUnusedOnsiteCourse(Repository);

            var course = Repository.GetOnsiteCourse(nonexistentID);
            Assert.IsNull(course);
        }

        [TestMethod]
        public void GetOnsiteCourse_Test()
        {
            var obj = CreateTestCourse(Repository);

            Assert.IsNotNull(obj.CourseID);
            Assert.IsNotNull(obj.Location);
            Assert.IsNotNull(obj.Days);
            Assert.IsNotNull(obj.Time);

            Assert.IsTrue(obj.CourseID > 0);

            DeleteTestObject(obj, Repository);
        }

        [TestMethod]
        public void AddOnsiteCourse_Test_valid()
        {
            var obj = CreateTestCourse(Repository);

            try
            {
                Assert.IsTrue(Repository.GetAllOnsiteCourses().Any(x => x.CourseID == obj.CourseID));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void AddOnsiteCourse_Test_valid_already_exists()
        {
            var obj = CreateTestCourse(Repository);

            try
            {
                Assert.IsTrue(Repository.GetAllOnsiteCourses().Any(x => x.CourseID == obj.CourseID));
                obj = Repository.AddOnsiteCourse(obj);
                Assert.IsTrue(Repository.GetAllOnsiteCourses().Any(x => x.CourseID == obj.CourseID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void AddOnsiteCourse_Test_invalid_null()
        {
            OnsiteCourseDTO course = null;

            try
            {
                Repository.AddOnsiteCourse(course);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.Contains("course"));
            }
        }

        [TestMethod]
        public void AddOnsiteCourse_Test_invalid_no_course_id()
        {
            var course = new OnsiteCourseDTO { CourseID = 0 };

            try
            {
                Repository.AddOnsiteCourse(course);
            }
            catch (KeyNotFoundException ex)
            {
                Assert.IsTrue(ex.Message.Contains("The course ID 0 was not found in the system."));
            }
        }

        
        
        [TestMethod]
        public void DeleteOnsiteCourse_Test()
        {
            var obj = CreateTestCourse(Repository);

            var currentCount = Repository.GetAllOnsiteCourses().Count();

            DeleteTestObject(obj, Repository);

            Assert.IsTrue(Repository.GetAllOnsiteCourses().Count() < currentCount);
        }

        [TestMethod]
        public void UpdateOnsiteCourse_Test_Location()
        {
            var obj = CreateTestCourse(Repository);

            try
            {
                var newLocation = "665 Kennedy";

                obj.Location = newLocation;               

                obj = Repository.UpdateOnsiteCourse(obj);

                //confirm the object was updated.
                var updated = Repository.GetOnsiteCourse(obj.CourseID);

                Assert.IsNotNull(updated);
                Assert.AreEqual(newLocation, updated.Location);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //Remove the test data.            
                OnsiteCourseTest.DeleteTestObject(obj, Repository);
            }
        }              
               
        /// <summary>
        /// Creates the onsite course.
        /// </summary>
        /// <param name="_repository">The repository.</param>
        /// <returns></returns>
        public static OnsiteCourseDTO CreateTestCourse(ISchoolData _repository)
        {
            var course = CourseTest.CreateTestCourse(_repository);
            var location = "444 TEST";
            var days = "TWHF";
            var time = new DateTime(2010, 1, 1, 8, 00, 00);
            
            var obj = new OnsiteCourseDTO();
            obj.Course = course;
            obj.CourseID = course.CourseID;
            obj.Location = location;
            obj.Days = days;
            obj.Time = time;

            obj = _repository.AddOnsiteCourse(obj);

            return obj;
        }

        /// <summary>
        /// Deletes the test object.
        /// </summary>
        /// <param name="toDelete">The object to delete.</param>
        /// <param name="_repository">The repository.</param>
        public static void DeleteTestObject(OnsiteCourseDTO toDelete, ISchoolData _repository)
        {
            _repository.DeleteOnsiteCourse(toDelete.CourseID);
            _repository.DeleteCourse(toDelete.CourseID);            

            var originalDepartment = _repository.GetDepartment(toDelete.Course.DepartmentID);
            if(originalDepartment != null)
            {
                DepartmentTest.DeleteTestObject(originalDepartment, _repository);
            }
        }

        /// <summary>
        /// Gets an unused onsite course ID.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <returns></returns>
        public static int GetUnusedOnsiteCourse(ISchoolData repository)
        {
            var courses = repository.GetAllCourses();

            var result = 0;

            foreach (var thing in courses)
            {
                if (repository.GetAllOnsiteCourses().Any(x => x.CourseID == thing.CourseID) == false)
                {
                    result = thing.CourseID;
                    break;
                }
            }

            return result;
        }
    }
}
