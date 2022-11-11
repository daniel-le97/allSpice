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
        v-if="modalForm == 0"
      >
        add
      </button>
      <button
        class="btn btn-primary"
        @click="addIngredient(editableIngredient)"
        v-else
      >
        add
      </button>
    </div>
  </div>
  <div class="col-8 mb-5">
    <ul v-if="modalForm == 0">
      <li v-for="(newIngredient, index) in newIngredients" :key="index">
        <i class="mdi mdi-delete" @click.stop="removeIngredient(index)"></i>
        <IngredientDetail :ingredient="ingredient" />
      </li>
    </ul>
    <ul v-else>
      <li v-for="(ingredient, index) in ingredients" :key="ingredient.id">
        <i
          class="mdi mdi-delete"
          @click="deleteIngredient(index, ingredient.id)"
        ></i>
        <IngredientDetail :ingredient="ingredient" />
      </li>
    </ul>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { ref } from "vue";
import { AppState } from "../AppState.js";
import { Ingredient } from "../models/Ingredient.js";
import { ingredientsService } from "../services/IngredientsService";
import Pop from "../utils/Pop";
// import IngredientInput from "./ModalFolder/IngredientInput.vue";
import IngredientDetail from "./RecipesFolder/IngredientDetail.vue";

export default {
  setup() {
    const editableIngredient = ref({});

    return {
      editableIngredient,
      modalForm: computed(() => AppState.modalForm),
      newIngredients: computed(() => AppState.newRecipeIngredients),
      ingredients: computed(() => AppState.activeRecipeIngredients),

      async addIngredient(ingredientData) {
        try {
          if (AppState.modalForm == 0) {
            AppState.newRecipeIngredients.push(new Ingredient(ingredientData));
            editableIngredient.value = {};
            return;
          }
          ingredientData = new Ingredient(ingredientData);
          ingredientData.recipeId = AppState.activeRecipe.id;
          // console.log(ingredientData);
          await ingredientsService.createIngredient(ingredientData)
        } catch (error) {
          Pop.error(error);
        }
      },
      removeIngredient(ingredientIndex) {
        AppState.newRecipeIngredients.splice(ingredientIndex, 1);
      },
      async deleteIngredient(index, ingID) {
        try {
          const yes = await Pop.confirm();
          if (!yes) {
            return;
          }
          await ingredientsService.deleteIngredient(index, ingID);
        } catch (error) {
          console.error("[]", error);
          Pop.error(error);
        }
      },
    };
  },
  components: { IngredientDetail },
};
</script>

<style lang="scss" scoped></style>
