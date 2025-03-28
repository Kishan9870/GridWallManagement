namespace GridWallManagement.App.Repository.Common
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }

        void BringBack();
        void Delete();
    }
}