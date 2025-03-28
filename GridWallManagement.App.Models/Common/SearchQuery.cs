namespace GridWallManagement.App.Models.Common
{
    public class SearchQuery
    {        
        public SearchingOptions SearchOption { get; set; }
        #nullable enable
        public string? SearchProperty { get; set; }
        public string? SearchValue { get; set; }
    }
    public enum SearchingOptions
    {
        Equal = 0,
        Contains = 1,
        GreaterThan = 2,
        GreaterThanOrEqual = 3,
        LessThan = 4,
        LessThanOrEqual = 5
    }
}
