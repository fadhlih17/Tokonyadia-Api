using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tokonyadia_Api.Controllers;

[ApiController]
[Authorize]
public abstract class BaseController : ControllerBase
{
}