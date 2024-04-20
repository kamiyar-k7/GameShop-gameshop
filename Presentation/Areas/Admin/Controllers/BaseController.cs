
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers;

[Authorize]
[Area("Admin")]
[AdminAttribute.CheckUserIsAdmin]
public class BaseController : Controller
{
 



}
