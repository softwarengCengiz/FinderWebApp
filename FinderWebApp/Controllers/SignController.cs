using FinderWebApp.Models.ApiRequest.Sign;
using FinderWebApp.Models.ViewModels.Sign;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Application.User.Contract;
using AutoMapper;
using Application.User.Interfaces;
using FinderWebApp.Models.Request.Sign;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Application.Student.Interfaces;
using Application.Student.Contract;
using Application.Participant.Interfaces;
using Application.Participant.Contract;

namespace FinderWebApp.Controllers
{
    public class SignController : Controller
    {

        #region Constructor
        private readonly IMapper mapper;
        private readonly ISignUpService signUpService;
        private readonly ISignInService signinService;
        private readonly IStudentService studentService;
        private readonly IParticipantService participantService;
        public SignController(
            IMapper mapper,
            ISignUpService signUpService,
            ISignInService signinService,
            IStudentService studentService,
            IParticipantService participantService)
        {
            this.mapper = mapper;
            this.signUpService = signUpService;
            this.signinService = signinService;
            this.studentService = studentService;
            this.participantService = participantService;
        }
        #endregion


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpFormData formData)
        {
            var request = new SignUpRequest()
            {
                Name = formData.Name,
                Surname = formData.Surname,
                Email = formData.Email,
                PhoneNumber = formData.PhoneNumber,
                Password = formData.Password,
                Role = formData.Role
            };

            var mappingModel = mapper.Map<SignUpRequest, UserDto>(request);

            var result = await signUpService.SignUp(mappingModel).ConfigureAwait(false);

            if (formData.Role == 0)
            {
                var student = new StudentDto
                {
                    UserId = result
                };

                await studentService.AddStudent(student).ConfigureAwait(false);
            }

            else
            {
                var participant = new ParticipantDto
                {
                    UserId = result
                };

                await participantService.AddParticipant(participant).ConfigureAwait(false);
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,formData.Email)
                };

            var userIdentity = new ClaimsIdentity(claims, "User");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);

            Response.Cookies.Append("User", request.Name);
            Response.Cookies.Append("UserId", result.ToString());
            Response.Cookies.Append("Role", HashPassword(request.Role.ToString()));

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInFormData formData)
        {
            var request = new SignInRequest()
            {
                Email = formData.Email,
                Password = formData.Password,
            };


            var mappingModel = mapper.Map<SignInRequest, UserDto>(request);

            var result = await signinService.SignIn(mappingModel).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(result.Name) && !string.IsNullOrEmpty(result.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,formData.Email)
                };

                var userIdentity = new ClaimsIdentity(claims, "User");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                Response.Cookies.Append("User", result.Name);
                Response.Cookies.Append("UserId", result.Id.ToString());
                Response.Cookies.Append("Role", HashPassword(result.Role.ToString()));
            }

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            Response.Cookies.Delete("User");
            Response.Cookies.Delete("Role");
            Response.Cookies.Delete("UserId");

            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }


        #region Private Methodd
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Hash the input.
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string.
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        #endregion
    }
}
