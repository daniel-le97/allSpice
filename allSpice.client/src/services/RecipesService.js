import { AppState } from "../AppState.js";
import { Recipe } from "../models/Recipe.js";
import { api } from "./AxiosService.js";

class RecipeService {
  async getRecipes() {
    const res = await api.get("api/recipes");
    // console.log(res.data);
    AppState.recipes = res.data.map((r) => new Recipe(r));
    console.log(AppState.recipes.filter((r) => r.favorited == true));
  }
  async getMyRecipes() {
    const res = await api.get("/account/recipes");
    // console.log(res.data);
    AppState.recipes = res.data.map((r) => new Recipe(r));
  }
  async favoriteRecipe(recipe) {
    let newFav = {};
    newFav.recipeId = recipe.id;
    const res = await api.post("api/favorites", newFav);
    recipe.favoriteId = res.data.id;
    AppState.favorites.push(recipe);
  }
  async deleteFavorite(favoriteId) {
    console.log(favoriteId);
    const res = await api.delete(`/api/favorites/${favoriteId}`);
    let favIndex = AppState.favorites.findIndex(
      (f) => f.favoriteId == favoriteId
    );
    AppState.favorites.splice(favIndex, 1);
  }
  async createRecipe(recipeData) {
    const res = await api.post("/api/recipes", recipeData);
    AppState.activeRecipe = new Recipe(res.data);
    AppState.modalForm = 1
    AppState.recipes = [AppState.activeRecipe, ...AppState.recipes];
  }
}
export const recipeService = new RecipeService();
