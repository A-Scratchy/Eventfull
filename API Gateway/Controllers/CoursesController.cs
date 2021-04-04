using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Courses.Domain;
using Courses.Domain.Exceptions;
using Courses.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure;

namespace API_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesContext _coursesContext;
        private readonly IPeopleContext _peopleContext;

        public CoursesController(ICoursesContext coursesContext, IPeopleContext peopleContext)
        {
            _coursesContext = coursesContext;
            _peopleContext = peopleContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseListItemVm>>> GetCourses()
        {
            var courses = await _coursesContext.Courses.ToListAsync();
            var response = new List<CourseListItemVm>();

            foreach (var course in courses)
            {
                var courseListItemVm = new CourseListItemVm
                {
                    CourseId = course.CourseId,
                    Capacity = course.Capacity,
                    CourseDate = course.CourseDate,
                    Modules = course.CourseModules.Aggregate("", (a, b) => a + b.ModuleName + " ")
                };

                response.Add(courseListItemVm);
            }

            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseVm>> GetCourse(Guid id)
        {
            var course = await _coursesContext.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            var courseVm = new CourseVm
            {
                CourseId = course.CourseId, Capacity = course.Capacity, CourseDate = course.CourseDate
            };

            foreach (var courseModule in course.CourseModules)
            {
                var mod = await _coursesContext.Modules.FindAsync(courseModule.ModuleId);
                var instructor = await _peopleContext.People.FindAsync(courseModule.Instructor);
                var moduleVm = new ModuleVm
                {
                    Name = mod.Name,
                    LengthInHours = mod.LengthInHours,

                    StartTime = courseModule.ModuleStartTime,
                };
                moduleVm.Instructor = (instructor == null)
                    ? null
                    : new PersonVm
                        {FirstName = instructor.FirstName, LastName = instructor.LastName, Email = instructor.Email};

                courseVm.CourseModules.Add(moduleVm);
            }

            foreach (var courseDelegate in course.CourseDelegates)
            {
                var person = await _peopleContext.People.FindAsync(courseDelegate.ContactId);
                courseVm.CourseDelegates.Add(new CourseDelegateVm
                {
                    Person = new PersonVm
                    {
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Email = person.Email
                    }
                });
            }

            return courseVm;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(Guid id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            _coursesContext.Entry(course).State = EntityState.Modified;
            try
            {
                await _coursesContext.SaveChangesAsync(new CancellationToken());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _coursesContext.Courses.Add(course);
            await _coursesContext.SaveChangesAsync(new CancellationToken());
            return CreatedAtAction("GetCourse", new
            {
                id = course.CourseId
            }, course);
        }

        [HttpPost]
        [Route("registerDelegate")]
        public async Task<ActionResult> RegisterDelegate(Guid personId, Guid accountId, Guid courseId)
        {
            var course = await _coursesContext.Courses.FindAsync(courseId);
            if (course == null)
            {
                return NotFound();
            }

            try
            {
                course.AddDelegate(personId, accountId);
                await _coursesContext.SaveChangesAsync(new CancellationToken());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(courseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpDelete]
        [Route("removeDelegate")]
        public async Task<ActionResult> RemoveDelegate(Guid personId, Guid accountId, Guid courseId)
        {
            var course = await _coursesContext.Courses.FindAsync(courseId);
            course.RemoveDelegate(personId, accountId);
            try
            {
                await _coursesContext.SaveChangesAsync(new CancellationToken());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(courseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpPost]
        [Route("registerCourseModule")]
        public async Task<ActionResult> RegisterCourseModule(Guid moduleId, Guid courseId, DateTime startTime,
                                                             int lengthInHours,
                                                             string moduleName,
                                                             Guid? instructorId = null)
        {
            var course = await _coursesContext.Courses.FindAsync(courseId);
            if (course == null)
            {
                return NotFound();
            }

            try
            {
                course.AddModule(moduleId, moduleName, startTime, lengthInHours, instructorId);
                await _coursesContext.SaveChangesAsync(new CancellationToken());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(courseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            catch (AddModuleException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("removeCourseModule")]
        public async Task<ActionResult> RemoveCourseModule(Guid moduleId, Guid courseId)
        {
            var course = await _coursesContext.Courses.FindAsync(courseId);
            if (course == null)
            {
                return NotFound();
            }

            try
            {
                course.RemoveCourseModule(moduleId);
                await _coursesContext.SaveChangesAsync(new CancellationToken());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(courseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            catch (ModuleNotRegisteredOnCourseException)
            {
                return BadRequest("Module is not registered on this course");
            }

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var course = await _coursesContext.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _coursesContext.Courses.Remove(course);
            await _coursesContext.SaveChangesAsync(new CancellationToken());
            return NoContent();
        }

        private bool CourseExists(Guid id)
        {
            return _coursesContext.Courses.Any(p => p.CourseId == id);
        }
    }

    public class CourseListItemVm
    {
        public int AvailableSpaces { get; set; }
        public Guid CourseId { get; set; }
        public DateTime CourseDate { get; set; }
        public int Capacity { get; set; }
        public string Modules { get; set; }
    }

    public class CourseVm
    {
        public Guid CourseId { get; set; }
        public DateTime CourseDate { get; set; }
        public int Capacity { get; set; }
        public ICollection<ModuleVm> CourseModules { get; set; } = new List<ModuleVm>();
        public ICollection<CourseDelegateVm> CourseDelegates { get; set; } = new List<CourseDelegateVm>();
        public ICollection<BookingVm> Bookings { get; set; } = new List<BookingVm>();
        public int TotalBooked => Bookings.Aggregate(0, (a, b) => a + b.PlacesBooked);
    }

    public class BookingVm
    {
        public AccountVm Account { get; set; }
        public DateTime BookingDate { get; set; }
        public int PlacesBooked { get; set; }
    }

    public class AccountVm
    {
        public Guid AccountId { get; set; }
        public string Name { get; set; }
        public PersonVm MainContact { get; set; }
    }

    public class PersonVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class CourseDelegateVm
    {
        public PersonVm Person { get; set; }
        public bool Attended { get; set; }
        public bool Passed { get; set; }
    }

    public class ModuleVm
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public int LengthInHours { get; set; }
        public DateTime EndTime => StartTime.AddHours(LengthInHours);
        public PersonVm Instructor { get; set; }
    }
}