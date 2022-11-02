namespace allSpice.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;
  private readonly FavoritesRepository _favRepo;
  private readonly RecipesRepository _recRepo;

  public AccountService(AccountsRepository repo, FavoritesRepository favRepo, RecipesRepository recRepo)
  {
    _repo = repo;
    _favRepo = favRepo;
    _recRepo = recRepo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account editData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
    return _repo.Edit(original);
  }

  internal List<FavRecipe> GetFavoritesByAccountId(Account userInfo)
  {
    return _favRepo.GetFavoritesByAccountId(userInfo.Id);
  }

  internal List<Recipe> GetRecipesByAccountId(Account userInfo)
  {
    List<Recipe> recipes = _recRepo.GetRecipesByAccountId(userInfo.Id);
    // foreach (var recipe in recipes)
    // {
    //   recipe.Creator = userInfo;
    // }
    return  recipes;
  }

  internal List<Favorite> GetAllFavoritesByAccount(Account userInfo)
  {
    return _favRepo.GetAllFavoritesByAccount(userInfo.Id);
  }
}
