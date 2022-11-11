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

  [HttpGet("{offset}")]
  public async Task<ActionResult<List<Recipe>>> GetAllRecipes( int offset)
  {
    try

    {
      
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
    
       List<Recipe> recipes = _rs.GetAllRecipes(offset);
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{recipeId}/one")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try
    { Recipe recipe = _rs.GetRecipeById(recipeId);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
  {
    try
    { List<Ingredient> ingredients = _rs.GetIngredientsByRecipeId(recipeId);
      return Ok(ingredients);
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
      createdRecipe.Creator = userInfo;
      return Ok(createdRecipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  
    [HttpDelete("{exampleId}")]
    [Authorize]
    public async Task<ActionResult<string>> Deleteexample(int exampleId)
    {
      try
      {
        Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
        _is.DeleteIngredient(exampleId, userInfo.Id);
        return Ok("example deleted");
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
          Recipe recipe =  _rs.UpdateRecipe(recipeData, userInfo.Id, recipeId);
          // recipe.Creator = userInfo;
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
  

    
    

   
}

