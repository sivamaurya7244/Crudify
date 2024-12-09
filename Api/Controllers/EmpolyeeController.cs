using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Request;
using Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Models.Responce;

namespace API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/Empolyee")]

    public class EmpolyeeController : ControllerBase
    {
        public static IEmpolyeeService _IEmpolyeeService;

        private readonly IConfiguration _configuration;
        public EmpolyeeController(IEmpolyeeService IEmpolyeeService, IConfiguration IConfiguration)
        {
            _IEmpolyeeService = IEmpolyeeService;
            _configuration = IConfiguration;
        }
        [Authorize]
        [HttpPost]
        [Route("GetEmployeeList")]

        public async Task<IActionResult> GetEmpolyeeList()
        {
            try
            {
                var res = await _IEmpolyeeService.GetEmpolyeeList();
                return Ok(res);

            }
            catch (Exception)
            {

                throw;
            }


        }

        [HttpPost]
        [Route("InsertEmpolyee")]

        public async Task<IActionResult> InsertEmpolyeeData([FromBody] EmpolyeeAddUpdateParam obj)
        {

            try
            {
                var res = await _IEmpolyeeService.InsertEmpolyeeData(obj);
                return Ok(res.message);

            }
            catch (Exception)
            {

                throw;
            }
            
        }


        [HttpPost]
        [Route("UpdateEmpolyee")]
        public async Task<IActionResult> UpdateEmpolyeeData([FromBody] EmpolyeeAddUpdateParam obj)
        {
            try
            {
                var res = await _IEmpolyeeService.InsertEmpolyeeData(obj);
                return Ok(res.message);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("DeleteEmpolyee")]
        public async Task<IActionResult> DeleteEmpolyeeData([FromBody] EmpolyeeDeleteParam obj)
        {
            try
            {
                var res = await _IEmpolyeeService.DeleteEmpolyeeData(obj);
                return Ok(res.message);

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [Authorize]
        [HttpPost]
        [Route("DetailEmpolyee")]
        public async Task<IActionResult> DetailEmpolyeeData([FromBody] EmpolyeeDetailParam obj)
        {
            try
            {
                var res = await _IEmpolyeeService.DetailEmpolyeeData(obj);
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("LoginUser")]

        public async Task<IActionResult> Login([FromBody] UserLoginParam obj)
        {
            clsResponse clsResponse = new clsResponse();
            var vResponse = await _IEmpolyeeService.ValidateUser(obj);

            try
            {
                if (vResponse.LoginSuccess == 1)
                {


                    var token = IssueToken(vResponse);
                    clsResponse.isSuccess = true;
                    clsResponse.data = token;


                    return Ok(clsResponse);
                }
                else
                {
                    return Ok(clsResponse);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private string IssueToken(EmpolyeeList user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);



            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(ClaimTypes.Name, user.UserId),

            };

            var token = new JwtSecurityToken(
               issuer: _configuration["Jwt:Issuer"],
               audience: _configuration["Jwt:Audience"],
               claims: claims,
               expires: DateTime.Now.AddHours(1),
               signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }

}