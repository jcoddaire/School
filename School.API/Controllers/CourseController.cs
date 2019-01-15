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
    public class CourseController : ControllerBase
    {
        public CourseController(ISchoolData repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets all courses in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<CourseDTO> Get()
        {
            return Repository.GetAllCourses();
        }

        /// <summary>
        /// Gets a given course.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<CourseDTO> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var target = Repository.GetCourse(id);
            if (target != null && target.CourseID > 0)
            {
                return target;
            }

            //cannot find it, throw a 404.
            return NotFound();
        }

        /// <summary>
        /// Creates a new course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpPost]
        public ActionResult<CourseDTO> Post(CourseDTO course)
        {
            if (course == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }
            
            course = Repository.CreateCourse(course);

            return course;
        }

        /// <summary>
        /// Updates the specified course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException">
        /// </exception>
        [HttpPut]
        public ActionResult<CourseDTO> Put(CourseDTO course)
        {
            if (course == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            if (course.CourseID <= 0)
            {
                //return 404 not found.
                return NotFound();
            }

            var target = Repository.GetCourse(course.CourseID);
            if (target == null)
            {
                //return 404 not found.
                return NotFound();
            }
            
            course = Repository.UpdateCourse(course);

            return course;
        }

        /// <summary>
        /// Deletes the specified course.
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

            var taraget = Repository.GetCourse(id);
            if (taraget == null)
            {
                //return 404 not found.
                return NotFound();
            }

            var result = Repository.DeleteCourse(id);
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
