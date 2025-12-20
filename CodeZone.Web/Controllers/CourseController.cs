using AutoMapper;
using CodeZone.Core.DTOs.Courses;
using CodeZone.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeZone.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseController ( ICourseService courseService, IMapper mapper )
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index ( )
        {
            var result = await _courseService.GetAllAsync ( );
            return View ( result.Data ?? new List<CourseResponse> ( ) );
        }
        
        [HttpGet]
        public async Task<IActionResult> GetStudents ( int id )
        {
            var students = await _courseService.GetEnrolledStudentsAsync ( id );
            return Json ( students );
        }

        [HttpGet]
        public IActionResult Create ( )
        {
            return View ( );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ( CourseAddRequest request )
        {
            if ( !ModelState.IsValid ) return View ( request );

            var result = await _courseService.AddAsync ( request );

            if ( result.IsSuccess )
            {
                TempData ["SuccessMessage"] = "Course added successfully";
                return RedirectToAction ( nameof ( Index ) );
            }

            ModelState.AddModelError ( "", result.ErrorMessage );
            return View ( request );
        }

        [HttpGet]
        public async Task<IActionResult> Edit ( int id )
        {
            var result = await _courseService.GetByIdAsync ( id );
            if ( !result.IsSuccess ) return NotFound ( );

            var updateDto = _mapper.Map<CourseUpdateRequest> ( result.Data );
            return View ( updateDto );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ( int id, CourseUpdateRequest request )
        {
            if ( id != request.Id ) return BadRequest ( );
            if ( !ModelState.IsValid ) return View ( request );

            var result = await _courseService.UpdateAsync ( request );

            if ( result.IsSuccess )
            {
                TempData ["SuccessMessage"] = "Course updated successfully";
                return RedirectToAction ( nameof ( Index ) );
            }

            ModelState.AddModelError ( "", result.ErrorMessage );
            return View ( request );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete ( int id )
        {
            await _courseService.DeleteAsync ( id );
            TempData ["SuccessMessage"] = "Course deleted successfully";
            return RedirectToAction ( nameof ( Index ) );
        }
    }
}