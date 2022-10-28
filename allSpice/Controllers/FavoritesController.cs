namespace allSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  private readonly FavoritesService _fs;

  public FavoritesController(Auth0Provider auth0provider, FavoritesService fs)
  {
    _auth0provider = auth0provider;
    _fs = fs;
  }

  [HttpGet]
  public ActionResult<List<string>> Get()
  {
    try
    {
      return Ok(new List<string>() { "Value 1", "Value 2" });
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpPost]
  public ActionResult<List<string>> Create([FromBody] string value)
  {
    try
    {
      return Ok(value);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}