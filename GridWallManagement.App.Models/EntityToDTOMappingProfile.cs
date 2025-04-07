using AutoMapper;
using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Models.Response.Role;
using GridWallManagement.App.Models.Response.User;
using GridWallManagement.App.Models.Response.UserLicense;

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

            #region User
            CreateMap<Users, UserResponse>().ReverseMap();
            #endregion

            #region Role
            CreateMap<Roles, RolesResponse>().ReverseMap();
            #endregion
        }
    }
}