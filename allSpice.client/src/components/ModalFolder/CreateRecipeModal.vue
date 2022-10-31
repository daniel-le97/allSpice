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
          <RecipeInput/>
        </div>
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
          >
            Close
          </button>
          <button type="button" class="btn btn-primary">edit</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from "vue";
import RecipeInput from "./RecipeInput.vue";

export default {
  setup() {
    const editable = ref({});
    return {
      editable,
    };
  },
};
</script>

<style lang="scss" scoped></style>
