using CodeZone.Core.DTOs.Enrollments;
using CodeZone.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeZone.Web.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController (
            IStudentService studentService,
            ICourseService courseService,
            IEnrollmentService enrollmentService )
        {
            _studentService = studentService;
            _courseService = courseService;
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index ( )
        {
            var studentsResult = await _studentService.GetAllAsync ( );
            var coursesResult = await _courseService.GetAllAsync ( );

            ViewBag.Students = new SelectList ( studentsResult.Data, "Id", "FullName" );
            ViewBag.Courses = new SelectList ( coursesResult.Data, "Id", "Title" );

            return View ( );
        }

        [HttpPost]
        public async Task<IActionResult> Enroll ( [FromBody] EnrollmentRequest request )
        {
            if ( !ModelState.IsValid )
                return BadRequest ( "Invalid data sent." );

            var result = await _enrollmentService.EnrollStudentAsync ( request );

            if ( result.IsSuccess )
                return Ok ( new { message = "Student enrolled successfully" } );
            

            return BadRequest ( new { message = result.ErrorMessage } );
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseSlots ( int courseId )
        {
            var slots = await _enrollmentService.GetAvailableSlotsAsync ( courseId );
            return Ok ( new { availableSlots = slots } );
        }
    }
}