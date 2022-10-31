import { AppState } from "../AppState.js";
import { Ingredient } from "../models/Ingredient.js";
import { api } from "./AxiosService.js";

class IngredientsService {
  async getIngredients(recipeId) {
    const res = await api.get(`/api/recipes/${recipeId}/ingredients`);
    // console.log(res.data);
    AppState.activeRecipeIngredients = res.data.map((i) => new Ingredient(i));
    console.log(AppState.activeRecipeIngredients);
  }
  async createIngredient(ingredientData) {
    const res = await api.post("/api/ingredients", ingredientData);
    AppState.activeRecipeIngredients = [
      ...AppState.activeRecipeIngredients,
      new Ingredient(res.data),
    ];
  }
}
export const ingredientsService = new IngredientsService();
