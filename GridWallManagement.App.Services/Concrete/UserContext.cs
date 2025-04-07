using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Services.Abstract;
using Microsoft.AspNetCore.Http;

namespace GridWallManagement.App.Services.Concrete
{
    public class UserContext : IUserContext
    {
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            var currentUser = (Users)httpContextAccessor.HttpContext.Items["User"];
            if (currentUser != null)
            {
                this.UserId = currentUser.Id;
            }
            else
            {
                this.UserId = "41fe6a2f-d00d-469c-8ef5-8177fdd06ec9";
            }

            TimeZoneId = "Canada Central Standard Time";

        }
        public string UserId { get; set; }
        public string TimeZoneId { get; set; }
    }
}
