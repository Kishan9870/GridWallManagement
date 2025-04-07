using AutoMapper;
using GridWallManagement.App.Services.Abstract;

namespace GridWallManagement.App.Services.Common
{
    public class ServiceBase
    {
        public readonly IMapper _mapper;
        public readonly IUserContext _userContext;

        public ServiceBase(IMapper _mapper, IUserContext _userContext)
        {
            this._mapper = _mapper;
            this._userContext = _userContext;
        }
    }
}
