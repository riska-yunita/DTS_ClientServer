using API.Models;
using API.Repository.Contracts;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : BaseController<Account, IAccountRepository, string>
{
    private readonly IAccountRepository repository;


    public AccountController(IAccountRepository _repository) : base(_repository)
    {
        repository = _repository;
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login(LoginVM loginVM)
    {
        
        var login = await repository.LoginAsync(loginVM);
        if (login)
        {
            return Ok("Login Sukses");
                
        }
        else
        {
            return NotFound("Data Login Tidak Ditemukan");
        } 
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register(RegisterVM registerVM)
    {
        try
        {
            await repository.RegisterAsync(registerVM);
            return Ok("Register Berhasil");
        }
        catch
        {
            return BadRequest("Register gagal");
        }
    }
    
}
