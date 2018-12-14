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
    public class OnlineCourseTest : TestBase
    {
        [TestMethod]
        public void GetCourse_Invalid_Zero()
        {
            var onlineCourse = Repository.GetOnlineCourse(0);
            Assert.IsNull(onlineCourse);
        }

        [TestMethod]
        public void GetCourse_Invalid_does_not_exist()
        {
            var nonexistentID = GetUnusedOnlineCourse(Repository);

            var onlineCourse = Repository.GetOnlineCourse(nonexistentID);
            Assert.IsNull(onlineCourse);
        }

        [TestMethod]
        public void AddOnlineCourse_Test_valid()
        {
            var obj = CreateTestCourse(Repository);

            try
            {
                Assert.IsTrue(Repository.GetAllOnlineCourses().Any(x => x.CourseID == obj.CourseID));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                OnlineCourseTest.DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void AddOnlineCourse_Test_valid_already_exists()
        {
            var obj = CreateTestCourse(Repository);

            try
            {
                Assert.IsTrue(Repository.GetAllOnlineCourses().Any(x => x.CourseID == obj.CourseID));
                obj = Repository.AddOnlineCourse(obj);
                Assert.IsTrue(Repository.GetAllOnlineCourses().Any(x => x.CourseID == obj.CourseID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                OnlineCourseTest.DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void AddOnlineCourse_Test_invalid_null()
        {
            OnlineCourseDTO course = null;

            try
            {
                Repository.AddOnlineCourse(course);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.Contains("course"));
            }
        }

        [TestMethod]
        public void AddOnlineCourse_Test_invalid_no_course_id()
        {
            OnlineCourseDTO course = new OnlineCourseDTO { CourseID = 0 };

            try
            {
                Repository.AddOnlineCourse(course);
            }
            catch (KeyNotFoundException ex)
            {
                Assert.IsTrue(ex.Message.Contains("The course ID 0 was not found in the system."));
            }
        }

        [TestMethod]
        public void ReadCourse_Test()
        {
            var obj = CreateTestCourse(Repository);

            Assert.IsNotNull(obj.CourseID);
            Assert.IsNotNull(obj.URL);

            Assert.IsTrue(obj.CourseID > 0);
            Assert.IsTrue(obj.URL.Length > 0);
            Assert.IsTrue(obj.URL.Length <= 100);

            OnlineCourseTest.DeleteTestObject(obj, Repository);
        }
        
        [TestMethod]
        public void DeleteCourse_Test()
        {
            var obj = CreateTestCourse(Repository);

            var currentCount = Repository.GetAllOnlineCourses().Count();

            OnlineCourseTest.DeleteTestObject(obj, Repository);

            Assert.IsTrue(Repository.GetAllOnlineCourses().Count() < currentCount);
        }

        [TestMethod]
        public void UpdateOnlineCourse_Test_URL()
        {
            var obj = CreateTestCourse(Repository);

            try
            {
                var randomURL = Guid.NewGuid().ToString();

                obj.URL = randomURL;

                obj = Repository.UpdateOnlineCourse(obj);

                //confirm the object was updated.
                var updated = Repository.GetOnlineCourse(obj.CourseID);

                Assert.IsNotNull(updated);
                Assert.AreEqual(randomURL, updated.URL);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //Remove the test data.            
                OnlineCourseTest.DeleteTestObject(obj, Repository);
            }
        }       
       
               
        /// <summary>
        /// Creates the online course.
        /// </summary>
        /// <param name="_repository">The repository.</param>
        /// <returns></returns>
        public static OnlineCourseDTO CreateTestCourse(ISchoolData _repository)
        {
            var course = CourseTest.CreateTestCourse(_repository);
            var url = Guid.NewGuid().ToString();
            
            var obj = new OnlineCourseDTO();
            obj.Course = course;
            obj.CourseID = course.CourseID;
            obj.URL = url;

            obj = _repository.AddOnlineCourse(obj);

            return obj;
        }

        /// <summary>
        /// Deletes the test object.
        /// </summary>
        /// <param name="toDelete">The object to delete.</param>
        /// <param name="_repository">The repository.</param>
        public static void DeleteTestObject(OnlineCourseDTO toDelete, ISchoolData _repository)
        {
            _repository.DeleteOnlineCourse(toDelete.CourseID);
            _repository.DeleteCourse(toDelete.CourseID);            

            var originalDepartment = _repository.GetDepartment(toDelete.Course.DepartmentID);
            if(originalDepartment != null)
            {
                DepartmentTest.DeleteTestObject(originalDepartment, _repository);
            }
        }

        /// <summary>
        /// Gets an unused online course ID.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <returns></returns>
        public static int GetUnusedOnlineCourse(ISchoolData repository)
        {
            var courses = repository.GetAllCourses();

            var result = 0;

            foreach (var thing in courses)
            {
                if (repository.GetAllOnlineCourses().Any(x => x.CourseID == thing.CourseID) == false)
                {
                    result = thing.CourseID;
                    break;
                }
            }

            return result;
        }
    }
}
