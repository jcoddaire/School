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
    public class StudentGradeController : ControllerBase
    {
        public StudentGradeController(ISchoolData repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets all grades in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<StudentGradeDTO> Get()
        {
            return Repository.GetAllStudentGrades();
        }

        /// <summary>
        /// Gets all grades for a given student.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<StudentGradeDTO> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var target = Repository.GetStudentGrade(id);
            if (target != null && target.EnrollmentID > 0)
            {
                return target;
            }

            //cannot find it, throw a 404.
            return NotFound();
        }

        /// <summary>
        /// Creates a new student grade.
        /// </summary>
        /// <param name="studentGrade">The student grade.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpPost]
        public ActionResult<StudentGradeDTO> Post(StudentGradeDTO studentGrade)
        {
            if (studentGrade == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            studentGrade = Repository.AddStudentGrade(studentGrade);

            return studentGrade;
        }

        /// <summary>
        /// Updates the specified student grade.
        /// </summary>
        /// <param name="studentGrade">The student grade.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException">
        /// </exception>
        [HttpPut]
        public ActionResult<StudentGradeDTO> Put(StudentGradeDTO studentGrade)
        {
            if (studentGrade == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            if (studentGrade.EnrollmentID <= 0)
            {
                //return 404 not found.
                return NotFound();
            }

            var target = Repository.GetStudentGrade(studentGrade.EnrollmentID);
            if (target == null)
            {
                //return 404 not found.
                return NotFound();
            }
            
            studentGrade = Repository.UpdateStudentGrade(studentGrade);

            return studentGrade;
        }

        /// <summary>
        /// Deletes the specified student grade.
        /// </summary>
        /// <param name="id">The enrollment id.</param>
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

            var target = Repository.GetStudentGrade(id);
            if (target == null)
            {
                //return 404 not found.
                return NotFound();
            }

            var result = Repository.DeleteStudentGrade(id);
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
