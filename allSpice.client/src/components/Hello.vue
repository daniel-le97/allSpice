<template>
  <div class="col-4">
    <div class="mb-3">
      <label for="ingredientName" class="form-label">ingredient</label>
      <input
        type="text"
        class="form-control"
        id="ingredientName"
        v-model="editableIngredient.name"
      />
    </div>
    <div class="mb-3">
      <label for="ingredientQuantity" class="form-label">quantity</label>
      <input
        type="text"
        class="form-control"
        id="ingredientQuantity"
        v-model="editableIngredient.quantity"
      />
    </div>
    <div>
      <button
        class="btn btn-primary"
        @click="addIngredient(editableIngredient)"
      >
        add
      </button>
    </div>
  </div>
  <div class="col-8 mb-5" v-if="ingredients">
    <ul>
      <li v-for="i in ingredients">
        <IngredientDetail :ingredient="i" />
      </li>
    </ul>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { ref } from "vue";
import { AppState } from "../AppState.js";
import { Ingredient } from "../models/Ingredient.js";
// import IngredientInput from "./ModalFolder/IngredientInput.vue";
import IngredientDetail from "./RecipesFolder/IngredientDetail.vue";

export default {
  setup() {
    const editableIngredient = ref({});

    return {
      editableIngredient,
      ingredients: computed(() => AppState.newRecipeIngredients),
      async addIngredient(ingredientData) {
        try {
          AppState.newRecipeIngredients.push(new Ingredient(ingredientData));
          console.log(AppState.newRecipeIngredients);
          editableIngredient.value = {};
        } catch (error) {
          Pop.error(error);
        }
      },
    };
  },
  components: { IngredientDetail },
};
</script>

<style lang="scss" scoped></style>
