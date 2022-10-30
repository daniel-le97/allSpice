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
  async favoriteRecipe(recipeId) {
    let recipe = {};
    recipe.recipeId = recipeId;
    const res = await api.post("api/favorites", recipe);
    console.log(res.data);
    // let favorite = AppState.recipes.find(r => r.id == res.data.recipeId)
    // favorite.favorited == true
  }
  async deleteFavorite(recipeId) {
    const res = await api.delete(`/api/favorites/${recipeId}`);
    // console.log(res.data);
  }
}
export const recipeService = new RecipeService();
