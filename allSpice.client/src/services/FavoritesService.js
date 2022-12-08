import { AppState } from "../AppState.js";
import { Favorite } from "../models/Favorite.js";
import { api } from "./AxiosService.js";

class FavoritesService {
  async favoriteRecipe(recipe) {
    let newFav = {};
    newFav.recipeId = recipe.id;
    const res = await api.post("api/favorites", newFav);
    // recipe.favoriteId = res.data.id;
    AppState.favorites = [new Favorite(res.data), ...AppState.favorites];
  }
  async deleteFavorite(favoriteId) {
    // console.log(favoriteId);
    const res = await api.delete(`/api/favorites/${favoriteId}`);
    let favIndex = AppState.favorites.findIndex((f) => f.id == favoriteId);
    AppState.favorites.splice(favIndex, 1);
    if (AppState.favNumber == 1) {
      AppState.recipes = AppState.recipes.filter(
        (r) => r.favoriteId != favoriteId
      );
    }
  }
}
export const favoritesService = new FavoritesService();
