namespace GridWallManagement.App.Database.Common
{
    public class BaseModel : GuidBase
    {
        public bool IsActive { get; set; } = false;

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public void Update(string modifiedBy)
        {
            this.ModifiedBy = modifiedBy;
            this.ModifiedDate = DateTime.UtcNow;
        }

        public void Add(string createdBy)
        {
            this.CreatedBy = createdBy;
            this.CreatedDate = DateTime.UtcNow;
            IsActive = true;
        }

        public void Delete(string modifiedBy)
        {
            Update(modifiedBy);
            IsActive = false;
        }
    }
}
