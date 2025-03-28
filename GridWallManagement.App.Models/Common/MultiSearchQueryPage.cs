namespace GridWallManagement.App.Models.Common
{
    public abstract class MultiSearchQueryPage
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortProperty { get; set; }
        public bool IsDescending { get; set; }
        public List<SearchQuery> SearchQueries { get; set; }
    }
}
