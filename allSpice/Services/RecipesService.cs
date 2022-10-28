namespace allSpice.Services;


public class RecipesService{
  private readonly RecipesRepository _recipeRepo;
  private readonly FavoritesRepository _favoritesRepo;
  private readonly IngredientsRepository _ingredientsRepo;

  public RecipesService(RecipesRepository recipeRepo, FavoritesRepository favoritesRepo, IngredientsRepository ingredientsRepo)
  {
    _recipeRepo = recipeRepo;
    _favoritesRepo = favoritesRepo;
    _ingredientsRepo = ingredientsRepo;
  }

  internal Recipe CreateRecipe(Recipe newRecipe){
    return _recipeRepo.CreateRecipe(newRecipe);
  }

  internal List<Recipe> GetAllRecipes()
  {
    return _recipeRepo.GetAllRecipes();
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _recipeRepo.GetRecipeById(recipeId);
    if (recipe == null)
    {
      throw new Exception("recipe does not exist");
    }
    if (recipe.Id == 0)
    {
      throw new Exception("bad id");
    }
    return recipe;
  }

  internal void DeleteRecipe(int recipeId, string accountId)
  {
   Recipe recipe = GetRecipeById(recipeId);
   if (recipe.CreatorId != accountId)
   {
    throw new Exception("not your Recipe to archive");
   }
   
   _recipeRepo.DeleteRecipe(recipe);
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    return _ingredientsRepo.GetIngredientsByRecipeId(recipeId);
  }

  internal Recipe UpdateRecipe(Recipe recipeData, string accountId, int recipeId)
  {
   
    Recipe original = GetRecipeById(recipeId);
    if (original.CreatorId != accountId)
    {
      throw new Exception("not your recipe to Update");
    }
   original.Title = recipeData.Title?? original.Title;
   original.Img = recipeData.Img?? original.Img;
   original.Category = recipeData.Category?? original.Category;
   original.Instructions = recipeData.Instructions?? original.Instructions;
    Recipe recipe = _recipeRepo.UpdateRecipe(original);
    return recipe;
  }
  
}