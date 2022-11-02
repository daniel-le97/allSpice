<template>
  <div
    class="modal fade"
    id="exampleModal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-xl modal-dialog-centered modal-scrollable">
      <div class="modal-content">
        <div class="modal-header">
          <button
            class="btn btn-primary"
            @click="deleteRecipe()"
            data-bs-dismiss="modal"
            v-if="owner"
          >
            remove Recipe
          </button>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <RecipeDetail :recipe="recipe" v-if="recipe" />
        </div>
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-primary"
            @click="getIngredientForm"
            data-bs-toggle="modal"
            data-bs-target="#createRecipeModal"
          >
            Ingredients +/-
          </button>
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
          >
            Close
          </button>
          <button type="button" class="btn btn-primary">edit</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../../AppState.js";
import { recipeService } from "../../services/RecipesService.js";
import Pop from "../../utils/Pop.js";
import RecipeDetail from "../RecipesFolder/RecipeDetail.vue";

export default {
  setup() {
    return {
      recipe: computed(() => AppState.activeRecipe),
      owner: computed(
        () => AppState.activeRecipe?.creatorId == AppState.account.id
      ),
      getIngredientForm() {
        AppState.modalForm = 1;
      },
      async deleteRecipe() {
        try {
          let recipeId = this.recipe.id;
          await recipeService.deleteRecipe(recipeId);
        } catch (error) {
          Pop.error(error);
        }
      },
      // ingredients: computed(() => AppState.activeRecipeIngredients)
    };
  },
  components: { RecipeDetail },
};
</script>

<style lang="scss" scoped></style>
