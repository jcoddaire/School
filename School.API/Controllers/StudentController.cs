using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using School.Data;
using School.DTOs;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public StudentController(ISchoolData repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets all people in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<StudentDTO> Get()
        {
            return Repository.GetAllStudents();
        }

        /// <summary>
        /// Gets a given person.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<StudentDTO> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var target = Repository.GetStudent(id);
            if (target != null && target.StudentID > 0)
            {
                return target;
            }

            //cannot find it, throw a 404.
            return NotFound();
        }

        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpPost]
        public ActionResult<StudentDTO> Post(StudentDTO person)
        {
            if (person == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            person = Repository.CreateStudent(person);

            return person;
        }

        /// <summary>
        /// Updates the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException">
        /// </exception>
        [HttpPut]
        public ActionResult<StudentDTO> Put(StudentDTO person)
        {
            if (person == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            if (person.StudentID <= 0)
            {
                //return 404 not found.
                return NotFound();
            }

            var foundPerson = Repository.GetStudent(person.StudentID);
            if (foundPerson == null)
            {
                //return 404 not found.
                return NotFound();
            }

            if (foundPerson.FirstName.Equals(person.FirstName)
                && foundPerson.LastName.Equals(person.LastName)
                && foundPerson.EnrollmentDate.Equals(person.EnrollmentDate)
                && foundPerson.EnrollmentDate.Equals(person.EnrollmentDate))
            {
                //There are no changes to the object.
                //return 204 no change.
                return person;
            }

            person = Repository.UpdateStudent(person);

            return person;
        }

        /// <summary>
        /// Deletes the specified person.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException">
        /// </exception>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                //Return 404 not found. Could also return 400.
                return NotFound();
            }

            var foundPerson = Repository.GetStudent(id);
            if (foundPerson == null)
            {
                //return 404 not found.
                return NotFound();
            }

            var result = Repository.DeleteStudent(id);
            if (result > 0)
            {
                //return 204 no content.
                return NoContent();
            }

            //something went wrong. TODO: find a better way to handle this.            
            return StatusCode(500, "This should never happen.");
        }
    }
}
