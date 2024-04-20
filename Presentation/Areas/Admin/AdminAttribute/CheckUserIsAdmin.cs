using Application.Helpers;
using Application.Services.Interfaces.UserSide;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Areas.Admin.AdminAttribute;


public class CheckUserIsAdmin : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var _service = (IRoleService)context.HttpContext.RequestServices.GetService(typeof(IRoleService))!;

    

        #region Check USer 
      

        var result = await _service.IsUserAdmin((int)context.HttpContext.User.GetUserId());

       
        if (result ==  false)
        {
           
            
            context.HttpContext.Response.Redirect("/");
            return;
           
        }

        #endregion
       await base.OnActionExecutionAsync(context , next);
    }
}
