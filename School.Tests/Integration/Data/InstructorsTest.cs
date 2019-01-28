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
    public class InstructorsTest : TestBase
    {   
        [TestMethod]
        public void AddInstructor_Test()
        {
            var currentPeopleCount = Repository.GetAllInstructors().Count();

            var obj = new InstructorDTO();
            obj.FirstName = "Test";
            obj.LastName = "Subject";
            
            obj = Repository.CreateInstructor(obj);

            Assert.IsTrue(Repository.GetAllInstructors().Count() > currentPeopleCount);

            InstructorsTest.DeleteTestObject(obj, Repository);
        }

        [TestMethod]
        public void ReadInstructor_Test()
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

            InstructorsTest.DeleteTestObject(obj, Repository);
        }

        [TestMethod]
        public void DeleteInstructor_Test()
        {
            var obj = new InstructorDTO();
            obj.FirstName = "Test";
            obj.LastName = "Subject";

            obj = Repository.CreateInstructor(obj);
            
            var currentCount = Repository.GetAllInstructors().Count();

            InstructorsTest.DeleteTestObject(obj, Repository);

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
                InstructorsTest.DeleteTestObject(obj, Repository);
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
                InstructorsTest.DeleteTestObject(obj, Repository);
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
                InstructorsTest.DeleteTestObject(obj, Repository);
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
                InstructorsTest.DeleteTestObject(obj, Repository);
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
                InstructorsTest.DeleteTestObject(obj, Repository);
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
        public static void DeleteTestObject(InstructorDTO toDelete, ISchoolData Repository)
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
