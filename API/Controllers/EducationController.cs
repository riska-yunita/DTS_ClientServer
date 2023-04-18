using API.Models;
using API.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationController : BaseController<Education, IEducationRepository, int>
{
    private readonly IEducationRepository repository;
    public EducationController(IEducationRepository _repository) : base(_repository)
    {
        repository = _repository;
    }
}