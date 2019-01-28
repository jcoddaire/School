using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using School.DTOs;
using School.Data;
using System.Diagnostics.CodeAnalysis;

namespace School.Tests.Integration.Data
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CourseTest : TestBase
    {
        [TestMethod]
        public void AddCourse_Test()
        {
            var obj = CreateTestCourse(Repository);

            try
            {
                Assert.IsTrue(Repository.GetAllCourses().Any(x => x.CourseID == obj.CourseID));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                CourseTest.DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void ReadCourse_Test()
        {
            var obj = CreateTestCourse(Repository);

            Assert.IsNotNull(obj.CourseID);
            Assert.IsNotNull(obj.Name);
            Assert.IsNotNull(obj.Credits);
            Assert.IsNotNull(obj.DepartmentID);

            Assert.IsTrue(obj.CourseID > 0);
            Assert.IsTrue(obj.DepartmentID > 0);
            Assert.IsTrue(obj.Name.Length > 5);

            var allCourses = Repository.GetAllCourses();
            Assert.IsNotNull(allCourses);
            Assert.IsTrue(allCourses.Count() > 0);


            CourseTest.DeleteTestObject(obj, Repository);
        }
        
        [TestMethod]
        public void DeleteCourse_Test()
        {
            var obj = CreateTestCourse(Repository);

            var currentCount = Repository.GetAllCourses().Count();

            CourseTest.DeleteTestObject(obj, Repository);

            Assert.IsTrue(Repository.GetAllCourses().Count() < currentCount);
        }

        [TestMethod]
        public void UpdateCourse_Test_Name()
        {
            var obj = CreateTestCourse(Repository);

            try
            {
                var randomName = Guid.NewGuid().ToString();

                obj.Name = randomName;

                obj = Repository.UpdateCourse(obj);

                //confirm the object was updated.
                var updated = Repository.GetCourse(obj.CourseID);

                Assert.IsNotNull(updated);
                Assert.AreEqual(randomName, updated.Name);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Remove the test data.            
                CourseTest.DeleteTestObject(obj, Repository);
            }
        }
        
        [TestMethod]
        public void UpdateCourse_Test_Credits()
        {
            var obj = CreateTestCourse(Repository);

            try
            {
                var differentCredit = obj.Credits + 1;

                obj.Credits = differentCredit;

                obj = Repository.UpdateCourse(obj);

                //confirm the object was updated.
                var updated = Repository.GetCourse(obj.CourseID);

                Assert.IsNotNull(updated);
                Assert.AreEqual(differentCredit, updated.Credits);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Remove the test data.
                CourseTest.DeleteTestObject(obj, Repository);
            }

        }

        [TestMethod]
        public void UpdateCourse_Test_Department()
        {
            var obj = CreateTestCourse(Repository);

            var originalDepartmentID = obj.DepartmentID;

            try
            {
                var newDepartment = DepartmentTest.CreateTestDepartment(Repository);

                obj.DepartmentID = newDepartment.DepartmentID;

                obj = Repository.UpdateCourse(obj);

                //confirm the object was updated.
                var updated = Repository.GetCourse(obj.CourseID);

                Assert.IsNotNull(updated);
                Assert.AreEqual(newDepartment.DepartmentID, updated.DepartmentID);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Remove the test data.
                CourseTest.DeleteTestObject(obj, Repository);

                var originalDepartment = Repository.GetDepartment(originalDepartmentID);
                DepartmentTest.DeleteTestObject(originalDepartment, Repository);
            }
        }

        /// <summary>
        /// Creates the test course.
        /// </summary>
        /// <param name="_repository">The repository.</param>
        /// <returns></returns>
        public static CourseDTO CreateTestCourse(ISchoolData _repository)
        {            
            var randomName = Guid.NewGuid().ToString();
            var random = new Random();
            var credits = random.Next(1, 4);
            var departmentID = DepartmentTest.CreateTestDepartment(_repository).DepartmentID;                 
            
            var obj = new CourseDTO();
            obj.CourseID = 0;
            obj.Name = randomName;
            obj.Credits = credits;
            obj.DepartmentID = departmentID;

            obj = _repository.CreateCourse(obj);

            return obj;
        }

        /// <summary>
        /// Deletes the test object.
        /// </summary>
        /// <param name="toDelete">The object to delete.</param>
        /// <param name="_repository">The repository.</param>
        public static void DeleteTestObject(CourseDTO toDelete, ISchoolData _repository)
        {
            _repository.DeleteCourse(toDelete.CourseID);

            var originalDepartment = _repository.GetDepartment(toDelete.DepartmentID);
            if(originalDepartment != null)
            {
                DepartmentTest.DeleteTestObject(originalDepartment, _repository);
            }
        }

        public static int GetUnusedCourse(ISchoolData repository)
        {
            var courses = repository.GetAllCourses();

            for (int ii = 1; ii < Int32.MaxValue; ii++)
            {
                bool isUsed = false;

                foreach (var thing in courses)
                {
                    if (thing.CourseID.Equals(ii))
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
