import { api } from "./AxiosService.js";

class IngredientsService {
  async getIngredients(recipeId) {
    const res = await api.get(`/api/recipes/${recipeId}/ingredients`);
    console.log(res.data);
  }
}
export const ingredientsService = new IngredientsService();
