using API.Models;
using API.Repository;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public IConfiguration _configuration;
        public AccountsController(AccountRepository accountRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;
        }

        //[Route("login")]
        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var code = 0;
            var message = "";

            var result = accountRepository.Login(loginVM);

            switch (result)
            {
                case 1:
                    {
                        code = StatusCodes.Status200OK;
                        message = $"Login Success";

                        ///////////////JWT TOKEN GENERATOR////////////////
                        var getRole = accountRepository.GetRoles(loginVM);

                        var claims = new List<Claim>
                            {
                                new Claim("Email", loginVM.email)
                            };
                        foreach (var item in getRole)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item.ToString().ToLower()));
                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.Now.AddMinutes(10),
                            signingCredentials: signIn
                        );

                        var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                        claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
                        ///////////////////////////////////////////////////////////

                        return Ok(new JWTokenVM { code = HttpStatusCode.OK, token = idtoken, message = message });
                    }
                case 2:
                    {
                        message = $"Wrong Password";
                        return Ok(new JWTokenVM { code = HttpStatusCode.Forbidden, token = null, message = message });
                    }
                case 3:
                    {
                        message = $"Email not found";
                        return Ok(new JWTokenVM { code = HttpStatusCode.NotFound, token = null, message = message });
                    }
                default:
                    {
                        message = $"Login Failed";
                        return Ok(new JWTokenVM { code = HttpStatusCode.BadRequest, token = null, message = message });
                    }
            }
        }

        [HttpPost("forgot")]
        public ActionResult ForgotPassword(ForgotVM forgotVM)
        {
            var code = 0;
            var message = "";
            var result = accountRepository.ForgotPassword(forgotVM);

            switch (result)
            {
                case 1:
                    {
                        code = StatusCodes.Status200OK;
                        accountRepository.SendMail(forgotVM);
                        message = $"Email sent to {forgotVM.email}, Please check your mailbox / spam folder";
                        break;
                    }
                case 2:
                    {
                        code = StatusCodes.Status404NotFound;
                        message = $"Account not found";
                        break;
                    }
                default:
                    {
                        code = StatusCodes.Status400BadRequest;
                        message = "Forgot Password Failed";
                        break;
                    }
            }

            return Ok(new { code, result, message });
        }

        [HttpPost("change")]
        public ActionResult ChangePassword(ChangePassVM changePassVM)
        {
            var code = 0;
            var message = "";
            var result = accountRepository.ChangePassword(changePassVM);

            switch (result)
            {
                case 1:
                    {
                        code = StatusCodes.Status200OK;
                        message = $"Password Succesfully Changed";
                        break;
                    }
                case 2:
                    {
                        code = StatusCodes.Status401Unauthorized;
                        message = $"OTP is Expired, please request new OTP";
                        break;
                    }
                case 3:
                    {
                        code = StatusCodes.Status401Unauthorized;
                        message = $"OTP is Incorrect";
                        break;
                    }
                case 4:
                    {
                        code = StatusCodes.Status404NotFound;
                        message = $"OTP is already used, please request new OTP";
                        break;
                    }
                case 5:
                    {
                        code = StatusCodes.Status404NotFound;
                        message = $"Account not found, please check the email you entered " +
                                  $"or register if you don't have an account";
                        break;
                    }
                default:
                    {
                        code = StatusCodes.Status400BadRequest;
                        message = "Forgot Password Failed";
                        break;
                    }
            }
            return Ok(new { code, result, message });
        }

        [Authorize]
        [HttpGet]
        [Route("testjwt")]
        public ActionResult TestJWT()
        {
            return Ok("Tes JWT berhasil");
        }
    }
}
