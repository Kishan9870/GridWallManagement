namespace GridWallManagement.App.Models.Common
{
    public abstract class Page : SearchQuery
    {
        public Page()
        {
            this.PageNumber = 0;
            this.PageSize = 10;
            //this.IsDescending = false;
            //this.SortProperty = "CreatedDate";
        }

        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
        public bool IsDescending { get; set; }
#nullable enable
        public string? SortProperty { get; set; }
    }
}
