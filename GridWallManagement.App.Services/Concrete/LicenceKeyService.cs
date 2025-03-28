using AutoMapper;
using GridWallManagement.App.Service.Common;
using GridWallManagement.App.Services.Abstract;
using Microsoft.Extensions.Configuration;

namespace GridWallManagement.App.Services.Concrete
{
    public class LicenceKeyService : ServiceBase, ILicenceKeyService
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public LicenceKeyService(IConfiguration config,
                                 IMapper mapper) : base(mapper)
        {
            _config = config;
            _mapper = mapper;
        }

    }
}
