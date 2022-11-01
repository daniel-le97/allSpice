<template>
  <form @submit.prevent="handleRecipeSubmit()">
    <div
      class="d-flex justify-content-between gap-2 position-absolute buttons mb-3 me-3"
    >
      <button
        type="button"
        class="btn btn-secondary"
        data-bs-dismiss="modal"
        aria-label="Close"
      >
        close
      </button>
      <button type="submit" class="btn btn-primary">submit</button>
    </div>
    <div class="row">
      <div class="col-md-6">
        <div class="mb-3">
          <label for="recipeTitle" class="form-label">title</label>
          <input
            type="text"
            class="form-control"
            id="recipeTitle"
            v-model="editable.title"
            required
          />
        </div>
        <div class="mb-3">
          <label for="recipeCategory" class="form-label">category</label>
          <input
            type="text"
            class="form-control"
            id="recipeCategory"
            v-model="editable.category"
          />
        </div>
        <div class="mb-3">
          <label for="recipeImg" class="form-label"
            >give your recipe an image!</label
          >
          <input
            type="url"
            class="form-control"
            id="recipeImg"
            v-model="editable.img"
          />
        </div>
        <div class="mb-3">
          <label for="recipeInstruction" class="form-label">Instructions</label>
          <textarea
            class="form-control"
            id="recipeInstruction"
            rows="3"
            v-model="editable.instructions"
          ></textarea>
        </div>
      </div>
      <div class="col-md-6 d-flex justify-content-center">
        <div class="bg-dark img-card rounded">
          <img
            :src="editable.img"
            alt=""
            class="img-fluid"
            v-if="editable.img"
          />
          <div class="text-center" v-else><span>no image yet</span></div>
        </div>
      </div>
    </div>
  </form>
  <div class="row border-top mt-1 position-relative">
    <Hello />
  </div>
</template>

<script>
import { ref } from "vue";
import { AppState } from "../../AppState.js";
import { ingredientsService } from "../../services/IngredientsService.js";
import { recipeService } from "../../services/RecipesService.js";
import Pop from "../../utils/Pop.js";
import Hello from "../Hello.vue";

export default {
  setup() {
    const editable = ref({});
    return {
      editable,
      async handleRecipeSubmit() {
        try {
          editable.value.category = editable.value.category.toLowerCase();
          // console.log(editable.value);
          const recipeNew = await recipeService.createRecipe(editable.value);

          editable.value = {};
          for await (const ingredient of AppState.newRecipeIngredients) {
            ingredient.recipeId = recipeNew.id;
            const newIngredients = await ingredientsService.createIngredient(
              ingredient
            );
          }
          // console.log(AppState.activeRecipeIngredients);
          AppState.newRecipeIngredients = [];
          // AppState.modalForm++
        } catch (error) {
          Pop.error(error);
        }
      },
    };
  },
  components: { Hello },
};
</script>

<style lang="scss" scoped>
.img-card {
  min-height: 20rem;
  width: 20rem;
}
.buttons {
  bottom: 0;
  right: 0;
  z-index: 10;
}
</style>
