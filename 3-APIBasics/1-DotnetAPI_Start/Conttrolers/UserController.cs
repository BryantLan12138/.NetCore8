using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController: ControllerBase 
{
    DataContextDapper _dapper;
    // constructor 
    public UserController(IConfiguration config)
    {
        // Console.WriteLine(config.GetConnectionString("DefaultConnection"));
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime GetDateTime()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("GetUsers/{user}")]
    public string[] GetUsers(string user)
    {
        string[] users = new string[] {"leyi", "pig", "tutu", user};

        return users; 
    }
}