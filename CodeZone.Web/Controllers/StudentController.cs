using AutoMapper;
using CodeZone.Core.Common;
using CodeZone.Core.DTOs.Students;
using CodeZone.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeZone.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController ( IStudentService studentService, IMapper mapper )
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<IActionResult> Index ( )
        //{
        //    var result = await _studentService.GetAllAsync ( );

        //    if ( result.IsSuccess )
        //        return View ( result.Data );

        //    return View ( new List<StudentResponse> ( ) );
        //}


        [HttpGet]
        public async Task<IActionResult> Index ( string search = "", int page = 1, int pageSize = 5 )
        {
            var result = await _studentService.GetFilteredAsync ( search, page, pageSize );
            
            ViewBag.CurrentSearch = search;

            if ( result.IsSuccess )
                return View ( result.Data );
           
            return View ( new PaginatedResult<StudentResponse> ( new List<StudentResponse> ( ), 0, 1, pageSize ) );
        }



        [HttpGet]
        public IActionResult Create ( )
        {
            return View ( );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ( StudentAddRequest request )
        {
            if ( !ModelState.IsValid )
                return View ( request );

            var result = await _studentService.AddAsync ( request );

            if ( result.IsSuccess )
            {
                TempData ["SuccessMessage"] = "Student added successfully";
                return RedirectToAction ( nameof ( Index ) );
            }

            ModelState.AddModelError ( string.Empty, result.ErrorMessage );
            return View ( request );
        }

        [HttpGet]
        public async Task<IActionResult> Edit ( int id )
        {
            var result = await _studentService.GetByIdAsync ( id );

            if ( !result.IsSuccess )
                return NotFound ( );

            var updateDto = _mapper.Map<StudentUpdateRequest> ( result.Data );

            return View ( updateDto );
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ( int id, StudentUpdateRequest request )
        {
            if ( id != request.Id )
                return BadRequest ( );

            if ( !ModelState.IsValid ) 
                return View ( request );

            var result = await _studentService.UpdateAsync ( request );

            if ( result.IsSuccess )
            {
                TempData ["SuccessMessage"] = "Student updated successfully";
                return RedirectToAction ( nameof ( Index ) );
            }

            ModelState.AddModelError ( string.Empty, result.ErrorMessage );
            return View ( request );
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete ( int id )
        {
            var result = await _studentService.DeleteAsync ( id );

            if ( result.IsSuccess )
                TempData ["SuccessMessage"] = "Student deleted successfully";
            
            else
                TempData ["ErrorMessage"] = result.ErrorMessage;
            

            return RedirectToAction ( nameof ( Index ) );
        }
    }
}