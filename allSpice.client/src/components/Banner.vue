<template>
  <div class="col-11 p-0 mt-3 position-relative">
    <img
      src="https://plus.unsplash.com/premium_photo-1663126351065-741a1d338b5d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2832&q=80"
      alt=""
      class="img img-fluid elevation-1 rounded"
    />
    <div class="row">
      <div
        class="col-8 bg-white position-absolute overlay rounded d-flex justify-content-between elevation-5"
      >
        <button class="btn no-select selectable" @click="getAllRecipes">
          home
        </button>
        <button class="btn no-select selectable" @click="getMyRecipes">
          recipes
        </button>
        <button class="btn no-select selectable" @click="getFavorites">
          favorites
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import { accountService } from "../services/AccountService.js";
import { recipeService } from "../services/RecipesService.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    return {
      // offset: computed(() => AppState.offset),
      async getFavorites() {
        try {
          let offset = AppState.offset;
          offset = 0;
          AppState.favNumber = 1;

          AppState.recipes = []
          await accountService.getFavoriteRecipes(offset);
        } catch (error) {
          Pop.error(error);
        }
      },
      async getAllRecipes() {
        try {
          let offset = AppState.offset;
          offset = 0;
          AppState.favNumber = 0;

          await recipeService.getRecipes(offset);
        } catch (error) {
          Pop.error(error);
        }
      },
      async getMyRecipes() {
        try {
          let offset = AppState.offset;
          offset = 0;
          AppState.favNumber = 2;

          await accountService.getMyRecipes(offset);
        } catch (error) {
          Pop.error(error);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.img {
  width: 100%;
  height: 25vh;
  object-fit: cover;
}

.overlay {
  bottom: -20px;
  right: 190px;
}
</style>
