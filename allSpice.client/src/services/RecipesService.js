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
    newFav.recipeId = recipe.id
    const res = await api.post("api/favorites", newFav);
    console.log(res.data);
    let favorite = AppState.favorites.push(recipe)
    // favorite.favorited = true
    // console.log(favorite);
  }
  async deleteFavorite(recipeId) {
    const res = await api.delete(`/api/favorites/${recipeId}`);
    console.log(res.data);
    AppState.favorites.filter((f) => f.id != recipeId);
    //  let favorite = AppState.recipes.find((r) => r.id == recipeId);
    //  favorite.favorited = false;
    //  console.log(favorite);
  }
}
export const recipeService = new RecipeService();
