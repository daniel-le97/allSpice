namespace allSpice.Repositories;

public class FavoritesRepository : BaseRepository
{
  public FavoritesRepository(IDbConnection db) : base(db)
  {
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
      INSERT INTO ingredients(recipeId)
      VALUES(@RecipeId);
      SELECT LAST_INSERT_ID()
      ;";
        int id = _db.ExecuteScalar<int>(sql, favoriteData);
        favoriteData.Id = id;
        return favoriteData;
  }
}