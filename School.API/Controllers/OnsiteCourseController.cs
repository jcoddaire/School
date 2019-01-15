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
    public class OnsiteCourseController : ControllerBase
    {
        public OnsiteCourseController(ISchoolData repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets all onsite courses in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<OnsiteCourseDTO> Get()
        {
            return Repository.GetAllOnsiteCourses();
        }

        /// <summary>
        /// Gets a given Onsite Course.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<OnsiteCourseDTO> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var target = Repository.GetOnsiteCourse(id);
            if (target != null && target.CourseID > 0)
            {
                return target;
            }

            //cannot find it, throw a 404.
            return NotFound();
        }

        /// <summary>
        /// Creates a new Onsite Course.
        /// </summary>
        /// <param name="onsiteCourse">The Onsite Course.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpPost]
        public ActionResult<OnsiteCourseDTO> Post(OnsiteCourseDTO onsiteCourse)
        {
            if (onsiteCourse == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            onsiteCourse = Repository.AddOnsiteCourse(onsiteCourse);

            return onsiteCourse;
        }

        /// <summary>
        /// Updates the specified Onsite Course.
        /// </summary>
        /// <param name="onsiteCourse">The Onsite Course.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException">
        /// </exception>
        [HttpPut]
        public ActionResult<OnsiteCourseDTO> Put(OnsiteCourseDTO onsiteCourse)
        {
            if (onsiteCourse == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            if (onsiteCourse.CourseID <= 0)
            {
                //return 404 not found.
                return NotFound();
            }

            var target = Repository.GetOnsiteCourse(onsiteCourse.CourseID);
            if (target == null)
            {
                //return 404 not found.
                return NotFound();
            }
            
            onsiteCourse = Repository.UpdateOnsiteCourse(onsiteCourse);

            return onsiteCourse;
        }

        /// <summary>
        /// Deletes the specified Onsite Course.
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

            var target = Repository.GetOnsiteCourse(id);
            if (target == null)
            {
                //return 404 not found.
                return NotFound();
            }

            var result = Repository.DeleteOnsiteCourse(id);
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
