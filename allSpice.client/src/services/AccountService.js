import { AppState } from "../AppState";
import { Favorite } from "../models/Favorite.js";
import { Recipe } from "../models/Recipe.js";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class AccountService {
  async getAccount() {
    try {
      const res = await api.get("/account");
      AppState.account = res.data;
    } catch (err) {
      logger.error("HAVE YOU STARTED YOUR SERVER YET???", err);
    }
  }
  async getFavoriteRecipes(offset) {
    const res = await api.get(`/account/favorites`, {
      params: {
        offset: offset,
      },
    });
    let favoriteRecipes = res.data.map((r) => new Recipe(r));
    AppState.recipes = [...AppState.recipes, ...favoriteRecipes];
    AppState.offset += favoriteRecipes.length;
  }
  async getMyRecipes(offset) {
    const res = await api.get(`/account/recipes`, {
      params: {
        offset: offset,
      },
    });
    // console.log(res.data);
    let myRecipes = res.data.map((r) => new Recipe(r));
    AppState.recipes = [...AppState.recipes, ...myRecipes];

    AppState.offset += myRecipes.length;
  }
  async getMyFavorites() {
    const res = await api.get("/account/favorites/all");
    // console.log(res.data);
    AppState.favorites = res.data.map((r) => new Favorite(r));
    console.log(AppState.favorites);
    AppState.recipes.map((r) => {
      let fav = AppState.favorites.find((f) => f.recipeId == r.id);
      if (fav) {
        r.favorited = true;
        r.favoriteId = fav.id;
      }
    });
  }
}

export const accountService = new AccountService();
