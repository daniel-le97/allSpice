namespace allSpice.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly FavoritesService _fs;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, FavoritesService fs)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _fs = fs;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

   [HttpGet("favorites")]
  public async Task<ActionResult<List<FavRecipe>>> GetFavoritesByAccountId([FromQuery] int offset )
  {
    try
    { 
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<FavRecipe> favorites = _accountService.GetFavoritesByAccountId(userInfo, offset);
      return Ok(favorites);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
   [HttpGet("favorites/all")]
  public async Task<ActionResult<List<Favorite>>> GetAllFavoritesByAccount()
  {
    try
    { 
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Favorite> favorites = _accountService.GetAllFavoritesByAccount(userInfo);
      return Ok(favorites);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
   [HttpGet("recipes")]
  public async Task<ActionResult<List<Recipe>>> GetRecipesByAccountId([FromQuery] int offset )
  {
    try
    { 
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Recipe> recipes = _accountService.GetRecipesByAccountId(userInfo, offset);
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
