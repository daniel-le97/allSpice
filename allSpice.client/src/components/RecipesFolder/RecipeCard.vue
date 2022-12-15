<template>
  <div class="card text-shadow skelton-loader">
    <img :src="recipe.img" class="card-img img-fluid imgSize" alt="..." />
    <div class="card-img-overlay p-1 px-2 event">
      <div class="d-flex justify-content-between index">
        <i
          class="mdi mdi-heart text-danger fs-3"
          @click="deleteFavorite(favorited, index)"
          v-if="favorited"
        ></i>
        <i class="mdi mdi-heart fs-3" @click="favoriteRecipe()" v-else></i>
      </div>
      <div class="d-flex flex-column justify-content-center align-content-center">
        <h5 class="card-title">{{ recipe.title }}</h5>
        <p class="card-text">{{ recipe.instructions }}</p>
        <p class="card-text">
          <small>{{ recipe.category }}</small>
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../../AppState.js";
import { Recipe } from "../../models/Recipe.js";
import { favoritesService } from "../../services/FavoritesService.js";
import { recipeService } from "../../services/RecipesService.js";
import Pop from "../../utils/Pop.js";
export default {
  props: {
    recipe: { type: Recipe, required: true },
  },
  setup(props) {
    return {
      favorited: computed(() => {
        let favorited = AppState.favorites.find(
          (f, index) => f.recipeId == props.recipe.id
        );
        if (favorited) {
          let recipe = AppState.recipes.find((r) => r.id == favorited.recipeId);
          recipe.favorited = true;
          recipe.favoriteId = favorited.id;
        }
        return favorited;
      }),
      makeActive(recipeData) {
        if (AppState.activeRecipe != recipeData) {
          AppState.activeRecipe = recipeData;
        }
      },
      async favoriteRecipe() {
        try {
          AppState.favoriting = true
          await favoritesService.favoriteRecipe(props.recipe);
          AppState.favoriting = false
        } catch (error) {
          Pop.error(error);
        }
      },
      async deleteFavorite(favorited, index) {
        try {
          AppState.favoriting = true
          const yes = await Pop.confirm();
          if (!yes) {
            AppState.favoriting = false
            return;
          }
          // console.log(favorited, index);
          let favoriteId = this.favorited.id;
          await favoritesService.deleteFavorite(favoriteId);
          AppState.favoriting = false
          Pop.success(`${props.recipe.title} removed from favorites`);
        } catch (error) {
          Pop.error(error);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.index {
  z-index: 9999 !important;
}
.imgSize {
  height: 20rem;
}
.event {
  background: rgba(204, 243, 253, 0.2);
  border: 1px solid rgba(86, 199, 251, 0.2);
  backdrop-filter: blur(2px);
  /* Note: backdrop-filter has minimal browser support */

  border-radius: 3px;
}
.event:hover {
  background: rgba(204, 243, 253, 0.2);
  border: 1px solid rgba(86, 199, 251, 0.2);
  backdrop-filter: blur(0px);

  border-radius: 3px;
}
.text-shadow {
  color: aliceblue;
  text-shadow: 1px 1px black, 0px 0px 5px salmon;
  font-weight: bold;
  letter-spacing: 0.08rem;

  /* Second Color  in text-shadow is the blur */
}
</style>
