using App.Domain.Core.Home.Enum;
using App.Infra.Data.Db.SqlServer.Ef.Home.DataDBContaxt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DwellMVC.Areas.Admin.CustomAuthorize
{
    public class CustomAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly RoleEnum _requiredRole;

        public CustomAuthorizeAttribute(RoleEnum requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var dbContext = context.HttpContext.RequestServices.GetService(typeof(AppDbContext)) as AppDbContext;
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (dbContext == null || userId == null)
            {
                context.Result = new RedirectToActionResult("Login", "Admin", null);
                return;
            }

            var user = await dbContext.Users.FindAsync(int.Parse(userId));

            if (user == null || user.RoleType != _requiredRole)
            {
                context.Result = new RedirectToActionResult("Login", "Admin", null);
            }
        }
    }
}
