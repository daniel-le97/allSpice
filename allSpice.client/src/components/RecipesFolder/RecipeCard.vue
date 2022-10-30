<template>
  <div class="card text-bg-dark">
    <img :src="recipe.img" class="card-img img-fluid imgSize" alt="..." />
    <div class="card-img-overlay p-1 px-2">
      <div class="d-flex justify-content-end">
        <i
          class="mdi mdi-heart text-danger"
          @click="deleteFavorite()"
          v-if="favorited"
        ></i>
        <i class="mdi mdi-heart" @click="favoriteRecipe" v-else></i>
      </div>
      <div
        @click="makeActive(recipe)"
        data-bs-toggle="modal"
        data-bs-target="#exampleModal"
      >
        <h5 class="card-title">{{ recipe.title }}</h5>
        <p class="card-text">{{ recipe.instructions }}</p>
        <p class="card-text">
          <small>{{ recipe.category }}</small>
        </p>
      </div>
    </div>
  </div>
  <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
  Launch demo modal
</button> -->
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../../AppState.js";
import { Recipe } from "../../models/Recipe.js";
import { recipeService } from "../../services/RecipesService.js";
import Pop from "../../utils/Pop.js";

export default {
  props: {
    recipe: { type: Recipe, required: true },
  },
  setup(props) {
    return {
      favorited: computed(() =>
        AppState.favorites.find((f) => f.id == props.recipe.id)
      ),
      makeActive(recipeData) {
        if (AppState.activeRecipe != recipeData) {
          AppState.activeRecipe = recipeData;
        }
        // console.log(AppState.activeRecipe);
      },
      async favoriteRecipe() {
        try {
          props.favorited = true;
          await recipeService.favoriteRecipe(props.recipe);
        } catch (error) {
          Pop.error(error);
        }
      },
      async deleteFavorite() {
        try {
          props.favorited = false;
          await recipeService.deleteFavorite(props.recipe.id);
        } catch (error) {
          Pop.error(error);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.imgSize {
  height: 20rem;
}
</style>
