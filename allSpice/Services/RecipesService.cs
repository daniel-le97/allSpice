namespace allSpice.Services;


public class RecipesService{
  private readonly RecipesRepository _recipeRepo;
  private readonly FavoritesRepository _favoritesRepo;

  public RecipesService(RecipesRepository recipeRepo, FavoritesRepository favoritesRepo)
  {
    _recipeRepo = recipeRepo;
    _favoritesRepo = favoritesRepo;
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
    return recipe;
  }

  internal void ArchiveRecipe(int recipeId, string accountId)
  {
   Recipe recipe = GetRecipeById(recipeId);
   if (recipe.Archived)
   {
    throw new Exception("recipe is already archived");
   }
   if (recipe.CreatorId != accountId)
   {
    throw new Exception("not your Recipe to archive");
   }
   recipe.Archived = true;
   _recipeRepo.ArchiveRecipe(recipe);
  }

  internal Recipe UpdateRecipe(Recipe recipeData, string accountId)
  {
    if (recipeData.CreatorId != accountId)
    {
      throw new Exception("not your recipe to Update");
    }
    Recipe original = GetRecipeById(recipeData.Id);
   original.Title = recipeData.Title?? original.Title;
   original.Img = recipeData.Img?? original.Img;
   original.Category = recipeData.Category?? original.Category;
   original.Instructions = recipeData.Instructions?? original.Instructions;
    Recipe recipe = _recipeRepo.UpdateRecipe(original);
    return recipe;
  }
  
}