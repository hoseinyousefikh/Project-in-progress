using App.Domain.Core.Home.Contract.AppServices.Other;
using App.Domain.Core.Home.Contract.AppServices.Users;
using App.Domain.Core.Home.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DwellApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly ICityAppService _cityAppService;
        private readonly IUserAppService _userAppService;
        public AuthenticationController(ICityAppService cityAppService, IUserAppService userAppService)
        {
            _cityAppService = cityAppService;
            _userAppService = userAppService;
        }


        [HttpGet("register/cities")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities(CancellationToken cancellationToken)
        {
            var cities = await _cityAppService.GetAllCitiesAsync(cancellationToken);
            var cityDtos = cities.Select(city => new CityDto
            {
                Id = city.Id,
                Name = city.Name
            }).ToList();

            return Ok(cityDtos);
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userAppService.RegisterAsync(
                model.FirstName, model.LastName, model.Email, model.Password,
                model.ConfirmPassword, model.CityId, model.RoleType, cancellationToken);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                return BadRequest(new { Message = "ثبت‌نام با خطا مواجه شد.", Errors = errors });
            }

            return Ok(new { Message = "ثبت‌نام با موفقیت انجام شد. لطفاً وارد شوید." });
        }

    }
}
