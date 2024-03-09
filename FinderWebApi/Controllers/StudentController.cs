using Application.Student.Contract;
using Application.Student.Interfaces;
using AutoMapper;
using FinderWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FinderWebApi.Controllers
{
    [Route("Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        #region Constructor
        private readonly IMapper mapper;
        private readonly IStudentService studentService;
        public StudentController(
            IMapper mapper,
            IStudentService studentService)
        {
            this.mapper = mapper;
            this.studentService = studentService;
        }
        #endregion

        [HttpGet("AddStudent")]
        [Authorize]
        public async Task<IActionResult> AddStudent(Student student)
        {
            var mappingModel = mapper.Map<Student, StudentDto>(student);

            var result = await studentService.AddStudent(mappingModel).ConfigureAwait(false);

            if (result)
            {
                return Ok(true);
            }

            return Ok(false);
        }
    }
}
