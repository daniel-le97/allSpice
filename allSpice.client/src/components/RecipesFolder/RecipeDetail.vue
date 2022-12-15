<template>
  <div class="component container">
    <div class="row">
      <div class="col-6">
        <img
          :src="recipe?.img"
          alt=""
          class="img-fluid rounded elevation-1 img"
        />
      </div>
      <div class="col-6" v-if="ingredients">
        <div>
          <span class="fs-4">
            Ingredients
          </span>
        </div>
        <IngredientDetail
          v-for="i in ingredients"
          :key="i.id"
          :ingredient="i"
        />
      </div>
      <div class="col-6" v-else>
        <div>
          <span class="fs-4">
            No details found
          </span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { Recipe } from "../../models/Recipe.js";
import { Ingredient } from "../../models/Ingredient.js";
import IngredientDetail from "./IngredientDetail.vue";
import { computed } from "@vue/reactivity";
import { AppState } from "../../AppState.js";

export default {
  props: {
    recipe: { type: Recipe, required: true },
    // ingredient: { type: Ingredient, required: true },
  },
  setup() {
    return {
      ingredients: computed(() => AppState.activeRecipeIngredients),
    };
  },
  components: { IngredientDetail },
};
</script>

<style lang="scss" scoped>
.img {
  height: 20rem;
  min-width: 20rem;
  max-width: 20rem;
}
</style>
