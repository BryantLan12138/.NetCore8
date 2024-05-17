using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using DotnetAPI.Dtos;
using DotnetAPI.Data;
using System.Security.Cryptography;
using System.Text;

namespace DotnetAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly DataContextDapper _dapper;
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _config = config;
        }

        [HttpPost("Register")]
        public IActionResult Register(UserForRegistrationDto userForRegistration)
        {
            if(userForRegistration.Password != userForRegistration.PasswordConfirm)
            {
                throw new Exception("Passwords do not match!");
            }
            string sqlCheckUserExists = "SELECT Email FROM TutorialAppSchema.Auth WHERE Email = '" + 
                userForRegistration.Email + "'";
            
            IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckUserExists);

            // if email has not been registered 
            if (existingUsers.Count() == 0)
            {
                byte[] passwordSalt = new byte[128 / 8];

                // assign random value to salt 
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetNonZeroBytes(passwordSalt);
                }

                string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey").Value + 
                    Convert.ToBase64String(passwordSalt);

                byte[] passwordHash = KeyDerivation.Pbkdf2(
                    password: userForRegistration.Password,
                    salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10,
                    numBytesRequested: 256 / 8
                );

                // string sqlAddAuth = @"
                //     INSERT INTO TutorialAppSchema.Auth(
                //         [Email], [PasswordHash], [PasswordSalt]) VALUES ('" + 
                //         userForRegistration.Email + 
                //         "', @PasswordHash, @PasswordSalt)";

                string sqlAddAuth = @"
                        INSERT INTO TutorialAppSchema.Auth  ([Email],
                        [PasswordHash],
                        [PasswordSalt]) VALUES ('" + userForRegistration.Email +
                        "', @PasswordHash, @PasswordSalt)";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                SqlParameter passwordSaltParameter = new SqlParameter("@PasswordSalt", SqlDbType.VarBinary);
                passwordSaltParameter.Value = passwordSalt;

                SqlParameter passwordHashParameter = new SqlParameter("@PasswordHash", SqlDbType.VarBinary);
                passwordHashParameter.Value = passwordHash;

                sqlParameters.Add(passwordSaltParameter);
                sqlParameters.Add(passwordHashParameter);

                if(_dapper.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
                {
                    return Ok();
                }
                throw new Exception ("Failed to register user.");
            }

            throw new Exception("User with this email already exists!");

        }

        [HttpPost("Login")]
        public IActionResult Login(UserForLoginDto userForLogin)
        {
            return Ok();
        }
    }
}