<template>
  <div class="d-flex gap-2">
    <div>{{ ingredient.quantity }}</div>
    <div class="fw-bold">{{ ingredient.name }}</div>
  </div>
</template>

<script>
import { Ingredient } from "../../models/Ingredient.js";

export default {
  props: {
    ingredient: { type: Ingredient, required: true },
  },
  setup() {
    return {};
  },
};
</script>

<style lang="scss" scoped></style>
