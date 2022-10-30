import { AppState } from "../AppState";
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
  async getFavorites() {
    const res = await api.get("/account/favorites");
    AppState.recipes = res.data.map((r) => new Recipe(r));
  }
  async getMyFavorites() {
    const res = await api.get("/account/favorites");
    AppState.favorites = res.data.map((r) => new Recipe(r));
    AppState.recipes.forEach((r) => {
      let fav = AppState.favorites.find((f) => f.id == r.id);
      if (fav) {
        r.favorited = true;
      }
    });

    let hi = AppState.recipes.filter((r) => r.favorited == true);
    console.log(hi);
  }
}

export const accountService = new AccountService();
