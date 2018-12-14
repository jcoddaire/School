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
    public class DepartmentTest : TestBase
    {
        [TestMethod]
        public void AddDepartment_Test()
        {
            var obj = CreateTestDepartment(Repository);

            Assert.IsTrue(Repository.GetAllDepartments().Any(x => x.DepartmentID == obj.DepartmentID));

            DepartmentTest.DeleteTestObject(obj, Repository);
        }

        [TestMethod]
        public void ReadDepartment_Test()
        {
            var obj = CreateTestDepartment(Repository);

            Assert.IsNotNull(obj.DepartmentID);
            Assert.IsNotNull(obj.Name);
            Assert.IsNotNull(obj.StartDate);
            Assert.IsNotNull(obj.Budget);
            Assert.IsNull(obj.Administrator);

            Assert.IsTrue(obj.DepartmentID > 0);
            Assert.AreEqual(DateTime.Today, obj.StartDate);
            Assert.AreEqual(1000000, obj.Budget);

            DepartmentTest.DeleteTestObject(obj, Repository);
        }

        [TestMethod]
        public void DeleteDepartment_Test()
        {
            var obj = CreateTestDepartment(Repository);

            var currentCount = Repository.GetAllDepartments().Count();

            DepartmentTest.DeleteTestObject(obj, Repository);

            Assert.IsTrue(Repository.GetAllDepartments().Count() < currentCount);            
        }

        [TestMethod]
        public void UpdateDepartment_Test_Name()
        {
            var obj = CreateTestDepartment(Repository);

            try
            {
                var randomName = Guid.NewGuid().ToString();

                obj.Name = randomName;

                obj = Repository.UpdateDepartment(obj);

                //confirm the object was updated.
                var updated = Repository.GetDepartment(obj.DepartmentID);

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
                DepartmentTest.DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void UpdateDepartment_Test_Budget()
        {
            var obj = CreateTestDepartment(Repository);

            try
            {
                var differentBudget = 1;

                obj.Budget = differentBudget;

                obj = Repository.UpdateDepartment(obj);

                //confirm the object was updated.
                var updated = Repository.GetDepartment(obj.DepartmentID);

                Assert.IsNotNull(updated);
                Assert.AreEqual(differentBudget, updated.Budget);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Remove the test data.
                DepartmentTest.DeleteTestObject(obj, Repository);
            }
        }
        
        [TestMethod]
        public void UpdateDepartment_Test_StartDate()
        {
            var obj = CreateTestDepartment(Repository);

            try
            {
                var differentDate = new DateTime(1999, 12, 31);

                obj.StartDate = differentDate;

                obj = Repository.UpdateDepartment(obj);

                //confirm the object was updated.
                var updated = Repository.GetDepartment(obj.DepartmentID);

                Assert.IsNotNull(updated);
                Assert.AreEqual(differentDate, updated.StartDate);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Remove the test data.
                DepartmentTest.DeleteTestObject(obj, Repository);
            }
        }
        
        [TestMethod]
        public void UpdateDepartment_Test_Administrator()
        {
            var obj = CreateTestDepartment(Repository);
                                    
            var person = PersonTest.CreateTestPerson(Repository);

            try
            {
                obj.Administrator = person.PersonID;

                obj = Repository.UpdateDepartment(obj);

                //confirm the object was updated.
                var updated = Repository.GetDepartment(obj.DepartmentID);

                Assert.IsNotNull(updated);
                Assert.AreEqual(person.PersonID, updated.Administrator);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Remove the test data.
                DepartmentTest.DeleteTestObject(obj, Repository);
                PersonTest.DeleteTestObject(person, Repository);
            }
        }

        /// <summary>
        /// Creates the test department.
        /// </summary>
        /// <param name="_repository">The database.</param>
        /// <returns></returns>
        public static DepartmentDTO CreateTestDepartment(ISchoolData _repository)
        {            
            var departmentName = Guid.NewGuid().ToString();
            decimal budget = 1000000;
            var startDate = DateTime.Today;            
            
            var obj = new DepartmentDTO();
            obj.Name = departmentName;
            obj.Budget = budget;
            obj.StartDate = startDate;            
            
            obj.DepartmentID = _repository.GetAllDepartments().Max(x => x.DepartmentID) + 1;

            obj = _repository.CreateDepartment(obj);

            return obj;
        }

        /// <summary>
        /// Deletes the test object.
        /// </summary>
        /// <param name="toDelete">The object to delete.</param>
        /// <param name="_repository">The repository.</param>
        public static void DeleteTestObject(DepartmentDTO toDelete, ISchoolData _repository)
        {
            _repository.DeleteDepartment(toDelete.DepartmentID);
        }
    }
}
