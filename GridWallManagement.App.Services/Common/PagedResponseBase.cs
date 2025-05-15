namespace GridWallManagement.App.Services.Common
{
    public class PagedResponseBase<TModel> : ResponseBaseModel
    {
        public PagedResponseBase(TModel model, int pageNumber, int pageSize, int totalRecords)
        {
            this.Result = model;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalRecords = totalRecords;

            this.TotalPages = Convert.ToInt32(Math.Ceiling(((double)totalRecords / (double)pageSize)));
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public TModel Result { get; set; }
    }
}
