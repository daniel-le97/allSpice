<template>
  <form>
    <div class="mb-3">
      <label for="recipeTitle" class="form-label">title</label>
      <input type="text" class="form-control" id="recipeTitle" />
    </div>
    <div class="mb-3">
      <label for="recipeCategory" class="form-label">category</label>
      <input type="text" class="form-control" id="recipeCategory" />
    </div>
    <div class="mb-3">
      <label for="recipeImg" class="form-label"
        >give your recipe an image!</label
      >
      <input type="url" class="form-control" id="recipeImg" />
    </div>
    <div class="mb-3">
      <label for="recipeInstruction" class="form-label">Instructions</label>
      <textarea class="form-control" id="recipeInstruction" rows="3"></textarea>
    </div>
  </form>
</template>

<script>
export default {
  setup() {
    return {};
  },
};
</script>

<style lang="scss" scoped></style>
