namespace allSpice.Services;

public class FavoritesService{
  private readonly FavoritesRepository _favoritesRepo;

  public FavoritesService(FavoritesRepository favoritesRepo)
  {
    _favoritesRepo = favoritesRepo;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    return _favoritesRepo.CreateFavorite(favoriteData);
  }

  internal void DeleteFavorite(int favoriteId, string accountId)
  {
    Favorite favorite = GetFavoriteById(favoriteId);
    if (favorite.AccountId != accountId)
    {
      throw new Exception("not your favorite to delete");
    }
    _favoritesRepo.DeleteFavorite(favorite);
  }
    internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _favoritesRepo.GetFavoriteById(favoriteId);
    if (favorite == null)
    {
      throw new Exception("favorite does not exist");
    }
    if (favorite.Id == 0)
    {
      throw new Exception("bad id");
    }
    return favorite;
  }

}