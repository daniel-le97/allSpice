<template>
  <div>{{ ingredient.name }}</div>
  <div>{{ ingredient.quantity }}</div>
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
