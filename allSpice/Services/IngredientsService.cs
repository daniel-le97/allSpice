namespace allSpice.Services;

public class IngredientsService{
private readonly IngredientsRepository _ingredientsRepo;

  public IngredientsService(IngredientsRepository ingredientsRepo)
  {
    _ingredientsRepo = ingredientsRepo;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
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