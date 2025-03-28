using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace GridWallManagement.App.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    private readonly IMapper _mapper;

    public ApiControllerBase(IMapper mapper)
    {
        this._mapper = mapper;
    }
    public IMapper Mapper { get { return _mapper; } }
}
