using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.MVC.Services
{
    public class CurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public string VisitorId
        {
            get
            {
               return httpContextAccessor.HttpContext.Request.Cookies["visitorId"];

                
            }
            set
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddMonths(1);
                cookieOptions.IsEssential = true;
                httpContextAccessor.HttpContext.Response.Cookies.Append("visitorId", value, cookieOptions);
                
            }
        }
    }
}
