using App.Application.Services;
using App.Domain.Constants;
using System.Security.Claims;

namespace App.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string UserId
        {
            get 
            { 
                return _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier); 
            }
        }

        public string UserName
        {
            get
            {
                return _contextAccessor.HttpContext?.User?.FindFirstValue(Constant.UserName);
            }
        }

        public string CustomerId
        {
            get
            {
                return _contextAccessor.HttpContext?.User?.FindFirstValue(Constant.CustomerId);
            }
        }

        public string UserType
        {
            get
            {
                return _contextAccessor.HttpContext?.User?.FindFirstValue(Constant.UserType);
            }
        }
    }
}
