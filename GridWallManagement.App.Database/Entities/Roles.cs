using GridWallManagement.App.Database.Common;

namespace GridWallManagement.App.Database.Entities
{
    public class Roles : BaseModel
    {
        public string Name { get; set; }

        public string? Code { get; set; }

        public string DisplayName { get; set; }

    }
}
