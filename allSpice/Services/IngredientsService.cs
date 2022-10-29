namespace allSpice.Services;

public class IngredientsService{
private readonly IngredientsRepository _ingredientsRepo;
private readonly RecipesRepository _recipeRepo;

  public IngredientsService(IngredientsRepository ingredientsRepo, RecipesRepository recipeRepo)
  {
    _ingredientsRepo = ingredientsRepo;
    _recipeRepo = recipeRepo;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  { 
    Recipe recipe = _recipeRepo.GetRecipeById(ingredientData.RecipeId);
    if (ingredientData.CreatorId != recipe.CreatorId)
    {
      throw new Exception("this isn't your recipe too add ingredients too");
    }
    return _ingredientsRepo.CreateIngredient(ingredientData);
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _ingredientsRepo.GetIngredientsById(ingredientId);
    if (ingredient == null)
    {
      throw new Exception("ingredient does not exist");
    }
    return ingredient;
  }

  internal void DeleteIngredient(int ingredientId, string accountId)
  {
    Ingredient ingredient= GetIngredientById(ingredientId);
    if (ingredient.CreatorId != accountId)
    {
      throw new Exception("not your ingredient to delete");
    }
    _ingredientsRepo.DeleteIngredient(ingredient);
  }
}