<template>
  <form @submit.prevent="handleIngredientSubmit()">
    <div class="row">
      <div class="col-6">
        <div class="mb-3">
          <label for="ingredientName" class="form-label">ingredient</label>
          <input
            type="text"
            class="form-control"
            id="ingredientName"
            v-model="editable.name"
          />
        </div>
        <div class="mb-3">
          <label for="ingredientQuantity" class="form-label">quantity</label>
          <input
            type="text"
            class="form-control"
            id="ingredientQuantity"
            v-model="editable.quantity"
          />
        </div>
      </div>
      <div class="col-6">
        <div class="p-3">
          <RecipeCard :recipe="recipe" />
        </div>
      </div>
    </div>

    <div class="d-flex justify-content-end gap-2">
      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
        Close
      </button>
      <button
        type="submit"
        class="btn btn-primary"
      >
        Add ingredient
      </button>
    </div>
  </form>
</template>

<script>
import { computed } from "@vue/reactivity";
import { ref } from "vue";
import { AppState } from "../../AppState.js";
import { ingredientsService } from "../../services/IngredientsService.js";
import Pop from "../../utils/Pop.js";
import RecipeCard from "../RecipesFolder/RecipeCard.vue";

export default {
  setup() {
    const editable = ref({});
    return {
      editable,
      recipe: computed(() => AppState.activeRecipe),
      ingredients: computed(() => AppState.activeRecipeIngredients),
      async handleIngredientSubmit() {
        try {
         editable.value.recipeId = this.recipe.id
        //  console.log(editable.value);
          await ingredientsService.createIngredient(editable.value);
        } catch (error) {
          Pop.error(error);
        }
      },
    };
  },
  components: { RecipeCard },
};
</script>

<style lang="scss" scoped>
// .img-card{
//   height: 15%;
//   width: 20vw;
// }
</style>
