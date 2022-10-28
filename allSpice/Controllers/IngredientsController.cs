namespace allSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  private readonly IngredientsService _is;

  public IngredientsController(Auth0Provider auth0provider, IngredientsService @is)
  {
    _auth0provider = auth0provider;
    _is = @is;
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