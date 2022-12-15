<template>
  <div
    class="modal fade"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-xl modal-dialog-centered modal-scrollable">
      <div class="modal-content">
        <div class="modal-header">
          <i
            class="mdi mdi-heart fs-1"
            @click="deleteFavorite(recipe)"
            v-if="recipe?.favoriteId"
          ></i>
          <i
            v-else
            class="mdi mdi-heart fs-1 text-danger"
            @click="favoriteRecipe()"
          ></i>

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
        <div class="modal-footer d-flex justify-content-between">
          <div>
            <button
              class="btn btn-primary"
              @click="deleteRecipe()"
              data-bs-dismiss="modal"
              v-if="owner"
            >
              remove Recipe
            </button>
          </div>
          <div class="d-flex gap-2">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              Close
            </button>
            <button
              type="button"
              class="btn btn-primary"
              @click="editActive()"
              data-bs-toggle="modal"
              data-bs-target="#createRecipeModal"
              v-if="owner"
            >
              edit
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../../AppState.js";
import { favoritesService } from "../../services/FavoritesService";
import { ingredientsService } from "../../services/IngredientsService";
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
      favorited: computed(() =>
        AppState.recipes.find((r) => r.id == AppState.activeRecipe?.id)
      ),

      async deleteRecipe() {
        try {
          let recipeId = this.recipe.id;
          await recipeService.deleteRecipe(recipeId);
        } catch (error) {
          Pop.error(error);
        }
      },
      async editActive() {
        try {
          AppState.modalForm = 1;
          // await ingredientsService.getIngredients(this.recipe.id)
        } catch (error) {
          Pop.error(error);
        }
      },
      async favoriteRecipe() {
        try {
          // AppState.favNumber = 1
          await favoritesService.favoriteRecipe(AppState.activeRecipe.id);
        } catch (error) {
          Pop.error(error);
        }
      },
      async deleteFavorite() {
        try {
          const yes = await Pop.confirm();
          if (!yes) {
            return;
          }
          // AppState.favNumber = 1
          if (AppState.activeRecipe?.favoriteId) {
            
            await favoritesService.deleteFavorite(AppState.activeRecipe.favoriteId);
          }
          // AppState.activeRecipe.favorited = false;
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
