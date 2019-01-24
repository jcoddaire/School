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
    public class StudentCourseController : ControllerBase
    {
        public StudentCourseController(ISchoolData repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets all student courses in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<StudentCourseDTO> Get()
        {
            return Repository.GetAllStudentCourses();
        }

        /// <summary>
        /// Gets all courses for a given student.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<StudentCourseDTO>> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var target = Repository.GetStudentCourse(id);
            if (target != null && target.Count() > 0)
            {
                return target.ToList();
            }

            //cannot find it, throw a 404.
            return NotFound();
        }        
    }
}
