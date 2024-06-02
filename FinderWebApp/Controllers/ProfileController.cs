using Application.Community.Interfaces;
using Application.Participant.Contract;
using Application.Participant.Interfaces;
using Application.Student.Contract;
using Application.Student.Interfaces;
using Application.User.Contract;
using Application.User.Interfaces;
using Application.User.Services;
using Azure.Core;
using FinderWebApp.Models.Request.Participant;
using FinderWebApp.Models.Request.Student;
using FinderWebApp.Models.ViewModels.API;
using FinderWebApp.Models.ViewModels.Profile;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenAI_API.Moderation;

namespace FinderWebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly AppDbContext context;
        private readonly IStudentService studentService;
        private readonly IParticipantService participantService;
        private readonly IUserService userService;
        private readonly ICommunityService communityService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProfileController(IHttpContextAccessor HttpContextAccessor,
            AppDbContext context,
            IStudentService studentService,
            IParticipantService participantService,
            IUserService userService,
            ICommunityService communityService,
            IWebHostEnvironment hostingEnvironment)
        {
            this.HttpContextAccessor = HttpContextAccessor;
            this.context = context;
            this.studentService = studentService;
            this.participantService = participantService;
            this.userService = userService;
            this.communityService = communityService;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Student Section

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> StudentProfile(Guid? id)
        {
            var student = context.Students.FirstOrDefault(x => x.UserId == id);
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            if (student != null && user != null)
            {
                string universitiesJson = await GetAllUniversities();
                List<University> universities = JsonConvert.DeserializeObject<List<University>>(universitiesJson);
                var community = communityService.GetCommunityByUserId(student.UserId);

                var model = new StudentProfileViewModel
                {
                    Student = student,
                    User = user,
                    Universities = universities,
                };

                if (community != null)
                {
                    model.Community = community.Result.CommunityName;
                }

                return View(model);
            }

            return View();
        }

        [Authorize]
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

        [Authorize]
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
        [Authorize]
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

        [Authorize]
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

        [HttpGet]
        [AllowAnonymous]

        public IActionResult UserCVProfile(Guid userId)
        {
            var userInfo = userService.GetUser(userId).Result;
            var participantInfo = participantService.GetParticipant(userId).Result;

            var model = new CVUserViewModel
            {
                Name = userInfo.Name,
                Surname = userInfo.Surname,
                Email = userInfo.Email,
                Job = participantInfo.Job,
                CV = participantInfo.CV,
                Company = participantInfo.Company,
                Photo = userInfo.Photo,
                Role = userInfo.Role
            };
            return View(model);
        }

        #endregion

        [Authorize]
        [HttpPost]
        public IActionResult ChangeProfilePhoto(IFormFile file, string userId, string flag)
        {
            var uniqueImgFileName = String.Empty;
            string? uploadsFolder;
            if (file != null && file.Length > 0)
            {
                if (flag == "1")
                {
                    uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "files", "ProfileImages", "ParticipantImages");
                }
                else
                {
                    uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "files", "ProfileImages", "StudentImages");
                }
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueImgFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                var filePath = Path.Combine(uploadsFolder, uniqueImgFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            userService.ChangeProfilePhoto(uniqueImgFileName, Guid.Parse(userId));

            return Redirect("/UserCVProfile?id=" + Guid.Parse(userId));
        }
    }
}

