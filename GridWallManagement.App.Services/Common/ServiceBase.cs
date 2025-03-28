using AutoMapper;

namespace GridWallManagement.App.Service.Common
{
    public class ServiceBase
    {
        public readonly IMapper Mapper;

        public ServiceBase(IMapper _mapper)
        {
            this.Mapper = _mapper;
        }
    }
}
