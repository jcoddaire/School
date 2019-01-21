using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using School.DTOs;
using School.Data;
using Moq;
using School.Tests.Unit.Helpers;
using Microsoft.EntityFrameworkCore;

namespace School.Tests.Unit.Data
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PeopleRepository
    {
        //[TestMethod]
        //public void GetAllPeople_Valid()
        //{
        //    var people = PeopleHelper.GetPeopleList().AsQueryable();
            
        //    var mockSet = new Mock<DbSet<Person>>();
        //    mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(people.Provider);
        //    mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(people.Expression);
        //    mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(people.ElementType);
        //    //mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(0 => people.GetEnumerator());

        //    var mockContext = new Mock<SchoolContext>();
        //    mockContext.Setup(c => c.Person).Returns(mockSet.Object);

        //    var service = new SchoolServiceRepository(mockContext.Object);
        //    List<PersonDTO> result = service.GetAllPersons().ToList();

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(3, result.Count);
        //    Assert.AreEqual("Amber", result[0].FirstName);
        //    Assert.AreEqual("Ben", result[1].FirstName);
        //    Assert.AreEqual("Ronald", result[2].FirstName);

        //    Assert.AreEqual("Baar", result[0].LastName);
        //    Assert.AreEqual("Xavier", result[1].LastName);
        //    Assert.AreEqual("Weasley", result[2].LastName);

        //    Assert.AreEqual(3, result[0].PersonID);
        //    Assert.AreEqual(1, result[1].PersonID);
        //    Assert.AreEqual(5, result[2].PersonID);
        //}
    }
}
