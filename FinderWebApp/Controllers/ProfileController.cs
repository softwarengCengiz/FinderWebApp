using Application.Student.Contract;
using Application.Student.Interfaces;
using Application.User.Contract;
using Application.User.Interfaces;
using Domain.User;
using FinderWebApp.Models.Request.Student;
using FinderWebApp.Models.ViewModels.Profile;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinderWebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly AppDbContext context;
        private readonly IStudentService studentService;
        private readonly IUserService userService;

        public ProfileController(IHttpContextAccessor HttpContextAccessor, AppDbContext context, IStudentService studentService, IUserService userService)
        {
            this.HttpContextAccessor = HttpContextAccessor;
            this.context = context;
            this.studentService = studentService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult StudentProfile(Guid? id)
        {
            var student = context.Students.FirstOrDefault(x => x.UserId == id);
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            if (student != null && user != null) 
            {
                var model = new StudentProfileViewModel
                {
                    Student = student,
                    User = user
                };
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StudentProfile(StudentProfileRequest request)
        {
            var user = new UserDto
            {
                Id= request.Id,
                Name = request.Name,
                Surname = request.Lastname,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
            };

            await userService.UpdateUser(user);

            var student = new StudentDto
            {
                UserId = request.Id,
                University = request.University,
                Department = request.Department
            };

            var result = await studentService.UpdateStudent(student);

            return Redirect("/Profile?id=" + result);
        }
    }
}
