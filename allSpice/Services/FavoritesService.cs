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
    throw new NotImplementedException();
  }
}