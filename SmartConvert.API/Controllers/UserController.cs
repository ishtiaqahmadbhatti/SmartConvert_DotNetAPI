using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SmartConvert.API.Data;
using SmartConvert.API.Models;
using System;

namespace SmartConvert.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("InsertRecord")]
        public IActionResult InsertRecord([FromBody] UserModel userModel)
        {
            var parameters = new[]
{
    new SqlParameter("@FullName", userModel.FullName),
    new SqlParameter("@Email", userModel.Email),
    new SqlParameter("@Password", userModel.Password)
};
            _context.ExecuteStoredProcedure("pr_UserRecord_Insert", parameters);
            return Ok();
        }
    }
}
