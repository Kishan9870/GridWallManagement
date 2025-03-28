using AutoMapper;

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
           
        }
    }
}