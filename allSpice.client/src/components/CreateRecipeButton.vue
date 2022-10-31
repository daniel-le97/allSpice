<template>
  <button
    class="rounded-circle btn btn-success buttonFix "
    data-bs-toggle="modal"
    data-bs-target="#createRecipeModal"
  >
    <i class="mdi mdi-plus"></i>
  </button>
</template>

<script>
export default {
  setup() {
    return {};
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
