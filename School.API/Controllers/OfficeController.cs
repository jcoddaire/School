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
    public class OfficeController : ControllerBase
    {
        public OfficeController(ISchoolData repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets all offices in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<OfficeAssignmentDTO> Get()
        {
            return Repository.GetAllOfficeAssignments();
        }

        /// <summary>
        /// Gets a given Office.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<OfficeAssignmentDTO> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var target = Repository.GetOfficeAssignment(id);
            if (target != null && target.InstructorID > 0)
            {
                return target;
            }

            //cannot find it, throw a 404.
            return NotFound();
        }

        /// <summary>
        /// Creates a new office.
        /// </summary>
        /// <param name="office">The office.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpPost]
        public ActionResult<OfficeAssignmentDTO> Post(OfficeAssignmentDTO office)
        {
            if (office == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            office = Repository.CreateOfficeAssignment(office);

            return office;
        }

        /// <summary>
        /// Updates the specified office.
        /// </summary>
        /// <param name="office">The office (not the show).</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException">
        /// </exception>
        [HttpPut]
        public ActionResult<OfficeAssignmentDTO> Put(OfficeAssignmentDTO office)
        {
            if (office == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            if (office.InstructorID <= 0)
            {
                //return 404 not found.
                return NotFound();
            }

            var target = Repository.GetOfficeAssignment(office.InstructorID);
            if (target == null)
            {
                //return 404 not found.
                return NotFound();
            }
            
            office = Repository.UpdateOfficeAssignment(office);

            return office;
        }

        /// <summary>
        /// Deletes the specified office.
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

            var target = Repository.GetOfficeAssignment(id);
            if (target == null)
            {
                //return 404 not found.
                return NotFound();
            }

            var result = Repository.DeleteOfficeAssignment(id);
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
