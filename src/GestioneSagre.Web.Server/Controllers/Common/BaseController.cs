using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.Web.Server.Controllers.Common;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class BaseController : ControllerBase
{

}