<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-md-10">
        <Banner />
      </div>
    </div>
    <div class="row mt-4">
      <div
        class="col-md-3"
        ref="scrollComponent"
        v-for="recipe in recipes"
        :key="recipe.id"
        :class="!recipe ? 'skeleton-loader' : ''"
      >
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
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";
import { AuthService } from "../services/AuthService.js";
import { onAuthLoaded } from "@bcwdev/auth0provider-client";
import { ingredientsService } from "../services/IngredientsService";
import CreateRecipeButton from "../components/CreateRecipeButton.vue";
import {hubsService} from "../services/HubsService.js"

export default {
  setup() {
    async function getRecipes() {
      try {
        let offset = AppState.offset;

        await recipeService.getRecipes(offset);
        // offset += 12;
      } catch (error) {
        Pop.error(error);
      }
    }
    // NOTE this is for my static fav array not to draw recipes
    async function getFavorites() {
      try {
        await accountService.getMyFavorites();
      } catch (error) {
        Pop.error(error);
      }
    }
     async function hub(){
      try {
          await hubsService.start()
        } catch (error) {
          console.error('[]',error)
          Pop.error(error)
        }
     
      }

    onMounted(() => {
 
      getRecipes();
      infiniteScroll();
    });
    onAuthLoaded(() => {
      getFavorites();
    });
    async function getCurrentRecipes() {
      // setTimeout(1000);
      let num = AppState.favNumber;
      let offset = AppState.offset;
      console.log(offset);
      if (num == 0) {
        await recipeService.getRecipes(offset);
      }
      if (num == 1) {
        await accountService.getFavoriteRecipes(offset);
      }
      if (num == 2) {
        await accountService.getMyRecipes(offset);
      }
    }
    function infiniteScroll() {
      window.onscroll = (e) => {
        let bottomOfWindow =
          document.documentElement.scrollTop + window.innerHeight ===
          document.documentElement.offsetHeight;
        if (bottomOfWindow) {
          getCurrentRecipes();
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
