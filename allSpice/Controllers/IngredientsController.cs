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
    [HttpGet("{ingredientId}")]
  public ActionResult<Ingredient> GetIngredientById(int ingredientId)
  {
    try
    { Ingredient ingredient = _is.GetIngredientById(ingredientId);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPost]
  public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
    {
    try
     {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      ingredientData.CreatorId = userInfo.Id;
      Ingredient ingredient = _is.CreateIngredient(ingredientData);
      // ingredient.CreatorId = userInfo.Id;
      ingredient.Creator = userInfo;
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

    [HttpDelete("{ingredientId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteIngredient(int ingredientId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            _is.DeleteIngredient(ingredientId, userInfo.Id);
            return Ok("ingredient successfully Deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

  
}
