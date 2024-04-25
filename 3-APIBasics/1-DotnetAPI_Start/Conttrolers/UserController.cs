using DotnetAPI.Data;
using DotnetAPI.Models;
using DotnetAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
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

    [HttpGet("GetUsers/")]
    public IEnumerable<User> GetUsers()
    {
        string sql = @"
        SELECT [UserId],
            [FirstName],
            [LastName],
            [Email],
            [Gender],
            [Active] 
        FROM TutorialAppSchema.Users
        ";
        return _dapper.LoadData<User>(sql);
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        string sql = @"
        SELECT [UserId],
            [FirstName],
            [LastName],
            [Email],
            [Gender],
            [Active] 
        FROM TutorialAppSchema.Users
            WHERE UserId = " + userId.ToString();
        return _dapper.LoadDataSingle<User>(sql);
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDTO  user)
    {
        string sql = @"
            INSERT TutorialAppSchema.Users (
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] 
            ) VALUES (
                '" + user.FirstName + @"',
                '" + user.LastName + @"',
                '" + user.Email + @"',
                '" + user.Gender + @"',
                '" + user.Active + @"'    
            )
        ";
        
        Console.WriteLine(sql);
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to add the select user");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
            UPDATE TutorialAppSchema.Users
                SET 
                    [FirstName] = '" + user.FirstName + @"',
                    [LastName]  = '" + user.LastName + @"',
                    [Email]     = '" + user.Email + @"',
                    [Gender]    = '" + user.Gender + @"',
                    [Active]    = '" + user.Active + @"' 
                WHERE UserId    = " + user.UserId;

        Console.WriteLine(sql);
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to update the select user");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.Users 
                WHERE UserId = '" + userId.ToString() + @"'
        ";
        Console.WriteLine(sql);
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to delete the select user");
    }
}