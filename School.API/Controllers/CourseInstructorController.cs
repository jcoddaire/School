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
    public class CourseInstructorController : ControllerBase
    {
        public CourseInstructorController(ISchoolData repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets all Instructors and courses in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<CourseInstructorDTO> Get()
        {
            return Repository.GetAllCourseInstructors();
        }
    }
}
