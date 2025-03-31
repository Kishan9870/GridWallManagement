using AutoMapper;
using GridWallManagement.App.Database.Entities;

namespace GridWallManagement.App.Models
{
    public class EntityToDTOMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "EntityToDTOMappingProfile";
            }
        }

        public EntityToDTOMappingProfile()
        {
            ConfigureMappings();
        }

        private void ConfigureMappings()
        {
            CreateMap<UserLicense, UserLicenseResponse>().ReverseMap();
            CreateMap<UserLicenseRenewal, UserLicenseRenewalResponse>().ReverseMap();
        }
    }
}