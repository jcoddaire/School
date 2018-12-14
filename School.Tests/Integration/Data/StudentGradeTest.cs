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
    public class StudentGradeTest : TestBase
    {
        [TestMethod]
        public void AddStudentGrade_Test()
        {
            var obj = CreateTestStudentGrade(Repository);

            try
            {
                Assert.IsTrue(Repository.GetAllStudentGrades().Any(x => x.EnrollmentID == obj.EnrollmentID));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                StudentGradeTest.DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void AddStudentGrade_Test_Invalid_Null()
        {
            try
            {
                var obj = Repository.AddStudentGrade(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.Contains("grade"));
            }
        }

        [TestMethod]
        public void AddStudentGrade_Test_Invalid_CourseID_LessThanZero()
        {
            var obj = CreateTestStudentGrade(Repository);
            var idToDelete = obj.CourseID;

            obj.CourseID = 0;

            try
            {
                obj = Repository.AddStudentGrade(obj);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.Contains("grade.CourseID"));
            }
            finally
            {
                obj.CourseID = idToDelete;
                DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void AddStudentGrade_Test_Invalid_StudentID_LessThanZero()
        {
            var obj = CreateTestStudentGrade(Repository);
            var studentIDToDelete = obj.StudentID;

            obj.StudentID = 0;

            try
            {
                obj = Repository.AddStudentGrade(obj);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.Contains("grade.StudentID"));
            }
            finally
            {
                obj.StudentID = studentIDToDelete;
                DeleteTestObject(obj, Repository);
            }                
        }

        [TestMethod]
        public void AddStudentGrade_Test_Invalid_CourseID_NotFound()
        {
            //Find an ID that is not in the dataset.
            var notUsedID = CourseTest.GetUnusedCourse(Repository);

            var obj = CreateTestStudentGrade(Repository);
            var courseIDtoDelete = obj.CourseID;

            obj.CourseID = notUsedID;

            try
            {
                obj = Repository.AddStudentGrade(obj);
            }
            catch (KeyNotFoundException ex)
            {
                Assert.IsTrue(ex.Message.Contains("The courseID '"));
            }
            finally
            {
                obj.CourseID = courseIDtoDelete;
                DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void AddStudentGrade_Test_Invalid_StudentID_NotFound()
        {
            //Find an ID that is not in the dataset.
            var notUsedID = PersonTest.GetUnusedPersonID(Repository);

            var obj = CreateTestStudentGrade(Repository);
            var idToDelete = obj.StudentID;

            obj.StudentID = notUsedID;
            
            try
            {
                obj = Repository.AddStudentGrade(obj);
            }
            catch (KeyNotFoundException ex)
            {
                Assert.IsTrue(ex.Message.Contains("The studentID '"));
            }            
            finally
            {
                obj.StudentID = idToDelete;
                DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void GetStudentGrade_Test()
        {
            var obj = CreateTestStudentGrade(Repository);

            Assert.IsNotNull(obj.EnrollmentID);
            Assert.IsNotNull(obj.CourseID);
            Assert.IsNotNull(obj.StudentID);

            Assert.IsTrue(obj.EnrollmentID > 0);
            Assert.IsTrue(obj.CourseID > 0);
            Assert.IsTrue(obj.StudentID > 0);
            Assert.IsTrue(obj.Grade > 0);            

            StudentGradeTest.DeleteTestObject(obj, Repository);
        }

        [TestMethod]
        public void GetStudentGrade_Invalid_EnrollmentID_Zero()
        {
            var result = Repository.GetStudentGrade(0);
            Assert.IsNull(result);            
        }

        [TestMethod]
        public void GetStudentGrade_Invalid_EnrollmentID_NotExists()
        {
            //Find an ID that is not in the dataset.
            var notUsedID = GetUnusedEnrollmentID(Repository);
            
            var result = Repository.GetStudentGrade(notUsedID);
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void GetStudentGrade_Valid()
        {
            var obj = CreateTestStudentGrade(Repository);

            var resulted = Repository.GetStudentGrade(obj.EnrollmentID);

            Assert.IsNotNull(resulted.EnrollmentID);
            Assert.IsNotNull(resulted.CourseID);
            Assert.IsNotNull(resulted.StudentID);

            Assert.IsTrue(resulted.EnrollmentID > 0);
            Assert.IsTrue(resulted.CourseID > 0);
            Assert.IsTrue(resulted.StudentID > 0);
            Assert.IsTrue(resulted.Grade > 0);

            StudentGradeTest.DeleteTestObject(obj, Repository);

        }

        [TestMethod]
        public void DeleteStudentGrade_Test()
        {
            var obj = CreateTestStudentGrade(Repository);

            var currentCount = Repository.GetAllStudentGrades().Count();

            StudentGradeTest.DeleteTestObject(obj, Repository);

            Assert.IsTrue(Repository.GetAllStudentGrades().Count() < currentCount);
        }

        #region UpdateStudentGrade tests
        [TestMethod]
        public void UpdateStudentGrade_Test()
        {
            var obj = CreateTestStudentGrade(Repository);

            try
            {
                Assert.IsTrue(Repository.GetAllStudentGrades().Any(x => x.EnrollmentID == obj.EnrollmentID));

                obj.Grade = (decimal)2.0;

                Repository.UpdateStudentGrade(obj);

                Assert.AreEqual((decimal)2.0, Repository.GetStudentGrade(obj.EnrollmentID).Grade);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                StudentGradeTest.DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void UpdateStudentGrade_Test_Invalid_Null()
        {
            try
            {
                var obj = Repository.UpdateStudentGrade(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.Contains("grade"));
            }
        }

        [TestMethod]
        public void UpdateStudentGrade_Test_Invalid_CourseID_LessThanZero()
        {
            var obj = CreateTestStudentGrade(Repository);
            var idToDelete = obj.CourseID;

            obj.CourseID = 0;

            try
            {
                obj = Repository.UpdateStudentGrade(obj);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.Contains("grade.CourseID"));
            }
            finally
            {
                obj.CourseID = idToDelete;
                DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void UpdateStudentGrade_Test_Invalid_StudentID_LessThanZero()
        {
            var obj = CreateTestStudentGrade(Repository);
            var studentIDToDelete = obj.StudentID;

            obj.StudentID = 0;

            try
            {
                obj = Repository.UpdateStudentGrade(obj);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.Contains("grade.StudentID"));
            }
            finally
            {
                obj.StudentID = studentIDToDelete;
                DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void UpdateStudentGrade_Test_Invalid_CourseID_NotFound()
        {
            //Find an ID that is not in the dataset.
            var notUsedID = CourseTest.GetUnusedCourse(Repository);

            var obj = CreateTestStudentGrade(Repository);
            var courseIDtoDelete = obj.CourseID;

            obj.CourseID = notUsedID;

            try
            {
                obj = Repository.UpdateStudentGrade(obj);
            }
            catch (KeyNotFoundException ex)
            {
                Assert.IsTrue(ex.Message.Contains("The courseID '"));
            }
            finally
            {
                obj.CourseID = courseIDtoDelete;
                DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void UpdateStudentGrade_Test_Invalid_StudentID_NotFound()
        {
            //Find an ID that is not in the dataset.
            var notUsedID = PersonTest.GetUnusedPersonID(Repository);

            var obj = CreateTestStudentGrade(Repository);
            var idToDelete = obj.StudentID;

            obj.StudentID = notUsedID;

            try
            {
                obj = Repository.UpdateStudentGrade(obj);
            }
            catch (KeyNotFoundException ex)
            {
                Assert.IsTrue(ex.Message.Contains("The studentID '"));
            }
            finally
            {
                obj.StudentID = idToDelete;
                DeleteTestObject(obj, Repository);
            }
        }

        [TestMethod]
        public void UpdateStudentGrade_Test_Invalid_EnrollmentID_Null()
        {
            var obj = new StudentGradeDTO();

            try
            {
                obj = Repository.UpdateStudentGrade(obj);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.Contains("grade.EnrollmentID"));
            }
        }

        [TestMethod]
        public void UpdateStudentGrade_Test_Invalid_EnrollmentID_NotFound()
        {
            //Find an ID that is not in the dataset.
            var notUsedID = GetUnusedEnrollmentID(Repository);

            var obj = CreateTestStudentGrade(Repository);
            var idToDelete = obj.EnrollmentID;

            obj.EnrollmentID = notUsedID;

            try
            {
                obj = Repository.UpdateStudentGrade(obj);
            }
            catch (KeyNotFoundException ex)
            {
                Assert.IsTrue(ex.Message.Contains("The enrollmentID '"));
            }
            finally
            {
                obj.EnrollmentID = idToDelete;
                DeleteTestObject(obj, Repository);
            }
        }

        #endregion

        /// <summary>
        /// Creates the test student grade.
        /// This will automatically create a student and assign it to the result object.
        /// This will automatically create a course and assign it to the result object.
        /// </summary>
        /// <param name="_repository">The repository.</param>
        /// <returns></returns>
        public static StudentGradeDTO CreateTestStudentGrade(ISchoolData _repository)
        {
            var obj = new StudentGradeDTO();
            obj.Person = PersonTest.CreateTestPerson(_repository);
            obj.Course = CourseTest.CreateTestCourse(_repository);
            obj.CourseID = obj.Course.CourseID;
            obj.StudentID = obj.Person.PersonID;
            obj.Grade = (decimal)3.5;
            
            obj = _repository.AddStudentGrade(obj);

            return obj;
        }

        /// <summary>
        /// Deletes the test object.
        /// </summary>
        /// <param name="toDelete">The object to delete.</param>
        /// <param name="_repository">The repository.</param>
        public static void DeleteTestObject(StudentGradeDTO toDelete, ISchoolData _repository)
        {
            _repository.DeleteStudentGrade(toDelete.EnrollmentID);
            _repository.DeleteCourse(toDelete.CourseID);
            _repository.DeleteDepartment(toDelete.Course.DepartmentID);
            _repository.DeletePerson(toDelete.StudentID);
        }

        /// <summary>
        /// Finds an Enrollment ID that is not in the system.
        /// </summary>
        /// <param name="_repository">The repository.</param>
        /// <returns></returns>
        public static int GetUnusedEnrollmentID(ISchoolData _repository)
        {
            var grades = _repository.GetAllStudentGrades();

            for (int ii = 1; ii < Int32.MaxValue; ii++)
            {
                bool isUsed = false;

                foreach (var grade in grades)
                {
                    if (grade.EnrollmentID.Equals(ii))
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
