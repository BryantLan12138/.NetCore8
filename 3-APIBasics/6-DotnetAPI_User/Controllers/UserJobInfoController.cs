using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobInfoController : ControllerBase
{
    DataContextDapper _dapper;
    IMapper _mapper;
    public UserJobInfoController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
        _mapper = new Mapper(new MapperConfiguration(config => {
            config.CreateMap<UserJobInfoToAddDto, UserJobInfo>();
        }));
    }
    
    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("GetJobInfos")]
    public IEnumerable<UserJobInfo> GetUserJobInfo()
    {
        string sql = @"SELECT [UserId],
                    [JobTitle],
                    [Department] FROM TutorialAppSchema.UserJobInfo";
        
        return _dapper.LoadData<UserJobInfo>(sql);
    }

    [HttpGet("GetSingleJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {
        string sql = @"SELECT [UserId],
                    [JobTitle],
                    [Department] FROM TutorialAppSchema.UserJobInfo
                    WHERE UserId = '" + userId + "'";

        return _dapper.LoadDataSingle<UserJobInfo>(sql);
    }


    // Update
    [HttpPut("EditJobInfos")]
    public IActionResult EditUserJobInfo(UserJobInfo jobInfo)
    {
        string sql = @"UPDATE TutorialAppSchema.UserJobInfo
            SET
                [JobTitle] = '" + jobInfo.JobTitle + @"', 
                [Department] = '" + jobInfo.Department + @"'
            WHERE UserId = '" + jobInfo.UserId + @"'";

        if(_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to edit the JobInfo");
    }

    [HttpPost("CreateJobInfos")]
    public IActionResult AddUserJobInfo(UserJobInfoToAddDto jobInfo)
    {
        string sql = @"INSERT TutorialAppSchema.UserJobInfo (
                        [JobTitle],
                        [Department]
                      ) VALUES ( '" + jobInfo.jobTitle + @"',
                                 '" + jobInfo.department + @"'
                      )";

        if(_dapper.ExecuteSql(sql))
        {
            return Ok();
        }      

        throw new Exception("Failed to add the JobInfo");
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        string sql = @"DELETE FROM TutorialAppSchema.UserJobInfo
                            WHERE UserId = " + userId.ToString();

        if(_dapper.ExecuteSql(sql))
        {
            return Ok();
        }      

        throw new Exception("Failed to Delete the JobInfo");
    }

}