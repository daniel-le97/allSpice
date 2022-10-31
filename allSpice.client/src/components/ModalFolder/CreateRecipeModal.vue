<template>
  <div
    class="modal fade"
    id="createRecipeModal"
    tabindex="-1"
    aria-labelledby="createRecipeModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-xl modal-dialog-centered modal-scrollable">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="exampleModalLabel"></h1>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <RecipeInput v-if="modalForm == 0" />
          <IngredientInput v-if="modalForm == 1"/>
        </div>
       
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { ref } from "vue";
import { AppState } from "../../AppState.js";
import IngredientInput from "./IngredientInput.vue";
import RecipeInput from "./RecipeInput.vue";

export default {
  setup() {
    const editable = ref({});
    return {
      editable,
      modalForm: computed(() => AppState.modalForm),
    };
  },
  components: { RecipeInput, IngredientInput },
};
</script>

<style lang="scss" scoped></style>
