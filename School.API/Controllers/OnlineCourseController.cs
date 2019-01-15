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
    public class OnlineCourseController : ControllerBase
    {
        public OnlineCourseController(ISchoolData repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets all online courses in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<OnlineCourseDTO> Get()
        {
            return Repository.GetAllOnlineCourses();
        }

        /// <summary>
        /// Gets a given online course.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<OnlineCourseDTO> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var target = Repository.GetOnlineCourse(id);
            if (target != null && target.CourseID > 0)
            {
                return target;
            }

            //cannot find it, throw a 404.
            return NotFound();
        }

        /// <summary>
        /// Creates a new online course.
        /// </summary>
        /// <param name="target">The online course.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpPost]
        public ActionResult<OnlineCourseDTO> Post(OnlineCourseDTO target)
        {
            if (target == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            target = Repository.AddOnlineCourse(target);

            return target;
        }

        /// <summary>
        /// Updates the specified online course.
        /// </summary>
        /// <param name="onlineCourse">The course.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException">
        /// </exception>
        [HttpPut]
        public ActionResult<OnlineCourseDTO> Put(OnlineCourseDTO onlineCourse)
        {
            if (onlineCourse == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            if (onlineCourse.CourseID <= 0)
            {
                //return 404 not found.
                return NotFound();
            }

            var target = Repository.GetOnlineCourse(onlineCourse.CourseID);
            if (target == null)
            {
                //return 404 not found.
                return NotFound();
            }
            
            target = Repository.UpdateOnlineCourse(target);

            return target;
        }

        /// <summary>
        /// Deletes the specified Online Course.
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

            var target = Repository.GetOnlineCourse(id);
            if (target == null)
            {
                //return 404 not found.
                return NotFound();
            }

            var result = Repository.DeleteOnlineCourse(id);
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
