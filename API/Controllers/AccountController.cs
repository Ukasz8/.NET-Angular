using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Controllers;

public class AccountController : BaseApiController  
{
    private readonly DataContext _context;

    public AccountController(DataContext context)
    {
        _context = context;
    }
    [HttpPost("register")] 
    public async Task<ActionResult<AppUser>> Register(RegisterDTO registerDTO)
    {
        if(await UserExist(registerDTO.UserName)) return BadRequest("Taki uzytkownik juz jest!!!");
        using var hmac = new HMACSHA512(); 
        var user = new AppUser
        {
            UserName = registerDTO.UserName.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            PasswordSalt = hmac.Key
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
        
    }
        private async Task<bool> UserExist(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}
