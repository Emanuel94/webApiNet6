using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Asp.Application.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IRepositoryWrapper _repository;


        public AuthController(IConfiguration configuration, IRepositoryWrapper repository, IMapper mapper, ILoggerManager logger)
        {
            _mapper = mapper;
            _configuration = configuration;
            _repository = repository;   
            _logger = logger;
        }

        [HttpGet]   
        public  IActionResult getAllUser()
        {
            try
            {
                var users = _repository.User.GetAllUsers();
                _logger.LogInfo("I get alls the user in DB.");

                var userResult = _mapper.Map<IEnumerable<User>>(users);
                return Ok(userResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something was wrong in server, Error:{ex.Message}");
                return StatusCode(500, "Internal Error");
            }
             
        }
        [HttpGet("{:id}" ,Name ="userById")]
        public IActionResult GetUserById(Guid Id)
        {
            try
            {
                var user = _repository.User.GetUserById(Id);
                if (user is null)
                {
                    _logger.LogInfo($"It can't found user with this Id: {Id}");
                    return StatusCode(401, "It does not found User");
                }
                else
                {
                    var userResult = _mapper.Map<UserDto>(user);
                    return Ok(userResult);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Error: {ex.Message}");
                return StatusCode(500, "Internal Error");
            }
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser([FromBody] UserForCreationDto  userRequest)
        {
            try
            {
                if (userRequest is null)
                {
                    _logger.LogError("Object for user is null");
                    return BadRequest("");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("User not found");
                }
                CreatePasswordhash(userRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

                userRequest.UserName = userRequest.UserName;
                userRequest.PasswordHash = passwordHash;
                userRequest.PasswordSalt = passwordSalt;


                var userEntity = _mapper.Map<User>(userRequest);
                _repository.User.CreateUser(userEntity);
                _repository.Save();

                return Ok(userEntity);
            }
            catch (Exception ex)
            {

                _logger.LogError($"It throw internal error: {ex.Message}");
                return BadRequest(ex.Message);
            }
              
        }

        [HttpPut]
        public IActionResult UpdateUser(Guid Id, [FromBody] UserUpdateDto userUpdate)
        {
            try
            {
                if (userUpdate is null)
                {
                    _logger.LogError("User doesn't  exits in the client request.");
                    return BadRequest("User invalid in the request");

                }

                var User = _repository.User.GetUserById(Id);
                if (User is null)
                {
                    _logger.LogError("User doesn't  exits in DB for Updating it.");
                    return StatusCode(404, "User does't find in DB");

                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid objetc from the request");
                    return BadRequest("Invalid objetc from the request");
                }

                _mapper.Map(userUpdate, User);

                _repository.User.UpdateUser(User);
                _repository.Save();

                return Ok(User);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal Error in serve {ex.Message}");
                return StatusCode(500,"Internal error in the server.");
 
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto userRequest)
        {
            if (user.UserName != userRequest.UserName)
            {
                return BadRequest("User not found");
            }
            if (!VerifyPasswordHash(userRequest.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong Password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

        private void CreatePasswordhash( string password, out byte[] passwordHash, out byte[] passworSalt )
        {
            using(var hmac = new HMACSHA512())
            {
                passworSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordhash, byte[] passwordSalt )
        {
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordhash);
            }
        }
    }
}
