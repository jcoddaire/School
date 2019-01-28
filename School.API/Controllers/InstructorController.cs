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
            var instructors = Repository.GetAllInstructors().ToList();

            //Add course data to the instructors objects.
            instructors = AddCourses(instructors);

            return instructors;
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
                //TODO: fix this. Data layer maybe.
                var n = new List<InstructorDTO>();
                n.Add(target);
                n = AddCourses(n);

                return n.FirstOrDefault();
            }

            //cannot find it, throw a 404.
            return NotFound();
        }

        /// <summary>
        /// Creates a new instructor.
        /// </summary>
        /// <param name="person">The instructor.</param>
        [HttpPost]
        public ActionResult<InstructorDTO> Post(InstructorDTO person)
        {
            if (person == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            person = Repository.CreateInstructor(person);

            return person;
        }

        /// <summary>
        /// Updates the instructor.
        /// </summary>
        /// <param name="person">The instructor.</param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<InstructorDTO> Put(InstructorDTO person)
        {
            if (person == null)
            {
                //return 400 bad reqeust.
                return BadRequest();
            }

            if (person.InstructorID <= 0)
            {
                //return 404 not found.
                return NotFound();
            }

            var foundPerson = Repository.GetInstructor(person.InstructorID);
            if (foundPerson == null)
            {
                //return 404 not found.
                return NotFound();
            }

            if (foundPerson.FirstName.Equals(person.FirstName)
                && foundPerson.LastName.Equals(person.LastName)
                && foundPerson.HireDate.Equals(person.HireDate)
                && foundPerson.Terminated.Equals(person.Terminated))
            {
                //There are no changes to the object.
                //return 204 no change.
                return person;
            }

            person = Repository.UpdateInstructor(person);

            return person;
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

        /// <summary>Adds courses to the list of instructors.</summary>
        /// <param name="instructors">The instructors.</param>
        /// <returns></returns>
        private List<InstructorDTO> AddCourses(List<InstructorDTO> instructors)
        {
            var courses = Repository.GetAllCourses().ToList();
            if (courses == null || courses.Count() <= 0)
            {
                return instructors;
            }

            foreach (var i in instructors)
            {
                i.Courses = new List<CourseDTO>();

                var instructorCourses = Repository.GetCoursesByInstructor(i.InstructorID);
                if (instructorCourses != null && instructorCourses.Count() > 0)
                {
                    foreach (var ic in instructorCourses)
                    {
                        var courseData = courses.Where(x => x.CourseID == ic.CourseID).FirstOrDefault();
                        if (courseData != null)
                        {
                            i.Courses.Add(courseData);
                        }
                    }
                }
            }

            return instructors;
        }
    }
}
