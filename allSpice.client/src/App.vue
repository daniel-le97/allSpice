<template>
  <header>
    <!-- <Navbar /> -->
  </header>
  <main>
    <router-view />
  </main>
  <RecipeModal />
  <CreateRecipeModal />
</template>

<script>
import { computed } from "vue";
import { AppState } from "./AppState";
import Navbar from "./components/Navbar.vue";
import RecipeModal from "./components/ModalFolder/RecipeModal.vue";
import CreateRecipeModal from "./components/ModalFolder/CreateRecipeModal.vue";

export default {
  setup() {
    return {
      appState: computed(() => AppState),
    };
  },
  components: { Navbar, RecipeModal, CreateRecipeModal },
};
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}

.modal-open {
  overflow-y: scroll !important;
  margin: 0 !important;
  padding: 0 !important;
}
</style>
