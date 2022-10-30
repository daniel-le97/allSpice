<template>
  <div class="component container">
    <div class="row">
      <div class="col-6">
        <div class="row">
          <div class="col-10">
            <img :src="recipe?.img" alt="" class="img-fluid" />
          </div>
        </div>
      </div>
      <div class="col-6" v-show="ingredients">
        <IngredientDetail
          v-for="i in ingredients"
          :key="i.id"
          :ingredient="i"
        />
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

<style lang="scss" scoped></style>
