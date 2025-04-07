using System.ComponentModel.DataAnnotations;

namespace GridWallManagement.App.Database.Common
{
    public abstract class GuidBase
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }

        protected GuidBase()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
