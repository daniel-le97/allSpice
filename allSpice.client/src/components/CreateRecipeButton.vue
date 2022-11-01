<template>
  <button
    class="rounded-circle btn btn-success buttonFix"
    @click="resetActive()"
    data-bs-toggle="modal"
    data-bs-target="#createRecipeModal"
  >
    <i class="mdi mdi-plus"></i>
  </button>
</template>

<script>
import { AppState } from "../AppState.js";

export default {
  setup() {
    return {
      resetActive() {
        AppState.activeRecipe = null;
        AppState.activeRecipeIngredients = [];
        AppState.newRecipeIngredients = [];
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.buttonFix {
  position: fixed;
  bottom: 4rem;
  right: 4rem;
}
</style>
