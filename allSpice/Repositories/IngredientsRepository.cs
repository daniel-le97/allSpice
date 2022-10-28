namespace allSpice.Repositories;

public class IngredientsRepository : BaseRepository
{
  public IngredientsRepository(IDbConnection db) : base(db)
  {
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
      string sql = @"
      INSERT INTO ingredients(quantity, name, recipeId, creatorId)
      VALUES(@Quantity, @Name, @RecipeId, @CreatorId);
      SELECT LAST_INSERT_ID()
      ;";
        int id = _db.ExecuteScalar<int>(sql, ingredientData);
        ingredientData.Id = id;
        return ingredientData;
  }

  internal Ingredient GetIngredientsById(int ingredientId)
  {
        string sql = @"SELECT ing.*,
        a.*
        FROM ingredients ing
        JOIN accounts a ON a.id = ing.creatorId
        WHERE ing.id = @ingredientId
        ;";
          return _db.Query<Ingredient, Profile, Ingredient>(sql, (ingredient, profile) =>
        {
            ingredient.Creator = profile;
            return ingredient;
        }, new { ingredientId }).First();
    
  }

  internal void DeleteIngredient(Ingredient ingredient)
  {
     string sql = @"DELETE FROM ingredients
      WHERE id = @Id
      LIMIT 1
      ;";
        var rowsAffected = _db.Execute(sql, ingredient);
        if (rowsAffected == 0)
        {
            throw new Exception("Unable to update recipe");
        }
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {

            string sql = @"SELECT ing.*,
        a.*
        FROM ingredients ing
        JOIN accounts a ON a.id = ing.creatorId
        WHERE ing.recipeId = @recipeId
        ;";
        return _db.Query<Ingredient, Profile, Ingredient>(sql, (ingredient, profile) =>
        {
            ingredient.Creator = profile;
            return ingredient;
        }, new { recipeId }).ToList();
  }
}