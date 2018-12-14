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
    public class PersonTest : TestBase
    {   
        [TestMethod]
        public void AddPerson_Test()
        {
            var currentPeopleCount = Repository.GetAllPersons().Count();

            var obj = new PersonDTO();
            obj.FirstName = "Test";
            obj.LastName = "Subject";
            
            obj = Repository.CreatePerson(obj);

            Assert.IsTrue(Repository.GetAllPersons().Count() > currentPeopleCount);

            PersonTest.DeleteTestObject(obj, Repository);
        }

        [TestMethod]
        public void ReadPerson_Test()
        {            
            var obj = new PersonDTO();
            obj.FirstName = "Test";
            obj.LastName = "Subject";

            Assert.AreEqual(0, obj.PersonID);

            obj = Repository.CreatePerson(obj);

            Assert.IsTrue(obj.PersonID > 0);
            Assert.AreEqual("Test", obj.FirstName);
            Assert.AreEqual("Subject", obj.LastName);
            Assert.IsNull(obj.HireDate);
            Assert.IsNull(obj.EnrollmentDate);

            PersonTest.DeleteTestObject(obj, Repository);
        }

        [TestMethod]
        public void DeletePerson_Test()
        {
            var obj = new PersonDTO();
            obj.FirstName = "Test";
            obj.LastName = "Subject";

            obj = Repository.CreatePerson(obj);
            
            var currentCount = Repository.GetAllPersons().Count();

            PersonTest.DeleteTestObject(obj, Repository);

            Assert.IsTrue(Repository.GetAllPersons().Count() < currentCount);
        }

        [TestMethod]
        public void UpdatePerson_Test_LastName()
        {
            var obj = CreateTestPerson(Repository);
            //confirm they are saved in the database.
            Assert.IsTrue(obj.PersonID > 0);

            try
            {
                var randomName = Guid.NewGuid().ToString();

                obj.LastName = randomName;

                obj = Repository.UpdatePerson(obj);

                //confirm the object was updated.
                var updatedPerson = Repository.GetPerson(obj.PersonID);

                Assert.IsNotNull(updatedPerson);
                Assert.AreEqual(randomName, updatedPerson.LastName);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //Remove the test data.
                PersonTest.DeleteTestObject(obj, Repository);
            }            
        }

        [TestMethod]
        public void UpdatePerson_Test_FirstName()
        {
            var obj = CreateTestPerson(Repository);

            //confirm they are saved in the database.
            Assert.IsTrue(obj.PersonID > 0);
            try
            {

                var randomName = Guid.NewGuid().ToString();

                obj.FirstName = randomName;


                obj = Repository.UpdatePerson(obj);

                //confirm the object was updated.
                var updatedPerson = Repository.GetPerson(obj.PersonID);

                Assert.IsNotNull(updatedPerson);
                Assert.AreEqual(randomName, updatedPerson.FirstName);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Remove the test data.
                PersonTest.DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void UpdatePerson_Test_HireDate()
        {
            var obj = CreateTestPerson(Repository);

            //confirm they are saved in the database.
            Assert.IsTrue(obj.PersonID > 0);

            try
            {
                var randomDate = DateTime.Today;

                obj.HireDate = randomDate;

                obj = Repository.UpdatePerson(obj);

                //confirm the object was updated.
                var updatedPerson = Repository.GetPerson(obj.PersonID);

                Assert.IsNotNull(updatedPerson);
                Assert.AreEqual(randomDate, updatedPerson.HireDate);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Remove the test data.
                PersonTest.DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void UpdatePerson_Test_EnrollmentDate()
        {
            var obj = CreateTestPerson(Repository);

            //confirm they are saved in the database.
            Assert.IsTrue(obj.PersonID > 0);

            try
            {
                var randomDate = DateTime.Today;

                obj.EnrollmentDate = randomDate;

                obj = Repository.UpdatePerson(obj);

                //confirm the object was updated.
                var updatedPerson = Repository.GetPerson(obj.PersonID);

                Assert.IsNotNull(updatedPerson);
                Assert.AreEqual(randomDate, updatedPerson.EnrollmentDate);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //Remove the test data.
                PersonTest.DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void DeletePerson_Test_PersonIsAssignedToOffice()
        {
            //create a person.
            var currentPeopleCount = Repository.GetAllPersons().Count();

            var obj = new PersonDTO();
            obj.FirstName = "Test";
            obj.LastName = "Subject";

            obj = Repository.CreatePerson(obj);

            Assert.IsTrue(Repository.GetAllPersons().Count() > currentPeopleCount);

            //create an Office Assignment.
            var officeAssignment = new OfficeAssignmentDTO { InstructorID = obj.PersonID, Location = "321 Test Location" };
            officeAssignment = Repository.CreateOfficeAssignment(officeAssignment);

            //Attempt to delete the person.
            //Entity framework should figure out there's a FK constraint between the two objects.
            PersonTest.DeleteTestObject(obj, Repository);

            Assert.AreEqual(currentPeopleCount, Repository.GetAllPersons().Count());
        }

        [TestMethod]
        public void DeletePerson_Test_StudentHasGrades()
        {
            //create a person.
            var currentPeopleCount = Repository.GetAllPersons().Count();

            var obj = new PersonDTO();
            obj.FirstName = "Test";
            obj.LastName = "Subject";
            obj.EnrollmentDate = DateTime.Now;

            obj = Repository.CreatePerson(obj);

            Assert.IsTrue(Repository.GetAllPersons().Count() > currentPeopleCount);

            //create a course.
            var course = CourseTest.CreateTestCourse(Repository);

            //create a grade.
            var grade = new StudentGradeDTO
            {
                CourseID = course.CourseID,
                StudentID = obj.PersonID,
                Grade = (decimal)3.5
            };

            grade = Repository.AddStudentGrade(grade);

            Assert.IsTrue(grade.EnrollmentID > 0);

            //Attempt to delete the person.
            try
            {
                PersonTest.DeleteTestObject(obj, Repository);
                Assert.AreEqual(currentPeopleCount, Repository.GetAllPersons().Count());
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                //clean up test data.
                Repository.DeleteStudentGrade(grade.EnrollmentID);
                CourseTest.DeleteTestObject(course, Repository);
                PersonTest.DeleteTestObject(obj, Repository);
            }            
        }

        /// <summary>
        /// Creates the test person.
        /// </summary>
        /// <param name="Repository">The repository.</param>
        /// <returns></returns>
        public static PersonDTO CreateTestPerson(ISchoolData Repository)
        {
            var firstName = "Test";
            var lastName = "Subject";
            DateTime? hireDate = null;
            DateTime? enrollmentDate = null;

            var person = new PersonDTO();
            person.FirstName = firstName;
            person.LastName = lastName;
            person.HireDate = hireDate;
            person.EnrollmentDate = enrollmentDate;

            person = Repository.CreatePerson(person);

            return person;
        }
        
        /// <summary>
        /// Deletes the test object.
        /// </summary>
        /// <param name="toDelete">The object to delete.</param>
        public static void DeleteTestObject(PersonDTO toDelete, ISchoolData Repository)
        {
            Repository.DeletePerson(toDelete.PersonID);            
        }

        /// <summary>
        /// Finds a Person ID that is not in the system.
        /// </summary>
        /// <param name="_repository">The repository.</param>
        /// <returns></returns>
        public static int GetUnusedPersonID(ISchoolData _repository)
        {
            var assignments = _repository.GetAllPersons();

            for (int ii = 1; ii < Int32.MaxValue; ii++)
            {
                bool isUsed = false;

                foreach (var assignment in assignments)
                {
                    if (assignment.PersonID.Equals(ii))
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
