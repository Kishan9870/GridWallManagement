namespace GridWallManagement.App.Models.Common
{
    public abstract class Page : SearchQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool IsDescending { get; set; }
        #nullable enable
        public string? SortProperty { get; set; }
    }
}
