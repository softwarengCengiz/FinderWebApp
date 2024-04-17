using Application.Student.Contract;
using Application.Student.Interfaces;
using Application.Student.Services;
using Application.User.Contract;
using Application.User.Interfaces;
using AutoMapper;
using FinderWebApi.Models.Sign.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinderWebApi.Controllers
{
    [Route("SignUp")]
    [ApiController]
    public class SignController : ControllerBase
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

        [HttpPost("AddUser")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpFormData formData)
        {
            var mappingModel = mapper.Map<SignUpFormData, UserDto>(formData);

            var result = await signUpService.SignUp(mappingModel).ConfigureAwait(false);

            if (result)
            {
                return Ok(true);
            }

            return Ok(false);
        }
    }
}
