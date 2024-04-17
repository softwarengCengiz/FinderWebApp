using Application.Common;
using FinderWebApp.Models.ApiRequest.Sign;
using FinderWebApp.Models.ViewModels.Sign;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Application.User.Contract;
using Application.User.Services;
using AutoMapper;
using Application.User.Interfaces;

namespace FinderWebApp.Controllers
{
    public class SignController : Controller
    {

        #region Constructor
        private readonly IMapper mapper;
        private readonly ISignUpService signUpService;
        public SignController(
            IMapper mapper,
            ISignUpService signUpService)
        {
            this.mapper = mapper;
            this.signUpService = signUpService;
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
                Password = formData.Password,
                Role = formData.Role
            };


            var mappingModel = mapper.Map<SignUpRequest, UserDto>(request);

            var result = await signUpService.SignUp(mappingModel).ConfigureAwait(false);

            return View(result);
        }
    }
}
