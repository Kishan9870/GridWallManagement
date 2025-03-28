using System.ComponentModel.DataAnnotations;
using System.Net;

namespace GridWallManagement.App.Services.Common
{
    public class ResponseBaseModel
    {
        public static bool IsDebuggingMode { get; set; }

        public bool IsSuccess
         => (ResponseStatusCode == HttpStatusCode.OK
             || ResponseStatusCode == HttpStatusCode.Created
             || ResponseStatusCode == HttpStatusCode.Accepted);

        public string Message { get; set; }

        public IList<string> Errors { get; set; }

        public HttpStatusCode ResponseStatusCode { get; set; } = HttpStatusCode.OK;

        public void AddExceptionLog(Exception ex)
        {
            if (ResponseStatusCode == HttpStatusCode.OK)
                ResponseStatusCode = HttpStatusCode.BadRequest;

            // Don't show exceptions in production.
            if (ex is ValidationException
                || ex is ArgumentNullException
                || ex is InvalidOperationException
                || ex is Exception)
            {
                if (Errors == null) // Initialize if needed
                    Errors = new List<string>();

                Errors.Add(ex.Message);
                if (ex.InnerException != null)
                    AddExceptionLog(ex.InnerException);
            }
        }
    }
}
