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
  <button class="rounded-circle position-absolute buttonFix">add</button>
</template>

<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Banner from '../components/Banner.vue';
import RecipeCard from '../components/RecipesFolder/RecipeCard.vue';
import { recipeService } from '../services/RecipesService.js';
import { ingredientsService } from '../services/IngredientsService.js';
import Pop from '../utils/Pop.js';

export default {
    setup() {
      async function getRecipes(){
        try {
            await recipeService.getRecipes()
          } catch (error) {
            Pop.error(error)
          }
      }
      onMounted(() =>{
        getRecipes()
      })
        return {
          recipes: computed(() => AppState.recipes),
          async getIngredients(recipeId){
              try {
             await ingredientsService.getIngredients(recipeId)
              } catch (error) {
                 Pop.error(error)
              }
          }
        };
    },
    components: { Banner, RecipeCard }
};
</script>

<style scoped lang="scss">
.buttonFix {
  position: fixed;
  bottom: -50px;
}
</style>
