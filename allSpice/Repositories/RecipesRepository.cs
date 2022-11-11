namespace allSpice.Repositories;

public class RecipesRepository : BaseRepository
{
  public RecipesRepository(IDbConnection db) : base(db){}

  internal Recipe CreateRecipe(Recipe newRecipe)
  {
       string sql = @"INSERT INTO recipes(title, img, instructions, category, creatorId)
      VALUES(@Title, @Img, @Instructions, @Category, @CreatorId);
      SELECT LAST_INSERT_ID()
      ;";
        int recipeId = _db.ExecuteScalar<int>(sql, newRecipe);
        // newRecipe.Id = recipeId;
        return GetRecipeById(recipeId);
  }

  internal List<Recipe> GetAllRecipes(int offset)
  {
    string sql = @"SELECT rec.*,
    COUNT(favs.id) AS FavoriteCount,
    COUNT(ing.id) AS IngredientCount,
    a.*
    FROM recipes rec
    JOIN accounts a on a.id = rec.creatorId
    LEFT JOIN favorites favs on favs.recipeId = rec.id
    LEFT JOIN ingredients ing on ing.recipeId = rec.id
    GROUP BY rec.id
    LIMIT 12 OFFSET @offset
   ;";
     return _db.Query<Recipe, Profile, Recipe>(sql ,(recipe, profile) =>
        {
            recipe.Creator = profile;
            return recipe;
        }, new {offset}).ToList();
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"SELECT rec.*,
    COUNT(favs.id) AS FavoriteCount,
    a.*
    FROM recipes rec
    JOIN accounts a ON a.id = rec.creatorId
    LEFT JOIN favorites favs on favs.recipeId = rec.id
    WHERE rec.id = @recipeId
    ;";
      return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
        {
            recipe.Creator = profile;
            return recipe;
        }, new { recipeId }).First();
  }

  internal List<Recipe> GetRecipesByAccountId(string id, int offset)
  {
   string sql = @"SELECT rec.*,
    COUNT(favs.id) AS FavoriteCount,
    COUNT(ing.id) AS IngredientCount,
    a.*
    FROM recipes rec
    JOIN accounts a ON a.id = rec.creatorId
    LEFT JOIN favorites favs on favs.recipeId = rec.id
    LEFT JOIN ingredients ing on ing.recipeId = rec.id
    WHERE rec.creatorId = @id
    GROUP BY rec.id
    LIMIT 12 OFFSET @offset
   ;";
      return _db.Query<Recipe, Profile, Recipe>(sql ,(recipe, profile) =>
        {
            recipe.Creator = profile;
            return recipe;
        }, new {id, offset}).ToList();
  }

  internal void DeleteRecipe(Recipe recipe)
  {
      string sql = @"DELETE FROM recipes
      WHERE id = @Id
      LIMIT 1
      ;";
        var rowsAffected = _db.Execute(sql, recipe);
        if (rowsAffected == 0)
        {
            throw new Exception("Unable to update recipe");
        }
  }

  internal Recipe UpdateRecipe(Recipe recipe)
  {
      string sql = @"UPDATE recipes SET
                title = @title,
                img = @img,
                instructions = @instructions,
                category = @category
            WHERE id = @Id;";
            int recipeRow = _db.Execute(sql, recipe);
            if (recipeRow == 0)
            {
              throw new Exception("could not update recipe properly");
            }
    return recipe;
  }


  
}