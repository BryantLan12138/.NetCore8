using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;


namespace DotnetAPI.Controllers;

// EF Controller 
[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    DataContextEF _entityFramework;
    IMapper _mapper;
    public UserEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);

        _mapper = new Mapper(new MapperConfiguration(config => {
            config.CreateMap<UserToAddDto, User>();
        }));
    }


    [HttpGet("GetUsers")]
    // public IEnumerable<User> GetUsers()
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _entityFramework.Users.ToList<User>();
        return users; 
    }

    [HttpGet("GetSingleUser/{userId}")]
    // public IEnumerable<User> GetUsers()
    public User GetSingleUser(int userId)
    {
        User? user = _entityFramework.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();

        if(user != null)
        {
            return user;
        }

        throw new Exception("Failed to get user");
    }
    
    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User? userDb = _entityFramework.Users.Where(u => u.UserId == user.UserId).FirstOrDefault<User>();

        if(user != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            if(_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
        }


        throw new Exception("Failed to Update User");
    }


    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        User? userDb = _mapper.Map<User>(user);

        _entityFramework.Add(userDb);
        if(_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        

        throw new Exception("Failed to Add User");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        User? user = _entityFramework.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();

        if(user != null)
        {
            _entityFramework.Users.Remove(user);
            if(_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
        }

        throw new Exception("Failed to Delete User");
    }
}
