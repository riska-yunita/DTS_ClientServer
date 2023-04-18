using API.Context;
using API.Controllers;
using API.Models;
using API.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfilingsController : BaseController<Profiling, IProfilingsRepository, string>
{
    private readonly IProfilingsRepository repository;
    public ProfilingsController (IProfilingsRepository _repository) : base(_repository)
    {
        repository = _repository;
    }
}