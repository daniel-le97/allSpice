<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <Banner />
    </div>
    <div class="row mt-4">
      <div class="col-3" v-for="recipe in recipes" :key="recipe.id">
        <RecipeCard
          :recipe="recipe"
          class="m-3"
          @click="getIngredients(recipe.id)"
        />
      </div>
    </div>
  </div>
  <CreateRecipeButton />
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, onUnmounted, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import Banner from "../components/Banner.vue";
import RecipeCard from "../components/RecipesFolder/RecipeCard.vue";
import { recipeService } from "../services/RecipesService.js";
import { ingredientsService } from "../services/IngredientsService.js";
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";
import { AuthService } from "../services/AuthService.js";
import { onAuthLoaded } from "@bcwdev/auth0provider-client";
import CreateRecipeButton from "../components/CreateRecipeButton.vue";
import { ref } from "vue";

export default {
  setup() {
    async function getRecipes() {
      try {
        await recipeService.getRecipes();
      } catch (error) {
        Pop.error(error);
      }
    }
    async function getFavoriteRecipes() {
      try {
        await accountService.getMyFavorites();
      } catch (error) {
        Pop.error(error);
      }
    }

    onMounted(() => {
      getRecipes();
      infiniteScroll();
    });
    onAuthLoaded(() => {
      getFavoriteRecipes();
    });

    function infiniteScroll() {
      window.onscroll = () => {
        let bottomOfWindow =
          document.documentElement.scrollTop + window.innerHeight ===
          document.documentElement.offsetHeight;

        if (bottomOfWindow) {
          console.log("hi");
        }
      };
    }

    return {
      recipes: computed(() => AppState.recipes),
      async getIngredients(recipeId) {
        try {
          AppState.activeRecipeIngredients = [];
          console.log(recipeId);
          await ingredientsService.getIngredients(recipeId);
        } catch (error) {
          Pop.error(error);
        }
      },
    };
  },
  components: { Banner, RecipeCard, CreateRecipeButton },
};
</script>

<style scoped lang="scss"></style>
