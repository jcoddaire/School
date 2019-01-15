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
    public class DepartmentController : ControllerBase
    {
        public DepartmentController(ISchoolData repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets all departments in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<DepartmentDTO> Get()
        {
            return Repository.GetAllDepartments();
        }

        /// <summary>
        /// Gets a given Department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<DepartmentDTO> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var target = Repository.GetDepartment(id);
            if (target != null && target.DepartmentID > 0)
            {
                return target;
            }

            //cannot find it, throw a 404.
            return NotFound();
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpPost]
        public ActionResult<DepartmentDTO> Post(DepartmentDTO department)
        {
            if (department == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            department = Repository.CreateDepartment(department);

            return department;
        }

        /// <summary>
        /// Updates the specified department.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException">
        /// </exception>
        [HttpPut]
        public ActionResult<DepartmentDTO> Put(DepartmentDTO department)
        {
            if (department == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            if (department.DepartmentID <= 0)
            {
                //return 404 not found.
                return NotFound();
            }

            var target = Repository.GetDepartment(department.DepartmentID);
            if (target == null)
            {
                //return 404 not found.
                return NotFound();
            }
            
            department = Repository.UpdateDepartment(department);

            return department;
        }

        /// <summary>
        /// Deletes the specified department.
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

            var target = Repository.GetDepartment(id);
            if (target == null)
            {
                //return 404 not found.
                return NotFound();
            }

            var result = Repository.DeleteDepartment(id);
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
