namespace allSpice.Repositories;

public class FavoritesRepository : BaseRepository
{
  public FavoritesRepository(IDbConnection db) : base(db)
  {
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql = @"INSERT INTO favorites(recipeId, accountId)
      VALUES(@RecipeId, @AccountId);
      SELECT LAST_INSERT_ID()
      ;";
        int id = _db.ExecuteScalar<int>(sql, favoriteData);
        favoriteData.Id = id;
        return favoriteData;
  }

  internal Favorite DeleteFavoriteBefore(Favorite favorite)
  {
    string sql = @"DELETE FROM favorites
      WHERE id = @Id
      LIMIT 1
      ;";
        var rowsAffected = _db.Execute(sql, favorite);
        if (rowsAffected == 0)
        {
            throw new Exception("Unable to Delete Favorite");
        }
        return favorite;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
       string sql = @"SELECT * FROM favorites
        WHERE id = @favoriteId
        ;";
          return _db.Query<Favorite>(sql,new { favoriteId }).First();
  }

  internal Favorite GetFavoriteByAccountAndRecipe(Favorite favoriteData){

     string sql = @"SELECT * FROM favorites
        WHERE recipeId = @recipeId AND accountId = @accountId
        ;";
      Favorite favorites = _db.Query<Favorite>(sql, favoriteData).FirstOrDefault();
          return favorites;
  }

  internal void DeleteFavorite(Favorite favorite)
  {
     string sql = @"DELETE FROM favorites
      WHERE id = @Id
      ;";
        var rowsAffected = _db.Execute(sql, favorite);
        if (rowsAffected == 0)
        {
            throw new Exception("Unable to Delete Favorite");
        }
  }

  internal List<Favorite> GetAllFavoritesByAccount(string accountId)
  {
    string sql = @"
    SELECT * FROM favorites
    WHERE accountId =  @accountId
    ;";
    return _db.Query<Favorite>(sql, new {accountId}).ToList();

  }

  internal List<FavRecipe> GetFavoritesByAccountId(string accountId, int offset)
  {
      string sql = @"SELECT rec.*,
       COUNT(fav.id) AS FavoriteCount,
       fav.id AS FavoriteId,
       rec.id AS recipeId,
       a.*
       FROM favorites fav
       JOIN recipes rec ON rec.id = fav.recipeId
       JOIN accounts a ON a.id = rec.creatorId
       WHERE fav.accountId = @accountId
       GROUP BY fav.id
       limit 12 OFFSET @offset
       ;";
        return _db.Query<FavRecipe, Profile, FavRecipe>(sql, (recipe, profile) =>
        {
            recipe.Creator = profile;
            recipe.AccountId = profile.Id;
            recipe.favorited = true;
            return recipe;
        }, new { accountId, offset }).ToList();
  }
}