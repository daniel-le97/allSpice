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
  public async Task<ActionResult<Favorite>> CreateFavorite([FromBody] Favorite favoriteData)
    {
    try
     {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      favoriteData.AccountId = userInfo.Id;
      Favorite favorite = _fs.CreateFavorite(favoriteData);
      favorite.Creator = userInfo;
      return Ok(favorite);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

    [HttpDelete("{favoriteId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteFavorite(int favoriteId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            _fs.DeleteFavorite(favoriteId, userInfo.Id);
            return Ok("favorite successfully Deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

       [HttpGet("{favoriteId}")]
  public ActionResult<Ingredient> GetFavoriteById(int favoriteId)
  {
    try
    { Favorite favorite = _fs.GetFavoriteById(favoriteId);
      return Ok(favorite);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}