using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationPlugin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SWAT_Project_API.Data;
using SWAT_Project_API.Models;

namespace SWAT_Project_API.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public SWATDbContext _dbContext;


        private IConfiguration _configuration;
        private readonly AuthService _auth;


        public UsersController(SWATDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;

            _configuration = configuration;
            _auth = new AuthService(_configuration);
        }


        

        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            var userWithSameEmail = _dbContext.Users.Where(u => u.Email == user.Email).SingleOrDefault();
            if (userWithSameEmail != null)
            {
                return BadRequest("User with same email already exists");
            }
            var userObj = new User
            {
                // user.Password
                Name = user.Name,
                Email = user.Email,
                //makes the password more secure
                Password = SecurePasswordHasherHelper.Hash(user.Password),
                Role = "Users"
            };
            _dbContext.Users.Add(userObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

      
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            //check if email is equal to the user email and store in a variable 
             var useremail = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            //if the result is null return 404 error
             if (useremail == null)
             {
                 return NotFound();
             }
             if (!SecurePasswordHasherHelper.Verify(user.Password, useremail.Password))
             {
                 return Unauthorized();

             }

             var claims = new[]
              {
                   new Claim(JwtRegisteredClaimNames.Email, user.Email),
                   new Claim(ClaimTypes.Email, user.Email),
                   new Claim(ClaimTypes.Role,useremail.Role)
              };

             var token = _auth.GenerateAccessToken(claims);

             return new ObjectResult(new
             {
                 access_token = token.AccessToken,
                 expires_in = token.ExpiresIn,
                 token_type = token.TokenType,
                 creation_Time = token.ValidFrom,
                 expiration_Time = token.ValidTo,
                 user_id = useremail.Id,
             });

        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllUsers ()
        {
            var all_users = _dbContext.Users;
            return Ok(all_users);
        }
        
    }

}