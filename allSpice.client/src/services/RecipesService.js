import { AppState } from "../AppState.js";
import { Ingredient } from "../models/Ingredient.js";
import { Recipe } from "../models/Recipe.js";
import { api } from "./AxiosService.js";

class RecipeService {
  async getRecipes(offset) {
    const res = await api.get(`/api/recipes/${offset}`);
    // console.log(res.data);
    // let thisArray = [];
    let thisArray = res.data.map((r) => new Recipe(r));
    AppState.recipes = [...AppState.recipes, ...thisArray];

    AppState.offset += thisArray.length;
    // console.log(AppState.recipes.filter((r) => r.favorited == true));
  }
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
  async editRecipe(editableData) {
    editableData.recipeId = AppState.activeRecipe.id;
    // console.log(editableData);
    const res = await api.put(
      `api/recipes/${editableData.recipeId}`,
      editableData
    );
    // console.log(res.data);
    let recipe = new Recipe(res.data);
    AppState.activeRecipe = recipe;
    let index = AppState.recipes.findIndex(
      (r) => r.id == editableData.recipeId
    );
    AppState.recipes.splice(index, 1, recipe);
  }
}
export const recipeService = new RecipeService();
