using Application.Participant.Contract;
using Application.Participant.Interfaces;
using Application.Student.Contract;
using Application.Student.Interfaces;
using Application.User.Contract;
using Application.User.Interfaces;
using Domain.User;
using FinderWebApp.Models.Request.Participant;
using FinderWebApp.Models.Request.Student;
using FinderWebApp.Models.ViewModels.API;
using FinderWebApp.Models.ViewModels.Profile;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinderWebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly AppDbContext context;
        private readonly IStudentService studentService;
        private readonly IParticipantService participantService;
        private readonly IUserService userService;

        public ProfileController(IHttpContextAccessor HttpContextAccessor, AppDbContext context, IStudentService studentService, IParticipantService participantService, IUserService userService)
        {
            this.HttpContextAccessor = HttpContextAccessor;
            this.context = context;
            this.studentService = studentService;
            this.participantService = participantService;
            this.userService = userService;
        }

        #region Student Section

        [HttpGet]
        public async Task<IActionResult> StudentProfile(Guid? id)
        {
            var student = context.Students.FirstOrDefault(x => x.UserId == id);
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            if (student != null && user != null)
            {
                string universitiesJson = await GetAllUniversities();
                List<University> universities = JsonConvert.DeserializeObject<List<University>>(universitiesJson);


                var model = new StudentProfileViewModel
                {
                    Student = student,
                    User = user,
                    Universities = universities
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
                Id = request.Id,
                Name = request.Name,
                Surname = request.Lastname,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
            };

            await userService.UpdateUser(user);

            var student = new StudentDto
            {
                UserId = request.Id,
                University = request.University
            };

            var result = await studentService.UpdateStudent(student);

            return Redirect("/StudentProfile?id=" + result);
        }

        [HttpGet]
        public async Task<string> GetAllUniversities()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = "https://api.kadircolak.com/Universite/JSON/API/AllUniversity";
                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    return responseBody;
                }
                catch (HttpRequestException e)
                {
                    return e.Message;
                }
            }
        }

        #endregion


        #region ParticipantSection

        [HttpGet]
        public IActionResult ParticipantProfile(Guid? id)
        {
            var participant = context.Participants.FirstOrDefault(x => x.UserId == id);
            var user = context.Users.FirstOrDefault(x => x.Id == id);

            if (participant != null && user != null)
            {
               
                var model = new ParticipantProfileViewModel
                {
                    Participant = participant,
                    User = user
                };

                return View(model);
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ParticipantProfile(ParticipantProfileRequest request)
        {
            var user = new UserDto
            {
                Id = request.Id,
                Name = request.Name,
                Surname = request.Lastname,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
            };

            await userService.UpdateUser(user);

            var participant = new ParticipantDto
            {
                UserId = request.Id,
                Job = request.Job,
                Company = request.Company,
                CV = request.CV
            };

            var result = await participantService.UpdateParticipant(participant);

            return Redirect("/ParticipantProfile?id=" + result);
        }

        #endregion
    }
}

