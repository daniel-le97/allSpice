import { AppState } from "../AppState.js";
import { Ingredient } from "../models/Ingredient.js";
import { api } from "./AxiosService.js";

class IngredientsService {
  async getIngredients(recipeId) {
    const res = await api.get(`/api/recipes/${recipeId}/ingredients`);
    // console.log(res.data);
    AppState.activeRecipeIngredients = res.data.map((i) => new Ingredient(i));
    // console.log(AppState.activeRecipeIngredients);
  }
  async createIngredient(ingredientData) {
    const res = await api.post("/api/ingredients", ingredientData);
    AppState.activeRecipeIngredients = [
      ...AppState.activeRecipeIngredients,
      new Ingredient(res.data),
    ];
    // console.log("another ingredient added");
  }
  async deleteIngredient(index, ingID) {
    const res = await api.delete("api/ingredients/" + ingID);
    // console.log(res.data);
    AppState.activeRecipeIngredients.splice(index,1)
  }
}
export const ingredientsService = new IngredientsService();
