import { AppState } from "../AppState.js";
import { Recipe } from "../models/Recipe.js";
import { api } from "./AxiosService.js";

class RecipeService {
  async getRecipes(offset) {
    const res = await api.get(`/api/recipes/${offset}`);
    // console.log(res.data);
    let thisArray = [];
    thisArray = res.data.map((r) => new Recipe(r));
    AppState.recipes = [...AppState.recipes, ...thisArray];
    // console.log(AppState.recipes.filter((r) => r.favorited == true));
  }
  // async getMyRecipes() {
  //   const res = await api.get("/account/recipes");
  //   // console.log(res.data);
  //   AppState.recipes = res.data.map((r) => new Recipe(r));
  // // }
  // async favoriteRecipe(recipe) {
  //   let newFav = {};
  //   newFav.recipeId = recipe.id;
  //   const res = await api.post("api/favorites", newFav);
  //   recipe.favoriteId = res.data.id;
  //   AppState.favorites.push(recipe);
  // }
  // async deleteFavorite(favoriteId) {
  //   console.log(favoriteId);
  //   const res = await api.delete(`/api/favorites/${favoriteId}`);
  //   let favIndex = AppState.favorites.findIndex(
  //     (f) => f.favoriteId == favoriteId
  //   );
  //   AppState.favorites.splice(favIndex, 1);
  //   if (AppState.favNumber == 1) {
  //     AppState.recipes = AppState.recipes.filter(
  //       (r) => r.favoriteId != favoriteId
  //     );
  //   }
  //   // let index = AppState.recipes.findIndex(r => r.favoriteId == favoriteId)
  //   // AppState.recipes.splice(index,1)
  // }
  async createRecipe(recipeData) {
    const res = await api.post("/api/recipes", recipeData);
    AppState.activeRecipe = new Recipe(res.data);
    // AppState.modalForm = 1
    AppState.recipes = [AppState.activeRecipe, ...AppState.recipes];
    return AppState.activeRecipe;
  }

  async deleteRecipe(recipeId) {
    const res = await api.delete(`/api/recipes/${recipeId}`);
    AppState.recipes = AppState.recipes.filter((r) => r.id != recipeId);
    // AppState.activeRecipe = null
    // console.log(res.data);
  }
}
export const recipeService = new RecipeService();
