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
    public class InstructorController : ControllerBase
    {
        public InstructorController(ISchoolData repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets all instructors in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<InstructorDTO> Get()
        {
            return Repository.GetAllInstructors().ToList();
        }

        /// <summary>
        /// Gets a single instructor
        /// </summary>
        /// <param name="id">The instructor ID.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<InstructorDTO> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var target = Repository.GetInstructor(id);
            if (target != null && target.InstructorID > 0)
            {
                return target;
            }

            //cannot find it, throw a 404.
            return NotFound();
        }

        /// <summary>
        /// Creates a new instructor.
        /// </summary>
        /// <param name="instructor">The instructor.</param>
        [HttpPost]
        public ActionResult<InstructorDTO> Post(InstructorDTO instructor)
        {
            if (instructor == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            instructor = Repository.CreateInstructor(instructor);

            return instructor;
        }

        /// <summary>
        /// Updates the instructor.
        /// </summary>
        /// <param name="instructor">The instructor.</param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<InstructorDTO> Put(InstructorDTO instructor)
        {
            if (instructor == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            if (instructor.InstructorID <= 0)
            {
                //return 404 not found.
                return NotFound();
            }

            var targetInstructor = Repository.GetInstructor(instructor.InstructorID);
            if (targetInstructor == null)
            {
                //return 404 not found.
                return NotFound();
            }

            if (targetInstructor.FirstName.Equals(instructor.FirstName)
                && targetInstructor.LastName.Equals(instructor.LastName)
                && targetInstructor.HireDate.Equals(instructor.HireDate)
                && targetInstructor.Terminated.Equals(instructor.Terminated))
            {
                if(instructor.Courses == null && targetInstructor.Courses == null)
                {
                    //There are no changes to the object.
                    //return 204 no change.
                    return instructor;
                }
            }

            instructor = Repository.UpdateInstructor(instructor);

            return instructor;
        }

        /// <summary>
        /// Deletes the instructor.
        /// </summary>
        /// <param name="id">The instructor.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                //Return 404 not found. Could also return 400.
                return NotFound();
            }

            var foundPerson = Repository.GetInstructor(id);
            if (foundPerson == null)
            {
                //return 404 not found.
                return NotFound();
            }

            var result = Repository.DeleteInstructor(id);
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
