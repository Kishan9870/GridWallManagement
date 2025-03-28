namespace GridWallManagement.App.Services.Common
{
    public class ResponseBase<TModel> : ResponseBaseModel
    {
        public ResponseBase(TModel model)
        {
            Result = model;
        }

        public TModel Result { get; set; }
    }
}
