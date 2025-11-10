using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.HttpContext
{
    public class HttpContextHelper
    {

        public static bool IsUserAuthenticated(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            return user != null && user.Identity != null && user.Identity.IsAuthenticated;
        }

        public static int CurrentUserId(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
            {
                return int.Parse(user.FindFirst("Id")!.Value);
            }
            throw new InvalidOperationException("User is not authenticated.");
        }
    }
}
