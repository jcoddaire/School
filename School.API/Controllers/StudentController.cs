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
        /// Gets all students in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<StudentDTO> Get()
        {
            return Repository.GetAllStudents();
        }

        /// <summary>
        /// Gets a specific student.
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
        /// Creates a new student.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<StudentDTO> Post(StudentDTO student)
        {
            if (student == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            return Repository.CreateStudent(student);
        }

        /// <summary>
        /// Updates the student.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<StudentDTO> Put(StudentDTO student)
        {
            if (student == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            if (student.StudentID <= 0)
            {
                //return 404 not found.
                return NotFound();
            }

            var foundPerson = Repository.GetStudent(student.StudentID);
            if (foundPerson == null || foundPerson.StudentID <= 0)
            {
                //return 404 not found.
                return NotFound();
            }

            student = Repository.UpdateStudent(student);

            return student;
        }

        /// <summary>
        /// Removes the student from the system.
        /// </summary>
        /// <param name="id">The student ID.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                //Return 404 not found. Could also return 400.
                return NotFound();
            }

            var foundPerson = Repository.GetStudent(id);
            if (foundPerson == null || foundPerson.StudentID <= 0)
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
