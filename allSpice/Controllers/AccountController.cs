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
  public async Task<ActionResult<List<FavRecipe>>> GetFavoritesByAccountId()
  {
    try
    { 
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<FavRecipe> favorites = _accountService.GetFavoritesByAccountId(userInfo);
      return Ok(favorites);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
}
}
