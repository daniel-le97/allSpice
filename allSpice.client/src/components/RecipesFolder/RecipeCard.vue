<template>
  <div class="card text-bg-dark" @click="makeActive(recipe)" data-bs-toggle="modal" data-bs-target="#exampleModal">
    <img :src="recipe.img" class="card-img img-fluid imgSize" alt="..." />
    <div class="card-img-overlay">
      <h5 class="card-title">{{ recipe.title }}</h5>
      <p class="card-text">{{ recipe.instructions }}</p>
      <p class="card-text">
        <small>{{ recipe.category }}</small>
      </p>
    </div>
  </div>
  <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
  Launch demo modal
</button> -->
</template>

<script>
import { AppState } from "../../AppState.js";
import { Recipe } from "../../models/Recipe.js";

export default {
  props: {
    recipe: { type: Recipe, required: true },
  },
  setup() {
    return {
      makeActive(recipeData) {
        if (AppState.activeRecipe != recipeData) {
          
          AppState.activeRecipe = recipeData;
        }
        console.log(AppState.activeRecipe);
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
