namespace allSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  private readonly IngredientsService _is;
  private readonly RecipesService _rs;

  public RecipesController(Auth0Provider auth0provider, RecipesService rs, IngredientsService @is)
  {
    _auth0provider = auth0provider;
    _rs = rs;
    _is = @is;
  }

  [HttpGet]
  public ActionResult<List<Recipe>> GetAllRecipes()
  {
    try
    { List<Recipe> recipe = _rs.GetAllRecipes();
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{recipeId}")]
  public ActionResult<List<Recipe>> GetRecipeById(int id)
  {
    try
    { Recipe recipe = _rs.GetRecipeById(id);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe newRecipe)
  {
    try
    { 
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      newRecipe.CreatorId = userInfo.Id;
      Recipe createdRecipe = _rs.CreateRecipe(newRecipe);
      return Ok(createdRecipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

   [HttpDelete("{recipeId}")]
    [Authorize]
    public async Task<ActionResult<string>> ArchiveRecipe(int recipeId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            _rs.ArchiveRecipe(recipeId, userInfo.Id);
            return Ok("Recipe successfully archived");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
   [HttpPut("{recipeId}")]
    [Authorize]
    public async Task<ActionResult<Recipe>> UpdateRecipe([FromBody] Recipe recipeData ,int recipeId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
          Recipe recipe =  _rs.UpdateRecipe(recipeData, userInfo.Id);
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}

